using System;

namespace ATS.DTO.JobResponse
{
    public class JobResponseFilters
    {
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int? Location_Id { get; set; }
        public int? JobAdvertisement_Id { get; set; }
        public string Status { get; set; }
    }
}
