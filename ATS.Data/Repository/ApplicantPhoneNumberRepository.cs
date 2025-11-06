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
    public interface IApplicantPhoneNumberRepository
    {
        Task<IList<ApplicantPhoneNumber>> GetApplicantPhoneNumbers(Guid applicantId);
    }

    public class ApplicantPhoneNumberRepository: IApplicantPhoneNumberRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ApplicantPhoneNumberRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public Task<IList<ApplicantPhoneNumber>> GetApplicantPhoneNumbers(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
           {
                  {"applicantId", applicantId.ToString() }
           };

            var data = _webServiceHelper.GetAsync<IList<ApplicantPhoneNumber>>("/v1/ApplicantPhoneNumber/GetByApplicant", parameters);

            return data; 
        }
    }
}
