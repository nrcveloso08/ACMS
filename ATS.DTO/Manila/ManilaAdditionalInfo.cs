using ATS.DTO.Applicant;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATS.DTO.Manila
{
    public class ManilaAdditionalInfo
    {
        public int Id { get; set; }
        [ForeignKey("Applicant")]
        public Guid Applicant_Id { get; set; }
        public ApplicantBasicInfo Applicant { get; protected set; }
        [MaxLength(100)]
        public string SssNumber { get; set; }
        [MaxLength(100)]
        public string PhilHealthNumber { get; set; }
        [MaxLength(100)]
        public string PagIbigNumber { get; set; }
        [MaxLength(100)]
        public string Tin { get; set; }
        public bool IsProjectBased { get; set; }
        public string ProjectBasedEndDate { get; set; }
    }
}
