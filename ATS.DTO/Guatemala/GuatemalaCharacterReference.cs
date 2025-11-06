using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Guatemala
{
    public class GuatemalaCharacterReference
    {
        public int Id { get; set; }
        public Guid Applicant_Id { get; set; }
        public string FullName { get; set; }
        public int SequenceNo { get; set; }
        public string PhoneNumber { get; set; }
        public string Relationship { get; set; }
    }
}
