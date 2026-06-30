using System;

namespace ATS.DTO.Scheduler
{
    public class ApplicantAppointment
    {
        public Guid ApplicantId { get; set; }
        public Guid ApplicationId { get; set; }
        public DateTimeOffset? AppointmentDate { get; set; }
        public string TimeSlotString { get; set; }
        public int LocationId { get; set; }
    }
}
