/* Address book Database for MySQL   */
/* DB: EVOL_Contacts, EVOL_ContactCategory   */
/* www.evolutility.org - (c) 2010 Olivier Giulieri  */
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

CREATE TABLE `EVOL_Contact` (
	`ID`  int(10) unsigned NOT NULL auto_increment,
	`Firstname`varchar(50) default NULL,
	`Middlename` varchar(5) NULL,
	`Lastname` varchar(50) NOT NULL default '',
	`Suffix` varchar(10) NULL,
	`JobTitle`varchar(50) default NULL,
	`Title`varchar(50) default NULL,
	`Company`varchar(50) default NULL,
	`CategoryID` int(10) unsigned default 0,
	`Phone` varchar(15) NULL,
	`Phoneh` varchar(15) NULL,
	`Phonem` varchar(15) NULL,
	`Fax` varchar(15) NULL,
	`emailAddress` varchar(255) NULL,
	`url` varchar(255) NULL,
	`AddressLine1` varchar(250) NULL,
	`AddressLine2` varchar(250) NULL,
	`City` varchar(100) NULL,
	`State` varchar(2) NULL,
	`Zip` varchar(15) NULL,
	`Country` varchar(60) NULL,
	`Notes` varchar(800) NULL,
	`Custom1` varchar(250) NULL,
	`Custom2` varchar(250) NULL,
	`Custom3` varchar(250) NULL,
	`Custom4` varchar(250) NULL,
	`Publish` int(10) unsigned default 0,
	`UserID` int(10) unsigned default 1,
	`CommentCount` int(10) unsigned default 0,
	`creationdate` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP, 
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8; 
  

CREATE TABLE `EVOL_ContactCategory` (
	`ID`  int(10) unsigned NOT NULL auto_increment,
	`Name` varchar(50) NOT NULL default '',
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;  
 

INSERT INTO EVOL_ContactCategory (`Name`)  VALUES ('Unfiled');
INSERT INTO EVOL_ContactCategory (`Name`)  VALUES ('Hobby');
INSERT INTO EVOL_ContactCategory (`Name`)  VALUES ('Travel');
INSERT INTO EVOL_ContactCategory (`Name`)  VALUES ('Finances');
INSERT INTO EVOL_ContactCategory (`Name`)  VALUES ('Family');
INSERT INTO EVOL_ContactCategory (`Name`)  VALUES ('Business');
INSERT INTO EVOL_ContactCategory (`Name`)  VALUES ('Restaurant');
INSERT INTO EVOL_ContactCategory (`Name`)  VALUES ('Internet');
INSERT INTO EVOL_ContactCategory (`Name`)  VALUES ('Health');
 

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
 

