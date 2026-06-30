using System.Collections.Generic;

namespace ATS.DTO.Scheduler
{
    public class TimeSlotConfig
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int IntervalMinutes { get; set; }
        public int MaxPerSlot { get; set; }
        public IList<TimeSlotSegment> Segments { get; set; }
    }
}
