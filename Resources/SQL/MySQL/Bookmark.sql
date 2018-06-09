/* Bookmark */
/* ** www.evolutility.org - Wine Cellar - (c) 2010 Olivier Giulieri  ** */ 
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

CREATE TABLE `EVOL_Bookmark`(
	`ID` int(10) unsigned NOT NULL auto_increment,
	`UserID` int(10) unsigned default 1,
	`Title` varchar(200) DEFAULT '',
	`url` varchar(300) DEFAULT NULL,
	`CategoryID` int(10) unsigned default NULL,
	`notes` varchar(1000) DEFAULT NULL,
	`Publish` int(10) unsigned default 0,
	`CommentCount` int(10) unsigned default 0,
	`creationdate` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP, 
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
 

CREATE TABLE `EVOL_BookmarkCategory`(
	`ID` int(10) unsigned NOT NULL auto_increment,
	`name` varchar(100) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

INSERT INTO `EVOL_BookmarkCategory` (`name`) VALUES ('Web Design');
INSERT INTO `EVOL_BookmarkCategory` (`name`) VALUES ('CSS');
INSERT INTO `EVOL_BookmarkCategory` (`name`) VALUES ('Javascript');
INSERT INTO `EVOL_BookmarkCategory` (`name`) VALUES ('HTML');
INSERT INTO `EVOL_BookmarkCategory` (`name`) VALUES ('Database');
INSERT INTO `EVOL_BookmarkCategory` (`name`) VALUES ('Metadata');
INSERT INTO `EVOL_BookmarkCategory` (`name`) VALUES ('Sites to watch');
INSERT INTO `EVOL_BookmarkCategory` (`name`) VALUES ('ASP.net');
INSERT INTO `EVOL_BookmarkCategory` (`name`) VALUES ('Code');
 

INSERT INTO `EVOL_Bookmark` (Title, CategoryID, url, notes)
 VALUES ('A List Apart',1,'http://www.alistapart.com/','"For people who make websites"');
INSERT INTO `EVOL_Bookmark` (Title, CategoryID, url, notes)
 VALUES ('Evolutility.org',8,'http://www.evolutility.org','Smart and easy web applications');
INSERT INTO `EVOL_Bookmark` (Title, CategoryID, url, notes)
 VALUES ('The Code Project',9,'http://www.codeproject.com/','Development resource'); 
INSERT INTO `EVOL_Bookmark` (Title, CategoryID, url, notes)
 VALUES ('Zen Garden',2,'http://www.csszengarden.com/','The css Zen Garden invites you to relax and meditate on the important lessons of the masters. Begin to see with clarity. Learn to use the (yet to be) time-honored techniques in new and invigorating fashion. Become one with the web.');
INSERT INTO `EVOL_Bookmark` (Title, CategoryID, url, notes)
 VALUES ('SourceForge',9,'http://www.sourceforge.net','Open Source Repository'); 
INSERT INTO `EVOL_Bookmark` (Title, CategoryID, url, notes)
 VALUES ('Coolest icons',1,'http://www.sourceforge.net','Icon sets by Mark James http://www.famfamfam.com/lab/icons/silk/'); 
INSERT INTO `EVOL_Bookmark` (Title, CategoryID, url, notes)
 VALUES ('Ajaxian',3,'http://ajaxian.com/','Because you need to have the coolest rounded corners'); 
 

