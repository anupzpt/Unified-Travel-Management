 
  ALTER  PROCEDURE Setting.PROC_HotelManagement
	(
		@Flag							NVARCHAR(100),
		@RowId							VARCHAR(100)			= NULL,
		@FromDate						VARCHAR(10)				= NULL,
		@ToDate							VARCHAR(10)				= NULL,
		@DisplayLength					INT						= NULL,					
		@DisplayStart					INT						= NULL,
		@SortCol						INT						= NULL,	
		@SortDir						NVARCHAR(10)			= NULL,	
		@Search							NVARCHAR(100)			= NULL,
		@UserName						NVARCHAR(100)			= NULL,
		@Branch							VARCHAR(100)			= NULL,
		@BranchUnit						VARCHAR(100)			= NULL,
		@Status 						VARCHAR(1)				= NULL,
		@CreatedBy						VARCHAR(250)			= NULL,
		@CreatedDate					DATETIME2				= NULL,

		@HotelCode						VARCHAR(150)			= NULL,
		@HotelName 						VARCHAR(150)			= NULL,
		@Location 						VARCHAR(150)			= NULL,
		@Description 					VARCHAR(150)			= NULL,
		@ReviewCode 					VARCHAR(150)			= NULL,
		@DocumentCode 					VARCHAR(150)			= NULL,
		@TotalPrice 					MONEY					= NULL,
		@AccommodationType 				VARCHAR(150)			= NULL,
		@HotelImageView 				VARCHAR(MAX)			= NULL,
		@ContactNo 						VARCHAR(150)			= NULL,
		@AlternativeContactNo 			VARCHAR(150)			= NULL,
		@RoomType 						VARCHAR(150)			= NULL,
		@HotelAvailabilityJson 			VARCHAR(MAX)			= NULL,
		@HotelPropertySurroundingJson 	VARCHAR(MAX)			= NULL,
		@HotelFacilityJson 				VARCHAR(MAX)			= NULL,
		@HotelGalleryJson 				VARCHAR(MAX)			= NULL
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='GetRequiredDetails'
	BEGIN
		SELECT sdc.Value,sdc.Description FROM Setting.StaticDataCommon AS sdc WHERE sdc.StaticCode = 'AccommodationType'
 
	    return
	END
	ELSE IF @Flag= 'GetGridDetails'
	BEGIN
	    WITH hotelList AS
	    (
			SELECT  
					ROW_NUMBER() OVER (ORDER BY hd.HotelName DESC) AS RowNum,
					COUNT(*) over()  AS FilterCount,
                   hd.HotelCode,
                   hd.DocumentCode,
                   hd.HotelName,
                   hd.Location,
                   hd.Status
				   FROM Setting.HotelDetails AS hd
				   WHERE hd.AccommodationType=@AccommodationType
	    )
	    SELECT *
	    FROM hotelList AS ht 
		WHERE ht.RowNum BETWEEN @FirstRec AND @LastRec
	END
	ELSE IF @Flag='GetHotelAccomodationList'
	BEGIN
	    WITH accomodationList AS
	    (
			SELECT ROW_NUMBER() OVER (ORDER BY sdc.StaticCode DESC) AS RowNum,
			       COUNT(*) over()  AS FilterCount,sdc.Description AccommodationType FROM Setting.StaticDataCommon AS sdc
				   WHERE sdc.StaticCode='AccommodationType'
				   AND ( @Search IS  NULL OR  sdc.Description LIKE '%'+@Search+'%')
	    )
	    SELECT *
	    FROM accomodationList AS at WHERE at.RowNum BETWEEN @FirstRec AND @LastRec
	END
	ELSE IF @Flag='AddHotelInformationDetails'
	BEGIN
		UPDATE Setting.Common SET HotelSeqCode = ISNULL(HotelSeqCode,0)+1

		SELECT @HotelCode ='Hotel'+CAST(c.HotelSeqCode AS VARCHAR(150)) FROM Setting.Common AS c 
	    BEGIN TRY
	        BEGIN TRANSACTION HotelSetup
				INSERT INTO Setting.HotelDetails
				(
				    HotelCode,
				    AccommodationType,
				    HotelName,
				    Location,
				    ContactNo,
				    AlternativeContactNo,
				    Image,
				    Description,
				    Status,
				    CreatedBy,
				    CreatedDate
				)
				VALUES
				(   @HotelCode,
				    @AccommodationType,
				    @HotelName,
				    @Location,
				    @ContactNo,
				    @AlternativeContactNo,
				    @HotelImageView,
				    @Description,
				    'A',
				    @UserName,
				    GETDATE()
				 )		

				 INSERT INTO Hotel.HotelAvailabilityDetails
				 (
				     HotelCode,
				     RoomType,
				     NumberGuest,
				     Price,
				     DiscountPercentage,
				     TotalPrice,
				     CreatedBy,
				     CreatedDate
				 )
				 SELECT
						@HotelCode,
						temp.RoomType,
                        temp.NumberOfGuest,
                        temp.Price,
                        temp.DiscountPercent,
                        temp.TotalPrice,
						@CreatedBy,
						GETDATE()
				 FROM OPENJSON(@HotelAvailabilityJson) WITH(
						RoomType				VARCHAR(150)			'$.RoomType',
						NumberOfGuest 			BIGINT					'$.NumberOfGuest',
						Price 					MONEY					'$.Price',
						DiscountPercent 		BIGINT					'$.DiscountPercent',
						TotalPrice 				MONEY					'$.TotalPrice'
				 )AS temp


				 INSERT INTO Hotel.PropertySurroundingsInformation
				 (
				     HotelCode,
				     PropertyType,
				     Name
				 )
				SELECT
						@HotelCode,
					   temp.PropertyType,
                       temp.Name
				FROM OPENJSON(@HotelPropertySurroundingJson)
				WITH
                (
					PropertyType				VARCHAR(150)			'$.PropertyType', 
					Name 						VARCHAR(150)			'$.Name' 
				)temp
				 
				INSERT INTO Hotel.FacilityDetails
				(
				    HotelCode,
				    FacilityType,
				    Name,
				    IsPopularFaculity
				)
				SELECT 
					   @HotelCode,
					   temp.FacilityType,
                       temp.Name,
                       temp.IsPopularFaculity 
				FROM OPENJSON(@HotelFacilityJson) 
				WITH
				(
					FacilityType					VARCHAR(150)		'$.FacilityType',
					Name 							VARCHAR(150)		'$.Name',
					IsPopularFaculity 				VARCHAR(150)		'$.IsPopularFaculity' 
				)temp
	       COMMIT TRANSACTION HotelSetup
			SELECT 000 Code, 'Hotel Information Added Sucessfully' Message, '' Id
	    END TRY
	    BEGIN CATCH
			ROLLBACK TRANSACTION HotelSetup
			SELECT 111 Code, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END
	ELSE IF @Flag='GetHotelGalleryDetails'
	BEGIN
	    SELECT hd.HotelCode,hd.HotelName,hd.Location 
		FROM Setting.HotelDetails AS hd 
		WHERE hd.HotelCode=@HotelCode

		SELECT dd.ImageURL HotelGalleryView 
		FROM Setting.DocumentDetail AS dd 
		WHERE dd.Code=@HotelCode
	END
	ELSE IF @Flag='UploadHotelGallery'
	BEGIN
		UPDATE Setting.Common SET DocumentSeqCode = ISNULL(DocumentSeqCode,0)+1

		SELECT @DocumentCode ='Doc'+CAST(c.DocumentSeqCode AS VARCHAR(150)) FROM Setting.Common AS c 
	    BEGIN TRY
	        BEGIN TRANSACTION UploadGallery
				INSERT INTO Setting.DocumentDetail
				(
					DocumentCode,
				    Code,
				    ImageURL
				)
				SELECT
					@DocumentCode,
					@HotelCode,
					temp.Image
				FROM OPENJSON(@HotelGalleryJson)
				WITH
				(
					Image			VARCHAR(Max)		'$.Image'
				)temp
	       COMMIT TRANSACTION UploadGallery
			SELECT 000 Code, 'Hotel Gallery Uploaded Sucessfully' Message, '' Id
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION UploadGallery
	    SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END
	ELSE IF @Flag='GetHotelListByAccomodationType'
	BEGIN
		DECLARE @count INT
		 SELECT @count=COUNT(*) FROM Setting.HotelDetails AS hd WHERE hd.AccommodationType=@AccommodationType
	    SELECT hd.HotelCode,hd.HotelName,hd.Location,REPLACE(hd.Image,'\','/') HotelImageView,@count HotelCount FROM Setting.HotelDetails AS hd WHERE hd.AccommodationType=@AccommodationType
	END
	ELSE IF @Flag='GetHotelDetails'
	BEGIN
	  SELECT  hd.HotelCode,hd.AccommodationType,hd.HotelName,hd.Location,hd.ContactNo,hd.AlternativeContactNo,REPLACE(hd.Image,'\','/') HotelImageView,hd.Description
	  FROM Setting.HotelDetails AS hd
	  WHERE hd.HotelCode=@HotelCode

	  SELECT HotelCode,RoomType,RoomNumber,NumberGuest NumberOfGuest,NumberOfBed NoOfBed,Price,DiscountPercentage DiscountPercent,TotalPrice,Description 
	  FROM Hotel.HotelAvailabilityDetails AS had
	  WHERE had.HotelCode=@HotelCode


	  SELECT psi.PropertyType,psi.Name
	  FROM Hotel.PropertySurroundingsInformation AS psi 
	  WHERE psi.HotelCode=@HotelCode

	  SELECT fd.FacilityType,fd.Name FROM Hotel.FacilityDetails AS fd WHERE fd.HotelCode=@HotelCode

	  SELECT dd.ImageURL HotelGalleryView FROM Setting.DocumentDetail AS dd WHERE dd.Code=@HotelCode
	END
	ELSE IF @Flag='GetDetailsForPayment'
	BEGIN
		SELECT hd.HotelCode,hd.ReviewCode,hd.DocumentCode,hd.AccommodationType,hd.HotelName,hd.Location,
		hd.ContactNo,hd.AlternativeContactNo,hd.Image HotelImageView,hd.Description,hd.Status
		FROM Setting.HotelDetails AS hd WHERE hd.HotelCode=@HotelCode

		INSERT INTO Setting.BookingInformationDetail
		(
		    Code,
		    CodeType,
		    Name
		)
		 SELECT 
			@HotelCode,
			'Hotel',
			pd.HotelName
		 FROM Setting.HotelDetails  AS pd 
		 WHERE pd.HotelCode=@HotelCode
	END
	ELSE IF @Flag='GetRequiredList'
	BEGIN
		SELECT had.RoomType Value,had.RoomType Description 
		FROM Hotel.HotelAvailabilityDetails AS had 
		WHERE had.HotelCode=@HotelCode
	END
	ELSE IF @Flag='CalculateHotelInformation'
	BEGIN
	    DECLARE @totalAmount  MONEY ,@Amount  MONEY ,@taxAmount  MONEY
        
		SELECT TOP 1 @Amount=pad.TotalPrice FROM Hotel.HotelAvailabilityDetails  AS pad 
		WHERE pad.HotelCode =@HotelCode AND pad.RoomType=@RoomType

		SELECT @taxAmount = @Amount * 0.01

		SELECT @totalAmount = @Amount + @taxAmount + 10

		SELECT @totalAmount totalAmount,@Amount Amount,@taxAmount taxAmount,'10' ServiceCharge

	END
	ELSE IF @Flag='SuccessPayment'
	BEGIN
	    UPDATE Setting.BookingInformationDetail SET 
				STATUS='BOOKED',
				BookedBy=@UserName,
				BookedDate=GETDATE()
		WHERE Code=@HotelCode

	   SELECT '0' code,'Payment Successfully' Message

	END
		ELSE IF @Flag='UpdateHotelInformationDetails'
	BEGIN
		DELETE Setting.HotelDetails WHERE HotelCode=@HotelCode
		DELETE Hotel.HotelAvailabilityDetails WHERE HotelCode=@HotelCode
		DELETE Hotel.FacilityDetails WHERE HotelCode=@HotelCode
		DELETE Hotel.PropertySurroundingsInformation WHERE HotelCode=@HotelCode
	    BEGIN TRY
	        BEGIN TRANSACTION HotelSetup
				INSERT INTO Setting.HotelDetails
				(
				    HotelCode,
				    AccommodationType,
				    HotelName,
				    Location,
				    ContactNo,
				    AlternativeContactNo,
				    Image,
				    Description,
				    Status,
				    CreatedBy,
				    CreatedDate
				)
				VALUES
				(   @HotelCode,
				    @AccommodationType,
				    @HotelName,
				    @Location,
				    @ContactNo,
				    @AlternativeContactNo,
				    @HotelImageView,
				    @Description,
				    'A',
				    @UserName,
				    GETDATE()
				 )		

				 INSERT INTO Hotel.HotelAvailabilityDetails
				 (
				     HotelCode,
				     RoomType,
				     NumberGuest,
				     Price,
				     DiscountPercentage,
				     TotalPrice,
				     CreatedBy,
				     CreatedDate
				 )
				 SELECT
						@HotelCode,
						temp.RoomType,
                        temp.NumberOfGuest,
                        temp.Price,
                        temp.DiscountPercent,
                        temp.TotalPrice,
						@CreatedBy,
						GETDATE()
				 FROM OPENJSON(@HotelAvailabilityJson) WITH(
						RoomType				VARCHAR(150)			'$.RoomType',
						NumberOfGuest 			BIGINT					'$.NumberOfGuest',
						Price 					MONEY					'$.Price',
						DiscountPercent 		BIGINT					'$.DiscountPercent',
						TotalPrice 				MONEY					'$.TotalPrice'
				 )AS temp


				 INSERT INTO Hotel.PropertySurroundingsInformation
				 (
				     HotelCode,
				     PropertyType,
				     Name
				 )
				SELECT
						@HotelCode,
					   temp.PropertyType,
                       temp.Name
				FROM OPENJSON(@HotelPropertySurroundingJson)
				WITH
                (
					PropertyType				VARCHAR(150)			'$.PropertyType', 
					Name 						VARCHAR(150)			'$.Name' 
				)temp
				 
				INSERT INTO Hotel.FacilityDetails
				(
				    HotelCode,
				    FacilityType,
				    Name,
				    IsPopularFaculity
				)
				SELECT 
					   @HotelCode,
					   temp.FacilityType,
                       temp.Name,
                       temp.IsPopularFaculity 
				FROM OPENJSON(@HotelFacilityJson) 
				WITH
				(
					FacilityType					VARCHAR(150)		'$.FacilityType',
					Name 							VARCHAR(150)		'$.Name',
					IsPopularFaculity 				VARCHAR(150)		'$.IsPopularFaculity' 
				)temp
	       COMMIT TRANSACTION HotelSetup
			SELECT 000 Code, 'Hotel Information Updated Sucessfully' Message, '' Id
	    END TRY
	    BEGIN CATCH
			ROLLBACK TRANSACTION HotelSetup
			SELECT 111 Code, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END
	ELSE IF @Flag='GetBookedHotelList'
	BEGIN
	    
	    WITH bookedList AS
	    (
			SELECT bid.Code HotelCode,bid.Name HotelName,bid.BookedBy,bid.BookedDate,bid.STATUS,
			ROW_NUMBER() OVER (ORDER BY bid.Code DESC) AS RowNum,
			 COUNT(*) over()  AS FilterCount
			FROM Setting.BookingInformationDetail AS bid
			WHERE bid.CodeType='Hotel'
	    )
	    SELECT *
	    FROM bookedList AS b
	END
	ELSE IF @Flag='GetBookedPackagelList'
	BEGIN
	    
	    WITH bookedList AS
	    (
			SELECT bid.Code PackageCode,bid.Name PackageName,bid.BookedBy,bid.BookedDate,bid.STATUS,
			ROW_NUMBER() OVER (ORDER BY bid.Code DESC) AS RowNum,
			 COUNT(*) over()  AS FilterCount
			FROM Setting.BookingInformationDetail AS bid
			WHERE bid.CodeType='Package'
	    )
	    SELECT *
	    FROM bookedList AS b
	END
END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
END CATCH
GO
