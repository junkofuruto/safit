USE [safit]
GO

CREATE PROCEDURE sf.generate_usertoken @result VARCHAR(100) OUTPUT AS BEGIN
	DECLARE @token VARBINARY(500);
	DECLARE @base64token VARCHAR(100);
	SET @token = CRYPT_GEN_RANDOM(100);
	SET @base64token = CAST(N'' AS XML).value('xs:base64Binary(sql:variable("@Token"))', 'VARCHAR(MAX)');
	SET @base64token = REPLACE(REPLACE(@base64token, '+', 'A'), '/', 'B');
	SET @result = LEFT(@base64token, 100)
END;
GO

CREATE PROC sf.confirm_identity
(
	@p_user_id BIGINT,
	@p_token VARCHAR(100),
	@confirmed BIT OUTPUT
) AS BEGIN
	IF EXISTS (SELECT * FROM sf.[user] WHERE [id] = @p_user_id AND [token] = @p_token) SET @confirmed = 1;
    ELSE SET @confirmed = 0;
END;
GO

CREATE PROCEDURE sf.drop_token 
(
	@p_user_id BIGINT,
	@p_token VARCHAR(100)
) AS BEGIN
	DECLARE @new_token VARCHAR(100);
	DECLARE @user_exists BIT;
	EXEC sf.generate_usertoken @new_token OUTPUT;
	EXEC sf.confirm_identity @p_user_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 'ERR.USER_DOESNT_EXIST' AS [message] RETURN END;
	UPDATE sf.[user] SET [token] = @new_token WHERE [id] = @p_user_id
	SELECT 'SUC.TOKEN_DROPPED' AS [message]
END;
GO

CREATE PROCEDURE sf.[login]
(
	@p_username NVARCHAR(32),
	@p_password NVARCHAR(32)
) AS BEGIN
	DECLARE @user_id BIGINT;
	DECLARE @e_p_password NVARCHAR(32) = HASHBYTES('SHA2_256', @p_password);
	SELECT @user_id = id FROM sf.[user] WHERE [username] = @p_username AND [password] = @e_p_password;
	IF @user_id IS NULL BEGIN SELECT 'ERR.INVALID_PASSWORD_OR_LOGIN' AS [message] END;
	ELSE BEGIN SELECT * FROM sf.[user] WHERE [id] = @user_id END;
END;
GO

CREATE PROCEDURE sf.[register]
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
		EXEC sf.generate_usertoken @token OUTPUT;
		INSERT INTO sf.[user] VALUES (@p_email, @p_username, @e_p_password, @p_first_name, @p_last_name, NULL, DEFAULT, @token);
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
           ROLLBACK TRANSACTION;
		   SELECT 'ERR.INPUT_DATA_CONFLICT' AS [message];
	END CATCH;
	IF @@TRANCOUNT > 0 BEGIN
       COMMIT TRANSACTION;
	SELECT 'SUC.USER_REGISTERED' AS [message];
	END;
END;
GO

EXEC sf.[register] 'admin', '12345', 'admin@gmail.com', 'John', 'Doe';
EXEC sf.[login] 'admin', '12345';
EXEC sf.[drop_token] 0, 'XO1QysW1Mw9dIGPZKiIPj5E7vqWeVFkWOQzsgJyWElfl2UqWxwxaZBLl4jgfFYK5KdnCajAlVAV1Sg2ZYbhWtBZJBRkD8mDD30yI';