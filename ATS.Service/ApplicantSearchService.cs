using ATS.Data.Repository;
using ATS.DTO.Applicant;

namespace ATS.Service
{
    public interface IApplicantSearchService
    {
        Task<Applicant> GetByIdAsync(Guid id);
        Task<IEnumerable<Applicant>> SearchByNameAsync(string namePartial, int max = 50);
        Task<Applicant> CreateOrUpdateAsync(Applicant applicant);
    }

    public class ApplicantSearchService : IApplicantSearchService
    {
        private readonly IApplicantRepository _applicantRepository;

        public ApplicantSearchService(IApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public async Task<Applicant> GetByIdAsync(Guid id)
        {
            return await _applicantRepository.GetApplicant(id);
        }

        public async Task<IEnumerable<Applicant>> SearchByNameAsync(string namePartial, int max = 50)
        {
            return await _applicantRepository.GetByNamePartial(namePartial, max);
        }

        public async Task<Applicant> CreateOrUpdateAsync(Applicant applicant)
        {
            return await _applicantRepository.AddOrUpdateApplicant(applicant);
        }
    }
}
