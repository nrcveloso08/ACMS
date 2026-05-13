using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Geo
{
    public class StateDTO
    {
        public int Id { get; set; }

        public int CountryId { get; set; }

        public string Name { get; set; }

        public string StateCode { get; set; }
    }
}
