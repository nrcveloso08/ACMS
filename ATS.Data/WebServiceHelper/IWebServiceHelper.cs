using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service.WebServiceHelper
{
    public interface IWebServiceHelper
    {
        Task<T> GetAsync<T>(string relativePath, NameValueCollection queryParams = null,
            string token = null, string clientId = null, string clientSecret = null);
        Task<TResponse> PostAsync<TRequest, TResponse>(
            string relativePath, TRequest body,
            string token = null, string clientId = null, string clientSecret = null);
        Task<TResponse> PutAsync<TRequest, TResponse>(
             string relativePath, TRequest body,
             string token = null, string clientId = null, string clientSecret = null);
    }
}
