Feature: Manage customer in the system
	
	Scenario: Customers get created successfully
		When I create customers with the following details
			| FirstName | LastName | Email           | DateOfBirth | PhoneCountryCode | PhoneNumber | BankAccountNumber |
			| Amir H.   | Jabari   | test1@gmail.com | 2002-12-02  | 98               | 9051877561  | 5422570172410822  |
			| Amir H.   | Jabari   | test2@gmail.com | 2002-12-03  | 98               | 9051877561  | 5422570172410822  |
		Then the customers are created successfully

	Scenario: Customers get deleted successfully
		Given Following customers created
			| FirstName | LastName | Email            | DateOfBirth | PhoneCountryCode | PhoneNumber | BankAccountNumber |
			| Amir H.   | Jabari   | test21@gmail.com | 2002-12-21  | 98               | 9051877561  | 5422570172410822  |
			| Amir H.   | Jabari   | test22@gmail.com | 2002-12-23  | 98               | 9051877561  | 5422570172410822  |
		When Created customers in previous step get deleted
		Then Customers are deleted successfully

