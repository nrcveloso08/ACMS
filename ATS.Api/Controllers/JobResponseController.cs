using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class JobResponseController : ControllerBase
    {
        private readonly IJobResponsePageService _jobResponsePageService;

        public JobResponseController(IJobResponsePageService jobResponsePageService)
        {
            _jobResponsePageService = jobResponsePageService;
        }

        // TODO: GET employment application viewer data by applicant
        // TODO: PUT multi-section form save (personal, work, education, language, refs, docs)
    }
}
