using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.InterviewSchedule
{
    public class InterviewStatusLookup
    {
        public int Id { get; set; }
        public InterviewStatus InterviewStatus { get; set; }
        public string Name { get; set; }
        public bool isDeleted { get; set; }
    }

    public enum InterviewStatusOld
    {
        New_Applicant = 1,
        Scheduled = 2,
        In_Testing = 3,
        Ready_For_Interview = 4,
        In_Interview = 5,
        CSI_Interview = 6,
        HR_Processing = 7,
        Pending_Offer = 8,
        PreHire = 9,
        Hired = 10,
        On_Hold = 11,
        Not_Right_Now = 12,
        Checked_In = 13,
        Awaiting_Medical = 14,
        Passed_Medical = 15,
        Failed_Medical = 16,
        Awaiting_For_Testing = 17,
        NCNS = 18,
        Candidate_Withdrew = 19
    }
}
