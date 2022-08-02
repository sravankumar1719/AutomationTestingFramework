Feature: BankingApplication

@banking @automation
Scenario Outline: Verify user details in Banking page
	Given I click on Customer login
	When I select the <holderName> from the user dropdown
	And Click on Login button
	Then I should be able to see all the details of that user <holderName>, <accountNumber> , <balance> and <currency>

Examples: 
| holderName			| accountNumber		| balance	| currency	|
| Hermoine Granger      |    1001           |   5096    |   Dollar  |
| Harry Potter			|    1004           |   0	    |   Dollar  |
| Hermoine Granger      |    1001           |   50961   |   Dollar  |
| Albus Dumbledore      |    1010           |   10	    |   Dollar  |