CREATE DATABASE [safit];
GO;

USE [safit];
GO;

CREATE SCHEMA sf;
GO;

CREATE TABLE sf.[user]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),

	PRIMARY KEY ([id])
);

CREATE TABLE sf.[message]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[from_user_id]		BIGINT NOT NULL,
	[dest_user_id]		BIGINT NOT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([from_user_id]) REFERENCES sf.[user] ([id]),
	FOREIGN KEY ([dest_user_id]) REFERENCES sf.[user] ([id])
)

CREATE TABLE sf.[attachment]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[message_id]		BIGINT NOT NULL,

	FOREIGN KEY ([message_id]) REFERENCES sf.[message] ([id])
)

CREATE TABLE sf.[trainer]
(
	[id]				BIGINT NOT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([id]) REFERENCES sf.[user] ([id])
)

CREATE TABLE sf.[subscription]
(
	[user_id]			BIGINT NOT NULL,
	[trainer_id]		BIGINT NOT NULL,

	PRIMARY KEY ([user_id], [trainer_id]),
	FOREIGN KEY ([user_id]) REFERENCES sf.[user] ([id]),
	FOREIGN KEY ([trainer_id]) REFERENCES sf.[trainer] ([id])
)

CREATE TABLE sf.[sport]
(
	[id]				BIGINT NOT NULL,

	PRIMARY KEY ([id])				
)

CREATE TABLE sf.[trainer_sport]
(
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,

	PRIMARY KEY ([trainer_id], [sport_id]),
	FOREIGN KEY ([trainer_id]) REFERENCES sf.[trainer] ([id]),
	FOREIGN KEY ([sport_id]) REFERENCES sf.[sport] ([id]),
)

CREATE TABLE sf.[course]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([trainer_id], [sport_id]) REFERENCES sf.[trainer_sport] ([trainer_id], [sport_id])
)

CREATE TABLE sf.[post]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,
	[course_id]			BIGINT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([trainer_id], [sport_id]) REFERENCES sf.[trainer_sport] ([trainer_id], [sport_id]),
	FOREIGN KEY ([course_id]) REFERENCES sf.[course] ([id])
)

CREATE TABLE sf.[video]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,
	[course_id]			BIGINT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([trainer_id], [sport_id]) REFERENCES sf.[trainer_sport] ([trainer_id], [sport_id]),
	FOREIGN KEY ([course_id]) REFERENCES sf.[course] ([id])
)

CREATE TABLE sf.[product]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[trainer_id]		BIGINT NOT NULL,
	[sport_id]			BIGINT NOT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([trainer_id], [sport_id]) REFERENCES sf.[trainer_sport] ([trainer_id], [sport_id]),
)

CREATE TABLE sf.[timecode]
(
	[video_id]			BIGINT NOT NULL,
	[timing]			TIME NOT NULL,
	[product_id]		BIGINT NULL,
	[course_id]			BIGINT NULL,
	[post_id]			BIGINT NULL,
	[trainer_id]		BIGINT NULL,

	PRIMARY KEY ([video_id], [timing]),
	FOREIGN KEY ([product_id]) REFERENCES sf.[product] ([id]),
	FOREIGN KEY ([course_id]) REFERENCES sf.[course] ([id]),
	FOREIGN KEY ([post_id]) REFERENCES sf.[post] ([id]),
	FOREIGN KEY ([trainer_id]) REFERENCES sf.[trainer] ([id])
)

CREATE TABLE sf.[like]
(
	[user_id]			BIGINT NOT NULL,
	[video_id]			BIGINT NOT NULL,

	PRIMARY KEY ([user_id], [video_id]),
	FOREIGN KEY ([user_id]) REFERENCES sf.[user] ([id]),
	FOREIGN KEY ([video_id]) REFERENCES sf.[video] ([id])
)

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

CREATE TABLE sf.[cart]
(
	[id]				BIGINT NOT NULL IDENTITY(0, 1),
	[user_id]			BIGINT NOT NULL,

	PRIMARY KEY ([id]),
	FOREIGN KEY ([user_id]) REFERENCES sf.[user] ([id])
)

CREATE TABLE sf.[cart_content]
(
	[cart_id]			BIGINT NOT NULL,
	[product_id]		BIGINT NOT NULL,

	PRIMARY KEY ([cart_id], [product_id]),
	FOREIGN KEY ([cart_id]) REFERENCES sf.[cart] ([id]),
	FOREIGN KEY ([product_id]) REFERENCES sf.[product] ([id])
)

CREATE TABLE sf.[course_access]
(
	[user_id]			BIGINT NOT NULL,
	[course_id]			BIGINT NOT NULL,

	PRIMARY KEY ([user_id], [course_id]),
	FOREIGN KEY ([user_id]) REFERENCES sf.[user] ([id]),
	FOREIGN KEY ([course_id]) REFERENCES sf.[course] ([id])
)

CREATE TABLE sf.[video_part]
(
	[part_id]			BIGINT NOT NULL IDENTITY(0, 1),
	[video_id]			BIGINT NOT NULL,
	[source]			VARCHAR(65) NOT NULL,

	PRIMARY KEY ([part_id], [video_id]),
	FOREIGN KEY ([video_id]) REFERENCES sf.video ([id])
)