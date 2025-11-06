using ATS.DTO.Hyderabad;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IHyderabadRepository
    {
        Task<HyderabadAdditionalInfo?> GetAdditionalInfoByApplicant(Guid applicantId);
    }

    public class HyderabadRepository: IHyderabadRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public HyderabadRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<HyderabadAdditionalInfo?> GetAdditionalInfoByApplicant(Guid applicantId)
        {
            var parameters = new System.Collections.Specialized.NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };
            var data = await _webServiceHelper.GetAsync<HyderabadAdditionalInfo?>("/v1/HyderabadLocal/GetLocalAdditionalInfo", parameters);
            return data;
        }
    }
}
