using ATS.DTO.Guatemala;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IGuatemalaRepository
    {
        Task<GuatemalaAdditionalInfo> GetAdditionalInfoByApplicant(Guid applicantId);
    }

    public class GuatemalaRepository: IGuatemalaRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

       public GuatemalaRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public Task<GuatemalaAdditionalInfo> GetAdditionalInfoByApplicant(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
           {
                 {"applicantId", applicantId.ToString() }
           };
            var data = _webServiceHelper.GetAsync<GuatemalaAdditionalInfo>("/v1/Guatemala/GetAdditionalInfoByApplicantId", parameters);
            return data;
        }

    }
}
