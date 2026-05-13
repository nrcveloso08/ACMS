using ATS.DTO.Application;
using ATS.DTO.HR;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IHRRepository
    {
        Task<IList<HRChecklistOption>> GetAllChecklistOptions();
        Task<IList<HRCheckListItem>> GetAllChecklistItems();
        Task<IList<HRChecklist>> GetAllChecklists();

        Task<IList<HRChecklist>> GetChecklistsByLocationId(int locationId);
        Task<IList<HRChecklist>> GetChecklistsByLocationIds(IList<int> locationIds);

        Task<IList<HRCheckResponse>> GetCheckResponsesByApplicantId(Guid applicantId);
        Task<HRCheckResponse> AddOrUpdateCheckResponse(HRCheckResponse hrCheckResponse);

        Task<HRChecklistOption> AddOrUpdateChecklistOption(HRChecklistOption hrChecklistOption);
        Task<HRCheckListItem> AddOrUpdateChecklistItem(HRCheckListItem hrCheckListItem);
        Task<HRChecklist> AddOrUpdateChecklist(HRChecklist hrChecklist);

        Task<IList<ApplicantForHR>> GetApplicants(ApplicantHRProcessFilter applicantHRProcessFilter);
        Task<IList<HRCheckListItem>> GetApplicantCheckList(Guid applicantId);

        Task<bool> CompleteHRProcessingByApplicant(Guid applicantId);
        Task<ApplicationPaginationProperties> GetHRResultByLocation(ApplicantHRProcessFilter filters);
    }

    public class HRRepository : IHRRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public HRRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<HRChecklistOption>> GetAllChecklistOptions()
        {
            return await _webServiceHelper.GetAsync<IList<HRChecklistOption>>(
                "/v1/HR/GetAllChecklistOptions"
            );
        }

        public async Task<IList<HRCheckListItem>> GetAllChecklistItems()
        {
            return await _webServiceHelper.GetAsync<IList<HRCheckListItem>>(
                "/v1/HR/GetAllChecklistItems"
            );
        }

        public async Task<IList<HRChecklist>> GetAllChecklists()
        {
            return await _webServiceHelper.GetAsync<IList<HRChecklist>>(
                "/v1/HR/GetAllChecklists"
            );
        }

        public async Task<IList<HRChecklist>> GetChecklistsByLocationId(int locationId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "locationId", locationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<HRChecklist>>(
                "/v1/HR/GetChecklistsByLocationId",
                parameters
            );
        }

        public async Task<IList<HRChecklist>> GetChecklistsByLocationIds(IList<int> locationIds)
        {
            if (locationIds == null)
                throw new ArgumentNullException(nameof(locationIds));

            return await _webServiceHelper.PostAsync<IList<int>, IList<HRChecklist>>(
                "/v1/HR/GetChecklistsByLocationIds",
                locationIds
            );
        }

        public async Task<IList<HRCheckResponse>> GetCheckResponsesByApplicantId(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<HRCheckResponse>>(
                "/v1/HR/GetCheckResponsesByApplicantId",
                parameters
            );
        }

        public async Task<HRCheckResponse> AddOrUpdateCheckResponse(HRCheckResponse hrCheckResponse)
        {
            if (hrCheckResponse == null)
                throw new ArgumentNullException(nameof(hrCheckResponse));

            return await _webServiceHelper.PostAsync<HRCheckResponse, HRCheckResponse>(
                "/v1/HR/AddOrUpdateCheckResponse",
                hrCheckResponse
            );
        }

        public async Task<HRChecklistOption> AddOrUpdateChecklistOption(HRChecklistOption hrChecklistOption)
        {
            if (hrChecklistOption == null)
                throw new ArgumentNullException(nameof(hrChecklistOption));

            return await _webServiceHelper.PostAsync<HRChecklistOption, HRChecklistOption>(
                "/v1/HR/AddOrUpdateChecklistOption",
                hrChecklistOption
            );
        }

        public async Task<HRCheckListItem> AddOrUpdateChecklistItem(HRCheckListItem hrCheckListItem)
        {
            if (hrCheckListItem == null)
                throw new ArgumentNullException(nameof(hrCheckListItem));

            return await _webServiceHelper.PostAsync<HRCheckListItem, HRCheckListItem>(
                "/v1/HR/AddOrUpdateChecklistItem",
                hrCheckListItem
            );
        }

        public async Task<HRChecklist> AddOrUpdateChecklist(HRChecklist hrChecklist)
        {
            if (hrChecklist == null)
                throw new ArgumentNullException(nameof(hrChecklist));

            return await _webServiceHelper.PostAsync<HRChecklist, HRChecklist>(
                "/v1/HR/AddOrUpdateChecklist",
                hrChecklist
            );
        }

        public async Task<IList<ApplicantForHR>> GetApplicants(ApplicantHRProcessFilter applicantHRProcessFilter)
        {
            if (applicantHRProcessFilter == null)
                throw new ArgumentNullException(nameof(applicantHRProcessFilter));

            return await _webServiceHelper.PostAsync<ApplicantHRProcessFilter, IList<ApplicantForHR>>(
                "/v1/HR/GetApplicants",
                applicantHRProcessFilter
            );
        }

        public async Task<IList<HRCheckListItem>> GetApplicantCheckList(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<HRCheckListItem>>(
                "/v1/HR/GetApplicantCheckList",
                parameters
            );
        }

        public async Task<bool> CompleteHRProcessingByApplicant(Guid applicantId)
        {
            string endpoint =
                $"/v1/HR/CompleteHRProcessingByApplicant" +
                $"?applicantId={Uri.EscapeDataString(applicantId.ToString())}";

            return await _webServiceHelper.PostAsync<object, bool>(
                endpoint,
                null
            );
        }

        public async Task<ApplicationPaginationProperties> GetHRResultByLocation(ApplicantHRProcessFilter filters)
        {
            if (filters == null)
                throw new ArgumentNullException(nameof(filters));

            return await _webServiceHelper.PostAsync<ApplicantHRProcessFilter, ApplicationPaginationProperties>(
                "/v1/HR/GetHRResultByLocation",
                filters
            );
        }
    }
}