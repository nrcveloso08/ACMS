using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service.WebServiceHelper
{
    public interface IWebServiceHelper
    {
        Task<string> GetAsync(string url, string token = null, string clientId = null, string clientSecret = null);
        Task<string> PostAsync<T>(string url, T body, string token = null, string clientId = null, string clientSecret = null);
        Task<string> PutAsync<T>(string url, T body, string token = null, string clientId = null, string clientSecret = null);
    }
}
