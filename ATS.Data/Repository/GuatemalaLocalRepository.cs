using ATS.DTO.EmploymentApplication;
using ATS.DTO.Guatemala;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IGuatemalaLocalRepository
    {
        Task<GuatemalaLocalRequirement> AddOrUpdateLocalRequirement(
            GuatemalaLocalRequirement guatemalaLocalRequirement);

        Task<GuatemalaLocalRequirement> GetLocalRequirementByApplicantId(
            Guid applicantId);

        Task<GuatemalaAdditionalName> AddOrUpdateLocalNames(
            GuatemalaAdditionalName guatemalaAdditionalName);

        Task<GuatemalaAdditionalName> GetLocalNamesByApplicantId(
            Guid applicantId);

        Task<bool> AddOrUpdateAdditionalInfo(
            GuatemalaAdditionalInfo guatemalaAdditionalInfo);

        Task<GuatemalaAdditionalInfo> GetAdditionalInfoByApplicantId(
            Guid applicantId);
    }

    public class GuatemalaLocalRepository : IGuatemalaLocalRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public GuatemalaLocalRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<GuatemalaLocalRequirement> AddOrUpdateLocalRequirement(
            GuatemalaLocalRequirement guatemalaLocalRequirement)
        {
            if (guatemalaLocalRequirement == null)
                throw new ArgumentNullException(nameof(guatemalaLocalRequirement));

            return await _webServiceHelper.PostAsync
                <GuatemalaLocalRequirement, GuatemalaLocalRequirement>(
                    "/v1/Guatemala/AddOrUpdateLocalRequirement",
                    guatemalaLocalRequirement
                );
        }

        public async Task<GuatemalaLocalRequirement> GetLocalRequirementByApplicantId(
            Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<GuatemalaLocalRequirement>(
                "/v1/Guatemala/GetLocalRequirementByApplicantId",
                parameters
            );
        }

        public async Task<GuatemalaAdditionalName> AddOrUpdateLocalNames(
            GuatemalaAdditionalName guatemalaAdditionalName)
        {
            if (guatemalaAdditionalName == null)
                throw new ArgumentNullException(nameof(guatemalaAdditionalName));

            return await _webServiceHelper.PostAsync
                <GuatemalaAdditionalName, GuatemalaAdditionalName>(
                    "/v1/Guatemala/AddOrUpdateLocalNames",
                    guatemalaAdditionalName
                );
        }

        public async Task<GuatemalaAdditionalName> GetLocalNamesByApplicantId(
            Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<GuatemalaAdditionalName>(
                "/v1/Guatemala/GetLocalNamesByApplicantId",
                parameters
            );
        }

        public async Task<bool> AddOrUpdateAdditionalInfo(
            GuatemalaAdditionalInfo guatemalaAdditionalInfo)
        {
            if (guatemalaAdditionalInfo == null)
                throw new ArgumentNullException(nameof(guatemalaAdditionalInfo));

            return await _webServiceHelper.PostAsync
                <GuatemalaAdditionalInfo, bool>(
                    "/v1/Guatemala/AddOrUpdateAdditionalInfo",
                    guatemalaAdditionalInfo
                );
        }

        public async Task<GuatemalaAdditionalInfo> GetAdditionalInfoByApplicantId(
            Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<GuatemalaAdditionalInfo>(
                "/v1/Guatemala/GetAdditionalInfoByApplicantId",
                parameters
            );
        }
    }
}