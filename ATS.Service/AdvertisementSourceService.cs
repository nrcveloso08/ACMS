using ATS.Data.Repository;
using ATS.DTO.AdvertisementSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service
{
    public interface IAdvertisementSourceService
    {
        Task<List<AdvertisementSource>> GetAll();
        Task<AdvertisementSource> AddOrUpdate(AdvertisementSource obj);
        Task<object> Get(int id);
    }
    
    public class AdvertisementSourceService: IAdvertisementSourceService
    {
        private readonly IAdvertisementSourceRepository _advertisementSourceRepository;

        public AdvertisementSourceService(IAdvertisementSourceRepository advertisementSourceRepository)
        {
            _advertisementSourceRepository = advertisementSourceRepository;
        }

        public async Task<List<AdvertisementSource>> GetAll()
        {
            return await _advertisementSourceRepository.GetAll();
        }

        public async Task<AdvertisementSource> AddOrUpdate(AdvertisementSource obj)
        {
            return await _advertisementSourceRepository.AddOrUpdate(obj);
        }

        public async Task<object> Get(int id)
        {
            return await _advertisementSourceRepository.Get(id);
        }

    }
}
