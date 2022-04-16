CREATE PROCEDURE [dbo].[spUser_GetByUserName]
	@UserName nvarchar(100)
AS
BEGIN
	SELECT 
		 [Id]
		,[UserName]
		,[Email]
		,[PasswordHash]
		,[EmailConfirmed]
		,[DateJoined]
		,[DateModified]
	FROM [dbo].[User]
	WHERE [UserName] = @UserName
END