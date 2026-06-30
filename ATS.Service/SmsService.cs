using ATS.Data.Repository;

namespace ATS.Service
{
    public interface ISmsService
    {
        // TODO: Define SMS template CRUD / number assignment / WhatsApp sync orchestration methods.
    }

    public class SmsService : ISmsService
    {
        private readonly ISMSTemplateRepository _smsTemplateRepository;
        private readonly ISMSNumberRepository _smsNumberRepository;

        public SmsService(
            ISMSTemplateRepository smsTemplateRepository,
            ISMSNumberRepository smsNumberRepository)
        {
            _smsTemplateRepository = smsTemplateRepository;
            _smsNumberRepository = smsNumberRepository;
        }

        // TODO: Orchestrate SMS template CRUD, model source CRUD, WhatsApp sync, and
        // SMS number assignment using _smsTemplateRepository / _smsNumberRepository.
    }
}
