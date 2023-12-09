USE [safit]
GO

CREATE PROCEDURE sf.pc_post_create
(
	@p_trainer_id BIGINT,
	@p_token VARCHAR(100),
	@p_sport_id BIGINT,
	@p_content NVARCHAR(2500),
	@p_course_id BIGINT NULL
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_trainer_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	DECLARE @is_trainer BIT;
	EXEC sf.pc_trainer_is_promoted @p_trainer_id, @is_trainer OUTPUT;
	IF (@is_trainer = 0) BEGIN SELECT 0 AS [success], 'NOT_TRAINER' AS [message] RETURN END;
	BEGIN TRY
		BEGIN TRANSACTION;
		INSERT INTO sf.post VALUES (@p_trainer_id, @p_sport_id, @p_content, @p_course_id, DEFAULT);
		SELECT * FROM sf.post WHERE [id] = SCOPE_IDENTITY() AND [trainer_id] = @p_trainer_id AND [sport_id] = @p_sport_id;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH 
		ROLLBACK TRANSACTION;
		SELECT 0 AS [success], 'UNABLE_TO_CREATE_POST' AS [message] RETURN;
	END CATCH
END
GO

CREATE PROCEDURE sf.pc_post_edit
(
	@p_trainer_id BIGINT,
	@p_token VARCHAR(100),
	@p_post_id BIGINT,
	@p_sport_id BIGINT,
	@p_content NVARCHAR(2500),
	@p_course_id BIGINT NULL
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_trainer_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	DECLARE @is_trainer BIT;
	EXEC sf.pc_trainer_is_promoted @p_trainer_id, @is_trainer OUTPUT;
	IF (@is_trainer = 0) BEGIN SELECT 0 AS [success], 'NOT_TRAINER' AS [message] RETURN END;
	BEGIN TRY
		BEGIN TRANSACTION;
		UPDATE sf.post SET [sport_id] = @p_sport_id, [content] = @p_content, [course_id] = @p_course_id 
			WHERE [id] = @p_post_id AND [trainer_id] = @p_trainer_id
		SELECT 1 AS [success], 'POST_MODIFIED' AS [message] RETURN;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH 
		ROLLBACK TRANSACTION;
		SELECT 0 AS [success], 'UNABLE_TO_CREATE_POST' AS [message] RETURN;
	END CATCH
END
GO

CREATE PROCEDURE sf.pc_post_get
(
	@p_user_id BIGINT,
	@p_token VARCHAR(100),
	@p_post_id BIGINT
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_user_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	DECLARE @course_id BIGINT;
	SELECT @course_id = [course_id] FROM sf.post WHERE [id] = @p_post_id;
	IF (@course_id IS NULL) BEGIN
		UPDATE sf.post SET [views] = [views] + 1 WHERE [id] = @p_post_id;
		SELECT * FROM sf.post WHERE [id] = @p_post_id;
	END ELSE BEGIN
		IF EXISTS(SELECT * FROM sf.course_access WHERE [user_id] = @p_user_id AND [course_id] = @course_id) BEGIN
			SELECT * FROM sf.post WHERE [id] = @p_post_id;
		END ELSE BEGIN
			SELECT 0 AS [success], 'NO_ACCESS_TO_POST' AS [message] RETURN 
		END
	END
END
GO

CREATE PROCEDURE sf.pc_post_get_recommended
(
	@p_user_id BIGINT,
	@p_token VARCHAR(100),
	@p_trainer_id BIGINT NULL
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_user_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	CREATE TABLE #recommended_tag ([id] BIGINT, [name] NVARCHAR(100));
	INSERT INTO #recommended_tag EXEC sf.pc_recommendation_get_random_tag @p_user_id;
	IF (@p_trainer_id IS NULL) BEGIN
		SELECT TOP(1) P.* FROM sf.post P
			INNER JOIN sf.post_tag PT ON P.id = PT.post_id
		WHERE P.[course_id] = NULL AND PT.tag_id = (SELECT [id] FROM #recommended_tag)
		ORDER BY NEWID()
	END ELSE BEGIN
		SELECT TOP(1) P.* FROM sf.post P
			INNER JOIN sf.post_tag PT ON P.id = PT.post_id
		WHERE P.[course_id] = NULL AND PT.tag_id = (SELECT [id] FROM #recommended_tag)AND P.[trainer_id] = @p_trainer_id
		ORDER BY NEWID()
	END
END
GO

