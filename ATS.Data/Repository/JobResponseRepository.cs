using ATS.DTO.Document;
using ATS.DTO.EmploymentApplication;
using ATS.DTO.Guatemala;
using ATS.DTO.JobResponse;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IJobResponseRepository
    {
        Task<IEnumerable<GuatemalaJobResponse>> GetJobResponse(JobResponseFilters filters);

        Task<IEnumerable<GuatemalaDocument>> GetDocuments(int empAppId);

        Task<object> GetDropdowns();

        Task<EmploymentApplicationExt> GetDetails(Guid applicantId);

        Task<bool> SavePersonalInformation(EmploymentApplication application, int empAppId);

        Task<IEnumerable<GuatemalaAcademicLevel>> GetAcademicLevels();

        Task<bool> SaveEmploymentInformation(EmploymentApplication application);

        Task<bool> SaveEducationHistory(EmploymentApplication application);

        Task<bool> SaveCharacterReference(EmploymentApplication application);

        Task<bool> SaveLanguageSkills(EmploymentApplication application);

        Task<bool> SaveQuestionsResponses(
            Guid applicantId,
            int empAppId,
            List<EmploymentApplicationQuestionResponse> application);

        Task<bool> SaveDocuments(
            Guid applicantId,
            int empAppId,
            GuatemalaDocument document,
            FileAttachment file);
    }

    public class JobResponseRepository : IJobResponseRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public JobResponseRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IEnumerable<GuatemalaJobResponse>> GetJobResponse(JobResponseFilters filters)
        {
            if (filters == null)
                throw new ArgumentNullException(nameof(filters));

            return await _webServiceHelper.PostAsync
                <JobResponseFilters, IEnumerable<GuatemalaJobResponse>>(
                    "/v1/JobResponse/JobResponse",
                    filters
                );
        }

        public async Task<IEnumerable<GuatemalaDocument>> GetDocuments(int empAppId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "empAppId", empAppId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IEnumerable<GuatemalaDocument>>(
                "/v1/JobResponse/GetDocuments",
                parameters
            );
        }

        public async Task<object> GetDropdowns()
        {
            return await _webServiceHelper.GetAsync<object>(
                "/v1/JobResponse/GetDropdownValues"
            );
        }

        public async Task<EmploymentApplicationExt> GetDetails(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<EmploymentApplicationExt>(
                "/v1/JobResponse/GetDetails",
                parameters
            );
        }

        public async Task<bool> SavePersonalInformation(EmploymentApplication application, int empAppId)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            string endpoint =
                $"/v1/JobResponse/SavePersonalInformation?empAppId={empAppId}";

            return await _webServiceHelper.PostAsync
                <EmploymentApplication, bool>(
                    endpoint,
                    application
                );
        }

        public async Task<IEnumerable<GuatemalaAcademicLevel>> GetAcademicLevels()
        {
            return await _webServiceHelper.GetAsync<IEnumerable<GuatemalaAcademicLevel>>(
                "/v1/JobResponse/GetAcademicLevels"
            );
        }

        public async Task<bool> SaveEmploymentInformation(EmploymentApplication application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            return await _webServiceHelper.PostAsync
                <EmploymentApplication, bool>(
                    "/v1/JobResponse/SaveEmploymentInformation",
                    application
                );
        }

        public async Task<bool> SaveEducationHistory(EmploymentApplication application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            return await _webServiceHelper.PostAsync
                <EmploymentApplication, bool>(
                    "/v1/JobResponse/SaveEducationInfo",
                    application
                );
        }

        public async Task<bool> SaveCharacterReference(EmploymentApplication application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            return await _webServiceHelper.PostAsync
                <EmploymentApplication, bool>(
                    "/v1/JobResponse/SaveCharacterReference",
                    application
                );
        }

        public async Task<bool> SaveLanguageSkills(EmploymentApplication application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            return await _webServiceHelper.PostAsync
                <EmploymentApplication, bool>(
                    "/v1/JobResponse/SaveLanguageSkills",
                    application
                );
        }

        public async Task<bool> SaveQuestionsResponses(
            Guid applicantId,
            int empAppId,
            List<EmploymentApplicationQuestionResponse> application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            string endpoint =
                $"/v1/JobResponse/SaveQuestionResponses" +
                $"?applicantId={applicantId}" +
                $"&empAppId={empAppId}";

            return await _webServiceHelper.PostAsync
                <List<EmploymentApplicationQuestionResponse>, bool>(
                    endpoint,
                    application
                );
        }

        public Task<bool> SaveDocuments(
            Guid applicantId,
            int empAppId,
            GuatemalaDocument document,
            FileAttachment file)
        {
            // TODO: Requires DB/business decision - see ATS_BACKEND_ARCHITECTURE.md
            // Old KITT implementation read uploaded files from HttpFileCollectionBase
            // (ASP.NET Framework) and converted them into a FileAttachment before
            // posting to /v1/JobResponse/AddDocument. File handling/upload strategy
            // for ATS.Api needs to be decided before this can be wired up.
            throw new NotImplementedException("SaveDocuments requires later DB/API decision.");
        }
    }
}
