Feature: PayOrTransferDialog
	User should be able to transfer money using the Pay Or Transfer Dialog

Scenario: User should be able to transfer money from Everyday account to Bills account
	Given that I am in the home page
	When I access the pay or transfer dialog
	And I transfer $500  from Everyday account to Bills account
	Then I should see that successful message is displayed
	And the current balance of Everyday account to Bills account is correct