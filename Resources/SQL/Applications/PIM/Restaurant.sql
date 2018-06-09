/* Restaurants   
 (c) 2011 Olivier Giulieri - www.evolutility.org */
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

CREATE TABLE [EVOL_Restaurant] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_EVOL_Restaurant_Name] DEFAULT ('[Untitled]'),
	[UserID] [int] NULL ,
	[Publish] [int] NULL,
	[CommentCount] [int] NULL CONSTRAINT [DF_EVOL_Restaurant_CommentCount] DEFAULT (0),
	[CategoryID] [int] NULL CONSTRAINT [DF_EVOL_Restaurant_CategoryID] DEFAULT (1),
	[Price] [money] NULL ,
	[RatingID] [int] NOT NULL CONSTRAINT [DF_EVOL_Restaurant_RatingID] DEFAULT (1),
	[Phone] [nvarchar] (16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Phone2] [nvarchar] (16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Fax] [nvarchar] (16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[email] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[url] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[urlMap] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[address1] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[address2] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[City] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[State] [nvarchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Zip] [nvarchar] (12) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Favorite] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Comments] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[creationdate] [datetime] NULL CONSTRAINT [DF_EVOL_Restaurant_creationdate] DEFAULT (getdate())
) ON [PRIMARY]
GO
  
CREATE TABLE [EVOL_Restaurant_Category] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_EVOL_Restaurant_Category_Name] DEFAULT ('')
) ON [PRIMARY]
GO

CREATE TABLE [EVOL_Restaurant_Rating] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_EVOL_Restaurant_Rating_Name] DEFAULT (''),
	[Description] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [EVOL_Restaurant_Price] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL  CONSTRAINT [DF_EVOL_Restaurant_Price_Name] DEFAULT ('')
) ON [PRIMARY]
GO

ALTER TABLE [EVOL_Restaurant] WITH NOCHECK ADD 
	CONSTRAINT [PEVOL_Restaurant] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [EVOL_Restaurant_Category] WITH NOCHECK ADD 
	CONSTRAINT [PEVOL_Restaurant_Category] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [EVOL_Restaurant_Rating] WITH NOCHECK ADD 
	CONSTRAINT [PEVOL_Restaurant_Rating] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [EVOL_Restaurant_Price] WITH NOCHECK ADD 
	CONSTRAINT [PEVOL_Restaurant_Price] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO


 CREATE  INDEX [IDX_EVOL_Restaurant_name] ON [EVOL_Restaurant]([Name]) ON [PRIMARY]
GO

 CREATE  INDEX [IDX_EVOL_Restaurant_Category] ON [EVOL_Restaurant]([CategoryID]) ON [PRIMARY]
GO

INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('[unfiled]'); 
INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('French');
INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('Vietnamese');
INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('Chinese');
INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('Fusion');
INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('Japanese');
INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('Thai');
INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('Mexican');
INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('Mediterranean');
INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('American');
INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('Indian');
INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('Korean');
INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('Italian');
INSERT INTO EVOL_Restaurant_Category (Name) VALUES ('Spanish');

GO

INSERT INTO EVOL_Restaurant_Rating (Name) VALUES ('NR');
INSERT INTO EVOL_Restaurant_Rating (Name) VALUES ('');
INSERT INTO EVOL_Restaurant_Rating (Name) VALUES ('*');
INSERT INTO EVOL_Restaurant_Rating (Name) VALUES ('**');
INSERT INTO EVOL_Restaurant_Rating (Name) VALUES ('***');
INSERT INTO EVOL_Restaurant_Rating (Name) VALUES ('****');
INSERT INTO EVOL_Restaurant_Rating (Name) VALUES ('*****');

GO


INSERT INTO EVOL_Restaurant_Price (Name) VALUES ('NR');
INSERT INTO EVOL_Restaurant_Price (Name) VALUES ('$');
INSERT INTO EVOL_Restaurant_Price (Name) VALUES ('$$');
INSERT INTO EVOL_Restaurant_Price (Name) VALUES ('$$$');
INSERT INTO EVOL_Restaurant_Price (Name) VALUES ('$$$$'); 

GO

INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('42 Degrees', 9, 0, 1, '(415) 777-5558', '', '', '', '', '235 16th & Illinois', '', 'San Francisco', 'CA', '', '', 'California Mediterranean Restaurant', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('ABC Seafood', 4, 0, 5, '650 328 2288', '', '650 358 9764', '', '', '973 E. Hillsdale Blvd B-5', '', 'Foster City', 'CA', '94404', 'dim sum', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Amira', 13, 0, 1, '415 621 6213', '', '', '', '', '590 Valencia St.', '', 'San Francisco', 'CA', '94110', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Anamandara', 3, 0, 6, '415 771 6800', '', '', '', 'http://www.anamandara.com', '891 Beach Street at Polk', '', 'San Francisco', 'CA', '94122', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Anjou', 2, 0, 1, '415 392 5373', '', '', '', '', '44 Campton Place', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Arayssi', 1, 0, 1, '718 745 2115', '', '718 745 2820', '', '', '7216 5th Ave', '', 'Brooklyn', 'NY', '11209', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Basil', 7, 0, 5, '415 552 8999', '', '', '', '', '1175 Folsom St', '', 'San Francisco', 'CA', '94103', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Adriano''s', 13, 0, 1, '415 474 4180', '', '', '', '', '3347 Fillmore St (@ Chestnuts)', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Benihana', 6, 0, 1, '415 563 4844', '', '', '', 'http://www.benihana.com', '1737 Post St', '', 'San Francisco', 'CA', '94115', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Breads of India', 11, 0, 1, '510 849 2452', '', '', '', '', '1700 Shattuck Ave @ Virginia', '', 'Berkeley', 'CA', '94709', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Calzone''s', 13, 0, 1, '415 397 3600', '', '', '', 'http://www.calzonesf.com', '430 Columbus Ave', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Casa Madrona', 13, 0, 1, '415 331 5888', '', '', '', '', '801 bridge way', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Cassis Bistro', 2, 0, 1, '415 292 0770', '', '', '', '', '2120 Greenwich St', '', 'San Francisco', 'CA', '94123', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Cha Cha Cha', 1, 0, 1, '415 386 5758', '', '', '', '', '1801 Height Street', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Charm (le)', 2, 0, 1, '415 546 6128', '', '', '', '', '315 5th Street', '', 'San Francisco', 'CA', '94107', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Cheval (le)', 3, 0, 5, '510 763 8957', '', '', '', '', '1007 Clay St. @11th', '', 'Oakland', 'CA', '94607', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Chez Nous', 2, 0, 1, '415 441 8044', '', '', '', '', '1911 Fillmore St', '', 'San Francisco', 'CA', '94115', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('China Village', 4, 0, 6, '650 593 1831', '', '650 593 1832', '', '', '600 Ralston Ave', '', 'Belmont', 'CA', '94002', 'dim sum', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Coriya - Hot Pot City', 12, 0, 4, '415 387 7888', '', '', '', '', '852 Clement Street (x 10th St)', '', 'San Francisco', 'CA', '', 'buffer, w/ grill and hot pot at the table.', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Crustacean', 3, 0, 6, '415 776 CRAB', '', '415 776 1069', '', '', '1475 Polk St', '', 'San Francisco', 'CA', '94109', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('East Side', 2, 0, 1, '415 885 4000', '', '', '', '', '3154 Fillmore', '', 'San Francisco', 'CA', '94123', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Ebisu', 6, 0, 6, '415 566 1770', '', '', '', 'ebisu.citysearch.com', '1283 9th Ave', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Eliza''s', 4, 0, 1, '415 621 4819', '', '', '', '', '2877 California St.@ Broderick St.', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Eliza''s 2', 4, 0, 1, '415 648 9999', '', '', '', '', '1457 18th St (x Connecticut St)', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Enrico''s', 13, 0, 1, '', '', '', '', '', '504 Broadway', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Esperpento', 1, 0, 1, '415 282 8867', '', '', '', 'Maryam', '3295 22nd St (between Valencia & Mission)', '', 'San Francisco', 'CA', '94110', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Faz', 1, 0, 1, '408 752 8000', '', '408 752 8020', '', '', '1108 North Mathilda Ave', '', 'Sunnyvale', 'CA', '94089', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Firefly', 1, 0, 1, '415 821 7652', '', '', '', '', '4288 24th Street (@Douglas)', '', 'San Francisco', 'CA', '94114', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Folie (la)', 2, 0, 6, '415 776 5577', '', '', '', '', '2316 Polk  (between Union & Green)', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Fondue (la)', 1, 0, 1, '408 867 3332', '', '', '', 'http://www.lafondue.com', '14510 Big Basin Way #3', '', 'Saratoga', 'CA', '95070', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Garibaldis', 1, 0, 5, '415 563 8841', '', '', '', '', '347 Presidio Ave', '', 'San Francisco', 'CA', '94115', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Ichi', 1, 0, 1, '650 948 6767', '', '650 948 5758', '', '', '244 State St', '', 'Los Altos', 'CA', '94022', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Indian Oven', 11, 0, 1, '415 626 1628', '', '415 626 3945', '', '', '233 Fillmore St (Haight/Waller)', '', 'San Francisco', 'CA', '94117', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('International Vegetarian House', 4, 0, 1, '408 292 3798', '', '', '', '', '580 East Santa Clara', '', 'San Jose', 'CA', '95112', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Jitlada', 1, 0, 1, '415 292 9027', '', '', '', '', '1826 Buchanan St(between Sutter & Bush)', '', 'San Francisco', 'CA', '94115', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Kabul', 1, 0, 1, '408 245 4350', '', '', '', '', '833 W. El Camino Real', '', 'Sunnyvale', 'CA', '94087', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Kanzanman', 9, 0, 1, '415 751 9656', '', '', '', '', '1793 Height Street', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Kirala', 6, 0, 1, '510 549 3486', '', '', '', '', '2100 Ward St. @Shattuck', '', 'Berkeley', 'CA', '94705', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Kitaro', 6, 0, 1, '415 386 2777', '', '', '', '', '5850 Geary Blvd (23rd Ave)', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('L''Estrilha', 2, 0, 5, '04 93 62 62 00', '', '', '', '', '11/13 rue de l''Abbaye', '', 'Nice', '', '6300', 'amphone de fruits de mer', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Lemongrass', 1, 0, 1, '', '', '', '', '', '4-871 Kuhio highway', '', 'Kapaa', 'HI', '96746', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Lulu', 1, 0, 1, '415 495 5775', '', '415 495 7810', '', '', '816 Folsom Street', '', 'San Francisco', 'CA', '94107', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Luna Piena', 1, 0, 1, '415 621 2566', '', '', '', '', '558 Castro St.', '', 'San Francisco', 'CA', '94114', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('MacArthur Park', 10, 0, 4, '415 398 5700', '', '', '', '', '607 Front St', '', 'San Francisco', 'CA', '94111', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Masa''s Sushi', 6, 0, 1, '415 941 2117', '', '415 941 5014', '', '', '400 San Antonio Rd', '', 'Mountain View', 'CA', '94040', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('May Flower', 1, 0, 1, '415 387 8338', '', '415 387 1760', '', '', '6255 Geary Blvd (@ 27 ave)', '', 'San Francisco', 'CA', '94121', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Mekong Mekong', 1, 0, 1, '510 848 3148', '', '', '', '', '824 University Ave', '', 'Berkeley', 'CA', '94710', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Mi Ranchito', 8, 0, 1, '415 592 0597', '', '', '', '', '660 Laurel St', '', 'San Carlos', 'CA', '94070', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Minh Tri', 1, 0, 1, '415 566 5335', '', '', '', '', '534 Irving Street', '', 'San Francisco', 'CA', '94122', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Miramar Beach Restaurant', 1, 0, 1, '650 726 9053', '', '650 726 5060', '', '', '131 Mirada RoadPO Box 278', '', 'Half Moon Bay', 'CA', '94019', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Hunan Village', 4, 0, 5, '510 465 4629', '', '', '', '', '3232 Grand Avenue', '', 'Oakland', 'CA', '94610', '', 'Excellent food. Family style.', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Momo''s', 1, 0, 1, '415 227 8660', '', '', '', 'http://www.eatatmomos.com', '760 Second Street(at the Embarcadero)', '', 'San Francisco', 'CA', '94107', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('New Sun Hong Kong Restaurant', 4, 0, 1, '415 956 3338', '', '', '', '', '606 Broadway Street', '', 'San Francisco', 'CA', '94133', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Nirvana', 9, 0, 1, '415 861 2226', '', '415 863 2221', '', 'http://www.citysearch.com/sfo/nirvana', '544 Castro Street', '', 'San Francisco', 'CA', '94114', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Nouveau Trattoria', 2, 0, 5, '415 327 0132', '', '', '', 'http://www.3ad.com/NouveauTrattoria/', '541 Bryant St.(101 South Exit University)', '', 'Palo Alto', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Oak city', 1, 0, 1, '650 321 6882', '', '', '', '', '1029 El Camino Real', '', 'Menlo Pak', 'CA', '94025', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Pasand', 1, 0, 1, '510 549 2559', '', '', '', '', '2286 Shattuck Ave', '', 'Berkeley', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Pastis', 2, 0, 1, '415 391 2555', '', '415 391 1159', '', '', '1015 Battery Street', '', 'San Francisco', 'CA', '94111', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Pho Phu Quoc', 3, 0, 1, '415 661 8869', '', '415 661 8859', '', '', '1816 Irving St', '', 'San Francisco', 'CA', '94122', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Pho To Chau', 3, 0, 1, '650 961 8069', '', '', '', '', '853 Villa St', '', 'Mountain View', 'CA', '94041', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Picaro', 1, 0, 1, '415 431 4089', '', '', '', '', '3120 16th Ave', '', 'San Francisco', 'CA', '94103', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('PJ''s', 1, 0, 1, '415 566 7775', '', '415 566 8088', '', '', '737 Irving St', '', 'San Francisco', 'CA', '94122', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Quoc Te', 1, 0, 1, '408 739 8880', '', '', '', '', '590 Old San Francisco', '', 'Sunnyville', 'CA', '94086', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Rasa Sayang', 1, 0, 1, '510 525 7000', '', '', '', '', '977 San Pablo', '', 'Albany', 'CA', '94706', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('RioCity', 1, 0, 1, '916 442 8226', '', '', '', '', '1110 Front Street', '', 'Old Sacramento', 'CA', '95814', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Rose Pistola', 13, 0, 1, '415 399 0499', '', '415 399 8758', '', '', '532 Columbus Ave', '', 'San Francisco', 'CA', '94133', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Sanraku', 1, 0, 1, '415 771 0803', '', '', '', '', '704 Sutter St', '', 'San Francisco', 'CA', '94109', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Scala''s', 1, 0, 5, '415 395 8555', '', '415 395 8549', '', '', '450 Powell St', '', 'San Francisco', 'CA', '94102', 'moules frites', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Slanted Door', 3, 0, 6, '415 861 8034', '', '861 8329', 'eat@slanteddoor.com', '', '584 Valencia (@ 17th St)', '', 'San Francisco', 'CA', '94110', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Straits Cafe', 1, 0, 1, '415 668 1783', '', '415 668 3901', '', '', '3300 Geary Blvd', '', 'San Francisco', 'CA', '94118', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Sushi Groove', 6, 0, 1, '415 440 1905', '', '415 440 1914', '', '', '1916 Hyde St (@ Union)', '', 'San Francisco', 'CA', '94109', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Sushi Sam''s', 6, 0, 1, '415 344 0888', '', '', '', '', '218 E 3rd Avenue', '', 'San Mateo', 'CA', '94401', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Tadish Grill', 10, 0, 1, '415 391 1849', '', '', '', '', '240 California St', '', 'San Francisco', 'CA', '94111', '', 'Meat Restaurant', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Tavolino', 13, 0, 1, '415 392 1472', '', '', '', '', '401 Colombus Ave', '', 'San Francisco', 'CA', '94133', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Thai Basil', 7, 0, 5, '408 774 9090', '', '', '', 'http://www.thaibasil.com', '101 S. Murphy Ave.', '', 'Sunnyvale', 'CA', '94086', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Thai Stick', 7, 0, 1, '415 928 7730', '', '415 692 9488', '', '', '698 Post Street', '', 'San Francisco', 'CA', '94109', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Thep Phanom', 7, 0, 1, '415 431 2526', '', '', '', '', '400 Waller St (x Filmore)', '', 'San Francisco', 'CA', '94117', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Thirsty Bear', 10, 0, 1, '415 974 0905', '', '415 974 0955', '', '', '661 Howard St', '', 'San Francisco', 'CA', '94105', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Timo''s', 14, 0, 1, '415 647 0558', '', '', '', '', '842 Valencia (19th & 20th St)', '', 'San Francisco', 'CA', '94110', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Tokie''s', 6, 0, 1, '415 570 6609', '', '', '', '', '1058 Shell Blvd', '', 'Foster City', 'CA', '94404', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Tong Kiang', 4, 0, 6, '415 387 8273', '', '', '', '415 752 4440', '5821 Geary Blvd (22nd & 23rd Ave)', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Town house', 2, 0, 1, '510 652 6151', '', '', '', '', '5862 Doyle Street', '', 'Emeryville', 'Ca', '94608', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Truly Mediterranean', 9, 0, 1, '415 751 7482', '', '', '', '', '1724 Haight St', '', 'San Francisco', 'CA', '', 'shawarma & shishkebab', 'Take away place. Excellent food.', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Baking Company', 1, 0, 1, '925 988 9222', '', '', '', '', '1686 Locust St.', '', 'Walnut Creek', 'CA', '94596', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Vie (la)', 3, 0, 6, '415 668 8080', '', '', '', '', '5830 Geary Blvd (x 22 Ave)', '', 'San Francisco', 'CA', '94121', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Yank Sing', 4, 0, 1, '415 781 1111', '', '', '', '', '427 Battery St', '', 'San Francisco', 'CA', '94122', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Yoshi''s', 6, 0, 1, '510 238 9200', '', '510 238 4551', '', 'http://www.yoshis.com', '510 Embarcadero West', '', 'Oakland', 'CA', '94607', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Yuet Lee', 1, 0, 1, '415 982 6020', '', '415 421 8662', '', '', '1300 Stockton Street', '', 'San Francisco', 'CA', '94133', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Zachary''s Chicago Pizza', 10, 0, 1, '510 525 5950', '', '', '', '', '1853 Solnano Ave', '', 'Berkeley', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Zare', 1, 0, 1, '415 291 9145', '', '415 291 9146', '', '', '568 Sacramento St.', '', 'San Francisco', 'CA', '94111', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Zarzuella', 14, 0, 6, '415 346 0800', '', '', '', '', 'corner of Hide & Union', '', 'San Francisco', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Zibibbo', 1, 0, 1, '650 328 6722', '', '', '', 'lapin', '430 Kipling St', '', 'Palo Alto', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Golden Wok', 4, 0, 1, '650 969 8232', '', '', '', '', '895 Villa St', '', 'Mountain View', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Grand Cafe', 2, 0, 1, '415 292 0101', '', '', '', '', '501 Geary (@ Taylor)', '', 'San Francisco', 'CA', '94102', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Hong Kong Flower Lounge', 4, 0, 1, '415 668 8998', '', '', '', '', '5322 Geary Blvd', '', 'San Francisco', 'CA', '94121', 'dim sum', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Houston''s', 10, 0, 1, '415 392 9280', '', '415 392 9285', '', '', '1800 Montgomery St', '', 'San Francisco', 'CA', '94111', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Plouf Seafood Bistro', 2, 0, 1, '415 986 6491', '', '', '', '', '40 Belden Place(between Bush & Pine)', '', 'San Francisco', 'CA', '94104', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Tin''s Tea House', 4, 0, 1, '510 832 7661', '', '', '', 'http://www.themenupage/tins.html', '701 webster (@7)', '', 'Oakland', 'CA', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Boca Chica', 1, 0, 1, '01 43 57 93 13', '', '', '', 'http://www.labocachica.com', '58 rue de Charonne', '', 'Paris XI', '', '', '', '', getdate());
INSERT INTO 
EVOL_Restaurant (Name, CategoryID, Price, RatingID, Phone, Phone2, Fax, email, url, address1, address2, City, State, Zip, Favorite, Comments, creationdate) VALUES ('Cambuse', 2, 0, 1, '04 93 80 82 40', '', '', '', '', '5 cours Saleya', '', 'Nice', '', '6300', '', '', getdate());

GO
