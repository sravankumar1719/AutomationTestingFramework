using System.Collections.Generic;
using Newtonsoft.Json;

namespace ApiAutomationTests
{
    public class ReportPortalModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "statistics")]
        public ReportPortalStatistics Statistics { get; set; }

        [JsonProperty(PropertyName = "attributes")]
        public List<Dictionary<string, string>> Attributes { get; set; }
    }

    public class SuiteLaunches
    {
        [JsonProperty(PropertyName = "content")]
        public List<ReportPortalModel> Launches { get; set; }
    }
}