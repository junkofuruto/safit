USE [safit]

CREATE SCHEMA dev;

CREATE TABLE [dev].[user]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),

	PRIMARY KEY ([id])
);

CREATE TABLE [dev].[message]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[from_user_id]		BIGINT NOT NULL,
	[dest_user_id]		BIGINT NOT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([from_user_id]) REFERENCES [dev].[user] ([id]),
	FOREIGN KEY ([dest_user_id]) REFERENCES [dev].[user] ([id])
)

CREATE TABLE [dev].[attachment]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[message_id]		BIGINT NOT NULL,

	FOREIGN KEY ([message_id]) REFERENCES [dev].[message] ([id])
)

CREATE TABLE [dev].[trainer]
(
	[id]				BIGINT NOT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([id]) REFERENCES [dev].[user] ([id])
)

CREATE TABLE [dev].[subscription]
(
	[user_id]			BIGINT NOT NULL,
	[trainer_id]		BIGINT NOT NULL,

	PRIMARY KEY ([user_id], [trainer_id]),
	FOREIGN KEY ([user_id]) REFERENCES [dev].[user] ([id]),
	FOREIGN KEY ([trainer_id]) REFERENCES [dev].[trainer] ([id])
)

CREATE TABLE [dev].[sport]
(
	[id]				BIGINT NOT NULL,

	PRIMARY KEY ([id])				
)

CREATE TABLE [dev].[trainer_sport]
(
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,

	PRIMARY KEY ([trainer_id], [sport_id]),
	FOREIGN KEY ([trainer_id]) REFERENCES [dev].[trainer] ([id]),
	FOREIGN KEY ([sport_id]) REFERENCES [dev].[sport] ([id]),
)

CREATE TABLE [dev].[course]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([trainer_id], [sport_id]) REFERENCES [dev].[trainer_sport] ([trainer_id], [sport_id])
)

CREATE TABLE [dev].[post]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,
	[course_id]			BIGINT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([trainer_id], [sport_id]) REFERENCES [dev].[trainer_sport] ([trainer_id], [sport_id]),
	FOREIGN KEY ([course_id]) REFERENCES [dev].[course] ([id])
)

CREATE TABLE [dev].[video]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,
	[course_id]			BIGINT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([trainer_id], [sport_id]) REFERENCES [dev].[trainer_sport] ([trainer_id], [sport_id]),
	FOREIGN KEY ([course_id]) REFERENCES [dev].[course] ([id])
)

CREATE TABLE [dev].[product]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([trainer_id], [sport_id]) REFERENCES [dev].[trainer_sport] ([trainer_id], [sport_id]),
)

CREATE TABLE [dev].[timecode]
(
	[video_id]			BIGINT NOT NULL,
	[timing]			TIME NOT NULL,
	[product_id]		BIGINT NULL,
	[course_id]			BIGINT NULL,
	[post_id]			BIGINT NULL,
	[trainer_id]		BIGINT NULL,

	PRIMARY KEY ([video_id], [timing]),
	FOREIGN KEY ([product_id]) REFERENCES [dev].[product] ([id]),
	FOREIGN KEY ([course_id]) REFERENCES [dev].[course] ([id]),
	FOREIGN KEY ([post_id]) REFERENCES [dev].[post] ([id]),
	FOREIGN KEY ([trainer_id]) REFERENCES [dev].[trainer] ([id])
)

CREATE TABLE [dev].[like]
(
	[user_id]			BIGINT NOT NULL,
	[video_id]			BIGINT NOT NULL,

	PRIMARY KEY ([user_id], [video_id]),
	FOREIGN KEY ([user_id]) REFERENCES [dev].[user] ([id]),
	FOREIGN KEY ([video_id]) REFERENCES [dev].[video] ([id])
)

CREATE TABLE [dev].[comment]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[video_id]			BIGINT NOT NULL,
	[user_id]			BIGINT NOT NULL,
	[branch_id]			BIGINT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([user_id]) REFERENCES [dev].[user] ([id]),
	FOREIGN KEY ([video_id]) REFERENCES [dev].[video] ([id]),
	FOREIGN KEY ([branch_id]) REFERENCES [dev].[comment] ([id])
)

CREATE TABLE [dev].[cart]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[user_id]			BIGINT NOT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([user_id]) REFERENCES [dev].[user] ([id])
)

CREATE TABLE [dev].[cart_content]
(
	[cart_id]			BIGINT NOT NULL,
	[product_id]		BIGINT NOT NULL,

	PRIMARY KEY ([cart_id], [product_id]),
	FOREIGN KEY ([cart_id]) REFERENCES [dev].[cart] ([id]),
	FOREIGN KEY ([product_id]) REFERENCES [dev].[product] ([id])
)

CREATE TABLE [dev].[course_access]
(
	[user_id]			BIGINT NOT NULL,
	[course_id]			BIGINT NOT NULL,

	PRIMARY KEY ([user_id], [course_id]),
	FOREIGN KEY ([user_id]) REFERENCES [dev].[user] ([id]),
	FOREIGN KEY ([course_id]) REFERENCES [dev].[course] ([id])
)