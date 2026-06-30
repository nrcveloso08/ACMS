using System;

namespace ATS.DTO.JobResponse
{
    public class GuatemalaJobResponse
    {
        public Guid Applicant_Id { get; set; }
        public int EmploymentApplication_Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string JobAdvertisement { get; set; }
        public int JobAdvertisement_Id { get; set; }
        public string Status { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateSubmitted { get; set; }
    }
}
