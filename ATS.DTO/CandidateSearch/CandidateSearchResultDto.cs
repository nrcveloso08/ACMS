namespace ATS.DTO.CandidateSearch
{
    public class CandidateSearchResultDto
    {
        public string ApplicantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsPrimaryPhone { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string EmploymentType { get; set; } = string.Empty;
        public string DayforceId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string StatusDisposition { get; set; } = string.Empty;
        public string LineOfBusiness { get; set; } = string.Empty;
        public string HireType { get; set; } = string.Empty;
        public bool HasPendingFollowUps { get; set; }
        public string ProfileStatus { get; set; } = string.Empty;
        public string RowState { get; set; } = "default";
    }
}
