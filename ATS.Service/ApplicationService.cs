using ATS.Data.Repository;

namespace ATS.Service
{
    public interface IApplicationService
    {
        // TODO: Define application CRUD / status tracking / prescreen response / appointment orchestration methods.
    }

    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        // TODO: Orchestrate application CRUD, status tracking, prescreen responses,
        // and appointment management using _applicationRepository.
    }
}
