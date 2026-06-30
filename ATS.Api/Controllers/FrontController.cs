using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FrontController : ControllerBase
    {
        private readonly IFrontService _frontService;

        public FrontController(IFrontService frontService)
        {
            _frontService = frontService;
        }

        // TODO: GET Front inbox messages / conversation threads
        // TODO: GET message details by applicant email
    }
}
