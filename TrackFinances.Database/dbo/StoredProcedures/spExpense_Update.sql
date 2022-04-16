CREATE PROCEDURE [dbo].[spExpense_Update]
	 @Id int
	,@UserId nvarchar(36) = NULL
	,@Name nvarchar(100) = NULL
	,@Description nvarchar(1000) = NULL
	,@Charge decimal(19,4) = NULL
	,@ChargeDate datetime2(7) = NULL
	,@CategoryId int = NULL
	,@IsDirectCharge bit = NULL
	,@IsCleared bit = NULL
AS
BEGIN
	UPDATE [dbo].[Expense]
	SET
		 [UserId] = ISNULL(@UserId, [UserId])
		,[Name] = ISNULL(@Name, [Name])
		,[Description] = ISNULL(@Description, [Description])
		,[Charge] = ISNULL(@Charge, [Charge])
		,[ChargeDate] = ISNULL(@ChargeDate, [ChargeDate])
		,[CategoryId] = ISNULL(@CategoryId, [CategoryId])
		,[IsDirectCharge] = ISNULL(@IsDirectCharge, [IsDirectCharge])
		,[IsCleared] = ISNULL(@IsCleared, [IsCleared])
		,[DateModified] = GETDATE()
	WHERE [Id] = @Id
END