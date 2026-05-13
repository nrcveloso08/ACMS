using ATS.DTO.Applicant;
using ATS.DTO.FCRA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.HR
{
    public class ApplicantForHR : ApplicantBasicInfo
    {
        public IList<HRCheckResponse> HRCheckResponses { get; set; }
        public FCRAStatus? FCRAStatus { get; set; }

        public string FCRAStatusDisplay
        {
            get
            {
                return !this.FCRAStatus.HasValue || this.FCRAStatus == 0 ? "" : this.FCRAStatus.GetDescription();
            }
        }
    }
}
