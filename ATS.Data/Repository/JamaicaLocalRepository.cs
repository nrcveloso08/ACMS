using ATS.DTO.Jamaica;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IJamaicaLocalRepository
    {
        Task<JamaicaLocalRequirement> GetRequirementByApplicant(Guid applicantId);
    }

    public class JamaicaLocalRepository : IJamaicaLocalRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public JamaicaLocalRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public Task<JamaicaLocalRequirement> GetRequirementByApplicant(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            var data = _webServiceHelper.GetAsync<JamaicaLocalRequirement>("/v1/JamaicaLocal/GetLocalRequirementByApplicantId", parameters);

            return data;
        }
    }
}
