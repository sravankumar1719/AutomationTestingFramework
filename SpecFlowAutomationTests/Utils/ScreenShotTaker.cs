using System;
using System.IO;
using OpenQA.Selenium;
//using ReportPortal.Log4Net;
using TechTalk.SpecFlow;
using TestAutomationFramework;
using TestAutomationFramework.Utils;
using TestAutomationFramework.Utils.Custom;

namespace SpecFlowAutomationTests.Utils
{
    public class ScreenShotTaker
    {
        public void TakeScreenShot()
        {
            string fileName = $"{ScenarioContext.Current.ScenarioInfo.Title}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName); 
            DriverExtensions.GetScreenShot().SaveAsFile(filePath, ScreenshotImageFormat.Png); 
            Console.WriteLine("Screenshot : {0}", new Uri(filePath));
        }

        public void TakeScreenShotForFailedAssertions(CustomizedAssert assert)
        {
            string fileName = $"{assert.ExpectedConditionMessage}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName); 
            DriverExtensions.GetScreenShot().SaveAsFile(filePath, ScreenshotImageFormat.Png); 
            Console.WriteLine("Screenshot : {0}", new Uri(filePath));
            Logger.LogInfo(filePath);
        }
    }
}
