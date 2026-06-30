using ATS.DTO.Roster;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IRosterReconciliationRepository
    {
        Task<List<RosterReconciliation>> GetRosterReconciliations(string status);
        Task<bool> Update(RosterReconciliation roster);
        Task<List<RosterReconciliationLogs>> GetLogs(int? rosterReconciliationId);
        Task<List<RosterReconciliationLogs>> AddLogs(string rosterReconciliationId, string userName, string notes);
    }

    public class RosterReconciliationRepository : IRosterReconciliationRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public RosterReconciliationRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<List<RosterReconciliation>> GetRosterReconciliations(string status)
        {
            var parameters = new NameValueCollection
            {
                { "status", status }
            };

            return await _webServiceHelper.GetAsync<List<RosterReconciliation>>(
                "/v1/RosterReconciliation/GetAll",
                parameters
            );
        }

        public async Task<bool> Update(RosterReconciliation roster)
        {
            if (roster == null)
                throw new ArgumentNullException(nameof(roster));

            return await _webServiceHelper.PostAsync<RosterReconciliation, bool>(
                "/v1/RosterReconciliation/Update",
                roster
            );
        }

        public async Task<List<RosterReconciliationLogs>> GetLogs(int? rosterReconciliationId)
        {
            var parameters = new NameValueCollection
            {
                { "Id", rosterReconciliationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<List<RosterReconciliationLogs>>(
                "/v1/RosterReconciliation/GetLogs",
                parameters
            );
        }

        public async Task<List<RosterReconciliationLogs>> AddLogs(string rosterReconciliationId, string userName, string notes)
        {
            var endpoint =
                $"/v1/RosterReconciliation/AddLogs" +
                $"?rosterReconciliation_Id={Uri.EscapeDataString(rosterReconciliationId ?? string.Empty)}" +
                $"&userName={Uri.EscapeDataString(userName ?? string.Empty)}" +
                $"&notes={Uri.EscapeDataString(notes ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<object, List<RosterReconciliationLogs>>(
                endpoint,
                null
            );
        }
    }
}
