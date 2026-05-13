using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Front
{
    public class MessageDetails
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("created_at")]
        public double TimeCreatedInSeconds { get; set; }
        public string CreatedDate { get => new DateTime(1970, 1, 1).AddSeconds(this.TimeCreatedInSeconds).ToString("yyyy-MM-dd hh:mm tt"); }
        [JsonProperty("recipients")]
        public IList<RecipientDetails> Recipients { get; set; }
        [JsonProperty("attachments")]
        public IList<Attachments> Attachments { get; set; }
    }
}
