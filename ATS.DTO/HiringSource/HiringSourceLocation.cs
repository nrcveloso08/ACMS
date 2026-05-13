using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.HiringSource
{
    public class HiringSourceLocation
    {
        public int Id { get; set; }
        public int Location_Id { get; set; }
        [ForeignKey("HiringSource")]
        public int HiringSource_Id { get; set; }
        public HiringSource HiringSource { get; protected set; }
    }
}
