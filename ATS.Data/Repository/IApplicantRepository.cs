using ATS.DTO.Applicant;

namespace ATS.Data.Repository
{
    public interface IApplicantRepository
    {
        Task<Applicant> GetApplicant(Guid id);
    }
}
