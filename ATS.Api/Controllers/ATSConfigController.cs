using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ATSConfigController : ControllerBase
    {
        private readonly IATSConfigService _atsConfigService;

        public ATSConfigController(IATSConfigService atsConfigService)
        {
            _atsConfigService = atsConfigService;
        }

        // TODO: GET/PUT status-email mappings, interview types, employee statuses
        // TODO: GET/PUT job ads, prescreening questions, schedule exceptions, timeslot configs
    }
}
