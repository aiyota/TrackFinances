CREATE PROCEDURE [dbo].[spExpense_Create]
	 @UserId nvarchar(36)
	,@Name nvarchar(100)
	,@Description nvarchar(1000) = NULL
	,@Charge decimal(19,4)
	,@ChargeDate datetime2(7) = NULL
	,@CategoryId int
	,@IsDirectCharge bit = NULL
	,@IsCleared bit = NULL
AS
BEGIN
	INSERT INTO [dbo].[Expense]
	(
		 [UserId]
		,[Name]
		,[Description]
		,[Charge]
		,[ChargeDate]
		,[CategoryId]
		,[IsDirectCharge]
		,[IsCleared]
	) VALUES (
		 @UserId
		,@Name
		,@Description
		,@Charge
		,ISNULL(@ChargeDate, GETDATE())
		,@CategoryId
		,ISNULL(@IsDirectCharge, 0)
		,ISNULL(@IsCleared, 0)
	);

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
	WHERE [Id] = @@IDENTITY;
END
