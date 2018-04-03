use DBTest1
SELECT  CASE WHEN col.colorder = 1 THEN obj.name
                                        ELSE ''
                                    END AS ����,
                            col.colorder AS ��� ,
                            col.name AS ���� ,
                            ISNULL(ep.[value], '') AS ��˵�� ,
                            t.name AS �������� ,
                            col.length AS ���� ,
                            ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) AS С��λ�� ,
                            CASE WHEN COLUMNPROPERTY(col.id, col.name, 'IsIdentity') = 1 THEN 1
                                    ELSE 0
                            END AS ��ʶ ,
                            CASE WHEN EXISTS ( SELECT   1
                                                FROM     dbo.sysindexes si
                                                        INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id
                                                                                    AND si.indid = sik.indid
                                                        INNER JOIN dbo.syscolumns sc ON sc.id = sik.id
                                                                                    AND sc.colid = sik.colid
                                                        INNER JOIN dbo.sysobjects so ON so.name = si.name
                                                                                    AND so.xtype = 'PK'
                                                WHERE    sc.id = col.id
                                                        AND sc.colid = col.colid ) THEN 1
                                    ELSE 0
                            END AS ���� ,
                            CASE WHEN col.isnullable = 1 THEN 1
                                    ELSE 0
                            END AS ����� ,
                            ISNULL(comm.text, '') AS Ĭ��ֵ
                    FROM    dbo.syscolumns col
                            LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype
                            inner JOIN dbo.sysobjects obj ON col.id = obj.id
                                                                AND obj.xtype = 'U'
                                                                AND obj.status >= 0
                            LEFT  JOIN dbo.syscomments comm ON col.cdefault = comm.id
                            LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id
                                                                            AND col.colid = ep.minor_id
                                                                            AND ep.name = 'MS_Description'
                            LEFT  JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id
                                                                                AND epTwo.minor_id = 0
                                                                                AND epTwo.name = 'MS_Description'
                    ORDER BY obj.name,col.colorder;