using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Applicant
{
    public class ApplicantEducationHistory
    {
        public int Id { get; set; }
        public string NameOfSchool { get; set; }
        public string Course { get; set; }
        public bool IsCompleted { get; set; }
        public TypeOfSchool TypeOfSchool { get; set; }
        public string TypeOfSchoolName { get; set; }
    }

    public enum TypeOfSchool
    {
        High_School = 1,
        University_or_College,
        Business_or_Technical,
        Other
    }
}
