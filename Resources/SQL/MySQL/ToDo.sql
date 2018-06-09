/* To Do List  */
/* www.evolutility.org - (c) 2012 Olivier Giulieri */
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

CREATE TABLE  EVOL_ToDo (
	`ID`  int(10) unsigned NOT NULL auto_increment,
	`UserID` int(10) unsigned default 1,
	`Publish` int(10) unsigned default 0,
	`Title`  varchar(255)  DEFAULT 'Untitled',
	`PriorityID` int(10) unsigned DEFAULT NULL,
	`CategoryID` int(10) unsigned DEFAULT NULL,
	`Complete` int(1) DEFAULT 0,
	`DueDate`  DATETIME ,
	`Notes`  varchar(1000) DEFAULT NULL,
	`CommentCount` int(10) unsigned default 0,
	`CreationDate`  timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
 
CREATE TABLE EVOL_ToDoCategory (
	`ID`  int(10) unsigned NOT NULL auto_increment,
	`Name`  varchar(50) DEFAULT '',
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE EVOL_ToDoPriority (
	`ID`   int(10) unsigned NOT NULL auto_increment,
	`Name` varchar(50) DEFAULT '',
	`Description`  varchar(50) DEFAULT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
 

/*** Populates EVOL_ToDoCategory ***/
INSERT INTO EVOL_ToDoCategory (`Name`) VALUES ('Unfiled');
INSERT INTO EVOL_ToDoCategory (`Name`) VALUES ('Hobby');
INSERT INTO EVOL_ToDoCategory (`Name`) VALUES ('Travel');
INSERT INTO EVOL_ToDoCategory (`Name`) VALUES ('Finances');
INSERT INTO EVOL_ToDoCategory (`Name`) VALUES ('Family');
INSERT INTO EVOL_ToDoCategory (`Name`) VALUES ('Business');
INSERT INTO EVOL_ToDoCategory (`Name`) VALUES ('Misc.');
INSERT INTO EVOL_ToDoCategory (`Name`) VALUES ('Health');
 

/*** Populates EVOL_ToDoPriority ***/
INSERT INTO EVOL_ToDoPriority (Name, Description) VALUES ('1-ASAP', 'ASAP');
INSERT INTO EVOL_ToDoPriority (Name, Description) VALUES ('2-Urgent', 'Urgent');
INSERT INTO EVOL_ToDoPriority (Name, Description) VALUES ('3-Important', 'Important');
INSERT INTO EVOL_ToDoPriority (Name, Description) VALUES ('4-Secondary', 'Secondary');
INSERT INTO EVOL_ToDoPriority (Name, Description) VALUES ('5-Whenever', 'Whenever');
 

/*** Populates EVOL_ToDo ***/
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes)
 VALUES (1, 1, 'Call John Doe', 4, 6, 0, '2012-08-08', 'Remind him of our deal');
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes)
 VALUES (1, 1, 'Dentist', 5, 8, 0, CURTIME()+4, '');
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes)
 VALUES (3, 1, 'Take plane ticket...', 2, 3, 1, CURTIME()-3, '... for New Year where?');
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes)
 VALUES (3, 1, 'Backup system', 1, 6, 1, CURTIME()-10, '');
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes)
 VALUES (2, 1, 'Update Kakoo', 3, 6, 1, CURTIME()+4, 'w/ new controls');
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes)
 VALUES (1, 1, 'email Jim', 1, 4, 1, CURTIME(), '');
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes)
 VALUES (1, 1, 'Car smog check', 1, 1, 0, CURTIME()-12, '');
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes)
 VALUES (1, 1, 'Acupuncture  ', 4, 8, 0, CURTIME()+25, '');
 


INSERT INTO EVOL_ToDo (UserID, Publish, Title )
 VALUES (1, 1, 'Call John Doe' );