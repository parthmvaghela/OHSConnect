Feature: AddIncidentFeature

@addnewincidenttag
Scenario: Add new Incident and Save
	Given I add new incident
	When I Fill the incident report and save
	Then Incident report should be saved and finish