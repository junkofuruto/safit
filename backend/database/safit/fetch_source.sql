USE [safit]
GO

CREATE TYPE sf.fetch_list AS TABLE ([source] VARCHAR(100) UNIQUE)
GO

CREATE PROCEDURE sf.pc_fetch_source_add
(
	@p_trainer_id BIGINT,
	@p_token VARCHAR(100),
	@p_video_id BIGINT,
	@p_fetch_list sf.fetch_list READONLY
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
		INSERT INTO sf.fetch_source SELECT @p_video_id, [source] FROM @p_fetch_list;
		SELECT 1 AS [success], 'SOURCES_ADDED' AS [message] RETURN;
	END
END
GO