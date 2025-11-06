using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Applicant
{
    public class ApplicantStatus
    {
        public int Id { get; set; }
        public Guid ApplicantId { get; set; }
        public InterviewStatus InterviewStatus { get; set; }
        public string Notes { get; set; }
        public string Message { get; set; }
    }
}
