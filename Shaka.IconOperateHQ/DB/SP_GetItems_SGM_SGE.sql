USE [HeadQ0328]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetItems_SGM_SGE]    Script Date: 2016/5/27 15:03:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--[dbo].[SP_GetItems_SGM_SGE] 'number,name,[status]','VW_Recipe','','Number ASC',1,80,'S','001,CC01', '2014-11-17', 10000
ALTER PROCEDURE [dbo].[SP_GetItems_SGM_SGE]   
@selectedFields NVARCHAR(MAX) = '*',		-- ���ؼ�¼���ֶ�����","������Ĭ����"*" 
@TVName NVARCHAR(500),						-- ��������ͼ��
@whereStatement NVARCHAR(4000),				-- ��������
@sortExpression VARCHAR(1000) = '',			-- ������ֶ���
@pageIndex INT = 1,							-- ��ǰҳ��
@pageSize INT = 5,							-- ÿҳ��¼����(ҳ���С) 
@SGType NVARCHAR(2),						-- �ŵ���ŵ���
@SGNumbers NVARCHAR(MAX),					-- �ŵ���ŵ������б�
@effectiveDate VARCHAR(22),                 -- ��Ч����
@totalRow INT OUTPUT						-- ��¼����

AS 
BEGIN 

-- ============================================= 
-- Author: Harry Hou
-- Create date: 2014-5-10 
-- ============================================= 

-------------------�������---------------- 
IF @pageSize IS NULL OR @pageSize <= 0
    BEGIN
       SET @pageSize = 999999999
    END


IF LEN(RTRIM(LTRIM(@TVName))) !> 0 
    BEGIN 
       SET @totalRow = -1; 
       RETURN; 
    END 

IF LEN(RTRIM(LTRIM(@sortExpression))) !> 0 
    BEGIN
       SET @sortExpression = 'Number ASC'
    END

IF ISNULL(@pageSize, 0) <= 0 
    BEGIN
       SET @pageSize = 5
    END 

IF ISNULL(@pageIndex, 0) <= 0 
    BEGIN
       SET @pageIndex = 1
    END 
-------------------������---------------- 
 
    -- ����SQL 
    DECLARE @SQLNE NVARCHAR(MAX), 
			@SQLSGE	NVARCHAR(MAX),
			@SQLSGETemp NVARCHAR(MAX),
			@SQL NVARCHAR(MAX);
	
	DECLARE @ROWNUM VARCHAR(9);
	SET @ROWNUM = CONVERT(VARCHAR(9), (@pageIndex -1) * @pageSize)
    
    CREATE TABLE #tmpNE
    (
         Number NVARCHAR(100) , 
         EffectiveDate DATETIME 
    )
    
    SET ROWCOUNT 99999999;
    IF @SGType = 'S'
    BEGIN
		SET @SQLNE='
							INSERT INTO #tmpNE(Number, EffectiveDate) 

							SELECT DISTINCT VW.Number, 
											MAX(VW.effective_date) AS EffectiveDate 
							FROM  '+@TVName+'  VW
							WHERE 1=1
									AND CHARINDEX('','' + CONVERT(VARCHAR(22), VW.StoreNum) + '','', '','' + ''' + @SGNumbers + ''' + '','') > 0					 
									AND VW.effective_date <= CONVERT(DATE,'''+ @effectiveDate + ''')   
							GROUP BY VW.Number   
							UNION   
							SELECT DISTINCT VW.Number, MAX(VW.effective_date) AS EffectiveDate
							 FROM   '+@TVName+' VW
							LEFT JOIN storeno ON storeno.strgroup = VW.strgroup
							 WHERE 1=1
								   AND  CHARINDEX('','' + CONVERT(VARCHAR(22), storeno.number) + '','', '','' + ''' + @SGNumbers + ''' + '','') > 0
								   AND VW.effective_date <= CONVERT(DATE,'''+ @effectiveDate + ''')  
							 GROUP BY VW.number   
							 ORDER BY VW.number '
    END
    ELSE
    BEGIN
        SET @SQLNE ='
						INSERT INTO #tmpNE(Number, EffectiveDate)    
						SELECT DISTINCT VW.Number, MAX(VW.effective_date) AS EffectiveDate    
						FROM ' + @TVName + ' AS VW  
						WHERE     
								
								 CHARINDEX('','' + CONVERT(VARCHAR(22), strgroup) + '','', '','' + ''' + @SGNumbers + ''' + '','') > 0
								AND VW.effective_date <= CONVERT(DATE,''' + @effectiveDate + ''')       
						 GROUP BY VW.number    
						 ORDER BY Number '
    END

    EXEC(@SQLNE)	

	--PRINT (@SQLNE)

		--SELECT * FROM #tmpNE
	SET ROWCOUNT @pageSize;
	IF (@SGType ='G')
	BEGIN
	
			SET @SQLSGETemp = 
			'
							SELECT DISTINCT VW.[number], VW.[name], MIN(VW.[status]) AS [status], ROW_NUMBER() OVER (ORDER BY VW.'+ @sortExpression +')  AS ROWNUM 
							FROM ' + @TVName + ' AS VW
							INNER JOIN #tmpNE ON #tmpNE.number COLLATE SQL_Latin1_General_CP1_CI_AS = VW.number and #tmpNE.EffectiveDate = VW.effective_date
							WHERE 1=1
								AND VW.ttype in (''G'', ''U'')  
								AND CHARINDEX( '','' + CONVERT(VARCHAR(22), VW.strgroup) + '','',  '','' + ''' + @SGNumbers + ''' + '','') > 0
								' + @whereStatement + '
							GROUP BY VW.[number], VW.[name]
			'
			SET @SQLSGE='
					 	SELECT  [number], [name], [status]
						FROM
						(
							' + @SQLSGETemp + '
						) AS tab
						WHERE tab.ROWNUM >' + @ROWNUM 
			
			EXEC(@SQLSGE)
			
			---- ����SQL 
			SET @SQL = 'SET @Rows = (SELECT MAX(ROWNUM) FROM 
											(' 
												+@SQLSGETemp + '
											) AS P 
									  )'; 
		    
			-- ִ��SQL, ȡ���ҳ��
			EXECUTE sp_executesql @SQL, N'@Rows INT OUTPUT', @totalRow OUTPUT;
			
			
			
		END
	ELSE
	BEGIN

				SET @SQLSGETemp =
			'
				SELECT [number], [name], MAX([status]) as [status], ROW_NUMBER() OVER (ORDER BY VW.'+ @sortExpression +')  AS ROWNUM 
				FROM
				( 
					SELECT DISTINCT VW.[number], VW.[name], MIN(VW.[status]) AS [status]
					FROM '+@TVName+' AS VW
					INNER JOIN #tmpNE ON #tmpNE.number COLLATE SQL_Latin1_General_CP1_CI_AS = VW.number and #tmpNE.EffectiveDate = VW.effective_date
					WHERE 1=1
					AND CHARINDEX( '','' + CONVERT(VARCHAR(22), VW.storenum) + '','',  '','' + ''' + @SGNumbers + ''' + '','') > 0
					AND VW.effective_date <= CONVERT(DATE, '''+ @effectiveDate + ''')
					' + @whereStatement + '
					GROUP BY VW.[number], VW.[name]
					UNION 
					SELECT DISTINCT VW.[number], VW.[name], MIN(VW.[status]) AS [status]
					FROM '+@TVName+' AS VW
					LEFT JOIN storeno ON storeno.strgroup = VW.strgroup
					INNER JOIN #tmpNE ON #tmpNE.number COLLATE SQL_Latin1_General_CP1_CI_AS = VW.number and #tmpNE.EffectiveDate = VW.effective_date
					WHERE 1=1  
					AND CHARINDEX( '','' + CONVERT(VARCHAR(22), storeno.number) + '','',  '','' + ''' + @SGNumbers + ''' + '','') > 0
					AND VW.effective_date <= CONVERT(DATE, '''+ @effectiveDate + ''')
					' + @whereStatement + '
					GROUP BY VW.[number], VW.[name]
				) AS VW
				WHERE 1=1 
				GROUP BY [number], [name]
			'
			
		SET @SQLSGE='
						SELECT [number], [name], [status]
						FROM 
						(' 
							+ @SQLSGETemp +  '
						)AS tab
						WHERE ROWNUM > '+ @ROWNUM 
		
		EXEC(@SQLSGE)	
	  
		---- ����SQL 
		SET @SQL = 'SET @Rows = (SELECT MAX(ROWNUM) FROM 
										(' +
											@SQLSGETemp +  '
										) AS P 
								  )'; 
	    
		-- ִ��SQL, ȡ���ҳ��
		EXECUTE sp_executesql @SQL, N'@Rows INT OUTPUT', @totalRow OUTPUT; 

		END
	  
    TRUNCATE TABLE #tmpNE
	DROP TABLE #tmpNE

-- ִ�гɹ�

END

