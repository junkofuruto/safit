USE [safit]
GO

CREATE PROCEDURE sf.pc_sport_create
(
	@p_trainer_id BIGINT,
	@p_token VARCHAR(100),
	@p_name NVARCHAR(30),
	@p_description NVARCHAR(200),
	@p_preview_src VARCHAR(65)
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_trainer_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 'ERR.USER_DOESNT_EXIST' AS [message] RETURN END;
	DECLARE @is_trainer BIT;
	EXEC sf.pc_trainer_is_promoted @p_trainer_id, @is_trainer OUTPUT;
	IF (@is_trainer = 0) BEGIN SELECT 'ERR.NOT_TRAINER' AS [message] RETURN END;
	BEGIN TRY
		INSERT INTO sf.sport ([name], [description], [preview_src]) VALUES (@p_name, @p_description, @p_preview_src)
	END TRY
	BEGIN CATCH
		SELECT 'ERR.UNABLE_TO_CREATE_SPORT' AS [message] 
		RETURN 
	END CATCH
	SELECT 'SUC.SPORT_CREATED'
END GO