using System;

namespace ATS.DTO.Reporting
{
    public class GlobalRecruitingPipeline
    {
        public DateTime ExpandedDate { get; set; }
        public int AllTime { get; set; }
        public int LessThanSevenDays { get; set; }
        public int LessThanFourteenDays { get; set; }
        public int Yesterday { get; set; }
        public int ScheduledInterviews { get; set; }
        public string LocationName { get; set; }
        public string LocationSubGroup { get; set; }
    }

    public class GlobalRecruitingPipelineFilters
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string LocationIds { get; set; }
    }
}
