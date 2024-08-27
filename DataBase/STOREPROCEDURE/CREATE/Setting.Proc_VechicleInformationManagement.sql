 
  ALTER  PROCEDURE Setting.Proc_VechicleInformationManagement
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

		@VechicleCode				VARCHAR(150)			= NULL,
		@VechicleType				VARCHAR(150)			= NULL,
		@Name						VARCHAR(150)			= NULL,
		@Brand						VARCHAR(150)			= NULL,
		@Model						VARCHAR(150)			= NULL,
		@Capacity					VARCHAR(150)			= NULL,
		@RegistrationNo				NVARCHAR(150)			= NULL,
		@VechicleFeatureJson		VARCHAR(MAX)			= NULL,
		@Description				VARCHAR(MAX)			= NULL,
		@VechicleImageView			VARCHAR(MAX)			= NULL
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='GetRequiredDetailList'
	BEGIN
		WITH vechicleList AS
		(
			SELECT  ROW_NUMBER() OVER (ORDER BY bd.VechicleType DESC) AS RowNum,
			        COUNT(*) OVER()  AS FilterCount,
					bd.VechicleCode,
                    bd.VechicleType,
					bd.Name,
					bd.Brand,
					bd.Model,
					bd.RegisterNo RegistrationNo,
					bd.Status
			FROM Setting.VechicleDetails  AS bd 
			WHERE bd.VechicleType = @VechicleType
		)
		SELECT *
		FROM vechicleList AS a WHERE a.RowNum BETWEEN @FirstRec AND @LastRec
	END
	ELSE IF @Flag='GetVechicleTypeList'
	BEGIN
	     WITH vechileTypeList AS
	    (
			SELECT ROW_NUMBER() OVER (ORDER BY sdc.Id DESC) AS RowNum,
			       COUNT(*) over()  AS FilterCount, 
				   sdc.Value AS VechicleType
				   FROM Setting.StaticDataCommon AS sdc WHERE sdc.StaticCode ='VechicleType'
	    )
	    SELECT *
	    FROM vechileTypeList AS vt
	END
	ELSE IF @Flag='GetRequiredDetails'
	BEGIN
	    SELECT sdc.Value,sdc.Description FROM Setting.StaticDataCommon AS sdc WHERE sdc.StaticCode='VechicleType'
		RETURN
	END
	ELSE IF @Flag='GetVechicleInformationDetails'
	BEGIN
	    SELECT bd.RowId,
               bd.VechicleCode,
			   bd.VechicleType,
               bd.Name,
               bd.Brand,
			   bd.Model,
			   bd.Capacity,
			   bd.RegisterNo RegistrationNo,
               bd.Image VechicleImageView,
               bd.Description,
               bd.Status
            FROM Setting.VechicleDetails  AS bd WHERE bd.VechicleCode=@VechicleCode

			SELECT vfd.VechicleCode,
                   vfd.Feature VechicleFeature
				   FROM Setting.VechicleFacilitiesDetails AS vfd WHERE vfd.VechicleCode=@VechicleCode
	END
	ELSE IF @Flag ='AddVechicleDetail'
	BEGIN
	    BEGIN TRY
	        BEGIN TRANSACTION VechicleSetup
				UPDATE Setting.Common SET VechicleSeqCode = ISNULL(VechicleSeqCode,0)+1

				SELECT @VechicleCode ='Vech'+CAST(c.VechicleSeqCode AS VARCHAR(150)) FROM Setting.Common AS c 
				INSERT INTO Setting.VechicleDetails
				(
				    VechicleCode,
				    VechicleType,
				    Name,
				    Brand,
				    Model,
				    Capacity,
				    RegisterNo,
				    Status,
				    Image,
				    Description,
				    CreatedBy,
				    CreatedDate
				)
				VALUES
				(   @VechicleCode,
				    @VechicleType,
				    @Name,
				    @Brand,
				    @Model,
				    @Capacity,
				    @RegistrationNo,
				    'New',
				    @VechicleImageView,
				    @Description,
				    @CreatedBy,
				    GETDATE()
				 )
				 
				 INSERT INTO Setting.VechicleFacilitiesDetails
				 (
				     VechicleCode,
				     Feature
				 )
				 SELECT @VechicleCode,VechicleFeature 
				 FROM OPENJSON(@VechicleFeatureJson) 
				 WITH 
				 (
					VechicleFeature				VARCHAR(MAX)   '$.VechicleFeature'
				 )
	       COMMIT TRANSACTION VechicleSetup
		   SELECT '000' Code,'Vechicle Information Added Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION VechicleSetup
			SELECT 101 ErrorCode, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END

	ELSE IF @Flag='UpdateVechicleDetail'
	BEGIN
	    BEGIN TRY
	        BEGIN TRANSACTION UpdatebannerSetup
				UPDATE Setting.VechicleDetails SET 
					VechicleType			=		@VechicleType,
					Name					=		@Name,
					Brand					=		@Brand,
					Model					=		@Model,
					Capacity				=		@Capacity,
					RegisterNo				=		@RegistrationNo,
					Image					=		@VechicleImageView,
					ModifiedBy				=		@ModifiedBy,
					ModifiedDate			=		GETDATE()
				WHERE VechicleCode = @VechicleCode

				IF EXISTS (SELECT 'a' FROM Setting.VechicleFacilitiesDetails AS vfd WHERE vfd.VechicleCode = @VechicleCode)
				BEGIN
				    DELETE FROM Setting.VechicleFacilitiesDetails WHERE VechicleCode=@VechicleCode
				END

				 INSERT INTO Setting.VechicleFacilitiesDetails
				 (
				     VechicleCode,
				     Feature
				 )
				 SELECT @VechicleCode,VechicleFeature 
				 FROM OPENJSON(@VechicleFeatureJson) 
				 WITH 
				 (
					VechicleFeature				VARCHAR(MAX)   '$.VechicleFeature'
				 )
	       COMMIT TRANSACTION UpdatebannerSetup
		   SELECT '000' Code,'Vechicle Information Updated Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION UpdatebannerSetup
	    SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END
	ELSE IF @Flag='UpdateVechicleStatusDetail'
	BEGIN
	    BEGIN TRY
	        BEGIN TRANSACTION VechicleStatus
				UPDATE Setting.VechicleDetails SET 
					Status			=		CASE WHEN Status='A' THEN 'I' ELSE 'A'  END,
					ModifiedBy		=		@ModifiedBy,
					ModifiedDate	=		GETDATE()
				WHERE VechicleCode = @VechicleCode
	       COMMIT TRANSACTION VechicleStatus
		   SELECT '000' Code,'Vechicle Status Updated Sucessfully' Message
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION VechicleStatus
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
