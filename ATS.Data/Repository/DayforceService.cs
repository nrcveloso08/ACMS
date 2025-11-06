
using ATS.DTO.Dayforce;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IDayforceService
    {
        Task<EmployeeStatus?> GetEmployeeStatus(string dayforceId);
    }

    public class DayforceService: IDayforceService
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public DayforceService(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<EmployeeStatus?> GetEmployeeStatus(string dayforceId)
        {
           NameValueCollection  parameters = new NameValueCollection
           {
                 {"dayforceId", dayforceId.ToString() }
           };

            var data = await _webServiceHelper.GetAsync<EmployeeStatus?>("/v1/Dayforce/ValidateEmployeeStatus", parameters);

            return data;
        }
    }
}
