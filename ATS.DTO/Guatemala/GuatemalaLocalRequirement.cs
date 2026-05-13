using ATS.DTO.Applicant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Guatemala
{
    public class GuatemalaLocalRequirement
    {
        public int Id { get; set; }
        [ForeignKey("Applicant")]
        public Guid Applicant_Id { get; set; }
        public ApplicantBasicInfo Applicant { get; protected set; }
        public bool JobApplication { get; set; }
        public bool Resume { get; set; }
        public bool DPIPhotoCopies { get; set; }
        public bool HSDiploma { get; set; }
        public bool PersonalLetterOfRecommendation { get; set; }
        public bool EmploymentVerificationLetter { get; set; }
        public bool OriginalPoliceRecord { get; set; }
        public bool OriginalCriminalRecord { get; set; }
        public bool IGSSID { get; set; }
        public bool IRTRAId { get; set; }
        public bool PhotoCopyProofOfBilling { get; set; }
        public bool ISRCertificate { get; set; }
        public bool WageGarnishLetter { get; set; }
    }
}
