CREATE TABLE [dbo].[Expense]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(1000) NULL, 
    [Charge] DECIMAL(19, 4) NOT NULL, 
    [ChargeDate] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [CategoryId] INT NULL, 
    [IsDirectCharge] BIT NOT NULL DEFAULT 0, 
    [IsCleared] BIT NOT NULL DEFAULT 0, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [DateModified] DATETIME2 NULL, 
    CONSTRAINT [FK_Expense_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_Expense_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([Id]) 
)
