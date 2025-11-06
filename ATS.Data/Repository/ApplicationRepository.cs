using ATS.DTO.Application;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Repository   
{
    public interface IApplicationRepository
    {
        public Task<Application> GetRecentApplication(Guid id);
    }
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ApplicationRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public Task<Application> GetRecentApplication(Guid id)
        {
            NameValueCollection parameter = new NameValueCollection
            {
                { "applicantId", id.ToString() }
            };

            var data = _webServiceHelper.GetAsync<Application>("/v1/Application/GetRecentApplication", parameter);

            return data;
        }
    }
}
