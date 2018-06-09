/* **  www.evolutility.org - (c) 2012 Olivier Giulieri  ** */
/*    SQL script for generic pagin with Evolutility     */

CREATE PROCEDURE CH_SP_PagedItem
	(
	@Select  varchar(1000),
	@Table varchar(200),
	@TableS varchar(800),
	@WhereClause  varchar(2000),
	@OrderBy  varchar(200),
	@pk varchar(50), 
	@Page int,
	@RecsPerPage int	
	)
AS

SET NOCOUNT ON
CREATE TABLE #TempItems ( IDt int IDENTITY, IDo int)
IF (@WhereClause='')
  	INSERT INTO #TempItems (IDo) EXEC('SELECT T.'+@pk+' FROM '+@TableS+'  ORDER BY ' +@OrderBy)
ELSE
	EXEC( 'INSERT INTO #TempItems (IDo) SELECT T.'+@pk+' FROM '+@TableS+'  WHERE ' + @WhereClause+ ' ORDER BY '+@OrderBy)
DECLARE @FirstRec int, @LastRec int
SELECT @FirstRec = (@Page - 1) * @RecsPerPage
SELECT @LastRec = (@Page * @RecsPerPage + 1)
IF (@WhereClause='')
	EXEC( 'SELECT '+@Select + ', MoreRecords = ( SELECT COUNT(*)  FROM #TempItems Temp  WHERE Temp.IDt >= ' 
+ @LastRec + ')  FROM #TempItems Temp,  ' + @TableS  
	+ ' WHERE T.'+@pk+' = Temp.IDo AND Temp.IDt > '+ @FirstRec + ' AND Temp.IDt < '+ @LastRec + '  ORDER BY '+  @OrderBy)
ELSE
	EXEC( 'SELECT '+@Select + ', MoreRecords = ( SELECT COUNT(*)  FROM #TempItems Temp  WHERE Temp.IDt >= ' 
+ @LastRec + ')  FROM #TempItems Temp,  ' + @TableS  
	+ ' WHERE T.'+@pk+' = Temp.IDo AND Temp.IDt > '+ @FirstRec + ' AND Temp.IDt < '+ @LastRec + ' AND ' + @WhereClause+ ' ORDER BY '+  @OrderBy)
SET NOCOUNT OFF

GO


CREATE PROCEDURE  [CH_Employees_SP_Reportees](
    @UserID int
)
AS

SELECT   ID, LastName + ', ' + FirstName AS value, @UserID as UserID 
	FROM  CH_Employee
	WHERE 	ReportsTo=@UserID  and ID<>@UserID
	ORDER BY LastName, FirstName


GO