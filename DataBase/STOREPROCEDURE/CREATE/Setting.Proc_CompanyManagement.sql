 
  ALTER  PROCEDURE Setting.Proc_CompanyManagement
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
		@Status 					VARCHAR(1)				= NULL,
		@CreatedBy					VARCHAR(250)			= NULL,
		@CreatedDate				DATETIME2				= NULL,
		@ModifiedBy					VARCHAR(250)			= NULL,
		@ModifiedDate				DATETIME2				= NULL,
		 
		@CompanyCode				VARCHAR(150)			= NULL,
		@CompanyName 				VARCHAR(150)			= NULL,
		@Description 				VARCHAR(MAX)			= NULL,
		@PopularServices 			VARCHAR(150)			= NULL,
		@CompanyImageView 			VARCHAR(250)			= NULL
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='GetCompanySetupDetails'
	BEGIN
		SELECT cd.CompanyCode,
               cd.CompanyName,
               cd.PopularServices,
               cd.Image CompanyImageView,
               cd.Description,
               cd.Status
		FROM Setting.CompanyDetails AS cd
	END
	ELSE IF @Flag='AddCompanyDetails'
	BEGIN
		UPDATE Setting.Common SET CommpanySeqCode = ISNULL(CommpanySeqCode,0) + 1
		SELECT @CompanyCode = 'COMP'+ CAST(c.CommpanySeqCode AS VARCHAR(150) ) FROM Setting.Common AS c
	    BEGIN TRY
	        BEGIN TRANSACTION AddCompany
				INSERT INTO Setting.CompanyDetails
				(
				    CompanyCode,
				    CompanyName,
				    PopularServices,
				    Image,
				    Description,
				    Status,
				    CreatedBy,
				    CreatedDate
				)
				VALUES
				(   @CompanyCode, 
				    @CompanyName, 
				    @PopularServices, 
				    @CompanyImageView, 
				    @Description, 
				    'A', 
				    @CreatedBy, 
				    GETDATE() 
				   
				 )
	       COMMIT TRANSACTION AddCompany
		   SELECT '000' Code,'Company Information Added Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION AddCompany
			SELECT 111 Code, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END
	ELSE IF @Flag='UpdateCompanyDetails'
	BEGIN
	    BEGIN TRY
	        BEGIN TRANSACTION UpdateCompanyDetails
				 UPDATE Setting.CompanyDetails SET 
					CompanyName			=			@CompanyName,
					PopularServices		=			@PopularServices,
					Image				=			@CompanyImageView,
					Description			=			@Description,
					ModifiedBy			=			@ModifiedBy,
					ModifiedDate		=			GETDATE()
	       COMMIT TRANSACTION UpdateCompanyDetails
		   SELECT '000' Code,'Company Information Update Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION UpdateCompanyDetails
			SELECT 111 Code, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END
	
END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 111 Code, ERROR_MESSAGE() Message, '' Id
END CATCH
GO
