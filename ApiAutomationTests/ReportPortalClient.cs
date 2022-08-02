namespace ApiAutomationTests
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using ApiAutomationTests.Utils;

    using Newtonsoft.Json;

    using RestSharp;

    public class ReportPortalClient
    {
        /// <summary>
        /// Gets or sets the rest client.
        /// </summary>
        private RestClient RestClient { get; set; }

        /// <summary>
        /// Gets or sets the rest request.
        /// </summary>
        private RestRequest RestRequest { get; set; }

        private ExcelFileWriter excelFileWriter = ExcelFileWriter.GetInstance();

        public ReportPortalClient()
        {
            var baseUrl = ConfigurationManager.AppSettings["ReportPortalLink"];
            var token = ConfigurationManager.AppSettings["AuthenticationToken"];

            this.RestClient = new RestClient(baseUrl);
            this.RestRequest = new RestRequest(string.Empty);
            RestRequest.AddHeader("Authorization", "Bearer " + token);
        }

        public RestResponse GetSuiteSpecificDetails(string filterName, string month, string day)
        {
            string suiteFilterUrl = "?page.page=1&amp;page.size=50&amp;page.sort=startTime%2Cnumber%2CDESC&filter.cnt.name={0}&filter.has.compositeAttribute={1}%2C{2}";
            suiteFilterUrl = string.Format(suiteFilterUrl, filterName, month, day);
            this.RestRequest.Resource = suiteFilterUrl;
            RestRequest.Method = Method.Get;
            return this.RestClient.Execute(RestRequest);
        }

        public void UpdateSuiteSpecificBestTag(string filterName, string month, string day)
        {
            RestResponse suiteSpecificDetails = GetSuiteSpecificDetails(filterName, month, day);

            var suiteLaunches = JsonConvert.DeserializeObject<SuiteLaunches>(suiteSpecificDetails.Content);
            var bestSuites = GetBestSuiteLaunches(suiteLaunches.Launches, month, day);

            RestRequest restRequest = RestRequest;

            foreach (var suite in bestSuites)
            {
                restRequest.Resource = $"{suite.Id}/update";
                restRequest.Method = Method.Put;

                var resultTag = suite.Attributes.Find(attributes => attributes.ContainsValue("Result"));
                resultTag["value"] = "Best";
                restRequest.AddJsonBody(suite);

                restRequest.AddParameter("application/json", suite, ParameterType.RequestBody);

                RestClient.Execute(restRequest);
            }
        }

        public List<ReportPortalModel> UpdateOwnerSpecificSuitesBestTag(string ownerName, string month, string day)
        {
            string suiteFilterUrl = "?page.page=1&amp;page.size=300&amp;page.sort=startTime%2Cnumber%2CDESC&filter.has.compositeAttribute={0}%2C{1}%2C{2}%2CObsolete";
            suiteFilterUrl = string.Format(suiteFilterUrl, ownerName, month, day);
            RestRequest restRequest = RestRequest; 
            restRequest.Resource = suiteFilterUrl;
            restRequest.Method = Method.Get;

            var ownertaggedSuiteLaunches = this.RestClient.Execute(restRequest);
            var suiteLaunches = JsonConvert.DeserializeObject<SuiteLaunches>(ownertaggedSuiteLaunches.Content).Launches;
            
            var bestSuites = GetBestSuiteLaunches(suiteLaunches, month, day);
            excelFileWriter.SaveExcelFile(bestSuites);

            foreach (var suite in bestSuites)
            {
                restRequest.Resource = $"{suite.Id}/update";
                restRequest.Method = Method.Put;

                var resultTag = suite.Attributes.Find(attributes => attributes.ContainsValue("Result"));
                resultTag["value"] = "Best";
                restRequest.AddJsonBody(suite);

                restRequest.AddParameter("application/json", suite, ParameterType.RequestBody);

                RestClient.Execute(restRequest);
            }

            return bestSuites;
        }

        public List<ReportPortalModel> GetBestTaggedLaunchesForSpecificDay(string month, string day)
        {
            string suiteFilterUrl = "?page.page=1&amp;page.size=300&amp;page.sort=startTime%2Cnumber%2CDESC&filter.has.compositeAttribute=Sravan_Grandhi%2C{0}%2C{1}%2CBest";
            suiteFilterUrl = string.Format(suiteFilterUrl, month, day);
            this.RestRequest.Resource = suiteFilterUrl;
            RestRequest.Method = Method.Get;
            var bestTaggedLaunches = this.RestClient.Execute(RestRequest);
            return JsonConvert.DeserializeObject<SuiteLaunches>(bestTaggedLaunches.Content).Launches;
        }

        public string GetResultTagForSpecificLaunchId(string launchId)
        {
            var restRequest = new RestRequest(string.Empty);
            var token = ConfigurationManager.AppSettings["AuthenticationToken"];
            restRequest.AddHeader("Authorization", "Bearer " + token);
            string launchIdUrl = "?filter.eq.id={0}";
            launchIdUrl = string.Format(launchIdUrl, launchId);
            restRequest.Resource = launchIdUrl;
            restRequest.Method = Method.Get;
            var bestTaggedLaunches = this.RestClient.Execute(restRequest);
            var suiteDetails = JsonConvert.DeserializeObject<SuiteLaunches>(bestTaggedLaunches.Content).Launches;
            return suiteDetails[0].Attributes.Find(attributes => attributes.ContainsValue("Result"))["value"];
        }

        private List<ReportPortalModel> GetBestSuiteLaunches(List<ReportPortalModel> suiteLaunches, string month, string day)
        {
            List<ReportPortalModel> bestSuitesDetails = new List<ReportPortalModel>();
            suiteLaunches = suiteLaunches.FindAll(obsoleteLaunches =>
                obsoleteLaunches.Attributes.Find(attribute => attribute.ContainsValue("Result"))
                    .ContainsValue("Obsolete"));

            suiteLaunches = UpdateEnvironmentInSuiteLaunchName(suiteLaunches);

            var bestTaggedSuiteLaunches = UpdateEnvironmentInSuiteLaunchName(GetBestTaggedLaunchesForSpecificDay(month, day));

            foreach (var suites in suiteLaunches)
            {
                if (bestTaggedSuiteLaunches.Any(suite => suite.Name.Equals(suites.Name)))
                {
                    continue;
                }

                if (!bestSuitesDetails.Any(suiteDetails => suiteDetails.Name.Equals(suites.Name)))
                {
                    var resultTag = suites.Attributes.Find(attributes => attributes.ContainsValue("Result"));
                    resultTag["value"] = "Obsolete -> Best";
                    bestSuitesDetails.Add(suites);
                }
                else
                {
                    var bestSuite = bestSuitesDetails.Find(suiteDetails => suiteDetails.Name.Contains(suites.Name));
                    if (bestSuite.Statistics.ExecutionStatus.Passed > suites.Statistics.ExecutionStatus.Passed)
                    {
                        continue;
                    }

                    var resultTag = suites.Attributes.Find(attributes => attributes.ContainsValue("Result"));
                    resultTag["value"] = "Obsolete -> Best";
                    bestSuitesDetails.Remove(bestSuite);
                    bestSuitesDetails.Add(suites);
                }

            }

            excelFileWriter.SaveExcelFile(bestTaggedSuiteLaunches);

            return bestSuitesDetails;
        }

        private List<ReportPortalModel> UpdateEnvironmentInSuiteLaunchName(List<ReportPortalModel> suiteLaunches)
        {
            foreach (var launch in suiteLaunches)
            {
                launch.Name = launch.Name.Replace("DEMOB", "DEMO").Replace("DEMOPC1", "DEMO").Replace("QEDA", "QED")
                    .Replace("QEDB", "QED");
            }

            return suiteLaunches;
        }
    }
}