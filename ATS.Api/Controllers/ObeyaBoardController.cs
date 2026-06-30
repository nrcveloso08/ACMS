using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ObeyaBoardController : ControllerBase
    {
        private readonly IObeyaBoardService _obeyaBoardService;

        public ObeyaBoardController(IObeyaBoardService obeyaBoardService)
        {
            _obeyaBoardService = obeyaBoardService;
        }

        // TODO: GET wave cards / wave status / wave notes
        // TODO: PUT applicant bulk updates
        // TODO: GET Excel exports
    }
}
