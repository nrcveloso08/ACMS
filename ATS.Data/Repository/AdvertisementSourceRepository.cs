using ATS.DTO.AdvertisementSource;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IAdvertisementSourceRepository
    {
        Task<List<AdvertisementSource>> GetAll();
        Task<AdvertisementSource> AddOrUpdate(AdvertisementSource obj);
        Task<AdvertisementSource> Get(int id);
    }

    public class AdvertisementSourceRepository : IAdvertisementSourceRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public AdvertisementSourceRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<List<AdvertisementSource>> GetAll()
        {
            return await _webServiceHelper.GetAsync<List<AdvertisementSource>>(
                "/v1/AdvertisementSource/GetAll"
            );
        }

        public async Task<AdvertisementSource> AddOrUpdate(AdvertisementSource obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (obj.Id == 0)
            {
                return await Add(obj.Name);
            }

            return await Update(obj.Id, obj.Name);
        }

        private async Task<AdvertisementSource> Add(string newName)
        {
            var endpoint = $"/v1/AdvertisementSource/Add?newName={Uri.EscapeDataString(newName ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<object, AdvertisementSource>(
                endpoint,
                null
            );
        }

        private async Task<AdvertisementSource> Update(int id, string newName)
        {
            var endpoint = $"/v1/AdvertisementSource/Update?id={id}&newName={Uri.EscapeDataString(newName ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<object, AdvertisementSource>(
                endpoint,
                null
            );
        }

        public async Task<AdvertisementSource> Get(int id)
        {
            var items = await _webServiceHelper.GetAsync<List<AdvertisementSource>>(
                "/v1/AdvertisementSource/GetAll"
            );

            var result = items?.FirstOrDefault(x => x.Id == id);

            return result ?? new AdvertisementSource();
        }
    }
}