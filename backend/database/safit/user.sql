USE [safit]
GO

CREATE PROCEDURE sf.pc_user_generate_usertoken @result VARCHAR(100) OUTPUT AS BEGIN
	DECLARE @token VARBINARY(500);
	DECLARE @base64token VARCHAR(100);
	SET @token = CRYPT_GEN_RANDOM(100);
	SET @base64token = CAST(N'' AS XML).value('xs:base64Binary(sql:variable("@Token"))', 'VARCHAR(MAX)');
	SET @base64token = REPLACE(REPLACE(@base64token, '+', 'A'), '/', 'B');
	SET @result = LEFT(@base64token, 100)
END 
GO

CREATE PROCEDURE sf.pc_user_confirm_identity
(
	@p_user_id BIGINT,
	@p_token VARCHAR(100),
	@confirmed BIT OUTPUT
) AS BEGIN
	IF EXISTS (SELECT * FROM sf.[user] WHERE [id] = @p_user_id AND [token] = @p_token) SET @confirmed = 1;
    ELSE SET @confirmed = 0;
END 
GO

CREATE PROCEDURE sf.pc_user_drop_token 
(
	@p_user_id BIGINT,
	@p_token VARCHAR(100)
) AS BEGIN
	DECLARE @new_token VARCHAR(100);
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_generate_usertoken @new_token OUTPUT;
	EXEC sf.pc_user_confirm_identity @p_user_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	UPDATE sf.[user] SET [token] = @new_token WHERE [id] = @p_user_id
	SELECT  1 AS [success], 'TOKEN_DROPPED' AS [message]
END 
GO

CREATE PROCEDURE sf.pc_user_login
(
	@p_username NVARCHAR(32),
	@p_password NVARCHAR(32)
) AS BEGIN
	DECLARE @user_id BIGINT;
	DECLARE @e_p_password NVARCHAR(32) = HASHBYTES('SHA2_256', @p_password);
	SELECT @user_id = id FROM sf.[user] WHERE [username] = @p_username AND [password] = @e_p_password;
	IF @user_id IS NULL BEGIN SELECT 0 AS [success], 'INVALID_PASSWORD_OR_LOGIN' AS [message] RETURN END;
	ELSE BEGIN SELECT * FROM sf.[user] WHERE [id] = @user_id END;
END 
GO

CREATE PROCEDURE sf.pc_user_register
(
	@p_username NVARCHAR(32),
	@p_password NVARCHAR(32),
	@p_email NVARCHAR(64),
	@p_first_name NVARCHAR(32),
	@p_last_name NVARCHAR(32)
) AS BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
		DECLARE @token VARCHAR(100);
		DECLARE @e_p_password NVARCHAR(32) = HASHBYTES('SHA2_256', @p_password);
		EXEC sf.pc_user_generate_usertoken @token OUTPUT;
		INSERT INTO sf.[user] VALUES (@p_email, @p_username, @e_p_password, @p_first_name, @p_last_name, NULL, DEFAULT, @token);
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
           ROLLBACK TRANSACTION;
		   SELECT  0 AS [success], 'INPUT_DATA_CONFLICT' AS [message];
	END CATCH;
		IF @@TRANCOUNT > 0 BEGIN
		   COMMIT TRANSACTION;
		SELECT  1 AS [success], 'SUC.USER_REGISTERED' AS [message];
	END;
END 
GO

CREATE PROCEDURE sf.pc_user_update_data
(
	@p_id BIGINT,
	@p_token VARCHAR(100),
	@p_email NVARCHAR(64),
	@p_first_name NVARCHAR(32),
	@p_last_name NVARCHAR(32),
	@p_profile_src VARCHAR(65) NULL
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	BEGIN TRY
		BEGIN TRANSACTION;
		UPDATE sf.[user] SET
			[email] = @p_email,
			[first_name] = @p_first_name,
			[last_name] = @p_last_name,
			[profile_src] = @p_profile_src	
		WHERE [id] = @p_id;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 BEGIN
			ROLLBACK TRANSACTION;
			SELECT 0 AS [success], 'INPUT_DATA_CONFLICT' AS [message];
		END;
	END CATCH;
	IF @@TRANCOUNT > 0 BEGIN
		COMMIT TRANSACTION;
		SELECT 1 AS [success], 'CHANGES_COMMITED' AS [message];
	END;
END 
GO

CREATE PROCEDURE sf.pc_user_update_balance
(
	@p_id BIGINT,
	@p_token VARCHAR(100),
	@p_balance_change MONEY
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	DECLARE @previous_balance MONEY;
	SELECT @previous_balance = [balance] FROM sf.[user] WHERE [id] = @p_id;
	IF @previous_balance + @p_balance_change < 0 BEGIN SELECT 0 AS [success], 'NEGATIVE_BALANCE' AS [message] END;
	ELSE BEGIN
		UPDATE sf.[user] SET [balance] = @previous_balance + @p_balance_change WHERE [id] = @p_id;
		SELECT 1 AS [success], 'BALANCE_CHANGED' AS [message];
	END;
END 
GO

CREATE PROCEDURE sf.pc_user_buy_course
(
	@p_user_id BIGINT,
	@p_token VARCHAR(100),
	@p_course_id BIGINT
) AS BEGIN
	DECLARE @price MONEY;
	SELECT @price = [price] * -1 FROM sf.course WHERE [id] = @p_course_id;
	IF (@price = NULL) BEGIN
		SELECT 0 AS [success], 'ERR.COURSE_NOT_EXIST' AS [message]
	END ELSE BEGIN
		INSERT INTO sf.course_access VALUES (@p_user_id, @p_token, DEFAULT)
		EXEC sf.pc_user_update_balance @p_user_id, @p_token, @price
		SELECT * FROM sf.[user] WHERE [id] = @p_user_id
	END
END
GO

EXEC sf.pc_user_drop_token 0, 'P6MpUJm5qp0f89jnzTSc6h2AroxwphiNycAE3oLie5JGyLrHCEGRWl0B6M2ZjkiCcaVVov1wdacFBvwPrvIFIDLThrDdA6U5KLvO';
EXEC sf.pc_user_login 'admin', '12345';
EXEC sf.pc_user_register 'admin', '12345', 'admin@gmail.com', 'John', 'Doe';
EXEC sf.pc_user_update_balance 0, 'A04BmoN6hLKVNsHpQFdDeuVzeZuPFUQ6W7Vq9dzSM3K6e5WtiK3YDZ99MTg4VpGBupnf6vGCAk04LDUMF8kcaqtg1kOQYbINVSN2', -60
EXEC sf.pc_user_update_data 0, 'A04BmoN6hLKVNsHpQFdDeuVzeZuPFUQ6W7Vq9dzSM3K6e5WtiK3YDZ99MTg4VpGBupnf6vGCAk04LDUMF8kcaqtg1kOQYbINVSN2',
	'admin@gmail.com', '12345223', 'John', 'Marx'