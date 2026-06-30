using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
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

        // TODO: GET locations
        // TODO: GET languages
        // TODO: GET countries / states / cities
        // TODO: GET advertisement sources
        // TODO: GET document types
        // TODO: GET interview statuses
    }
}
