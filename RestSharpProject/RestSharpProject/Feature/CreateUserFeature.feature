Feature: CreateUser
	
Scenario: Add a new user
	Given I input name,lastname, emailid, password
	When I create the user
	Then user is created