CREATE TABLE Setting.StaticDataCommon(
		Id					BIGINT		NOT NULL	IDENTITY(1,1),
		StaticCode			VARCHAR(150),
		Value				VARCHAR(150),
		Description			VARCHAR(150),
		Status				VARCHAR(10)
)