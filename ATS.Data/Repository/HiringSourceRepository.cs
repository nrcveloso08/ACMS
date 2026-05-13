using ATS.DTO.HiringSource;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IHiringSourceRepository
    {
        Task<IList<HiringSourceLocation>> GetByLocationId(int locationId);
        Task<IList<HiringSourceLocation>> GetByLocationIds(IList<int> locationIds);
        Task<HiringSourceLocation> Add(HiringSourceLocation hiringSourceLocation);
    }

    public class HiringSourceRepository : IHiringSourceRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public HiringSourceRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<HiringSourceLocation>> GetByLocationId(int locationId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "locationId", locationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<HiringSourceLocation>>(
                "/v1/Contractor/GetByLocationId",
                parameters
            );
        }

        public async Task<IList<HiringSourceLocation>> GetByLocationIds(IList<int> locationIds)
        {
            if (locationIds == null)
                throw new ArgumentNullException(nameof(locationIds));

            return await _webServiceHelper.PostAsync<IList<int>, IList<HiringSourceLocation>>(
                "/v1/Contractor/GetByLocationIds",
                locationIds
            );
        }

        public async Task<HiringSourceLocation> Add(HiringSourceLocation hiringSourceLocation)
        {
            if (hiringSourceLocation == null)
                throw new ArgumentNullException(nameof(hiringSourceLocation));

            return await _webServiceHelper.PostAsync<HiringSourceLocation, HiringSourceLocation>(
                "/v1/Contractor/Add",
                hiringSourceLocation
            );
        }
    }
}