USE contact_keeper

CREATE TABLE [User]
(
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(128) NOT NULL,
	[Email] NVARCHAR(MAX) NOT NULL,
	[Password] NVARCHAR(MAX) NOT NULL,
	[created] DATETIME NULL DEFAULT GETDATE()
);

CREATE PROCEDURE sp_GetUsers
AS
BEGIN
SELECT [Id]
      ,[Name]
      ,[Email]
      ,[Password]
      ,[created]
  FROM [dbo].[User]
END

CREATE PROCEDURE sp_InsertNewUser
@Name nvarchar(128),
@Email nvarchar(max),
@Password nvarchar(max)
AS
BEGIN
DECLARE @Key TABLE(
	Id INT NOT NULL ,
	[created] DATETIME NULL
) 

INSERT INTO dbo.[User]
(
    [Name],
    Email,
    [Password]
)
OUTPUT Inserted.Id, Inserted.created
INTO @Key
VALUES
(   @Name,      -- Name - nvarchar(128)
    @Email,      -- Email - nvarchar(max)
    @Password      -- Password - nvarchar(max)
    )

	SELECT Id,
           created FROM @Key
END