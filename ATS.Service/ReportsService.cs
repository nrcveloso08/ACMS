using ATS.Data.Repository;

namespace ATS.Service
{
    public interface IReportsService
    {
        // TODO: Define ad response rate / prescreener hires / FurstPerson results / global pipeline report orchestration methods.
    }

    public class ReportsService : IReportsService
    {
        private readonly IReportingRepository _reportingRepository;
        private readonly IFurstPersonRepository _furstPersonRepository;

        public ReportsService(
            IReportingRepository reportingRepository,
            IFurstPersonRepository furstPersonRepository)
        {
            _reportingRepository = reportingRepository;
            _furstPersonRepository = furstPersonRepository;
        }

        // TODO: Orchestrate ad response rates, prescreener hires, FurstPerson results,
        // global pipeline reports, and Excel exports using _reportingRepository / _furstPersonRepository.
    }
}
