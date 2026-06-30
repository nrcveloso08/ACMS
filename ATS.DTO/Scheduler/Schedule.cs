using System;
using System.Collections.Generic;

namespace ATS.DTO.Scheduler
{
    public enum ScheduleType
    {
        Interview = 1,
        CSI = 2,
        Medical = 3
    }

    public class Schedule
    {
        public int Id { get; set; }
        public ScheduleType ScheduleType { get; set; }
        public int LocationId { get; set; }
        public int LineOfBusinessId { get; set; }
        public int RequisitionId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int Capacity { get; set; }
        public int StatusId { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ScheduleFilterArgs
    {
        public IList<int> LocationIds { get; set; }
        public int LineOfBusinessId { get; set; }
        public int StatusId { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
    }

    public class ScheduleDetails
    {
        public int Id { get; set; }
        public ScheduleType ScheduleType { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int LineOfBusinessId { get; set; }
        public int RequisitionId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int Capacity { get; set; }
        public int BookedCount { get; set; }
        public int StatusId { get; set; }
        public string StatusDisplay { get; set; }
    }

    public class ScheduleBooking
    {
        public int ScheduleId { get; set; }
        public Guid ApplicantId { get; set; }
        public Guid ApplicationId { get; set; }
        public int StatusId { get; set; }
        public string Notes { get; set; }
    }

    public class ScheduleApplicantInfo
    {
        public Guid ApplicantId { get; set; }
        public Guid ApplicationId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int StatusId { get; set; }
        public string StatusDisplay { get; set; }
    }
}
