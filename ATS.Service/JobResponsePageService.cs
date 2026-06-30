using ATS.Data.Repository;
using ATS.DTO.Document;
using ATS.DTO.EmploymentApplication;
using ATS.DTO.Guatemala;
using ATS.DTO.JobResponse;
using ATS.DTO.Location;

namespace ATS.Service
{
    public interface IJobResponsePageService
    {
        Task<IEnumerable<GuatemalaJobResponse>> GetJobResponsesAsync(JobResponseFilters filters);

        Task<object> GetDropdownsAsync();

        Task<EmploymentApplicationExt> GetApplicationDetailsAsync(Guid applicantId);

        Task<IEnumerable<GuatemalaDocument>> GetDocumentsAsync(int empAppId);

        Task<IEnumerable<GuatemalaAcademicLevel>> GetAcademicLevelsAsync();

        Task<bool> SavePersonalInformationAsync(EmploymentApplication application, int empAppId);

        Task<bool> SaveEmploymentInformationAsync(EmploymentApplication application);

        Task<bool> SaveEducationHistoryAsync(EmploymentApplication application);

        Task<bool> SaveCharacterReferenceAsync(EmploymentApplication application);

        Task<bool> SaveLanguageSkillsAsync(EmploymentApplication application);

        Task<bool> SaveQuestionResponsesAsync(Guid applicantId, int empAppId, List<EmploymentApplicationQuestionResponse> responses);

        Task<bool> SaveLocaleAdditionalInfoAsync(int locationId, Guid applicantId, EmploymentApplication application);
    }

    /// <summary>
    /// Page/workflow oriented service for the Job Response (Employment Application)
    /// pages. Orchestrates the multi-section employment application flow - personal
    /// info, employment history, education history, character references, language
    /// skills, prescreen question responses, and country/locale specific additional
    /// info - delegating to <see cref="IJobResponseRepository"/> and the relevant
    /// locale repositories based on the applicant's location.
    /// </summary>
    public class JobResponsePageService : IJobResponsePageService
    {
        private readonly IJobResponseRepository _jobResponseRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IBogotaLocalRepository _bogotaLocalRepository;
        private readonly IGuatemalaLocalRepository _guatemalaLocalRepository;
        private readonly IHondurasLocalRepository _hondurasLocalRepository;
        private readonly IManilaLocalRepository _manilaLocalRepository;
        private readonly IMexicoLocalRepository _mexicoLocalRepository;

        public JobResponsePageService(
            IJobResponseRepository jobResponseRepository,
            ILocationRepository locationRepository,
            IBogotaLocalRepository bogotaLocalRepository,
            IGuatemalaLocalRepository guatemalaLocalRepository,
            IHondurasLocalRepository hondurasLocalRepository,
            IManilaLocalRepository manilaLocalRepository,
            IMexicoLocalRepository mexicoLocalRepository)
        {
            _jobResponseRepository = jobResponseRepository;
            _locationRepository = locationRepository;
            _bogotaLocalRepository = bogotaLocalRepository;
            _guatemalaLocalRepository = guatemalaLocalRepository;
            _hondurasLocalRepository = hondurasLocalRepository;
            _manilaLocalRepository = manilaLocalRepository;
            _mexicoLocalRepository = mexicoLocalRepository;
        }

        /// <summary>
        /// Pass-through to retrieve job responses (submitted employment applications) matching the supplied filters.
        /// </summary>
        public async Task<IEnumerable<GuatemalaJobResponse>> GetJobResponsesAsync(JobResponseFilters filters)
        {
            return await _jobResponseRepository.GetJobResponse(filters);
        }

        /// <summary>
        /// Pass-through to retrieve the dropdown lookup values used to render the Job Response form.
        /// </summary>
        public async Task<object> GetDropdownsAsync()
        {
            return await _jobResponseRepository.GetDropdowns();
        }

        /// <summary>
        /// Pass-through to retrieve the full employment application details for an applicant.
        /// </summary>
        public async Task<EmploymentApplicationExt> GetApplicationDetailsAsync(Guid applicantId)
        {
            return await _jobResponseRepository.GetDetails(applicantId);
        }

        /// <summary>
        /// Pass-through to retrieve the documents attached to an employment application.
        /// </summary>
        public async Task<IEnumerable<GuatemalaDocument>> GetDocumentsAsync(int empAppId)
        {
            return await _jobResponseRepository.GetDocuments(empAppId);
        }

        /// <summary>
        /// Pass-through to retrieve the academic level lookup values for the education history section.
        /// </summary>
        public async Task<IEnumerable<GuatemalaAcademicLevel>> GetAcademicLevelsAsync()
        {
            return await _jobResponseRepository.GetAcademicLevels();
        }

        /// <summary>
        /// Pass-through to save the personal information section of an employment application.
        /// </summary>
        public async Task<bool> SavePersonalInformationAsync(EmploymentApplication application, int empAppId)
        {
            return await _jobResponseRepository.SavePersonalInformation(application, empAppId);
        }

        /// <summary>
        /// Pass-through to save the employment history section of an employment application.
        /// </summary>
        public async Task<bool> SaveEmploymentInformationAsync(EmploymentApplication application)
        {
            return await _jobResponseRepository.SaveEmploymentInformation(application);
        }

        /// <summary>
        /// Pass-through to save the education history section of an employment application.
        /// </summary>
        public async Task<bool> SaveEducationHistoryAsync(EmploymentApplication application)
        {
            return await _jobResponseRepository.SaveEducationHistory(application);
        }

        /// <summary>
        /// Pass-through to save the character reference section of an employment application.
        /// </summary>
        public async Task<bool> SaveCharacterReferenceAsync(EmploymentApplication application)
        {
            return await _jobResponseRepository.SaveCharacterReference(application);
        }

        /// <summary>
        /// Pass-through to save the language skills section of an employment application.
        /// </summary>
        public async Task<bool> SaveLanguageSkillsAsync(EmploymentApplication application)
        {
            return await _jobResponseRepository.SaveLanguageSkills(application);
        }

        /// <summary>
        /// Pass-through to save prescreen/employment application question responses.
        /// </summary>
        public async Task<bool> SaveQuestionResponsesAsync(Guid applicantId, int empAppId, List<EmploymentApplicationQuestionResponse> responses)
        {
            return await _jobResponseRepository.SaveQuestionsResponses(applicantId, empAppId, responses);
        }

        /// <summary>
        /// Saves the country/locale specific additional information for an employment
        /// application by resolving the applicant's location and delegating to the
        /// matching locale repository (Bogota, Guatemala, Honduras, Manila, Mexico, ...).
        /// </summary>
        public async Task<bool> SaveLocaleAdditionalInfoAsync(int locationId, Guid applicantId, EmploymentApplication application)
        {
            var location = await _locationRepository.Get(locationId);
            var country = location?.Country ?? string.Empty;

            switch (country)
            {
                case "Colombia":
                    // TODO: Old KITT logic mapped EmploymentApplication fields to BogotaLocalRequirement
                    // before calling AddOrUpdateLocalRequirement. Needs field mapping decision.
                    throw new NotImplementedException("Bogota locale additional info mapping requires later business decision.");

                case "Guatemala":
                    // TODO: Old KITT logic mapped EmploymentApplication.GuatemalaAdditionalInfos /
                    // GuatemalaAdditionalNames to GuatemalaAdditionalInfo / GuatemalaAdditionalName
                    // before calling AddOrUpdateAdditionalInfo / AddOrUpdateLocalNames. Needs field mapping decision.
                    throw new NotImplementedException("Guatemala locale additional info mapping requires later business decision.");

                case "Honduras":
                    // TODO: Old KITT logic mapped EmploymentApplication fields to HondurasAdditionalInfo
                    // before calling AddOrUpdateAdditionalInfo. Needs field mapping decision.
                    throw new NotImplementedException("Honduras locale additional info mapping requires later business decision.");

                case "Philippines":
                    // TODO: Old KITT logic mapped EmploymentApplication fields to ManilaAdditionalInfo
                    // before calling AddOrUpdateAdditionalInfo. Needs field mapping decision.
                    throw new NotImplementedException("Manila locale additional info mapping requires later business decision.");

                case "Mexico":
                    // TODO: Old KITT logic mapped EmploymentApplication fields to MexicoAdditionalInfo
                    // before calling AddOrUpdateAdditionalInfo. Needs field mapping decision.
                    throw new NotImplementedException("Mexico locale additional info mapping requires later business decision.");

                default:
                    // No locale-specific additional info required for this location.
                    return true;
            }
        }
    }
}
