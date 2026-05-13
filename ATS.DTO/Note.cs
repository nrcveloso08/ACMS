using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO
{
    public class Note
    {
        public int Id { get; set; }
        public Guid ApplicationId { get; set; }
        public string Memo { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
