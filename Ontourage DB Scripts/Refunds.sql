CREATE TABLE Refunds
(
    Id INT PRIMARY KEY IDENTITY (1, 1),
	PaymentCheckId INT REFERENCES PaymentChecks,
	DateOfRefund DATETIME
)