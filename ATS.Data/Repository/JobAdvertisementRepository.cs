using ATS.DTO.JobAdvertisement;
using ATS.Service.WebServiceHelper;

namespace ATS.Data.Repository
{
    public interface IJobAdvertisementRepository
    {
        Task<List<JobAdvertisement>> GetAll();
        Task<JobAdvertisement> AddOrUpdate(JobAdvertisement obj);
        Task<JobAdvertisement?> Get(int id);
    }

    public class JobAdvertisementRepository : IJobAdvertisementRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;
        private readonly ILocationRepository _locationService;

        public JobAdvertisementRepository(IWebServiceHelper webServiceHelper, ILocationRepository locationService)
        {
            _webServiceHelper = webServiceHelper;
            _locationService = locationService; 
        }

        public async Task<List<JobAdvertisement>> GetAll()
        {
            //var location = _locationService.GetAll();
            return await _webServiceHelper.GetAsync<List<JobAdvertisement>>("/v1/Jobs/GetAll");
        }

        public async Task<JobAdvertisement> AddOrUpdate(JobAdvertisement obj)
        {
            string endpoint = string.Empty;
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            // ✅ Add new record
            endpoint = "/v1/Jobs/Save";
            return await _webServiceHelper.PostAsync<object, JobAdvertisement>(endpoint, obj);


        }

        public async Task<JobAdvertisement?> Get(int id)
        {
            var data = await this.GetAll();

            return data.FirstOrDefault(x => x.Id == id);
        }
    }
}
