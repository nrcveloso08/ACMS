namespace ATS.DTO.CandidateSearch
{
    public class CandidateSearchRequestDto
    {
        public string DayforceId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool HasPendingFollowUps { get; set; }
        public string Status { get; set; } = string.Empty;
        public string StatusDisposition { get; set; } = string.Empty;
        public string LineOfBusiness { get; set; } = string.Empty;
        public string HireType { get; set; } = string.Empty;
        public string ShowProfiles { get; set; } = string.Empty;
        public string LocationId { get; set; } = string.Empty;

        // TODO: Vue page currently sends a single LocationId. Old KITT search supported
        // multiple LocationIds; add support here if/when the frontend filter is upgraded.
        public IList<int>? LocationIds { get; set; }
    }
}
