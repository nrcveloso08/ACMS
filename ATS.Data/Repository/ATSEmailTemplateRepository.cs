using ATS.DTO.Email;
using ATS.Service.WebServiceHelper;

namespace ATS.Data.Repository
{
    public interface IATSEmailTemplateRepository
    {
        Task<IList<EmailTemplate>> GetByATSStatus(int statusId);
    }

    public class ATSEmailTemplateRepository : IATSEmailTemplateRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ATSEmailTemplateRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public Task<IList<EmailTemplate>> GetByATSStatus(int statusId)
        {
            // TODO: Requires DB/business decision - see ATS_BACKEND_ARCHITECTURE.md
            // Old KITT implementation resolves StatusActionMap entries (KITT DB) and then
            // filters EmailTemplates (KITT DB) by the mapped template ids - no ATSWebService endpoint involved.
            throw new NotImplementedException("GetByATSStatus requires later DB/API decision.");
        }
    }
}
