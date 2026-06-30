using ATS.DTO.CSI;
using ATS.DTO.Scheduler;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;
using System.Globalization;

namespace ATS.Data.Repository
{
    public interface IScheduleRepository
    {
        Task<ScheduleException> GetException(int id);
        Task<TimeSlotConfig> GetTimeSlotConfig(int locationId);
        Task<int> DeleteException(int id);
        Task<ScheduleException> SaveException(ScheduleException scheduleException);
        Task<TimeSlotConfig> SaveTimeSlotConfig(TimeSlotConfig timeSlotConfig);
        Task<IList<TimeSlotConfig>> GetTimeSlotConfigs();
        Task<IList<TimeSlotConfig>> GetTimeSlots();
        Task<IList<ScheduleException>> GetScheduleExceptions();
        Task<IList<AppointmentScheduleOption>> GetSlotsForApplication(Guid applicationId, DateTime date);
        Task<bool> SetAppointment(Guid applicationId, DateTime date, string timeSlotString);
        Task<ApplicantAppointment> GetApplicantAppointment(Guid applicantId);
        Task<CsiSchedule> AddOrUpdateCSISchedule(CsiSchedule csiSchedule);
        Task<CsiSchedule> GetCSISchedule(int id);
        Task<IEnumerable<CsiSchedule>> GetCSISchedules(int locationId, int lineOfBusinessId, int statusId = 0, DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<CsiSchedule>> GetCSISchedulesByRequisitionId(int requisitionId);
        Task<IList<ScheduleDetails>> GetSchedules(ScheduleType scheduleType, ScheduleFilterArgs filter);
        Task<IList<ScheduleDetails>> GetSchedulesByWave(ScheduleType scheduleType, int requisitionId);
        Task<bool> AddOrUpdateSchedule(Schedule schedule);
        Task<bool> BookApplicant(ScheduleType scheduleType, ScheduleBooking scheduleBooking, int rebookedId = 0);
        Task<ScheduleDetails> GetApplicantBooking(ScheduleType scheduleType, Guid applicantId);
        Task<ScheduleDetails> GetScheduleDetailsById(ScheduleType scheduleType, int id);
        Task<IEnumerable<ScheduleApplicantInfo>> GetApplicantsBySchedule(ScheduleType scheduleType, int scheduleId);
        Task<bool> SetParticipantStatus(ScheduleType scheduleType, ScheduleBooking scheduleBooking);
        Task<bool> RemoveParticipantFromSchedule(ScheduleType scheduleType, ScheduleBooking scheduleBooking);
        Task<bool> UpdateScheduleStatus(ScheduleType scheduleType, Schedule schedule);
    }

    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ScheduleRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<ScheduleException> GetException(int id)
        {
            var parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<ScheduleException>(
                "/v1/Scheduling/GetException",
                parameters
            );
        }

        public async Task<TimeSlotConfig> GetTimeSlotConfig(int locationId)
        {
            var parameters = new NameValueCollection
            {
                { "locationId", locationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<TimeSlotConfig>(
                "/v1/Scheduling/GetTimeSlotConfig",
                parameters
            );
        }

        public async Task<int> DeleteException(int id)
        {
            var parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<int>(
                "/v1/Scheduling/DeleteException",
                parameters
            );
        }

        public async Task<ScheduleException> SaveException(ScheduleException scheduleException)
        {
            if (scheduleException == null)
                throw new ArgumentNullException(nameof(scheduleException));

            return await _webServiceHelper.PostAsync<ScheduleException, ScheduleException>(
                "/v1/Scheduling/SetException",
                scheduleException
            );
        }

        public async Task<TimeSlotConfig> SaveTimeSlotConfig(TimeSlotConfig timeSlotConfig)
        {
            if (timeSlotConfig == null)
                throw new ArgumentNullException(nameof(timeSlotConfig));

            return await _webServiceHelper.PostAsync<TimeSlotConfig, TimeSlotConfig>(
                "/v1/Scheduling/SetTimeSlotConfig",
                timeSlotConfig
            );
        }

        public async Task<bool> SetAppointment(Guid applicationId, DateTime date, string timeSlotString)
        {
            string endpoint =
                $"/v1/Scheduling/SetAppointment" +
                $"?applicationId={Uri.EscapeDataString(applicationId.ToString())}" +
                $"&date={Uri.EscapeDataString(date.ToString())}" +
                $"&timeSlotString={Uri.EscapeDataString(timeSlotString ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<object, bool>(
                endpoint,
                null
            );
        }

        public async Task<IList<TimeSlotConfig>> GetTimeSlotConfigs()
        {
            return await _webServiceHelper.GetAsync<IList<TimeSlotConfig>>(
                "/v1/Scheduling/GetAllTimeSlotConfigs"
            );
        }

        public async Task<IList<TimeSlotConfig>> GetTimeSlots()
        {
            return await _webServiceHelper.GetAsync<IList<TimeSlotConfig>>(
                "/v1/Scheduling/GetAllTimeSlotConfigs"
            );
        }

        public async Task<IList<ScheduleException>> GetScheduleExceptions()
        {
            return await _webServiceHelper.GetAsync<IList<ScheduleException>>(
                "/v1/Scheduling/GetAllExceptions"
            );
        }

        public async Task<IList<AppointmentScheduleOption>> GetSlotsForApplication(Guid applicationId, DateTime date)
        {
            var parameters = new NameValueCollection
            {
                { "date", date.ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture) },
                { "applicationId", applicationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<AppointmentScheduleOption>>(
                "/v1/Scheduling/GetSlots",
                parameters
            );
        }

        public async Task<ApplicantAppointment> GetApplicantAppointment(Guid applicantId)
        {
            var parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<ApplicantAppointment>(
                "/v1/Scheduling/GetApplicantAppointment",
                parameters
            );
        }

        public async Task<CsiSchedule> AddOrUpdateCSISchedule(CsiSchedule csiSchedule)
        {
            if (csiSchedule == null)
                throw new ArgumentNullException(nameof(csiSchedule));

            return await _webServiceHelper.PostAsync<CsiSchedule, CsiSchedule>(
                "/v1/Scheduling/AddOrUpdateCSISchedule",
                csiSchedule
            );
        }

        public async Task<CsiSchedule> GetCSISchedule(int id)
        {
            var parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<CsiSchedule>(
                "/v1/Scheduling/GetCSISchedule",
                parameters
            );
        }

        public async Task<IEnumerable<CsiSchedule>> GetCSISchedules(int locationId, int lineOfBusinessId, int statusId = 0, DateTime? startDate = null, DateTime? endDate = null)
        {
            var parameters = new NameValueCollection
            {
                { "locationId", locationId.ToString() },
                { "lineOfBusinessId", lineOfBusinessId.ToString() },
                { "statusId", statusId.ToString() },
                { "startDate", startDate.ToString() },
                { "endDate", endDate.ToString() }
            };

            return await _webServiceHelper.GetAsync<IEnumerable<CsiSchedule>>(
                "/v1/Scheduling/GetCSISchedules",
                parameters
            );
        }

        public async Task<IEnumerable<CsiSchedule>> GetCSISchedulesByRequisitionId(int requisitionId)
        {
            var parameters = new NameValueCollection
            {
                { "requisitionId", requisitionId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IEnumerable<CsiSchedule>>(
                "/v1/Scheduling/GetCSISchedulesByRequisitionId",
                parameters
            );
        }

        public async Task<IList<ScheduleDetails>> GetSchedules(ScheduleType scheduleType, ScheduleFilterArgs filter)
        {
            return await _webServiceHelper.PostAsync<ScheduleFilterArgs, IList<ScheduleDetails>>(
                $"/v1/Scheduler/GetSchedules?scheduleType={Uri.EscapeDataString(scheduleType.ToString())}",
                filter
            );
        }

        public async Task<IList<ScheduleDetails>> GetSchedulesByWave(ScheduleType scheduleType, int requisitionId)
        {
            var parameters = new NameValueCollection
            {
                { "scheduleType", scheduleType.ToString() },
                { "requisitionId", requisitionId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<ScheduleDetails>>(
                "/v1/Scheduler/GetSchedulesByWave",
                parameters
            );
        }

        public async Task<bool> BookApplicant(ScheduleType scheduleType, ScheduleBooking scheduleBooking, int rebookedId = 0)
        {
            if (scheduleBooking == null)
                throw new ArgumentNullException(nameof(scheduleBooking));

            string endpoint =
                $"/v1/Scheduler/BookApplicant" +
                $"?scheduleType={Uri.EscapeDataString(scheduleType.ToString())}" +
                $"&rebookedId={rebookedId}";

            return await _webServiceHelper.PostAsync<ScheduleBooking, bool>(
                endpoint,
                scheduleBooking
            );
        }

        public async Task<bool> AddOrUpdateSchedule(Schedule schedule)
        {
            if (schedule == null)
                throw new ArgumentNullException(nameof(schedule));

            return await _webServiceHelper.PostAsync<Schedule, bool>(
                "/v1/Scheduler/AddOrUpdateSchedule",
                schedule
            );
        }

        public async Task<ScheduleDetails> GetApplicantBooking(ScheduleType scheduleType, Guid applicantId)
        {
            var parameters = new NameValueCollection
            {
                { "scheduleType", scheduleType.ToString() },
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<ScheduleDetails>(
                "/v1/Scheduler/GetApplicantBooking",
                parameters
            );
        }

        public async Task<ScheduleDetails> GetScheduleDetailsById(ScheduleType scheduleType, int id)
        {
            var parameters = new NameValueCollection
            {
                { "scheduleType", scheduleType.ToString() },
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<ScheduleDetails>(
                "/v1/Scheduler/GetScheduleDetailsById",
                parameters
            );
        }

        public async Task<IEnumerable<ScheduleApplicantInfo>> GetApplicantsBySchedule(ScheduleType scheduleType, int scheduleId)
        {
            var parameters = new NameValueCollection
            {
                { "scheduleType", scheduleType.ToString() },
                { "scheduleId", scheduleId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IEnumerable<ScheduleApplicantInfo>>(
                "/v1/Scheduler/GetApplicantsBySchedule",
                parameters
            );
        }

        public async Task<bool> SetParticipantStatus(ScheduleType scheduleType, ScheduleBooking scheduleBooking)
        {
            if (scheduleBooking == null)
                throw new ArgumentNullException(nameof(scheduleBooking));

            string endpoint =
                $"/v1/Scheduler/SetParticipantStatus" +
                $"?scheduleType={Uri.EscapeDataString(scheduleType.ToString())}";

            return await _webServiceHelper.PostAsync<ScheduleBooking, bool>(
                endpoint,
                scheduleBooking
            );
        }

        public async Task<bool> RemoveParticipantFromSchedule(ScheduleType scheduleType, ScheduleBooking scheduleBooking)
        {
            if (scheduleBooking == null)
                throw new ArgumentNullException(nameof(scheduleBooking));

            string endpoint =
                $"/v1/Scheduler/RemoveParticipantFromSchedule" +
                $"?scheduleType={Uri.EscapeDataString(scheduleType.ToString())}";

            return await _webServiceHelper.PostAsync<ScheduleBooking, bool>(
                endpoint,
                scheduleBooking
            );
        }

        public async Task<bool> UpdateScheduleStatus(ScheduleType scheduleType, Schedule schedule)
        {
            if (schedule == null)
                throw new ArgumentNullException(nameof(schedule));

            string endpoint =
                $"/v1/Scheduler/UpdateScheduleStatus" +
                $"?scheduleType={Uri.EscapeDataString(scheduleType.ToString())}";

            return await _webServiceHelper.PostAsync<Schedule, bool>(
                endpoint,
                schedule
            );
        }
    }
}
