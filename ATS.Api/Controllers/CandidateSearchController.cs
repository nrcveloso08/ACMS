using ATS.DTO.CandidateSearch;
using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/candidate-search")]
    public class CandidateSearchController : ControllerBase
    {
        private readonly ICandidateSearchService _candidateSearchService;

        public CandidateSearchController(ICandidateSearchService candidateSearchService)
        {
            _candidateSearchService = candidateSearchService;
        }

        // GET api/v1/candidate-search/dropdowns
        [HttpGet("dropdowns")]
        public async Task<ActionResult<CandidateSearchDropdownsDto>> GetDropdowns()
        {
            var dropdowns = await _candidateSearchService.GetDropdownsAsync();
            return Ok(dropdowns);
        }

        // POST api/v1/candidate-search
        [HttpPost]
        public async Task<ActionResult<IList<CandidateSearchResultDto>>> Search([FromBody] CandidateSearchRequestDto request)
        {
            var response = await _candidateSearchService.SearchAsync(request ?? new CandidateSearchRequestDto());
            return Ok(response.Results);
        }
    }
}
