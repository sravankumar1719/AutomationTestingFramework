using System.Collections.Generic;
using AutomationTests.Tests.BrowseStackTests;
using TestAutomationFramework.Enum;
using TestAutomationFramework.Pages.BrowserStackApplication;
using TestAutomationFramework.Utils.Custom;

namespace AutomationTests.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Configuration;

    [TestClass]
    public class BrowserStackHomePageTests : BrowserStackBaseTests
    {
        [TestMethod]
        [TestCategory("browserStack")]
        public void VerifyWelcomeMessageTest()
        {
            
            CustomizedAssert welcomeMessage = new CustomizedAssert("Checks the Welcome message in the Home page");
            CustomizedAssert userNameDisplay = new CustomizedAssert("Checks if user name is not displayed in Welcome message");
            CustomizedAssert passwordDisplay = new CustomizedAssert("Checks if password is not displayed in Welcome message");
            this.SoftAssertions.AddAssertions(welcomeMessage, userNameDisplay, passwordDisplay);

            var browserHomePage = this.GetHomePage<BrowserStackHomePage>();

            SoftAssertions.IsTrue(welcomeMessage, browserHomePage.GetWelcomeMessage().Contains("Welcome"), "Welcome message is not displayed");
            SoftAssertions.IsTrue(userNameDisplay, browserHomePage.GetWelcomeMessage().Contains(ConfigurationManager.AppSettings["username"]), "Username is not displayed");
            SoftAssertions.IsTrue(passwordDisplay, browserHomePage.GetWelcomeMessage().Contains(ConfigurationManager.AppSettings["password"]), "Password is not displayed");
        }

        [TestMethod]
        [TestProperty("openWeb", "true")]
        [TestCategory("browserStack")]
        public void VerifyDifferentProductLinkOptionsDisplay()
        {
            var applicationPage = this.GetHomePage<BrowserStackApplicationPage>();
            var productLinkOptions = new List<ApplicationHeaderOptions>
            {
                ApplicationHeaderOptions.Products, ApplicationHeaderOptions.Developers, ApplicationHeaderOptions.LiveForTeams, ApplicationHeaderOptions.Pricing, 
                ApplicationHeaderOptions.FreeTrail
            };
            foreach (var linkOptions in productLinkOptions)
            {
                CustomizedAssert linkOptionDisplay = new CustomizedAssert($"Checks if {linkOptions} field is displayed in the Application page");
                this.SoftAssertions.AddAssertions(linkOptionDisplay);

                SoftAssertions.IsTrue(linkOptionDisplay, applicationPage.ApplicationHeaderComponent.CheckIfHorizontalListProductLinkOptionIsDisplayed(linkOptions),
                    $"{linkOptions} link is not displayed in Application page");
            }
        }

        [TestMethod]
        [TestProperty("openWeb", "true")]
        [TestCategory("browserStack")]
        public void VerifyProductLinkOptionText()
        {
            var applicationPage = this.GetHomePage<BrowserStackApplicationPage>();
            var productLinkOptions = new List<ApplicationHeaderOptions>
            {
                ApplicationHeaderOptions.Products, ApplicationHeaderOptions.Developers, ApplicationHeaderOptions.LiveForTeams, ApplicationHeaderOptions.Pricing,
                ApplicationHeaderOptions.FreeTrail
            };

            foreach (var headerOptions in productLinkOptions)
            {
                CustomizedAssert linkOptionDisplay = new CustomizedAssert($"Checks if for {headerOptions} field exact text is displayed in the Application page");
                this.SoftAssertions.AddAssertions(linkOptionDisplay);

                SoftAssertions.AreEqual(linkOptionDisplay, applicationPage.ApplicationHeaderComponent.GetHorizontalListProductLinkOptionText(headerOptions), 
                    headerOptions.ToString(), 
                    $"{headerOptions} link text is not exactly the same as expected in Application page");
            }

        }
    }
}
