CREATE PROCEDURE [dbo].[spExpense_Delete]
	@Id int
AS
BEGIN
	DELETE FROM [dbo].[Expense]
	WHERE [dbo].[Expense].[Id] = @Id
END