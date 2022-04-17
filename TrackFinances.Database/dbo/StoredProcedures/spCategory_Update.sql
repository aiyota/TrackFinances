CREATE PROCEDURE [dbo].[spCategory_Update]
	 @Id int
	,@Name nvarchar(100) = NULL
	,@Description nvarchar(1000) = NULL
AS
BEGIN
	UPDATE [dbo].[Category]
	SET
		 [Name] = ISNULL(@Name, [Name])
		,[Description] = ISNULL(@Description, [Description])
		,[DateModified] = GETDATE()
	WHERE [Id] = @Id
END