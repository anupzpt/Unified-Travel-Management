 
  ALTER  PROCEDURE  Setting.PackageInformationManagement
	(
		@Flag									NVARCHAR(100),
		@RowId									NVARCHAR(100)			= NULL,
		@FromDate								NVARCHAR(10)			= NULL,
		@ToDate									NVARCHAR(10)			= NULL,
		@DisplayLength							INT						= NULL,					
		@DisplayStart							INT						= NULL,
		@SortCol								INT						= NULL,	
		@SortDir								NVARCHAR(10)			= NULL,	
		@Search									NVARCHAR(100)			= NULL,
		@UserName								NVARCHAR(100)			= NULL,
		@Branch									NVARCHAR(100)			= NULL,
		@BranchUnit								NVARCHAR(100)			= NULL,
		@Status 								NVARCHAR(1)				= NULL,

		@PackageCode							NVARCHAR(150)			= NULL,
		@PackageName 							NVARCHAR(150)			= NULL,
		@PackageType 							NVARCHAR(150)			= NULL,
		@PackageAverageCost 					MONEY					= NULL,
		@Country 								NVARCHAR(150)			= NULL,
		@Duration 								NVARCHAR(150)			= NULL,
		@BestSeason 							NVARCHAR(150)			= NULL,
		@Group 									NVARCHAR(150)			= NULL,
		@MinimumPerson 							NVARCHAR(150)			= NULL,
		@MaximumPerson 							NVARCHAR(150)			= NULL,
		@MaxAltiude 							NVARCHAR(150)			= NULL,
		@Accommodation 							NVARCHAR(150)			= NULL,
		@Transportation 						NVARCHAR(150)			= NULL,
		@PackageCount 							NVARCHAR(150)			= NULL,
		@Description 							NVARCHAR(MAX)			= NULL,
		@GuideCode 								NVARCHAR(150)			= NULL,
		@RouteImage 							NVARCHAR(MAX)			= NULL,
		@PackageImageView 						NVARCHAR(MAX)			= NULL,
		@RouteImageView 						NVARCHAR(MAX)			= NULL,
		@DocumentCode 							NVARCHAR(150)			= NULL,
		@ReviewCode 							NVARCHAR(150)			= NULL,
		@InclusionExcludesPackageJson			NVARCHAR(MAX)			= NULL,
		@PackageItinerariesJson					NVARCHAR(MAX)			= NULL,
		@PackageAvailabilitiesJson				NVARCHAR(MAX)			= NULL,
		@BookingDate							DATETIME				= NULL
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='GetPackageListByPackageType'
	BEGIN
		SELECT pd.PackageName,pd.Image PackageImageView,CONVERT(DECIMAL(10,2),pd.PackageAverageCost)PackageAverageCost,pd.PackageCode FROM Setting.PackageDetails AS pd 
		WHERE pd.PackageType=@PackageType
	    return
	END
	ELSE IF @Flag='GetPackageDetailByPackageType'
	BEGIN
	    SELECT pd.PackageCode,pd.PackageName,pd.PackageType,pd.PackageAverageCost,pd.Country,pd.Duration,pd.BestSeason,pd.[Group],pd.MinimumPerson,pd.MaximumPerson,pd.MaxAltiude,pd.Accommodation,
			   pd.Transportation,pd.PackageCount,pd.Description,pd.GuideCode,pd.Status,pd.RouteImage RouteImageView,pd.Image PackageImageView,
               gd.Name GuideName,gd.Age GuideAge,gd.Address GuideAddress,gd.Experience GuideExperience,gd.PhoneNo GuidePhoneNo,gd.CitizenshipNo GuideCitizenshipNo,
			   gd.SpecializedRegion GuideSpecializedRegion,gd.Image GuideImageView,
			   gd.Description GuideDescription
               FROM Setting.PackageDetails AS pd 
			   INNER JOIN Setting.GuideDetails AS gd ON gd.GuideCode=pd.GuideCode
		WHERE pd.PackageCode=@PackageCode

		SELECT pid.Title,pid.Accomodation,pid.Meals,pid.Transport,pid.Description,pid.Notes
		FROM Package.PackageItineraryDetails AS pid 
		WHERE pid.PackageCode=@PackageCode

		SELECT pad.Year,pad.Month,pad.AvailableDate,pad.PackageCost
		FROM Package.PackageAvailabilityDetails AS pad 
		WHERE pad.PackageCode=@PackageCode

		SELECT  iepd.IncludeExclude,iepd.Description
		FROM Package.InclusionExcludesPackageDetails AS iepd 
		WHERE iepd.PackageCode=@PackageCode
		ORDER BY iepd.IncludeExclude ASC
	END
	ELSE IF @Flag='GetRequiredDetails'
	BEGIN
	    SELECT sdc.Value,sdc.Description FROM Setting.StaticDataCommon AS sdc WHERE sdc.StaticCode  = 'PackageType' 

		SELECT sdc.Value,sdc.Description FROM Setting.StaticDataCommon AS sdc WHERE sdc.StaticCode='Group'

		SELECT gd.GuideCode Value,gd.Name Description FROM Setting.GuideDetails AS gd
	END
	ELSE IF @Flag='Accommodation'
	BEGIN
	    SELECT hd.Value as text  FROM Setting.StaticDataCommon  AS hd
		WHERE hd.StaticCode LIKE '%'+@Search+'%' OR hd.Value LIKE @Search+'%' AND hd.StaticCode='AccommodationType' 
	END
	ELSE IF @Flag = 'AddPackageDetails'
	BEGIN

	    SELECT @PackageCode = 'Pack' + CAST(c.PackageSeqCode AS VARCHAR(10)) FROM Setting.Common AS c
		 
		 UPDATE Setting.Common SET PackageSeqCode = ISNULL(PackageSeqCode,0) + 1 WHERE 1=1
		 BEGIN TRY
		     BEGIN TRANSACTION packageSetup
				INSERT INTO Setting.PackageDetails
				(
				    PackageCode,
				    PackageName,
				    PackageType,
				    PackageAverageCost,
				    Country,
				    Duration,
				    BestSeason,
				    [Group],
				    MinimumPerson,
				    MaximumPerson,
				    MaxAltiude,
				    Accommodation,
				    Transportation,
				    Description,
				    GuideCode,
				    Status,
				    RouteImage,
				    Image,
					CreatedBy,
					CreatedDate
				)
				VALUES
				(   @PackageCode, 
				    @PackageName, 
				    @PackageType, 
				    @PackageAverageCost, 
				    @Country, 
				    @Duration, 
				    @BestSeason, 
				    @Group, 
				    @MinimumPerson, 
				    @MaximumPerson, 
				    @MaxAltiude, 
				    @Accommodation, 
				    @Transportation, 
				    @Description, 
				    @GuideCode, 
				    'A', 
				    @RouteImageView, 
				    @PackageImageView ,
					@UserName,
					GETDATE()
				    )
			
				INSERT INTO Package.PackageItineraryDetails
				(
				    PackageCode,
				    Title,
				    Accomodation,
				    Meals,
				    Transport,
				    Description,
				    Notes,
				    Status,
				    CreatedBy,
				    CreatedDate
				)
				SELECT 
					@PackageCode,
					temp.Title,
					temp.Accomodation,
					temp.Meals,
					temp.Transport,
					temp.Description,
					temp.Note,
					'A',
					@UserName,
					GETDATE()
				FROM OPENJSON(@PackageItinerariesJson)
				WITH
				(
					Title						VARCHAR(150)				'$.Title',
					Accomodation				VARCHAR(150)				'$.Accomodation',
					Meals						VARCHAR(150)				'$.Meals',
					Transport					VARCHAR(150)				'$.Transport',
					Description					VARCHAR(150)				'$.Description',
					Note						VARCHAR(150)				'$.Note'
				)temp

				IF(@PackageAvailabilitiesJson <> 'null')
				BEGIN
					INSERT INTO Package.PackageAvailabilityDetails
				(
				    PackageCode,
				    Year,			
				    Month,
				    AvailableDate,
				    PackageCost,
				    Status,
				    CreatedBy,
				    CreatedDate
				)
				SELECT 
					@PackageCode,
					temp.Year,			
					temp.Month,
					temp.AvailableDate,
					temp.PackageCost,
					'A',
					@UserName,
					GETDATE()
				FROM  OPENJSON(@PackageAvailabilitiesJson)
				WITH
				(
					Year				VARCHAR(150)		'$.Year',
					Month				VARCHAR(150)		'$.Month',
					AvailableDate		VARCHAR(150)		'$.AvailableDate',
					PackageCost			VARCHAR(150)		'$.PackageCost'
				)temp
				END

				IF (@InclusionExcludesPackageJson <> 'null')
				BEGIN
					INSERT INTO Package.InclusionExcludesPackageDetails
				(
				    PackageCode,
				    IncludeExclude,
				    Description,
				    Status
				)
				SELECT 
					@PackageCode,
					temp.IncludeExclude,
					temp.Description,
					'A'
				FROM OPENJSON(@InclusionExcludesPackageJson) 
				WITH
				(
						IncludeExclude				VARCHAR(150)			'$.IncludeExclude',
						Description					VARCHAR(MAX)			'$.Description'
				)temp
				END
				 
				 SELECT '0' code,'Package Added Successfully' Message
		    COMMIT TRANSACTION packageSetup
		 END TRY
		 BEGIN CATCH
		 ROLLBACK TRANSACTION packageSetup
		 SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
		 END CATCH
	END
	ELSE IF @Flag='GetDetailsForPayment'
	BEGIN
	    SELECT pd.PackageCode,pd.PackageName,pd.PackageType,pd.PackageAverageCost,pd.Country,pd.Duration,pd.BestSeason,pd.[Group],pd.MinimumPerson,pd.MaximumPerson,pd.MaxAltiude,pd.Accommodation,
		pd.Transportation,pd.PackageCount,pd.Description,pd.GuideCode,pd.Status,pd.RouteImage RouteImageView,pd.Image PackageImageView
        FROM Setting.PackageDetails AS pd 
		WHERE pd.PackageCode=@PackageCode
		IF NOT EXISTS(SELECT 'A' FROM Setting.BookingInformationDetail AS bid WHERE bid.Code=@PackageCode AND bid.BookedBy IS Null )
		BEGIN
		    	INSERT INTO Setting.BookingInformationDetail
		(
		    Code,
		    CodeType,
		    Name,
		    TotalAmount
		)
		 SELECT 
			@PackageCode,
			'Package',
			pd.PackageName,
			pd.PackageAverageCost
		 FROM Setting.PackageDetails AS pd 
		 WHERE pd.PackageCode=@PackageCode
		END
		--INSERT INTO Setting.BookingInformationDetail
		--(
		--    Code,
		--    CodeType,
		--    Name,
		--    TotalAmount
		--)
		-- SELECT 
		--	@PackageCode,
		--	'Package',
		--	pd.PackageName,
		--	pd.PackageAverageCost
		-- FROM Setting.PackageDetails AS pd 
		-- WHERE pd.PackageCode=@PackageCode
	END
	ELSE IF @Flag='CalculatePackageInformation'
	BEGIN
	    DECLARE @totalAmount  MONEY ,@Amount  MONEY ,@taxAmount  MONEY
        
		SELECT TOP 1 @Amount=pad.PackageCost FROM Package.PackageAvailabilityDetails AS pad 
		WHERE pad.PackageCode =@PackageCode AND CONVERT(VARCHAR(10),pad.AvailableDate,23) =  CONVERT(VARCHAR(10),@BookingDate,23)

		SELECT @taxAmount = @Amount * 0.01

		SELECT @totalAmount = @Amount + @taxAmount +10

		SELECT @totalAmount totalAmount,@Amount Amount,@taxAmount taxAmount,'10' ServiceCharge

	END
	ELSE IF @Flag='SuccessPayment'
	BEGIN
	    UPDATE Setting.BookingInformationDetail SET 
				STATUS='BOOKED',
				BookedBy=@UserName,
				BookedDate=GETDATE()
		WHERE Code=@PackageCode

	   SELECT '0' code,'Payment Successfully' Message

	END
	ELSE IF @Flag='GetPackageTypeList'
	BEGIN
	    SELECT ROW_NUMBER() OVER (ORDER BY sdc.Id DESC) AS RowNum,
	    COUNT(*) over()  AS FilterCount,sdc.Value PackageType FROM Setting.StaticDataCommon AS sdc WHERE sdc.StaticCode='PackageType'
	END
	ELSE IF @Flag='GetPackageInformationList'
	BEGIN
		WITH package AS
	    (
			SELECT ROW_NUMBER() OVER (ORDER BY pd.PackageCode DESC) AS RowNum,
			       COUNT(*) over()  AS FilterCount,pd.PackageCode,pd.PackageName,pd.Status
			FROM Setting.PackageDetails AS pd 
			WHERE pd.PackageType=@PackageType
			AND pd.Status='A'
	    )
	    SELECT *
	    FROM package AS p 
		WHERE p.RowNum BETWEEN @FirstRec AND @LastRec
	END 
	ELSE IF @Flag='GetPackageDetails'
	BEGIN
	    SELECT pd.PackageCode,pd.PackageName,pd.PackageType,pd.PackageAverageCost,pd.Country,pd.Duration,pd.BestSeason,
		pd.[Group],pd.MinimumPerson,pd.MaximumPerson,pd.MaxAltiude,pd.Accommodation,pd.Transportation,pd.PackageCount,pd.Description,
		pd.GuideCode,pd.Status,pd.RouteImage,pd.Image PackageImageView ,pd.DocumentCode,pd.ReviewCode
          FROM Setting.PackageDetails AS pd 
		  WHERE pd.PackageCode=@PackageCode

		  SELECT pid.PackageCode,pid.Title,pid.Accomodation,pid.Meals,pid.Transport,pid.Description,pid.Notes,pid.Status
		  FROM Package.PackageItineraryDetails AS pid
		  WHERE pid.PackageCode=@PackageCode

		  SELECT pad.PackageCode,pad.Year,pad.Month,pad.AvailableDate,pad.PackageCost,pad.Status
		  FROM Package.PackageAvailabilityDetails AS pad
		  WHERE pad.PackageCode=@PackageCode

		  SELECT  iepd.PackageCode,iepd.IncludeExclude,iepd.Description,iepd.Status
		  FROM Package.InclusionExcludesPackageDetails AS iepd
		  WHERE iepd.PackageCode=@PackageCode
	END
		ELSE IF @Flag = 'AddPackageDetails'
	BEGIN
		 BEGIN TRY
		     BEGIN TRANSACTION PackageUpdate
				DELETE FROM Setting.PackageDetails WHERE PackageCode=@PackageCode
				DELETE FROM Package.PackageItineraryDetails WHERE PackageCode=@PackageCode
				DELETE FROM Package.PackageAvailabilityDetails WHERE PackageCode=@PackageCode
				DELETE FROM Package.InclusionExcludesPackageDetails WHERE PackageCode=@PackageCode

				INSERT INTO Setting.PackageDetails
				(
				    PackageCode,
				    PackageName,
				    PackageType,
				    PackageAverageCost,
				    Country,
				    Duration,
				    BestSeason,
				    [Group],
				    MinimumPerson,
				    MaximumPerson,
				    MaxAltiude,
				    Accommodation,
				    Transportation,
				    Description,
				    GuideCode,
				    Status,
				    RouteImage,
				    Image,
					CreatedBy,
					CreatedDate
				)
				VALUES
				(   @PackageCode, 
				    @PackageName, 
				    @PackageType, 
				    @PackageAverageCost, 
				    @Country, 
				    @Duration, 
				    @BestSeason, 
				    @Group, 
				    @MinimumPerson, 
				    @MaximumPerson, 
				    @MaxAltiude, 
				    @Accommodation, 
				    @Transportation, 
				    @Description, 
				    @GuideCode, 
				    'A', 
				    @RouteImageView, 
				    @PackageImageView ,
					@UserName,
					GETDATE()
				    )
			
				INSERT INTO Package.PackageItineraryDetails
				(
				    PackageCode,
				    Title,
				    Accomodation,
				    Meals,
				    Transport,
				    Description,
				    Notes,
				    Status,
				    CreatedBy,
				    CreatedDate
				)
				SELECT 
					@PackageCode,
					temp.Title,
					temp.Accomodation,
					temp.Meals,
					temp.Transport,
					temp.Description,
					temp.Note,
					'A',
					@UserName,
					GETDATE()
				FROM OPENJSON(@PackageItinerariesJson)
				WITH
				(
					Title						VARCHAR(150)				'$.Title',
					Accomodation				VARCHAR(150)				'$.Accomodation',
					Meals						VARCHAR(150)				'$.Meals',
					Transport					VARCHAR(150)				'$.Transport',
					Description					VARCHAR(150)				'$.Description',
					Note						VARCHAR(150)				'$.Note'
				)temp

				IF(@PackageAvailabilitiesJson <> 'null')
				BEGIN
					INSERT INTO Package.PackageAvailabilityDetails
				(
				    PackageCode,
				    Year,			
				    Month,
				    AvailableDate,
				    PackageCost,
				    Status,
				    CreatedBy,
				    CreatedDate
				)
				SELECT 
					@PackageCode,
					temp.Year,			
					temp.Month,
					temp.AvailableDate,
					temp.PackageCost,
					'A',
					@UserName,
					GETDATE()
				FROM  OPENJSON(@PackageAvailabilitiesJson)
				WITH
				(
					Year				VARCHAR(150)		'$.Year',
					Month				VARCHAR(150)		'$.Month',
					AvailableDate		VARCHAR(150)		'$.AvailableDate',
					PackageCost			VARCHAR(150)		'$.PackageCost'
				)temp
				END

				IF (@InclusionExcludesPackageJson <> 'null')
				BEGIN
					INSERT INTO Package.InclusionExcludesPackageDetails
				(
				    PackageCode,
				    IncludeExclude,
				    Description,
				    Status
				)
				SELECT 
					@PackageCode,
					temp.IncludeExclude,
					temp.Description,
					'A'
				FROM OPENJSON(@InclusionExcludesPackageJson) 
				WITH
				(
						IncludeExclude				VARCHAR(150)			'$.IncludeExclude',
						Description					VARCHAR(MAX)			'$.Description'
				)temp
				END
				 
				 SELECT '0' code,'Package Update Successfully' Message
		    COMMIT TRANSACTION PackageUpdate
		 END TRY
		 BEGIN CATCH
		 ROLLBACK TRANSACTION PackageUpdate
		 SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
		 END CATCH
	END
END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
END CATCH
GO
