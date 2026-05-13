using ATS.DTO;
using ATS.DTO.Application;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IEmployeeStatusRepository
    {
        Task<IEnumerable<EmployeeStatus>> GetAllEmployeeStatus();
        Task<EmployeeStatus> GetEmployeeStatus(int id);
        Task<int> SaveEmployeeStatus(EmployeeStatus employeeStatus);
        Task<int> RemoveEmployeeStatus(int id);
    }

    public class EmployeeStatusRepository : IEmployeeStatusRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public EmployeeStatusRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IEnumerable<EmployeeStatus>> GetAllEmployeeStatus()
        {
            return await _webServiceHelper.GetAsync<IEnumerable<EmployeeStatus>>(
                "/v1/EmployeeStatus/GetAllEmployeeStatus"
            );
        }

        public async Task<EmployeeStatus> GetEmployeeStatus(int id)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<EmployeeStatus>(
                "/v1/EmployeeStatus/GetEmployeeStatus",
                parameters
            );
        }

        public async Task<int> SaveEmployeeStatus(EmployeeStatus employeeStatus)
        {
            if (employeeStatus == null)
                throw new ArgumentNullException(nameof(employeeStatus));

            return await _webServiceHelper.PostAsync<EmployeeStatus, int>(
                "/v1/EmployeeStatus/SaveEmployeeStatus",
                employeeStatus
            );
        }

        public async Task<int> RemoveEmployeeStatus(int id)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<int>(
                "/v1/EmployeeStatus/RemoveEmployeeStatus",
                parameters
            );
        }
    }
}