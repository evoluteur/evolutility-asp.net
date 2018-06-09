/* Demo Database - Database Design Documents   */
/* www.evolutility.org - (c) 2009 Olivier Giulieri  */
/*
	This file is part of Evolutility CRUD Framework.
	Source link <http://www.evolutility.org/download/download.aspx>

	Evolutility is free software: you can redistribute it and/or modify
	it under the terms of the GNU Affero General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Evolutility is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU Affero General Public License
	along with Evolutility. If not, see <http://www.gnu.org/licenses/>.
*/

CREATE VIEW EvoVW_DBObjectColumn AS

SELECT  syscolumns.id, syscolumns.colid, syscolumns.name,
CASE 
	WHEN syscolumns.xtype = 231 THEN syscolumns.length/2
	ELSE syscolumns.length
END AS length, 
systypes.name AS typename, syscolumns.isnullable  
          FROM systypes (nolock), syscolumns (nolock)  
              WHERE syscolumns.xtype=systypes.xtype  
                  AND systypes.length<>256
GO 


CREATE VIEW evoVW_DBObjectList
 AS 

SELECT id, name, xtype, 'db'+rtrim(xtype)+'.gif' as pix,
CASE 
	WHEN xtype='U' THEN 'Table'
	WHEN xtype='V' THEN 'View'
	WHEN xtype='P' THEN 'Stored Procedure'
	ELSE 'Trigger'
END AS typeName
FROM sysobjects 
WHERE sysobjects.xtype in ('U','V','P','TR') 

GO 
