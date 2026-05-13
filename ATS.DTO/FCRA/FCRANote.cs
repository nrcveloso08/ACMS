using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.FCRA
{
    public class FCRANote
    {
        public string Memo { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
