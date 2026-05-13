using ATS.DTO.Application;
using ATS.DTO.ClientDetail;
using ATS.DTO.FCRA;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IFCRARepository
    {
        Task<bool> AddOrUpdate(
            Guid applicantId,
            FCRAStatus status,
            string userName,
            string note);

        Task<ApplicationPaginationProperties> GetFilteredData(
            DTO.FCRA.EndpointParameters.FCRAFilters filter);

        Task<FCRASentForReview> AddOrUpdateSRInformation(
            FCRASentForReview srInformation,
            string userName);

        Task<FCRASentForReview> GetSRInformationByFCRA(int fcraId);

        Task<IList<FCRANote>> GetNotesByApplicantId(Guid applicantId);

        Task<IList<FCRANote>> GetNotesByFCRAId(int fcraId);

        Task<IList<Client>> GetClients();
    }

    public class FCRARepository : IFCRARepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public FCRARepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<bool> AddOrUpdate(
            Guid applicantId,
            FCRAStatus status,
            string userName,
            string note)
        {
            string endpoint =
                $"/v1/FCRA/AddOrUpdate" +
                $"?applicantId={Uri.EscapeDataString(applicantId.ToString())}" +
                $"&status={(int)status}" +
                $"&userName={Uri.EscapeDataString(userName ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<string, bool>(
                endpoint,
                note ?? string.Empty
            );
        }

        public async Task<ApplicationPaginationProperties> GetFilteredData(
            DTO.FCRA.EndpointParameters.FCRAFilters filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            return await _webServiceHelper.PostAsync
                <DTO.FCRA.EndpointParameters.FCRAFilters, ApplicationPaginationProperties>(
                    "/v1/FCRA/GetFilteredData",
                    filter
                );
        }

        public async Task<FCRASentForReview> AddOrUpdateSRInformation(
            FCRASentForReview srInformation,
            string userName)
        {
            if (srInformation == null)
                throw new ArgumentNullException(nameof(srInformation));

            string endpoint =
                $"/v1/FCRA/AddOrUpdateSRInformation" +
                $"?userName={Uri.EscapeDataString(userName ?? string.Empty)}";

            return await _webServiceHelper.PostAsync
                <FCRASentForReview, FCRASentForReview>(
                    endpoint,
                    srInformation
                );
        }

        public async Task<FCRASentForReview> GetSRInformationByFCRA(int fcraId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "fcraId", fcraId.ToString() }
            };

            return await _webServiceHelper.GetAsync<FCRASentForReview>(
                "/v1/FCRA/GetSRInformationByFCRA",
                parameters
            );
        }

        public async Task<IList<FCRANote>> GetNotesByApplicantId(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<FCRANote>>(
                "/v1/FCRA/GetNotesByApplicantId",
                parameters
            );
        }

        public async Task<IList<FCRANote>> GetNotesByFCRAId(int fcraId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "fcraId", fcraId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<FCRANote>>(
                "/v1/FCRA/GetNotesByFCRAId",
                parameters
            );
        }

        public async Task<IList<Client>> GetClients()
        {
            return await _webServiceHelper.GetAsync<IList<Client>>(
                "/v1/FCRA/Clients"
            );
        }
    }
}