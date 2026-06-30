namespace ATS.DTO.CandidateSearch
{
    /// <summary>
    /// Mirrors a single DataTables row returned by the legacy KITT
    /// /v1/Applicant/SearchApplicantByInfo endpoint (see candidate.search.js).
    /// </summary>
    public class CandidateSearchApplicantRowDto
    {
        public string Applicant_Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public IList<CandidateSearchApplicantPhoneDto> PhoneNumbers { get; set; } = new List<CandidateSearchApplicantPhoneDto>();
        public string EmailAddress { get; set; } = string.Empty;
        public string LanguageName { get; set; } = string.Empty;
        public string EmploymentType { get; set; } = string.Empty;
        public string Dayforce_Id { get; set; } = string.Empty;
        public int CurrentStatus { get; set; }
        public string CurrentStatusDisplay { get; set; } = string.Empty;
        public int Location_Id { get; set; }
        public int RequisitionRequestId { get; set; }
        public bool IsDeleted { get; set; }
        public int PendingFollowUpItemsCount { get; set; }
    }

    public class CandidateSearchApplicantPhoneDto
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public int Location_Id { get; set; }
    }

    /// <summary>
    /// Mirrors KITT.Service.DTO.Pagination.PaginationObject with a typed Data list.
    /// </summary>
    public class CandidateSearchApplicantPageDto
    {
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public IList<CandidateSearchApplicantRowDto> data { get; set; } = new List<CandidateSearchApplicantRowDto>();
    }
}
