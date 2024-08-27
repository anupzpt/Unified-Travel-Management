 
  ALTER  PROCEDURE Setting.Proc_BlogManagement
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

		@BlogCode					VARCHAR(150)			= NULL,
		@Title						VARCHAR(150)			= NULL,
		@SloganName 				VARCHAR(150)			= NULL,
		@BlogImageView 				VARCHAR(250)			= NULL,
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
		WITH BlogList AS
		(
			SELECT  ROW_NUMBER() OVER (ORDER BY bd.BlogCode DESC) AS RowNum,
			        COUNT(*) OVER()  AS FilterCount,
                    bd.BlogCode,
                    bd.Title,
                    bd.SloganName,
                    bd.Status
			FROM Setting.BlogDetails AS bd 
		)
		SELECT *
		FROM BlogList AS a WHERE a.RowNum BETWEEN @FirstRec AND @LastRec
	END
	ELSE IF @Flag='GetBlogDetails'
	BEGIN
	    SELECT bd.RowId,
               bd.BlogCode,
               bd.Title,
               bd.SloganName,
               bd.Image BlogImageView,
               bd.Description,
               bd.Status
            FROM Setting.BlogDetails AS bd WHERE bd.BlogCode=@BlogCode
	END
	ELSE IF @Flag ='AddBlogDetails'
	BEGIN
 
	    BEGIN TRY
	        BEGIN TRANSACTION BlogSetup
				UPDATE Setting.Common SET BlogSeqCode= BlogSeqCode+1

				SELECT @BlogCode='Ban'+CAST(c.BlogSeqCode AS VARCHAR(150)) FROM Setting.Common AS c 
				INSERT INTO Setting.BlogDetails
				(
				    BlogCode,
				    Title,
				    SloganName,
				    Image,
				    Description,
				    Status,
				    CreatedBy,
				    CreatedDate
				)
				VALUES
				(   @BlogCode, 
				    @Title, 
				    @SloganName, 
				    @BlogImageView, 
				    @Description, 
				    'A', 
				    @UserName, 
				    GETDATE()
				)
	       COMMIT TRANSACTION BlogSetup
		   SELECT '000' Code,'Blog Information Added Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION BlogSetup
			SELECT 101 ErrorCode, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END

	ELSE IF @Flag='UpdateBlogDetails'
	BEGIN
	    BEGIN TRY
	        BEGIN TRANSACTION UpdateBlogSetup
				UPDATE Setting.BlogDetails SET 
					Title			=		@Title,
					SloganName		=		@SloganName,
					Image			=		@BlogImageView,
					Description		=		@Description,
					ModifiedBy		=		@UserName,
					ModifiedDate	=		GETDATE()
				WHERE BlogCode = @BlogCode
	       COMMIT TRANSACTION UpdateBlogSetup
		   SELECT '000' Code,'Blog Information Updated Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION UpdateBlogSetup
	    SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END
	ELSE IF @Flag='UpdateBlogStatus'
	BEGIN
	    BEGIN TRY
	        BEGIN TRANSACTION BlogStatus
				UPDATE Setting.BlogDetails SET 
					Status			=		CASE WHEN Status='A' THEN 'I' ELSE 'A'  END,
					ModifiedBy		=		@ModifiedBy,
					ModifiedDate	=		GETDATE()
				WHERE BlogCode = @BlogCode
	       COMMIT TRANSACTION BlogStatus
		   SELECT '000' Code,'Blog Status Updated Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION BlogStatus
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
