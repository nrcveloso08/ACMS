using ATS.DTO.InterviewSchedule;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IInterviewStatusRepository
    {
        Task<IList<InterviewStatus>> GetNextInterviewStatuses(InterviewStatus currentStatus, IList<int> locationIds);
        Task<IList<InterviewStatusDTO>> GetNextInterviewStatusesWithDispositions(InterviewStatus currentStatus, int locationId);
        Task<IList<StatusDisposition>> GetAllStatusDispositions();
        Task<IList<StatusDisposition>> GetStatusDispositions(InterviewStatus status, int locationId);
        Task<IList<StatusDisposition>> GetStatusDispositions(InterviewStatus status, IList<int> locationIds);
        Task<bool> AddOrUpdateStatusDisposition(StatusDisposition disposition);
        Task<IList<InterviewStatus>> GetInterviewStatusesByLocation(int locationId);
        Task<IList<InterviewStatus>> GetInterviewStatusesByLocations(IList<int> locationIds);
        Task<IList<InterviewStatusLookup>> GetAllInterviewStatuses();
        Task<IList<InterviewStatusLookup>> GetActiveInterviewStatuses();
        Task<bool> SaveInterviewStatus(int id, string name, bool isDeleted);
        Task<IList<LocationWorkflow>> GetAllLocationWorkflows();
        Task<IList<InterviewStatusMatrix>> GetMatrixByWorkflowIdAndCurrentStatus(int workflowId, InterviewStatus currentStatus);
        Task<bool> SaveInterviewStatusMatrix(int workflowId, InterviewStatus currentStatus, List<InterviewStatus> nextStatuses);
        Task<IList<InterviewStatusMatrix>> GetAllStatusMatrix();
    }

    public class InterviewStatusRepository : IInterviewStatusRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public InterviewStatusRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<InterviewStatus>> GetNextInterviewStatuses(
            InterviewStatus currentStatus,
            IList<int> locationIds)
        {
            if (locationIds == null)
                throw new ArgumentNullException(nameof(locationIds));

            string endpoint =
                $"/v1/InterviewStatus/GetNextInterviewStatuses" +
                $"?currentStatus={(int)currentStatus}";

            return await _webServiceHelper.PostAsync<IList<int>, IList<InterviewStatus>>(
                endpoint,
                locationIds
            );
        }

        public async Task<IList<InterviewStatusDTO>> GetNextInterviewStatusesWithDispositions(
            InterviewStatus currentStatus,
            int locationId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "currentStatus", ((int)currentStatus).ToString() },
                { "locationId", locationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<InterviewStatusDTO>>(
                "/v1/InterviewStatus/GetNextInterviewStatusesWithDispositions",
                parameters
            );
        }

        public async Task<IList<StatusDisposition>> GetAllStatusDispositions()
        {
            return await _webServiceHelper.GetAsync<IList<StatusDisposition>>(
                "/v1/InterviewStatus/GetAllStatusDispositions"
            );
        }

        public async Task<IList<StatusDisposition>> GetStatusDispositions(
            InterviewStatus status,
            int locationId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "status", ((int)status).ToString() },
                { "locationId", locationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<StatusDisposition>>(
                "/v1/InterviewStatus/GetStatusDispositions",
                parameters
            );
        }

        public async Task<IList<StatusDisposition>> GetStatusDispositions(
            InterviewStatus status,
            IList<int> locationIds)
        {
            if (locationIds == null)
                throw new ArgumentNullException(nameof(locationIds));

            string endpoint =
                $"/v1/InterviewStatus/GetStatusDispositions" +
                $"?status={(int)status}";

            return await _webServiceHelper.PostAsync<IList<int>, IList<StatusDisposition>>(
                endpoint,
                locationIds
            );
        }

        public async Task<bool> AddOrUpdateStatusDisposition(StatusDisposition disposition)
        {
            if (disposition == null)
                throw new ArgumentNullException(nameof(disposition));

            return await _webServiceHelper.PostAsync<StatusDisposition, bool>(
                "/v1/InterviewStatus/AddOrUpdateStatusDisposition",
                disposition
            );
        }

        public async Task<IList<InterviewStatus>> GetInterviewStatusesByLocation(int locationId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "locationId", locationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<InterviewStatus>>(
                "/v1/InterviewStatus/GetInterviewStatusesByLocation",
                parameters
            );
        }

        public async Task<IList<InterviewStatus>> GetInterviewStatusesByLocations(IList<int> locationIds)
        {
            if (locationIds == null)
                throw new ArgumentNullException(nameof(locationIds));

            return await _webServiceHelper.PostAsync<IList<int>, IList<InterviewStatus>>(
                "/v1/InterviewStatus/GetInterviewStatusesByLocations",
                locationIds
            );
        }

        public async Task<IList<InterviewStatusLookup>> GetAllInterviewStatuses()
        {
            return await _webServiceHelper.GetAsync<IList<InterviewStatusLookup>>(
                "/v1/InterviewStatus/GetAllInterviewStatuses"
            );
        }

        public async Task<IList<InterviewStatusLookup>> GetActiveInterviewStatuses()
        {
            return await _webServiceHelper.GetAsync<IList<InterviewStatusLookup>>(
                "/v1/InterviewStatus/GetActiveInterviewStatuses"
            );
        }

        public async Task<bool> SaveInterviewStatus(int id, string name, bool isDeleted)
        {
            string endpoint =
                $"/v1/InterviewStatus/SaveInterviewStatus" +
                $"?id={id}" +
                $"&name={Uri.EscapeDataString(name ?? string.Empty)}" +
                $"&isDeleted={isDeleted}";

            return await _webServiceHelper.PostAsync<object, bool>(
                endpoint,
                null
            );
        }

        public async Task<IList<LocationWorkflow>> GetAllLocationWorkflows()
        {
            return await _webServiceHelper.GetAsync<IList<LocationWorkflow>>(
                "/v1/InterviewStatus/GetAllLocationWorkflows"
            );
        }

        public async Task<IList<InterviewStatusMatrix>> GetMatrixByWorkflowIdAndCurrentStatus(
            int workflowId,
            InterviewStatus currentStatus)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "workflowId", workflowId.ToString() },
                { "currentStatus", ((int)currentStatus).ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<InterviewStatusMatrix>>(
                "/v1/InterviewStatus/GetMatrixByWorkflowIdAndCurrentStatus",
                parameters
            );
        }

        public async Task<bool> SaveInterviewStatusMatrix(
            int workflowId,
            InterviewStatus currentStatus,
            List<InterviewStatus> nextStatuses)
        {
            if (nextStatuses == null)
                throw new ArgumentNullException(nameof(nextStatuses));

            string endpoint =
                $"/v1/InterviewStatus/SaveInterviewStatusMatrix" +
                $"?workflowId={workflowId}" +
                $"&currentStatus={(int)currentStatus}";

            return await _webServiceHelper.PostAsync<List<InterviewStatus>, bool>(
                endpoint,
                nextStatuses
            );
        }

        public async Task<IList<InterviewStatusMatrix>> GetAllStatusMatrix()
        {
            return await _webServiceHelper.GetAsync<IList<InterviewStatusMatrix>>(
                "/v1/InterviewStatus/GetAllStatusMatrix"
            );
        }
    }
}