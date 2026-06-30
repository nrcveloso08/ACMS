using ATS.DTO.LogEntry;
using ATS.DTO.RequisitionRequest;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IRosterCSIRepository
    {
        Task<LogEntry> GetLatestCSINote(Guid applicantId);

        Task<bool> SaveCSINote(
            string applicantId,
            int requisitionId,
            string note,
            string userName,
            string userEmail);

        Task<bool> UpdateRequisitionAssignment(
            RequisitionAssignment requisitionAssignment,
            string applicantId,
            string userName,
            string userEmail);
    }

    public class RosterCSIRepository : IRosterCSIRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public RosterCSIRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<LogEntry> GetLatestCSINote(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<LogEntry>(
                "/v1/CSI/GetLatestCSINote",
                parameters
            );
        }

        public async Task<bool> SaveCSINote(
            string applicantId,
            int requisitionId,
            string note,
            string userName,
            string userEmail)
        {
            string endpoint =
                $"/v1/CSI/SaveCSINote" +
                $"?applicantId={Uri.EscapeDataString(applicantId ?? string.Empty)}" +
                $"&requisitionId={requisitionId}" +
                $"&note={Uri.EscapeDataString(note ?? string.Empty)}" +
                $"&userName={Uri.EscapeDataString(userName ?? string.Empty)}" +
                $"&userEmail={Uri.EscapeDataString(userEmail ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<object, bool>(
                endpoint,
                null
            );
        }

        public async Task<bool> UpdateRequisitionAssignment(
            RequisitionAssignment requisitionAssignment,
            string applicantId,
            string userName,
            string userEmail)
        {
            if (requisitionAssignment == null)
                throw new ArgumentNullException(nameof(requisitionAssignment));

            string endpoint =
                $"/v1/CSI/UpdateRequisitionAssignment" +
                $"?applicantId={Uri.EscapeDataString(applicantId ?? string.Empty)}" +
                $"&userName={Uri.EscapeDataString(userName ?? string.Empty)}" +
                $"&userEmail={Uri.EscapeDataString(userEmail ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<RequisitionAssignment, bool>(
                endpoint,
                requisitionAssignment
            );
        }
    }
}
