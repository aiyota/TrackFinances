CREATE PROCEDURE [dbo].[spUser_Delete]
	@Id nvarchar(36)
AS
BEGIN
	DELETE FROM [dbo].[User]
	WHERE [Id] = @Id
END