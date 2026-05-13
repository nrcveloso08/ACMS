using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Front
{
    public class RelatedConversations
    {
        [JsonProperty("_results")]
        public IList<MessageDetails> MessageDetails { get; set; }
    }
}
