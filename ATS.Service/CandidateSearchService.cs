using ATS.Data.Repository;
using ATS.DTO.Applicant;
using ATS.DTO.CandidateSearch;
using ATS.DTO.InterviewSchedule;
using ATS.DTO.RequisitionRequest;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace ATS.Service
{
    public interface ICandidateSearchService
    {
        Task<IEnumerable<Applicant>> SearchByNameAsync(string namePartial, int max = 50);
        Task<List<Applicant>> SearchByEmailAsync(string email, int max = 50);
        Task<List<Applicant>> SearchByPhoneAsync(string phone, int max = 1);

        Task<CandidateSearchDropdownsDto> GetDropdownsAsync();
        Task<CandidateSearchResponseDto> SearchAsync(CandidateSearchRequestDto request);
    }

    public class CandidateSearchService : ICandidateSearchService
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IInterviewStatusRepository _interviewStatusRepository;
        private readonly IRequisitionRequestRepository _requisitionRequestRepository;
        private readonly ILogger<CandidateSearchService> _logger;

        public CandidateSearchService(
            IApplicantRepository applicantRepository,
            ILocationRepository locationRepository,
            IInterviewStatusRepository interviewStatusRepository,
            IRequisitionRequestRepository requisitionRequestRepository,
            ILogger<CandidateSearchService> logger)
        {
            _applicantRepository = applicantRepository;
            _locationRepository = locationRepository;
            _interviewStatusRepository = interviewStatusRepository;
            _requisitionRequestRepository = requisitionRequestRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Applicant>> SearchByNameAsync(string namePartial, int max = 50)
        {
            return await _applicantRepository.GetByNamePartial(namePartial, max);
        }

        public async Task<List<Applicant>> SearchByEmailAsync(string email, int max = 50)
        {
            return await _applicantRepository.GetByEmail(email, max);
        }

        public async Task<List<Applicant>> SearchByPhoneAsync(string phone, int max = 1)
        {
            return await _applicantRepository.GetByPhone(phone, max);
        }

        public async Task<CandidateSearchDropdownsDto> GetDropdownsAsync()
        {
            var dropdowns = new CandidateSearchDropdownsDto
            {
                ShowProfiles = new List<CandidateSearchOptionDto>
                {
                    new() { Label = "Active", Value = "active" },
                    new() { Label = "Inactive", Value = "inactive" },
                    new() { Label = "All", Value = "all" },
                },
                HireType = Enum.GetValues<HireType>()
                    .Select(h => new CandidateSearchOptionDto
                    {
                        Label = h.ToString(),
                        Value = ((int)h).ToString()
                    })
                    .ToList()
            };

            var statuses = await SafeAsync(() => _interviewStatusRepository.GetActiveInterviewStatuses());
            if (statuses != null)
            {
                dropdowns.Status = statuses
                    .Select(s => new CandidateSearchOptionDto
                    {
                        Label = s.Name,
                        Value = ((int)s.InterviewStatus).ToString()
                    })
                    .ToList();
            }

            var dispositions = await SafeAsync(() => _interviewStatusRepository.GetAllStatusDispositions());
            if (dispositions != null)
            {
                dropdowns.StatusDisposition = dispositions
                    .GroupBy(d => d.Name)
                    .Select(g => new CandidateSearchOptionDto
                    {
                        Label = g.Key,
                        Value = g.First().Id.ToString()
                    })
                    .ToList();
            }

            var requisitions = await SafeAsync(() => _requisitionRequestRepository.GetAll());
            if (requisitions != null)
            {
                dropdowns.LineOfBusiness = requisitions
                    .Where(r => !string.IsNullOrWhiteSpace(r.LineOfBusiness))
                    .GroupBy(r => r.LineOfBusiness)
                    .Select(g => new CandidateSearchOptionDto
                    {
                        Label = g.Key,
                        Value = g.Key
                    })
                    .ToList();
            }

            var locations = await SafeAsync(() => _locationRepository.GetAll());
            if (locations != null)
            {
                dropdowns.Location = locations
                    .Select(l => new CandidateSearchOptionDto
                    {
                        Label = l.Name,
                        Value = l.Id.ToString()
                    })
                    .ToList();
            }

            return dropdowns;
        }

        public async Task<CandidateSearchResponseDto> SearchAsync(CandidateSearchRequestDto request)
        {
            request ??= new CandidateSearchRequestDto();

            var filters = new CandidateSearchApplicantFilters
            {
                Dayforce_Id = request.DayforceId ?? string.Empty,
                ApplicantName = request.Name ?? string.Empty,
                EmailAddress = request.Email ?? string.Empty,
                PhoneNumber = request.PhoneNumber ?? string.Empty,
                Disposition = request.StatusDisposition ?? string.Empty,
                LineOfBusiness = request.LineOfBusiness ?? string.Empty,
                HireType = request.HireType ?? string.Empty,
                PendingFollowUpOnly = request.HasPendingFollowUps,
                ShowActive = MapShowProfiles(request.ShowProfiles),
                LocationIds = ResolveLocationIds(request)
            };

            if (int.TryParse(request.Status, out var statusId))
                filters.StatusId = statusId;

            var page = await SafeAsync(() => _applicantRepository.SearchApplicantsByInfo(filters));

            var response = new CandidateSearchResponseDto();

            if (page?.data == null)
                return response;

            response.Results = page.data
                .Select(MapResult)
                .ToList();

            return response;
        }

        private static CandidateSearchResultDto MapResult(CandidateSearchApplicantRowDto row)
        {
            var primaryPhone = row.PhoneNumbers?.FirstOrDefault(p => p.IsPrimary)
                ?? row.PhoneNumbers?.FirstOrDefault();

            return new CandidateSearchResultDto
            {
                ApplicantId = row.Applicant_Id ?? string.Empty,
                Name = row.FullName ?? string.Empty,
                PhoneNumber = primaryPhone?.PhoneNumber ?? string.Empty,
                IsPrimaryPhone = primaryPhone?.IsPrimary ?? false,
                Email = row.EmailAddress ?? string.Empty,
                Language = row.LanguageName ?? string.Empty,
                EmploymentType = row.EmploymentType ?? string.Empty,
                DayforceId = row.Dayforce_Id ?? string.Empty,
                Status = row.CurrentStatus.ToString(),
                // TODO: SearchApplicantByInfo rows do not currently include the applicant's
                // status disposition or line of business; requires joining requisition/status data.
                StatusDisposition = string.Empty,
                LineOfBusiness = string.Empty,
                HireType = string.Empty,
                HasPendingFollowUps = row.PendingFollowUpItemsCount > 0,
                ProfileStatus = row.IsDeleted ? "inactive" : "active",
                RowState = "default"
            };
        }

        private static string MapShowProfiles(string? showProfiles)
        {
            return (showProfiles ?? string.Empty).ToLowerInvariant() switch
            {
                "inactive" => "Inactive",
                "all" => "Default",
                _ => "Active"
            };
        }

        private static IList<int> ResolveLocationIds(CandidateSearchRequestDto request)
        {
            if (request.LocationIds is { Count: > 0 })
                return request.LocationIds;

            if (!string.IsNullOrWhiteSpace(request.LocationId)
                && request.LocationId != "all"
                && int.TryParse(request.LocationId, out var locationId))
            {
                return new List<int> { locationId };
            }

            return new List<int>();
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
