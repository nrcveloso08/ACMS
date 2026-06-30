using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FCRAController : ControllerBase
    {
        private readonly IFCRAService _fcraService;

        public FCRAController(IFCRAService fcraService)
        {
            _fcraService = fcraService;
        }

        // TODO: GET FCRA record / status / notes by applicant
        // TODO: POST/PUT FCRA record CRUD / document attachment
        // TODO: GET FCRA Excel report
    }
}
