namespace SpecFlowAutomationTests.Steps
{
    using SpecFlowAutomationTests.Hooks;

    using TechTalk.SpecFlow;

    using TestAutomationFramework.Pages.BankingApplication;
    using TestAutomationFramework.Utils.Custom;

    [Binding]
    public class BankingApplicationSteps : BaseTest
    {
        private readonly ScenarioContext _scenarioContext;

        public BankingApplicationSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I click on Customer login")]
        public void GivenIClickOnCustomerLogin()
        {
            _scenarioContext["bankingApplicationPage"] = this.GetHomePage<BankingHomePage>();
            _scenarioContext.Get<BankingHomePage>("bankingApplicationPage").ClickOnCustomerLoginButton();
        }

        [When(@"I select the (.*) from the user dropdown")]
        public void WhenISelectTheFromTheUserDropdown(string userName)
        {
            _scenarioContext.Get<BankingHomePage>("bankingApplicationPage").SelectUserName(userName);
        }

        [When(@"Click on Login button")]
        public void WhenClickOnLoginButton()
        {
            _scenarioContext.Get<BankingHomePage>("bankingApplicationPage").ClickOnLoginButton();
        }

        [Then(@"I should be able to see all the details of that user (.*), (.*) , (.*) and (.*)")]
        public void ThenIShouldBeAbleToSeeAllTheDetailsOfThatUserAnd(string holderName, string accountNumber, string balance, string currency)
        {
            CustomizedAssert holderNameCheck = new CustomizedAssert("Check whether the bank holder name is displayed");
            CustomizedAssert accountNumberCheck = new CustomizedAssert("Check whether the account number is displayed");
            CustomizedAssert balanceCheck = new CustomizedAssert("Check whether the balance is displayed");
            CustomizedAssert currencyCheck = new CustomizedAssert("Check whether the currency is displayed");

            SoftAssertions.AddAssertions(holderNameCheck, accountNumberCheck, balanceCheck, currencyCheck);

            _scenarioContext["bankingUserHomePage"] = this.GetHomePage<BankingUserHomePage>();

            SoftAssertions.IsTrue(holderNameCheck, _scenarioContext.Get<BankingUserHomePage>("bankingUserHomePage").IsUserNameDisplayed(holderName), "Username is not displayed in user home page");
            var userDetails = _scenarioContext.Get<BankingUserHomePage>("bankingUserHomePage").GetUserDetails();
            SoftAssertions.AreEqual(accountNumberCheck, accountNumber, userDetails.AccountNumber, "User Account number is not matching");
            SoftAssertions.AreEqual(balanceCheck, balance, userDetails.Balance, "User Balance is not matching");
            SoftAssertions.AreEqual(currencyCheck, currency, userDetails.Currency, "User Currency details is not matching");
        }
    }
}
