using ATS.DTO.Manila;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IManilaLocalRepository
    {
        Task<bool> AddOrUpdateAdditionalInfo(
            ManilaAdditionalInfo manilaAdditionalInfo);

        Task<ManilaAdditionalInfo> GetAdditionalInfoByApplicantId(
            Guid applicantId);
    }

    public class ManilaLocalRepository : IManilaLocalRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ManilaLocalRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<bool> AddOrUpdateAdditionalInfo(
            ManilaAdditionalInfo manilaAdditionalInfo)
        {
            if (manilaAdditionalInfo == null)
                throw new ArgumentNullException(nameof(manilaAdditionalInfo));

            return await _webServiceHelper.PostAsync
                <ManilaAdditionalInfo, bool>(
                    "/v1/Manila/AddOrUpdateAdditionalInfo",
                    manilaAdditionalInfo
                );
        }

        public async Task<ManilaAdditionalInfo> GetAdditionalInfoByApplicantId(
            Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<ManilaAdditionalInfo>(
                "/v1/Manila/GetAdditionalInfoByApplicantId",
                parameters
            );
        }
    }
}
