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


CREATE TABLE Contacts
(
Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL DEFAULT NEWID(),
[Name] NVARCHAR(256) NOT NULL,
[Email] NVARCHAR(256) NULL,
[Phone] NVARCHAR(256) NULL,
[ContactType] NVARCHAR(50) NULL DEFAULT 'Personal',
Created DATETIME NOT NULL DEFAULT GETDATE(),
[Owner] INT NOT NULL REFERENCES dbo.[User](Id)
);

ALTER PROCEDURE sp_GetContactsByUserId
@UserId int
AS
BEGIN
SELECT C.Id,
       C.[Name],
       C.Email,
       C.Phone,
       C.ContactType,
       C.Created,
       C.[Owner],
	   U.[Name] OwnerName,
	   U.Email OwnerEmail
	   FROM dbo.Contacts C
	   JOIN dbo.[User] U
	   ON U.Id = C.[Owner]
	   WHERE [Owner] = @UserId
	   ORDER BY C.Created DESC
END

CREATE PROCEDURE sp_InsertNewContact
@Name nvarchar(256),
@Email nvarchar(256) = null,
@Phone nvarchar(256) = null,
@ContactType nvarchar(256) = null,
@Owner int
AS 
BEGIN

END