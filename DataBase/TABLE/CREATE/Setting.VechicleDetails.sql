CREATE TABLE Setting.VechicleDetails(
	RowId				BIGINT		NOT NULL	IDENTITY(1,1),
	VechicleCode		VARCHAR(150),
	VechicleType		VARCHAR(150),
	Name				VARCHAR(150),
	Brand				VARCHAR(150),
	Model				VARCHAR(150),
	Capacity			BIGINT,
	RegisterNo			NVARCHAR(150),
	Status				VARCHAR(150),
	Image				NVARCHAR(MAX),
	Description			NVARCHAR(MAX),
	CreatedBy			VARCHAR(150),
	CreatedDate			DATETIME2,
	ModifiedBy			VARCHAR(150),
	ModifiedDate		DATETIME2
)