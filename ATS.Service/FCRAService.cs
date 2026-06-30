using ATS.Data.Repository;

namespace ATS.Service
{
    public interface IFCRAService
    {
        // TODO: Define FCRA record CRUD / status / notes / document attachment / report orchestration methods.
    }

    public class FCRAService : IFCRAService
    {
        private readonly IFCRARepository _fcraRepository;
        private readonly IFCRADocumentRepository _fcraDocumentRepository;

        public FCRAService(
            IFCRARepository fcraRepository,
            IFCRADocumentRepository fcraDocumentRepository)
        {
            _fcraRepository = fcraRepository;
            _fcraDocumentRepository = fcraDocumentRepository;
        }

        // TODO: Orchestrate FCRA record CRUD, status/notes updates, document attachments,
        // and Excel report generation using _fcraRepository / _fcraDocumentRepository.
    }
}
