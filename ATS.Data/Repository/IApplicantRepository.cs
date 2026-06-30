using ATS.DTO.Applicant;
using ATS.DTO.CandidateSearch;
using ATS.DTO.LogEntry;

namespace ATS.Data.Repository
{
    public interface IApplicantRepository
    {
        Task<Applicant> GetApplicant(Guid id);
        Task<Applicant> AddOrUpdateApplicant(Applicant applicant);
        Task<List<Applicant>> GetByEmail(string email, int max = 50);
        Task<List<Applicant>> GetByPhone(string phone, int max = 1);
        Task<IEnumerable<Applicant>> GetByNamePartial(string namePartial, int max = 50);
        Task<int> AddNotes(Guid applicantId, string note, string userName, string userEmail);
        Task<IList<LogEntry>> GetNotes(Guid applicantId);
        Task<LogEntry> GetLatestNote(Guid applicantId);
        Task<bool> RemoveApplicantPII(Guid applicantId);
        Task<int> SetApplicantIsSmsEnabledTrue(Guid applicantId);
        Task<CandidateSearchApplicantPageDto> SearchApplicantsByInfo(CandidateSearchApplicantFilters filters);
    }
}
