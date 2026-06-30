using ATS.Data.Repository;

namespace ATS.Service
{
    public interface IFrontService
    {
        // TODO: Define Front inbox / conversation thread / message detail orchestration methods.
    }

    public class FrontService : IFrontService
    {
        private readonly IFrontApiRepository _frontApiRepository;

        public FrontService(IFrontApiRepository frontApiRepository)
        {
            _frontApiRepository = frontApiRepository;
        }

        // TODO: Orchestrate Front inbox messages, conversation threads, and message details
        // by applicant email using _frontApiRepository.
    }
}
