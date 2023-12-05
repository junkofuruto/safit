USE [safit]
GO

CREATE PROCEDURE sf.pc_subscription_create
(
	@p_user_id BIGINT,
	@p_token BIGINT,
	@p_trainer_id BIGINT
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_user_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 'ERR.USER_DOESNT_EXIST' AS [message] RETURN END;
	BEGIN TRY INSERT INTO sf.subscription VALUES (@p_user_id, @p_trainer_id) END TRY
	BEGIN CATCH SELECT 'ERR.UNABLE_TO_SUBSCRIBE' AS [message] RETURN END CATCH
END GO
CREATE PROCEDURE sf.pc_subscription_remove
(
	@p_user_id BIGINT,
	@p_token BIGINT,
	@p_trainer_id BIGINT
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_user_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 'ERR.USER_DOESNT_EXIST' AS [message] RETURN END;
	BEGIN TRY DELETE FROM sf.subscription WHERE [user_id] = @p_user_id AND [trainer_id] = @p_trainer_id END TRY
	BEGIN CATCH SELECT 'ERR.UNABLE_TO_SUBSCRIBE' AS [message] RETURN END CATCH
END GO

