using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.HR
{
    public class HRChecklistOption
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int HRChecklistItemId { get; set; }
    }
}
