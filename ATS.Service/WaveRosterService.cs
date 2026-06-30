using ATS.Data.Repository;
using ATS.DTO.Applicant;
using ATS.DTO.ObeyaBoard;
using ATS.DTO.Roster;

namespace ATS.Service
{
    public interface IWaveRosterService
    {
        Task<IList<WaveRoster>> GetWaveRosterAsync(int requisitionId);
        Task<Applicant> AddExistingProfileToWaveAsync(WaveReassignment waveReassignment);
        Task<List<RosterReconciliation>> GetRosterReconciliationsAsync(string status);
        Task<bool> UpdateRosterReconciliationAsync(RosterReconciliation reconciliation);
        Task<List<RosterReconciliationLogs>> AddReconciliationLogAsync(string rosterReconciliationId, string userName, string notes);
        Task<WaveRosterSummary> GetWaveRosterSummaryAsync(int requisitionId);
    }

    public class WaveRosterSummary
    {
        public IList<WaveRoster> Roster { get; set; } = new List<WaveRoster>();
        public List<RosterReconciliation> Reconciliations { get; set; } = new List<RosterReconciliation>();
    }

    public class WaveRosterService : IWaveRosterService
    {
        private readonly IObeyaBoardWaveRepository _obeyaBoardWaveRepository;
        private readonly IRosterReconciliationRepository _rosterReconciliationRepository;

        public WaveRosterService(
            IObeyaBoardWaveRepository obeyaBoardWaveRepository,
            IRosterReconciliationRepository rosterReconciliationRepository)
        {
            _obeyaBoardWaveRepository = obeyaBoardWaveRepository;
            _rosterReconciliationRepository = rosterReconciliationRepository;
        }

        public async Task<IList<WaveRoster>> GetWaveRosterAsync(int requisitionId)
        {
            return await _obeyaBoardWaveRepository.GetWaveRosterById(requisitionId);
        }

        public async Task<Applicant> AddExistingProfileToWaveAsync(WaveReassignment waveReassignment)
        {
            return await _obeyaBoardWaveRepository.AddExisintingProfile(waveReassignment);
        }

        public async Task<List<RosterReconciliation>> GetRosterReconciliationsAsync(string status)
        {
            return await _rosterReconciliationRepository.GetRosterReconciliations(status);
        }

        public async Task<bool> UpdateRosterReconciliationAsync(RosterReconciliation reconciliation)
        {
            return await _rosterReconciliationRepository.Update(reconciliation);
        }

        public async Task<List<RosterReconciliationLogs>> AddReconciliationLogAsync(string rosterReconciliationId, string userName, string notes)
        {
            return await _rosterReconciliationRepository.AddLogs(rosterReconciliationId, userName, notes);
        }

        public async Task<WaveRosterSummary> GetWaveRosterSummaryAsync(int requisitionId)
        {
            // TODO: Old KITT logic correlates the wave roster (per-requisition applicant list) with
            // open roster reconciliation issues for the same requisition, joining on RequisitionId/ApplicantId.
            // GetRosterReconciliations only takes a status filter and is not requisition-scoped server-side,
            // so the filtering/joining logic needs to be ported once the reconciliation endpoint
            // supports requisition-based filtering (or once business rules for the join are confirmed).
            var roster = await _obeyaBoardWaveRepository.GetWaveRosterById(requisitionId);

            return new WaveRosterSummary
            {
                Roster = roster ?? new List<WaveRoster>(),
                Reconciliations = new List<RosterReconciliation>()
            };
        }
    }
}
