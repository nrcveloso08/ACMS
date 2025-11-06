using ATS.DTO;
using ATS.DTO.Vacancies;
using ATS.Service.WebServiceHelper;

namespace ATS.Data.Repository   
{
    public interface IHarverRepository
    {
        Task<Vacancy> AddOrUpdate(Vacancy obj);
        Task<ATSResult> GetAll();
    }

    public class HarverRepository : IHarverRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public HarverRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<ATSResult> GetAll()
        {
            return await _webServiceHelper.GetAsync<ATSResult>("/v1/VacancyDefaultJobAd/GetAll");
        }

        public async Task<Vacancy> AddOrUpdate(Vacancy obj)
        {
            string endpoint = string.Empty;
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (obj.Id == 0)
            {
                // ✅ Add new record
                endpoint = "/v1/VacancyDefaultJobAd/Add";
                return await _webServiceHelper.PostAsync<object, Vacancy>(endpoint, obj);
            }
            else
            {
                // ✅ Update existing record
                endpoint = "/v1/VacancyDefaultJobAd/Update";
                return await _webServiceHelper.PostAsync<object, Vacancy>(endpoint, obj);
            }
        }

    }
}
