USE [safit]
GO

CREATE PROCEDURE sf.pc_comment_create
(
	@p_user_id BIGINT,
	@p_token VARCHAR(100),
	@p_video_id BIGINT,
	@p_branch_id BIGINT NULL
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_user_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	BEGIN TRY
		BEGIN TRANSACTION;
		INSERT INTO sf.comment VALUES (@p_video_id, @p_user_id, @p_branch_id);
		EXEC sf.pc_recommendation_calculate_comment @p_user_id, @p_video_id;
		SELECT * FROM sf.comment WHERE [id] = SCOPE_IDENTITY();
	END TRY
	BEGIN CATCH
		SELECT 0 AS [success], 'COMMENT_NOT_CREATED' AS [message] RETURN
		ROLLBACK TRANSACTION;
	END CATCH
	COMMIT TRANSACTION;
END
GO

