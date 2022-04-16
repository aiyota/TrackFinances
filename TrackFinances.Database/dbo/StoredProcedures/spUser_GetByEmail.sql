CREATE PROCEDURE [dbo].[spUser_GetByEmail]
	@Email nvarchar(255)
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
	WHERE [Email] = @Email
END