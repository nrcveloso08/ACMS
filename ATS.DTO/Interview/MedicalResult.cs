using System;
using System.Collections.Generic;

namespace ATS.DTO.Interview
{
    public class MedicalResult
    {
        public int Id { get; set; }
        public Guid ApplicantId { get; set; }
        public int LocationId { get; set; }
        public DateTimeOffset? AppointmentDate { get; set; }
        public bool Passed { get; set; }
        public string Notes { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedByUser { get; set; }
        public string CreatedByEmail { get; set; }
    }

    public class MedicalFilters
    {
        public IList<int> LocationIds { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int StatusId { get; set; }
    }
}
