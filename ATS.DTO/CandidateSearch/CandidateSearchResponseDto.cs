namespace ATS.DTO.CandidateSearch
{
    public class CandidateSearchResponseDto
    {
        public IList<CandidateSearchResultDto> Results { get; set; } = new List<CandidateSearchResultDto>();
    }
}
