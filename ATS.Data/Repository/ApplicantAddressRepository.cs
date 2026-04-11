using ATS.DTO.Applicant;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IApplicantAddressRepository
    {
        Task<ApplicantAddress> GetByApplicant(Guid id);
    }

    public  class ApplicantAddressRepository: IApplicantAddressRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ApplicantAddressRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public Task<ApplicantAddress> GetByApplicant(Guid id)
        {
         NameValueCollection parameters = new NameValueCollection {
                        {"applicantId", id.ToString() }
                    };
            return _webServiceHelper.GetAsync<ApplicantAddress>("/v1/ApplicantAddress/GetAddressByApplicantId", parameters);
        }
    }
}
