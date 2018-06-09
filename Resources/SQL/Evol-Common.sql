/* **  (c) 2012 Olivier Giulieri - www.evolutility.org   ** */
/*    SQL script for generic paging with Evolutility     */ 
/*
	This file is part of Evolutility CRUD Framework.
	Source link <http://www.evolutility.org/download/download.aspx>

	Evolutility is open source software: you can redistribute it and/or modify
	it under the terms of the GNU Affero General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Evolutility is distributed WITHOUT ANY WARRANTY; 
	without even the implied warranty of	MERCHANTABILITY 
	or FITNESS FOR A PARTICULAR PURPOSE.  
	See the GNU General Public License for more details.

	You should have received a copy of the GNU Affero General Public License
	along with Evolutility. If not, see <http://www.fsf.org/licensing/licenses/agpl-3.0.html>.
	
	Commercial license may be purchased at www.evolutility.org <http://www.evolutility.org/product/Purchase.aspx>.
*/

CREATE PROCEDURE EvoSP_PagedItem
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
DECLARE @FirstRec int, @LastRec int

SELECT @FirstRec = (@Page - 1) * @RecsPerPage + 1

SELECT @LastRec = (@Page * @RecsPerPage)

IF(@RecsPerPage > 0)
BEGIN
	IF (@WhereClause='')
	BEGIN	
		EXEC('WITH Entries AS
		(SELECT ROW_NUMBER() OVER (ORDER BY ' + @OrderBy + ' ) AS ROW, ' + @Select 
			+ ' FROM ' + @TableS  + ') '
		+ 'SELECT *, MoreRecords = (SELECT COUNT(*) FROM Entries WHERE ROW > ' + @LastRec + ') FROM Entries T WHERE ROW BETWEEN ' + @FirstRec 
			+ ' AND ' + @LastRec) 
	END
	ELSE
	BEGIN
		EXEC('WITH Entries AS
		(SELECT ROW_NUMBER() OVER (ORDER BY ' + @OrderBy + ' ) AS ROW, ' + @Select 
			+ ' FROM ' + @TableS  + ' WHERE ' + @WhereClause + ')'
		+ 'SELECT *, MoreRecords = (SELECT COUNT(*) FROM Entries WHERE ROW > ' + @LastRec + ') FROM Entries T WHERE ROW BETWEEN ' + @FirstRec 
			+ ' AND ' + @LastRec) 
	END
END
ELSE
BEGIN
	IF (@WhereClause='')
		EXEC('SELECT ' + @Select + ' FROM ' + @TableS  + ' ORDER BY ' +  @OrderBy) 
	ELSE
		EXEC('SELECT ' + @Select + ' FROM ' + @TableS  + ' WHERE ' + @WhereClause + ' ORDER BY ' +  @OrderBy) 
END
SET NOCOUNT OFF


GO


