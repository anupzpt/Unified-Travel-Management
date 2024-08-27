  alter  PROCEDURE Setting.Proc_HomePageManagement
	(
		@Flag						NVARCHAR(100),
		@RowId						VARCHAR(100)			= NULL,
		@FromDate					VARCHAR(10)				= NULL,
		@ToDate						VARCHAR(10)				= NULL,
		@DisplayLength				INT						= NULL,					
		@DisplayStart				INT						= NULL,
		@SortCol					INT						= NULL,	
		@SortDir					NVARCHAR(10)			= NULL,	
		@Search						NVARCHAR(100)			= NULL,
		@UserName					NVARCHAR(100)			= NULL,
		@Branch						VARCHAR(100)			= NULL,
		@BranchUnit					VARCHAR(100)			= NULL,
		@Status 					VARCHAR(1)				= NULL,
		@CreatedBy					VARCHAR(250)			= NULL,
		@CreatedDate				DATETIME2				= NULL,
		@BlogCode					VARCHAR(150)			= NULL,
		@PackageCode				VARCHAR(150)			= NULL,
		@HotelCode					VARCHAR(150)			= NULL

)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='GetDashboardDetails'
	BEGIN
		SELECT bd.Title,bd.SloganName,bd.Image BannerImageView,bd.Description FROM Setting.BannerDetails AS bd
		WHERE bd.Status='A'

		SELECT * FROM Setting.PackageDetails AS pd

		SELECT dd.ImageURL GalleryView FROM Setting.DocumentDetail AS dd WHERE dd.Code='Gallery'

		SELECT TOP 3 ud.FullName UserName,bd.Title,bd.SloganName,bd.BlogCode, bd.Image BlogImageView
		FROM Setting.BlogDetails AS bd
		INNER JOIN Setting.UserDetails AS ud ON ud.Username=bd.CreatedBy 

		SELECT TOP 4 gd.Name,gd.Experience,gd.GuideCode,gd.Image GuideImageView FROM Setting.GuideDetails AS gd 
 	    return
	END
	ELSE IF @Flag='GetBookingList'
	BEGIN
	    SELECT  bid.Code,bid.CodeType,bid.Name,bid.TotalAmount,bid.BookedDate,bid.STATUS Status INTO #tempBookingList
		FROM Setting.BookingInformationDetail AS bid 
		WHERE bid.BookedBy=@UserName
		UNION ALL
		SELECT pd.PackageCode,'Package',pd.PackageName,pd.PackageAverageCost,NULL,pd.Status Status
		FROM Setting.PackageDetails AS pd
		WHERE pd.CreatedBy=@UserName;

		WITH bookingList AS
		(
			SELECT ROW_NUMBER() OVER (ORDER BY tbl.Code DESC) AS RowNum,
			       COUNT(*) over()  AS FilterCount,tbl.Code,tbl.CodeType,tbl.Name,tbl.TotalAmount,tbl.BookedDate,tbl.Status
				   FROM #tempBookingList AS tbl 
		)
		SELECT *
		FROM bookingList AS b
		WHERE b.RowNum BETWEEN @FirstRec AND @LastRec
		
		DROP TABLE #tempBookingList
	END
	ELSE IF @Flag='GetBlogPostDetails'
	BEGIN
	    SELECT bd.Title,bd.SloganName,bd.Description,bd.Image BlogImageView,ud.FullName UserName,CONVERT(VARCHAR(10),bd.CreatedDate,103)CreatedDate
		FROM Setting.BlogDetails AS bd 
		INNER JOIN Setting.UserDetails AS ud ON ud.Username=bd.CreatedBy
		WHERE bd.BlogCode=@BlogCode
	END
	ELSE IF @Flag='GetBookedPackageDetails'
	BEGIN
	    		SELECT CONVERT(VARCHAR(10),ISNULL(pd.CreatedDate,GETDATE()),103)CreatedDate,pd.PackageCode,pd.CreatedBy,pd.PackageName,pd.Duration,CONVERT(DECIMAL(10,2),bid.TotalAmount) PackageAverageCost
		FROM Setting.PackageDetails AS pd 
		INNER JOIN Setting.BookingInformationDetail AS bid ON bid.Code=pd.PackageCode
		WHERE pd.PackageCode=@PackageCode

		SELECT 
		pid.Title,pid.Accomodation,pid.Description
		FROM Package.PackageItineraryDetails AS pid
		WHERE pid.PackageCode=@PackageCode
	END
	ELSE IF @Flag='GetBookedHotelDetails'
	BEGIN
	    SELECT hd.HotelCode,hd.HotelName,'2000' TotalPrice 
		FROM Setting.HotelDetails AS hd 
		WHERE hd.HotelCode=@HotelCode
	END
	
END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
END CATCH
GO
