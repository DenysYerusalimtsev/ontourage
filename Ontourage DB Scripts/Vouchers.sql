CREATE TABLE Vouchers
(
    Id INT PRIMARY KEY IDENTITY (1, 1),
	TourName NVARCHAR(MAX),
	CountryCode NVARCHAR(2) REFERENCES Countries (Code),
	HotelId INT REFERENCES Hotels (Id),
	PassageInclude BIT,
	FoodId INT REFERENCES Food (Id),
	TourOperatorId INT REFERENCES TourOperators (Id),
	Price SMALLMONEY,
	CountFreeVouchers INT,
	DepartureTime DATETIME,
	DeparturePlace NVARCHAR(MAX),
	ArrivalTime DATETIME,
	ArrivalPlace NVARCHAR(MAX)
)