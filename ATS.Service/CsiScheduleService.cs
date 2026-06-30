using ATS.Data.Repository;
using ATS.DTO.CSI;
using ATS.DTO.LogEntry;
using ATS.DTO.Scheduler;

namespace ATS.Service
{
    public interface ICsiScheduleService
    {
        Task<IEnumerable<CsiSchedule>> GetCsiSchedulesAsync(int locationId, int lineOfBusinessId, int statusId = 0, DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<CsiSchedule>> GetCsiSchedulesByRequisitionIdAsync(int requisitionId);
        Task<CsiSchedule> SaveCsiScheduleAsync(CsiSchedule csiSchedule);
        Task<bool> BookApplicantToCsiAsync(ScheduleBooking scheduleBooking, int rebookedId = 0);
        Task<LogEntry> GetLatestCsiNoteAsync(Guid applicantId);
        Task<bool> SaveCsiNoteAsync(string applicantId, int requisitionId, string note, string userName, string userEmail);
    }

    public class CsiScheduleService : ICsiScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IRosterCSIRepository _rosterCSIRepository;

        public CsiScheduleService(
            IScheduleRepository scheduleRepository,
            IRosterCSIRepository rosterCSIRepository)
        {
            _scheduleRepository = scheduleRepository;
            _rosterCSIRepository = rosterCSIRepository;
        }

        public async Task<IEnumerable<CsiSchedule>> GetCsiSchedulesAsync(int locationId, int lineOfBusinessId, int statusId = 0, DateTime? startDate = null, DateTime? endDate = null)
        {
            return await _scheduleRepository.GetCSISchedules(locationId, lineOfBusinessId, statusId, startDate, endDate);
        }

        public async Task<IEnumerable<CsiSchedule>> GetCsiSchedulesByRequisitionIdAsync(int requisitionId)
        {
            return await _scheduleRepository.GetCSISchedulesByRequisitionId(requisitionId);
        }

        public async Task<CsiSchedule> SaveCsiScheduleAsync(CsiSchedule csiSchedule)
        {
            return await _scheduleRepository.AddOrUpdateCSISchedule(csiSchedule);
        }

        public async Task<bool> BookApplicantToCsiAsync(ScheduleBooking scheduleBooking, int rebookedId = 0)
        {
            return await _scheduleRepository.BookApplicant(ScheduleType.CSI, scheduleBooking, rebookedId);
        }

        public async Task<LogEntry> GetLatestCsiNoteAsync(Guid applicantId)
        {
            return await _rosterCSIRepository.GetLatestCSINote(applicantId);
        }

        public async Task<bool> SaveCsiNoteAsync(string applicantId, int requisitionId, string note, string userName, string userEmail)
        {
            return await _rosterCSIRepository.SaveCSINote(applicantId, requisitionId, note, userName, userEmail);
        }
    }
}
