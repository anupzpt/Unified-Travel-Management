CREATE TABLE Setting.DocumentDetail(
		RowId						BIGINT			NOT NULL		IDENTITY(1,1),
		DocumentCode				VARCHAR(150),
		Code						VARCHAR(150),
		ImageURL					VARCHAR(MAX),
)