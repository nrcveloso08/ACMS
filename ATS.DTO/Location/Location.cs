using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Location
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string AlternateSystemLocation { get; set; }

        public string TimeZoneId { get; set; }
        public string Country { get; set; }

        public TimeZoneInfo TimeZone
        {
            get { return TimeZoneInfo.FindSystemTimeZoneById(this.TimeZoneId); }
        }

        public int LocationGroupId { get; set; }

        public string LocationGroupName { get; set; }
        public int GeoLocation_Id { get; set; }
        public string GeoLocationName { get; set; }
        public string PhoneMaskingFormat { get; set; }
        public string InternationalCode { get; set; }

    }
}
