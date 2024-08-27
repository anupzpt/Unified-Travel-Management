  ALTER  PROCEDURE Setting.GalleryManagement
	(
		@Flag						NVARCHAR(100),
		@RowId					VARCHAR(100)			= NULL,
		@FromDate					VARCHAR(10)				= NULL,
		@ToDate					VARCHAR(10)				= NULL,
		@DisplayLength				INT					= NULL,					
		@DisplayStart				INT					= NULL,
		@SortCol					INT					= NULL,	
		@SortDir					NVARCHAR(10)			= NULL,	
		@Search					NVARCHAR(100)			= NULL,
		@UserName					NVARCHAR(100)			= NULL,
		@Branch					VARCHAR(100)			= NULL,
		@BranchUnit					VARCHAR(100)			= NULL,
		@Status 					VARCHAR(1)				= NULL,

		@DocumentCode				VARCHAR(150)			= NULL,
		@GalleryJson				VARCHAR(MAX)			= NULL
		
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	 IF @Flag='UploadGalleryDetail'
	BEGIN
		UPDATE Setting.Common SET DocumentSeqCode = ISNULL(DocumentSeqCode,0)+1

		SELECT @DocumentCode ='Doc'+CAST(c.DocumentSeqCode AS VARCHAR(150)) FROM Setting.Common AS c 
	    BEGIN TRY
	        BEGIN TRANSACTION UploadGallery
				INSERT INTO Setting.DocumentDetail
				(
					DocumentCode,
				    Code,
				    ImageURL
				)
				SELECT
					@DocumentCode,
					'Gallery',
					temp.Image
				FROM OPENJSON(@GalleryJson)
				WITH
				(
					Image			VARCHAR(Max)		'$.Image'
				)temp
	       COMMIT TRANSACTION UploadGallery
			SELECT 000 Code, 'Gallery Uploaded Sucessfully' Message, '' Id
	    END TRY
	    BEGIN CATCH
	    ROLLBACK TRANSACTION UploadGallery
	    SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
	    END CATCH
	END
		ELSE IF @Flag='GetGalleryDetails'
	BEGIN
		SELECT dd.RowId,dd.ImageURL GalleryView 
		FROM Setting.DocumentDetail AS dd 
		WHERE dd.Code='Gallery'
	END
	ELSE IF @Flag='RemoveImage'
	BEGIN
	    DELETE Setting.DocumentDetail WHERE RowId=@RowId
		SELECT 000 Code, 'Gallery Deleted Sucessfully' Message, '' Id

	END
	
	
END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
END CATCH
GO
