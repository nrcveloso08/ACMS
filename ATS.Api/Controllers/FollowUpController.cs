using ATS.DTO.FollowUp;
using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/follow-up")]
    public class FollowUpController : ControllerBase
    {
        private readonly IFollowUpManagementService _followUpManagementService;

        public FollowUpController(IFollowUpManagementService followUpManagementService)
        {
            _followUpManagementService = followUpManagementService;
        }

        // GET api/v1/follow-up/statuses
        [HttpGet("statuses")]
        public async Task<ActionResult<IList<FollowUpStatusDto>>> GetStatuses()
        {
            return Ok(await _followUpManagementService.GetFollowUpStatusesAsync());
        }

        // GET api/v1/follow-up/dispositions/{locationId}
        [HttpGet("dispositions/{locationId:int}")]
        public async Task<ActionResult<IList<FollowUpDispositionDto>>> GetDispositionsByLocation(int locationId)
        {
            if (locationId <= 0)
                return BadRequest("locationId must be a positive integer.");

            return Ok(await _followUpManagementService.GetDispositionOptionsAsync(locationId));
        }

        // POST api/v1/follow-up/dispositions
        [HttpPost("dispositions")]
        public async Task<ActionResult<IList<FollowUpDispositionDto>>> GetDispositionsByLocations([FromBody] IList<int>? locationIds)
        {
            return Ok(await _followUpManagementService.GetDispositionOptionsAsync(locationIds ?? new List<int>()));
        }

        // POST api/v1/follow-up/search
        [HttpPost("search")]
        public async Task<ActionResult<FollowUpSearchResponseDto>> Search([FromBody] FollowUpSearchRequestDto? request)
        {
            request ??= new FollowUpSearchRequestDto();

            var hasLocation = (request.LocationId.HasValue && request.LocationId.Value > 0)
                || (request.LocationIds != null && request.LocationIds.Any(id => id > 0));

            if (!hasLocation)
                return BadRequest("locationId or locationIds is required.");

            return Ok(await _followUpManagementService.SearchFollowUpsAsync(request));
        }

        // POST api/v1/follow-up/applicant/{applicantId}
        [HttpPost("applicant/{applicantId:guid}")]
        public async Task<ActionResult<FollowUpApplicantResponseDto>> GetByApplicant(Guid applicantId, [FromBody] FollowUpApplicantRequestDto? request)
        {
            if (applicantId == Guid.Empty)
                return BadRequest("applicantId must be a valid, non-empty GUID.");

            request ??= new FollowUpApplicantRequestDto();

            return Ok(await _followUpManagementService.GetApplicantFollowUpsAsync(applicantId, request.PendingOnly));
        }

        // GET api/v1/follow-up/location/{locationId}
        [HttpGet("location/{locationId:int}")]
        public async Task<ActionResult<IList<FollowUpResultDto>>> GetByLocation(int locationId, [FromQuery] bool pendingOnly = true)
        {
            if (locationId <= 0)
                return BadRequest("locationId must be a positive integer.");

            return Ok(await _followUpManagementService.GetLocationFollowUpsAsync(locationId, pendingOnly));
        }

        // TODO (deferred write actions): AddOrUpdateFollowUp and BulkComplete are implemented in
        // IFollowUpManagementService.SaveFollowUpAsync / CompleteFollowUpsAsync and
        // IFollowUpsRepository, but are not yet exposed here pending UI/auth requirements.
    }
}
