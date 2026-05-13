using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Front
{
    public class Attachments
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("filename")]
        public string FileName { get; set; }
    }
}
