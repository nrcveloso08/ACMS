using ATS.DTO.Applicant;
using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantSearchService _applicantService;

        public ApplicantController(IApplicantSearchService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var applicant = await _applicantService.GetByIdAsync(id);
            if (applicant == null)
                return NotFound();

            return Ok(applicant);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string namePartial, [FromQuery] int max = 50)
        {
            var results = await _applicantService.SearchByNameAsync(namePartial, max);
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromBody] Applicant applicant)
        {
            if (applicant == null)
                return BadRequest();

            var result = await _applicantService.CreateOrUpdateAsync(applicant);
            return Ok(result);
        }
    }
}
