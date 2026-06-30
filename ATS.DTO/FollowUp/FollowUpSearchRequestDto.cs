using System;
using System.Collections.Generic;

namespace ATS.DTO.FollowUp
{
    public class FollowUpSearchRequestDto
    {
        public int? LocationId { get; set; }
        public IList<int>? LocationIds { get; set; }
        public Guid? ApplicantId { get; set; }
        public FollowUpStatus? Status { get; set; }
        public int? DispositionId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? AssignedTo { get; set; }
        public string? Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
