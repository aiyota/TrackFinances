CREATE TABLE [dbo].[User]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserName] NVARCHAR(100) NOT NULL, 
    [Email] NVARCHAR(255) NOT NULL, 
    [PasswordHash] NVARCHAR(MAX) NOT NULL, 
    [EmailConfirmed] BIT NOT NULL DEFAULT 0, 
    [DateJoined] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [DateModified] DATETIME2 NULL, 
    CONSTRAINT [UQ_User_UserName] UNIQUE ([UserName]),
    CONSTRAINT [UQ_User_Email] UNIQUE ([Email]) 
)
