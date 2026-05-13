using ATS.DTO.Applicant;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IApplicantPhoneNumberRepository
    {
        Task<IList<ApplicantPhoneNumber>> AddOrUpdate(IList<ApplicantPhoneNumber> phoneNumberDetailList);
        Task<ApplicantPhoneNumber> SetPrimary(ApplicantPhoneNumber phoneNumberDetails);
        Task<int> Delete(int id);
        Task<IList<ApplicantPhoneNumber>> GetApplicantPhoneNumbers(Guid applicantId);
    }

    public class ApplicantPhoneNumberRepository : IApplicantPhoneNumberRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ApplicantPhoneNumberRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<ApplicantPhoneNumber>> AddOrUpdate(
            IList<ApplicantPhoneNumber> phoneNumberDetailList)
        {
            if (phoneNumberDetailList == null)
                throw new ArgumentNullException(nameof(phoneNumberDetailList));

            return await _webServiceHelper.PostAsync
                <IList<ApplicantPhoneNumber>, IList<ApplicantPhoneNumber>>(
                    "/v1/ApplicantPhoneNumber/AddOrUpdate",
                    phoneNumberDetailList
                );
        }

        public async Task<ApplicantPhoneNumber> SetPrimary(
            ApplicantPhoneNumber phoneNumberDetails)
        {
            if (phoneNumberDetails == null)
                throw new ArgumentNullException(nameof(phoneNumberDetails));

            return await _webServiceHelper.PostAsync
                <ApplicantPhoneNumber, ApplicantPhoneNumber>(
                    "/v1/ApplicantPhoneNumber/SetPrimary",
                    phoneNumberDetails
                );
        }

        public async Task<int> Delete(int id)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<int>(
                "/v1/ApplicantPhoneNumber/Delete",
                parameters
            );
        }

        public async Task<IList<ApplicantPhoneNumber>> GetApplicantPhoneNumbers(
            Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<ApplicantPhoneNumber>>(
                "/v1/ApplicantPhoneNumber/GetByApplicant",
                parameters
            );
        }
    }
}