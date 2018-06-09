/*** Demo Database - Database Design Documents - (c) 2007 Evolutility, Inc.  ***/

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
