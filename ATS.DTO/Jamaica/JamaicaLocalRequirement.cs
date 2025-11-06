using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Jamaica
{
    public class JamaicaLocalRequirement
    {
        public int Id { get; set; }
        public Guid Applicant_Id { get; set; }
        public string TRN { get; set; }
        public string NIS { get; set; }
        public string IdType { get; set; }
        public string IdNumber { get; set; }
    }
}
