using ATS.Service;
using ATS.Service.WebServiceHelper;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/applicant-details")]
    public class ApplicantDetailsController : ControllerBase
    {
        private readonly IApplicantDetailsService _applicantDetailsService;
        private readonly ILogger<ApplicantDetailsController> _logger;

        public ApplicantDetailsController(IApplicantDetailsService applicantDetailsService, ILogger<ApplicantDetailsController> logger)
        {
            _applicantDetailsService = applicantDetailsService;
            _logger = logger;
        }

        // GET api/v1/applicant-details/{applicantId}
        [HttpGet("{applicantId:guid}")]
        public async Task<IActionResult> GetApplicantDetails(Guid applicantId)
        {
            if (applicantId == Guid.Empty)
                return BadRequest("applicantId must be a valid, non-empty GUID.");

            try
            {
                var details = await _applicantDetailsService.GetApplicantDetailsAsync(applicantId);
                if (details == null)
                    return NotFound();

                return Ok(details);
            }
            catch (ExternalServiceUnavailableException ex)
            {
                _logger.LogError(ex, "ATSWebService unavailable while fetching applicant details for {ApplicantId}", applicantId);
                return Problem(
                    title: "External service unavailable",
                    detail: "The applicant data service (ATSWebService) is currently unreachable. Please try again later.",
                    statusCode: StatusCodes.Status502BadGateway);
            }
        }

        // GET api/v1/applicant-details/{applicantId}/status-options
        [HttpGet("{applicantId:guid}/status-options")]
        public async Task<IActionResult> GetStatusOptions(Guid applicantId)
        {
            if (applicantId == Guid.Empty)
                return BadRequest("applicantId must be a valid, non-empty GUID.");

            var statusOptions = await _applicantDetailsService.GetStatusOptionsAsync(applicantId);
            return Ok(statusOptions);
        }

        // GET api/v1/applicant-details/{applicantId}/scheduling-history
        [HttpGet("{applicantId:guid}/scheduling-history")]
        public async Task<IActionResult> GetSchedulingHistory(Guid applicantId)
        {
            if (applicantId == Guid.Empty)
                return BadRequest("applicantId must be a valid, non-empty GUID.");

            var history = await _applicantDetailsService.GetSchedulingHistoryAsync(applicantId);
            return Ok(history);
        }

        // GET api/v1/applicant-details/{applicantId}/email-templates
        [HttpGet("{applicantId:guid}/email-templates")]
        public async Task<IActionResult> GetEmailTemplates(Guid applicantId)
        {
            if (applicantId == Guid.Empty)
                return BadRequest("applicantId must be a valid, non-empty GUID.");

            var templates = await _applicantDetailsService.GetEmailTemplatesAsync(applicantId);
            return Ok(templates);
        }

        // GET api/v1/applicant-details/{applicantId}/prescreen-responses
        [HttpGet("{applicantId:guid}/prescreen-responses")]
        public async Task<IActionResult> GetPrescreenResponses(Guid applicantId)
        {
            if (applicantId == Guid.Empty)
                return BadRequest("applicantId must be a valid, non-empty GUID.");

            var responses = await _applicantDetailsService.GetPrescreenResponsesAsync(applicantId);
            return Ok(responses);
        }
    }
}
