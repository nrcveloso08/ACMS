using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service.WebServiceHelper
{
    public class WebServiceHelper : IWebServiceHelper
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        private readonly string _defaultToken;
        private readonly string _defaultClientId;
        private readonly string _defaultClientSecret;

        public static WebServiceHelper KittInstance { get; private set; }

        /// <summary>
        /// Constructor with optional default credentials and base URL.
        /// Ensures HttpClient is initialized with a valid BaseAddress.
        /// </summary>
        public WebServiceHelper(string baseUrl, string token = null, string clientId = null, string clientSecret = null)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl), "Base URL cannot be null or empty.");

            _baseUrl = baseUrl.TrimEnd('/');

            // ✅ FIX: set BaseAddress here so relative paths work
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl),
                Timeout = TimeSpan.FromSeconds(60)
            };

            _defaultToken = token;
            _defaultClientId = clientId;
            _defaultClientSecret = clientSecret;
        }

        public static void InitializeKitt(string baseUrl, string token = null, string clientId = null, string clientSecret = null)
        {
            KittInstance = new WebServiceHelper(baseUrl, token, clientId, clientSecret);
        }

        /// <summary>
        /// Adds optional headers (Token, Client ID, Client Secret).
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
        /// Builds the relative URL path with optional query parameters.
        /// (No base URL here; HttpClient.BaseAddress handles that.)
        /// </summary>
        private string BuildUrl(string relativePath, NameValueCollection queryParams = null)
        {
            var url = $"{relativePath.TrimStart('/')}";
            if (queryParams != null && queryParams.Count > 0)
            {
                var qs = string.Join("&",
                    queryParams.AllKeys
                        .Where(k => k != null)
                        .Select(k => $"{Uri.EscapeDataString(k)}={Uri.EscapeDataString(queryParams[k])}")
                );
                url += "?" + qs;
            }
            return url;
        }

        /// <summary>
        /// Generic GET request that deserializes JSON to T.
        /// </summary>
        public async Task<T> GetAsync<T>(string relativePath, NameValueCollection queryParams = null, string token = null,
            string clientId = null, string clientSecret = null)
        {
            string relativeUrl = BuildUrl(relativePath, queryParams);
            string fullUrl = $"{_baseUrl}/{relativeUrl}";
            Console.WriteLine($"➡️ [GET] {fullUrl}");

            AddHeaders(token, clientId, clientSecret);

            const int maxRetries = 2;
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    var response = await _httpClient.GetAsync(relativeUrl);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();

                    // ✅ Try to handle double-encoded JSON payloads
                    try
                    {
                        // Attempt normal deserialization
                        return SafeDeserialize<T>(json);
                    }
                    catch (JsonSerializationException)
                    {
                        // ✅ Fallback: handle escaped JSON strings (double serialized)
                        var outer = JsonConvert.DeserializeObject<JObject>(json);
                        var innerJson = outer?["Data"]?.ToString();

                        if (!string.IsNullOrWhiteSpace(innerJson) &&
                            (innerJson.TrimStart().StartsWith("[") || innerJson.TrimStart().StartsWith("{")))
                        {
                            var innerObj = JsonConvert.DeserializeObject(innerJson);
                            var normalized = new JObject { ["Data"] = JToken.FromObject(innerObj) };
                            return normalized.ToObject<T>();
                        }

                        throw; // rethrow if still invalid
                    }
                }
                catch (HttpRequestException ex) when (attempt < maxRetries)
                {
                    Console.WriteLine($"⚠️ GET retry {attempt}/{maxRetries} due to network issue: {ex.Message}");
                    await Task.Delay(1000);
                }
            }

            throw new HttpRequestException($"GET {fullUrl} failed after {maxRetries} attempts.");
        }


        /// <summary>
        /// Executes a POST request with JSON body and returns deserialized response.
        /// </summary>
        public async Task<TResponse> PostAsync<TRequest, TResponse>(
      string relativePath,
      TRequest body,
      string token = null,
      string clientId = null,
      string clientSecret = null)
        {
            // 🧠 Build and log the full URL
            string relativeUrl = BuildUrl(relativePath);
            string fullUrl = $"{_baseUrl.TrimEnd('/')}/{relativeUrl.TrimStart('/')}";
            Console.WriteLine($"➡️ [POST] {fullUrl}");

            // 🧠 Add headers (Auth, ClientId, etc.)
            AddHeaders(token, clientId, clientSecret);

            // 🧠 Serialize object to JSON
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // ✅ Use FULL URL (not relative!)
                var response = await _httpClient.PostAsync(fullUrl, content);

                Console.WriteLine($"⬅️ Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");

                // ✅ If API returns error (400–599), throw with full message
                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ Error Response: {errorBody}");
                    throw new HttpRequestException(
                        $"HTTP {(int)response.StatusCode} {response.ReasonPhrase} - {errorBody}");
                }

                // ✅ Read and log the response body
                var respContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"⬅️ Response Body: {respContent}");

                // ✅ Handle empty body (e.g. 204 No Content)
                if (string.IsNullOrWhiteSpace(respContent))
                    return default!;

                // ✅ Deserialize and return response
                return JsonConvert.DeserializeObject<TResponse>(respContent)!;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"❌ HTTP ERROR: {ex.Message}");
                throw; // rethrow to allow service-level handling
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"❌ JSON PARSE ERROR: {ex.Message}");
                throw new Exception($"Failed to deserialize response from {relativePath}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ UNKNOWN ERROR: {ex}");
                throw;
            }
        }

        /// <summary>
        /// Executes a PUT request with JSON body and returns deserialized response.
        /// </summary>
        public async Task<TResponse> PutAsync<TRequest, TResponse>(
            string relativePath,
            TRequest body,
            string token = null,
            string clientId = null,
            string clientSecret = null)
        {
            string relativeUrl = BuildUrl(relativePath);
            string fullUrl = $"{_baseUrl}/{relativeUrl}";
            Console.WriteLine($"➡️ [PUT] {fullUrl}");

            AddHeaders(token, clientId, clientSecret);
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(relativeUrl, content); // ✅ relative path ok
            response.EnsureSuccessStatusCode();

            var respContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(respContent);
        }

        private static T SafeDeserialize<T>(string json)
        {
            try
            {
                // 🟢 Try normal deserialization first (fast path)
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonSerializationException)
            {
                Console.WriteLine("⚠️ Standard JSON parse failed — attempting to normalize structure...");

                try
                {
                    var jObj = JsonConvert.DeserializeObject<JObject>(json);

                    // If there's a 'Data' or 'data' node
                    var dataNode = jObj?["Data"] ?? jObj?["data"];
                    if (dataNode != null)
                    {
                        // 1️⃣ If 'Data' is a string (double-encoded JSON)
                        if (dataNode.Type == JTokenType.String)
                        {
                            string innerRaw = dataNode.ToString();
                            if (!string.IsNullOrWhiteSpace(innerRaw) &&
                                (innerRaw.TrimStart().StartsWith("[") || innerRaw.TrimStart().StartsWith("{")))
                            {
                                Console.WriteLine("🟡 Double-encoded JSON detected — unwrapping...");
                                var inner = JsonConvert.DeserializeObject(innerRaw);
                                jObj["Data"] = JToken.FromObject(inner);
                            }
                        }

                        // 2️⃣ If 'Data' is an object instead of an array
                        if (dataNode.Type == JTokenType.Object)
                        {
                            Console.WriteLine("🟢 Wrapping single object into array for consistency...");
                            jObj["Data"] = new JArray(dataNode); // wrap in array
                        }
                    }

                    // ✅ Deserialize the normalized JSON into the target type
                    return jObj.ToObject<T>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ JSON normalization failed: {ex.Message}");
                    throw new JsonSerializationException("Unable to deserialize JSON after normalization.", ex);
                }
            }
        }

    }
}
