namespace AutomationTests.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Text;
    using TestAutomationFramework;
    using TestAutomationFramework.Pages;
    using TestAutomationFramework.Utils;
    using TestAutomationFramework.Utils.Custom;

    public class BaseTest
    {
        public TestContext TestContext { get; set; }

        public string ApplicationUrl;

        private SoftAssertions _softAssertions;

        private ScreenShotTaker _screenShotTaker;

        public ScreenShotTaker ScreenShotTaker
        {
            get
            {
                this._screenShotTaker = this._screenShotTaker ?? new ScreenShotTaker(TestContext);
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
                this._softAssertions = this._softAssertions ?? new SoftAssertions(TestContext);
                return this._softAssertions;
            }

            set
            {
                this._softAssertions = value;
            }
        }

        [TestInitialize]
        public virtual void InitSetUp()
        {
            DriverExtensions.NavigateToTheApplication(ApplicationUrl);
            DriverExtensions.WaitForPageLoad();
        }

        public T GetHomePage<T>() where T : BasePage
        {
            return DriverExtensions.CreatePageInstance<T>();
        }

        [TestCleanup]
        public virtual void TestCaseCleanUp()
        {
            try
            {
                if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
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