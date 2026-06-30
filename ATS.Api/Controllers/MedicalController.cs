using ATS.DTO.Medical;
using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/medical/results")]
    public class MedicalController : ControllerBase
    {
        private readonly IMedicalService _medicalService;

        public MedicalController(IMedicalService medicalService)
        {
            _medicalService = medicalService;
        }

        // GET api/v1/medical/results/dropdowns
        [HttpGet("dropdowns")]
        public async Task<ActionResult<MedicalResultDropdownsDto>> GetDropdowns()
        {
            return Ok(await _medicalService.GetDropdownsAsync());
        }

        // POST api/v1/medical/results/search
        [HttpPost("search")]
        public async Task<ActionResult<MedicalResultSearchResponseDto>> Search([FromBody] MedicalResultSearchRequestDto? request)
        {
            request ??= new MedicalResultSearchRequestDto();

            var hasLocation = (request.LocationId.HasValue && request.LocationId.Value > 0)
                || (request.LocationIds != null && request.LocationIds.Any(id => id > 0));

            if (!hasLocation)
                return BadRequest("locationId or locationIds is required.");

            return Ok(await _medicalService.SearchMedicalResultsAsync(request));
        }

        // GET api/v1/medical/results/applicant/{applicantId}
        [HttpGet("applicant/{applicantId:guid}")]
        public async Task<ActionResult<MedicalResultApplicantDto>> GetByApplicant(Guid applicantId)
        {
            if (applicantId == Guid.Empty)
                return BadRequest("applicantId must be a valid, non-empty GUID.");

            return Ok(await _medicalService.GetApplicantMedicalResultAsync(applicantId));
        }

        // GET api/v1/medical/results/location/{locationId}
        [HttpGet("location/{locationId:int}")]
        public async Task<ActionResult<IList<MedicalResultDto>>> GetByLocation(int locationId)
        {
            if (locationId <= 0)
                return BadRequest("locationId must be a positive integer.");

            return Ok(await _medicalService.GetLocationMedicalResultsAsync(locationId));
        }

        // TODO (deferred write action): AddOrUpdate medical result is implemented in
        // IMedicalService.SaveMedicalResultAsync / IMedicalResultRepository.AddOrUpdate, but is
        // not yet exposed here pending UI/auth requirements (CreatedByUser/CreatedByEmail).
    }
}
