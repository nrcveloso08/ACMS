using ATS.Data.Repository;
using ATS.DTO;
using ATS.DTO.JobAdvertisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service
{
    public interface IJobAdvertisementService
    {
        Task<List<JobAdvertisement>> GetAll();
        Task<JobAdvertisement> AddOrUpdate(JobAdvertisement obj);
    }

    public class JobAdvertisementService: IJobAdvertisementService
    {
        private readonly IJobAdvertisementRepository _jobAdvertisementRepository;

        public JobAdvertisementService(IJobAdvertisementRepository jobAdvertisementRepository)
        {
            _jobAdvertisementRepository = jobAdvertisementRepository;
        }

        public async Task<List<JobAdvertisement>> GetAll()
        {
            return await _jobAdvertisementRepository.GetAll();
        }

        public async Task<JobAdvertisement> AddOrUpdate(JobAdvertisement obj)
        {
           
            return await _jobAdvertisementRepository.AddOrUpdate(obj);
        }
    }
}
