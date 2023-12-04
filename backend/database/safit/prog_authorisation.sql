USE [safit]
GO

ALTER PROCEDURE sf.[login]
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

ALTER PROCEDURE sf.[register]
(
	@p_username NVARCHAR(32),
	@p_password NVARCHAR(32),
	@p_email NVARCHAR(64),
	@p_first_name NVARCHAR(32),
	@p_last_name NVARCHAR(32)
) AS BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
		DECLARE @e_p_password NVARCHAR(32) = HASHBYTES('SHA2_256', @p_password);
		INSERT INTO sf.[user] VALUES (@p_email, @p_username, @e_p_password, @p_first_name, @p_last_name, NULL, DEFAULT);
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

EXEC sf.[login] 'admin', '12345';
EXEC sf.[register] 'admin', '12345', 'admin@gmail.com', 'John', 'Doe';