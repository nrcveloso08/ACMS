using ATS.DTO;
using ATS.DTO.Applicant;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IApplicantStatusRepository
    {
        Task<ApplicantStatus> GetRecentStatus(Guid applicantId);

        Task<ApplicantStatus> UpdateApplicantStatus(
            ApplicantStatus applicantStatus,
            int requisitionRequestId = 0,
            int disposition = 0,
            decimal wage = 0);

        Task<IList<ApplicantStatus>> BulkUpdateApplicantStatus(
            IList<ApplicantStatus> applicantStatuses);

        Task<IList<ApplicantStatus>> GetStatusByApplicantId(Guid applicantId);

        Task<ApplicantStatus> SetPreviousStatus(Guid applicantId, string note);
    }

    public class ApplicantStatusRepository : IApplicantStatusRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ApplicantStatusRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<ApplicantStatus> GetRecentStatus(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<ApplicantStatus>(
                "/v1/ApplicantStatus/GetRecentStatus",
                parameters
            );
        }

        public async Task<ApplicantStatus> UpdateApplicantStatus(
            ApplicantStatus applicantStatus,
            int requisitionRequestId = 0,
            int disposition = 0,
            decimal wage = 0)
        {
            if (applicantStatus == null)
                throw new ArgumentNullException(nameof(applicantStatus));

            string endpoint =
                $"/v1/ApplicantStatus/UpdateApplicantStatus" +
                $"?requisitionRequestId={requisitionRequestId}" +
                $"&disposition={disposition}" +
                $"&wage={wage}";

            return await _webServiceHelper.PostAsync<ApplicantStatus, ApplicantStatus>(
                endpoint,
                applicantStatus
            );
        }

        public async Task<IList<ApplicantStatus>> BulkUpdateApplicantStatus(
            IList<ApplicantStatus> applicantStatuses)
        {
            if (applicantStatuses == null)
                throw new ArgumentNullException(nameof(applicantStatuses));

            return await _webServiceHelper.PostAsync
                <IList<ApplicantStatus>, IList<ApplicantStatus>>(
                    "/v1/ApplicantStatus/BulkUpdateApplicantStatus",
                    applicantStatuses
                );
        }

        public async Task<IList<ApplicantStatus>> GetStatusByApplicantId(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<ApplicantStatus>>(
                "/v1/ApplicantStatus/GetStatusByApplicantId",
                parameters
            );
        }

        public async Task<ApplicantStatus> SetPreviousStatus(Guid applicantId, string note)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() },
                { "note", note ?? string.Empty }
            };

            string endpoint =
                $"/v1/ApplicantStatus/SetPreviousStatus" +
                $"?applicantId={Uri.EscapeDataString(applicantId.ToString())}" +
                $"&note={Uri.EscapeDataString(note ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<object, ApplicantStatus>(
                endpoint,
                null
            );
        }
    }
}