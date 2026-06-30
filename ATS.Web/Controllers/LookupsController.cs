using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupsService _lookupsService;

        public LookupsController(ILookupsService lookupsService)
        {
            _lookupsService = lookupsService;
        }

        [HttpGet("locations")]
        public async Task<IActionResult> GetLocations() => Ok(await _lookupsService.GetLocationsAsync());

        [HttpGet("languages")]
        public async Task<IActionResult> GetLanguages() => Ok(await _lookupsService.GetLanguagesAsync());

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries() => Ok(await _lookupsService.GetCountriesAsync());

        [HttpGet("states")]
        public async Task<IActionResult> GetStates([FromQuery] int countryId) => Ok(await _lookupsService.GetStatesAsync(countryId));

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities([FromQuery] int stateId) => Ok(await _lookupsService.GetCitiesAsync(stateId));

        [HttpGet("advertisement-sources")]
        public async Task<IActionResult> GetAdvertisementSources() => Ok(await _lookupsService.GetAdvertisementSourcesAsync());

        [HttpGet("document-types")]
        public async Task<IActionResult> GetDocumentTypes() => Ok(await _lookupsService.GetDocumentTypesAsync());

        [HttpGet("interview-statuses")]
        public async Task<IActionResult> GetInterviewStatuses() => Ok(await _lookupsService.GetInterviewStatusesAsync());
    }
}
