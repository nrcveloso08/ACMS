using ATS.Data.Repository;
using ATS.DTO.ObeyaBoard;
using ATS.DTO.RequisitionRequest;

namespace ATS.Service
{
    public interface IObeyaBoardService
    {
        Task<IList<ObeyaBoardWaveCard>> GetWaveCardsAsync(DateTime start, DateTime end, IList<int> locationIds, double minDays = 0, double maxDays = 0);
        Task<bool> UpdateWaveCardStatusAsync(RequisitionRequestStatus requisitionStatus);
        Task<RequisitionWaveNote> GetWaveNotesAsync(IList<int> requisitionIds);
        Task<RequisitionRequestDetail> GetRequisitionDetailsAsync(IList<int> requisitionIds);
    }

    public class ObeyaBoardService : IObeyaBoardService
    {
        private readonly IObeyaBoardWaveRepository _obeyaBoardWaveRepository;
        private readonly IRequisitionRequestRepository _requisitionRequestRepository;

        public ObeyaBoardService(
            IObeyaBoardWaveRepository obeyaBoardWaveRepository,
            IRequisitionRequestRepository requisitionRequestRepository)
        {
            _obeyaBoardWaveRepository = obeyaBoardWaveRepository;
            _requisitionRequestRepository = requisitionRequestRepository;
        }

        public async Task<IList<ObeyaBoardWaveCard>> GetWaveCardsAsync(DateTime start, DateTime end, IList<int> locationIds, double minDays = 0, double maxDays = 0)
        {
            // TODO: ObeyaBoardWaveRepository.GetWaveCards is itself a NotImplementedException skeleton pending
            // a DB/API decision (see ObeyaBoardWaveRepository.cs). Propagate until that repository method
            // is implemented; once available this can remain a direct delegation or add wave-card
            // aggregation/sorting logic ported from old KITT (ObeyaBoardController/WaveCardService).
            return await _obeyaBoardWaveRepository.GetWaveCards(start, end, locationIds, minDays, maxDays);
        }

        public async Task<bool> UpdateWaveCardStatusAsync(RequisitionRequestStatus requisitionStatus)
        {
            // TODO: ObeyaBoardWaveRepository.UpdateWaveCardStatus is itself a NotImplementedException skeleton
            // pending a DB/business decision (old KITT used IRequisitionRequestService.UpdateStatus directly
            // against KITT-DB). Propagate until that repository method is implemented.
            return await _obeyaBoardWaveRepository.UpdateWaveCardStatus(requisitionStatus);
        }

        public async Task<RequisitionWaveNote> GetWaveNotesAsync(IList<int> requisitionIds)
        {
            return await _requisitionRequestRepository.GetWaveNotesByRequisitionId(requisitionIds);
        }

        public async Task<RequisitionRequestDetail> GetRequisitionDetailsAsync(IList<int> requisitionIds)
        {
            return await _requisitionRequestRepository.GetDetailsByRequisitionId(requisitionIds);
        }
    }
}
