using ATS.DTO.Hyderabad;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IHyderabadLocalRepository
    {
        Task<HyderabadAdditionalInfo> AddOrUpdateAdditionalInfo(
            HyderabadAdditionalInfo hyberadadAdditionalInfo);

        Task<HyderabadAdditionalInfo> GetLocalAdditionalInfo(Guid applicantId);
    }

    public class HyderabadLocalRepository : IHyderabadLocalRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public HyderabadLocalRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<HyderabadAdditionalInfo> AddOrUpdateAdditionalInfo(
            HyderabadAdditionalInfo hyberadadAdditionalInfo)
        {
            if (hyberadadAdditionalInfo == null)
                throw new ArgumentNullException(nameof(hyberadadAdditionalInfo));

            return await _webServiceHelper.PostAsync
                <HyderabadAdditionalInfo, HyderabadAdditionalInfo>(
                    "/v1/HyderabadLocal/AddOrUpdateAdditionalInfo",
                    hyberadadAdditionalInfo
                );
        }

        public async Task<HyderabadAdditionalInfo> GetLocalAdditionalInfo(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<HyderabadAdditionalInfo>(
                "/v1/HyderabadLocal/GetLocalAdditionalInfo",
                parameters
            );
        }
    }
}
