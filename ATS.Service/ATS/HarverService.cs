using ATS.Service.ViewModels;
using ATS.Service.ViewModels.AdvertisementSource;
using ATS.Service.ViewModels.Vacancies;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service.ATS
{
    public interface IHarverService
    {
        Task<VacancyVM> AddOrUpdate(VacancyVM obj);
        Task<ATSResult> GetAll();
    }

    public class HarverService : IHarverService
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public HarverService(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<ATSResult> GetAll()
        {
            return await _webServiceHelper.GetAsync<ATSResult>("/v1/VacancyDefaultJobAd/GetAll");
        }

        public async Task<VacancyVM> AddOrUpdate(VacancyVM obj)
        {
            string endpoint = string.Empty;
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (obj.Id == 0)
            {
                // ✅ Add new record
                endpoint = "/v1/VacancyDefaultJobAd/Add";
                return await _webServiceHelper.PostAsync<object, VacancyVM>(endpoint, obj);
            }
            else
            {
                // ✅ Update existing record
                endpoint = "/v1/VacancyDefaultJobAd/Update";
                return await _webServiceHelper.PostAsync<object, VacancyVM>(endpoint, obj);
            }
        }

    }
}
