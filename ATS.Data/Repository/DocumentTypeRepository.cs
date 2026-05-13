using ATS.DTO.Document;
using ATS.Service.WebServiceHelper;

namespace ATS.Data.Repository
{
    public interface IDocumentTypeRepository
    {
        Task<IList<DocumentType>> GetDocumentTypes();
        Task<DocumentType> AddOrUpdate(DocumentType documentType);
    }

    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public DocumentTypeRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<DocumentType>> GetDocumentTypes()
        {
            return await _webServiceHelper.GetAsync<IList<DocumentType>>(
                "/v1/DocumentType/DocumentTypes"
            );
        }

        public async Task<DocumentType> AddOrUpdate(DocumentType documentType)
        {
            if (documentType == null)
                throw new ArgumentNullException(nameof(documentType));

            return await _webServiceHelper.PostAsync<DocumentType, DocumentType>(
                "/v1/DocumentType/DocumentType",
                documentType
            );
        }
    }
}