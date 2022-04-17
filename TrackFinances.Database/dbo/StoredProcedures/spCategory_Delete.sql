CREATE PROCEDURE [dbo].[spCategory_Delete]
	@Id int
AS
BEGIN
	DELETE FROM [dbo].[Category]
	WHERE [Id] = @Id
END
