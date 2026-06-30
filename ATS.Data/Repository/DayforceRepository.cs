
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
    public interface IDayforceRepository
    {
        Task<DayforceEmployeeStatus?> GetEmployeeStatus(string dayforceId);
    }

    public class DayforceRepository: IDayforceRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public DayforceRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<DayforceEmployeeStatus?> GetEmployeeStatus(string dayforceId)
        {
           NameValueCollection  parameters = new NameValueCollection
           {
                 {"dayforceId", dayforceId.ToString() }
           };

            var data = await _webServiceHelper.GetAsync<DayforceEmployeeStatus?>("/v1/Dayforce/ValidateEmployeeStatus", parameters);

            return data;
        }
    }
}
