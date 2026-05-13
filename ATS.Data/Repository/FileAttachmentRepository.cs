using ATS.Service.WebServiceHelper;
using System.Net.Mail;

namespace ATS.Data.Repository
{
    public interface IFileAttachmentRepository
    {
        Task<Attachment> Get(int id);
        Task<bool> Delete(int id);
    }

    public class FileAttachmentRepository : IFileAttachmentRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public FileAttachmentRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<Attachment> Get(int id)
        {
            return await _webServiceHelper.GetAsync<Attachment>(
                $"/v1/FileAttachment/Attachment/{id}"
            );
        }

        public async Task<bool> Delete(int id)
        {
            return await _webServiceHelper.GetAsync<bool>(
                $"/v1/FileAttachment/DeleteAttachment/{id}"
            );
        }
    }
}