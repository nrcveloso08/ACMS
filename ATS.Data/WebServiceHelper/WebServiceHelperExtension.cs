using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ATS.Service.WebServiceHelper
{
    /// <summary>
    /// Extension helpers for IWebServiceHelper.
    /// These methods simply delegate to the new, generic versions
    /// implemented in WebServiceHelper to keep your service calls clean.
    /// </summary>
    public static class WebServiceHelperExtensions
    {
        /// <summary>
        /// Performs a GET request with optional query parameters.
        /// Automatically uses the configured BaseUrl and handles deserialization.
        /// </summary>
        public static async Task<T> GetWithParamsAsync<T>(
            this IWebServiceHelper helper,
            string relativePath,
            NameValueCollection parameters = null)
        {
            return await helper.GetAsync<T>(relativePath, parameters);
        }

        /// <summary>
        /// Shortcut for simple GET requests with no parameters.
        /// </summary>
        public static async Task<T> GetAsync<T>(
            this IWebServiceHelper helper,
            string relativePath)
        {
            return await helper.GetAsync<T>(relativePath, null);
        }

        /// <summary>
        /// Performs a POST request with request and response types.
        /// </summary>
        public static async Task<TResponse> PostAsync<TRequest, TResponse>(
            this IWebServiceHelper helper,
            string relativePath,
            TRequest body)
        {
            return await helper.PostAsync<TRequest, TResponse>(relativePath, body);
        }

        /// <summary>
        /// Performs a PUT request with request and response types.
        /// </summary>
        public static async Task<TResponse> PutAsync<TRequest, TResponse>(
            this IWebServiceHelper helper,
            string relativePath,
            TRequest body)
        {
            return await helper.PutAsync<TRequest, TResponse>(relativePath, body);
        }
    }
}
