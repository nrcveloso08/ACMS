using ATS.DTO.Applicant;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IApplicantAddressRepository
    {
        Task<ApplicantAddress> AddOrUpdate(ApplicantAddress applicantAddress);
        Task<ApplicantAddress> GetAddressById(int id);
        Task<ApplicantAddress> GetByApplicant(Guid id);
    }

    public class ApplicantAddressRepository : IApplicantAddressRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ApplicantAddressRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<ApplicantAddress> AddOrUpdate(ApplicantAddress applicantAddress)
        {
            if (applicantAddress == null)
                throw new ArgumentNullException(nameof(applicantAddress));

            return await _webServiceHelper.PostAsync<ApplicantAddress, ApplicantAddress>(
                "/v1/ApplicantAddress/AddOrUpdate",
                applicantAddress
            );
        }

        public async Task<ApplicantAddress> GetAddressById(int id)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "Id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<ApplicantAddress>(
                "/v1/ApplicantAddress/GetAddressById",
                parameters
            );
        }

        public async Task<ApplicantAddress> GetByApplicant(Guid id)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<ApplicantAddress>(
                "/v1/ApplicantAddress/GetAddressByApplicantId",
                parameters
            );
        }
    }
}