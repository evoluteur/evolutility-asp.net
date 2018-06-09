/*** Demo Database : Photo Albums for MySQL  
 - www.evolutility.org - (c) 2010 Olivier Giulieri  ***/
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

	You should have received a copy of the GNU Affero General Public License Version 3
	along with Evolutility. If not, see <http://www.fsf.org/licensing/licenses/agpl-3.0.html>.
*/

CREATE TABLE `EVOL_Photo` (
	`ID`  int(10) unsigned NOT NULL auto_increment,
	`UserID` int(10) unsigned default 1,
	`Publish` int(10) unsigned default 0,
	`AlbumID` int(10) unsigned default NULL,
	`filename`  varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	`title`  varchar(50) default NULL,
	`notes`  varchar(200) default NULL,
	`CommentCount` int(10) unsigned default 0,
	`CreationDate` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMPPRIMARY KEY  (`ID`), 
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `EVOL_PhotoAlbum` (
	`ID`  int(10) unsigned NOT NULL auto_increment,
	`UserID` int(10) unsigned default 1,
	`Publish` int(10) unsigned default 0,
	`title`  varchar(50) default 'Untitled',
	`CategoryID` int(10) unsigned default NULL,
	`notes`  varchar(200) default NULL,
	`CommentCount` int(10) unsigned default 0,
	`CreationDate` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMPPRIMARY KEY  (`ID`),
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
  

SET IDENTITY_INSERT EVOL_PhotoAlbum ON;

INSERT INTO EVOL_PhotoAlbum (ID, title, CategoryID, notes)
  VALUES (1, 'Venezuela', 1, '');
INSERT INTO EVOL_PhotoAlbum (ID, title, CategoryID, notes)
  VALUES (2, 'Bali', 1, '');

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
