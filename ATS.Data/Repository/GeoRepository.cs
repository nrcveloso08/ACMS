using ATS.DTO.Geo;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IGeoRepository
    {
        Task<IList<CountryDTO>> GetCountries();
        Task<IList<StateDTO>> GetStates(int countryId);
        Task<IList<CityDTO>> GetCities(int stateId);
    }

    public class GeoRepository : IGeoRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public GeoRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<CountryDTO>> GetCountries()
        {
            return await _webServiceHelper.GetAsync<IList<CountryDTO>>(
                "/v1/Geo/GetCountries"
            );
        }

        public async Task<IList<StateDTO>> GetStates(int countryId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "countryId", countryId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<StateDTO>>(
                "/v1/Geo/GetStates",
                parameters
            );
        }

        public async Task<IList<CityDTO>> GetCities(int stateId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "stateId", stateId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<CityDTO>>(
                "/v1/Geo/GetCities",
                parameters
            );
        }
    }
}