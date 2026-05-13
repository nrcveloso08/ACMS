using ATS.DTO.Honduras;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IHondurasLocalRepository
    {
        Task<bool> AddOrUpdateAdditionalInfo(
            HondurasAdditionalInfo hondurasAdditionalInfo);

        Task<HondurasAdditionalInfo> GetByApplicantId(Guid applicantId);
    }

    public class HondurasLocalRepository : IHondurasLocalRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public HondurasLocalRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<bool> AddOrUpdateAdditionalInfo(
            HondurasAdditionalInfo hondurasAdditionalInfo)
        {
            if (hondurasAdditionalInfo == null)
                throw new ArgumentNullException(nameof(hondurasAdditionalInfo));

            return await _webServiceHelper.PostAsync
                <HondurasAdditionalInfo, bool>(
                    "/v1/Honduras/AddOrUpdateAdditionalInfo",
                    hondurasAdditionalInfo
                );
        }

        public async Task<HondurasAdditionalInfo> GetByApplicantId(
            Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<HondurasAdditionalInfo>(
                "/v1/Honduras/GetByApplicantId",
                parameters
            );
        }
    }
}