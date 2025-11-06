using ATS.Data.Repository;
using ATS.DTO.Applicant;
using ATS.DTO.Location;
using ATS.Service.Utilities;

namespace ATS.Service
{
    public interface ICandidateDetailService
    {
        Task<ApplicantDetails?> GetApplicantDetailsAsync(Guid? applicantId);
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

            // ✅ Use the generic mapper
            var applicantDetails = GenericMapper.Map<Applicant, ApplicantDetails>(applicant);

            // Initialize collections and defaults
            applicantDetails.PhoneNumbers = new List<ApplicantPhoneNumber>();
            applicantDetails.EmailAddresses = new List<ApplicantEmailAddress>();
            applicantDetails.ApplicantAddress = new ApplicantAddress();
            applicantDetails.PhoneMaskingFormat = "+1(999) 999-9999";
            applicantDetails.InternationalCode = "+1";

            var recentApplication = await _applicationRepository.GetRecentApplication(applicant.Id);
            var jobAd = recentApplication != null
                ? await _jobAdvertisementRepository.Get(recentApplication.JobAdvertismentId)
                : null;

            Location? location = await _locationRepository.Get(applicant.Location_Id);
            var dfStatus = applicant.Dayforce_Id != null
                ? await _dayforceService.GetEmployeeStatus(applicant.Dayforce_Id)
                : null;

            applicantDetails.PhoneNumbers = await _phoneNumberRepository.GetApplicantPhoneNumbers(applicant.Id) ?? new List<ApplicantPhoneNumber>();
            applicantDetails.EmailAddresses = await _emailRepository.GetByApplicant(applicant.Id) ?? new List<ApplicantEmailAddress>();
            applicantDetails.ApplicantAddress = await _addressRepository.GetByApplicant(applicant.Id) ?? new ApplicantAddress();

            applicantDetails.ApplicationCountry = (location?.Country?.ToLower() == "india")
                ? (location?.Name ?? string.Empty)
                : (location?.Country ?? string.Empty);
            applicantDetails.GeoLocationName = location?.GeoLocationName ?? string.Empty;
            applicantDetails.PhoneMaskingFormat = location?.PhoneMaskingFormat ?? "+1(999) 999-9999";
            applicantDetails.InternationalCode = location?.InternationalCode ?? "+1";

            applicantDetails.JobAdRequisitionId = jobAd?.RequisitionRequestId ?? 0;
            applicantDetails.HarverVacancyURL = jobAd?.HarverVacancyURL ?? string.Empty;
            applicantDetails.RecentApplicationId = recentApplication?.Id ?? Guid.Empty;
            applicantDetails.HarverProfileUrl = recentApplication?.HarverProfileUrl ?? string.Empty;
            applicantDetails.HarverVideoUrl = recentApplication?.HarverVideoUrl ?? string.Empty;

            if (dfStatus != null)
            {
                applicantDetails.EmploymentStatusGroupName = dfStatus.EmploymentStatusGroupName;
                applicantDetails.StatusReasonName = dfStatus.StatusReasonName;
            }

            string country = location?.Country ?? string.Empty;

            if (country == "Jamaica")
            {
                var localReq = await _jamaicaLocalService.GetRequirementByApplicant(applicant.Id);
                if (localReq != null)
                {
                    applicantDetails.JamaicaReqId = localReq.Id;
                    applicantDetails.TRN = localReq.TRN;
                    applicantDetails.NIS = localReq.NIS;
                    applicantDetails.IdNumber = localReq.IdNumber;
                    applicantDetails.IdType = localReq.IdType;
                }
            }

            if (country == "Guatemala")
            {
                var guatemalaInfo = await _guatemalaLocalService.GetAdditionalInfoByApplicant(applicant.Id);
                if (guatemalaInfo != null)
                    applicantDetails.BirthDate = applicant.BirthDate?.ToString() ?? string.Empty;
            }

            if (applicant.Location_Id == 37)
            {
                var hyderabadInfo = await _hyderabadLocalService.GetAdditionalInfoByApplicant(applicant.Id);
                if (hyderabadInfo != null)
                {
                    applicantDetails.EmployeeRole = hyderabadInfo.EmployeeRole;
                    applicantDetails.WaveCategory = int.TryParse(hyderabadInfo.WaveCategory, out int wave)
                        ? wave
                        : 0;
                    applicantDetails.GrossSalary = Convert.ToDecimal(hyderabadInfo.GrossSalary);
                    applicantDetails.HourlyRate = Convert.ToDecimal(hyderabadInfo.HourlyRate);
                    applicantDetails.HyderabadAddInfoId = hyderabadInfo.Id;
                }
            }

            return applicantDetails;
        }
    }
}
