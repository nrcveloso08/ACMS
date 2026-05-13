using ATS.DTO.EmploymentApplication;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IEmploymentApplicationRepository
    {
        Task<string> GenerateEncryptedLink(
            Guid applicantId,
            string userName,
            string userEmail);

        Task<IList<EmploymentApplicationExt>> GetAllSubmittedByApplicantId(
            Guid applicantId);
    }

    public class EmploymentApplicationRepository : IEmploymentApplicationRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public EmploymentApplicationRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<string> GenerateEncryptedLink(
            Guid applicantId,
            string userName,
            string userEmail)
        {
            string endpoint =
                $"/v1/EmploymentApplication/GenerateEncryptedLink" +
                $"?applicantId={Uri.EscapeDataString(applicantId.ToString())}" +
                $"&userName={Uri.EscapeDataString(userName ?? string.Empty)}" +
                $"&userEmail={Uri.EscapeDataString(userEmail ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<object, string>(
                endpoint,
                null
            );
        }

        public async Task<IList<EmploymentApplicationExt>> GetAllSubmittedByApplicantId(
            Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<EmploymentApplicationExt>>(
                "/v1/EmploymentApplication/GetAllSubmittedByApplicantId",
                parameters
            );
        }
    }
}