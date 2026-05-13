using ATS.DTO.Applicant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Honduras
{
    public class HondurasAdditionalInfo 
    {
        public int Id { get; set; }
        [ForeignKey("Applicant")]
        public Guid Applicant_Id { get; set; }
        public ApplicantBasicInfo Applicant { get; protected set; }
        [MaxLength(100)]
        public string DNI { get; set; }
    }
}
