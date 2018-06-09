/*  (c) 2012 Olivier Giulieri - www.evolutility.org    */
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

/*    SQL script using Evolutility in a multi-user environment     */
/* Users */

CREATE TABLE [EVOL_User] (
	[ID] [int] IDENTITY (1, 1) NOT NULL CONSTRAINT [PK_Evol_User] PRIMARY KEY  CLUSTERED ,
	[UserID] AS ([ID]) ,
	[Publish] [int] NULL ,
	[Admin] [int] NULL ,
	[intro] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Login] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Evol_User_Login] DEFAULT ('Anonymous'),
	[Password] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Evol_User_Password] DEFAULT (''),
	[TagLine] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Firstname] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Lastname] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[URL] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[email] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[phone] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[cell] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[fax] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Address] [nvarchar](300) NULL,
	[City] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[State] [nvarchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Zip] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Country] [nvarchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Notes] [nvarchar] (800) COLLATE SQL_Latin1_General_CP1_CI_AS NULL , 
	[lastvisit] [datetime] NULL ,
	[nbVisits] [int] NULL CONSTRAINT [DF_Evol_User_nbVisits] DEFAULT (0), 
	[CommentCount] [int] NULL CONSTRAINT [DF_Evol_User_CommentCount] DEFAULT (0),
	[creationdate]  [datetime] NOT NULL CONSTRAINT [DF_Evol_User_Creationdate]  DEFAULT (getdate())
) ON [PRIMARY]
GO
 
 
INSERT INTO EVOL_User (Publish, Admin, Login, TagLine, Password, Firstname, Lastname)
  VALUES (0, 1, 'Evol', 'Evolutility Administrator', 'love', 'Admin', 'Admin');

INSERT INTO EVOL_User (Publish, Login, TagLine, Password, Firstname, Lastname, URL, email, phone, intro)
  VALUES (1, 'John', 'DB guru', 'john', 'John', 'Smith', 'http://www.evolutility.com', 'info@evolutility.com', '', 'Contact me if you have any database questions');
INSERT INTO EVOL_User (Publish, Login, TagLine, Password, Firstname, Lastname, URL, email, phone, intro)
  VALUES (1, 'Mary', 'Hi', 'mary', 'Mary', 'Johnson', 'http://www.evolutility.org', 'mary@evolutility.com', '', 'Hi how are you.');

GO

/*  User Login  */

CREATE PROCEDURE EvoSP_Login  (@Login  nvarchar(50),	@Password nvarchar(50))
AS

DECLARE @userid INT
SELECT @userid =  ID FROM [EVOL_User] WHERE login= @Login AND password= @Password
IF (@userid>0)
  BEGIN
    	UPDATE [EVOL_User] SET lastvisit=getdate(), nbvisits=nbvisits+1  WHERE  ID= @userid
    	SELECT ID, login, firstname, 'Welcome ' + firstname AS welcome FROM [Evol_User]  WHERE  ID= @userid
  END
  
GO

CREATE PROCEDURE EvoDicoSP_Login  (@Login  nvarchar(50),	@Password nvarchar(50))
AS

DECLARE @userid INT
SELECT @userid =  ID FROM [EVOL_User] WHERE login= @Login AND password= @Password and admin=1
IF (@userid>0)
  BEGIN
    	UPDATE [EVOL_User] SET lastvisit=getdate(), nbvisits=nbvisits+1  WHERE  ID= @userid
    	SELECT ID, login, firstname, 'Welcome ' + firstname AS welcome FROM [Evol_User]  WHERE  ID= @userid 
  END
  
GO



/*  User comments  */

CREATE TABLE [EVOL_Comment] (
	[ID] [int] IDENTITY (1, 1) NOT NULL CONSTRAINT [PK_Evol_Comment] PRIMARY KEY  CLUSTERED ,
	[FormID] [int] NULL ,
	[ItemID] [int] NULL ,
	[UserID] [int] NULL ,
	[creationdate] [smalldatetime] NULL CONSTRAINT [DF_Evol_Comments_cdate] DEFAULT (getdate())  ,
	[message] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO
  
 CREATE  INDEX [IDX_UserID] ON [EVOL_Comment]([UserID]) ON [PRIMARY]
GO

 CREATE  INDEX [IDX_ItemID] ON [EVOL_Comment]([ItemID]) ON [PRIMARY]
GO


