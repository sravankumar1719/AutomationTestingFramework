using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAutomationFramework.Pages.BankingApplication;
using TestAutomationFramework.Utils.Custom;

namespace AutomationTests.Tests.BankingTests
{
    [TestClass]
    public class BankingBaseTests : BaseTest
    {
        public BankingBaseTests()
        {
            ApplicationUrl = ConfigurationManager.AppSettings["bankingApplicationUrl"];
        }

        [TestMethod]
        [DeploymentItem(@"Resource\BankingProjectDetails.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
             "|DataDirectory|\\BankingProjectDetails.csv", "BankingProjectDetails#csv", DataAccessMethod.Sequential)]
        public void VerifyUserDetails()
        {
            CustomizedAssert holderNameCheck = new CustomizedAssert("Check whether the bank holder name is displayed");
            CustomizedAssert accountNumberCheck = new CustomizedAssert("Check whether the account number is displayed");
            CustomizedAssert balanceCheck = new CustomizedAssert("Check whether the balance is displayed");
            CustomizedAssert currencyCheck = new CustomizedAssert("Check whether the currency is displayed");

            SoftAssertions.AddAssertions(holderNameCheck, accountNumberCheck, balanceCheck, currencyCheck);

            string holderName = this.TestContext.DataRow["holderName"].ToString().Trim();
            string accountNumber = this.TestContext.DataRow["accountNumber"].ToString();
            string balance = this.TestContext.DataRow["balance"].ToString();
            string currency = this.TestContext.DataRow["currency"].ToString();

            var bankingHomePage = this.GetHomePage<BankingHomePage>();
            bankingHomePage.SelectUserName(holderName);
            var bankingUserHomePage = bankingHomePage.ClickOnLoginButton();

            SoftAssertions.IsTrue(holderNameCheck, bankingUserHomePage.IsUserNameDisplayed(holderName), "Username is not displayed in user home page");
            var userDetails = bankingUserHomePage.GetUserDetails();
            SoftAssertions.AreEqual(accountNumberCheck, accountNumber, userDetails.AccountNumber, "User Account number is not matching");
            SoftAssertions.AreEqual(balanceCheck, balance, userDetails.Balance, "User Balance is not matching");
            SoftAssertions.AreEqual(currencyCheck, currency, userDetails.Currency, "User Currency details is not matching");
        }

        [TestInitialize]
        public override void InitSetUp()
        {
            base.InitSetUp();
            this.GetHomePage<BankingHomePage>().ClickOnCustomerLoginButton();
        }
    }
}