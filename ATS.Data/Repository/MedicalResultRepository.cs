using ATS.DTO.Interview;
using ATS.DTO.InterviewSchedule;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IMedicalResultRepository
    {
        Task<bool> AddOrUpdate(MedicalResult medicalResult);
        Task<MedicalResult> GetByApplicantId(Guid applicantId);
        Task<IList<MedicalResult>> GetFilteredMedicalResult(MedicalFilters medicalFilters);
        Task<IList<InterviewStatus>> GetMedicalStatuses(IList<int> locationIds);
    }

    public class MedicalResultRepository : IMedicalResultRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public MedicalResultRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<bool> AddOrUpdate(MedicalResult medicalResult)
        {
            if (medicalResult == null)
                throw new ArgumentNullException(nameof(medicalResult));

            return await _webServiceHelper.PostAsync<MedicalResult, bool>(
                "/v1/Medical/AddOrUpdate",
                medicalResult
            );
        }

        public async Task<MedicalResult> GetByApplicantId(Guid applicantId)
        {
            var parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<MedicalResult>(
                "/v1/Medical/GetByApplicantId",
                parameters
            );
        }

        public async Task<IList<MedicalResult>> GetFilteredMedicalResult(MedicalFilters medicalFilters)
        {
            if (medicalFilters == null)
                throw new ArgumentNullException(nameof(medicalFilters));

            return await _webServiceHelper.PostAsync<MedicalFilters, IList<MedicalResult>>(
                "/v1/Medical/GetFilteredMedicalResult",
                medicalFilters
            );
        }

        public async Task<IList<InterviewStatus>> GetMedicalStatuses(IList<int> locationIds)
        {
            if (locationIds == null)
                throw new ArgumentNullException(nameof(locationIds));

            return await _webServiceHelper.PostAsync<IList<int>, IList<InterviewStatus>>(
                "/v1/Medical/GetMedicalStatuses",
                locationIds
            );
        }
    }
}
