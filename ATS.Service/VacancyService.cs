using ATS.Data.Repository;
using ATS.DTO;
using ATS.DTO.Vacancies;

namespace ATS.Service
{
    public interface IVacancyService
    {
        Task<ATSResult> GetAll(bool includeDeleted = false);
        Task<Vacancy> Get(int id);
        Task<Vacancy> AddOrUpdate(Vacancy vacancy);
    }


public class VacancyService : IVacancyService
    {
        private readonly IVacancyRepository _vacancyRepository;

        public VacancyService(IVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public Task<ATSResult> GetAll(bool includeDeleted = false)
        {
            return _vacancyRepository.GetAll(includeDeleted);
        }

        public Task<Vacancy> Get(int id)
        {
            return _vacancyRepository.Get(id);
        }
        public async Task<Vacancy> AddOrUpdate(Vacancy vacancy)
        {
            if (vacancy.Id == 0)
                return await _vacancyRepository.Add(vacancy);
            else
                return await _vacancyRepository.Update(vacancy);
        }
    }
}
