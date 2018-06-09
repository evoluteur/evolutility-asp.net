/* Demo Database : Photo Albums
 - (c) 2012 Olivier Giulieri - www.evolutility.org  */
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
	GNU Affero General Public License for more details.

	You should have received a copy of the GNU Affero General Public License
	along with Evolutility. If not, see <http://www.fsf.org/licensing/licenses/agpl-3.0.html>.
*/

CREATE TABLE [EVOL_Photo] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[UserID] [int] NOT NULL ,
	[Publish] [int] NULL ,
	[AlbumID] [int] NULL ,
	[filename] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[title] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[notes] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CreationDate] [datetime] NOT NULL ,
	[CommentCount] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [EVOL_PhotoAlbum] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[UserID] [int] NOT NULL ,
	[Publish] [int] NULL ,
	[title] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CategoryID] [int] NULL ,
	[notes] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CreationDate] [datetime] NOT NULL ,
	[CommentCount] [int] NULL 
) ON [PRIMARY]
GO

ALTER TABLE [EVOL_Photo] WITH NOCHECK ADD 
	CONSTRAINT [PK_EVOL_Photo] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [EVOL_PhotoAlbum] WITH NOCHECK ADD 
	CONSTRAINT [PK_EVOL_PhotoAlbum] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [EVOL_Photo] WITH NOCHECK ADD 
	CONSTRAINT [DF_EVOL_Photo_UserID] DEFAULT (1) FOR [UserID],
	CONSTRAINT [DF_EVOL_Photo_filename] DEFAULT ('') FOR [filename],
	CONSTRAINT [DF_EVOL_Photo_CreationDate] DEFAULT (getdate()) FOR [CreationDate],
	CONSTRAINT [DF_EVOL_Photo_CommentCount] DEFAULT (0) FOR [CommentCount]
GO

ALTER TABLE [EVOL_PhotoAlbum] WITH NOCHECK ADD 
	CONSTRAINT [DF_EVOL_PhotoAlbum_UserID] DEFAULT (1) FOR [UserID],
	CONSTRAINT [DF_EVOL_PhotoAlbum_CreationDate] DEFAULT (getdate()) FOR [CreationDate],
	CONSTRAINT [DF_EVOL_PhotoAlbum_CommentCount] DEFAULT (0) FOR [CommentCount]
GO



SET IDENTITY_INSERT EVOL_PhotoAlbum ON;

INSERT INTO EVOL_PhotoAlbum (ID, title, CategoryID, notes, CreationDate)
  VALUES (1, 'Venezuela', 1, '', getdate());
INSERT INTO EVOL_PhotoAlbum (ID, title, CategoryID, notes, CreationDate)
  VALUES (2, 'Bali', 1, '', getdate());

SET IDENTITY_INSERT EVOL_PhotoAlbum OFF; 

GO

INSERT INTO EVOL_Photo (Publish, AlbumID, filename, title, notes)
  VALUES (1, 1, 'canaima.jpg', 'Canaima', '');
INSERT INTO EVOL_Photo (Publish, AlbumID, filename, title, notes)
  VALUES (1, 1, 'sun.jpg', 'Sun rise', '');
INSERT INTO EVOL_Photo (Publish, AlbumID, filename, title, notes)
  VALUES (1, 2, 'batur1.jpg', 'Batur', '');
INSERT INTO EVOL_Photo (Publish, AlbumID, filename, title, notes)
  VALUES (1, 2, 'dragonfly1.jpg', 'Dragonfly', '');
INSERT INTO EVOL_Photo (Publish, AlbumID, filename, title, notes)
  VALUES (1, 2, 'shadow_puppet2.jpg', 'Shadow puppets', 'Like a movie...');

GO
