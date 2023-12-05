USE [safit]
GO

CREATE PROCEDURE sf.pc_trainer_is_promoted
(
	@p_user_id BIGINT,
	@p_is_promoted BIT OUTPUT
) AS BEGIN
	SET @p_is_promoted = 0;
	IF EXISTS(SELECT [id] FROM sf.trainer WHERE [id] = @p_user_id) BEGIN SET @p_is_promoted = 1 END;
END GO
CREATE PROCEDURE sf.pc_trainer_promote
(
	@p_user_id BIGINT,
	@p_token VARCHAR(100)
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_user_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 'ERR.USER_DOESNT_EXIST' AS [message] RETURN END;
	BEGIN TRY INSERT INTO sf.trainer VALUES (@p_user_id) END TRY
	BEGIN CATCH SELECT 'ERR.UNABLE_TO_PROMOTE' AS [message] RETURN END CATCH
END GO