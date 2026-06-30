using ATS.Data.Repository;

namespace ATS.Service
{
    public interface IInterviewService
    {
        // TODO: Define interview Q&A / questionnaire / disposition orchestration methods.
    }

    public class InterviewService : IInterviewService
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly IInterviewTypeRepository _interviewTypeRepository;

        public InterviewService(
            IInterviewRepository interviewRepository,
            IInterviewTypeRepository interviewTypeRepository)
        {
            _interviewRepository = interviewRepository;
            _interviewTypeRepository = interviewTypeRepository;
        }

        // TODO: Orchestrate interview questionnaire retrieval/save and interview type lookups
        // using _interviewRepository / _interviewTypeRepository. Distinct from InterviewScheduleService,
        // which owns scheduling/appointment slots.
    }
}
