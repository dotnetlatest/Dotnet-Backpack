#### SQL Cheat Sheet


###### Importing CSV files into Sql Server.


Create Test Table

	
		USE TestData
		GO
		CREATE TABLE CSVTest
		(ID INT,
		FirstName VARCHAR(40),
		LastName VARCHAR(40),
		BirthDate SMALLDATETIME)
		GO
	
	
Contents of CSV file

	1,James,Smith,19750101
	
	2,Meggie,Smith,19790122
	
	3,Robert,Smith,20071101
	
	4,Alex,Smith,20040202

Import Script

	BULK
	INSERT CSVTest
	FROM 'c:\csvtest.txt'
	WITH
	(
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '\n'
	)
	GO


---