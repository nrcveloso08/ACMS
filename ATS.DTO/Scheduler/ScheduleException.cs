using System;
using System.Collections.Generic;

namespace ATS.DTO.Scheduler
{
    public class ScheduleException
    {
        public ScheduleException()
        {
            OverrideBehavior = OverrideBehavior.ObserveConstraints;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public IList<int> LocationIds { get; set; }
        public IList<TimeSlotSegment> Segments { get; set; }
        public ScheduleExceptionType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public OverrideBehavior OverrideBehavior { get; set; }
    }

    public enum ScheduleExceptionType
    {
        AllowBooking,
        DisallowBooking
    }

    public enum OverrideBehavior
    {
        ForceExceptionType,
        ObserveConstraints
    }
}
