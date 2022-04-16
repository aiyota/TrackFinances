CREATE PROCEDURE [dbo].[spUser_Update]
	 @Id nvarchar(36)
	,@UserName nvarchar(100) = NULL
	,@Email nvarchar(255) = NULL
	,@PasswordHash nvarchar(MAX) = NULL
	,@EmailConfirmed bit = NULL
AS
BEGIN
	UPDATE [dbo].[User]
	SET
		 [UserName] = ISNULL(@UserName, [UserName])
		,[Email] = ISNULL(@Email, [Email])
		,[PasswordHash] = ISNULL(@PasswordHash, [PasswordHash])
		,[EmailConfirmed] = ISNULL(@EmailConfirmed, [EmailConfirmed])
		,[DateModified] = GETDATE()
	WHERE [Id] = @Id
END
