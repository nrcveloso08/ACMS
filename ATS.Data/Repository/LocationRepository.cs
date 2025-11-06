using ATS.DTO.Location;
using ATS.Service.WebServiceHelper;

namespace ATS.Data.Repository
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAll();
    }

    public class LocationRepository : ILocationRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public LocationRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<List<Location>> GetAll()
        {
            return await _webServiceHelper.GetAsync<List<Location>>("/v1/Location/GetAll");
        }

    }
}
