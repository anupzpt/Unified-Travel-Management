 
  ALTER  PROCEDURE Setting.Proc_GuideSetupManagement
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

        @GuideImageView				VARCHAR(150)			= NULL,		
        @GuideCode					VARCHAR(150)			= NULL,		
	    @Name						VARCHAR(150)			= NULL,
	    @Age						VARCHAR(150)			= NULL,
	    @Address					VARCHAR(150)			= NULL,
	    @CitizenshipNo				VARCHAR(150)			= NULL,
	    @PhoneNo					VARCHAR(150)			= NULL,
	    @SpecializedRegion			VARCHAR(150)			= NULL,
	    @Experience					VARCHAR(150)			= NULL,
	    @Description				VARCHAR(MAX)			= NULL
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='GetRequiredDetailList'
	BEGIN
		WITH GuideList AS
		(
			SELECT  ROW_NUMBER() OVER (ORDER BY bd.GuideCode DESC) AS RowNum,
			        COUNT(*) OVER()  AS FilterCount,
                    bd.GuideCode,
                    bd.Name,
                    bd.SpecializedRegion,
                    bd.Experience
			FROM Setting.GuideDetails AS bd 
		)
		SELECT *
		FROM GuideList AS a WHERE a.RowNum BETWEEN @FirstRec AND @LastRec
	END
	ELSE IF @Flag='GetGuideDetails'
	BEGIN
	    SELECT  bd.GuideCode,bd.Name,bd.Age,bd.Address,bd.Experience,bd.PhoneNo,bd.CitizenshipNo,bd.SpecializedRegion,bd.Status,
		bd.Image GuideImageView,bd.Description
         FROM Setting.GuideDetails AS bd WHERE bd.GuideCode=@GuideCode
	END
	ELSE IF @Flag ='AddGuideDetail'
	BEGIN
	    BEGIN TRY
	        BEGIN TRANSACTION GuideSetup
				UPDATE Setting.Common SET GuideSeqCode= GuideSeqCode+1

				SELECT @GuideCode='Gui'+CAST(c.GuideSeqCode AS VARCHAR(150)) FROM Setting.Common AS c 

				INSERT INTO Setting.GuideDetails
				(
				    GuideCode,
				    Name,
				    Age,
				    Address,
				    Experience,
				    PhoneNo,
				    CitizenshipNo,
				    SpecializedRegion,
				    Status,
				    Image,
				    Description
				)
				VALUES
				(   @GuideCode,
				    @Name,
				    @Age,
				    @Address,
				    @Experience,
				    @PhoneNo,
				    @CitizenshipNo,
				    @SpecializedRegion,
				    'A',
				    @GuideImageView,
				    @Description
				)
				 
	       COMMIT TRANSACTION GuideSetup
		   SELECT '000' Code,'Guide Information Added Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION GuideSetup
			SELECT 101 ErrorCode, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END

	ELSE IF @Flag='UpdateGuideDetail'
	BEGIN
	    BEGIN TRY
	        BEGIN TRANSACTION UpdateGuideSetup
				 UPDATE Setting.GuideDetails SET 
					Name					=   @Name,
                    Age						=	@Age,
					Address					=	@Address,
                    Experience				=	@Experience,
					PhoneNo					=	@PhoneNo,
				    CitizenshipNo			=	@CitizenshipNo,
				    SpecializedRegion		=	@SpecializedRegion,
				    Image					=	@GuideImageView,
				    Description				=	@Description
					WHERE GuideCode=@GuideCode

	       COMMIT TRANSACTION UpdateGuideSetup
		   SELECT '000' Code,'Guide Information Updated Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION UpdateGuideSetup
	    SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END
	ELSE IF @Flag='UpdateGuideStatus'
	BEGIN
	    BEGIN TRY
	        BEGIN TRANSACTION GuideStatus
				UPDATE Setting.GuideDetails SET 
					Status			=		CASE WHEN Status='A' THEN 'I' ELSE 'A'  END,
					ModifiedBy		=		@ModifiedBy,
					ModifiedDate	=		GETDATE()
				WHERE GuideCode = @GuideCode
	       COMMIT TRANSACTION GuideStatus
		   SELECT '000' Code,'Guide Status Updated Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION GuideStatus
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
