using ATS.DTO.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Harver
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class HarverVacancyList
    {
        [JsonProperty("data")]
        public List<Vacancy> vacancies { get; set; }
    }

    [JsonConverter(typeof(JsonPathConverter))]
    public class Vacancy
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("attributes.name")]
        public string Name { get; set; }
    }
}
