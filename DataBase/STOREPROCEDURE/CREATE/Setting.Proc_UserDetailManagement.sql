 
  ALTER  PROCEDURE Setting.Proc_UserDetailManagement
	(
		@Flag						NVARCHAR(100),
		@RowId						VARCHAR(100)			= NULL,
		@FromDate					VARCHAR(10)				= NULL,
		@ToDate						VARCHAR(10)				= NULL,
		@DisplayLength				INT						= NULL,					
		@DisplayStart				INT						= NULL,
		@SortCol					INT						= NULL,	
		@Code						INT						= NULL,	
		@SortDir					NVARCHAR(10)			= NULL,	
		@Search						NVARCHAR(100)			= NULL,
		@UserName					NVARCHAR(100)			= NULL,
		@Branch						VARCHAR(100)			= NULL,
		@BranchUnit					VARCHAR(100)			= NULL,
		@Status 					VARCHAR(1)				= NULL,
		@CreatedBy					VARCHAR(250)			= NULL,
		@CreatedDate				DATETIME2				= NULL,

		@FullName					VARCHAR(150)			= NULL,
		@ContactNo					VARCHAR(150)			= NULL,
		@EmailId					VARCHAR(150)			= NULL,
		@IsAdmin					VARCHAR(150)			= NULL,
		@IsSupperUser				VARCHAR(150)			= NULL,
		@UserPassword				VARCHAR(150)			= NULL,
		@KeepLoggedIn				VARCHAR(150)			= NULL

)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='RegisterUserDetails'
	BEGIN
		BEGIN TRY
		    BEGIN TRANSACTION RegisterUser
				INSERT INTO Setting.UserDetails
				(
				    Username,
				    FullName,
				    ContactNo,
				    EmailId,
				    IsAdmin,
				    IsSupperUser,
				    UserPassword,
					CreatedBy,
					CreatedDate
				)
				VALUES
				(   @UserName,
				    @FullName,
				    @ContactNo,
				    @EmailId,
				    0,
				    0,
				    @UserPassword,
					@UserName,
					GETDATE()
				 )
		   COMMIT TRANSACTION RegisterUser
		   SELECT '000' Code, 'User Register Successfully' Message
		END TRY
		BEGIN CATCH
		ROLLBACK TRANSACTION RegisterUser
		SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
		END CATCH
	END
	ELSE IF @Flag='CheckUserLogin'
	BEGIN
	   IF EXISTS( SELECT 'a' FROM Setting.UserDetails AS ud WHERE ud.Username=@UserName AND ud.UserPassword=@UserPassword)
	   BEGIN
			IF EXISTS( SELECT 'a' FROM Setting.UserDetails AS ud WHERE ud.Username=@UserName AND ud.UserPassword=@UserPassword AND ud.KeepLoggedIn=1)
			BEGIN
				UPDATE Setting.UserDetails SET 
					KeepLoggedIn = @KeepLoggedIn,
					LoginCount = ISNULL(LoginCount,0)+1
				WHERE Username=@UserName AND UserPassword=@UserPassword
			END
			ELSE 
			BEGIN
				UPDATE Setting.UserDetails SET 
					KeepLoggedIn = @KeepLoggedIn,
					LoginCount = 1
				WHERE Username=@UserName AND UserPassword=@UserPassword
			END

			SELECT '000' Code, ud.FullName,ud.Username,ud.EmailId,ud.ContactNo,ud.IsAdmin,ud.IsSupperUser FROM Setting.UserDetails AS ud WHERE ud.Username=@UserName AND ud.UserPassword=@UserPassword
			RETURN
	   END 
	   ELSE
	   BEGIN
	       SELECT '111' Code 
		   RETURN
	   END
	END
	ELSE IF @Flag='UpdateUserInformation'
	BEGIN
	    UPDATE Setting.UserDetails SET 
				FullName = @FullName,
				ContactNo= @ContactNo,
				EmailId	 =@EmailId
		WHERE Username=@UserName
		SELECT '0' Code,'User Detail Updated Successfully' Message
	END
	
	
END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
END CATCH
GO
