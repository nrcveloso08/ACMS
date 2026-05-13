using ATS.DTO.Harver;
using ATS.Service.WebServiceHelper;
using Newtonsoft.Json.Linq;

namespace ATS.Data.Repository
{
    public interface IHarverAPIRepository
    {
        Task<object> GetToken();
        Task<object> Applicant(JObject json);
        Task<bool> SendTestAttemptContent(Guid applicationId);
        Task<bool> SendAssessmentReminder(Guid applicationId, int maxDays);
        Task<bool> SendTestSelfServiceInvite(Guid applicationId);
        Task<bool> SendHiredCandidatesToHarverByApplicantIds(List<Guid> applicantIds);
        Task<bool> SendHiredCandidatesToHarverByRequisitionId(int requisitionId);
        Task<bool> AddApplicantsByHarverId(List<string> harverIds);
        Task<bool> UpdateReferralCodeAndSmsByHarverIds(List<string> harverIds);
        Task<HarverVacancyList> GetVacancyList();
    }

    public class HarverAPIRepository : IHarverAPIRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public HarverAPIRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<object> GetToken()
        {
            return await _webServiceHelper.PostAsync<object, object>(
                "/v1/HarverAPI/GetToken",
                null
            );
        }

        public async Task<object> Applicant(JObject json)
        {
            if (json == null)
                throw new ArgumentNullException(nameof(json));

            return await _webServiceHelper.PostAsync<JObject, object>(
                "/v1/HarverAPI/Applicant",
                json
            );
        }

        public async Task<bool> SendTestAttemptContent(Guid applicationId)
        {
            string endpoint =
                $"/v1/HarverAPI/SendTestAttemptContent" +
                $"?applicationId={Uri.EscapeDataString(applicationId.ToString())}";

            return await _webServiceHelper.PostAsync<object, bool>(
                endpoint,
                null
            );
        }

        public async Task<bool> SendAssessmentReminder(
            Guid applicationId,
            int maxDays)
        {
            string endpoint =
                $"/v1/HarverAPI/SendAssessmentReminder" +
                $"?applicationId={Uri.EscapeDataString(applicationId.ToString())}" +
                $"&maxDays={maxDays}";

            return await _webServiceHelper.PostAsync<object, bool>(
                endpoint,
                null
            );
        }

        public async Task<bool> SendTestSelfServiceInvite(Guid applicationId)
        {
            string endpoint =
                $"/v1/HarverAPI/TestSelfServiceInvite" +
                $"?applicationId={Uri.EscapeDataString(applicationId.ToString())}";

            return await _webServiceHelper.PostAsync<object, bool>(
                endpoint,
                null
            );
        }

        public async Task<bool> SendHiredCandidatesToHarverByApplicantIds(
            List<Guid> applicantIds)
        {
            if (applicantIds == null)
                throw new ArgumentNullException(nameof(applicantIds));

            return await _webServiceHelper.PostAsync<List<Guid>, bool>(
                "/v1/HarverAPI/SendHiredCandidatesToHarverByApplicantIds",
                applicantIds
            );
        }

        public async Task<bool> SendHiredCandidatesToHarverByRequisitionId(
            int requisitionId)
        {
            string endpoint =
                $"/v1/HarverAPI/SendHiredCandidatesToHarverByRequisitionId" +
                $"?requisitionId={requisitionId}";

            return await _webServiceHelper.PostAsync<object, bool>(
                endpoint,
                null
            );
        }

        public async Task<bool> AddApplicantsByHarverId(List<string> harverIds)
        {
            if (harverIds == null)
                throw new ArgumentNullException(nameof(harverIds));

            return await _webServiceHelper.PostAsync<List<string>, bool>(
                "/v1/HarverAPI/AddApplicantsByHarverId",
                harverIds
            );
        }

        public async Task<bool> UpdateReferralCodeAndSmsByHarverIds(
            List<string> harverIds)
        {
            if (harverIds == null)
                throw new ArgumentNullException(nameof(harverIds));

            return await _webServiceHelper.PostAsync<List<string>, bool>(
                "/v1/HarverAPI/UpdateReferralCodeAndSmsByHarverIds",
                harverIds
            );
        }

        public async Task<HarverVacancyList> GetVacancyList()
        {
            return await _webServiceHelper.GetAsync<HarverVacancyList>(
                "/v1/HarverAPI/GetVacancyList"
            );
        }
    }
}