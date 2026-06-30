using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DayforceController : ControllerBase
    {
        private readonly IDayforceService _dayforceService;

        public DayforceController(IDayforceService dayforceService)
        {
            _dayforceService = dayforceService;
        }

        // TODO: GET/PUT XrefCode CRUD (job, dept, org, onboarding policy)
        // TODO: GET employee status / field validation
    }
}
