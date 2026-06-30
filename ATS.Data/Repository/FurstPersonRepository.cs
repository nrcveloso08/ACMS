using ATS.DTO.FurstPerson;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;
using System.Globalization;

namespace ATS.Data.Repository
{
    public interface IFurstPersonRepository
    {
        Task<FurstPersonReport> GetReportByDateRange(
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            string jobIds);
    }

    public class FurstPersonRepository : IFurstPersonRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public FurstPersonRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<FurstPersonReport> GetReportByDateRange(
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            string jobIds)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "startDate", startDate.ToString(CultureInfo.InvariantCulture) },
                { "endDate", endDate.ToString(CultureInfo.InvariantCulture) },
                { "jobIds", jobIds ?? string.Empty }
            };

            return await _webServiceHelper.GetAsync<FurstPersonReport>(
                "/v1/FurstPerson/GetReportByDateRange",
                parameters
            );
        }
    }
}
