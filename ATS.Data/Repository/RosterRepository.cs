using ATS.DTO;
using ATS.DTO.Roster;
using ATS.Service.WebServiceHelper;

namespace ATS.Data.Repository
{
    public interface IRosterRepository
    {
        Task<IList<ApplicantForRecruitment>> GetApplicantsByStatus(InterviewStatus status, IList<int> locationIds);
    }

    public class RosterRepository : IRosterRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public RosterRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<ApplicantForRecruitment>> GetApplicantsByStatus(InterviewStatus status, IList<int> locationIds)
        {
            if (locationIds == null)
                throw new ArgumentNullException(nameof(locationIds));

            var endpoint =
                $"/v1/Roster/GetApplicantsByStatus" +
                $"?status={Uri.EscapeDataString(status.ToString())}";

            return await _webServiceHelper.PostAsync<IList<int>, IList<ApplicantForRecruitment>>(
                endpoint,
                locationIds
            );
        }
    }
}
