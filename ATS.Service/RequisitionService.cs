using ATS.Data.Repository;

namespace ATS.Service
{
    public interface IRequisitionService
    {
        // TODO: Define requisition CRUD / detail rows / audit log / weekly view orchestration methods.
    }

    public class RequisitionService : IRequisitionService
    {
        private readonly IRequisitionRequestRepository _requisitionRequestRepository;

        public RequisitionService(IRequisitionRequestRepository requisitionRequestRepository)
        {
            _requisitionRequestRepository = requisitionRequestRepository;
        }

        // TODO: Orchestrate requisition CRUD, detail rows, audit log, weekly view, status lookups,
        // and Excel export using _requisitionRequestRepository.
    }
}
