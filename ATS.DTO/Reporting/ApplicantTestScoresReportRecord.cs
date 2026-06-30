using System;

namespace ATS.DTO.Reporting
{
    public class ApplicantTestScoresReportRecord
    {
        public Guid Applicant_Id { get; set; }
        public string FullName { get; set; }
        public string TestName { get; set; }
        public decimal? Score { get; set; }
        public DateTimeOffset? ApplicationDate { get; set; }
        public string Location { get; set; }
        public string JobAdvertisement { get; set; }
    }
}
