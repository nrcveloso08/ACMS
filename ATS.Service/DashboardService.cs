using ATS.DTO.Dashboard;

namespace ATS.Service
{
    public interface IDashboardService
    {
        Task<DashboardSummary> GetSummaryAsync();
    }

    public class DashboardService : IDashboardService
    {
        public Task<DashboardSummary> GetSummaryAsync()
        {
            // TODO: wire up real counts/widgets from repositories in a later iteration.
            return Task.FromResult(new DashboardSummary());
        }
    }
}
