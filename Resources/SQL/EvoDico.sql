/*    www.evolutility.org - (c) 2009 Olivier Giulieri     */
/*    EvoDico 2.5 - Evolutility database dictionary    */
/*    last updated  02/16/2009   */ 
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

/****** Object:  Table [EvoDico_Form]  ************************************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EvoDico_Form](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL CONSTRAINT [DF_EvoDico_Form_UserID]  DEFAULT (1),
	[Publish] [int] NULL,
	[Title] [nvarchar](100) NULL,
	[icon] [nvarchar](50) NULL CONSTRAINT [DF_EvoDico_Form_icon]  DEFAULT ('cube.gif'),
	[entity] [nvarchar](50) NULL,
	[entities] [nvarchar](50) NULL,
	[dbtable] [nvarchar](100) NULL,
	[dbwhere] [nvarchar](150) NULL,
	[dbwherelock] [nvarchar](50) NULL,
	[dborder] [nvarchar](200) NULL,
	[dbcolumnlead] [nvarchar](100) NULL,
	[dbcolumnpk] [nvarchar](50) NULL,
	[dbcolumnicon] [nvarchar](50) NULL,
	[dbtablecomments] [nvarchar](50) NULL,
	[dbtableusers] [nvarchar](50) NULL,
	[script] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NULL,
	[sppaging] [nvarchar](300) NULL,
	[splogin] [nvarchar](200) NULL,
	[spget] [nvarchar](200) NULL,
	[spdelete] [nvarchar](200) NULL,
	[useTabs] [bit] NULL,
	[dbDetails] [bit] NULL,
	[xmlfile] [nvarchar](200) NULL,
	[url] [nvarchar](200) NULL,
	[help] [nvarchar](500) NULL,
	[CommentsID] [int] NULL,
	[CreationDate] [datetime] NULL CONSTRAINT [DF_EvoDico_Form_CreationDate]  DEFAULT (getdate()),
	[wmodifdate] [smalldatetime] NULL,
	[wpublishdate] [smalldatetime] NULL,
 CONSTRAINT [PK_EvoDico_Form] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [EvoDico_Panel]  ************************************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EvoDico_Panel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FormID] [int] NULL,
	[UserID] [int] NULL CONSTRAINT [DF_EvoDico_Panel_UserID]  DEFAULT ((1)),
	[TypeID] [int] NULL CONSTRAINT [DF_EvoDico_Panel_PanelTypeID]  DEFAULT ((1)),
	[ModeID] [int] NULL CONSTRAINT [DF_EvoDico_Panel_PanelModeID]  DEFAULT ((1)),
	[TabID] [int] NULL CONSTRAINT [DF_EvoDico_Panel_TabID]  DEFAULT ((0)),
	[Label] [nvarchar](100) NOT NULL CONSTRAINT [DF_EvoDico_Panel_Label]  DEFAULT (''),
	[Width] [nvarchar](10) NULL CONSTRAINT [DF_EvoDico_Panel_Width]  DEFAULT ('100'),
	[CSSClass] [nvarchar](20) NULL,
	[CSSClassLabel] [nvarchar](20) NULL,
	[Readonly] [smallint] NULL,
	[Optional] [bit] NULL,
	[pPos] [smallint] NULL CONSTRAINT [DF_EvoDico_Panel_ppos]  DEFAULT ((1)),
	[DBtableDetails] [nvarchar](100) NULL,
	[DBcolumnDetails] [nvarchar](100) NULL,
	[DBWhere] [nvarchar](200) NULL,
	[DBOrder] [nvarchar](100) NULL,
	[Pix] [nvarchar](50) NULL,
	[Pix2] [nvarchar](50) NULL,
	[help] [nvarchar](500) NULL,
	[Summary] [nvarchar](250) NULL,
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_EvoDico_Panel_creationdate] DEFAULT (getdate()),
 CONSTRAINT [PK_EvoDico_Panel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_EvoDico_Panel_FormID ON EvoDico_Panel
	(FormID) ON [PRIMARY]
GO
 
ALTER TABLE EvoDico_Panel
ADD CONSTRAINT fk_EvoDico_Panel2Form
FOREIGN KEY (FormID) 
REFERENCES EvoDico_Form(ID)  ON DELETE CASCADE
GO
 

/****** Object:  Table [EvoDico_Field]  ************************************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EvoDico_Field](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FormID] [int] NOT NULL,
	[UserID] [int] NULL CONSTRAINT [DF_EvoDico_Field_UserID]  DEFAULT ((1)),
	[Publish] [int] NULL,
	[PanelID] [int] NOT NULL,
	[PanelIndex] [smallint] NULL,
	[Label] [nvarchar](100) NOT NULL CONSTRAINT [DF_EvoDico_Field_Label]  DEFAULT (''),
	[LabelEdit] [nvarchar](100) NULL,
	[LabelList] [nvarchar](50) NULL,
	[TypeID] [int] NOT NULL CONSTRAINT [DF_EvoDico_Field_TypeID]  DEFAULT ((5)),
	[dbcolumn] [nvarchar](100) NOT NULL CONSTRAINT [DF_EvoDico_Field_dbcolumn]  DEFAULT (''),
	[dbcolumnread] [nvarchar](100) NOT NULL CONSTRAINT [DF_EvoDico_Field_dbcolumnread]  DEFAULT (''),
	[dbcolumnicon] [nvarchar](100) NULL,
	[jsvalidation] [nvarchar](50) NULL,
	[jsdependency] [nvarchar](50) NULL,
	[dependency] [nvarchar](100) NULL,
	[min] [int] NULL,
	[max] [int] NULL,
	[maxlength] [smallint] NULL CONSTRAINT [DF_EvoDico_Field_maxlength]  DEFAULT ((100)),
	[regexp] [nvarchar](30) NULL,
	[dbtablelov] [nvarchar](100) NULL,
	[dborderlov] [nvarchar](100) NULL,
	[dbcolumnreadlov] [nvarchar](250) NULL,
	[dbcolumndetails] [nvarchar](100) NULL,
	[dbwherelov] [nvarchar](250) NULL,
	[lovmany] [smallint] NULL CONSTRAINT [DF_EvoDico_Field_lovmany]  DEFAULT ((0)),
	[lovsplist] [nvarchar](100) NULL,
	[img] [nvarchar](100) NULL,
	[imglist] [nvarchar](100) NULL,
	[defaultvalue] [nvarchar](100) NULL,
	[readonly] [smallint] NULL CONSTRAINT [DF_EvoDico_Field_readonly]  DEFAULT ((0)),
	[required] [bit] NULL CONSTRAINT [DF_EvoDico_Field_required]  DEFAULT ((0)),
	[optional] [bit] NULL CONSTRAINT [DF_EvoDico_Field_optional]  DEFAULT ((0)),
	[format] [nvarchar](30) NULL,
	[fpos] [smallint] NULL CONSTRAINT [DF_EvoDico_Field_fpos]  DEFAULT ((10)),
	[link] [nvarchar](150) NULL,
	[linklabel] [nvarchar](100) NULL,
	[linktarget] [nvarchar](20) NULL,
	[search] [bit] NULL CONSTRAINT [DF_EvoDico_Field_search]  DEFAULT ((1)),
	[searchadv] [bit] NULL CONSTRAINT [DF_EvoDico_Field_searchadv]  DEFAULT ((1)),
	[searchlist] [bit] NULL CONSTRAINT [DF_EvoDico_Field_searchlist]  DEFAULT ((1)),
	[lookup] [bit] NULL,
	[cssclass] [nvarchar](20) NULL,
	[cssclassview] [nvarchar](20) NULL,
	[cssclasslabel] [nvarchar](20) NULL,
	[height] [smallint] NULL CONSTRAINT [DF_EvoDico_Field_height]  DEFAULT ((1)),
	[width] [smallint] NULL CONSTRAINT [DF_EvoDico_Field_width]  DEFAULT ((100)),
	[help] [nvarchar](500) NULL,
	[options] [text] NULL,
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_EvoDico_Field_creationdate] DEFAULT (getdate()),
 CONSTRAINT [PK_EvoDico_Field] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_EvoDico_Field_FormID ON EvoDico_Field
	(FormID) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_EvoDico_Field_PanelID ON EvoDico_Field
	(PanelID) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_EvoDico_Field_TypeID ON EvoDico_Field
	(TypeID) ON [PRIMARY]
GO

/* 
ALTER TABLE EvoDico_Field
ADD CONSTRAINT fk_EvoDico_Field2Form  
FOREIGN KEY (FormID) 
REFERENCES EvoDico_Form(ID)  ON DELETE CASCADE 
GO
*/

ALTER TABLE EvoDico_Field
ADD CONSTRAINT fk_EvoDico_Field2Panel  
FOREIGN KEY (PanelID) 
REFERENCES EvoDico_Panel(ID)  ON DELETE CASCADE 
GO


/****** Object:  Table [EvoDico_Selection]  ************************************************************/
/*
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EvoDico_Selection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FormID] [int] NULL,
	[UserID] [int] NULL,
	[Label] [nvarchar](200) NULL,
	[Description] [nvarchar](500) NULL,
	[QuerySQL] [nvarchar](1500) NULL,
	[publish] [int] NULL,
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_EvoDico_Selection_creationdate] DEFAULT (getdate()),
 CONSTRAINT [PK_EvoDico_Selection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_EvoDico_Selection_FormID ON EvoDico_Selection
	(FormID) ON [PRIMARY]
GO


ALTER TABLE EvoDico_Selection
ADD CONSTRAINT fk_EvoDico_Selection2Form
FOREIGN KEY (FormID) 
REFERENCES EvoDico_Form(ID)  ON DELETE CASCADE
GO
*/

/****** Object:  Table [EvoDico_FieldType]  ************************************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EvoDico_FieldType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ftPos] [int] NULL,
	[name] [nvarchar](255) NOT NULL CONSTRAINT [DF_EvoDico_FieldType_name]  DEFAULT (''),
	[typePix] [nvarchar](20) NULL,
	[xmlname] [nvarchar](30) NULL,
	[sqlname] [nvarchar](30) NULL,
	[description] [nvarchar](100) NULL,
	[publish] [int] NULL,
 CONSTRAINT [PK_EvoDico_FieldType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
 
/****** Object:  View [EvoDico_xField]  ************************************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [EvoDico_xField]
AS
SELECT f.ID, f.FormID, ft.xmlname AS Type, ft.typepix, f.TypeID, 
		f.UserID, f.Label, f.LabelEdit, f.LabelList, f.dbcolumn, 
		f.dbcolumnread, f.dbcolumnicon, f.maxlength, f.dbtablelov, f.dborderlov, 
		f.dbcolumnreadlov, f.dbcolumndetails, f.dbwherelov, f.img, f.imglist, 
		f.jsvalidation,	f.jsdependency,	f.dependency,
		f.defaultvalue, f.readonly, f.required, f.optional, f.format, 
		f.PanelID, f.PanelIndex, f.fpos, f.link, f.linklabel, 
		f.linktarget, f.search, f.searchadv, f.searchlist, f.lookup, 
		f.cssclass, f.cssclassview, f.cssclasslabel, f.height, f.width, f.help, f.lovsplist
FROM         EvoDico_Field as f INNER JOIN
                      EvoDico_FieldType as ft ON f.TypeID = ft.ID
GO
 
/****** Object:  View [EvoDico_xFormPanels]    ************************************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [EvoDico_xFormPanels]
AS
SELECT f.ID AS fieldID, p.ID, p.FormID, p.UserID, p.TypeID, 
		p.Readonly, p.ModeID, p.TabID, p.Label, p.Width, p.help, 
		p.CSSClass, p.CSSClassLabel, p.Optional, p.pPos, p.DBtableDetails, 
		p.DBcolumnDetails, p.DBWhere, p.DBOrder, p.Pix, p.Pix2, 
		p.Summary, p.DBOrder AS Expr1
FROM   EvoDico_Panel as p INNER JOIN
                      EvoDico_Field as f ON p.FormID = f.FormID
WHERE  (p.TypeID = 1)
GO
 
/****** Object:  View [EvoDico_vFieldType]   ************************************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [EvoDico_vFieldType]
AS
SELECT     ID, ftPos, name, TypePix, xmlname, sqlname, description, publish
FROM         EvoDico_FieldType
WHERE     (publish = 1) 
GO
 
/****** Object:  View [EvoDico_vPanel]   ************************************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [EvoDico_vPanel]
AS
SELECT     f.Title + ' - ' + p.Label AS FormName, p.ID, p.pPos, p.FormID, 
                      p.Label, p.Width, p.CSSClass, p.Optional, p.Summary
FROM         EvoDico_Panel as p INNER JOIN
                      EvoDico_Form as f ON p.FormID = f.ID
WHERE     (p.TypeID = 1)
GO
 
/****** Object:  View [EvoDico_xPanel]   ************************************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [EvoDico_xPanel]
AS
SELECT     ID, FormID, UserID, TypeID, pPos, Label + ' - ' + Label AS Label, Width, Help, CSSClass, Optional, TabID, Summary
FROM         EvoDico_Panel
WHERE     (TypeID = 1)
GO
 
/****** Object:  StoredProcedure [EvoDico_Form_Get]   ************************************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [EvoDico_Form_Get] 
(
    @FormID int,
	@UserID int 
)
AS 

-- Form --
SELECT ID, Title, Description, help 
FROM EvoDico_Form  WHERE ID=@FormID;
-- Data --
SELECT ID, dbtable, dbwhere, dborder, entity, entities, icon, help, 
	dbcolumnlead, dbcolumnpk, dbcolumnicon, 
		script, dbtableusers, dbtablecomments,
		sppaging, splogin, spget, spdelete
	FROM EvoDico_Form  WHERE ID=@FormID;
-- Panels --
SELECT     ID, FormID, UserID, TypeID, pPos, Label, Width, CSSClass, Optional, TabID, Summary
FROM       EvoDico_Panel WHERE FormID=@FormID ORDER BY ppos, ID;
-- Fields --
SELECT * FROM EvoDico_xField WHERE FormID=@FormID ORDER BY fpos, ID;
-- Panel-details and Fields --
-- SELECT * FROM EvoDico_xPanelDetails  WHERE FormID=@FormID ORDER BY ppos, ID;
-- SELECT * FROM EvoDico_xFieldDetails  WHERE FormID=@FormID ORDER BY fpos, ID;
GO

 
/****** Object:  StoredProcedure [EvoDico_Form_GetHelp]   ************************************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE  [EvoDico_Form_GetHelp] 
(
    @FormID int,
	@UserID int 
)
AS 

SELECT DBColumn as id, Help as help 
FROM EvoDico_Field 
WHERE FormID=@FormID AND Help<>'';

GO

/****** Object:  View [EvoDocV_Field]   ************************************************************/ 

CREATE VIEW  EvoDocV_Field AS 
SELECT frm.id AS FormID, sc.colid AS ID, sc.name,
	CASE 
		WHEN sc.xtype = 231 THEN sc.length/2
		ELSE sc.length
	END AS length,
	systypes.name AS typename, 
	sc.isnullable  
FROM EvoDico_Form (nolock) frm,  systypes (nolock), syscolumns (nolock) sc,  sysobjects (nolock) so
WHERE so.id=sc.id 
	AND sc.xtype=systypes.xtype  
	AND systypes.length<>256
	AND so.name=frm.dbtable 
GO	

/****** Seed data for EvoDico - Evolutility database dictionary  **********************************/ 

SET IDENTITY_INSERT EvoDico_FieldType ON;

INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (1, 1000, 'Boolean', 'dico/ft-bool.gif', 'boolean', 'bit', 'Yes or No', 1);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (2, 40, 'Date', 'dico/ft-date.gif', 'date', 'datetime', 'Date', 1);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (3, 25, 'email', 'dico/ft-email.gif', 'email', 'nvarchar', 'email', 1);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (4, 30, 'List of Values', 'dico/ft-lov.gif', 'lov', 'int', 'List of values', 1);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (5, 10, 'Text ', 'dico/ft-txt.gif', 'text', 'nvarchar', 'Text ', 1);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (6, 20, 'Text Multiline', 'dico/ft-txtml.gif', 'textmultiline', 'text', 'Long Text', 1);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (7, 1000, 'URL', 'dico/ft-url.gif', 'url', 'nvarchar', 'Link', 1);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (8, 1000, 'HTML', 'dico/ft-htm.gif', 'html', 'nvarchar', 'HTML', 1);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (9, 510, 'Decimal', 'dico/ft-dec.gif', 'decimal', 'money', 'Decimal number', 1);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (10, 500, 'Integer', 'dico/ft-int.gif', 'integer', 'int', 'Integer', 1);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (11, 1000, 'Image', 'dico/ft-img.gif', 'image', 'nvarchar', 'Image', 1);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (12, 1000, 'Document', 'dico/ft-doc.gif', 'document', 'nvarchar', 'Document', 1);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (17, 41, 'Date-Time', 'dico/ft-datehm.gif', 'datetime', 'datetime', 'Date and Time (as one field)', 1);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (18, 42, 'Time', 'dico/ft-time.gif', 'time', 'datetime', 'Time', 1);
  
/*
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (13, 1000, 'Hidden', 'ft-hid.gif', 'hidden', 'nvarchar', 'Hidden Field', 0);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (14, 1000, 'Literal', 'ft-lit.gif', 'literal', 'nvarchar', 'Literal', 0);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (15, 1000, 'Place Holder', 'ft-ph.gif', 'placeholder', 'nvarchar', 'Place Holder', 0);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (16, 1000, 'List of Multiple Values', 'ft-lov2.gif', 'lov2', 'int', 'List of values (multiple)', 0);
INSERT INTO EvoDico_FieldType (ID, ftPos, name, TypePix, xmlname, sqlname, description, publish)
  VALUES (19, 1000, 'Formula', 'ft-fn.gif', 'formula', 'nvarchar', '', 0);
 */
 
SET IDENTITY_INSERT EvoDico_FieldType OFF;


