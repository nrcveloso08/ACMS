using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; }
        public string KittBaseUrl { get; set; }
        public string KittToken { get; set; }

        /// <summary>
        /// HTTP timeout (in seconds) applied to calls to ATSWebService / KITT.
        /// Keep this short in development so endpoints fail fast when the
        /// legacy ATSWebService is not running.
        /// </summary>
        public int TimeoutSeconds { get; set; } = 30;

        /// <summary>
        /// Number of attempts (including the first) made for GET requests
        /// before giving up and throwing ExternalServiceUnavailableException.
        /// </summary>
        public int RetryCount { get; set; } = 2;
    }
}
