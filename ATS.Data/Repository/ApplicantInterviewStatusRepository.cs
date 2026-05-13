using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    using global::ATS.DTO.Applicant;
    using global::ATS.Service.WebServiceHelper;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading.Tasks;

    namespace ATS.Data.Repository
    {
        public interface IApplicantInterviewStatusRepository
        {
            Task<IEnumerable<ApplicantInterviewStatus>> GetAllApplicantInterviewStatus(bool? includeDeleted = false);
            Task<ApplicantInterviewStatus> GetApplicantInterviewStatus(int id);
            Task<int> SaveApplicantInterviewStatus(ApplicantInterviewStatus applicantInterviewStatus);
            Task<int> RemoveApplicantInterviewStatus(int id);
        }

        public class ApplicantInterviewStatusRepository : IApplicantInterviewStatusRepository
        {
            private readonly IWebServiceHelper _webServiceHelper;

            public ApplicantInterviewStatusRepository(IWebServiceHelper webServiceHelper)
            {
                _webServiceHelper = webServiceHelper;
            }

            public async Task<IEnumerable<ApplicantInterviewStatus>> GetAllApplicantInterviewStatus(bool? includeDeleted = false)
            {
                NameValueCollection parameters = new NameValueCollection
            {
                { "includeDeleted", includeDeleted.ToString() }
            };

                return await _webServiceHelper.GetAsync<IEnumerable<ApplicantInterviewStatus>>(
                    "/v1/ApplicantInterviewStatus/GetAllApplicantInterviewStatus",
                    parameters
                );
            }

            public async Task<ApplicantInterviewStatus> GetApplicantInterviewStatus(int id)
            {
                NameValueCollection parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

                return await _webServiceHelper.GetAsync<ApplicantInterviewStatus>(
                    "/v1/ApplicantInterviewStatus/GetApplicantInterviewStatus",
                    parameters
                );
            }

            public async Task<int> SaveApplicantInterviewStatus(ApplicantInterviewStatus applicantInterviewStatus)
            {
                if (applicantInterviewStatus == null)
                    throw new ArgumentNullException(nameof(applicantInterviewStatus));

                return await _webServiceHelper.PostAsync<ApplicantInterviewStatus, int>(
                    "/v1/ApplicantInterviewStatus/SaveApplicantInterviewStatus",
                    applicantInterviewStatus
                );
            }

            public async Task<int> RemoveApplicantInterviewStatus(int id)
            {
                NameValueCollection parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

                return await _webServiceHelper.GetAsync<int>(
                    "/v1/ApplicantInterviewStatus/RemoveApplicantInterviewStatus",
                    parameters
                );
            }
        }
    }
}
