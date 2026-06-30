using System;

namespace ATS.Service.WebServiceHelper
{
    /// <summary>
    /// Thrown when a call to an external dependency (ATSWebService / KITT) cannot be
    /// completed because the service is unreachable or timed out. Callers that
    /// represent critical lookups can catch this and return a 502/ProblemDetails
    /// response instead of letting the request hang or fail with a generic 500.
    /// </summary>
    public class ExternalServiceUnavailableException : Exception
    {
        public ExternalServiceUnavailableException(string message, Exception? innerException = null)
            : base(message, innerException)
        {
        }
    }
}
