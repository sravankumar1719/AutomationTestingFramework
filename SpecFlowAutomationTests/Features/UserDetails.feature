Feature: UserDetails
	Sample file for Step Argument Transformation and Tables example

@mytag
Scenario: Adding new Users
Given I add users to UserDetails
| User Name       | Date of birth | Phone Number | Gender |
| sravan		  | 17/08/1998    | 8919097235   | M      |
| tulasi		  | 26/12/1997    | 9059001545   | F      |
| prathyusha	  | 21/01/1998    | 9948885750   | F      |
When I sort those details according to Age
Then "sravan" should be the first person in the sorting list with age 17/08