 
  ALTER  PROCEDURE Setting.Proc_BannerManagement
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
		@ModifiedBy					VARCHAR(250)			= NULL,
		@ModifiedDate				DATETIME2				= NULL,

		@BannerCode					VARCHAR(150)			= NULL,
		@Title						VARCHAR(150)			= NULL,
		@SloganName 				VARCHAR(150)			= NULL,
		@BannerImageView 			VARCHAR(250)			= NULL,
		@Description 				VARCHAR(250)			= NULL
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='GetRequiredDetailList'
	BEGIN
		WITH bannerList AS
		(
			SELECT  ROW_NUMBER() OVER (ORDER BY bd.BannerCode DESC) AS RowNum,
			        COUNT(*) OVER()  AS FilterCount,
                    bd.BannerCode,
                    bd.Title,
                    bd.SloganName,
                    bd.Status
			FROM Setting.BannerDetails AS bd 
		)
		SELECT *
		FROM bannerList AS a WHERE a.RowNum BETWEEN @FirstRec AND @LastRec
	END
	ELSE IF @Flag='GetBannerDetails'
	BEGIN
	    SELECT bd.RowId,
               bd.BannerCode,
               bd.Title,
               bd.SloganName,
               bd.Image BannerImageView,
               bd.Description,
               bd.Status
            FROM Setting.BannerDetails AS bd WHERE bd.BannerCode=@BannerCode
	END
	ELSE IF @Flag ='AddBannerDetails'
	BEGIN
 
	    BEGIN TRY
	        BEGIN TRANSACTION BannerSetup
				UPDATE Setting.Common SET BannerSeqCode= BannerSeqCode+1

				SELECT @BannerCode='Ban'+CAST(c.BannerSeqCode AS VARCHAR(150)) FROM Setting.Common AS c 
				INSERT INTO Setting.BannerDetails
				(
				    BannerCode,
				    Title,
				    SloganName,
				    Image,
				    Description,
				    Status,
				    CreatedBy,
				    CreatedDate
				)
				VALUES
				(   @BannerCode, 
				    @Title, 
				    @SloganName, 
				    @BannerImageView, 
				    @Description, 
				    'A', 
				    @CreatedBy, 
				    GETDATE()
				)
	       COMMIT TRANSACTION BannerSetup
		   SELECT '000' Code,'Banner Information Added Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION BannerSetup
			SELECT 101 ErrorCode, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END

	ELSE IF @Flag='UpdateBannerDetails'
	BEGIN
	    BEGIN TRY
	        BEGIN TRANSACTION UpdatebannerSetup
				UPDATE Setting.BannerDetails SET 
					Title			=		@Title,
					SloganName		=		@SloganName,
					Image			=		@BannerImageView,
					Description		=		@Description,
					ModifiedBy		=		@ModifiedBy,
					ModifiedDate	=		GETDATE()
				WHERE BannerCode = @BannerCode
	       COMMIT TRANSACTION UpdatebannerSetup
		   SELECT '000' Code,'Banner Information Updated Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION UpdatebannerSetup
	    SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END
	ELSE IF @Flag='UpdateBannerStatus'
	BEGIN
	    BEGIN TRY
	        BEGIN TRANSACTION BannerStatus
				UPDATE Setting.BannerDetails SET 
					Status			=		CASE WHEN Status='A' THEN 'I' ELSE 'A'  END,
					ModifiedBy		=		@ModifiedBy,
					ModifiedDate	=		GETDATE()
				WHERE BannerCode = @BannerCode
	       COMMIT TRANSACTION BannerStatus
		   SELECT '000' Code,'Banner Status Updated Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION BannerStatus
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
