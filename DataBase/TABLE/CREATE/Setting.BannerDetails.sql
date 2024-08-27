CREATE TABLE Setting.BannerDetails(
	RowId				BIGINT	 NOT NULL			IDENTITY(1,1),
	BannerCode			VARCHAR(150),
	Title				VARCHAR(150),
	SloganName			VARCHAR(150),
	Image				VARCHAR(150),
	Description			VARCHAR(250),
	Status				VARCHAR(20),
	CreatedBy			VARCHAR(150),
	CreatedDate			DATETIME2,
	ModifiedBy			VARCHAR(150),
	ModifiedDate		DATETIME2
)

 