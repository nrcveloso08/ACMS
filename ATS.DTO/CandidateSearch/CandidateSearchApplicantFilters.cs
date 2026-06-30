namespace ATS.DTO.CandidateSearch
{
    /// <summary>
    /// Mirrors the legacy KITT CandidateSearchFilters shape expected by
    /// ATSWebService's /v1/Applicant/SearchApplicantByInfo endpoint.
    /// </summary>
    public class CandidateSearchApplicantFilters
    {
        public string Dayforce_Id { get; set; } = string.Empty;
        public string ApplicantName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public IList<int> LocationIds { get; set; } = new List<int>();
        public string Disposition { get; set; } = string.Empty;
        public bool PendingFollowUpOnly { get; set; }

        // TODO: LineOfBusiness/HireType were derived client-side in old KITT (RequisitionIds,
        // HiringSourceId). Passed through here so ATSWebService can apply them if/when supported.
        public string LineOfBusiness { get; set; } = string.Empty;
        public string HireType { get; set; } = string.Empty;

        public string ShowActive { get; set; } = "Active";

        public CandidateSearchApplicantPaginationFilters Pagination { get; set; } = new();
    }

    public class CandidateSearchApplicantPaginationFilters
    {
        public int TableDrawSequence { get; set; } = 1;
        public int TableDataStartingIndex { get; set; }
        public int TablePageLength { get; set; } = 50;
        public string TableColumnToSort { get; set; } = "LastName";
        public string TableColumnSortDirection { get; set; } = "asc";
    }
}
