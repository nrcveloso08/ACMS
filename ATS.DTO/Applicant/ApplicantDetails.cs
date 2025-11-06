using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATS.DTO.Applicant
{
    public class ApplicantDetails
    {
        public ApplicantDetails()
        {
            OnboardingPolicyXrefCodes = new List<OptionDTO>();
            JobXrefCodes = new List<OptionDTO>();
            DepartmentXrefCodes = new List<OptionDTO>();
            OrganizationXrefCodes = new List<OptionDTO>();
        }
        public Guid Id { get; set; }
        public int ApplicantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public ApplicantAddress ApplicantAddress { get; set; }
        public IList<ApplicantPhoneNumber> PhoneNumbers { get; set; }
        public IList<ApplicantEmailAddress> EmailAddresses { get; set; }
        public IList<OptionDTO> JobLocations { get; set; }
        public IList<OptionDTO> AvailableJobs { get; set; }
        public DateTimeOffset? OrientationEnd { get; set; }
        public DateTimeOffset? OrientationStart { get; set; }
        public int? RequisitionId { get; set; }
        public InterviewStatus CurrentStatus { get; set; }
        public decimal Wage { get; set; }
        public int LanguageId { get; set; }
        public string ApplicantEmploymentType { get; set; }
        public int LanguageTestScore { get; set; }
        public int TypingTestScore { get; set; }
        public int SpellingTestScore { get; set; }
        public int GrammarTestScore { get; set; }
        public int? Agency_Id { get; set; }
        public int Location_Id { get; set; }
        public string CSINote { get; set; }
        public bool TextMessageOptIn { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool IsDeleted { get; set; }
        public string HarverVacancyURL { get; set; }
        public Guid RecentApplicationId { get; set; }
        public int JobAdRequisitionId { get; set; }
        public string ApplicationCountry { get; set; }
        public string PhoneMaskingFormat { get; set; }
        public string InternationalCode { get; set; }
        public string Dayforce_Id { get; set; }
        public string EmploymentStatusGroupName { get; set; }
        public string StatusReasonName { get; set; }
        public string SSN { get; set; }
        public bool RehireEligibility { get; set; }
        public string GeoLocationName { get; set; }

        public bool IsSmsEnabled { get; set; }

        [Required]
        [Display(Name = "Onboarding Policy")]
        public int OnboardingPolicyXrefCode_Id { get; set; }
        public IList<OptionDTO> OnboardingPolicyXrefCodes { get; set; }

        [Required]
        [Display(Name = "Job")]
        public int JobXrefCode_Id { get; set; }

        public IList<OptionDTO> JobXrefCodes { get; set; }
        [Required]
        [Display(Name = "Department")]
        public int DepartmentXrefCode_Id { get; set; }

        public IList<OptionDTO> DepartmentXrefCodes { get; set; }
        [Required]
        [Display(Name = "Organization")]
        public int OrganizationXrefCode_Id { get; set; }

        public IList<OptionDTO> OrganizationXrefCodes { get; set; }

        public string DF_RehireEligibility { get; set; }

        public string TRN { get; set; }
        public string NIS { get; set; }
        public string IdNumber { get; set; }
        public string IdType { get; set; }
        public int JamaicaReqId { get; set; }

        public string EmployeeRole { get; set; }
        public int? WaveCategory { get; set; }
        public decimal? GrossSalary { get; set; }
        public decimal? HourlyRate { get; set; }
        public int HyderabadAddInfoId { get; set; }
        public bool OrientationStatus { get; set; }
        public int HiringSourceLocation_Id { get; set; }
        public string HarverProfileUrl { get; set; }
        public string HarverVideoUrl { get; set; }
    }
}
