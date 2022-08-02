using OpenQA.Selenium;
using TestAutomationFramework.Pages.BrowserStackApplication;
using TestAutomationFramework.Utils;

namespace TestAutomationFramework.Components
{
    public class HomePageHeaderComponent
    {
        private static readonly By ContainerComponentLocator = By.XPath("//header[@class = 'header-habitat']");
        private static readonly By UserDropdownLocator = By.Id("account-menu-toggle");
        private static readonly By SignOutLinkLocator = By.Id("sign_out_link");

        public IWebElement ContainerComponent => DriverExtensions.WaitForElement(ContainerComponentLocator);

        public void ExpandUserDropdown()
        {
            Logger.LogInfo("Expanding the user dropdown");
            DriverExtensions.HoverTheElement(ContainerComponent, UserDropdownLocator);
        }

        public BrowserStackApplicationPage ClickOnSignOutLink()
        {
            Logger.LogInfo("Clicks on Sign out link");
            DriverExtensions.WaitForElement(ContainerComponent, SignOutLinkLocator).Click();
            DriverExtensions.WaitForPageLoad();
            return DriverExtensions.CreatePageInstance<BrowserStackApplicationPage>();
        }

        public bool IsDisplayed() => DriverExtensions.IsDisplayed(ContainerComponentLocator);
    }
}
