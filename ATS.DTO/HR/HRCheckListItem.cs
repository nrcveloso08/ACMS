using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.HR
{
    public class HRCheckListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<HRChecklistOption> HRChecklistOptions { get; set; }
        public HRCheckResponse HRCheckResponse { get; set; }
    }
}
