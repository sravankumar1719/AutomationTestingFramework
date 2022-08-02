using System.Collections.Generic;

namespace ApiAutomationTests
{
    public class ReportPortalDefects
    {
        public Dictionary<string, string> product_bug { get; set; }
        public Dictionary<string, string> automation_bug { get; set; }
        public Dictionary<string, string> system_issue { get; set; }
        public Dictionary<string, string> to_investigate { get; set; }
    }
}