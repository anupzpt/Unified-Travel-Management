 
  ALTER  PROCEDURE Setting.Proc_DashbaordManagement
	(
		@Flag							NVARCHAR(100)        = NULL,
		@RowId							VARCHAR(100)			= NULL,
		@FromDate						VARCHAR(10)				= NULL,
		@ToDate							VARCHAR(10)				= NULL,
		@DisplayLength					INT					= NULL,					
		@DisplayStart					INT					= NULL,
		@SortCol						INT					= NULL,	
		@SortDir						NVARCHAR(10)			= NULL,	
		@Search							NVARCHAR(100)			= NULL,
		@UserName						NVARCHAR(100)			= NULL,
		@Branch							VARCHAR(100)			= NULL,
		@BranchUnit						VARCHAR(100)			= NULL,
		@Status 						VARCHAR(1)				= NULL,
		@CreatedBy						VARCHAR(250)			= NULL,
		@CreatedDate					DATETIME2				= NULL,
		@VerifiedBy						VARCHAR(250)			= NULL,
		@ApprovedBy 					VARCHAR(250)			= NULL,
		@RejectedBy						VARCHAR(250)			= NULL,
		@ModifiedBy						VARCHAR(250)			= NULL,
		@VerifiedRemarks				VARCHAR(250)			= NULL,
		@ApprovedRemarks 				VARCHAR(250)			= NULL,
		@RejectedRemarks				VARCHAR(250)			= NULL,
		@ModifiedRemarks				VARCHAR(250)			= NULL,
		@RejectedMessageRemarks			VARCHAR(MAX)			= NULL,
		@RejectedDate					VARCHAR(250)			= NULL
		
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
		SET @Flag='GetDashboardDetail';
	IF @Flag='GetDashboardDetail'
	BEGIN
		SELECT 1 Code,'Data pulled without any issue' Message, '' Id
	    return
	END
	
	
	
END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
END CATCH
GO
