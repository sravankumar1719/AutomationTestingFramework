using Newtonsoft.Json;

namespace ApiAutomationTests
{
    public class ExecutionStatus
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "failed")]
        public int Failed { get; set; }

        [JsonProperty(PropertyName = "passed")]
        public int Passed { get; set; }
    }
}
