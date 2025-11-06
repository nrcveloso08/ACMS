using ATS.Service.ViewModels.Location;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service.KITT
{
    public interface ILocationService
    {
        Task<LocationVM> GetAll();
    }

    public class LocationService : ILocationService
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public LocationService(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<LocationVM> GetAll()
        {
            return await WebServiceHelper.WebServiceHelper.KittInstance.GetAsync<LocationVM>("/Admin/Locations/GetLocations");
        }
    }
}
