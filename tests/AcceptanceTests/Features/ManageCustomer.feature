Feature: Manage customer in the system
	
	Scenario: Customer gets created successfully
		When I create customers with the following details
			| FirstName | LastName | Email           | DateOfBirth | PhoneNumber   | BankAccountNumber |
			| Amir H.   | Jabari   | test1@gmail.com | 2002-12-02  | +989051877561 | 1234123412341234  |
			| Amir H.   | Jabari   | test2@gmail.com | 2002-12-03  | +989051877561 | 1234123412341234  |
		Then the customers are created successfully
