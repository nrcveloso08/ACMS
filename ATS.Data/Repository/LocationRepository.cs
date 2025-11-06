using ATS.DTO.Location;
using ATS.Service.WebServiceHelper;

namespace ATS.Data.Repository
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAll();
        Task<Location?> Get(int id);
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


        public async Task<Location?> Get(int id)
        {
            var data = await this.GetAll();

            return data.FirstOrDefault(x => x.Id == id);


        }
    }
}
