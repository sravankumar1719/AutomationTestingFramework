namespace SpecFlowAutomationTests.StepArgumentTransformation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    [Binding]
    public class ArgumentTransformation
    {
        [StepArgumentTransformation(@"add users to UserDetails")]
        public Table ConvertStringToDateTime(Table userDetailsTable)
        {
            foreach (var user in userDetailsTable.Rows)
            {
                user.TryGetValue("Date of birth", out var dateOfBirth);
                user["Date of birth"] = DateTime.ParseExact(dateOfBirth, "dd/MM/yyyy", null).ToShortDateString();
            }

            return userDetailsTable;
        }
    }
}
