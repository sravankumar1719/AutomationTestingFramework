using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAutomationFramework;
using TestAutomationFramework.Enum;
using TestAutomationFramework.Pages.BrowserStackApplication;

namespace AutomationTests.Tests.BrowseStackTests
{
    public class BrowserStackBaseTests : BaseTest
    {
        public BrowserStackBaseTests()
        {
            ApplicationUrl = ConfigurationManager.AppSettings["browserStackApplicationUrl"];
        }

        public void LoginToTheApplication()
        {
            var browserStackApplicationPage = this.GetHomePage<BrowserStackApplicationPage>();
            var browserStackLoginPage = browserStackApplicationPage.ApplicationHeaderComponent.ClickOnHorizontalListProductLink<BrowserStackLoginPage>(ApplicationHeaderOptions.SignIn);
            browserStackLoginPage.EnterEmailId(ConfigurationManager.AppSettings["username"]);
            browserStackLoginPage.EnterEmailPassword(ConfigurationManager.AppSettings["password"]);
            browserStackLoginPage.ClickOnSignMeIn();
            DriverExtensions.WaitForPageLoad();
        }

        [TestInitialize]
        public override void InitSetUp()
        {
            base.InitSetUp();
            if (string.IsNullOrEmpty((string)TestContext.Properties["openWeb"]))
            {
                LoginToTheApplication();
            }
        }

        public void SignOffTheApplication()
        {
            var homePage = this.GetHomePage<BrowserStackHomePage>();
            if (homePage.HeaderComponent.IsDisplayed())
            {
                homePage.HeaderComponent.ExpandUserDropdown();
                homePage.HeaderComponent.ClickOnSignOutLink();
            }
        }

        [TestCleanup]
        public override void TestCaseCleanUp()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                ScreenShotTaker.TakeScreenShot();
            }
            this.SignOffTheApplication();
            base.TestCaseCleanUp();
        }
    }
}
