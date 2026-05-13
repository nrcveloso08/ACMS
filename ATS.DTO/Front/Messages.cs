using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Front
{
    public class Messages
    {
        public IList<Message> MessageList { get; set; }
    }

    public class Message
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public MessageLinks MessageLinks { get; set; }
        public double TimeCreatedInSeconds { get; set; }
        public string CreatedDate { get => new DateTime(1970, 1, 1).AddSeconds(this.TimeCreatedInSeconds).ToString("yyyy-MM-dd hh:mm tt"); }
    }

    public class MessageLinks
    {
        [JsonProperty("related")]
        public RelatedLinks RelatedLinks { get; set; }
    }

    public class RelatedLinks
    {
        [JsonProperty("last_message")]
        public string LastMessage { get; set; }
    }

}
