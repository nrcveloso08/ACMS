using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Bogota
{
    public class BogotaLocalRequirement
    {
        public int Id { get; set; }
        [ForeignKey("Applicant")]
        public Guid Applicant_Id { get; set; }        
        [MaxLength(100)]
        public string EPS { get; set; }
        [MaxLength(100)]
        public string Pensiones { get; set; }
        [MaxLength(100)]
        public string Cedula { get; set; }
        [MaxLength(100)]
        public string Cesantia { get; set; }
    }
}
