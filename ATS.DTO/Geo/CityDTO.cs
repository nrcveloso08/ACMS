using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Geo
{
    public class CityDTO
    {
        public int Id { get; set; }

        public int StateId { get; set; }

        public int CountryId { get; set; }

        public string Name { get; set; }
    }
}
