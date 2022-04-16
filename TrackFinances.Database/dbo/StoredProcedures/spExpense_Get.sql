CREATE PROCEDURE [dbo].[spExpense_Get]
	@Id int
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
	WHERE [Id] = @Id
END