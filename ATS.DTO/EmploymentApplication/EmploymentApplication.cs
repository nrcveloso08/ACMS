using ATS.DTO.Applicant;
using ATS.DTO.Guatemala;
using ATS.DTO.PrescreenQuestion;

namespace ATS.DTO.EmploymentApplication
{
    public class EmploymentApplication
    {
        public int Id { get; set; }
        public EmploymentApplicationStatus Status { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public Guid Applicant_Id { get; set; }
        public Applicant.Applicant Applicant { get; set; }
        public string CountryName { get; set; }
        public DateTimeOffset DateSubmitted { get; set; }
        public string JobAdvertisement { get; set; }
        public int JobAdvertisement_Id { get; set; }

        public ApplicantPersonalData ApplicantPersonalData { get; set; }
        public ICollection<ApplicantEmploymentHistory> ApplicantEmploymentHistories { get; set; }
        public ICollection<ApplicantEducationHistory> ApplicantEducationHistories { get; set; }
        public ICollection<ApplicantLanguageSkill> ApplicantLanguageSkills { get; set; }
        public ICollection<ApplicantPreferredScedule> ApplicantPreferredScedules { get; set; }
        public ICollection<EmploymentApplicationQuestionResponse> EmploymentApplicationQuestionResponses { get; set; }
        public ICollection<ApplicationPreScreenQuestionResponse> PrescreenResponses { get; set; }

        public ICollection<GuatemalaAdditionalNames> GuatemalaAdditionalNames { get; set; }
        public ICollection<GuatemalaAdditionalInfo> GuatemalaAdditionalInfos { get; set; }
        public ICollection<GuatemalaDepartment> GuatemalaDepartments { get; set; }
        public ICollection<GuatemalaCharacterReference> GuatemalaCharacterReferences { get; set; }
        public ICollection<GuatemalaEducationInfo> GuatemalaEducationInfos { get; set; }
        public int DocumentId { get; set; }
    }

    public class EmploymentApplicationExt
    {
        public EmploymentApplication EmploymentApplication { get; set; }
        public ApplicantAddress ApplicantAddress { get; set; }
    }

    public enum EmploymentApplicationStatus
    {
        Created = 1,
        In_Progress,
        Submitted,
        Expired
    }
}
