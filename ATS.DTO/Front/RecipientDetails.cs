using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Front
{
    public class RecipientDetails
    {
        [JsonProperty("handle")]
        public string Handle { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
