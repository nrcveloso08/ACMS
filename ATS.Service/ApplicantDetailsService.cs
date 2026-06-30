using ATS.Data.Repository;
using ATS.DTO.Applicant;
using ATS.DTO.InterviewSchedule;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace ATS.Service
{
    public interface IApplicantDetailsService
    {
        Task<ApplicantDetails?> GetDetailsAsync(Guid applicantId);
        Task<ApplicantDetails> UpdateDetailsAsync(ApplicantDetails details);

        Task<ApplicantDetailsResponseDto?> GetApplicantDetailsAsync(Guid applicantId);
        Task<ApplicantStatusOptionsResponseDto> GetStatusOptionsAsync(Guid applicantId);
        Task<IList<ApplicantSchedulingHistoryDto>> GetSchedulingHistoryAsync(Guid applicantId);
        Task<IList<ApplicantEmailTemplateDto>> GetEmailTemplatesAsync(Guid applicantId);
        Task<IList<ApplicantPrescreenResponseDto>> GetPrescreenResponsesAsync(Guid applicantId);
    }

    public class ApplicantDetailsService : IApplicantDetailsService
    {
        private readonly ICandidateDetailService _candidateDetailService;
        private readonly IApplicantRepository _applicantRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobAdvertisementRepository _jobAdvertisementRepository;
        private readonly IAdvertisementSourceRepository _advertisementSourceRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IDayforceRepository _dayforceRepository;
        private readonly IResumeRepository _resumeRepository;
        private readonly IInterviewRepository _interviewRepository;
        private readonly IApplicantSkillRepository _applicantSkillRepository;
        private readonly IEmploymentApplicationRepository _employmentApplicationRepository;
        private readonly IApplicantStatusRepository _applicantStatusRepository;
        private readonly IApplicantInterviewStatusRepository _applicantInterviewStatusRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ILogger<ApplicantDetailsService> _logger;

        public ApplicantDetailsService(
            ICandidateDetailService candidateDetailService,
            IApplicantRepository applicantRepository,
            IApplicationRepository applicationRepository,
            IJobAdvertisementRepository jobAdvertisementRepository,
            IAdvertisementSourceRepository advertisementSourceRepository,
            ILanguageRepository languageRepository,
            ILocationRepository locationRepository,
            IDayforceRepository dayforceRepository,
            IResumeRepository resumeRepository,
            IInterviewRepository interviewRepository,
            IApplicantSkillRepository applicantSkillRepository,
            IEmploymentApplicationRepository employmentApplicationRepository,
            IApplicantStatusRepository applicantStatusRepository,
            IApplicantInterviewStatusRepository applicantInterviewStatusRepository,
            IScheduleRepository scheduleRepository,
            ILogger<ApplicantDetailsService> logger)
        {
            _candidateDetailService = candidateDetailService;
            _applicantRepository = applicantRepository;
            _applicationRepository = applicationRepository;
            _jobAdvertisementRepository = jobAdvertisementRepository;
            _advertisementSourceRepository = advertisementSourceRepository;
            _languageRepository = languageRepository;
            _locationRepository = locationRepository;
            _dayforceRepository = dayforceRepository;
            _resumeRepository = resumeRepository;
            _interviewRepository = interviewRepository;
            _applicantSkillRepository = applicantSkillRepository;
            _employmentApplicationRepository = employmentApplicationRepository;
            _applicantStatusRepository = applicantStatusRepository;
            _applicantInterviewStatusRepository = applicantInterviewStatusRepository;
            _scheduleRepository = scheduleRepository;
            _logger = logger;
        }

        public async Task<ApplicantDetails?> GetDetailsAsync(Guid applicantId)
        {
            return await _candidateDetailService.GetApplicantDetailsAsync(applicantId);
        }

        public Task<ApplicantDetails> UpdateDetailsAsync(ApplicantDetails details)
        {
            throw new NotImplementedException("Applicant details update will be implemented in a later iteration.");
        }

        public async Task<ApplicantDetailsResponseDto?> GetApplicantDetailsAsync(Guid applicantId)
        {
            var details = await _candidateDetailService.GetApplicantDetailsAsync(applicantId);
            if (details == null)
                return null;

            return new ApplicantDetailsResponseDto
            {
                Summary = await BuildSummaryAsync(applicantId, details),
                Personal = BuildPersonalInfo(details),
                Address = BuildAddress(details),
                Phones = BuildPhones(details),
                Emails = BuildEmails(details),
                Dayforce = await BuildDayforceAsync(details),
                ApplicationInfo = await BuildApplicationInfoAsync(applicantId, details),
                AdditionalInfo = new Dictionary<string, object>(), // TODO: locale-specific additional info (Guatemala/Jamaica/Hyderabad etc.)
                ActivityLog = await BuildActivityLogAsync(applicantId),
                EmailLog = new List<ApplicantEmailLogDto>(), // TODO: no email log endpoint available yet
                Attachments = await BuildAttachmentsAsync(applicantId),
                PrescreenResponses = await GetPrescreenResponsesAsync(applicantId),
                InterviewAnswers = await BuildInterviewAnswersAsync(applicantId),
                SkillTests = await BuildSkillTestsAsync(applicantId),
                Jobs = await BuildJobsAsync(applicantId),
                RequisitionAssignments = new List<ApplicantRequisitionAssignmentDto>(), // TODO: requisition assignment history not yet exposed by ATSWebService
            };
        }

        public async Task<ApplicantStatusOptionsResponseDto> GetStatusOptionsAsync(Guid applicantId)
        {
            var response = new ApplicantStatusOptionsResponseDto();

            var statuses = await SafeAsync(() => _applicantInterviewStatusRepository.GetAllApplicantInterviewStatus(false));
            if (statuses != null)
            {
                response.StatusOptions = statuses
                    .OrderBy(s => s.SortOrder)
                    .Select(s => new ApplicantStatusOptionDto { Value = s.Id.ToString(), Label = s.Name })
                    .ToList();
            }

            var details = await SafeAsync(() => _candidateDetailService.GetApplicantDetailsAsync(applicantId));
            if (details != null)
            {
                var timeSlotConfig = await SafeAsync(() => _scheduleRepository.GetTimeSlotConfig(details.Location_Id));
                if (timeSlotConfig?.Segments != null)
                {
                    response.InterviewTimeslotOptions = timeSlotConfig.Segments
                        .Select(seg =>
                        {
                            var start = seg.StartTime.ToString(@"hh\:mm");
                            var end = seg.EndTime.ToString(@"hh\:mm");
                            return new ApplicantStatusOptionDto { Value = start, Label = $"{start} - {end}" };
                        })
                        .ToList();
                }
            }

            return response;
        }

        public async Task<IList<ApplicantSchedulingHistoryDto>> GetSchedulingHistoryAsync(Guid applicantId)
        {
            // TODO: ATSWebService only exposes the applicant's current appointment, not a full
            // change history. Surface the current appointment as a single-entry history for now.
            var appointment = await SafeAsync(() => _scheduleRepository.GetApplicantAppointment(applicantId));
            if (appointment?.AppointmentDate == null)
                return new List<ApplicantSchedulingHistoryDto>();

            return new List<ApplicantSchedulingHistoryDto>
            {
                new()
                {
                    Id = appointment.ApplicationId.ToString(),
                    DateChanged = string.Empty, // TODO: not available from ATSWebService
                    AppointmentDateTime = appointment.AppointmentDate.Value.ToString("MM/dd/yyyy hh:mm tt")
                }
            };
        }

        public Task<IList<ApplicantEmailTemplateDto>> GetEmailTemplatesAsync(Guid applicantId)
        {
            // TODO: Requires ATSEmailTemplateRepository.GetByATSStatus, which depends on the deferred
            // StatusActionMap DB lookup (see ATS_BACKEND_ARCHITECTURE.md). Returning empty for now.
            return Task.FromResult<IList<ApplicantEmailTemplateDto>>(new List<ApplicantEmailTemplateDto>());
        }

        public async Task<IList<ApplicantPrescreenResponseDto>> GetPrescreenResponsesAsync(Guid applicantId)
        {
            var submittedApplications = await SafeAsync(() => _employmentApplicationRepository.GetAllSubmittedByApplicantId(applicantId));
            if (submittedApplications == null)
                return new List<ApplicantPrescreenResponseDto>();

            return submittedApplications
                .Where(ext => ext?.EmploymentApplication?.PrescreenResponses != null)
                .SelectMany(ext => ext.EmploymentApplication.PrescreenResponses)
                .Select(r => new ApplicantPrescreenResponseDto
                {
                    Id = r.Id.ToString(),
                    Question = r.PrescreenQuestion?.QuestionText ?? string.Empty,
                    Response = !string.IsNullOrEmpty(r.CapturedResponseText)
                        ? r.CapturedResponseText
                        : r.SelectedResponse?.ResponseText ?? string.Empty
                })
                .ToList();
        }

        #region PRIVATE BUILDERS

        private async Task<ApplicantSummaryDto> BuildSummaryAsync(Guid applicantId, ApplicantDetails details)
        {
            var location = await SafeAsync(() => _locationRepository.Get(details.Location_Id));
            var recentStatus = await SafeAsync(() => _applicantStatusRepository.GetRecentStatus(applicantId));

            return new ApplicantSummaryDto
            {
                ApplicantId = applicantId.ToString(),
                DayforceId = details.Dayforce_Id ?? string.Empty,
                CurrentStatus = FormatEnum(details.CurrentStatus),
                StatusDisposition = recentStatus?.Message ?? string.Empty,
                LocationId = details.Location_Id,
                LocationName = location?.Name ?? string.Empty,
                ApplicationCountry = details.ApplicationCountry ?? string.Empty,
            };
        }

        private static ApplicantPersonalInfoDto BuildPersonalInfo(ApplicantDetails details)
        {
            return new ApplicantPersonalInfoDto
            {
                FirstName = details.FirstName ?? string.Empty,
                LastName = details.LastName ?? string.Empty,
                // TODO: SecondName/ThirdName/MiddleName/SecondLastName/MarriedLastName are locale-specific
                // (e.g. Guatemala additional names) and not yet exposed via ApplicantDetails.
                SecondName = string.Empty,
                ThirdName = string.Empty,
                MiddleName = string.Empty,
                SecondLastName = string.Empty,
                MarriedLastName = string.Empty,
                DateOfBirth = details.BirthDate ?? string.Empty,
                WhatsappAlertsEnabled = details.TextMessageOptIn,
                HarverVideoUrl = details.HarverVideoUrl ?? string.Empty,
            };
        }

        private static ApplicantAddressDto BuildAddress(ApplicantDetails details)
        {
            var address = details.ApplicantAddress;
            if (address == null)
                return new ApplicantAddressDto();

            return new ApplicantAddressDto
            {
                Street = address.Street ?? string.Empty,
                ApartmentNo = address.ApartmentNo ?? string.Empty,
                City = address.City ?? string.Empty,
                State = address.State ?? string.Empty,
                PostCode = address.PostCode ?? string.Empty,
                CountryName = address.CountryName ?? string.Empty,
            };
        }

        private static IList<ApplicantPhoneDto> BuildPhones(ApplicantDetails details)
        {
            return (details.PhoneNumbers ?? new List<ApplicantPhoneNumber>())
                .Select(p => new ApplicantPhoneDto
                {
                    Id = p.Id.ToString(),
                    PhoneNumber = p.PhoneNumber ?? string.Empty,
                    IsPrimary = p.IsPrimary
                })
                .ToList();
        }

        private static IList<ApplicantEmailDto> BuildEmails(ApplicantDetails details)
        {
            return (details.EmailAddresses ?? new List<ApplicantEmailAddress>())
                .Select(e => new ApplicantEmailDto
                {
                    Id = e.Id.ToString(),
                    EmailAddress = e.EmailAddress ?? string.Empty,
                    IsPrimary = e.IsPrimary
                })
                .ToList();
        }

        private async Task<ApplicantDayforceInfoDto> BuildDayforceAsync(ApplicantDetails details)
        {
            var dto = new ApplicantDayforceInfoDto
            {
                DayforceId = details.Dayforce_Id ?? string.Empty,
                EmploymentStatusGroupName = details.EmploymentStatusGroupName ?? string.Empty,
                StatusReasonName = details.StatusReasonName ?? string.Empty,
                // TODO: OnboardingPolicyXrefCode/JobXrefCode/OrganizationXrefCode are not yet
                // populated on ApplicantDetails (Dayforce locale data pending wiring).
                OnboardingPolicyXrefCodeId = details.OnboardingPolicyXrefCode_Id != 0 ? details.OnboardingPolicyXrefCode_Id.ToString() : string.Empty,
                JobXrefCodeId = details.JobXrefCode_Id != 0 ? details.JobXrefCode_Id.ToString() : string.Empty,
                OrganizationXrefCodeId = details.OrganizationXrefCode_Id != 0 ? details.OrganizationXrefCode_Id.ToString() : string.Empty,
            };

            if (!string.IsNullOrWhiteSpace(details.Dayforce_Id))
            {
                var dfStatus = await SafeAsync(() => _dayforceRepository.GetEmployeeStatus(details.Dayforce_Id));
                if (dfStatus != null)
                    dto.EffectiveDate = dfStatus.EmployeeEmploymentStatusEffectiveStart.ToString("MM/dd/yyyy");
            }

            return dto;
        }

        private async Task<ApplicantApplicationInfoDto> BuildApplicationInfoAsync(Guid applicantId, ApplicantDetails details)
        {
            var dto = new ApplicantApplicationInfoDto
            {
                EmploymentType = details.ApplicantEmploymentType ?? string.Empty,
                Wage = details.Wage > 0 ? details.Wage.ToString("0.00") : string.Empty,
                CSINote = details.CSINote ?? string.Empty,
                RequisitionId = details.RequisitionId.HasValue && details.RequisitionId.Value != 0
                    ? details.RequisitionId.Value.ToString()
                    : (details.JobAdRequisitionId != 0 ? details.JobAdRequisitionId.ToString() : string.Empty),
                // TODO: HiringSourceLocation requires mapping HiringSourceLocation_Id via IHiringSourceRepository,
                // not yet wired for read-only details.
                HiringSourceLocation = string.Empty,
            };

            var recentApplication = await SafeAsync(() => _applicationRepository.GetRecentApplication(applicantId));
            if (recentApplication != null)
            {
                var jobAd = await SafeAsync(() => _jobAdvertisementRepository.Get(recentApplication.JobAdvertismentId));
                if (jobAd != null)
                    dto.JobAdvertisement = jobAd.PublishedTitle ?? jobAd.Name ?? string.Empty;

                var advertisementSource = await SafeAsync(() => _advertisementSourceRepository.Get(recentApplication.AdvertisementSourceId));
                if (advertisementSource != null)
                    dto.AdvertisementSource = advertisementSource.Name ?? string.Empty;
            }

            var languages = await SafeAsync(() => _languageRepository.GetAll());
            if (languages != null && languages.TryGetValue(details.LanguageId, out var languageName))
                dto.LanguageName = languageName;

            return dto;
        }

        private async Task<IList<ApplicantActivityLogDto>> BuildActivityLogAsync(Guid applicantId)
        {
            var notes = await SafeAsync(() => _applicantRepository.GetNotes(applicantId));
            if (notes == null)
                return new List<ApplicantActivityLogDto>();

            return notes
                .OrderByDescending(n => n.TimeStamp)
                .Select(n => new ApplicantActivityLogDto
                {
                    Id = n.Id.ToString(),
                    Timestamp = n.TimeStamp.ToString("MM/dd/yyyy hh:mm tt"),
                    UserName = n.UserName ?? string.Empty,
                    Action = FormatEnum(n.Status),
                    Status = FormatEnum(n.Status),
                    Details = n.ActionDetails ?? string.Empty,
                })
                .ToList();
        }

        private async Task<IList<ApplicantAttachmentDto>> BuildAttachmentsAsync(Guid applicantId)
        {
            var resumes = await SafeAsync(() => _resumeRepository.GetApplicantResumes(applicantId));
            if (resumes == null)
                return new List<ApplicantAttachmentDto>();

            return resumes
                .Where(r => !r.IsDeleted)
                .Select(r => new ApplicantAttachmentDto
                {
                    Id = r.Id.ToString(),
                    Name = r.Name ?? string.Empty,
                    Type = "Resume",
                    UploadedDate = r.Modified.ToString("yyyy-MM-dd"),
                })
                .ToList();
        }

        private async Task<IList<ApplicantInterviewAnswerDto>> BuildInterviewAnswersAsync(Guid applicantId)
        {
            var interviews = await SafeAsync(() => _interviewRepository.GetAllByApplicantId(applicantId));
            if (interviews == null)
                return new List<ApplicantInterviewAnswerDto>();

            var result = new List<ApplicantInterviewAnswerDto>();
            var number = 1;

            foreach (var interview in interviews.Where(i => !i.IsDeleted))
            {
                if (interview.Responses == null)
                    continue;

                foreach (var response in interview.Responses)
                {
                    var answer = response.CapturedResponseText;
                    if (string.IsNullOrEmpty(answer))
                    {
                        answer = response.PrescreenQuestion?.ResponseOptions?
                            .FirstOrDefault(o => o.Id == response.SelectedResponse_Id)?.ResponseText ?? string.Empty;
                    }

                    result.Add(new ApplicantInterviewAnswerDto
                    {
                        Id = response.Id.ToString(),
                        Number = number++,
                        Question = response.PrescreenQuestion?.QuestionText ?? string.Empty,
                        Answer = answer,
                    });
                }
            }

            return result;
        }

        private async Task<IList<ApplicantSkillTestDto>> BuildSkillTestsAsync(Guid applicantId)
        {
            var results = await SafeAsync(() => _applicantSkillRepository.GetSkillTestResults(applicantId, true));
            if (results == null)
                return new List<ApplicantSkillTestDto>();

            return results
                .OrderByDescending(r => r.TestDate)
                .Select(r => new ApplicantSkillTestDto
                {
                    Id = r.Id.ToString(),
                    Skill = r.SkillName ?? string.Empty,
                    TestingDate = r.TestDate.ToString("yyyy-MM-dd"),
                    Result = $"{r.Score:0.##} - {(r.Passed ? "Pass" : "Fail")}",
                    Note = r.Notes ?? string.Empty,
                })
                .ToList();
        }

        private async Task<IList<ApplicantJobDto>> BuildJobsAsync(Guid applicantId)
        {
            var applications = await SafeAsync(() => _applicationRepository.GetApplicationsByApplicant(applicantId));
            if (applications == null)
                return new List<ApplicantJobDto>();

            var result = new List<ApplicantJobDto>();

            foreach (var application in applications)
            {
                var jobAd = await SafeAsync(() => _jobAdvertisementRepository.Get(application.JobAdvertismentId));
                var location = jobAd != null ? await SafeAsync(() => _locationRepository.Get(jobAd.LocationId)) : null;
                var advertisementSource = await SafeAsync(() => _advertisementSourceRepository.Get(application.AdvertisementSourceId));

                result.Add(new ApplicantJobDto
                {
                    Id = application.Id.ToString(),
                    Job = !string.IsNullOrEmpty(application.JobName) ? application.JobName : (jobAd?.PublishedTitle ?? jobAd?.Name ?? string.Empty),
                    Location = location?.Name ?? string.Empty,
                    AdvertisementSource = advertisementSource?.Name ?? string.Empty,
                    LastUpdated = application.ApplicationDate.ToString("yyyy-MM-dd"),
                    Status = application.Status?.Name ?? FormatEnum(application.InternalStatus),
                });
            }

            return result;
        }

        #endregion

        private static string FormatEnum<T>(T value) where T : struct, Enum
        {
            return value.ToString().Replace("_", " ");
        }

        private async Task<T?> SafeAsync<T>(Func<Task<T>> action, [CallerArgumentExpression(nameof(action))] string? operation = null)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "External dependency call failed for {Operation}; returning safe empty result.", operation);
                return default;
            }
        }
    }
}
