 
  ALTER  PROCEDURE Setting.Proc_DriverManagement
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

		@DriverCode					VARCHAR(150)			= NULL,
		@FullName					VARCHAR(150)			= NULL,
		@Age 						VARCHAR(50)				= NULL,
		@ContactNo 					VARCHAR(150)			= NULL,
		@LiscineNo 					VARCHAR(150)			= NULL,
		@Experience 				BIGINT					= NULL,
		@DriverImageView 			VARCHAR(MAX)			= NULL
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='GetRequiredDetailList'
	BEGIN
		WITH DriverList AS
		(
			SELECT  ROW_NUMBER() OVER (ORDER BY bd.DriverCode DESC) AS RowNum,
			        COUNT(*) OVER()  AS FilterCount,
                    bd.DriverCode,
					bd.FullName,
                    bd.Status
			FROM Setting.DriverDetails AS bd 
		)
		SELECT *
		FROM DriverList AS a WHERE a.RowNum BETWEEN @FirstRec AND @LastRec
	END
	ELSE IF @Flag='GetDriverDetails'
	BEGIN
	    SELECT bd.DriverCode,
               bd.FullName,
               bd.Age,
			   bd.ContactNo,
			   bd.LiscineNo,
			   bd.Experience,
			   bd.Image DriverImageView,
			   bd.Status
            FROM Setting.DriverDetails AS bd WHERE bd.DriverCode=@DriverCode
	END
	ELSE IF @Flag ='AddDriverDetails'
	BEGIN
 
	    BEGIN TRY
	        BEGIN TRANSACTION DriverSetup
				UPDATE Setting.Common SET DriverSeqCode=ISNULL(DriverSeqCode,0)+1

				SELECT @DriverCode='Driv'+CAST(c.DriverSeqCode AS VARCHAR(150)) FROM Setting.Common AS c 
				INSERT INTO Setting.DriverDetails
				(
				    DriverCode,
				    FullName,
					Age,
					ContactNo,
					LiscineNo,
					Experience,
					Image,
				    Status
				)
				VALUES
				(   @DriverCode, 
					@FullName,
					@Age,
					@ContactNo,
					@LiscineNo,
					@Experience,
					@DriverImageView,
				    'A'
				)
	       COMMIT TRANSACTION DriverSetup
		   SELECT '000' Code,'Driver Information Added Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION DriverSetup
			SELECT 101 ErrorCode, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END

	ELSE IF @Flag='UpdateDriverDetails'
	BEGIN
	    BEGIN TRY
	        BEGIN TRANSACTION UpdateDriverSetup
				UPDATE Setting.DriverDetails SET 
						FullName			=		@FullName,
						Age					=		@Age,
						ContactNo			=		@ContactNo,
						LiscineNo			=		@LiscineNo,
						Experience			=		@Experience,
						Image				=		@DriverImageView
				WHERE DriverCode = @DriverCode
	       COMMIT TRANSACTION UpdateDriverSetup
		   SELECT '000' Code,'Driver Information Updated Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION UpdateDriverSetup
	    SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END
	ELSE IF @Flag='UpdateDriverStatus'
	BEGIN
	    BEGIN TRY
	        BEGIN TRANSACTION DriverStatus
				UPDATE Setting.DriverDetails SET 
					Status			=		CASE WHEN Status='A' THEN 'I' ELSE 'A'  END,
					ModifiedBy		=		@ModifiedBy,
					ModifiedDate	=		GETDATE()
				WHERE DriverCode = @DriverCode
	       COMMIT TRANSACTION DriverStatus
		   SELECT '000' Code,'Driver Status Updated Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION DriverStatus
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
