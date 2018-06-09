/* Address book Database - EVOL_Contacts, EVOL_ContactCategory   */
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
	
CREATE TABLE [EVOL_Contact] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Firstname] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Middlename] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Lastname] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Suffix] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[JobTitle] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Title] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Company] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CategoryID] [int] NOT NULL ,
	[Phone] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Phoneh] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Phonem] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Fax] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[emailAddress] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[url] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[AddressLine1] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[AddressLine2] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[City] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[State] [nvarchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Zip] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Country] [nvarchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Notes] [nvarchar] (800) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Custom1] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Custom2] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Custom3] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Custom4] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Publish] [int] NULL,
	[UserID] [int] NULL CONSTRAINT [DF_EVOL_Contact_UserID] DEFAULT (1),
	[CommentCount] [int] NULL CONSTRAINT [DF_EVOL_Contact_CommentCount] DEFAULT (0),
	[creationdate]  [datetime] NULL CONSTRAINT [DF_EvolContact_User_Creationdate]  DEFAULT (getdate())
) ON [PRIMARY]
GO

ALTER TABLE [EVOL_Contact] WITH NOCHECK ADD 
	CONSTRAINT [PK_EVOL_Contact] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO


 CREATE  INDEX [IDX_Firstname] ON [EVOL_Contact]([Firstname]) ON [PRIMARY]
GO

 CREATE  INDEX [IDX_Lastname] ON [EVOL_Contact]([Lastname]) ON [PRIMARY]
GO

 CREATE  INDEX [IDX_UserID] ON [EVOL_Contact]([UserID]) ON [PRIMARY]
GO

 CREATE  INDEX [IDX_CategoryID] ON [EVOL_Contact]([CategoryID]) ON [PRIMARY]
GO

 CREATE  INDEX [IDX_Publish] ON [EVOL_Contact]([Publish]) ON [PRIMARY]
GO

CREATE TABLE [EVOL_ContactCategory] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [EVOL_ContactCategory] WITH NOCHECK ADD 
	CONSTRAINT [PK_EVOL_ContactCategory] PRIMARY KEY  CLUSTERED 
	( 
		[ID]
	)  ON [PRIMARY] 
GO

INSERT INTO EVOL_ContactCategory (Name)  VALUES ('[Unfiled]');
INSERT INTO EVOL_ContactCategory (Name)  VALUES ('Hobby');
INSERT INTO EVOL_ContactCategory (Name)  VALUES ('Travel');
INSERT INTO EVOL_ContactCategory (Name)  VALUES ('Finances');
INSERT INTO EVOL_ContactCategory (Name)  VALUES ('Family');
INSERT INTO EVOL_ContactCategory (Name)  VALUES ('Business');
INSERT INTO EVOL_ContactCategory (Name)  VALUES ('Restaurant');
INSERT INTO EVOL_ContactCategory (Name)  VALUES ('Internet');
INSERT INTO EVOL_ContactCategory (Name)  VALUES ('Health');

GO

INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, Phone, Phoneh, Phonem, Fax, emailaddress, url, Addressline1, City, State, Zip, Notes)
  VALUES (1, 1, 'Olivier', 'Giulieri', 'CXO', 'Evolutility', 9, '', '', '', '', 'olivier@evolutility.org', 'http://www.evolutility.org', '', 'San Mateo', 'CA', '94401', 'Author of Evolutility');
INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, Phone, Phoneh, Phonem, Fax, emailaddress, url, Addressline1, City, State, Zip, Notes)
  VALUES (1, 2, 'John', 'Smith', 'Senior Developer', 'Test', 9, '415 123 1234', '415 123 1238', '', '', 'test@test.com', 'http://www.test.com', '123 2nd St.', 'San Francisco', 'CA', ' ', '');
INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, phone, Phoneh, Phonem, Fax, emailaddress, url, AddressLine1, City, State, Zip, Notes)
  VALUES (1, 2, 'Mary', 'Clark', 'MD', 'Acme Software', 9, '650 123 1456', '650 123 1456', '650 123 1456', '650 123 1456', 'jim@clark.com', '', '34 Olive Street', 'San Francisco', 'CA', '', 'He is Superman.');
INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, phone, Phoneh, Phonem, Fax, emailaddress, url, AddressLine1, City, State, Zip, Notes)
  VALUES (1, 1, 'Michael', 'Jackson', 'Singer', '', 1, '', '', '', '', '', 'http://www.michaeljackson.com', '', '', '', '', '');
INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, phone, Phoneh, Phonem, Fax, emailaddress, url, AddressLine1, City, State, Zip, Notes)
  VALUES (1, 3, 'John', 'Doe', 'CEO', 'Nasdaq', 2, '123 456 7891', '', '123 456 7891', '123 456 7891', 'johndoe@nasdaq.com', 'http://www.nasdaq.com', '123 Nowhere St', 'New York', 'NY', '94123', 'difficult to reach...');
INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, phone, Phoneh, Phonem, Fax, emailaddress, url, AddressLine1, City, State, Zip, Notes)
  VALUES (1, 3, 'Ralph', 'Smith', '', '', 6, '', '', '', '', '', '', '', '', '', '', '');
INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, phone, Phoneh, Phonem, Fax, emailaddress, url, AddressLine1, City, State, Zip, Notes)
  VALUES (1, 2, '', 'Crustacean', '', 'Crustacean', 1, '415 776 CRAB', '', '', '415 776 1069', '', '', '1475 Polk St', 'San Francisco', 'CA', '94109', 'Euro Asian, Excellent.');
INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, phone, Phoneh, Phonem, Fax, emailaddress, url, AddressLine1, City, State, Zip, Notes)
  VALUES (1, 2, 'Laura', 'White', '', 'CoolTool', 5, '415 123 1234', '415 234 5678', '415 456 7890', '415 123 1234', '', '', '', '', '', '', '');
INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, phone, Phoneh, Phonem, Fax, emailaddress, url, AddressLine1, City, State, Zip, Notes)
  VALUES (1, 1, 'Jean-Marc', 'Dupond', 'Commercial', 'FNAC', 1, '40 12 12 12 12', '', '', '', 'jmdupond@fnac.fr', 'http://www.fnac.fr', '', 'Paris', '', '75015', 'Tres sympa. Excellent au go.');
INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, phone, Phoneh, Phonem, Fax, emailaddress, url, AddressLine1, City, State, Zip, Notes)
  VALUES (1, 2, 'Han', 'Lee', 'Software Engineer', 'Microsoft', 1, '224 443 4433', '224 343 5554', '224 343 5555', '', 'hanlee@microsoft.com', 'http://www.microsoft.com', '452 12th Street', 'Redmond', 'WA', '', '');
INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, phone, Phoneh, Phonem, Fax, emailaddress, url, AddressLine1, City, State, Zip, Notes)
  VALUES (1, 2, 'Akemi', 'Inoue', 'Localization Engineer', 'Amazon', 3, '444 424 4444', '444 424 4455', '444 424 4466', '', 'akemiinoue@amazon.com', 'http://www.amazon.com', '1212 7th St', 'Redmond', 'WA', '', '');
INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, phone, Phoneh, Phonem, Fax, emailaddress, url, AddressLine1, City, State, Zip, Notes)
  VALUES (1, 1, 'Bart', 'Simpson', 'K-12', '', 1, '', '', '', '', '', '', '', '', '', '', 'I''ve seen him on TV');
INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, phone, Phoneh, Phonem, Fax, emailaddress, url, AddressLine1, City, State, Zip, Notes)
  VALUES (1, 2, 'Marie', 'Durand', 'Ingenieur Logiciel', 'CoolSoft', 1, '23 23 23 23 23', '12 12 23 34 45', '', '', 'mdurand@CoolSoft.fr', 'http://www.CoolSoft.fr', '', 'Paris', '', '', '');
INSERT INTO EVOL_Contact (Publish, UserID, Firstname, Lastname, Title, Company, CategoryID, phone, Phoneh, Phonem, Fax, emailaddress, url, AddressLine1, City, State, Zip, Notes)
  VALUES (1, 3, 'Sanjay', 'Ganesh', 'Director R&D', 'Tata', 5, '', '', '', '', '', '', '', '', '', '', '');

GO

