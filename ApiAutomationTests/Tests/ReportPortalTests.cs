namespace ApiAutomationTests.Tests
{
    using System.Collections.Generic;

    using ApiAutomationTests.Utils;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json;

    [TestClass]
    public class ReportPortalTests
    {
        private ExcelFileWriter excelFileWriter = ExcelFileWriter.GetInstance();

        [TestMethod]
        public void VerifySuiteDetails()
        {

            var request = new ReportPortalClient();
            var suiteSpecificDetails = request.GetSuiteSpecificDetails("Gold_Report", "Jun", "13");

            Assert.AreEqual(200, suiteSpecificDetails.ResponseStatus, "Unable to retrieve the results via API call");

            var suiteLaunches = JsonConvert.DeserializeObject<SuiteLaunches>(suiteSpecificDetails.Content);

            excelFileWriter.SaveExcelFile(suiteLaunches.Launches);
        }

        [TestMethod]
        public void PostSuiteTagTest()
        {
            var request = new ReportPortalClient();
            request.UpdateSuiteSpecificBestTag("GlobalSearch", "Jun", "14");
        }

        [TestMethod]
        public void UpdateBestTagForSpecificSuiteOwner()
        {
            var request = new ReportPortalClient();
            List<ReportPortalModel> updatedBestSuitesList = request.UpdateOwnerSpecificSuitesBestTag("Sravan_Grandhi", "Jun", "29");
            foreach (var bestLaunch in updatedBestSuitesList)
            {
                Assert.AreEqual("Best", request.GetResultTagForSpecificLaunchId(bestLaunch.Id));
            }
        }
    }
}
