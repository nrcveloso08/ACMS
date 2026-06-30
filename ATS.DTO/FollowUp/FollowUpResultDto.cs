using System;

namespace ATS.DTO.FollowUp
{
    public class FollowUpResultDto
    {
        public int FollowUpId { get; set; }
        public Guid ApplicantId { get; set; }
        public string ApplicantName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Disposition { get; set; } = string.Empty;
        public DateTime? FollowUpDate { get; set; }
        public string AssignedTo { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
