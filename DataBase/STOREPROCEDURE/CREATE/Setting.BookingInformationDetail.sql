CREATE TABLE Setting.BookingInformationDetail(
		RowId				BIGINT		NOT NULL  IDENTITY(1,1),
		Code				VARCHAR(150),
		CodeType			VARCHAR(150),
		Name				VARCHAR(150),
		TotalAmount			MONEY,
		STATUS				VARCHAR(150),
		BookedBy			VARCHAR(150),
		BookedDate			VARCHAR(150)
)