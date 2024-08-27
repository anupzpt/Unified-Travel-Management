CREATE TABLE Setting.DriverDetails(
		Rowid				BIGINT			NOT NULL		IDENTITY(1,1),
		DriverCode			VARCHAR(150),
		FullName			VARCHAR(150),
		Age					VARCHAR(150),
		ContactNo 			VARCHAR(150),
		LiscineNo 			VARCHAR(150),
		VechicleCode		VARCHAR(150),
		PackageCode			VARCHAR(150),
		HotelCode			VARCHAR(150),
		Experience 			VARCHAR(150),
		Image			 	VARCHAR(MAX),
		Status				VARCHAR(150),
		CreatedBy			VARCHAR(150),
		CreatedDate			DATETIME2,
		ModifiedBy			VARCHAR(150),
		ModifiedDate			DATETIME2
)