CREATE PROCEDURE [dbo].[spUser_Create]
	 @UserName nvarchar(100)
	,@Email nvarchar(255)
	,@PasswordHash nvarchar(MAX)
AS
BEGIN 
	INSERT INTO [dbo].[User]
	(
		 [UserName]
		,[Email]
		,[PasswordHash]
	) VALUES (
		 @UserName
		,@Email
		,@PasswordHash
	)
END