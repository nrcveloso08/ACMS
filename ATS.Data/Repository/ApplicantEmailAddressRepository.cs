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
    public interface IApplicantEmailAddressRepository
    {
        Task<IList<ApplicantEmailAddress>> GetByApplicant(Guid applicantId);
    }
    public class ApplicantEmailAddressRepository : IApplicantEmailAddressRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ApplicantEmailAddressRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public Task<IList<ApplicantEmailAddress>> GetByApplicant(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection {
                        {"applicantId", applicantId.ToString() }
                    };

            var data = _webServiceHelper.GetAsync<IList<ApplicantEmailAddress>>("/v1/ApplicantEmailAddress/GetByApplicant", parameters);

            return data;
        }

    }
}
