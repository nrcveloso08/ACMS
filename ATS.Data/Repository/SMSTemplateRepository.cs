using ATS.DTO.SMS;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface ISMSTemplateRepository
    {
        Task<IList<SMSTemplate>> GetAll();
        Task<IList<SMSTemplate>> GetAllInactive();
        Task<bool> Save(SMSTemplate smsTemplate);
        Task<string> GetTags();
        Task<SMSTemplate> GetById(int? id);
        Task<IList<ModelSource>> GetModelSources();
        Task<bool> SaveModelSource(ModelSource modelSource);
        Task<ModelSource> GetModelSourceById(int? id);
        Task<IList<SMSCategory>> GetAllSMSCategory();
        Task<bool> ValidateTemplateName(string templateName, string templateId, string provider);
        Task<bool> UpdateWhatsAppTemplate();
    }

    public class SMSTemplateRepository : ISMSTemplateRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public SMSTemplateRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<bool> UpdateWhatsAppTemplate()
        {
            return await _webServiceHelper.GetAsync<bool>("/v1/SMS/UpdateWhatsAppTemplate");
        }

        public async Task<bool> ValidateTemplateName(string templateName, string templateId, string provider)
        {
            var parameters = new NameValueCollection
            {
                { "templateName", templateName },
                { "templateId", templateId },
                { "provider", provider }
            };

            return await _webServiceHelper.GetAsync<bool>("/v1/SMS/ValidateTemplateName", parameters);
        }

        public async Task<IList<SMSTemplate>> GetAll()
        {
            return await _webServiceHelper.GetAsync<IList<SMSTemplate>>("/v1/SMS/Templates");
        }

        public async Task<IList<SMSTemplate>> GetAllInactive()
        {
            return await _webServiceHelper.GetAsync<IList<SMSTemplate>>("/v1/SMS/Inactives");
        }

        public async Task<bool> Save(SMSTemplate smsTemplate)
        {
            if (smsTemplate == null)
                throw new ArgumentNullException(nameof(smsTemplate));

            return await _webServiceHelper.PostAsync<SMSTemplate, bool>("/v1/SMS/Template", smsTemplate);
        }

        public async Task<string> GetTags()
        {
            return await _webServiceHelper.GetAsync<string>("/v1/SMS/Tags");
        }

        public async Task<SMSTemplate> GetById(int? id)
        {
            return await _webServiceHelper.GetAsync<SMSTemplate>($"/v1/SMS/Template/{id}");
        }

        public async Task<IList<ModelSource>> GetModelSources()
        {
            return await _webServiceHelper.GetAsync<IList<ModelSource>>("/v1/SMS/ModelSources");
        }

        public async Task<bool> SaveModelSource(ModelSource modelSource)
        {
            if (modelSource == null)
                throw new ArgumentNullException(nameof(modelSource));

            return await _webServiceHelper.PostAsync<ModelSource, bool>("/v1/SMS/ModelSource", modelSource);
        }

        public async Task<ModelSource> GetModelSourceById(int? id)
        {
            return await _webServiceHelper.GetAsync<ModelSource>($"/v1/SMS/ModelSource/{id}");
        }

        public async Task<IList<SMSCategory>> GetAllSMSCategory()
        {
            return await _webServiceHelper.GetAsync<IList<SMSCategory>>("/v1/SMSCategory/GetAll");
        }
    }
}
