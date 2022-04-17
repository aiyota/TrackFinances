CREATE PROCEDURE [dbo].[spCategory_GetAll]
AS
BEGIN
	SELECT
		 [Id]
		,[Name]
		,[Description]
		,[DateCreated]
		,[DateModified]
	FROM [dbo].[Category]
END