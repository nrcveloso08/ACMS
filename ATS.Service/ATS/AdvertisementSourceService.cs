using ATS.Service.ViewModels.AdvertisementSource;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service.ATS
{
    public interface IAdvertisementSourceService
    {
        Task<List<AdvertisementSourceVM>> GetAll();
        Task<AdvertisementSourceVM> AddOrUpdate(AdvertisementSourceVM obj);
        Task<object> Get(Guid id);
    }

    public class AdvertisementSourceService : IAdvertisementSourceService
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public AdvertisementSourceService(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<object> Get(Guid id)
        {
            NameValueCollection parameters = new NameValueCollection {
                        {"id", id.ToString() }
                    };
            return await _webServiceHelper.GetAsync<object>("/v1/Applicant/GetApplicant", parameters);
        }

        public async Task<List<AdvertisementSourceVM>> GetAll()
        {
            return await _webServiceHelper.GetAsync<List<AdvertisementSourceVM>>("/v1/AdvertisementSource/GetAll");
        }

        public async Task<AdvertisementSourceVM> AddOrUpdate(AdvertisementSourceVM obj)
        {
            string endpoint = string.Empty;
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (obj.Id == 0)
            {
                // Add
                endpoint = $"/v1/AdvertisementSource/Add?newName={Uri.EscapeDataString(obj.Name)}";
                return await _webServiceHelper.PostAsync<object, AdvertisementSourceVM>(endpoint, null);
            }

            // Update
            endpoint = $"/v1/AdvertisementSource/Update?id={obj.Id}&newName={Uri.EscapeDataString(obj.Name)}";
            return await _webServiceHelper.PostAsync<object, AdvertisementSourceVM>(endpoint, null);
        }





    }
}
