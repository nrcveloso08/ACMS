using ATS.Service.KITT;
using ATS.Service.ViewModels.AdvertisementSource;
using ATS.Service.ViewModels.JobAdvertisement;
using ATS.Service.ViewModels.Vacancies;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service.ATS
{
    public interface IJobAdvertisementService
    {
        Task<List<JobAdvertisementVM>> GetAll();
        Task<JobAdvertisementVM> AddOrUpdate(JobAdvertisementVM obj);
    }

    public class JobAdvertisementService : IJobAdvertisementService
    {
        private readonly IWebServiceHelper _webServiceHelper;
        private readonly ILocationService _locationService;

        public JobAdvertisementService(IWebServiceHelper webServiceHelper, ILocationService locationService)
        {
            _webServiceHelper = webServiceHelper;
            _locationService = locationService; 
        }

        public async Task<List<JobAdvertisementVM>> GetAll()
        {
            //var location = _locationService.GetAll();
            return await _webServiceHelper.GetAsync<List<JobAdvertisementVM>>("/v1/Jobs/GetAll");
        }

        public async Task<JobAdvertisementVM> AddOrUpdate(JobAdvertisementVM obj)
        {
            string endpoint = string.Empty;
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            // ✅ Add new record
            endpoint = "/v1/Jobs/Save";
            return await _webServiceHelper.PostAsync<object, JobAdvertisementVM>(endpoint, obj);


        }

    }
}
