using ATS.Data.Repository;
using ATS.DTO.Applicant;
using ATS.DTO.Application;
using ATS.DTO.Dayforce;
using ATS.DTO.JobAdvertisement;
using ATS.DTO.Location;
using ATS.Service.Utilities;

namespace ATS.Service
{
    public interface ICandidateDetailService
    {
        Task<ApplicantDetails?> GetApplicantDetailsAsync(Guid? applicantId);
        //Task<ApplicantDetailsVM> BuildEditViewModelAsync(Guid? applicantId);
    }

    public class CandidateDetailService : ICandidateDetailService
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobAdvertisementRepository _jobAdvertisementRepository;
        private readonly IApplicantPhoneNumberRepository _phoneNumberRepository;
        private readonly IApplicantEmailAddressRepository _emailRepository;
        private readonly IApplicantAddressRepository _addressRepository;
        private readonly IDayforceService _dayforceService;
        private readonly IJamaicaRepository _jamaicaLocalService;
        private readonly IGuatemalaRepository _guatemalaLocalService;
        private readonly IHyderabadRepository _hyderabadLocalService;

        public CandidateDetailService(
            IApplicantRepository applicantRepository,
            ILocationRepository locationRepository,
            IApplicationRepository applicationRepository,
            IJobAdvertisementRepository jobAdvertisementRepository,
            IApplicantPhoneNumberRepository phoneNumberRepository,
            IApplicantEmailAddressRepository emailRepository,
            IApplicantAddressRepository addressRepository,
            IDayforceService dayforceService,
            IJamaicaRepository jamaicaLocalRepository,
            IGuatemalaRepository guatemalaLocalRepository,
            IHyderabadRepository hyderabadLocalRepository)
        {
            _applicantRepository = applicantRepository;
            _locationRepository = locationRepository;
            _applicationRepository = applicationRepository;
            _jobAdvertisementRepository = jobAdvertisementRepository;
            _phoneNumberRepository = phoneNumberRepository;
            _emailRepository = emailRepository;
            _addressRepository = addressRepository;
            _dayforceService = dayforceService;
            _jamaicaLocalService = jamaicaLocalRepository;
            _guatemalaLocalService = guatemalaLocalRepository;
            _hyderabadLocalService = hyderabadLocalRepository;
        }


        public async Task<ApplicantDetails?> GetApplicantDetailsAsync(Guid? applicantId)
        {
            if (!applicantId.HasValue)
                return null;

            var applicant = await _applicantRepository.GetApplicant(applicantId.Value);
            if (applicant == null)
                return null;

            // Step 1: Initialize base details
            var applicantDetails = InitializeApplicantDetails(applicant);

            // Step 2: Load related data
            var (recentApplication, jobAd, location, dfStatus) = await LoadRelatedDataAsync(applicant);

            // Step 3: Populate basic contact and location info
            await PopulateContactDetailsAsync(applicantDetails, applicant.Id);
            PopulateLocationDetails(applicantDetails, location);
            PopulateJobAdDetails(applicantDetails, recentApplication, jobAd);
            PopulateEmploymentStatus(applicantDetails, dfStatus);

            // Step 4: Country and location-specific data
            await PopulateCountrySpecificDetailsAsync(applicantDetails, applicant, location);
            await PopulateLocationSpecificDetailsAsync(applicantDetails, applicant);

            return applicantDetails;
        }

        #region PRIVATE HELPERS

        private ApplicantDetails InitializeApplicantDetails(Applicant applicant)
        {
            var details = GenericMapper.Map<Applicant, ApplicantDetails>(applicant);

            details.PhoneNumbers = new List<ApplicantPhoneNumber>();
            details.EmailAddresses = new List<ApplicantEmailAddress>();
            details.ApplicantAddress = new ApplicantAddress();
            details.PhoneMaskingFormat = "+1(999) 999-9999";
            details.InternationalCode = "+1";

            return details;
        }

        private async Task<(Application? recentApplication, JobAdvertisement? jobAd, Location? location, DayforceEmployeeStatus? dfStatus)>
            LoadRelatedDataAsync(Applicant applicant)
        {
            var recentApplication = await _applicationRepository.GetRecentApplication(applicant.Id);
            var jobAd = recentApplication != null
                ? await _jobAdvertisementRepository.Get(recentApplication.JobAdvertismentId)
                : null;

            var location = await _locationRepository.Get(applicant.Location_Id);
            var dfStatus = applicant.Dayforce_Id != null
                ? await _dayforceService.GetEmployeeStatus(applicant.Dayforce_Id)
                : null;

            return (recentApplication, jobAd, location, dfStatus);
        }

        private async Task PopulateContactDetailsAsync(ApplicantDetails applicantDetails, Guid applicantId)
        {
            applicantDetails.PhoneNumbers = await _phoneNumberRepository.GetApplicantPhoneNumbers(applicantId)
                ?? new List<ApplicantPhoneNumber>();

            applicantDetails.EmailAddresses = await _emailRepository.GetByApplicant(applicantId)
                ?? new List<ApplicantEmailAddress>();

            applicantDetails.ApplicantAddress = await _addressRepository.GetByApplicant(applicantId)
                ?? new ApplicantAddress();
        }

        private void PopulateLocationDetails(ApplicantDetails applicantDetails, Location? location)
        {
            var country = location?.Country ?? string.Empty;

            applicantDetails.ApplicationCountry = country.Equals("india", StringComparison.OrdinalIgnoreCase)
                ? (location?.Name ?? string.Empty)
                : country;

            applicantDetails.GeoLocationName = location?.GeoLocationName ?? string.Empty;
            applicantDetails.PhoneMaskingFormat = location?.PhoneMaskingFormat ?? "+1(999) 999-9999";
            applicantDetails.InternationalCode = location?.InternationalCode ?? "+1";
        }

        private void PopulateJobAdDetails(ApplicantDetails applicantDetails, Application? recentApplication, JobAdvertisement? jobAd)
        {
            applicantDetails.JobAdRequisitionId = jobAd?.RequisitionRequestId ?? 0;
            applicantDetails.HarverVacancyURL = jobAd?.HarverVacancyURL ?? string.Empty;
            applicantDetails.RecentApplicationId = recentApplication?.Id ?? Guid.Empty;
            applicantDetails.HarverProfileUrl = recentApplication?.HarverProfileUrl ?? string.Empty;
            applicantDetails.HarverVideoUrl = recentApplication?.HarverVideoUrl ?? string.Empty;
        }

        private void PopulateEmploymentStatus(ApplicantDetails applicantDetails, DayforceEmployeeStatus? dfStatus)
        {
            if (dfStatus == null)
                return;

            applicantDetails.EmploymentStatusGroupName = dfStatus.EmploymentStatusGroupName;
            applicantDetails.StatusReasonName = dfStatus.StatusReasonName;
        }

        private async Task PopulateCountrySpecificDetailsAsync(ApplicantDetails applicantDetails, Applicant applicant, Location? location)
        {
            var country = location?.Country ?? string.Empty;

            switch (country)
            {
                case "Jamaica":
                    var localReq = await _jamaicaLocalService.GetRequirementByApplicant(applicant.Id);
                    if (localReq != null)
                    {
                        applicantDetails.JamaicaReqId = localReq.Id;
                        applicantDetails.TRN = localReq.TRN;
                        applicantDetails.NIS = localReq.NIS;
                        applicantDetails.IdNumber = localReq.IdNumber;
                        applicantDetails.IdType = localReq.IdType;
                    }
                    break;

                case "Guatemala":
                    var guatemalaInfo = await _guatemalaLocalService.GetAdditionalInfoByApplicant(applicant.Id);
                    if (guatemalaInfo != null)
                        applicantDetails.BirthDate = applicant.BirthDate?.ToString() ?? string.Empty;
                    break;
            }
        }

        private async Task PopulateLocationSpecificDetailsAsync(ApplicantDetails applicantDetails, Applicant applicant)
        {
            // Location_Id 37 => Hyderabad
            if (applicant.Location_Id != 37)
                return;

            var hyderabadInfo = await _hyderabadLocalService.GetAdditionalInfoByApplicant(applicant.Id);
            if (hyderabadInfo == null)
                return;

            applicantDetails.EmployeeRole = hyderabadInfo.EmployeeRole;
            applicantDetails.WaveCategory = int.TryParse(hyderabadInfo.WaveCategory, out int wave) ? wave : 0;
            applicantDetails.GrossSalary = Convert.ToDecimal(hyderabadInfo.GrossSalary);
            applicantDetails.HourlyRate = Convert.ToDecimal(hyderabadInfo.HourlyRate);
            applicantDetails.HyderabadAddInfoId = hyderabadInfo.Id;
        }
        #endregion
    }
}



