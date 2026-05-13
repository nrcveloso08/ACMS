using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.FCRA
{
    public class FCRASentForReview
    {
        public int Id { get; set; }
        public int FCRA_Id { get; set; }
        public bool IsInternalTransfer { get; set; }
        public DateTimeOffset? DateResultsReceived { get; set; }
        public DateTimeOffset? DatePARequested { get; set; }
        public DateTimeOffset? DatePASentToHR { get; set; }
        public DateTimeOffset? DateOfPAMeeting { get; set; }
        public DateTimeOffset? FiveDayPeriod { get; set; }
        public DateTimeOffset? AddlConv { get; set; }
        public DateTimeOffset? DateDisputeFiled { get; set; }
        public DateTimeOffset? DOCReceived { get; set; }
        public DateTimeOffset? ExpectingDocuments { get; set; }
        public SRStatus Status { get; set; }
        public DateTimeOffset? DateAARequested { get; set; }
        public DateTimeOffset? DateAASentToHR { get; set; }
        public DateTimeOffset? DateOfAAMeeting { get; set; }
        public Reason Reason { get; set; }
        public string Notes { get; set; }
        public string AddConv2 { get; set; }
        public AdverseStatus AdverseStatus { get; set; }
    }

    public enum SRStatus
    {
        On_Hold = 1,
        Cleared = 2
    }

    public enum Reason
    {
        Termed_in_Training = 1,
        Termed_in_Production = 2,
        Termed_for_Other_Reasons = 3,
        Termed_Before_Training = 4,
        Cleared = 5,
        NCNS_Resigned = 6,
    }

    public enum AdverseStatus
    {
        Adverse = 1,
        Pre_Adverse = 2
    }
}
