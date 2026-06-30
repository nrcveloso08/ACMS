using ATS.Data.Repository;
using ATS.DTO;
using ATS.DTO.AdvertisementSource;
using ATS.DTO.Application;
using ATS.DTO.Email;
using ATS.DTO.HiringSource;
using ATS.DTO.InterviewSchedule;
using ATS.DTO.JobAdvertisement;
using ATS.DTO.PrescreenQuestion;
using ATS.DTO.SMS;

namespace ATS.Service
{
    public interface IATSConfigService
    {
        Task<IList<StatusActionMap>> GetStatusActionMapAsync(int statusId);

        Task<IList<EmailTemplate>> GetEmailTemplatesByStatusAsync(int statusId);

        Task<IList<SMSTemplate>> GetSMSTemplatesAsync();

        Task<IList<SMSNumber>> GetSMSNumbersAsync();

        Task<IEnumerable<InterviewType>> GetInterviewTypesAsync(bool includeDeleted = false);

        Task<IList<InterviewStatusLookup>> GetInterviewStatusesAsync(bool activeOnly = true);

        Task<IEnumerable<EmployeeStatus>> GetEmployeeStatusesAsync();

        Task<List<AdvertisementSource>> GetAdvertisementSourcesAsync();

        Task<IList<HiringSourceLocation>> GetHiringSourcesByLocationAsync(int locationId);

        Task<IList<PrescreeningQuestionsGroup>> GetPrescreeningQuestionGroupsAsync();

        Task<List<JobAdvertisement>> GetJobAdvertisementsAsync();
    }

    /// <summary>
    /// Page/workflow oriented service that provides the configuration lookups used
    /// across the ATS admin/configuration screens (status-action map, email/SMS
    /// templates, interview types/statuses, employee statuses, advertisement and
    /// hiring sources, prescreening question groups, and job advertisements).
    /// Mostly thin pass-throughs to the corresponding repositories' "get all" style methods.
    /// </summary>
    public class ATSConfigService : IATSConfigService
    {
        private readonly IStatusActionMapRepository _statusActionMapRepository;
        private readonly IATSEmailTemplateRepository _atsEmailTemplateRepository;
        private readonly ISMSTemplateRepository _smsTemplateRepository;
        private readonly ISMSNumberRepository _smsNumberRepository;
        private readonly IInterviewTypeRepository _interviewTypeRepository;
        private readonly IInterviewStatusRepository _interviewStatusRepository;
        private readonly IEmployeeStatusRepository _employeeStatusRepository;
        private readonly IAdvertisementSourceRepository _advertisementSourceRepository;
        private readonly IHiringSourceRepository _hiringSourceRepository;
        private readonly IPrescreeningQuestionGroupsRepository _prescreeningQuestionGroupsRepository;
        private readonly IJobAdvertisementRepository _jobAdvertisementRepository;

        public ATSConfigService(
            IStatusActionMapRepository statusActionMapRepository,
            IATSEmailTemplateRepository atsEmailTemplateRepository,
            ISMSTemplateRepository smsTemplateRepository,
            ISMSNumberRepository smsNumberRepository,
            IInterviewTypeRepository interviewTypeRepository,
            IInterviewStatusRepository interviewStatusRepository,
            IEmployeeStatusRepository employeeStatusRepository,
            IAdvertisementSourceRepository advertisementSourceRepository,
            IHiringSourceRepository hiringSourceRepository,
            IPrescreeningQuestionGroupsRepository prescreeningQuestionGroupsRepository,
            IJobAdvertisementRepository jobAdvertisementRepository)
        {
            _statusActionMapRepository = statusActionMapRepository;
            _atsEmailTemplateRepository = atsEmailTemplateRepository;
            _smsTemplateRepository = smsTemplateRepository;
            _smsNumberRepository = smsNumberRepository;
            _interviewTypeRepository = interviewTypeRepository;
            _interviewStatusRepository = interviewStatusRepository;
            _employeeStatusRepository = employeeStatusRepository;
            _advertisementSourceRepository = advertisementSourceRepository;
            _hiringSourceRepository = hiringSourceRepository;
            _prescreeningQuestionGroupsRepository = prescreeningQuestionGroupsRepository;
            _jobAdvertisementRepository = jobAdvertisementRepository;
        }

        /// <summary>
        /// Pass-through to the status-action map repository.
        /// TODO: StatusActionMapRepository.GetMap is currently a NotImplementedException
        /// skeleton pending a DB/API decision - this propagates that until resolved.
        /// </summary>
        public async Task<IList<StatusActionMap>> GetStatusActionMapAsync(int statusId)
        {
            return await _statusActionMapRepository.GetMap(statusId);
        }

        /// <summary>
        /// Pass-through to the ATS email template repository.
        /// TODO: ATSEmailTemplateRepository.GetByATSStatus is currently a NotImplementedException
        /// skeleton pending a DB/API decision - this propagates that until resolved.
        /// </summary>
        public async Task<IList<EmailTemplate>> GetEmailTemplatesByStatusAsync(int statusId)
        {
            return await _atsEmailTemplateRepository.GetByATSStatus(statusId);
        }

        /// <summary>
        /// Pass-through to retrieve all active SMS templates.
        /// </summary>
        public async Task<IList<SMSTemplate>> GetSMSTemplatesAsync()
        {
            return await _smsTemplateRepository.GetAll();
        }

        /// <summary>
        /// Pass-through to retrieve all configured SMS sender numbers.
        /// </summary>
        public async Task<IList<SMSNumber>> GetSMSNumbersAsync()
        {
            return await _smsNumberRepository.GetAll();
        }

        /// <summary>
        /// Pass-through to retrieve all interview types.
        /// </summary>
        public async Task<IEnumerable<InterviewType>> GetInterviewTypesAsync(bool includeDeleted = false)
        {
            return await _interviewTypeRepository.GetAllInterviewType(includeDeleted);
        }

        /// <summary>
        /// Pass-through to retrieve interview statuses, either all of them or only the active ones.
        /// </summary>
        public async Task<IList<InterviewStatusLookup>> GetInterviewStatusesAsync(bool activeOnly = true)
        {
            return activeOnly
                ? await _interviewStatusRepository.GetActiveInterviewStatuses()
                : await _interviewStatusRepository.GetAllInterviewStatuses();
        }

        /// <summary>
        /// Pass-through to retrieve all employee statuses.
        /// </summary>
        public async Task<IEnumerable<EmployeeStatus>> GetEmployeeStatusesAsync()
        {
            return await _employeeStatusRepository.GetAllEmployeeStatus();
        }

        /// <summary>
        /// Pass-through to retrieve all advertisement sources.
        /// </summary>
        public async Task<List<AdvertisementSource>> GetAdvertisementSourcesAsync()
        {
            return await _advertisementSourceRepository.GetAll();
        }

        /// <summary>
        /// Pass-through to retrieve hiring sources configured for a location.
        /// </summary>
        public async Task<IList<HiringSourceLocation>> GetHiringSourcesByLocationAsync(int locationId)
        {
            return await _hiringSourceRepository.GetByLocationId(locationId);
        }

        /// <summary>
        /// Pass-through to retrieve all prescreening question groups.
        /// TODO: PrescreeningQuestionGroupsRepository.GetAllGroups is currently a NotImplementedException
        /// skeleton pending a DB/API decision - this propagates that until resolved.
        /// </summary>
        public async Task<IList<PrescreeningQuestionsGroup>> GetPrescreeningQuestionGroupsAsync()
        {
            return await _prescreeningQuestionGroupsRepository.GetAllGroups();
        }

        /// <summary>
        /// Pass-through to retrieve all job advertisements.
        /// </summary>
        public async Task<List<JobAdvertisement>> GetJobAdvertisementsAsync()
        {
            return await _jobAdvertisementRepository.GetAll();
        }
    }
}
