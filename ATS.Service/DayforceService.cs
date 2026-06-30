using ATS.Data.Repository;

namespace ATS.Service
{
    public interface IDayforceService
    {
        // TODO: Define XrefCode CRUD / employee status validation orchestration methods.
    }

    public class DayforceService : IDayforceService
    {
        private readonly IDayforceRepository _dayforceRepository;

        public DayforceService(IDayforceRepository dayforceRepository)
        {
            _dayforceRepository = dayforceRepository;
        }

        // TODO: Orchestrate Dayforce XrefCode CRUD (job, dept, org, onboarding policy) and
        // employee status / field validation using _dayforceRepository.
    }
}
