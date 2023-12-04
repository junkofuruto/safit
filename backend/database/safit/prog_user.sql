USE [safit]
GO

CREATE PROCEDURE sf.[update_user_data]
(
	@p_id BIGINT,
	@p_token VARCHAR(100),
	@p_email NVARCHAR(64),
	@p_password NVARCHAR(32),
	@p_first_name NVARCHAR(32),
	@p_last_name NVARCHAR(32),
	@p_profile_src VARCHAR(65) NULL
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.confirm_identity @p_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 'ERR.USER_DOESNT_EXIST' AS [message] RETURN END;
	BEGIN TRY
		BEGIN TRANSACTION;
		UPDATE sf.[user] SET
			[email] = @p_email,
			[password] = HASHBYTES('SHA2_256', @p_password),
			[first_name] = @p_first_name,
			[last_name] = @p_last_name,
			[profile_src] = @p_profile_src
		WHERE [id] = @p_id;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
			SELECT 'ERR.INPUT_DATA_CONFLICT' AS [message];
	END CATCH;
	IF @@TRANCOUNT > 0 BEGIN
		COMMIT TRANSACTION;
		SELECT 'SUC.CHANGES_COMMITED' AS [message];
	END;
END;
GO

CREATE PROCEDURE sf.[update_user_balance]
(
	@p_id BIGINT,
	@p_token VARCHAR(100),
	@p_balance_change MONEY
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.confirm_identity @p_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 'ERR.USER_DOESNT_EXIST' AS [message] RETURN END;
	DECLARE @previous_balance MONEY;
	SELECT @previous_balance = [balance] FROM sf.[user] WHERE [id] = @p_id;
	IF @previous_balance + @p_balance_change < 0 BEGIN SELECT 'ERR.NEGATIVE_BALANCE' AS [message] END;
	ELSE BEGIN
		UPDATE sf.[user] SET [balance] = @previous_balance + @p_balance_change WHERE [id] = @p_id;
		SELECT 'SUC.BALANCE_CHANGED' AS [message];
	END;
END;
GO

