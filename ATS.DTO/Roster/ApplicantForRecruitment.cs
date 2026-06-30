using System;
using ATS.DTO;

namespace ATS.DTO.Roster
{
    public class ApplicantForRecruitment
    {
        public Guid ApplicantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public InterviewStatus CurrentStatus { get; set; }
        public int? RequisitionId { get; set; }
        public int Location_Id { get; set; }
        public string LocationName { get; set; }
    }
}
