using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WaveRosterController : ControllerBase
    {
        private readonly IWaveRosterService _waveRosterService;

        public WaveRosterController(IWaveRosterService waveRosterService)
        {
            _waveRosterService = waveRosterService;
        }

        // TODO: GET wave roster applicant list
        // TODO: PUT applicant status updates / training details / pickup schedule
    }
}
