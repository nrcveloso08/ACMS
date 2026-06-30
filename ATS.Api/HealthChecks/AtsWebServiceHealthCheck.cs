using ATS.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace ATS.Api.HealthChecks
{
    /// <summary>
    /// Reports whether the legacy ATSWebService dependency is reachable.
    /// Exposed at GET /health/atswebservice.
    /// </summary>
    public class AtsWebServiceHealthCheck : IHealthCheck
    {
        private readonly ApiSettings _apiSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public AtsWebServiceHealthCheck(IOptions<ApiSettings> apiSettings, IHttpClientFactory httpClientFactory)
        {
            _apiSettings = apiSettings.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(_apiSettings.BaseUrl))
                return HealthCheckResult.Unhealthy("ApiSettings:BaseUrl is not configured.");

            var timeoutSeconds = _apiSettings.TimeoutSeconds > 0 ? _apiSettings.TimeoutSeconds : 5;

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds));

            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync(_apiSettings.BaseUrl, cts.Token);
                return HealthCheckResult.Healthy($"ATSWebService reachable at {_apiSettings.BaseUrl} ({(int)response.StatusCode}).");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"ATSWebService unreachable at {_apiSettings.BaseUrl}.", ex);
            }
        }
    }
}
