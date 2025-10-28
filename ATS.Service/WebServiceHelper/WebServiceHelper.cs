using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ATS.Service.WebServiceHelper
{
    public class WebServiceHelper : IWebServiceHelper
    {
        private readonly HttpClient _httpClient;

        private readonly string _defaultToken;
        private readonly string _defaultClientId;
        private readonly string _defaultClientSecret;

        /// <summary>
        /// Constructor with optional default credentials.
        /// </summary>
        public WebServiceHelper(string token = null, string clientId = null, string clientSecret = null)
        {
            _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(60) };

            _defaultToken = token;
            _defaultClientId = clientId;
            _defaultClientSecret = clientSecret;
        }

        /// <summary>
        /// Adds optional headers (Token, Client ID, Client Secret) from either defaults or overrides.
        /// </summary>
        private void AddHeaders(string token = null, string clientId = null, string clientSecret = null)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string resolvedToken = token ?? _defaultToken;
            string resolvedClientId = clientId ?? _defaultClientId;
            string resolvedClientSecret = clientSecret ?? _defaultClientSecret;

            if (!string.IsNullOrWhiteSpace(resolvedToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resolvedToken);

            if (!string.IsNullOrWhiteSpace(resolvedClientId))
                _httpClient.DefaultRequestHeaders.Add("Client-ID", resolvedClientId);

            if (!string.IsNullOrWhiteSpace(resolvedClientSecret))
                _httpClient.DefaultRequestHeaders.Add("Client-Secret", resolvedClientSecret);
        }

        /// <summary>
        /// Executes a GET request.
        /// </summary>
        public async Task<string> GetAsync(string url, string token = null, string clientId = null, string clientSecret = null)
        {
            AddHeaders(token, clientId, clientSecret);

            using (var response = await _httpClient.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Executes a POST request with a JSON body.
        /// </summary>
        public async Task<string> PostAsync<T>(string url, T body, string token = null, string clientId = null, string clientSecret = null)
        {
            AddHeaders(token, clientId, clientSecret);

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync(url, content))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Executes a PUT request with a JSON body.
        /// </summary>
        public async Task<string> PutAsync<T>(string url, T body, string token = null, string clientId = null, string clientSecret = null)
        {
            AddHeaders(token, clientId, clientSecret);

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PutAsync(url, content))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
