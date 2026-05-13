using ATS.DTO.FCRA;
using ATS.Service.WebServiceHelper;

namespace ATS.Data.Repository
{
    public interface IFCRADocumentRepository
    {
        Task<IList<FCRADocument>> GetAllByFCRAId(int id);
        Task<bool> AddOrUpdate(FCRADocument obj);
        Task<FCRADocument> Get(int id);
    }

    public class FCRADocumentRepository : IFCRADocumentRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public FCRADocumentRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<FCRADocument>> GetAllByFCRAId(int id)
        {
            return await _webServiceHelper.GetAsync<IList<FCRADocument>>(
                $"/v1/FCRADocument/Documents/{id}"
            );
        }

        public async Task<bool> AddOrUpdate(FCRADocument obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            return await _webServiceHelper.PostAsync<FCRADocument, bool>(
                "/v1/FCRADocument/Document",
                obj
            );
        }

        public async Task<FCRADocument> Get(int id)
        {
            return await _webServiceHelper.GetAsync<FCRADocument>(
                $"/v1/FCRADocument/Document/{id}"
            );
        }
    }
}