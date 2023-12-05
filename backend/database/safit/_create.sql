-- DROP DATABASE [safit]

CREATE DATABASE [safit];
GO;

USE [safit];
GO;

CREATE SCHEMA sf;
GO;

CREATE TABLE sf.[user]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[email]				NVARCHAR(64) NOT NULL UNIQUE,
	[username]			NVARCHAR(32) NOT NULL UNIQUE,
	[password]			NVARCHAR(32) NOT NULL,
	[first_name]		NVARCHAR(32) NOT NULL,
	[last_name]			NVARCHAR(32) NOT NULL,
	[profile_src]		VARCHAR(65) NULL,
	[balance]			MONEY NOT NULL DEFAULT 0,
	[token]				VARCHAR(100) NOT NULL,

	PRIMARY KEY ([id])
);
GO

CREATE TABLE sf.[trainer]
(
	[id]				BIGINT NOT NULL

	PRIMARY KEY ([id]),
	FOREIGN KEY ([id]) REFERENCES sf.[user] ([id])
)
GO

CREATE TABLE sf.[subscription]
(
	[user_id]			BIGINT NOT NULL,
	[trainer_id]		BIGINT NOT NULL,

	PRIMARY KEY ([user_id], [trainer_id]),
	FOREIGN KEY ([user_id]) REFERENCES sf.[user] ([id]),
	FOREIGN KEY ([trainer_id]) REFERENCES sf.[trainer] ([id])
)
GO

CREATE TABLE sf.[sport]
(
	[id]				BIGINT NOT NULL,
	[name]				NVARCHAR(30) NOT NULL,
	[description]		NVARCHAR(200) NOT NULL,

	PRIMARY KEY ([id])				
)

CREATE TABLE sf.[specialisation]
(
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,

	PRIMARY KEY ([trainer_id], [sport_id]),
	FOREIGN KEY ([trainer_id]) REFERENCES sf.[trainer] ([id]),
	FOREIGN KEY ([sport_id]) REFERENCES sf.[sport] ([id]),
)
GO

CREATE TABLE sf.[course]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,
	[price]				MONEY NOT NULL,
	[name]				NVARCHAR(100) NOT NULL,
	[description]		NVARCHAR(400) NOT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([trainer_id], [sport_id]) REFERENCES sf.[specialisation] ([trainer_id], [sport_id])
)
GO

CREATE TABLE sf.[post]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,
	[content]			NVARCHAR(2500) NOT NULL,
	[course_id]			BIGINT NULL,
	[views]				INT NOT NULL DEFAULT 0,
	[visible]			BIT NOT NULL DEFAULT 0,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([trainer_id], [sport_id]) REFERENCES sf.[specialisation] ([trainer_id], [sport_id]),
	FOREIGN KEY ([course_id]) REFERENCES sf.[course] ([id])
)
GO

CREATE TABLE sf.[video]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,
	[course_id]			BIGINT NULL,
	[views]				INT NOT NULL DEFAULT 0,
	[visible]			BIT NOT NULL DEFAULT 0,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([trainer_id], [sport_id]) REFERENCES sf.[specialisation] ([trainer_id], [sport_id]),
	FOREIGN KEY ([course_id]) REFERENCES sf.[course] ([id])
)
GO

CREATE TABLE sf.[product]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,
	[link]				NVARCHAR(512) NOT NULL,
	[price]				MONEY NOT NULL,
	[name]				NVARCHAR(100) NOT NULL,
	[description]		NVARCHAR(400) NOT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([trainer_id], [sport_id]) REFERENCES sf.[specialisation] ([trainer_id], [sport_id]),
)
GO

CREATE TABLE sf.[like]
(
	[user_id]			BIGINT NOT NULL,
	[video_id]			BIGINT NOT NULL,

	PRIMARY KEY ([user_id], [video_id]),
	FOREIGN KEY ([user_id]) REFERENCES sf.[user] ([id]),
	FOREIGN KEY ([video_id]) REFERENCES sf.[video] ([id])
)
GO

CREATE TABLE sf.[comment]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[video_id]			BIGINT NOT NULL,
	[user_id]			BIGINT NOT NULL,
	[branch_id]			BIGINT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([user_id]) REFERENCES sf.[user] ([id]),
	FOREIGN KEY ([video_id]) REFERENCES sf.[video] ([id]),
	FOREIGN KEY ([branch_id]) REFERENCES sf.[comment] ([id])
)
GO

CREATE TABLE sf.[cart]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[user_id]			BIGINT NOT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([user_id]) REFERENCES sf.[user] ([id])
)
GO

CREATE TABLE sf.[cart_content]
(
	[cart_id]			BIGINT NOT NULL,
	[product_id]		BIGINT NOT NULL,
	[amount]			INT NOT NULL DEFAULT 0,

	PRIMARY KEY ([cart_id], [product_id]),
	FOREIGN KEY ([cart_id]) REFERENCES sf.[cart] ([id]),
	FOREIGN KEY ([product_id]) REFERENCES sf.[product] ([id])
)
GO

CREATE TABLE sf.[course_access]
(
	[user_id]			BIGINT NOT NULL,
	[course_id]			BIGINT NOT NULL,
	[purchase_dt]		DATETIME NOT NULL DEFAULT GETDATE(),

	PRIMARY KEY ([user_id], [course_id]),
	FOREIGN KEY ([user_id]) REFERENCES sf.[user] ([id]),
	FOREIGN KEY ([course_id]) REFERENCES sf.[course] ([id])
)
GO
