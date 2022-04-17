CREATE TABLE [dbo].[Category]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(1000) NULL,
    [DateCreated] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [DateModified] DATETIME2 NULL, 
    CONSTRAINT [UQ_Category_Name] UNIQUE ([Name])
)
