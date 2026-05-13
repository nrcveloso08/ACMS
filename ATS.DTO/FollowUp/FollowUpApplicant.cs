using ATS.DTO.Applicant;
using ATS.DTO.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.FollowUp
{
    public class FollowUpApplicant 
    {
        public int Id { get; set; }
        public Guid Applicant_Id { get; set; }
        public ApplicantContactInfo Applicant { get; set; }
        public int FollowUpDisposition_Id { get; set; }
        public string FollowUpDispositionText { get; set; }
        public DateTime? DueDate { get; set; }
        public string StatusDisplay
        {
            get => this.Status.GetDescription();
        }
        public FollowUpStatus Status { get; set; } = FollowUpStatus.New;
        public string RequestedBy { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string CompletedBy { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
    }

    public enum FollowUpStatus
    {
        New = 1,
        In_Progress,
        Completed
    }
}
