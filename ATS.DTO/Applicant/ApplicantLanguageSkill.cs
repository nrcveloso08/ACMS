using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Applicant
{
    public class ApplicantLanguageSkill
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public bool IsWritten { get; set; }
        public bool IsSpoken { get; set; }
        public string Proficiency { get; set; }
    }
}
