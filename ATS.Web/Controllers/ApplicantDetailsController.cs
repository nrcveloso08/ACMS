using ATS.DTO.Applicant;
using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantDetailsController : ControllerBase
    {
        private readonly IApplicantDetailsService _applicantDetailsService;

        public ApplicantDetailsController(IApplicantDetailsService applicantDetailsService)
        {
            _applicantDetailsService = applicantDetailsService;
        }

        [HttpGet("{applicantId:guid}")]
        public async Task<IActionResult> Get(Guid applicantId)
        {
            var details = await _applicantDetailsService.GetDetailsAsync(applicantId);
            if (details == null)
                return NotFound();

            return Ok(details);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ApplicantDetails details)
        {
            var result = await _applicantDetailsService.UpdateDetailsAsync(details);
            return Ok(result);
        }
    }
}
