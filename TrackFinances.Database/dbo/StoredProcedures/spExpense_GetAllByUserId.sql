CREATE PROCEDURE [dbo].[spExpense_GetAllByUserId]
	@UserId nvarchar(36)
AS
BEGIN
	SELECT
		 [Id]
		,[UserId]
		,[Name]
		,[Description]
		,[Charge]
		,[ChargeDate]
		,[CategoryId]
		,[IsDirectCharge]
		,[IsCleared]
		,[DateCreated]
		,[DateModified]
	FROM [dbo].[Expense]
	WHERE [UserId] = @UserId
END