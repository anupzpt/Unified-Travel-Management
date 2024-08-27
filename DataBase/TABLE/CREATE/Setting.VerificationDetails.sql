CREATE TABLE Setting.VerificationDetails(
	Rowid				BIGINT		NOT NULL  IDENTITY(1,1),
	Username			VARCHAR(150),
	VerificationCode	VARCHAR(150),
	VerificationType	VARCHAR(150),
	Status				VARCHAR(150)
)