USE [safit]

CREATE TABLE dbo.sf_user
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[username]			NVARCHAR(30) NOT NULL UNIQUE,
	[password]			NVARCHAR(32) NOT NULL,
	
	PRIMARY KEY ([id])
)

CREATE TABLE dbo.