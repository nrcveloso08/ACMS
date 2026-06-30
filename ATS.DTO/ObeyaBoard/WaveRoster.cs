using System;
using ATS.DTO;

namespace ATS.DTO.ObeyaBoard
{
    public class WaveRoster
    {
        public Guid ApplicantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int RequisitionId { get; set; }
        public InterviewStatus CurrentStatus { get; set; }
        public string Language { get; set; }
        public string EmploymentType { get; set; }
    }
}
