using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Applicant
{
    public class ApplicantEmploymentHistory
    {
        public int Id { get; set; }
        public string NameOfCompany { get; set; }
        public string PositionHeld { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PhoneNumber { get; set; }
        public string LeaveReason { get; set; }
    }
}
