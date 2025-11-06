using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.InterviewSchedule
{
    public class InterviewSchedule
    {
        public string TimeSlot { get; set; }
        public List<ApplicantViewModel> Applicants { get; set; } = new();
    }

    public class ApplicantViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}
