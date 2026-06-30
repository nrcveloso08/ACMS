using ATS.Data.Repository;
using ATS.DTO.InterviewSchedule;
using ATS.DTO.Scheduler;

namespace ATS.Service
{
    public interface IInterviewScheduleService
    {
        Task<IEnumerable<InterviewType>> GetInterviewTypesAsync(bool includeDeleted = false);
        Task<IList<InterviewStatus>> GetNextInterviewStatusesAsync(InterviewStatus currentStatus, IList<int> locationIds);
        Task<IList<AppointmentScheduleOption>> GetAvailableSlotsAsync(Guid applicationId, DateTime date);
        Task<bool> ScheduleInterviewAsync(Guid applicationId, DateTime date, string timeSlotString);
        Task<Interview> GetLatestInterviewAsync(Guid applicantId);
        Task<bool> SaveInterviewAsync(Interview interview);
    }

    public class InterviewScheduleService : IInterviewScheduleService
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly IInterviewTypeRepository _interviewTypeRepository;
        private readonly IInterviewStatusRepository _interviewStatusRepository;
        private readonly IScheduleRepository _scheduleRepository;

        public InterviewScheduleService(
            IInterviewRepository interviewRepository,
            IInterviewTypeRepository interviewTypeRepository,
            IInterviewStatusRepository interviewStatusRepository,
            IScheduleRepository scheduleRepository)
        {
            _interviewRepository = interviewRepository;
            _interviewTypeRepository = interviewTypeRepository;
            _interviewStatusRepository = interviewStatusRepository;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<IEnumerable<InterviewType>> GetInterviewTypesAsync(bool includeDeleted = false)
        {
            return await _interviewTypeRepository.GetAllInterviewType(includeDeleted);
        }

        public async Task<IList<InterviewStatus>> GetNextInterviewStatusesAsync(InterviewStatus currentStatus, IList<int> locationIds)
        {
            return await _interviewStatusRepository.GetNextInterviewStatuses(currentStatus, locationIds);
        }

        public async Task<IList<AppointmentScheduleOption>> GetAvailableSlotsAsync(Guid applicationId, DateTime date)
        {
            return await _scheduleRepository.GetSlotsForApplication(applicationId, date);
        }

        public async Task<bool> ScheduleInterviewAsync(Guid applicationId, DateTime date, string timeSlotString)
        {
            return await _scheduleRepository.SetAppointment(applicationId, date, timeSlotString);
        }

        public async Task<Interview> GetLatestInterviewAsync(Guid applicantId)
        {
            return await _interviewRepository.GetLatestByApplicantId(applicantId);
        }

        public async Task<bool> SaveInterviewAsync(Interview interview)
        {
            return await _interviewRepository.Save(interview);
        }
    }
}
