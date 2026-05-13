using ATS.DTO.Applicant;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IApplicantEmailAddressRepository
    {
        Task<IList<ApplicantEmailAddress>> AddOrUpdate(IList<ApplicantEmailAddress> emailDetailList);
        Task<int> Delete(int id);
        Task<IList<ApplicantEmailAddress>> GetByApplicant(Guid applicantId);
        Task<ApplicantEmailAddress> SetPrimary(ApplicantEmailAddress emailAddressDetails);
    }

    public class ApplicantEmailAddressRepository : IApplicantEmailAddressRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ApplicantEmailAddressRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<ApplicantEmailAddress>> AddOrUpdate(IList<ApplicantEmailAddress> emailDetailList)
        {
            if (emailDetailList == null)
                throw new ArgumentNullException(nameof(emailDetailList));

            return await _webServiceHelper.PostAsync<IList<ApplicantEmailAddress>, IList<ApplicantEmailAddress>>(
                "/v1/ApplicantEmailAddress/AddOrUpdate",
                emailDetailList
            );
        }

        public async Task<int> Delete(int id)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<int>(
                "/v1/ApplicantEmailAddress/Delete",
                parameters
            );
        }

        public async Task<IList<ApplicantEmailAddress>> GetByApplicant(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<ApplicantEmailAddress>>(
                "/v1/ApplicantEmailAddress/GetByApplicant",
                parameters
            );
        }

        public async Task<ApplicantEmailAddress> SetPrimary(ApplicantEmailAddress emailAddressDetails)
        {
            if (emailAddressDetails == null)
                throw new ArgumentNullException(nameof(emailAddressDetails));

            return await _webServiceHelper.PostAsync<ApplicantEmailAddress, ApplicantEmailAddress>(
                "/v1/ApplicantEmailAddress/SetPrimary",
                emailAddressDetails
            );
        }
    }
}