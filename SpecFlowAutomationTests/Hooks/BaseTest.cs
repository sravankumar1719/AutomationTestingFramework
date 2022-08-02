using NUnit.Framework;
using SpecFlowAutomationTests.Utils;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TestAutomationFramework;
using TestAutomationFramework.Pages;
using TestAutomationFramework.Utils.Custom;

namespace SpecFlowAutomationTests.Hooks
{
    public class BaseTest
    {
        public string ApplicationUrl;

        private SoftAssertions _softAssertions;

        private ScreenShotTaker _screenShotTaker;

        public ScreenShotTaker ScreenShotTaker
        {
            get
            {
                this._screenShotTaker = this._screenShotTaker ?? new ScreenShotTaker();
                return this._screenShotTaker;
            }

            set
            {
                this._screenShotTaker = value;
            }
        }


        public SoftAssertions SoftAssertions
        {
            get
            {
                this._softAssertions = this._softAssertions ?? new SoftAssertions();
                return this._softAssertions;
            }

            set
            {
                this._softAssertions = value;
            }
        }

        public T GetHomePage<T>() where T : BasePage
        {
            return DriverExtensions.CreatePageInstance<T>();
        }

        [BeforeScenario("automation")]
        public void InitSetUp()
        {
            DriverExtensions.NavigateToTheApplication("https://www.globalsqa.com/angularJs-protractor/BankingProject/#/login"); //(ConfigurationManager.AppSettings["bankingApplicationUrl"]);
            DriverExtensions.WaitForPageLoad();
        }

        [AfterScenario("automation")]
        public void TestCaseCleanUp()
        {
            try
            {
                if (ScenarioContext.Current.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
                {
                    ScreenShotTaker.TakeScreenShot();
                }

                if (SoftAssertions.Assertions.Any(x => x.Outcome.Equals(Outcome.Failed) || x.Outcome.Equals(Outcome.Inconclusive)))
                {
                    var finalMessage = new StringBuilder();
                    var failedAssertions = SoftAssertions.Assertions.FindAll(x => !x.Outcome.Equals(Outcome.Passed));
                    foreach (var failedAssert in failedAssertions)
                    {
                        finalMessage.AppendLine(failedAssert.ToString());
                    }

                    if (failedAssertions.Any(x => x.Outcome.Equals(Outcome.Failed)))
                    {
                        Assert.Fail(finalMessage.ToString());
                    }
                    Assert.Inconclusive(finalMessage.ToString());
                }
            }
            finally
            {
                DriverExtensions.CloseBrowser();
            }
        }
    }
}
