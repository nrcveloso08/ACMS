using ATS.DTO.Applicant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.FollowUp
{
    public class FollowUpItemsByApplicant
    {
        public ApplicantContactInfo Applicant { get; set; }
        public IList<FollowUpApplicant> FollowUpItems { get; set; }
    }
}
