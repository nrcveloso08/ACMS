using ATS.DTO.Applicant;
using ATS.DTO.ObeyaBoard;
using ATS.DTO.RequisitionRequest;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IObeyaBoardWaveRepository
    {
        Task<IList<ObeyaBoardWaveCard>> GetWaveCards(DateTime start, DateTime end, IList<int> locationIds, double minDays = 0, double maxDays = 0);
        Task<bool> UpdateWaveCardStatus(RequisitionRequestStatus requisitionStatus);
        Task<Applicant> AddExisintingProfile(WaveReassignment waveReassignment);
        Task<IList<WaveRoster>> GetWaveRosterById(int requisitionId);
    }

    public class ObeyaBoardWaveRepository : IObeyaBoardWaveRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ObeyaBoardWaveRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public Task<IList<ObeyaBoardWaveCard>> GetWaveCards(DateTime start, DateTime end, IList<int> locationIds, double minDays = 0, double maxDays = 0)
        {
            // TODO: Requires DB/business decision - see ATS_BACKEND_ARCHITECTURE.md
            // Old KITT implementation composes requisition requests (RequisitionRequestRepository.GetByDateRangeAndLocationIDs)
            // with applicant counts (Applicant/GetCountByRequisitionId) and KITT-DB business logic to build the wave cards.
            throw new NotImplementedException("GetWaveCards requires later DB/API decision.");
        }

        public Task<bool> UpdateWaveCardStatus(RequisitionRequestStatus requisitionStatus)
        {
            // TODO: Requires DB/business decision - see ATS_BACKEND_ARCHITECTURE.md
            // Old KITT implementation updates the requisition request status via KITT-DB business logic
            // (IRequisitionRequestService.UpdateStatus) - no direct ATSWebService endpoint call.
            throw new NotImplementedException("UpdateWaveCardStatus requires later DB/API decision.");
        }

        public async Task<Applicant> AddExisintingProfile(WaveReassignment waveReassignment)
        {
            if (waveReassignment == null)
                throw new ArgumentNullException(nameof(waveReassignment));

            return await _webServiceHelper.PostAsync<WaveReassignment, Applicant>(
                "/v1/WaveRoster/AddExisintingProfile",
                waveReassignment
            );
        }

        public async Task<IList<WaveRoster>> GetWaveRosterById(int requisitionId)
        {
            var parameters = new NameValueCollection
            {
                { "id", requisitionId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<WaveRoster>>(
                "/v1/WaveRoster/GetById",
                parameters
            );
        }
    }
}
