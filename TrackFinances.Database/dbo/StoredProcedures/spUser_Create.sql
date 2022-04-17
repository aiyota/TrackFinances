CREATE PROCEDURE [dbo].[spUser_Create]
	 @UserName nvarchar(100)
	,@Email nvarchar(255)
	,@PasswordHash nvarchar(MAX)
AS
BEGIN
	DECLARE @Id uniqueidentifier = NEWID()
	INSERT INTO [dbo].[User]
	(
		 [Id]
		,[UserName]
		,[Email]
		,[PasswordHash]
	) VALUES (
		 @Id
		,@UserName
		,@Email
		,@PasswordHash
	);

	SELECT 
		 [Id]
		,[UserName]
		,[Email]
		,[PasswordHash]
		,[EmailConfirmed]
		,[DateJoined]
		,[DateModified] FROM [dbo].[User]
	WHERE [Id] = @Id
END