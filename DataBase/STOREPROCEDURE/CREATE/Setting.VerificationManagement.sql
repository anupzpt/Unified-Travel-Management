 
  ALTER  PROCEDURE Setting.VerificationManagement
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

		@VerificationCode			VARCHAR(150)			= NULL,
		@VerificationType			VARCHAR(150)			= NULL

)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='RegisterVerificationCode'
	BEGIN
		INSERT INTO Setting.VerificationDetails
		(
		    Username,
		    VerificationCode,
		    VerificationType,
		    Status
		)
		VALUES
		(   @UserName, -- Username - varchar(150)
		    @VerificationCode, -- VerificationCode - varchar(150)
		    @VerificationType, -- VerificationType - varchar(150)
		    'New'  -- Status - varchar(150)
		    )
			SELECT '000' Code,'Verification Code Created Successfully' Message
	    return
	END
	ELSE IF @Flag='CheckVerificationCode'
	BEGIN
	    IF EXISTS (SELECT 'a' FROM Setting.VerificationDetails AS vd WHERE vd.Username=@UserName AND vd.VerificationCode=@VerificationCode AND vd.VerificationType=@VerificationType AND vd.Status='New')
		BEGIN
		    UPDATE Setting.VerificationDetails SET 
				Status='Used'
			WHERE Username=@UserName AND VerificationCode=@VerificationCode AND VerificationType=@VerificationType AND Status='New'

			SELECT '000' Code,'Code Match' Message
		END
		ELSE
        BEGIN
			SELECT '111' Code,'Code didnot Match' Message
        END
	END
	
END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
END CATCH
GO
