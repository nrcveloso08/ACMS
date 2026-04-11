using ATS.DTO;
using ATS.DTO.Vacancies;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IVacancyRepository
    {
        Task<ATSResult> GetAll(bool includeDeleted = false);
        Task<Vacancy> Get(int id);
        Task<Vacancy> Add(Vacancy vacancy);
        Task<Vacancy> Update(Vacancy vacancy);
    }

    public class VacancyRepository : IVacancyRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public VacancyRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<ATSResult> GetAll(bool includeDeleted = false)
        {
            var parameters = new NameValueCollection
            {
                { "includeDeleted", includeDeleted.ToString().ToLower() }
            };
            return await _webServiceHelper.GetAsync<ATSResult>("/v1/VacancyDefaultJobAd/GetAll", parameters);
        }

        public async Task<Vacancy> Get(int id)
        {
            var parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };
            return await _webServiceHelper.GetAsync<Vacancy>("/v1/VacancyDefaultJobAd/Get", parameters);
        }

        public async Task<Vacancy> Add(Vacancy vacancy)
        {
            return await _webServiceHelper.PostAsync<object, Vacancy>("/v1/VacancyDefaultJobAd/Add", vacancy);
        }

        public async Task<Vacancy> Update(Vacancy vacancy)
        {
            return await _webServiceHelper.PostAsync<object, Vacancy>("/v1/VacancyDefaultJobAd/Update", vacancy);
        }
    }
}