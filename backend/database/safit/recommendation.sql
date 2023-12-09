USE [safit]
GO

CREATE PROCEDURE sf.pc_recommendation_alter_tag
(
	@p_user_id BIGINT,
	@p_tag_id BIGINT,
	@p_weight REAL
) AS BEGIN
	UPDATE sf.[recommendation] SET [weight] = [weight] + @p_weight WHERE [user_id] = @p_user_id AND [tag_id] = @p_tag_id;
	IF (@@ROWCOUNT = 0) BEGIN 
		INSERT INTO sf.[recommendation] VALUES (@p_user_id, @p_tag_id, @p_weight * 1.2)
	END;
END 
GO

CREATE PROCEDURE sf.pc_recommendation_calculate
(
	@p_user_id BIGINT,
	@p_video_id BIGINT,
	@p_weight REAL
) AS BEGIN
	DECLARE @id BIGINT;
	DECLARE cur CURSOR FOR SELECT DISTINCT [id] FROM sf.[tag] T
		INNER JOIN sf.[video_tag] VT ON T.[id] = VT.[tag_id]
		WHERE [video_id] = @p_video_id;
	OPEN cur;
	FETCH NEXT FROM cur INTO @id;
	WHILE @@FETCH_STATUS = 0 BEGIN
		EXEC sf.pc_recommendation_alter_tag @p_user_id, @id, @p_weight
	END;
	CLOSE cur;
	DEALLOCATE cur;
END 
GO

CREATE PROCEDURE sf.pc_recommendation_calculate_view
(
	@p_user_id BIGINT,
	@p_video_id BIGINT
) AS EXEC sf.pc_recommendation_calculate @p_user_id, @p_video_id, 0.001 
GO

CREATE PROCEDURE sf.pc_recommendation_calculate_like
(
	@p_user_id BIGINT,
	@p_video_id BIGINT
) AS EXEC sf.pc_recommendation_calculate @p_user_id, @p_video_id, 0.005 
GO

CREATE PROCEDURE sf.pc_recommendation_calculate_comment
(
	@p_user_id BIGINT,
	@p_video_id BIGINT
) AS EXEC sf.pc_recommendation_calculate @p_user_id, @p_video_id, 0.01 
GO

CREATE PROCEDURE sf.pc_recommendation_get
(
	@p_user_id BIGINT
) AS BEGIN
	DECLARE @weight_share REAL;
	SELECT @weight_share = SUM([weight]) FROM sf.recommendation WHERE [user_id] = @p_user_id;
	SELECT [tag_id], @weight_share / [weight] AS [weight_possibility] FROM sf.recommendation WHERE [user_id] = @p_user_id;
END 
GO

CREATE PROCEDURE sf.pc_recommendation_get_random_tag
(
	@p_user_id BIGINT
) AS BEGIN
	CREATE TABLE #recommendations_matrix (
		[tag_id] BIGINT NOT NULL PRIMARY KEY,
		[weight] REAL NOT NULL);
	INSERT INTO #recommendations_matrix EXEC sf.pc_recommendation_get @p_user_id;
	DECLARE @tag_id BIGINT;
	SELECT TOP(1) @tag_id = RM.tag_id FROM #recommendations_matrix RM
		ORDER BY RM.[weight], NEWID();
	IF (@tag_id IS NULL) SELECT TOP(1) * FROM sf.tag ORDER BY NEWID();
	ELSE SELECT TOP(1) * FROM sf.tag WHERE [id] = @tag_id;
END
GO

EXEC sf.pc_recommendation_get 0;
EXEC sf.pc_recommendation_get_random_tag 0;