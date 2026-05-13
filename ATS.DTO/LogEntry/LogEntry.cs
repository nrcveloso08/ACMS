using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.LogEntry
{
    public class LogEntry
    {
        public int Id { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid ApplicantId { get; set; }
        public string UserName { get; set; } = "";
        public string UserEmail { get; set; } = "";
        public string Action { get; set; } = "";
        public string ActionDetails { get; set; } = "";
        public DateTimeOffset TimeStamp { get; set; }
        public InternalApplicationStatus Status { get; set; }
    }
    public enum LogAction
    {
        Change_Status = 1,
        Set_CallBackRequest = 2,
        Sent_Email = 3,
        Appointment_Change = 4,
        Note = 5,
        Prescreen_Answers_Change = 6,
        CSI_Note = 7,
        Update_RequisitionAssignment = 8,
        Applicant_Note = 9,
        Interview_Note = 10,
        Sent_SMS = 11,
        Received_FurstPerson_Skills = 12,
        ClassicApplicantImport = 13,
        Generated_Assessment_URL = 14,
        PostEEID = 15,
        Schedule_CSI = 16,
        CSI_Check_In = 17,
        CSI_Result = 18,
        CSI_Rebooked = 19,
        Link_Generated = 20,
        Link_Accessed = 21,
        Saved_Online_Application = 22,
        Submitted_Online_Application = 23,
        Update_Date_of_Birth = 24,
        Update_Training_Details = 25,
        Update_Computer_Pickup_Schedule = 26,
        Update_HR_Information = 27,
        FollowUp_Create = 28,
        FollowUp_Update = 29,
        FollowUp_Complete = 30,
        Update_Hiring_Source = 31,
        Accessed_Self_Service = 32,
        Denied_Access_Self_Service = 33,
        Requested_AssessmentLink_Using_Self_Service = 34,
        Denied_Self_Service_Request = 35,
        Completed_Self_Service_Request = 36,
        Set_Appointment_Using_Using_Self_Service = 37,
        Unable_To_Create_Self_Service = 38
    }
}
