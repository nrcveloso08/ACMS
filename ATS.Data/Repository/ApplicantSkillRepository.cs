using ATS.DTO.Skills;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IApplicantSkillRepository
    {
        Task<IList<Skill>> GetSkillsByLocationAndLOB(int locationId, int lineOfBusinessId);
        Task<IList<SkillTestResult>> GetSkillTestResults(Guid applicantId, bool includeExpired = false);
        Task<IList<SkillTestResult>> GetSkillTestResultsByLocationAndByLineOfBusiness(Guid applicantId, int locationId, int lineOfBusinessId);
        Task<IList<SkillTestResult>> GetSkillTestResultByLocationIdByRequisition(Guid applicantId, int locationId, int requisitionId, int lineOfBusinessId);
        Task<int> SaveTestResults(IList<SkillTestResult> results);
        Task<int> SaveTestResults(IList<SkillTestResult> results, string notes);
        Task<FurstPersonResultSet> GetFurstPersonResultSetById(int id);
    }

    public class ApplicantSkillRepository : IApplicantSkillRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ApplicantSkillRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<Skill>> GetSkillsByLocationAndLOB(int locationId, int lineOfBusinessId)
        {
            var parameters = new NameValueCollection
            {
                { "locationId", locationId.ToString() },
                { "lineOfBusinessId", lineOfBusinessId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<Skill>>(
                "/v1/Skills/GetSkillsByLocationAndLOB",
                parameters
            );
        }

        public async Task<IList<SkillTestResult>> GetSkillTestResults(Guid applicantId, bool includeExpired = false)
        {
            var parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() },
                { "includeExpired", includeExpired.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<SkillTestResult>>(
                "/v1/Skills/GetSkillTestResults",
                parameters
            );
        }

        public async Task<IList<SkillTestResult>> GetSkillTestResultsByLocationAndByLineOfBusiness(Guid applicantId, int locationId, int lineOfBusinessId)
        {
            var parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() },
                { "locationId", locationId.ToString() },
                { "lineOfBusinessId", lineOfBusinessId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<SkillTestResult>>(
                "/v1/Skills/GetSkillTestResultsByLocationByLob",
                parameters
            );
        }

        public async Task<IList<SkillTestResult>> GetSkillTestResultByLocationIdByRequisition(Guid applicantId, int locationId, int requisitionId, int lineOfBusinessId)
        {
            var parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() },
                { "locationId", locationId.ToString() },
                { "requisitionId", requisitionId.ToString() },
                { "lineOfBusinessId", lineOfBusinessId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<SkillTestResult>>(
                "/v1/Skills/GetSkillTestResultsByLocationByRequisitionAssignment",
                parameters
            );
        }

        public async Task<int> SaveTestResults(IList<SkillTestResult> results)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            return await _webServiceHelper.PostAsync<IList<SkillTestResult>, int>(
                "/v1/Skills/SaveTestResults",
                results
            );
        }

        public async Task<int> SaveTestResults(IList<SkillTestResult> results, string notes)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            string endpoint =
                $"/v1/Skills/SaveTestResults" +
                $"?notes={Uri.EscapeDataString(notes ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<IList<SkillTestResult>, int>(
                endpoint,
                results
            );
        }

        public async Task<FurstPersonResultSet> GetFurstPersonResultSetById(int id)
        {
            var parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<FurstPersonResultSet>(
                "/v1/FurstPerson/GetResultSetById",
                parameters
            );
        }
    }
}
