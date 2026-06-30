using ATS.Data.Repository;
using ATS.DTO.FollowUp;
using ATS.DTO.Helpers;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace ATS.Service
{
    public interface IFollowUpManagementService
    {
        Task<FollowUpItemsByApplicant> GetFollowUpsForApplicantAsync(Guid applicantId, bool pendingOnly = true);

        Task<IList<FollowUpApplicant>> GetFollowUpsByLocationAsync(int locationId, bool pendingOnly = true);

        Task<IList<FollowUpApplicant>> GetFilteredFollowUpsAsync(FollowUpFilter filter);

        Task<bool> SaveFollowUpAsync(FollowUpApplicant followUpApplicant, string user, string userEmail);

        Task<bool> CompleteFollowUpsAsync(IList<int> ids, string user, string userEmail);

        Task<IList<FollowUpDisposition>> GetDispositionsByLocationAsync(int locationId);

        Task<bool> SaveDispositionAsync(FollowUpDisposition disposition);

        // Read/search API surface (ATS.Api FollowUpController)
        Task<IList<FollowUpStatusDto>> GetFollowUpStatusesAsync();

        Task<IList<FollowUpDispositionDto>> GetDispositionOptionsAsync(int locationId);

        Task<IList<FollowUpDispositionDto>> GetDispositionOptionsAsync(IList<int> locationIds);

        Task<FollowUpSearchResponseDto> SearchFollowUpsAsync(FollowUpSearchRequestDto request);

        Task<FollowUpApplicantResponseDto> GetApplicantFollowUpsAsync(Guid applicantId, bool pendingOnly = true);

        Task<IList<FollowUpResultDto>> GetLocationFollowUpsAsync(int locationId, bool pendingOnly = true);
    }

    /// <summary>
    /// Page/workflow oriented service for the Follow-Up Management screens.
    /// Orchestrates retrieval and persistence of applicant follow-ups and the
    /// follow-up disposition lookups associated with them.
    /// </summary>
    public class FollowUpManagementService : IFollowUpManagementService
    {
        private readonly IFollowUpsRepository _followUpsRepository;
        private readonly ILogger<FollowUpManagementService> _logger;

        public FollowUpManagementService(IFollowUpsRepository followUpsRepository, ILogger<FollowUpManagementService> logger)
        {
            _followUpsRepository = followUpsRepository;
            _logger = logger;
        }

        /// <summary>
        /// Pass-through to retrieve all follow-ups (optionally only pending ones) for a single applicant.
        /// </summary>
        public async Task<FollowUpItemsByApplicant> GetFollowUpsForApplicantAsync(Guid applicantId, bool pendingOnly = true)
        {
            return await _followUpsRepository.GetApplicantFollowUpsByApplicantId(applicantId, pendingOnly);
        }

        /// <summary>
        /// Pass-through to retrieve all follow-ups (optionally only pending ones) for a location.
        /// </summary>
        public async Task<IList<FollowUpApplicant>> GetFollowUpsByLocationAsync(int locationId, bool pendingOnly = true)
        {
            return await _followUpsRepository.GetApplicantFollowUpsByLocation(locationId, pendingOnly);
        }

        /// <summary>
        /// Pass-through to retrieve follow-ups matching the supplied filter (location, line of
        /// business, requisitions, statuses and date range) for the Follow-Up Management grid.
        /// </summary>
        public async Task<IList<FollowUpApplicant>> GetFilteredFollowUpsAsync(FollowUpFilter filter)
        {
            return await _followUpsRepository.GetFilteredData(filter);
        }

        /// <summary>
        /// Pass-through to create or update a follow-up for an applicant.
        /// </summary>
        // TODO (deferred write action): exposed via IFollowUpManagementService for a future
        // POST /api/v1/follow-up endpoint once AddOrUpdateFollowUp is wired into the controller.
        public async Task<bool> SaveFollowUpAsync(FollowUpApplicant followUpApplicant, string user, string userEmail)
        {
            return await _followUpsRepository.AddOrUpdateFollowUp(followUpApplicant, user, userEmail);
        }

        /// <summary>
        /// Pass-through to mark a batch of follow-ups as complete.
        /// </summary>
        // TODO (deferred write action): exposed via IFollowUpManagementService for a future
        // POST /api/v1/follow-up/bulk-complete endpoint once BulkComplete is wired into the controller.
        public async Task<bool> CompleteFollowUpsAsync(IList<int> ids, string user, string userEmail)
        {
            return await _followUpsRepository.BulkComplete(ids, user, userEmail);
        }

        /// <summary>
        /// Pass-through to retrieve the follow-up dispositions configured for a location.
        /// </summary>
        public async Task<IList<FollowUpDisposition>> GetDispositionsByLocationAsync(int locationId)
        {
            return await _followUpsRepository.GetDispositionByLocation(locationId);
        }

        /// <summary>
        /// Pass-through to create or update a follow-up disposition.
        /// </summary>
        public async Task<bool> SaveDispositionAsync(FollowUpDisposition disposition)
        {
            return await _followUpsRepository.AddOrUpdateDisposition(disposition);
        }

        /// <summary>
        /// Returns the fixed set of follow-up statuses. These are defined locally by the
        /// <see cref="FollowUpStatus"/> enum, so no ATSWebService call is required.
        /// </summary>
        public Task<IList<FollowUpStatusDto>> GetFollowUpStatusesAsync()
        {
            IList<FollowUpStatusDto> statuses = Enum.GetValues(typeof(FollowUpStatus))
                .Cast<FollowUpStatus>()
                .Select(s => new FollowUpStatusDto { Value = (int)s, Label = s.GetDescription() })
                .ToList();

            return Task.FromResult(statuses);
        }

        /// <summary>
        /// Returns the follow-up disposition options configured for a single location.
        /// </summary>
        public async Task<IList<FollowUpDispositionDto>> GetDispositionOptionsAsync(int locationId)
        {
            var dispositions = await SafeAsync(() => _followUpsRepository.GetDispositionByLocation(locationId));
            return MapDispositions(dispositions);
        }

        /// <summary>
        /// Returns the follow-up disposition options configured across multiple locations.
        /// </summary>
        public async Task<IList<FollowUpDispositionDto>> GetDispositionOptionsAsync(IList<int> locationIds)
        {
            if (locationIds == null || locationIds.Count == 0)
                return new List<FollowUpDispositionDto>();

            var dispositions = await SafeAsync(() => _followUpsRepository.GetDispositionByLocationIDs(locationIds));
            return MapDispositions(dispositions);
        }

        /// <summary>
        /// Searches follow-ups across one or more locations, applying the additional
        /// applicant/disposition/assignee/keyword filters and pagination requested by the
        /// Follow-Up Management grid.
        /// </summary>
        public async Task<FollowUpSearchResponseDto> SearchFollowUpsAsync(FollowUpSearchRequestDto request)
        {
            var response = new FollowUpSearchResponseDto
            {
                Page = request.Page < 1 ? 1 : request.Page,
                PageSize = request.PageSize < 1 ? 25 : Math.Min(request.PageSize, 100)
            };

            var locationIds = ResolveLocationIds(request);
            if (locationIds.Count == 0)
                return response;

            var items = new List<FollowUpApplicant>();
            foreach (var locationId in locationIds)
            {
                var filter = new FollowUpFilter
                {
                    LocationId = locationId,
                    LineOfBusinessIds = new List<int>(),
                    RequisitionIds = new List<int>(),
                    Statuses = request.Status.HasValue ? new List<FollowUpStatus> { request.Status.Value } : new List<FollowUpStatus>(),
                    StartDate = request.DateFrom ?? DateTime.MinValue,
                    EndDate = request.DateTo ?? DateTime.MaxValue
                };

                var locationItems = await SafeAsync(() => _followUpsRepository.GetFilteredData(filter));
                if (locationItems != null)
                    items.AddRange(locationItems);
            }

            IEnumerable<FollowUpApplicant> filtered = items;

            if (request.ApplicantId.HasValue && request.ApplicantId.Value != Guid.Empty)
                filtered = filtered.Where(i => i.Applicant_Id == request.ApplicantId.Value);

            if (request.DispositionId.HasValue)
                filtered = filtered.Where(i => i.FollowUpDisposition_Id == request.DispositionId.Value);

            if (!string.IsNullOrWhiteSpace(request.AssignedTo))
                filtered = filtered.Where(i => string.Equals(i.RequestedBy, request.AssignedTo, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                var keyword = request.Keyword.Trim();
                filtered = filtered.Where(i =>
                    (i.Applicant?.FullName?.Contains(keyword, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (i.Notes?.Contains(keyword, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (i.FollowUpDispositionText?.Contains(keyword, StringComparison.OrdinalIgnoreCase) ?? false));
            }

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
        /// Returns the follow-ups for a single applicant, along with the applicant's contact info.
        /// </summary>
        public async Task<FollowUpApplicantResponseDto> GetApplicantFollowUpsAsync(Guid applicantId, bool pendingOnly = true)
        {
            var response = new FollowUpApplicantResponseDto { ApplicantId = applicantId };

            var result = await SafeAsync(() => _followUpsRepository.GetApplicantFollowUpsByApplicantId(applicantId, pendingOnly));
            if (result == null)
                return response;

            if (result.Applicant != null)
            {
                response.ApplicantName = result.Applicant.FullName?.Trim() ?? string.Empty;
                response.PhoneNumber = result.Applicant.PhoneNumber ?? string.Empty;
                response.Email = result.Applicant.EmailAddress ?? string.Empty;
            }

            response.FollowUps = (result.FollowUpItems ?? new List<FollowUpApplicant>())
                .Select(MapResult)
                .ToList();

            return response;
        }

        /// <summary>
        /// Returns the follow-ups for a location.
        /// </summary>
        public async Task<IList<FollowUpResultDto>> GetLocationFollowUpsAsync(int locationId, bool pendingOnly = true)
        {
            var items = await SafeAsync(() => _followUpsRepository.GetApplicantFollowUpsByLocation(locationId, pendingOnly));
            return (items ?? new List<FollowUpApplicant>())
                .Select(MapResult)
                .ToList();
        }

        private static IList<FollowUpDispositionDto> MapDispositions(IList<FollowUpDisposition>? dispositions)
        {
            if (dispositions == null)
                return new List<FollowUpDispositionDto>();

            return dispositions
                .Where(d => !d.IsDeleted)
                .Select(d => new FollowUpDispositionDto
                {
                    Id = d.Id,
                    LocationId = d.Location_Id,
                    Name = d.Name ?? string.Empty
                })
                .ToList();
        }

        private static FollowUpResultDto MapResult(FollowUpApplicant item)
        {
            return new FollowUpResultDto
            {
                FollowUpId = item.Id,
                ApplicantId = item.Applicant_Id,
                ApplicantName = item.Applicant?.FullName?.Trim() ?? string.Empty,
                // TODO: location name is not exposed on FollowUpApplicant by ATSWebService; requires
                // joining against ILocationRepository if the grid needs to display it.
                Location = string.Empty,
                PhoneNumber = item.Applicant?.PhoneNumber ?? string.Empty,
                Email = item.Applicant?.EmailAddress ?? string.Empty,
                Status = item.StatusDisplay,
                Disposition = item.FollowUpDispositionText ?? string.Empty,
                FollowUpDate = item.DueDate,
                AssignedTo = item.RequestedBy ?? string.Empty,
                Notes = item.Notes ?? string.Empty,
                // TODO: created date is not exposed on FollowUpApplicant by ATSWebService.
                CreatedDate = null,
                UpdatedDate = item.CompletedOn
            };
        }

        private static IList<int> ResolveLocationIds(FollowUpSearchRequestDto request)
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
