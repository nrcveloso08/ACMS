using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Web;

namespace ATS.Service.WebServiceHelper
{
    public static class WebServiceHelperExtensions
    {
        public static async Task<T> GetAsync<T>(
            this IWebServiceHelper helper,
            string endpoint,
            NameValueCollection parameters)
        {
            // Convert NameValueCollection to query string (e.g., "?id=123&status=active")
            string query = string.Empty;
            if (parameters != null && parameters.Count > 0)
            {
                var queryString = string.Join("&", parameters.AllKeys
                    .Select(k => $"{HttpUtility.UrlEncode(k)}={HttpUtility.UrlEncode(parameters[k])}"));
                query = "?" + queryString;
            }

            // Build full endpoint with query parameters
            string fullEndpoint = endpoint + query;

            // Call your original IWebServiceHelper.GetAsync(string, string, string, string)
            string json = await helper.GetAsync(fullEndpoint, null, null, null);

            // Deserialize JSON response into any type you specify
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
