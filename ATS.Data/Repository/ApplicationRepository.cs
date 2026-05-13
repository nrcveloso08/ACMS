using ATS.DTO;
using ATS.DTO.Application;
using ATS.DTO.LogEntry;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IApplicationRepository
    {
        Task<IList<Application>> GetAll(int max = 500000, bool? isdeleted = false);
        Task<Application> GetApplication(Guid id);
        Task<Application> AddApplication(Application application, string userName = "", string userEmail = "");
        Task<object> SetApplicationStatus(Guid applicationId, int statusId, string userName = "", string userEmail = "");
        Task<IList<Application>> GetApplicationsByApplicant(Guid applicantId);
        Task<IList<Application>> GetApplicationsByLocation(int locationId, int max = 500000);
        Task<IList<Application>> GetApplicationsByJobPostingId(int jobPostingId, int max = 500000);
        Task<IList<LogEntry>> GetStatusLog(Guid applicationId);
        Task<IList<Note>> GetApplicationNotes(Guid applicationId);
        Task<IList<Note>> AddApplicationNote(Guid applicationId, string userName = "", string note = "", string userEmail = "");
        Task<Application> GetRecentApplication(Guid applicantId);
    }

    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ApplicationRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<Application>> GetAll(int max = 500000, bool? isdeleted = false)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "max", max.ToString() },
                { "isdeleted", isdeleted.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<Application>>(
                "/v1/Application/GetAll",
                parameters
            );
        }

        public async Task<Application> GetApplication(Guid id)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<Application>(
                "/v1/Application/GetApplication",
                parameters
            );
        }

        public async Task<Application> AddApplication(
            Application application,
            string userName = "",
            string userEmail = "")
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            var endpoint =
                $"/v1/Application/AddApplication" +
                $"?userName={Uri.EscapeDataString(userName ?? string.Empty)}" +
                $"&userEmail={Uri.EscapeDataString(userEmail ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<Application, Application>(
                endpoint,
                application
            );
        }

        public async Task<object> SetApplicationStatus(
            Guid applicationId,
            int statusId,
            string userName = "",
            string userEmail = "")
        {
            var endpoint =
                $"/v1/Application/SetApplicationStatus" +
                $"?applicationId={Uri.EscapeDataString(applicationId.ToString())}" +
                $"&statusId={statusId}" +
                $"&userName={Uri.EscapeDataString(userName ?? string.Empty)}" +
                $"&userEmail={Uri.EscapeDataString(userEmail ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<object, object>(
                endpoint,
                null
            );
        }

        public async Task<IList<Application>> GetApplicationsByApplicant(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<Application>>(
                "/v1/Application/GetApplicationsByApplicant",
                parameters
            );
        }

        public async Task<IList<Application>> GetApplicationsByLocation(
            int locationId,
            int max = 500000)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "locationId", locationId.ToString() },
                { "max", max.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<Application>>(
                "/v1/Application/GetApplicationsByLocation",
                parameters
            );
        }

        public async Task<IList<Application>> GetApplicationsByJobPostingId(
            int jobPostingId,
            int max = 500000)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "jobPostingId", jobPostingId.ToString() },
                { "max", max.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<Application>>(
                "/v1/Application/GetApplicationsByJobPostingId",
                parameters
            );
        }

        public async Task<IList<LogEntry>> GetStatusLog(Guid applicationId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicationId", applicationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<LogEntry>>(
                "/v1/Application/GetStatusLog",
                parameters
            );
        }

        public async Task<IList<Note>> GetApplicationNotes(Guid applicationId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicationId", applicationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<Note>>(
                "/v1/Application/GetApplicationNotes",
                parameters
            );
        }

        public async Task<IList<Note>> AddApplicationNote(
            Guid applicationId,
            string userName = "",
            string note = "",
            string userEmail = "")
        {
            var endpoint =
                $"/v1/Application/AddApplicationNote" +
                $"?applicationId={Uri.EscapeDataString(applicationId.ToString())}" +
                $"&userName={Uri.EscapeDataString(userName ?? string.Empty)}" +
                $"&note={Uri.EscapeDataString(note ?? string.Empty)}" +
                $"&userEmail={Uri.EscapeDataString(userEmail ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<object, IList<Note>>(
                endpoint,
                null
            );
        }

        public async Task<Application> GetRecentApplication(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<Application>(
                "/v1/Application/GetRecentApplication",
                parameters
            );
        }
    }
}