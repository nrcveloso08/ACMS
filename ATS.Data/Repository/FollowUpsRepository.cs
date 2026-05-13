using ATS.DTO.FollowUp;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IFollowUpsRepository
    {
        Task<bool> AddOrUpdateDisposition(FollowUpDisposition followUpDisposition);

        Task<IList<FollowUpDisposition>> GetDispositionByLocation(int locationId);

        Task<IList<FollowUpDisposition>> GetDispositionByLocationIDs(IList<int> locationIds);

        Task<bool> AddOrUpdateFollowUp(
            FollowUpApplicant followUpApplicant,
            string user,
            string userEmail);

        Task<bool> BulkComplete(
            IList<int> ids,
            string user,
            string userEmail);

        Task<IList<FollowUpApplicant>> GetApplicantFollowUpsByLocation(
            int locationId,
            bool pendingOnly = true);

        Task<IList<FollowUpApplicant>> GetFilteredData(
            FollowUpFilter filter);

        Task<FollowUpItemsByApplicant> GetApplicantFollowUpsByApplicantId(
            Guid applicantId,
            bool pendingOnly = true);

        Task<object> GetFollowUpStatus();
    }

    public class FollowUpsRepository : IFollowUpsRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public FollowUpsRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<bool> AddOrUpdateDisposition(
            FollowUpDisposition followUpDisposition)
        {
            if (followUpDisposition == null)
                throw new ArgumentNullException(nameof(followUpDisposition));

            return await _webServiceHelper.PostAsync
                <FollowUpDisposition, bool>(
                    "/v1/FollowUps/AddOrUpdateDisposition",
                    followUpDisposition
                );
        }

        public async Task<IList<FollowUpDisposition>> GetDispositionByLocation(
            int locationId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "locationId", locationId.ToString() }
            };

            return await _webServiceHelper.GetAsync
                <IList<FollowUpDisposition>>(
                    "/v1/FollowUps/GetDispositionByLocation",
                    parameters
                );
        }

        public async Task<IList<FollowUpDisposition>> GetDispositionByLocationIDs(
            IList<int> locationIds)
        {
            if (locationIds == null)
                throw new ArgumentNullException(nameof(locationIds));

            return await _webServiceHelper.PostAsync
                <IList<int>, IList<FollowUpDisposition>>(
                    "/v1/FollowUps/GetDispositionByLocationIDs",
                    locationIds
                );
        }

        public async Task<bool> AddOrUpdateFollowUp(
            FollowUpApplicant followUpApplicant,
            string user,
            string userEmail)
        {
            if (followUpApplicant == null)
                throw new ArgumentNullException(nameof(followUpApplicant));

            string endpoint =
                $"/v1/FollowUps/AddOrUpdateFollowUp" +
                $"?user={Uri.EscapeDataString(user ?? string.Empty)}" +
                $"&userEmail={Uri.EscapeDataString(userEmail ?? string.Empty)}";

            return await _webServiceHelper.PostAsync
                <FollowUpApplicant, bool>(
                    endpoint,
                    followUpApplicant
                );
        }

        public async Task<bool> BulkComplete(
            IList<int> ids,
            string user,
            string userEmail)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            string endpoint =
                $"/v1/FollowUps/BulkComplete" +
                $"?user={Uri.EscapeDataString(user ?? string.Empty)}" +
                $"&userEmail={Uri.EscapeDataString(userEmail ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<IList<int>, bool>(
                endpoint,
                ids
            );
        }

        public async Task<IList<FollowUpApplicant>> GetApplicantFollowUpsByLocation(
            int locationId,
            bool pendingOnly = true)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "locationId", locationId.ToString() },
                { "pendingOnly", pendingOnly.ToString() }
            };

            return await _webServiceHelper.GetAsync
                <IList<FollowUpApplicant>>(
                    "/v1/FollowUps/GetApplicantFollowUpsByLocation",
                    parameters
                );
        }

        public async Task<IList<FollowUpApplicant>> GetFilteredData(
            FollowUpFilter filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            return await _webServiceHelper.PostAsync
                <FollowUpFilter, IList<FollowUpApplicant>>(
                    "/v1/FollowUps/GetFilteredData",
                    filter
                );
        }

        public async Task<FollowUpItemsByApplicant> GetApplicantFollowUpsByApplicantId(
            Guid applicantId,
            bool pendingOnly = true)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() },
                { "pendingOnly", pendingOnly.ToString() }
            };

            return await _webServiceHelper.GetAsync
                <FollowUpItemsByApplicant>(
                    "/v1/FollowUps/GetApplicantFollowUpsByApplicantId",
                    parameters
                );
        }

        public async Task<object> GetFollowUpStatus()
        {
            return await _webServiceHelper.GetAsync<object>(
                "/v1/FollowUps/GetFollowUpStatus"
            );
        }
    }
}