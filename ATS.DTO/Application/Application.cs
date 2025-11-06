namespace ATS.DTO.Application
{
    public class Application
    {
        public Application()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        public string ApplicantFirstName { get; set; }
        public string ApplicantLastName { get; set; }
        public string ApplicantEmailAddress { get; set; }
        public string ApplicantPhoneNumber { get; set; }
        public int ApplicantAgency_Id { get; set; }
        public DateTimeOffset ApplicationDate { get; set; }
        public DateTime AvailableStartDate { get; set; }
        public ShiftType DesiredShiftType { get; set; }
        public int JobAdvertismentId { get; set; }
        public int KITTJobAdvertisementId { get; set; }
        public bool PrescreenResult { get; set; }
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTimeSlot { get; set; }
        public int AdvertisementSourceId { get; set; }
        public int LegacyPrerecruitmentId { get; set; }
        public ApplicationStatus Status { get; set; }
        public InternalApplicationStatus InternalStatus { get; set; }
        public string ReferralCode { get; set; }
        public string JobName { get; set; }
        public string TestResults { get; set; }
        public string Recruiter { get; set; }
        public string BestTimeToCall { get; set; }
        public string ApplicantSource { get; set; }
        public FileSpec Resume { get; set; }
        public virtual EmployeeStatus EmployeeStatus { get; set; }
        public virtual Applicant.ApplicantStatus ApplicantStatus { get; set; }
        public virtual InterviewSchedule.InterviewType InterviewType { get; set; }
        public virtual Applicant.ApplicantInterviewStatus ApplicantInterviewStatus { get; set; }
        public int LanguageId { get; set; }
        public int RequisitionRequestId { get; set; }
        public string FurstPersonAssessmentURL { get; set; }
        public string InterviewURL { get; set; }
        public string HarverProfileUrl { get; set; }
        public string HarverVideoUrl { get; set; }
    }

    public enum ShiftType
    {
        Unset = 0, PartTime = 1, FullTime = 2
    }


    public class EmployeeStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }

}