using ATS.Data.Repository;

namespace ATS.Service
{
    public interface ISkillTestService
    {
        // TODO: Define skill test result retrieval / FurstPerson assessment orchestration methods.
    }

    public class SkillTestService : ISkillTestService
    {
        private readonly IApplicantSkillRepository _applicantSkillRepository;

        public SkillTestService(IApplicantSkillRepository applicantSkillRepository)
        {
            _applicantSkillRepository = applicantSkillRepository;
        }

        // TODO: Orchestrate skill test result CRUD and result sets by location/wave
        // using _applicantSkillRepository.
    }
}
