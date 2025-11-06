using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service.ViewModels.Location
{
    public class LocationVM
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        [MaxLength(50)]
        public string TimeZoneId { get; set; } = "";

        [MaxLength(50)]
        public string Country { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

    }
}
