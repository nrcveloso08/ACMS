using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.HR
{
    public class HRChecklist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public ICollection<HRCheckListItem> HRCheckItems { get; set; }
    }
}
