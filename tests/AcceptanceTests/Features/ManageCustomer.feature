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

	Scenario: Customers pagination returns correct data get
		When All following customers created
			| FirstName | LastName | Email            | DateOfBirth | PhoneCountryCode | PhoneNumber | BankAccountNumber |
			| Amir H.   | Jabari   | test21@gmail.com | 2002-12-21  | 98               | 9051877561  | 5422570172410822  |
			| Amir H.   | Jabari   | test22@gmail.com | 2002-12-23  | 98               | 9051877561  | 5422570172410822  |
			| Amir H.   | Jabari   | test23@gmail.com | 2002-12-24  | 98               | 9051877561  | 5422570172410822  |
			| Amir H.   | Jabari   | test24@gmail.com | 2002-12-25  | 98               | 9051877561  | 5422570172410822  |
			| Amir H.   | Jabari   | test25@gmail.com | 2002-12-26  | 98               | 9051877561  | 5422570172410822  |
			| Amir H.   | Jabari   | test26@gmail.com | 2002-12-27  | 98               | 9051877561  | 5422570172410822  |
			| Amir H.   | Jabari   | test27@gmail.com | 2002-12-28  | 98               | 9051877561  | 5422570172410822  |
			| Amir H.   | Jabari   | test28@gmail.com | 2002-12-29  | 98               | 9051877561  | 5422570172410822  |
			| Amir H.   | Jabari   | test29@gmail.com | 2002-12-30  | 98               | 9051877561  | 5422570172410822  |
		Then Customers are returned successfully with pagination