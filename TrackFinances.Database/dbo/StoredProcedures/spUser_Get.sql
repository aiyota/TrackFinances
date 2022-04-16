CREATE PROCEDURE [dbo].[spUser_Get]
	@Id nvarchar(36)
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
	WHERE [Id] = @Id
END
