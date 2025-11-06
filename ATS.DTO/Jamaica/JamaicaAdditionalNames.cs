using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Jamaica
{
    public class JamaicaAdditionalNames
    {
        public int Id { get; set; }
        public Guid Applicant_Id { get; set; }
        public string MiddleName { get; set; }
        public string SecondLastName { get; set; }
        public string MarriedSurName { get; set; }
    }
}
