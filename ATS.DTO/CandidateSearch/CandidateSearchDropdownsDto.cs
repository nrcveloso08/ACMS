namespace ATS.DTO.CandidateSearch
{
    public class CandidateSearchDropdownsDto
    {
        public IList<CandidateSearchOptionDto> Status { get; set; } = new List<CandidateSearchOptionDto>();
        public IList<CandidateSearchOptionDto> StatusDisposition { get; set; } = new List<CandidateSearchOptionDto>();
        public IList<CandidateSearchOptionDto> LineOfBusiness { get; set; } = new List<CandidateSearchOptionDto>();
        public IList<CandidateSearchOptionDto> HireType { get; set; } = new List<CandidateSearchOptionDto>();
        public IList<CandidateSearchOptionDto> ShowProfiles { get; set; } = new List<CandidateSearchOptionDto>();
        public IList<CandidateSearchOptionDto> Location { get; set; } = new List<CandidateSearchOptionDto>();
    }
}
