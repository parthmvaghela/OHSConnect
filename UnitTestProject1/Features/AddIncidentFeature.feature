Feature: AddIncidentFeature

@mytag
Scenario: Add two numbers
	Given I add new incident
	When I Fill the incident report and save
	Then Incident report should be saved and finish