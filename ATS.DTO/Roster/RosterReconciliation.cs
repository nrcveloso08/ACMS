using System;
using ATS.DTO;

namespace ATS.DTO.Roster
{
    public class RosterReconciliation
    {
        public int Id { get; set; }
        public string CandidateName { get; set; }
        public string HireType { get; set; }
        public string DayforceId { get; set; }
        public DateTime? ReportedDate { get; set; }
        public string DayforceProfileStatus { get; set; }
        public InterviewStatus ATSProfileStatus { get; set; }
        public int RequisitionId { get; set; }
        public DateTime? IssueAssignedDate { get; set; }
        public DateTime? ResolveDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public Guid? ApplicantId { get; set; }
        public string AssignedTo { get; set; }
    }

    public class RosterReconciliationLogs
    {
        public int Id { get; set; }
        public int RosterReconciliation_Id { get; set; }
        public string UserName { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
    }
}
