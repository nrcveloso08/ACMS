using ATS.DTO.ClientDetail;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IClientDetailRepository
    {
        Task<ClientDetail> GetClientDetails(Guid id);
        Task<ClientDetail> Add(ClientDetail clientDetail);
        Task<ClientDetail> Update(ClientDetail clientDetail);
        Task<bool> Delete(ClientDetail clientDetail);
    }

    public class ClientDetailRepository : IClientDetailRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ClientDetailRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<ClientDetail> GetClientDetails(Guid id)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<ClientDetail>(
                "/v1/ClientDetail/GetClientDetails",
                parameters
            );
        }

        public async Task<ClientDetail> Add(ClientDetail clientDetail)
        {
            if (clientDetail == null)
                throw new ArgumentNullException(nameof(clientDetail));

            return await _webServiceHelper.PostAsync<ClientDetail, ClientDetail>(
                "/v1/ClientDetail/Add",
                clientDetail
            );
        }

        public async Task<ClientDetail> Update(ClientDetail clientDetail)
        {
            if (clientDetail == null)
                throw new ArgumentNullException(nameof(clientDetail));

            return await _webServiceHelper.PostAsync<ClientDetail, ClientDetail>(
                "/v1/ClientDetail/Update",
                clientDetail
            );
        }

        public async Task<bool> Delete(ClientDetail clientDetail)
        {
            if (clientDetail == null)
                throw new ArgumentNullException(nameof(clientDetail));

            return await _webServiceHelper.PostAsync<ClientDetail, bool>(
                "/v1/ClientDetail/Delete",
                clientDetail
            );
        }
    }
}