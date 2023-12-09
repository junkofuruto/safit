USE [safit]
GO

CREATE TYPE sf.tag_list AS TABLE ([name] NVARCHAR(65) NOT NULL UNIQUE)
GO

CREATE PROCEDURE sf.pc_tag_add_video
(
	@p_trainer_id BIGINT,
	@p_token VARCHAR(100),
	@p_video_id BIGINT,
	@p_tag_list sf.tag_list READONLY
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_trainer_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	DECLARE @is_trainer BIT;
	EXEC sf.pc_trainer_is_promoted @p_trainer_id, @is_trainer OUTPUT;
	IF (@is_trainer = 0) BEGIN SELECT 0 AS [success], 'NOT_TRAINER' AS [message] RETURN END;
	IF NOT EXISTS(SELECT * FROM sf.video WHERE [id] = @p_video_id AND [trainer_id] = @p_trainer_id) BEGIN
		SELECT 0 AS [success], 'NOT_OWNER' AS [message] RETURN
	END ELSE BEGIN
		INSERT INTO sf.tag SELECT TL.[name] FROM @p_tag_list TL WHERE NOT EXISTS(SELECT * FROM sf.tag WHERE [name] <> TL.[name]);
		INSERT INTO sf.video_tag SELECT @p_video_id, [name] FROM @p_tag_list;
		SELECT 1 AS [success], 'TAGS_ADDED' AS [message] RETURN;
	END
END
GO

CREATE PROCEDURE sf.pc_tag_add_post
(
	@p_trainer_id BIGINT,
	@p_token VARCHAR(100),
	@p_post_id BIGINT,
	@p_tag_list sf.tag_list READONLY
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_trainer_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	DECLARE @is_trainer BIT;
	EXEC sf.pc_trainer_is_promoted @p_trainer_id, @is_trainer OUTPUT;
	IF (@is_trainer = 0) BEGIN SELECT 0 AS [success], 'NOT_TRAINER' AS [message] RETURN END;
	IF NOT EXISTS(SELECT * FROM sf.post WHERE [id] = @p_post_id AND [trainer_id] = @p_trainer_id) BEGIN
		SELECT 0 AS [success], 'NOT_OWNER' AS [message] RETURN
	END ELSE BEGIN
		INSERT INTO sf.tag SELECT TL.[name] FROM @p_tag_list TL WHERE NOT EXISTS(SELECT * FROM sf.tag WHERE [name] <> TL.[name]);
		INSERT INTO sf.post_tag SELECT @p_post_id, [name] FROM @p_tag_list;
		SELECT 1 AS [success], 'TAGS_ADDED' AS [message] RETURN;
	END
END
GO