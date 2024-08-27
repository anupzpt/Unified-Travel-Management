CREATE TABLE Setting.UserDetails(
		RowId				BIGINT			NOT NULL		IDENTITY(1,1),
		Username			VARCHAR(150),
		FullName			VARCHAR(150),
		ContactNo			VARCHAR(150),
		EmailId				VARCHAR(150),
		IsAdmin				BIT,
		IsSupperUser		BIT,
		KeepLoggedIn		BIT,
		LoginCount			INT,
		UserPassword	    NVARCHAR(150),
		CreatedBy			VARCHAR(150),
		CreatedDate			DATETIME2,
		ModifiedBy			VARCHAR(150),
		ModifiedDate		DATETIME2
)
 