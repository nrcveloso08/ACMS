using ATS.Data.Repository;
using ATS.DTO;
using ATS.DTO.Helpers;
using ATS.DTO.Medical;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace ATS.Service
{
    public interface IMedicalService
    {
        Task<MedicalResultDropdownsDto> GetDropdownsAsync();

        Task<MedicalResultSearchResponseDto> SearchMedicalResultsAsync(MedicalResultSearchRequestDto request);

        Task<MedicalResultApplicantDto> GetApplicantMedicalResultAsync(Guid applicantId);

        Task<IList<MedicalResultDto>> GetLocationMedicalResultsAsync(int locationId);

        // TODO (deferred write action): exposed for a future POST /api/v1/medical/results endpoint
        // once AddOrUpdate is wired into the controller (requires CreatedByUser/CreatedByEmail from
        // the authenticated user context).
        Task<bool> SaveMedicalResultAsync(ATS.DTO.Interview.MedicalResult medicalResult);
    }

    /// <summary>
    /// Page/workflow oriented service for the Medical Results screens.
    /// Orchestrates retrieval of applicant medical results and the status
    /// lookups associated with them.
    /// </summary>
    public class MedicalService : IMedicalService
    {
        private static readonly IList<InterviewStatus> RelevantStatuses = new List<InterviewStatus>
        {
            InterviewStatus.Awaiting_Medical,
            InterviewStatus.Passed_Medical,
            InterviewStatus.Failed_Medical
        };

        private readonly IMedicalResultRepository _medicalResultRepository;
        private readonly ILogger<MedicalService> _logger;

        public MedicalService(IMedicalResultRepository medicalResultRepository, ILogger<MedicalService> logger)
        {
            _medicalResultRepository = medicalResultRepository;
            _logger = logger;
        }

        /// <summary>
        /// Returns the medical status options used by the Medical Results page filters.
        /// These are defined locally by the <see cref="InterviewStatus"/> enum, so no
        /// ATSWebService call is required.
        /// </summary>
        public Task<MedicalResultDropdownsDto> GetDropdownsAsync()
        {
            var dto = new MedicalResultDropdownsDto
            {
                Statuses = RelevantStatuses
                    .Select(s => new MedicalResultOptionDto { Value = (int)s, Label = s.GetDescription() })
                    .ToList()
            };

            return Task.FromResult(dto);
        }

        /// <summary>
        /// Searches medical results across one or more locations, applying the additional
        /// applicant/status/date filters and pagination requested by the Medical Results grid.
        /// </summary>
        public async Task<MedicalResultSearchResponseDto> SearchMedicalResultsAsync(MedicalResultSearchRequestDto request)
        {
            var response = new MedicalResultSearchResponseDto
            {
                Page = request.Page < 1 ? 1 : request.Page,
                PageSize = request.PageSize < 1 ? 25 : Math.Min(request.PageSize, 100)
            };

            var locationIds = ResolveLocationIds(request);
            if (locationIds.Count == 0)
                return response;

            var filter = new ATS.DTO.Interview.MedicalFilters
            {
                LocationIds = locationIds,
                StartDate = request.DateFrom,
                EndDate = request.DateTo,
                StatusId = request.StatusId ?? 0
            };

            var items = await SafeAsync(() => _medicalResultRepository.GetFilteredMedicalResult(filter));
            IEnumerable<ATS.DTO.Interview.MedicalResult> filtered = items ?? new List<ATS.DTO.Interview.MedicalResult>();

            if (request.ApplicantId.HasValue && request.ApplicantId.Value != Guid.Empty)
                filtered = filtered.Where(i => i.ApplicantId == request.ApplicantId.Value);

            // TODO: ApplicantName filtering is not applied because MedicalResult does not expose
            // applicant name from ATSWebService; requires joining against applicant data.

            var filteredList = filtered.ToList();
            response.TotalCount = filteredList.Count;
            response.Items = filteredList
                .Skip((response.Page - 1) * response.PageSize)
                .Take(response.PageSize)
                .Select(MapResult)
                .ToList();

            return response;
        }

        /// <summary>
        /// Returns the medical result for a single applicant, if one has been recorded.
        /// </summary>
        public async Task<MedicalResultApplicantDto> GetApplicantMedicalResultAsync(Guid applicantId)
        {
            var result = await SafeAsync(() => _medicalResultRepository.GetByApplicantId(applicantId));

            if (result == null)
                return new MedicalResultApplicantDto { ApplicantId = applicantId, HasResult = false };

            return new MedicalResultApplicantDto
            {
                ApplicantId = result.ApplicantId == Guid.Empty ? applicantId : result.ApplicantId,
                HasResult = true,
                MedicalStatus = result.Passed ? "Passed" : "Failed",
                ResultDate = result.AppointmentDate?.DateTime,
                Notes = result.Notes ?? string.Empty,
                CreatedDate = result.Created == default ? null : result.Created.DateTime
            };
        }

        /// <summary>
        /// Returns the medical results recorded for a location.
        /// </summary>
        public async Task<IList<MedicalResultDto>> GetLocationMedicalResultsAsync(int locationId)
        {
            var filter = new ATS.DTO.Interview.MedicalFilters
            {
                LocationIds = new List<int> { locationId }
            };

            var items = await SafeAsync(() => _medicalResultRepository.GetFilteredMedicalResult(filter));
            return (items ?? new List<ATS.DTO.Interview.MedicalResult>())
                .Select(MapResult)
                .ToList();
        }

        /// <summary>
        /// Pass-through to create or update a medical result for an applicant.
        /// </summary>
        // TODO (deferred write action): exposed via IMedicalService for a future
        // POST /api/v1/medical/results endpoint once AddOrUpdate is wired into the controller.
        public async Task<bool> SaveMedicalResultAsync(ATS.DTO.Interview.MedicalResult medicalResult)
        {
            return await _medicalResultRepository.AddOrUpdate(medicalResult);
        }

        private static MedicalResultDto MapResult(ATS.DTO.Interview.MedicalResult item)
        {
            return new MedicalResultDto
            {
                ApplicantId = item.ApplicantId,
                // TODO: applicant name is not exposed on MedicalResult by ATSWebService; requires
                // joining against applicant data if the grid needs to display it.
                ApplicantName = string.Empty,
                LocationId = item.LocationId,
                // TODO: location name is not exposed on MedicalResult by ATSWebService; requires
                // joining against ILocationRepository if the grid needs to display it.
                Location = string.Empty,
                // TODO: phone number and email are not exposed on MedicalResult by ATSWebService.
                PhoneNumber = string.Empty,
                Email = string.Empty,
                MedicalStatus = item.Passed ? "Passed" : "Failed",
                ResultDate = item.AppointmentDate?.DateTime,
                Notes = item.Notes ?? string.Empty,
                CreatedDate = item.Created == default ? null : item.Created.DateTime,
                // TODO: updated date is not exposed on MedicalResult by ATSWebService.
                UpdatedDate = null
            };
        }

        private static IList<int> ResolveLocationIds(MedicalResultSearchRequestDto request)
        {
            if (request.LocationIds is { Count: > 0 })
                return request.LocationIds.Where(id => id > 0).ToList();

            if (request.LocationId.HasValue && request.LocationId.Value > 0)
                return new List<int> { request.LocationId.Value };

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
