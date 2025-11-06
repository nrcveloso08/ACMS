using ATS.Data.Repository;
using ATS.DTO;
using ATS.DTO.Vacancies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service
{
    public interface IHarverService
    {
        Task<ATSResult> GetAll();
        Task<Vacancy> AddOrUpdate(Vacancy obj);
    }

    public  class HarverService: IHarverService
    {
        private readonly IHarverRepository _harverRepository;

        public HarverService(IHarverRepository harverRepository)
        {
            _harverRepository = harverRepository;
        }

        public async Task<ATSResult> GetAll()
        {
            return await _harverRepository.GetAll();
        }
                
        public async Task<Vacancy> AddOrUpdate(Vacancy obj)
        {
            return await _harverRepository.AddOrUpdate(obj);
        }

    }
}
