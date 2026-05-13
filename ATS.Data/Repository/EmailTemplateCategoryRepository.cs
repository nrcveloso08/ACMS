using ATS.DTO.Email;
using ATS.Service.WebServiceHelper;

namespace ATS.Data.Repository
{
    public interface IEmailTemplateCategoryRepository
    {
        Task<IEnumerable<EmailTemplateCategory>> GetAll();
    }

    public class EmailTemplateCategoryRepository : IEmailTemplateCategoryRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public EmailTemplateCategoryRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IEnumerable<EmailTemplateCategory>> GetAll()
        {
            return await _webServiceHelper.GetAsync<IEnumerable<EmailTemplateCategory>>(
                "/v1/EmailTempalteCategory/Categories"
            );
        }
    }
}