using System;
using System.Collections.Generic;

namespace ATS.DTO.Applicant
{
    /// <summary>
    /// Read-only payload for the Applicant Details Vue page (GET /api/v1/applicant-details/{applicantId}).
    /// Mirrors the shape of ApplicantForm in src/types/applicant.ts.
    /// </summary>
    public class ApplicantDetailsResponseDto
    {
        public ApplicantSummaryDto Summary { get; set; } = new();
        public ApplicantPersonalInfoDto Personal { get; set; } = new();
        public ApplicantAddressDto Address { get; set; } = new();
        public IList<ApplicantPhoneDto> Phones { get; set; } = new List<ApplicantPhoneDto>();
        public IList<ApplicantEmailDto> Emails { get; set; } = new List<ApplicantEmailDto>();
        public ApplicantDayforceInfoDto Dayforce { get; set; } = new();
        public ApplicantApplicationInfoDto ApplicationInfo { get; set; } = new();
        public IDictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
        public IList<ApplicantActivityLogDto> ActivityLog { get; set; } = new List<ApplicantActivityLogDto>();
        public IList<ApplicantEmailLogDto> EmailLog { get; set; } = new List<ApplicantEmailLogDto>();
        public IList<ApplicantAttachmentDto> Attachments { get; set; } = new List<ApplicantAttachmentDto>();
        public IList<ApplicantPrescreenResponseDto> PrescreenResponses { get; set; } = new List<ApplicantPrescreenResponseDto>();
        public IList<ApplicantInterviewAnswerDto> InterviewAnswers { get; set; } = new List<ApplicantInterviewAnswerDto>();
        public IList<ApplicantSkillTestDto> SkillTests { get; set; } = new List<ApplicantSkillTestDto>();
        public IList<ApplicantJobDto> Jobs { get; set; } = new List<ApplicantJobDto>();
        public IList<ApplicantRequisitionAssignmentDto> RequisitionAssignments { get; set; } = new List<ApplicantRequisitionAssignmentDto>();
    }

    public class ApplicantSummaryDto
    {
        public string ApplicantId { get; set; } = string.Empty;
        public string DayforceId { get; set; } = string.Empty;
        public string CurrentStatus { get; set; } = string.Empty;
        public string StatusDisposition { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public string ApplicationCountry { get; set; } = string.Empty;
    }

    public class ApplicantPersonalInfoDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string ThirdName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string SecondLastName { get; set; } = string.Empty;
        public string MarriedLastName { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public bool WhatsappAlertsEnabled { get; set; }
        public string HarverVideoUrl { get; set; } = string.Empty;
    }

    public class ApplicantAddressDto
    {
        public string Street { get; set; } = string.Empty;
        public string ApartmentNo { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
    }

    public class ApplicantPhoneDto
    {
        public string Id { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
    }

    public class ApplicantEmailDto
    {
        public string Id { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
    }

    public class ApplicantDayforceInfoDto
    {
        public string DayforceId { get; set; } = string.Empty;
        public string EmploymentStatusGroupName { get; set; } = string.Empty;
        public string StatusReasonName { get; set; } = string.Empty;
        public string EffectiveDate { get; set; } = string.Empty;
        public string OnboardingPolicyXrefCodeId { get; set; } = string.Empty;
        public string JobXrefCodeId { get; set; } = string.Empty;
        public string OrganizationXrefCodeId { get; set; } = string.Empty;
    }

    public class ApplicantApplicationInfoDto
    {
        public string JobAdvertisement { get; set; } = string.Empty;
        public string AdvertisementSource { get; set; } = string.Empty;
        public string RequisitionId { get; set; } = string.Empty;
        public string LanguageName { get; set; } = string.Empty;
        public string EmploymentType { get; set; } = string.Empty;
        public string Wage { get; set; } = string.Empty;
        public string HiringSourceLocation { get; set; } = string.Empty;
        public string CSINote { get; set; } = string.Empty;
    }

    public class ApplicantActivityLogDto
    {
        public string Id { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }

    public class ApplicantEmailLogDto
    {
        public string Id { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string Preview { get; set; } = string.Empty;
    }

    public class ApplicantAttachmentDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string UploadedDate { get; set; } = string.Empty;
    }

    public class ApplicantPrescreenResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
    }

    public class ApplicantInterviewAnswerDto
    {
        public string Id { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }

    public class ApplicantSkillTestDto
    {
        public string Id { get; set; } = string.Empty;
        public string Skill { get; set; } = string.Empty;
        public string TestingDate { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }

    public class ApplicantJobDto
    {
        public string Id { get; set; } = string.Empty;
        public string Job { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string AdvertisementSource { get; set; } = string.Empty;
        public string LastUpdated { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    public class ApplicantRequisitionAssignmentDto
    {
        public string Id { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
        public string Wave { get; set; } = string.Empty;
        public string Disposition { get; set; } = string.Empty;
        public string Wage { get; set; } = string.Empty;
    }

    /// <summary>Response for GET /api/v1/applicant-details/{applicantId}/status-options.</summary>
    public class ApplicantStatusOptionsResponseDto
    {
        public IList<ApplicantStatusOptionDto> StatusOptions { get; set; } = new List<ApplicantStatusOptionDto>();
        public IList<ApplicantStatusOptionDto> InterviewTimeslotOptions { get; set; } = new List<ApplicantStatusOptionDto>();
    }

    public class ApplicantStatusOptionDto
    {
        public string Value { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }

    /// <summary>Entry for GET /api/v1/applicant-details/{applicantId}/scheduling-history.</summary>
    public class ApplicantSchedulingHistoryDto
    {
        public string Id { get; set; } = string.Empty;
        public string DateChanged { get; set; } = string.Empty;
        public string AppointmentDateTime { get; set; } = string.Empty;
    }

    /// <summary>Entry for GET /api/v1/applicant-details/{applicantId}/email-templates.</summary>
    public class ApplicantEmailTemplateDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Cc { get; set; } = string.Empty;
        public string Bcc { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
