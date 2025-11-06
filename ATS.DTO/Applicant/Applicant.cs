using ATS.DTO;

namespace ATS.DTO.Applicant
{

    public class Applicant
    {
        public Applicant()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public int ApplicantId { get; set; }

        public IList<Application.Application> Applications { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AddressId { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }

        public DateTime? Availability { get; set; }

        public bool TextMessageOptIn { get; set; }

        public string CSINote { get; set; }

        public string FCRAStatus { get; set; }

        //KITT Location
        public int Location_Id { get; set; }

        public bool IsDeleted { get; set; }

        public int? Agency_Id { get; set; }

        public int GrammarTestScore { get; set; }

        public int SpellingTestScore { get; set; }

        public int TypingTestScore { get; set; }

        public int LanguageTestScore { get; set; }

        public string ApplicantEmploymentType { get; set; }

        public int LanguageId { get; set; }

        public decimal Wage { get; set; }

        public int ApplicantCSIResultId { get; set; }

        public InterviewStatus CurrentStatus { get; set; }

        public int? RequisitionId { get; set; }

        public DateTimeOffset? OrientationStart { get; set; }

        public DateTimeOffset? OrientationEnd { get; set; }

        //public ICollection<RequisitionRequest.RequisitionAssignment> RequisitionAssignments { get; set; }

        //public ICollection<Skills.SkillTestResult> SkillTestResults { get; set; }
        //public bool ReHire { get; set; }
        //public bool InternalHire { get; set; }
        //public int HiringSourceLocation_Id { get; set; }
        //public DTO.HiringSource.HiringSourceLocation HiringSourceLocation { get; protected set; }
        //public string SuperpunchId { get; set; }
        //public bool AttendedTrainingDay1 { get; set; }
        //public bool TrainingEmailSent { get; set; }
        //public ComputerPickupSchedule ComputerSchedule { get; set; }
        //public HRInformation HRInformation { get; set; }
        public string Dayforce_Id { get; set; }
        //public string SSN { get; set; }
        //public bool RehireEligibility { get; set; }
        //public int OnboardingPolicyXrefCode_Id { get; set; }
        //public int JobXrefCode_Id { get; set; }
        //public int DepartmentXrefCode_Id { get; set; }
        //public int OrganizationXrefCode_Id { get; set; }
        //public bool IsSmsEnabled { get; set; }

        //public bool OrientationStatus { get; set; }
        //public bool SPHireType { get; set; }
        //public ICollection<GuatemalaAdditionalInfo> GuatemalaAdditionalInfo { get; set; }


    }
}
