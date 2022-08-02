using Newtonsoft.Json;

namespace ApiAutomationTests
{
    public class ReportPortalStatistics
    {
        [JsonProperty(PropertyName = "executions")]
        public ExecutionStatus ExecutionStatus { get; set; }

        [JsonProperty(PropertyName = "defects")]
        public ReportPortalDefects Defects { get; set; }
    }
}