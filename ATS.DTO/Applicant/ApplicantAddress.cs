using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Applicant
{
    public class ApplicantAddress
    {
        public int Id { get; set; }
        public Guid ApplicantId { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ApartmentNo { get; set; }
        public string PostCode { get; set; }
        public string State { get; set; }
    }
}
