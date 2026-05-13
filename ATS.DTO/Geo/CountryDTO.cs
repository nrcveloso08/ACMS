using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Geo
{
    public class CountryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Nationality { get; set; }

        public string Iso2 { get; set; }

        public string Iso3 { get; set; }

        public string PhoneCode { get; set; }
    }
}
