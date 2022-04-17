CREATE PROCEDURE [dbo].[spCategory_Create]
	 @Name nvarchar(100)
	,@Description nvarchar(1000) = NULL
AS
BEGIN
	INSERT INTO [dbo].[Category]
	(
		 [Name]
		,[Description]
	) VALUES (
		 @Name
		,@Description
	);

	SELECT 
		 [Id]
		,[Name]
		,[Description]
		,[DateCreated]
		,[DateModified]
	FROM [dbo].[Category]
	WHERE [Id] = @@IDENTITY
END