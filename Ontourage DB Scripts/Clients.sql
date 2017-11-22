CREATE TABLE Clients
(
    Id INT PRIMARY KEY IDENTITY (1, 1),
	FirstName NVARCHAR(MAX),
	LastName NVARCHAR(MAX),
	Sex NVARCHAR(MAX),
	DateOfBirth DATE,
	Passport NVARCHAR(MAX),
	PhoneNumber NVARCHAR(MAX),
	Email NVARCHAR(MAX),
	DiscountId INT REFERENCES Discount (Id),
	UserLevel INT
)