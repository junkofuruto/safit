USE [safit]
GO

EXEC sf.pc_user_register 'admin', '12345', 'admin@gmail.com', 'John', 'Doe'
GO

CREATE TABLE #user ([id] BIGINT, [email] NVARCHAR(64), [username] NVARCHAR(32), 
	[first_name] NVARCHAR(32), [last_name] NVARCHAR(32), [balance] MONEY, [token] VARCHAR(100), [is_trainer] BIT);
INSERT INTO #user EXEC sf.pc_user_login 'admin', '12345';
GO

DECLARE @user_id BIGINT;
DECLARE @token VARCHAR(100);
SELECT @user_id = [id], @token = [token] FROM #user;

EXEC sf.pc_trainer_promote @user_id, @token;
GO