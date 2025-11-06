using ATS.DTO.RequisitionRequest;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IRequisitionRequestService
    {
        Task<IEnumerable<Requisition>> GetAll();
        Task<Requisition> Get(Guid id);
        Task<Requisition> GetByDateRangeAndLocationIDs(DateTime startDate, DateTime endDate, IList<int> locationIds);
        Task<Requisition> GetByStartDateAndLocationIDs(DateTime startDate, IList<int> locationIds);
        Task<RequisitionWaveNote> GetWaveNotesByRequisitionId(IList<int> requisitionIds);
        Task<RequisitionRequestDetail> GetDetailsByRequisitionId(IList<int> requisitionIds);
    }

    public class RequisitionRequestRepository : IRequisitionRequestService
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public RequisitionRequestRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IEnumerable<Requisition>> GetAll()
        {
            return await _webServiceHelper.GetAsync<IEnumerable<Requisition>>("/v1/Requisition/GetAll");
        }

        public async Task<Requisition> Get(Guid id)
        {
            NameValueCollection parameters = new NameValueCollection {
                        {"id", id.ToString() }
                    };
            return await _webServiceHelper.GetAsync<Requisition>("/v1/Requisition/Get", parameters);
        }

        public async Task<Requisition> GetByDateRangeAndLocationIDs(DateTime startDate, DateTime endDate, IList<int> locationIds)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "startDate", startDate.ToString("yyyy-MM-dd") },
                { "endDate", endDate.ToString("yyyy-MM-dd") },
                { "locationIds", string.Join(",", locationIds) }
            };

            return await _webServiceHelper.GetAsync<Requisition>("/v1/Requisition/GetByDateRangeAndLocationIDs", parameters);
        }

        public async Task<Requisition> GetByStartDateAndLocationIDs(DateTime startDate, IList<int> locationIds)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "startDate", startDate.ToString("yyyy-MM-dd") },
                { "locationIds", string.Join(",", locationIds) }
            };

            return await _webServiceHelper.GetAsync<Requisition>("/v1/Requisition/GetByDateRangeAndLocationIDs", parameters);
        }

        public async Task<RequisitionWaveNote> GetWaveNotesByRequisitionId(IList<int> requisitionIds)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "requisitionIds",string.Join(",", requisitionIds) }
            };

            return await _webServiceHelper.GetAsync<RequisitionWaveNote>("/v1/Requisition/GetWaveNotesByRequisitionId", parameters);
        }

        public async Task<RequisitionRequestDetail> GetDetailsByRequisitionId(IList<int> requisitionIds)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "requisitionIds",string.Join(",", requisitionIds) }
            };

            return await _webServiceHelper.GetAsync<RequisitionRequestDetail>("/v1/Requisition/GetDetailsByRequisitionId", parameters);
        }


    }
}
