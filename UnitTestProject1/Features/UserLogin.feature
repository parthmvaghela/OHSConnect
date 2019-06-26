Feature: UserLogin
User wants Login, select an account and then selects the system.

@LoginTag
Scenario: Login
	Given That I am on OHS Connect Website
	When I have entered username and password
	Then I should be redirected to select account page

@AccountSystemTag
Scenario: Select account and System
	Given I have selected an account
	When I have selected System
	Then I should be redirected to Home Page