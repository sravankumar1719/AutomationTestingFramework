namespace TestAutomationFramework.Components
{
    using OpenQA.Selenium;
    using System;
    using TestAutomationFramework.Utils;
    using TestAutomationFramework.Enum;

    public class ApplicationHeaderComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@class= 'container top-bar-menu-container']");
        private static readonly String HeaderOptionsLocatorLctmask = "//a[text() = '{0}']";

        public IWebElement ContainerComponent => DriverExtensions.WaitForElement(ContainerLocator);

        public T ClickOnHorizontalListProductLink<T>(ApplicationHeaderOptions headerOptions)
        {
            Logger.LogInfo($"Clicking on '{headerOptions.GetEnumValue()}' link displayed in the header section");
            DriverExtensions.WaitForElement(By.XPath(string.Format(HeaderOptionsLocatorLctmask, headerOptions.GetEnumValue()))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        public bool CheckIfHorizontalListProductLinkOptionIsDisplayed(ApplicationHeaderOptions headerOptions)
        {
            Logger.LogInfo($"Checks if '{headerOptions.GetEnumValue()}' link is displayed in the header section");
            return DriverExtensions.IsDisplayed(headerOptions.GetByLocator());
        }

        public string GetHorizontalListProductLinkOptionText(ApplicationHeaderOptions headerOptions)
        {
            string headerOptionText = DriverExtensions.GetText(headerOptions.GetByLocator());
            Logger.LogInfo($"Returns the text displayed for '{headerOptions.GetEnumValue()}' link in the header section : {headerOptionText}");
            return headerOptionText;
        }
    }
}
