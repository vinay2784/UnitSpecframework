@chrome
Feature: UserRegistration
	In order to access parabank site
	As a user
	I want to register

@Registration
Scenario Outline: User Registration	
	
	Given User clicks on Registration link
	And User Enter <firstname>,<lastname>,<street>,<city>,<state>,<zipcode>,<phone>,<ssn>,<uname>,<password>,<rpassword>
	When User submit registration
	Then The welcome message displays to the user with <uname>
	And User LogOut from the Application
	Examples: 
	| firstname | lastname  | street  | city  | state   | zipcode | phone      | ssn        | uname    | password  | rpassword |
	| <firstname> | <lastname> | <street> | <city> | <state> | <zipcode>   | <phone> | <ssn> | <uname> | <password> | <rpassword> |
	| <firstname> | <lastname> | <street> | <city> | <state> | <zipcode>   | <phone> | <ssn> | <uname> | <password> | <rpassword> |
	| <firstname> | <lastname> | <street> | <city> | <state> | <zipcode>   | <phone> | <ssn> | <uname> | <password> | <rpassword> |
@Login
	Scenario: User Login
	
		When User login with 
		| userid   | password   |
		| <userid> | <password> |

		Then Accounts OverView Page displays to the user.
		And User LogOut from the Application
	


	
