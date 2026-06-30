using System.Collections.Generic;

namespace ATS.DTO.Medical
{
    public class MedicalResultSearchResponseDto
    {
        public IList<MedicalResultDto> Items { get; set; } = new List<MedicalResultDto>();
        public int TotalCount { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
