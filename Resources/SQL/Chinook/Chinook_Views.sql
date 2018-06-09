
/*******************************************************************************
   Create Views & SPs
********************************************************************************/


CREATE VIEW CH_VW_Employee AS 
SELECT ID, lastname+', '+firstname as name
FROM CH_Employee;

GO

CREATE VIEW CH_VW_Employee2 AS 
SELECT ID, lastname+', '+firstname as name
FROM CH_Employee;

GO

CREATE VIEW CH_VW_Customer AS 
SELECT ID, lastname+', '+firstname as name
FROM CH_Customer;

GO

CREATE VIEW CH_VW_TrackSameCustomer AS
 select t2.id, count(t2.id) as purchases, t2.name, t2.albumID, a.ArtistID as ArtistID, t1.ID as TrackID
 from CH_invoice i, CH_invoice i2, 
	CH_invoiceline il, CH_invoiceline il2, 
	CH_track t1, CH_track t2,
	CH_album a
 where i.customerID=i2.customerID
	and i.id=il.invoiceid
	and i2.id=il2.invoiceid
	and il.TrackID=t1.id
	and il2.TrackID=t2.id
	and t1.genreid=t2.genreid
	and t1.id<>t2.id
	and t2.albumID=a.id 
group by t2.name, t2.id, t2.albumID, t1.ID, ArtistID 

GO
 

CREATE VIEW CH_VW_DBColumn 
 AS
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


CREATE VIEW CH_VW_DBObject
 AS 
SELECT id, name, xtype, 'db'+rtrim(xtype)+'.gif' as pix,
CASE 
	WHEN xtype='U' THEN 'Table'
	WHEN xtype='V' THEN 'View'
	WHEN xtype='P' THEN 'Stored Procedure'
	ELSE 'Trigger'
END AS typeName
FROM sysobjects so
WHERE so.xtype in ('U','V','P','TR')
AND name like 'CH[_]%'

GO 

