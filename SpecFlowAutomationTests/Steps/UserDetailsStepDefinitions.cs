using System;
using TechTalk.SpecFlow;

namespace SpecFlowAutomationTests.Steps
{
    using System.Collections.Generic;
    using System.Linq;

    using TechTalk.SpecFlow.Assist;

    [Binding]
    public class UserDetailsSteps
    {
        private List<UserDetails> userDetails;

        [Given(@"I add users to UserDetails")]
        public void GivenIAddUsersToUserDetails(Table userDetailsTable)
        {
            //foreach (var user in userDetailsTable.Rows)
            //{
            //    user.TryGetValue("Date of birth", out var dateOfBirth); 
            //    user["Date of birth"] = DateTime.ParseExact(dateOfBirth, "dd/MM/yyyy", null).ToShortDateString();
            //}

            userDetails = userDetailsTable.CreateSet<UserDetails>().ToList();
        }
        
        [When(@"I sort those details according to Age")]
        public void WhenISortThoseDetailsAccordingToAge()
        {
            this.userDetails = this.userDetails.OrderBy(user => user.DateOfBirth).ToList();
        }
        
        [Then(@"""(.*)"" should be the first person in the sorting list with age (.*)")]
        public void ThenShouldBeTheFirstPersonInTheSortingListWithAge(string p0, int p1)
        {
            ScenarioContext.Current.Pending();
        }


        [StepArgumentTransformation(@"in the sorting list with age (\d+)")]
        public int ConvertAge(string p0, int p1)
        {
            return 1;
        }
    }
}
