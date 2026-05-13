using ATS.DTO.Helpers;
using ATS.DTO.InterviewSchedule;
using ATS.DTO.RequisitionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Applicant
{
    public class ApplicantBasicInfo
    {
        public Guid Applicant_Id { get; set; }
        public string FullName { get => $"{this.FirstName} {this.MiddleName} {this.MarriedSurName} {this.LastName} {this.SecondLastName}"; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string MarriedSurName { get; set; }
        public string SecondLastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int Location_Id { get; set; }
        public InterviewStatus CurrentStatus { get; set; }
        public string CurrentStatusDisplay
        {
            get => this.CurrentStatus.GetDescription();
        }
        public string EmploymentType { get; set; }
        public bool ReHire { get; set; }
        public string ReHireDisplay
        {
            get => this.ReHire ? "Yes" : "No";
        }
        public bool InternalHire { get; set; }
        public string InternalHireDisplay
        {
            get => this.InternalHire ? "Internal" : "External";
        }
        public decimal Wage { get; set; }
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string Location { get; set; }
        public string ApplicationCountry { get; set; }
        public int RequisitionRequestId { get; set; }
        public Requisition RequisitionRequest { get; set; }
        public int PendingFollowUpItemsCount { get; set; }
        public ICollection<ApplicantPhoneNumber> PhoneNumbers { get; set; }
        public int HiringSourceLocation_Id { get; set; }
        public bool RehireEligibility { get; set; }
        public string Dayforce_Id { get; set; }
        public InterviewMode ApplicationInterviewMode { get; set; }
        public bool IsDeleted { get; set; }

        public Guid Application_Id { get; set; }
        public int ApplicationStatusId { get; set; }
        public string ApplicationStatusName { get; set; }
        public string JobName { get; set; }
        public DateTimeOffset RecentTimeStamp { get; set; }

        public string AdvertisementSourceName { get; set; }
    }
}
