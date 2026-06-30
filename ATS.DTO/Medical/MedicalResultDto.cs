using System;

namespace ATS.DTO.Medical
{
    public class MedicalResultDto
    {
        public Guid ApplicantId { get; set; }
        public string ApplicantName { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public string Location { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MedicalStatus { get; set; } = string.Empty;
        public DateTime? ResultDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
