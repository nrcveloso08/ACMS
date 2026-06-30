using ATS.DTO.Email;
using ATS.Service.WebServiceHelper;

namespace ATS.Data.Repository
{
    public interface IStatusActionMapRepository
    {
        Task<IList<StatusActionMap>> GetMap(int statusId);
        Task<int> SetMap(int statusId, List<int> templateIds);
    }

    public class StatusActionMapRepository : IStatusActionMapRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public StatusActionMapRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public Task<IList<StatusActionMap>> GetMap(int statusId)
        {
            // TODO: Requires DB/business decision - see ATS_BACKEND_ARCHITECTURE.md
            throw new NotImplementedException("GetMap requires later DB/API decision.");
        }

        public Task<int> SetMap(int statusId, List<int> templateIds)
        {
            // TODO: Requires DB/business decision - see ATS_BACKEND_ARCHITECTURE.md
            throw new NotImplementedException("SetMap requires later DB/API decision.");
        }
    }
}
