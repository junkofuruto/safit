USE [safit]
GO

CREATE PROCEDURE sf.pc_cart_add_product
(
	@p_user_id BIGINT,
	@p_token VARCHAR(100),
	@p_product_id BIGINT,
	@amount INT
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_user_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	DECLARE @last_cart_instance BIGINT;
	SELECT @last_cart_instance = MAX([id]) FROM sf.cart WHERE [user_id] = @p_user_id
	BEGIN TRY
		IF EXISTS(SELECT * FROM sf.cart_content WHERE [cart_id] = @last_cart_instance AND [product_id] = @p_product_id) BEGIN
			UPDATE sf.cart_content SET [amount] = @amount WHERE [cart_id] = @last_cart_instance AND [product_id] = @p_product_id
		END ELSE BEGIN
			INSERT INTO sf.cart_content VALUES (@last_cart_instance, @p_product_id, @amount)
		END
	END TRY
	BEGIN CATCH
		SELECT 0 AS [success], 'UNALE_TO_ADD_PRODUCT' AS [message] RETURN 
	END CATCH
	SELECT 1 AS [success], 'PRODUCT_ADDED' AS [message]
END 
GO

CREATE PROCEDURE sf.pc_cart_new_instance
(
	@p_user_id BIGINT,
	@p_token VARCHAR(100)
) AS BEGIN
	DECLARE @user_exists BIT;
	EXEC sf.pc_user_confirm_identity @p_user_id, @p_token, @user_exists OUTPUT;
	IF (@user_exists = 0) BEGIN SELECT 0 AS [success], 'USER_DOESNT_EXIST' AS [message] RETURN END;
	INSERT INTO sf.cart VALUES (@p_user_id);
	SELECT 0 AS [success], 'INSANCE_CREATED' AS [message];
END 
GO