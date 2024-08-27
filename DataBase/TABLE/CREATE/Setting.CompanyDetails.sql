CREATE TABLE Setting.CompanyDetails(
	RowId				INT			NOT NULL   IDENTITY(1,1),
	CompanyCode			VARCHAR(150),
	CompanyName			VARCHAR(150),
	PopularServices		VARCHAR(150),
	Image				VARCHAR(150),
	Description			VARCHAR(250),
	Status				VARCHAR(10),
	CreatedBy			VARCHAR(150),
	CreatedDate			DATETIME2,
	ModifiedBy			VARCHAR(150),
	ModifiedDate		DATETIME2
)