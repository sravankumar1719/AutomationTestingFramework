using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestAutomationFramework.Utils.Custom;

namespace SpecFlowAutomationTests.Utils
{
    public class SoftAssertions
    {
        private ScreenShotTaker _screenShotTaker;

        public List<CustomizedAssert> Assertions;

        protected ScreenShotTaker ScreenShotTaker
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

        public SoftAssertions()
        {
            Assertions = new List<CustomizedAssert>();
        }

        public void AreEqual<T>(CustomizedAssert assert, T expected, T actual, string failureMessage)
        {
            CustomizedAssert currentAssert = Assertions.Find(customizedAssert => customizedAssert.ExpectedConditionMessage.Equals(assert.ExpectedConditionMessage));
            var finalMessage = new StringBuilder();
            finalMessage.AppendLine(failureMessage);
            finalMessage.AppendLine($"ExpectedResult -> {expected} and Actual result -> {actual}");

            currentAssert.FailureMessage = finalMessage.ToString();
            try
            {
                Assert.AreEqual(expected, actual, failureMessage);
                currentAssert.Outcome = Outcome.Passed;
            }
            catch (AssertionException ex)
            {
                currentAssert.Outcome = Outcome.Failed;
                ScreenShotTaker.TakeScreenShotForFailedAssertions(assert);
            }
        }

        public void IsTrue(CustomizedAssert assert, bool expected, string failureMessage)
        {
            CustomizedAssert currentAssert = Assertions.Find(customizedAssert => customizedAssert.ExpectedConditionMessage.Equals(assert.ExpectedConditionMessage));
            currentAssert.FailureMessage = failureMessage;
            try
            {
                Assert.IsTrue(expected, failureMessage);
                currentAssert.Outcome = Outcome.Passed;
            }
            catch (AssertionException ex)
            {
                currentAssert.Outcome = Outcome.Failed;
                ScreenShotTaker.TakeScreenShotForFailedAssertions(assert);
            }
        }

        public void IsFalse(CustomizedAssert assert, bool expected, string failureMessage)
        {
            CustomizedAssert currentAssert = Assertions.Find(customizedAssert => customizedAssert.ExpectedConditionMessage.Equals(assert.ExpectedConditionMessage));
            currentAssert.FailureMessage = failureMessage;
            try
            {
                Assert.IsFalse(expected, failureMessage);
                currentAssert.Outcome = Outcome.Passed;
            }
            catch (AssertionException ex)
            {
                currentAssert.Outcome = Outcome.Failed;
                ScreenShotTaker.TakeScreenShotForFailedAssertions(assert);
            }
        }

        public void AreNotEqual<T>(CustomizedAssert assert, T expected, T actual, string failureMessage)
        {
            CustomizedAssert currentAssert = Assertions.Find(customizedAssert => customizedAssert.ExpectedConditionMessage.Equals(assert.ExpectedConditionMessage));
            var finalMessage = new StringBuilder();
            finalMessage.AppendLine(failureMessage);
            finalMessage.AppendLine($"ExpectedResult -> {expected} and Actual result -> actual");

            currentAssert.FailureMessage = finalMessage.ToString();
            try
            {
                Assert.AreNotEqual(expected, actual, failureMessage);
                currentAssert.Outcome = Outcome.Passed;
            }
            catch (AssertionException ex)
            {
                currentAssert.Outcome = Outcome.Failed;
                ScreenShotTaker.TakeScreenShotForFailedAssertions(assert);
            }
        }

        public void AddAssertions(params CustomizedAssert[] customizedAsserts)
        {
            foreach (var assertion in customizedAsserts)
            {
                if (!Assertions.Any(assert => assert.ExpectedConditionMessage.Equals(assertion.ExpectedConditionMessage)))
                {
                    Assertions.Add(assertion);
                }
            }
        }
    }
}
