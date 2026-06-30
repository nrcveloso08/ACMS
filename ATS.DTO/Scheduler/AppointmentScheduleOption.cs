using System;

namespace ATS.DTO.Scheduler
{
    public class AppointmentScheduleOption
    {
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public string TimeSlotString { get; set; }
        public int AvailableSlots { get; set; }
    }
}
