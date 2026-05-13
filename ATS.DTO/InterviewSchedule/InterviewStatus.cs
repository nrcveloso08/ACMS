using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.InterviewSchedule
{
    public enum InterviewStatus
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


        Declined_Position = 19,


        Background_Check_Pending = 20,


        Background_Check_Pass = 21,


        Background_Check_Fail = 22,

        Terminated = 23
    }
    public class InterviewStatusDTO
    {
        public InterviewStatus InterviewStatus { get; set; }
        public string Name { get; set; }
        public IList<StatusDisposition> Dispositions { get; set; }
    }
    public class StatusDisposition
    {
        public int Id { get; set; }
        public InterviewStatus InterviewStatus { get; set; }
        public int Location_Id { get; set; }
        public string Name { get; set; }
    }


}
