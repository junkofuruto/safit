USE [safit] 
GO

CREATE PROCEDURE sf.pc_video_create
(
	@p_trainer_id BIGINT,
	@p_token VARCHAR(100),
	@p_sport_id BIGINT,
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
		INSERT INTO sf.video VALUES (@p_trainer_id, @p_sport_id, @p_course_id, DEFAULT, DEFAULT);
		SELECT * FROM sf.video WHERE [id] = SCOPE_IDENTITY();
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		SELECT 0 AS [success], 'UNABLE_TO_CREATE_VIDEO' AS [message] RETURN;
	END CATCH
END
GO

CREATE PROCEDURE sf.pc_video_edit
(
	@p_trainer_id BIGINT,
	@p_token VARCHAR(100),
	@p_video_id BIGINT,
	@p_sport_id BIGINT,
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
		UPDATE sf.video SET [sport_id] = @p_sport_id, [course_id] = @p_course_id 
			WHERE [id] = @p_video_id AND [trainer_id] = @p_trainer_id;
		SELECT 1 AS [success], 'VIDEO_REDACTED' AS [message] RETURN;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		SELECT 0 AS [success], 'UNABLE_TO_EDIT_VIDEO' AS [message] RETURN;
	END CATCH
END
GO

CREATE PROCEDURE sf.pc_video_get
(
	@p_user_id BIGINT,
	@p_token VARCHAR(100),
	@p_video_id BIGINT
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_user_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	DECLARE @course_id BIGINT;
	SELECT @course_id = [course_id] FROM sf.video WHERE [id] = @p_video_id;
	IF (@course_id IS NULL) BEGIN
		UPDATE sf.post SET [views] = [views] + 1 WHERE [id] = @p_video_id;
		EXEC sf.pc_recommendation_calculate_view @p_user_id, @p_video_id;
		SELECT * FROM sf.video WHERE [id] = @p_video_id;
	END ELSE BEGIN
		IF EXISTS(SELECT * FROM sf.course_access WHERE [user_id] = @p_user_id AND [course_id] = @course_id) BEGIN
			EXEC sf.pc_recommendation_calculate_view @p_user_id, @p_video_id;
			SELECT * FROM sf.video WHERE [id] = @p_video_id;
		END ELSE BEGIN
			SELECT 0 AS [success], 'NO_ACCESS_TO_VIDEO' AS [message] RETURN 
		END
	END
END
GO

CREATE PROCEDURE sf.pc_video_get_recommended
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
		SELECT TOP(1) V.* FROM sf.video V
			INNER JOIN sf.video_tag VT ON V.id = VT.video_id
		WHERE V.[course_id] = NULL AND VT.tag_id = (SELECT [id] FROM #recommended_tag)
		ORDER BY NEWID()
	END ELSE BEGIN
		SELECT TOP(1) V.* FROM sf.video V
			INNER JOIN sf.video_tag VT ON V.id = VT.video_id
		WHERE V.[course_id] = NULL AND VT.tag_id = (SELECT [id] FROM #recommended_tag)AND V.[trainer_id] = @p_trainer_id
		ORDER BY NEWID()
	END
END
GO