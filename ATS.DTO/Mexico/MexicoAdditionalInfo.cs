using ATS.DTO.Applicant;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATS.DTO.Mexico
{
    public class MexicoAdditionalInfo
    {
        public int Id { get; set; }
        [ForeignKey("Applicant")]
        public Guid Applicant_Id { get; set; }
        public ApplicantBasicInfo Applicant { get; protected set; }
        [MaxLength(100)]
        public string CURP { get; set; }
        [MaxLength(100)]
        public string RFC { get; set; }
        [MaxLength(100)]
        public string NSS { get; set; }
    }
}
