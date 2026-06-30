using ATS.Data.Repository;
using ATS.DTO.Dayforce;
using ATS.DTO.EmploymentApplication;
using ATS.DTO.FCRA;
using ATS.DTO.HR;
using ATS.DTO.Interview;

namespace ATS.Service
{
    public interface IHRProcessingService
    {
        Task<IList<ApplicantForHR>> GetApplicants(ApplicantHRProcessFilter filter);

        Task<HRProcessingDetails?> GetProcessingDetailsAsync(Guid applicantId);

        Task<IList<HRCheckListItem>> GetApplicantCheckListAsync(Guid applicantId);

        Task<HRCheckResponse> AddOrUpdateCheckResponseAsync(HRCheckResponse hrCheckResponse);

        Task<bool> CompleteHRProcessingAsync(Guid applicantId);
    }

    /// <summary>
    /// Page/workflow oriented service for the HR Processing screen. Orchestrates
    /// HR checklist data together with Dayforce employee status, FCRA status and
    /// medical results, and submitted employment application data for a candidate.
    /// </summary>
    public class HRProcessingService : IHRProcessingService
    {
        private readonly IHRRepository _hrRepository;
        private readonly IDayforceRepository _dayforceRepository;
        private readonly IMedicalResultRepository _medicalResultRepository;
        private readonly IFCRARepository _fcraRepository;
        private readonly IFCRADocumentRepository _fcraDocumentRepository;
        private readonly IEmploymentApplicationRepository _employmentApplicationRepository;

        public HRProcessingService(
            IHRRepository hrRepository,
            IDayforceRepository dayforceRepository,
            IMedicalResultRepository medicalResultRepository,
            IFCRARepository fcraRepository,
            IFCRADocumentRepository fcraDocumentRepository,
            IEmploymentApplicationRepository employmentApplicationRepository)
        {
            _hrRepository = hrRepository;
            _dayforceRepository = dayforceRepository;
            _medicalResultRepository = medicalResultRepository;
            _fcraRepository = fcraRepository;
            _fcraDocumentRepository = fcraDocumentRepository;
            _employmentApplicationRepository = employmentApplicationRepository;
        }

        /// <summary>
        /// Pass-through to retrieve the list of applicants pending/being processed by HR for a location/status filter.
        /// </summary>
        public async Task<IList<ApplicantForHR>> GetApplicants(ApplicantHRProcessFilter filter)
        {
            return await _hrRepository.GetApplicants(filter);
        }

        /// <summary>
        /// Builds the HR Processing details view for a single applicant by combining the HR
        /// checklist responses with Dayforce employment status, FCRA status/notes and any
        /// medical results and submitted employment applications on file.
        /// </summary>
        public async Task<HRProcessingDetails?> GetProcessingDetailsAsync(Guid applicantId)
        {
            var checkList = await _hrRepository.GetApplicantCheckList(applicantId);
            var checkResponses = await _hrRepository.GetCheckResponsesByApplicantId(applicantId);
            var fcraNotes = await _fcraRepository.GetNotesByApplicantId(applicantId);
            var medicalResult = await _medicalResultRepository.GetByApplicantId(applicantId);
            var submittedApplications = await _employmentApplicationRepository.GetAllSubmittedByApplicantId(applicantId);

            var details = new HRProcessingDetails
            {
                ApplicantId = applicantId,
                CheckListItems = checkList,
                CheckResponses = checkResponses,
                FCRANotes = fcraNotes,
                MedicalResult = medicalResult,
                SubmittedEmploymentApplications = submittedApplications
            };

            // TODO: Old KITT logic resolved the Dayforce employee status using the
            // applicant's Dayforce_Id (fetched from ApplicantRepository). HRProcessingService
            // currently has no way to look up the applicant's Dayforce_Id without injecting
            // IApplicantRepository, which would extend the scope of this skeleton pass.
            // Wire this up once the page contract clarifies how the Dayforce_Id is supplied.
            details.DayforceEmployeeStatus = await GetDayforceStatusAsync(null);

            return details;
        }

        /// <summary>
        /// Pass-through to retrieve the HR checklist items applicable to an applicant.
        /// </summary>
        public async Task<IList<HRCheckListItem>> GetApplicantCheckListAsync(Guid applicantId)
        {
            return await _hrRepository.GetApplicantCheckList(applicantId);
        }

        /// <summary>
        /// Pass-through to add or update a single HR check response for an applicant.
        /// </summary>
        public async Task<HRCheckResponse> AddOrUpdateCheckResponseAsync(HRCheckResponse hrCheckResponse)
        {
            return await _hrRepository.AddOrUpdateCheckResponse(hrCheckResponse);
        }

        /// <summary>
        /// Pass-through to mark HR processing as complete for an applicant.
        /// </summary>
        public async Task<bool> CompleteHRProcessingAsync(Guid applicantId)
        {
            return await _hrRepository.CompleteHRProcessingByApplicant(applicantId);
        }

        #region PRIVATE HELPERS

        private async Task<DayforceEmployeeStatus?> GetDayforceStatusAsync(string? dayforceId)
        {
            if (string.IsNullOrWhiteSpace(dayforceId))
                return null;

            return await _dayforceRepository.GetEmployeeStatus(dayforceId);
        }

        #endregion
    }

    /// <summary>
    /// Aggregated view-model for the HR Processing details page.
    /// </summary>
    public class HRProcessingDetails
    {
        public Guid ApplicantId { get; set; }
        public IList<HRCheckListItem> CheckListItems { get; set; } = new List<HRCheckListItem>();
        public IList<HRCheckResponse> CheckResponses { get; set; } = new List<HRCheckResponse>();
        public IList<FCRANote> FCRANotes { get; set; } = new List<FCRANote>();
        public MedicalResult? MedicalResult { get; set; }
        public IList<EmploymentApplicationExt> SubmittedEmploymentApplications { get; set; } = new List<EmploymentApplicationExt>();
        public DayforceEmployeeStatus? DayforceEmployeeStatus { get; set; }
    }
}
