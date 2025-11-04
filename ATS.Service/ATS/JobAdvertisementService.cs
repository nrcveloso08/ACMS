using ATS.Service.ViewModels.AdvertisementSource;
using ATS.Service.ViewModels.JobAdvertisement;
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
    }

    public class JobAdvertisementService: IJobAdvertisementService
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public JobAdvertisementService(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<List<JobAdvertisementVM>> GetAll()
        {
            return await _webServiceHelper.GetAsync<List<JobAdvertisementVM>>("/v1/Jobs/GetAll");
        }
    }
}
