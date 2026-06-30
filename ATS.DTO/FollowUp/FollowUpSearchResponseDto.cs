using System.Collections.Generic;

namespace ATS.DTO.FollowUp
{
    public class FollowUpSearchResponseDto
    {
        public IList<FollowUpResultDto> Items { get; set; } = new List<FollowUpResultDto>();
        public int TotalCount { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
