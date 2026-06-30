using ATS.DTO.Reporting;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;
using System.Globalization;

namespace ATS.Data.Repository
{
    public interface IReportingRepository
    {
        Task<IPagedResult<ApplicantTestScoresReportRecord>> GetApplicantTestScoresReport(
            DateTime? applicationDateStart,
            DateTime? applicationDateEnd,
            int skipCount = 0,
            int maxResultCount = 0,
            string sortExpression = "");

        Task<IEnumerable<AdResponseRate>> GetAdResponseRates(
            DateTime startDate,
            DateTime endDate,
            string locationIds);

        Task<IEnumerable<AdResponseRate>> GetAdResponseRatesByAdSource(
            DateTime startDate,
            DateTime endDate,
            int locationId);

        Task<IEnumerable<PrescreenerHires>> GetPresceenerHires(
            DateTime startDate,
            DateTime endDate,
            int locationId);

        Task<IEnumerable<GlobalRecruitingPipeline>> GetGlobalRecruitingPipelinesAsync(
            GlobalRecruitingPipelineFilters filters);

        Task<byte[]> ExportGlobalRecruitingPipeline(
            GlobalRecruitingPipelineFilters filters);
    }

    public class ReportingRepository : IReportingRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ReportingRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IPagedResult<ApplicantTestScoresReportRecord>> GetApplicantTestScoresReport(
            DateTime? applicationDateStart,
            DateTime? applicationDateEnd,
            int skipCount = 0,
            int maxResultCount = 0,
            string sortExpression = "")
        {
            string startDate = applicationDateStart != null
                ? applicationDateStart.Value.ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture)
                : "";
            string endDate = applicationDateEnd != null
                ? applicationDateEnd.Value.ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture)
                : "";

            NameValueCollection parameters = new NameValueCollection
            {
                { "applicationDateStart", startDate },
                { "applicationDateEnd", endDate },
                { "sortExpression", sortExpression ?? string.Empty },
                { "skipCount", skipCount.ToString() },
                { "maxResultCount", maxResultCount.ToString() }
            };

            return await _webServiceHelper.GetAsync<PagedResult<ApplicantTestScoresReportRecord>>(
                "/v1/Reporting/GetApplicantTestScoresReport",
                parameters
            );
        }

        public async Task<IEnumerable<AdResponseRate>> GetAdResponseRates(
            DateTime startDate,
            DateTime endDate,
            string locationIds)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "startDate", startDate.ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture) },
                { "endDate", endDate.ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture) },
                { "locationIds", locationIds ?? string.Empty }
            };

            return await _webServiceHelper.GetAsync<IEnumerable<AdResponseRate>>(
                "/v1/Reporting/GetAdResponseRates",
                parameters
            );
        }

        public async Task<IEnumerable<AdResponseRate>> GetAdResponseRatesByAdSource(
            DateTime startDate,
            DateTime endDate,
            int locationId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "startDate", startDate.ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture) },
                { "endDate", endDate.ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture) },
                { "locationId", locationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IEnumerable<AdResponseRate>>(
                "/v1/Reporting/GetAdResponseRatesByAdSource",
                parameters
            );
        }

        public async Task<IEnumerable<PrescreenerHires>> GetPresceenerHires(
            DateTime startDate,
            DateTime endDate,
            int locationId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "startDate", startDate.ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture) },
                { "endDate", endDate.ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture) },
                { "locationId", locationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IEnumerable<PrescreenerHires>>(
                "/v1/Reporting/GetPresceenerHires",
                parameters
            );
        }

        public async Task<IEnumerable<GlobalRecruitingPipeline>> GetGlobalRecruitingPipelinesAsync(
            GlobalRecruitingPipelineFilters filters)
        {
            if (filters == null)
                throw new ArgumentNullException(nameof(filters));

            return await _webServiceHelper.PostAsync
                <GlobalRecruitingPipelineFilters, IEnumerable<GlobalRecruitingPipeline>>(
                    "/v1/Reporting/GetGlobalRecruitingPipelines",
                    filters
                );
        }

        public async Task<byte[]> ExportGlobalRecruitingPipeline(
            GlobalRecruitingPipelineFilters filters)
        {
            if (filters == null)
                throw new ArgumentNullException(nameof(filters));

            return await _webServiceHelper.PostAsync
                <GlobalRecruitingPipelineFilters, byte[]>(
                    "/v1/Reporting/ExportGlobalRecruitingPipeline",
                    filters
                );
        }
    }
}
