 
  ALTER  PROCEDURE Setting.Proc_FacilitySetupManagement
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
		@ModifiedBy						VARCHAR(250)			= NULL,
		@User							VARCHAR(250)			= NULL,

		@CategoryCode					VARCHAR(250)			= NULL,
		@CategoryName					VARCHAR(250)			= NULL
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='GetRequiredDetailList'
	BEGIN
	 WITH facilityDetails AS
	(
		SELECT 
			ROW_NUMBER() OVER (ORDER BY fd.Rowid DESC) AS RowNum,
			COUNT(*) over()  AS FilterCount,
			fd.CategoryCode,
			fd.CategoryName
		FROM Setting.FacilityDetails AS fd
	)
	SELECT *
	FROM facilityDetails
	    return
	END
	ELSE IF @Flag='AddFacilitySetup'
	BEGIN
	    INSERT INTO Setting.FacilityDetails
	    (
	        CategoryCode,
	        CategoryName,
	        CreatedBy,
	        CreatedDate
	    )
	    VALUES
	    (   @CategoryCode,
	        @CategoryName,
	        @UserName,
	        GETDATE() 
	     ) 
		SELECT 0 ErrorCode,'Facility Category Setup Sucessfully' Message, '' Id
	END
	ELSE IF @Flag='GetFacilityDetails'
	BEGIN
	    SELECT fd.CategoryCode,fd.CategoryName FROM Setting.FacilityDetails AS fd WHERE fd.CategoryCode=@CategoryCode
	END
	
	
END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 1 ErrorCode, ERROR_MESSAGE() Message, '' Id
END CATCH
GO
