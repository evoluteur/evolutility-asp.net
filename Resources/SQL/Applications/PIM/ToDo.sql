/* To Do List  */
/* (c) 2011 Olivier Giulieri -  www.evolutility.org */
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

CREATE TABLE  EVOL_ToDo (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[UserID] [int] NOT NULL CONSTRAINT [DF_EVOL_ToDo_UserID] DEFAULT (1),
	[Publish] [int] NULL,
	[Title] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_EVOL_ToDo_Title] DEFAULT ('[Untitled]'),
	[PriorityID] [int] NULL,
	[CategoryID] [int] NULL ,
	[Complete] [bit] CONSTRAINT [DF_EVOL_ToDo_Complete] DEFAULT (0),
	[DueDate] [datetime] ,
	[Notes] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[CommentCount] [int] NULL CONSTRAINT [DF_EVOL_ToDo_CommentCount] DEFAULT (0),
	[CreationDate] [datetime] NULL CONSTRAINT [DF_EVOL_ToDo_creationdate] DEFAULT (getdate()),
 CONSTRAINT [PK_Evol_ToDo] PRIMARY KEY CLUSTERED ([ID] ASC) ON [PRIMARY])
GO
 
CREATE TABLE EVOL_ToDoCategory (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_EVOL_ToDoCategory_Name] DEFAULT (''),
 CONSTRAINT [PK_Evol_ToDoCategory] PRIMARY KEY CLUSTERED ([ID] ASC) ON [PRIMARY])
GO

CREATE TABLE EVOL_ToDoPriority (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_EVOL_ToDoPriority_Name] DEFAULT (''),
	[Description] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_EVOL_ToDoPriority_Description] DEFAULT (''),
 CONSTRAINT [PK_Evol_ToDoPriority] PRIMARY KEY CLUSTERED ([ID] ASC) ON [PRIMARY])
GO

/************* Indexes ***************************************/
   
CREATE  INDEX [EVOL_ToDo_CategoryID] ON [EVOL_ToDo]([CategoryID]) ON [PRIMARY]
GO

CREATE  INDEX [EVOL_ToDo_PriorityID] ON [EVOL_ToDo]([CategoryID]) ON [PRIMARY]
GO

CREATE  INDEX [EVOL_ToDo_Title] ON [EVOL_ToDo]([Title]) ON [PRIMARY]
GO

 

/*** Populates EVOL_ToDoCategory ***/
INSERT INTO EVOL_ToDoCategory (Name) VALUES ('[Unfiled]');
INSERT INTO EVOL_ToDoCategory (Name) VALUES ('Hobby');
INSERT INTO EVOL_ToDoCategory (Name) VALUES ('Travel');
INSERT INTO EVOL_ToDoCategory (Name) VALUES ('Finances');
INSERT INTO EVOL_ToDoCategory (Name) VALUES ('Family');
INSERT INTO EVOL_ToDoCategory (Name) VALUES ('Business');
INSERT INTO EVOL_ToDoCategory (Name) VALUES ('Misc.');
INSERT INTO EVOL_ToDoCategory (Name) VALUES ('Health');

GO

/*** Populates EVOL_ToDoPriority ***/
INSERT INTO EVOL_ToDoPriority (Name, Description) VALUES ('1-ASAP', 'ASAP');
INSERT INTO EVOL_ToDoPriority (Name, Description) VALUES ('2-Urgent', 'Urgent');
INSERT INTO EVOL_ToDoPriority (Name, Description) VALUES ('3-Important', 'Important');
INSERT INTO EVOL_ToDoPriority (Name, Description) VALUES ('4-Secondary', 'Secondary');
INSERT INTO EVOL_ToDoPriority (Name, Description) VALUES ('5-Whenever', 'Whenever');

GO

/*** Populates EVOL_ToDo ***/
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes, creationdate)
 VALUES (1, 1, 'Call John Doe', 4, 6, 0, getdate()+15, 'Remind him of our deal', getdate()-5);
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes, creationdate)
 VALUES (1, 1, 'Dentist', 5, 8, 0, getdate()+4, '', getdate()-1);
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes, creationdate)
 VALUES (3, 1, 'Take plane ticket...', 2, 3, 1, getdate()-3, '... for New Year where?', getdate()-2);
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes, creationdate)
 VALUES (3, 1, 'Backup system', 1, 6, 1, getdate()-10, '', getdate()-7);
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes, creationdate)
 VALUES (2, 1, 'Update Kakoo', 3, 6, 1, getdate()+4, 'w/ new controls', getdate());
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes, creationdate)
 VALUES (1, 1, 'email Jim', 1, 4, 1, getdate(), '', getdate()-3);
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes, creationdate)
 VALUES (1, 1, 'Car smog check', 1, 1, 0, getdate()-12, '', getdate()-3);
INSERT INTO EVOL_ToDo (UserID, Publish, Title, PriorityID, CategoryID, Complete, duedate, Notes, creationdate)
 VALUES (1, 1, 'Acupuncture  ', 4, 8, 0, getdate()+25, '', getdate()-5);

GO

