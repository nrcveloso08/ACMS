using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HRProcessingController : ControllerBase
    {
        private readonly IHRProcessingService _hrProcessingService;

        public HRProcessingController(IHRProcessingService hrProcessingService)
        {
            _hrProcessingService = hrProcessingService;
        }

        // TODO: GET HR checklist / responses / HR info by applicant
        // TODO: PUT HR info CRUD / completion workflow
    }
}
