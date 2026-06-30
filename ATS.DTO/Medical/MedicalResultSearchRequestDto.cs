using System;
using System.Collections.Generic;

namespace ATS.DTO.Medical
{
    public class MedicalResultSearchRequestDto
    {
        public int? LocationId { get; set; }
        public IList<int>? LocationIds { get; set; }
        public Guid? ApplicantId { get; set; }
        public string? ApplicantName { get; set; }
        public int? StatusId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
