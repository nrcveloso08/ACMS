using ATS.DTO.AdvertisementSource;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IAdvertisementSourceRepository
    {
        Task<List<AdvertisementSource>> GetAll();
        Task<AdvertisementSource> AddOrUpdate(AdvertisementSource obj);
        Task<object> Get(Guid id);
    }

    public class AdvertisementSourceRepository : IAdvertisementSourceRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public AdvertisementSourceRepository(IWebServiceHelper webServiceHelper)
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

        public async Task<List<AdvertisementSource>> GetAll()
        {
            return await _webServiceHelper.GetAsync<List<AdvertisementSource>>("/v1/AdvertisementSource/GetAll");
        }

        public async Task<AdvertisementSource> AddOrUpdate(AdvertisementSource obj)
        {
            string endpoint = string.Empty;
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (obj.Id == 0)
            {
                // Add
                endpoint = $"/v1/AdvertisementSource/Add?newName={Uri.EscapeDataString(obj.Name)}";
                return await _webServiceHelper.PostAsync<object, AdvertisementSource>(endpoint, null);
            }

            // Update
            endpoint = $"/v1/AdvertisementSource/Update?id={obj.Id}&newName={Uri.EscapeDataString(obj.Name)}";
            return await _webServiceHelper.PostAsync<object, AdvertisementSource>(endpoint, null);
        }





    }
}
