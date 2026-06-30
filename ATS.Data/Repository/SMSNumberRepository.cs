using ATS.DTO.SMS;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface ISMSNumberRepository
    {
        Task<IList<SMSNumber>> GetAll();
        Task<SMSNumber> Get(int id);
        Task<bool> Save(SMSNumber newRecord);
        Task<bool> Delete(int id);
    }

    public class SMSNumberRepository : ISMSNumberRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public SMSNumberRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<SMSNumber>> GetAll()
        {
            return await _webServiceHelper.GetAsync<IList<SMSNumber>>("/v1/SMSNumber/GetAll");
        }

        public async Task<SMSNumber> Get(int id)
        {
            var parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<SMSNumber>("/v1/SMSNumber/GetById", parameters);
        }

        public async Task<bool> Save(SMSNumber newRecord)
        {
            if (newRecord == null)
                throw new ArgumentNullException(nameof(newRecord));

            return await _webServiceHelper.PostAsync<SMSNumber, bool>("/v1/SMSNumber/AddOrupdate", newRecord);
        }

        public async Task<bool> Delete(int id)
        {
            var parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<bool>("/v1/SMSNumber/Delete", parameters);
        }
    }
}
