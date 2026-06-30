using System.Collections.Generic;

namespace ATS.DTO.Medical
{
    public class MedicalResultDropdownsDto
    {
        public IList<MedicalResultOptionDto> Statuses { get; set; } = new List<MedicalResultOptionDto>();
    }
}
