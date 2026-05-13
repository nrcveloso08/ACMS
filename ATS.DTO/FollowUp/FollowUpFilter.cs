using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.FollowUp
{
    public class FollowUpFilter 
    {
        public int LocationId { get; set; }
        public IList<int> LineOfBusinessIds { get; set; }
        public IList<int> RequisitionIds { get; set; }
        public IList<FollowUpStatus> Statuses { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
