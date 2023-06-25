USE [PixelBrew]
GO

drop table Users;

CREATE TABLE Users (
	UserId int IDENTITY(1,1) NOT NULL,
	Username varchar(64) NOT NULL,
	FirstName varchar(128) NULL,
	LastName varchar(128) NULL,
	Email varchar(64) NULL,
	[Address] varchar(128) NULL,
	profileImage varchar(8000) NULL
CONSTRAINT PK_Users PRIMARY KEY (UserId)
)
;

INSERT INTO Users
(Username, FirstName, LastName, Email)
VALUES
('my_fancy_user', 'Bob', 'Smith', 'example@example.com')
;