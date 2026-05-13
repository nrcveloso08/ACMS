using ATS.DTO.Bogota;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IBogotaLocalRepository
    {
        Task<BogotaLocalRequirement> AddOrUpdateLocalRequirement(BogotaLocalRequirement bogotaLocalRequirement);
        Task<BogotaLocalRequirement> GetLocalRequirementByApplicantId(Guid applicantId);
    }

    public class BogotaLocalRepository : IBogotaLocalRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public BogotaLocalRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<BogotaLocalRequirement> AddOrUpdateLocalRequirement(
            BogotaLocalRequirement bogotaLocalRequirement)
        {
            if (bogotaLocalRequirement == null)
                throw new ArgumentNullException(nameof(bogotaLocalRequirement));

            return await _webServiceHelper.PostAsync<BogotaLocalRequirement, BogotaLocalRequirement>(
                "/v1/Bogota/AddOrUpdateLocalRequirement",
                bogotaLocalRequirement
            );
        }

        public async Task<BogotaLocalRequirement> GetLocalRequirementByApplicantId(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<BogotaLocalRequirement>(
                "/v1/Bogota/GetLocalRequirementByApplicantId",
                parameters
            );
        }
    }
}