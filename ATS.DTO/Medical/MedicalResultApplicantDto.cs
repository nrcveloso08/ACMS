using System;

namespace ATS.DTO.Medical
{
    public class MedicalResultApplicantDto
    {
        public Guid ApplicantId { get; set; }
        public bool HasResult { get; set; }
        public string MedicalStatus { get; set; } = string.Empty;
        public DateTime? ResultDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
    }
}
