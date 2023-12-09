USE [safit]
GO

CREATE PROCEDURE sf.pc_course_create
(
	@p_trainer_id BIGINT,
	@p_token VARCHAR(100),
	@p_sport_id BIGINT,
	@p_price MONEY,
	@p_name NVARCHAR(100),
	@p_description NVARCHAR(400)
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_trainer_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	DECLARE @is_trainer BIT;
	EXEC sf.pc_trainer_is_promoted @p_trainer_id, @is_trainer OUTPUT;
	IF (@is_trainer = 0) BEGIN SELECT 0 AS [success], 'NOT_TRAINER' AS [message] RETURN END;
	BEGIN TRY
		BEGIN TRANSACTION;
		INSERT INTO sf.course VALUES (@p_trainer_id, @p_sport_id, @p_price, @p_name, @p_description);
		SELECT * FROM sf.course WHERE [id] = SCOPE_IDENTITY();
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		SELECT 0 AS [success], 'UNABLE_TO_CREATE_COURSE' AS [message] RETURN;
	END CATCH
END
GO

CREATE PROCEDURE sf.pc_course_edit
(
	@p_trainer_id BIGINT,
	@p_token VARCHAR(100),
	@p_course_id BIGINT,
	@p_sport_id BIGINT,
	@p_price MONEY,
	@p_name NVARCHAR(100),
	@p_description NVARCHAR(400)
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_trainer_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	DECLARE @is_trainer BIT;
	EXEC sf.pc_trainer_is_promoted @p_trainer_id, @is_trainer OUTPUT;
	IF (@is_trainer = 0) BEGIN SELECT 0 AS [success], 'NOT_TRAINER' AS [message] RETURN END;
	BEGIN TRY
		BEGIN TRANSACTION;
		UPDATE sf.course SET [sport_id] = @p_sport_id, [price] = @p_price, [name] = @p_name, [description] = @p_description
			WHERE [id] = @p_course_id AND [trainer_id] = @p_trainer_id
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		SELECT 0 AS [success], 'UNABLE_TO_CREATE_COURSE' AS [message] RETURN;
	END CATCH
END
GO