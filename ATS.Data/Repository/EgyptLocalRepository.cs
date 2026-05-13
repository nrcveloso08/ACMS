using ATS.DTO.Egypt;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IEgyptLocalRepository
    {
        Task<bool> AddOrUpdateAdditionalInfo(EgyptAdditionalInfo egyptAdditionalInfo);
        Task<EgyptAdditionalInfo> GetByApplicantId(Guid applicantId);
    }

    public class EgyptLocalRepository : IEgyptLocalRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public EgyptLocalRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<bool> AddOrUpdateAdditionalInfo(
            EgyptAdditionalInfo egyptAdditionalInfo)
        {
            if (egyptAdditionalInfo == null)
                throw new ArgumentNullException(nameof(egyptAdditionalInfo));

            return await _webServiceHelper.PostAsync<EgyptAdditionalInfo, bool>(
                "/v1/Egypt/AddOrUpdateAdditionalInfo",
                egyptAdditionalInfo
            );
        }

        public async Task<EgyptAdditionalInfo> GetByApplicantId(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<EgyptAdditionalInfo>(
                "/v1/Egypt/GetByApplicantId",
                parameters
            );
        }
    }
}