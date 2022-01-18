Feature: PayeesPage
	User should be able to perform all payee activites in the payee page

Scenario: User should be able to navigate to payee page using the navigation menu
	Given that I am in the home page
	When I navigate to payee page via the navigation menu
	Then I should be able to see the payee page

Scenario: User should be able to add new payee in the payees page
	Given that I am in the payee page
	When I add a new payee
	Then I should be able to see the new payee in the payees list

Scenario: User should be able to see that payee name is a required field
	Given that I am in the payee page
	When I try to add a payee with blank information
	Then I should see that payee name is a required field
	But  If I populate the payee name
	Then the validation errors should disappear

Scenario: User should be able to see that payees can be sorted by name
	Given that I am in the payee page
	When I add a new payee
	Then I should see that the payee list is sorted in ascending order
	But  If I click the header
	Then  should see that the payee list is sorted in descending order