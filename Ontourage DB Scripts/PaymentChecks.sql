CREATE TABLE PaymentChecks
(
    Id INT PRIMARY KEY IDENTITY (1, 1),
	VoucherId INT REFERENCES Vouchers (Id),
	ClientId INT REFERENCES Clients (Id),
	CountOfVouchers INT,
	TotalPrice SMALLMONEY,
	DateOfSale SMALLDATETIME,
)