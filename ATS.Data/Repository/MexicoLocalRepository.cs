using ATS.DTO.Mexico;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IMexicoLocalRepository
    {
        Task<bool> AddOrUpdateAdditionalInfo(
            MexicoAdditionalInfo mexicoAdditionalInfo);

        Task<MexicoAdditionalInfo> GetAdditionalInfoByApplicantId(
            Guid applicantId);
    }

    public class MexicoLocalRepository : IMexicoLocalRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public MexicoLocalRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<bool> AddOrUpdateAdditionalInfo(
            MexicoAdditionalInfo mexicoAdditionalInfo)
        {
            if (mexicoAdditionalInfo == null)
                throw new ArgumentNullException(nameof(mexicoAdditionalInfo));

            return await _webServiceHelper.PostAsync
                <MexicoAdditionalInfo, bool>(
                    "/v1/Mexico/AddOrUpdateAdditionalInfo",
                    mexicoAdditionalInfo
                );
        }

        public async Task<MexicoAdditionalInfo> GetAdditionalInfoByApplicantId(
            Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<MexicoAdditionalInfo>(
                "/v1/Mexico/GetByApplicantId",
                parameters
            );
        }
    }
}
