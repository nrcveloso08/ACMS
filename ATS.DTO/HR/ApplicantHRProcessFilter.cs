using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.HR
{
    public class ApplicantHRProcessFilter
    {
        public IList<int> LocationIds { get; set; }
        public IList<HRStatus> HRStatuses { get; set; }
        public IList<int> RequisitionIds { get; set; }
        public bool IncludeOnHold { get; set; } = false;
        public PaginationFilters PaginationFilters { get; set; }
    }
}
