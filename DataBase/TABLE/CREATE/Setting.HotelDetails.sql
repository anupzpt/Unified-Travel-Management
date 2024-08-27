CREATE SCHEMA Hotel
CREATE TABLE Setting.HotelDetails(
		RowId							BIGINT		NOT NULL   IDENTITY(1,1),
		HotelCode						VARCHAR(150),
		ReviewCode 						VARCHAR(150),
		DocumentCode 					VARCHAR(150),
		AccommodationType 				VARCHAR(150),
		HotelName 						VARCHAR(150),
		Location 						VARCHAR(150),
		ContactNo 						VARCHAR(150),
		AlternativeContactNo 			VARCHAR(150),
		Image		 					VARCHAR(MAX),
		Description 					VARCHAR(150),
		[Status]						VARCHAR(10),
		CreatedBy						VARCHAR(150),
		CreatedDate						DATETIME2,
		ModifiedBy						VARCHAR(150),
		ModifiedDate					DATETIME2
)

CREATE TABLE Hotel.HotelAvailabilityDetails(
		RowId							BIGINT		NOT NULL   IDENTITY(1,1),
		HotelCode						VARCHAR(150),
		RoomType						VARCHAR(150),
		RoomNumber						BIGINT,
		NumberGuest						BIGINT,
		NumberOfBed						BIGINT,
		Price							MONEY,
		DiscountPercentage				BIGINT,
		TotalPrice						MONEY,
		Description						VARCHAR(MAX),
		BookedRoomNo					BIGINT,
		BookedDate						DATETIME2,
		BookedBy						VARCHAR(150),
		CheckedInDate					DATETIME2,
		CheckedInBy						VARCHAR(150),
		CheckedOutDate					DATETIME2,
		CheckedOutBy					VARCHAR(150),
		CreatedBy						VARCHAR(150),
		CreatedDate						DATETIME2,
		ModifiedBy						VARCHAR(150),
		ModifiedDate					DATETIME2
)
CREATE TABLE Hotel.PropertySurroundingsInformation(
		RowId							BIGINT		NOT NULL   IDENTITY(1,1),
		HotelCode						VARCHAR(150),
		PropertyType					VARCHAR(150),
		Name							VARCHAR(150)
)

CREATE TABLE Hotel.FacilityDetails(
		RowId							BIGINT		NOT NULL   IDENTITY(1,1),
		HotelCode						VARCHAR(150),
		FacilityType					VARCHAR(150),
		Name							VARCHAR(150),
		IsPopularFaculity				BIT	
)
