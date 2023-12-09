USE [safit]
GO

CREATE PROCEDURE sf.pc_like_create
(
	@p_user_id BIGINT,
	@p_token VARCHAR(100),
	@p_video_id BIGINT
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_user_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	BEGIN TRY
		BEGIN TRANSACTION;
		INSERT INTO sf.[like] VALUES (@p_user_id, @p_video_id);
		EXEC sf.pc_recommendation_calculate_like @p_user_id, @p_video_id;
		COMMIT TRANSACTION;
		SELECT 1 AS [success], 'LIKE_CREATED' AS [message] RETURN
	END TRY
	BEGIN CATCH
		SELECT 0 AS [success], 'LIKE_NOT_CREATED' AS [message] RETURN
		ROLLBACK TRANSACTION;
	END CATCH
END
GO

