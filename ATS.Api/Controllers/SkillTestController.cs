using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SkillTestController : ControllerBase
    {
        private readonly ISkillTestService _skillTestService;

        public SkillTestController(ISkillTestService skillTestService)
        {
            _skillTestService = skillTestService;
        }

        // TODO: GET skill test results by applicant
        // TODO: GET FurstPerson assessment / result sets by location/wave
        // TODO: POST/PUT skill test result CRUD
    }
}
