/* Resources for the Blind  */
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

CREATE TABLE [BRL_Resource] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[userID] [int] NULL ,
	[typeID] [int] NOT NULL ,
	[name] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[description] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[address1] [nvarchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[address2] [nvarchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[city] [nvarchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[stateID] [int] NULL ,
	[zip] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[country] [nvarchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[web] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[email] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[contact] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[phone] [nvarchar] (35) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[phone2] [nvarchar] (35) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[tdd] [nvarchar] (35) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Publish] [bit] NULL ,
	[CommentCount] [int] NULL ,	
	[creationdate] [smalldatetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [BRL_ResourceType] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Abbreviation] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Publish] [bit] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [zState] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[name] [nvarchar] (36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[state] [nvarchar] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

ALTER TABLE [BRL_Resource] WITH NOCHECK ADD 
	CONSTRAINT [PK_BRL_Resource] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [zState] WITH NOCHECK ADD 
	CONSTRAINT [PK_zState] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

 CREATE  UNIQUE  CLUSTERED  INDEX [PK_ID] ON [BRL_ResourceType]([ID]) ON [PRIMARY]
GO

ALTER TABLE [BRL_Resource] WITH NOCHECK ADD 
	CONSTRAINT [DF_BRL_Resource_userID] DEFAULT (1) FOR [userID],
	CONSTRAINT [DF_BRL_Resource_Publish] DEFAULT (1) FOR [Publish],
	CONSTRAINT [DF_BRL_Resource_CommentCount] DEFAULT (0) FOR [CommentCount],	
	CONSTRAINT [DF_BRL_Resource_creationdate] DEFAULT (getdate()) FOR [creationdate]
GO

ALTER TABLE [BRL_ResourceType] WITH NOCHECK ADD 
	CONSTRAINT [DF_BRL_ResourceType_Publish] DEFAULT (0) FOR [Publish],
	CONSTRAINT [PK_BRL_ResourceType] PRIMARY KEY  NONCLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [zState] WITH NOCHECK ADD 
	CONSTRAINT [DF_zState_name] DEFAULT ('') FOR [name]
GO

 CREATE  INDEX [SK_BRL_Resource_TypeID] ON [BRL_Resource]([typeID]) ON [PRIMARY]
GO

 CREATE  INDEX [SK_BRL_Resource_TypeIDnStateID] ON [BRL_Resource]([typeID], [stateID]) ON [PRIMARY]
GO



INSERT INTO zState (name, state)
  VALUES ('Alabama', 'AL');
INSERT INTO zState (name, state)
  VALUES ('Alaska', 'AK');
INSERT INTO zState (name, state)
  VALUES ('Arizona', 'AZ');
INSERT INTO zState (name, state)
  VALUES ('Arkansas', 'AR');
INSERT INTO zState (name, state)
  VALUES ('California', 'CA');
INSERT INTO zState (name, state)
  VALUES ('Colorado', 'CO');
INSERT INTO zState (name, state)
  VALUES ('Connecticut', 'CT');
INSERT INTO zState (name, state)
  VALUES ('Delaware', 'DE');
INSERT INTO zState (name, state)
  VALUES ('District of Columbia', 'DC');
INSERT INTO zState (name, state)
  VALUES ('Florida', 'FL');
INSERT INTO zState (name, state)
  VALUES ('Georgia', 'GA');
INSERT INTO zState (name, state)
  VALUES ('Hawaii', 'HI');
INSERT INTO zState (name, state)
  VALUES ('Idaho', 'ID');
INSERT INTO zState (name, state)
  VALUES ('Illinois', 'IL');
INSERT INTO zState (name, state)
  VALUES ('Indiana', 'IN');
INSERT INTO zState (name, state)
  VALUES ('Iowa', 'IA');
INSERT INTO zState (name, state)
  VALUES ('Kansas', 'KS');
INSERT INTO zState (name, state)
  VALUES ('Kentucky', 'KY');
INSERT INTO zState (name, state)
  VALUES ('Louisiana', 'LA');
INSERT INTO zState (name, state)
  VALUES ('Maine', 'ME');
INSERT INTO zState (name, state)
  VALUES ('Maryland', 'MD');
INSERT INTO zState (name, state)
  VALUES ('Massachusetts', 'MA');
INSERT INTO zState (name, state)
  VALUES ('Michigan', 'MI');
INSERT INTO zState (name, state)
  VALUES ('Minnesota', 'MN');
INSERT INTO zState (name, state)
  VALUES ('Mississippi', 'MS');
INSERT INTO zState (name, state)
  VALUES ('Missouri', 'MO');
INSERT INTO zState (name, state)
  VALUES ('Montana', 'MT');
INSERT INTO zState (name, state)
  VALUES ('Nebraska', 'NE');
INSERT INTO zState (name, state)
  VALUES ('Nevada', 'NV');
INSERT INTO zState (name, state)
  VALUES ('New Hampshire', 'NH');
INSERT INTO zState (name, state)
  VALUES ('New Jersey', 'NJ');
INSERT INTO zState (name, state)
  VALUES ('New Mexico', 'NM');
INSERT INTO zState (name, state)
  VALUES ('New York', 'NY');
INSERT INTO zState (name, state)
  VALUES ('North Carolina', 'NC');
INSERT INTO zState (name, state)
  VALUES ('North Dakota', 'ND');
INSERT INTO zState (name, state)
  VALUES ('Ohio', 'OH');
INSERT INTO zState (name, state)
  VALUES ('Oklahoma', 'OK');
INSERT INTO zState (name, state)
  VALUES ('Oregon', 'OR');
INSERT INTO zState (name, state)
  VALUES ('Pennsylvania', 'PA');
INSERT INTO zState (name, state)
  VALUES ('Rhode Island', 'RI');
INSERT INTO zState (name, state)
  VALUES ('South Carolina', 'SC');
INSERT INTO zState (name, state)
  VALUES ('South Dakota', 'SD');
INSERT INTO zState (name, state)
  VALUES ('Tennessee', 'TN');
INSERT INTO zState (name, state)
  VALUES ('Texas', 'TX');
INSERT INTO zState (name, state)
  VALUES ('Utah', 'UT');
INSERT INTO zState (name, state)
  VALUES ('Vermont', 'VT');
INSERT INTO zState (name, state)
  VALUES ('Virginia', 'VA');
INSERT INTO zState (name, state)
  VALUES ('Washington', 'WA');
INSERT INTO zState (name, state)
  VALUES ('West Virginia', 'WV');
INSERT INTO zState (name, state)
  VALUES ('Wisconsin', 'WI');
INSERT INTO zState (name, state)
  VALUES ('Wyoming', 'WY');





INSERT INTO BRL_ResourceType (Name, Abbreviation, Publish)
  VALUES ('Braille and Tapes Libraries', 'Library', 1);
INSERT INTO BRL_ResourceType (Name, Abbreviation, Publish)
  VALUES ('Schools for the Blind', 'School', 1);
INSERT INTO BRL_ResourceType (Name, Abbreviation, Publish)
  VALUES ('Guide Dog Schools', 'Dog School', 1);
INSERT INTO BRL_ResourceType (Name, Abbreviation, Publish)
  VALUES ('Non-Profit Organizations', 'Non-Profit', 1);






INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Fidelco Guide Dog Foundation', '', 'PO Box 142 ', '', 'Bloomfield ', 7, '06002 ', 'US', 'http://www.fidelco.org/', 'lauriebonneau@fidelco.org', 'Laurie Bonneau', '(860) 243-5200
', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Seeing Eye', '', 'Washington Valley Road ', '', 'Morristown ', 31, '07960 ', 'US', 'http://www.seeingeye.org/', '', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Guiding Eyes for the Blind', '', '611 Granite Springs Road ', '', 'Yorktown Heights ', 33, '10598 ', 'US', 'http://www.guiding-eyes.org/', 'info@guiding-eyes.org ', '', '', '914-245-4024 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Guide Dog Foundation for the Blind', '', '371 East Jericho Turnpike ', '', 'Smithtown ', 33, '11787 ', 'US', 'http://www.guidedog.org/', 'directory@guidedog.org', '', '', '516-265-2121 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Freedom Guide Dogs, Inc.', '', '1210 Hardscrabble Road ', '', 'Cassville ', 33, '13318 ', 'US', '', 'freedom@juno.com ', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Guide Dog Users, Inc.', '', '1155 15th Street, Suite 720, NW ', '', 'Washington ', 9, '20005', 'US', '', '', '', '', '202-467-5081 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Canien Visions, Inc.', '', '2305 Lugher Bailey Road ', '', 'Senoia ', 11, '30276 ', 'US', '', '', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Southeastern Guide Dogs Inc.', '', '4210 77th Street East ', '', 'Palmetto ', 10, '34221 ', 'US', 'http://www.guidedogs.org/', 'SEGD@bhip.infi.net ', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Pilot Dogs, Inc.', '', '625 West Town Street ', '', 'Columbus ', 36, '43215 ', 'US', '', '', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Leader Dogs for the Blind', '', '1039 South Rochester Rd', '', 'Rochester ', 23, '48307 ', 'US', 'http://www.leaderdog.org/', 'ldftb@ix.netcom.com ', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Kansas Specialty Dog Service', '', '123 West 8th, Box 216 ', '', 'Washington ', 17, '66968 ', 'US', '', '', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Eye Dog Foundation', '', '512 N Larchmont Blvd ', '', 'Los Angeles ', 5, '90004 ', 'US', '', '', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'International Guiding Eyes', '', '13445 Glenoaks Blvd. ', '', 'Sylmar ', 5, '91342 ', 'US', '', '', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Guide Dogs of the Desert', '', 'PO Box 1692 ', '', 'Palm Springs ', 5, '92263 ', 'US', '', 'gddesert@aol.com ', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Guide Dogs for the Blind', '', '350 Los Ranchitos Road ', '', 'San Rafael ', 5, '94915 ', 'US', 'http://www.guidedogs.com/', '', '', '', '415-499-4000 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Eye of the Pacific Guide Dogs & Mobility Services', '', '747 Amana Street #407 ', '', 'Honolulu ', 12, '96814 ', 'US', '', '', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Guide Dogs for the Blind', '', '32901 S.E. Kelso Road ', '', 'Boring ', 38, '97009 ', 'US', 'http://www.guidedogs.com/', '', '', '', '503-668-2100 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Alabama Regional Library for the Blind and Physically Handicapped', '', '6030 Monticello Drive', '', 'Montgomery', 1, '36130', 'US', 'http://www.apls.state.al.us/services/services.html#BPH ', 'fzaleski@apls.state.al.us', 'Librarian: Mrs. Fara L. Zaleski', '(334) 213-3906', '', '(334) 213-3900', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Public Library of Anniston and Calhoun County', '', 'Library for the Blind and Handicapped', 'P.O. Box 308', 'Anniston', 1, '36202 ', 'US', '', 'library@bigdog.ahs.anniston.k12.al.us ', 'Librarian: Mrs. Deenie M. Culver', '(256) 237-8501', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Houston-Love Memorial Library', '', 'Department for the Blind and Physically Handicapped', 'P.O. Box 1369', 'Dothan', 1, '36202 ', 'US', '', 'mmerow@yahoo.com ', 'Librarian: Ms. Myrtis Merrow', '(334) 793-9767', '', '(334) 793-9767', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Huntsville Subregional Library for the Blind and Physically Handicapped', '', 'P.O. Box 443', '', 'Huntsville', 1, '35804 ', 'US', '', 'mmerow@yahoo.com ', 'Librarian: Mrs. Joyce L. Smith', '(256) 532-5980 ', '', ' (256) 532-5968', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Alabama Institute for Deaf and Blind', '', 'Library for the Blind and Physically Handicapped', '705 South Street, P.O. Box 698', 'Talladega', 1, '35161', 'US', '', 'tlacy@aidb.state.al.us ', 'Librarian: Mrs. Teresa Lacy', '(205) 761-3287 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Tuscaloosa Public Library', '', 'Tuscaloosa Library for the Blind and Physically Handicapped', '1801 River Road', 'Tuscaloosa', 1, '35401 ', 'US', '', '', 'Librarian: Mrs. Barbara B. Jordan', '(205) 345-3994', '', ' (205) 345-3994', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Guide Dogs of America', '', '13445 Glenoaks Blvd.
', ' ', 'Sylmar', 5, '91342 
', 'US', 'http://www.guidedogsofamerica.org/', 'gdaguidedogs@earthlink.net
  ', '  ', '(818) 362-5834', ' ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Talking Book Center', '', 'Alaska State Library', '344 West Third Avenue, Suite 125', 'Anchorage', 2, '99501 ', 'US', 'http://www.educ.state.ak.us/lam/library/dev/tbc.html', 'patm@muskox.alaska.edu ', 'Librarian: Ms. Patricia Meek', '(907) 269-6575', '', ' (907) 269-6575', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Library for the Blind and Physically Handicapped', '', 'One Capitol Mall', '', 'Little Rock', 4, '72201-1081 ', 'US', 'http://www.asl.lib.ar.us/ASL_LBPH.htm', 'jhall@asl.lib.ar.us', 'Librarian: Mr. John J.D. Hall', '(501) 682-1155 ', '1-866-660-0885 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Fort Smith Public Library for the Blind and Handicapped', '', '61 South Eighth Street', '', 'Fort Smith', 4, '72901 ', 'US', 'http://www.fspl.lib.ar.us/publbph.html', 'khamlin@fspl.lib.ar.us', 'Librarian: Ms. Kelly Hamlin', '(501) 783-0229', '', ' (501) 783-5129', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'CLOC Regional Library', '', 'Library for the Blind and Handicapped, Southwest', 'P.O. Box 668', 'Magnolia', 4, '71753 ', 'US', '', '', 'Librarian: Ms. Susan Walker', '(870) 234-1991', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Braille Institute Library Services', '', '741 North Vermont Avenue', '', 'Los Angeles', 5, '90029-3594 ', 'US', 'http://www.braillelibrary.org', 'bils@braillelibrary.org', 'Librarian: Dr. Henry C. Chang', '(323) 660-3880', '1-800-808-2555', '(323) 660-3880', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'California State Library', '', 'Braille and Talking Book Library', 'P.O. Box 942837', 'Sacramento', 5, '94237-0001 ', 'US', 'http://library.ca.gov/pubser/pubser05.html ', 'btbl@library.ca.gov', 'Librarian: Ms. Donine Hedrick', '(916) 654-0640', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Ted Wills Community Center', '', 'Talking Book Library for the Blind', '770 North San Pablo', 'Fresno', 5, '93728-3640 ', 'US', '', 'djanzen@sjvls.lib.ca.us ', 'Librarian: Ms. Wendy Eisenberg', '(209) 488-3217', '', '(209) 488-1642', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'San Francisco Public Library Civic Center ', '', 'Library for the Blind and Print Disabled', '100 Larkin Street', 'San Francisco', 5, '94102 ', 'US', '', 'lbphmgr@sfpl.lib.ca.us ', 'Librarian: Mr. Martin Magid', '(415) 557-4253', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Colorado Talking Book Library', '', '180 Sheridan Boulevard', '', 'Denver', 6, '80226-8097 ', 'US', 'http://www.cde.state.co.us/cdelib/ctbl.htm', 'ctbl@csn.net', 'Librarian: Ms. Barbara Goral', '(303) 727-9277', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Connecticut State Library', '', 'Library for the Blind and Physically Handicapped', '198 West Street', 'Rocky Hill', 7, '06067 ', 'US', 'http://www.cslnet.ctstateu.edu/lbph.htm', 'ctaylor@csunet.ctstateu.edu ', 'Librarian: Ms. Carol A. Taylor', '(860) 566-2151', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Delaware Division of Libraries', '', 'Library for the Blind and Physically Handicapped', '43 South DuPont Highway', 'Dover', 8, '19901 ', 'US', '', 'blandon@lib.de.us ', 'Librarian: Ms. Beth Landon', '(302) 739-4748', '', '(302) 739-4748', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'District of Columbia Regional Library for the Blind and Physically Handicapped', '', '901 G Street NW, Room 215', '', 'Washington', 9, '20001 ', 'US', 'http://dclibrary.org/lbph/ ', '104416.2202@compserv.com', 'Librarian: Ms. Grace J. Lyons', '(202) 727-2142', '', ' (202) 727-2145', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Florida Bureau of Braille and Talking Book Library Services', '', '420 Platt Street', '', 'Daytona Beach', 10, '32114-2804', 'US', 'http://www.state.fl.us/dbs/lswel.htm ', 'weberd@mail.firn.edu', 'Librarian: Mr. Donald John Weber', '(904) 239-6000', '', ' 800-226-6079', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'South Manatee Branch Library', '', 'Talking Book Service', '6081 26th Street West', 'Bradenton', 10, '34207 ', 'US', '', '', 'Librarian: Ms. Candace Conklin', '(941) 742-5914', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Brevard County Library System', '', 'Talking Books Library', '308 Forrest Avenue', 'Cocoa', 10, '32922-7781 ', 'US', '', 'kbriley@manatee.brev.lib.fl.us ', 'Librarian: Ms. Kay Briley', '(407) 633-1810 ', '', '(407) 633-1811', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Broward County Talking Book Library', '', '100 South Andrews Avenue', '', 'Ft. Lauderdale', 10, '33301 ', 'US', '', 'jblock@mail.bcl.lib.fl.us ', 'Librarian: Ms. Joann Block', '(954) 357-7555 ', '', '(954) 357-7413', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Jacksonville Public Libraries', '', 'Talking Book Library', '1755 Edgewood Avenue West, Suite 1', 'Jacksonville', 10, '32208-7206 ', 'US', '', 'laurieb@coj.net ', 'Librarian: Ms. Laurie Baumgardner', '(904) 765-5588', '', '(904) 768-7822', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Pinellas Talking Book Library for the Blind and Physically Handicapped', '', '12345 Starkey Road, Suite L', '', 'Largo', 10, '33773-2629 ', 'US', 'http://snoopy.tblc.lib.fl.us/ptbl/', 'lapoinb@snoopy.tblc.lib.fl.us', 'Librarian: Ms. Barbara LaPointe, acting', '(727) 538-9567', '', ' (727) 538-8949', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Talking Book Library of Dade and Monroe Counties', '', 'Miami-Dade Public Library System', '150 NE 79th Street', 'Miami', 10, '33138-4890 ', 'US', '', 'moyerb@mail.seflin.org ', 'Librarian: Ms. Barbara L. Moyer', '(305) 751-8687', '', ' (305) 758-6599', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Lee County Talking Books Library', '', '13240 North Cleveland Avenue, #5-6', '', 'North Ft. Myers', 10, '33903-4855 ', 'US', '', 'abradley@bocc.co.lee.fl.us ', 'Librarian: Ms. Ann Bradley', '(941) 995-2665 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Orange County Library System', '', 'Audio-Visual Department  Talking Book Section', '101 East Central Boulevard', 'Orlando', 10, '32801 ', 'US', '', 'abradley@bocc.co.lee.fl.us ', 'Librarian: Ms. Sally Fry', '407-425-4694 x 421', '', ' (407) 425-5668', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'West Florida Regional Library', '', 'Subregional Talking Book Library', '200 West Gregory Street', 'Pensacola', 10, '32501 ', 'US', '', 'wfrl04@pcola.gulf.net ', 'Librarian: Ms. Blanche C. Hooper', '(850) 435-1760', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Palm Beach County Library Annex', '', 'Talking Books', '7950 Central Industrial Drive, Suite 104', 'Riviera Beach', 10, '33404-9947 ', 'US', 'http://www.seflin.org/pbcls/talking.html', 'wfrl04@pcola.gulf.net ', 'Librarian: Ms. Pat Mistretta', '(561) 845-4600', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Hillsborough County Talking Book Library', '', 'Tampa-Hillsborough County Public Library', '900 Ashley Drive, North', 'Tampa', 10, '33602-3704 ', 'US', 'http://scfn.thpl.lib.fl.us/thpl/libraries/special/talking_book/talking_book_library.html', 'tbluser@scfn.thpl.lib.fl.us', 'Librarian: Mr. Kurt Jasielonis', '(813) 273-3609 ', '', ' (813) 273-3610', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Georgia Library for the Blind and Physically Handicapped', '', '1150 Murphy Avenue SW', '', 'Atlanta', 11, '30310 ', 'US', 'http://lbph.public.lib.ga.us/', '1koldenhoven@dtae.org ', 'Librarian: Ms. Linda Koldenhoven', '(404) 756-4619', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Albany Library for the Blind and Handicapped Dougherty County Public Library', '', '300 Pine Avenue', '', 'Albany', 11, '31701 ', 'US', '', '', 'Librarian: Mrs. Kathryn R. Sinquefield', '(912) 430-3220', '', ' (912) 430-1911', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Athens Talking Book Center', '', 'Athens-Clarke County Regional Library', '2025 Baxter Street', 'Athens', 11, '30606', 'US', '', 'burnsp@mail.clarke.public.lib.ga.us ', 'Librarian: Ms. Paige Burns', '(706) 613-3655', '', ' (706) 613-3655', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Augusta-Richmond County Public Library', '', 'Talking Book Center', '425 Ninth Street', 'Augusta', 11, '30901 ', 'US', 'http://www.scescape.net/~ecgrl/lbph.htm', 'swintg@mail.richmond.public.lib.ga.us ', 'Librarian: Mr. Gary Swint', '(706) 821-2625', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Bainbridge Subregional Library for the Blind and Physically Handicapped', '', 'Southwest Georgia Regional Library ', '301 South Monroe Street', 'Bainbridge', 11, '31717 ', 'US', 'http://www.decatur.public.lib.ga.us/local/lbph/lbph1.htm', 'lbph@mail.decatur.public.lib.ga.us ', 'Librarian: Ms. Kathy Hutchins', '(912) 248-2680', '', ' (912) 248-2665', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Three Rivers Regional Library System', '', 'Talking Book Center', '208 Gloucester Street', 'Brunswick', 11, '31523-0901 ', 'US', '', '', 'Librarian: Mrs. Betty Ransom', '(912) 267-1212 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'W. C. Bradley Memorial Library', '', 'Subregional Library for the Blind and Physically Handicapped', '1120 Bradley Drive', 'Columbus', 11, '31906-2800 ', 'US', '', 'slpbh@mail.muscogee.public.lib.ga.us ', 'Librarian: Ms. Dorothy Bowen', '706-649-0780 x 22 ', '', ' (706) 649-0974', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Oconee Regional Library', '', 'Library for the Blind and Physically Handicapped', '801 Bellevue Avenue   P.O. Box 100', 'Dublin', 11, '31040 ', 'US', 'http://www.laurens.public.lib.ga.us/lbph.htm', '', 'Librarian: Ms. Susan S. Williams', '(912) 275-5382', '', ' (912) 275-3821', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Hall County Library', '', 'Library for the Blind and Physically Handicapped', '127 North Main Street', 'Gainesville', 11, '30505 ', 'US', ' ', '', 'Librarian: Ms. Sandra Whitmer', '770-532-3311 x 136', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'LaFayette Subregional Library for the Blind and Physically Handicapped', '', '305 South Duke Street', '', 'LaFayette', 11, '30728 ', 'US', 'http://www.walker.public.lib.ga.us/tbc/', 'stubblec@mail.walker.public.lib.ga.us ', 'Librarian: Mr. Charles Stubblefield', '(706) 638-2992', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Macon Subregional Library for the Blind and Physically Handicapped', '', 'Washington Memorial Library', '1180 Washington Avenue', 'Macon', 11, '31201-1790 ', 'US', '', 'sherrilr@mail.bibb.public.lib.ga.us ', 'Librarian: Ms. Rebecca M. Sherrill', '(912) 744-0877', '', ' (912) 744-0877', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Rome Subregional Library for the Blind and Physically Handicapped', '', 'Sara Hightower Regional Library', '205 Riverside Parkway NE', 'Rome', 11, '30161-2911 ', 'US', 'http://www.floyd.public.lib.ga.us/tbc.htm ', 'dianam@mail.floyd.public.lib.ga.us ', 'Librarian: Ms. Diana Mills', '(706) 236-4618', '', '(706) 236-4618', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'CEL Regional Library, Thunderbolt Branch', '', 'Subregional Library for the Blind and Physically Handicapped', '2708 Mechanics', 'Savannah', 11, '31404 ', 'US', '', '', 'Librarian: Ms. Linda Stokes', '(912) 354-5864', '', '(912) 652-3635', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'American Association of People with Disabilities', '', '1819 H St NW #330 ', '', 'Washington ', 9, '20006 ', 'US', 'http://aapd-dc.org/', 'aapd@aol.com ', '', '800-840-8844 ', '202-457-0046 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'American Council of the Blind', '', '1155 15th Street NW, Suite 720 ', '', 'Washington ', 9, '20005 ', 'US', 'http://www.acb.org/', '', '', '202-467-5081 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'American Foundation ft Blind', '', '11 Penn Plaza Suite 300 ', '', 'New York ', 33, '10001 ', 'US', 'http://www.afb.org/afb', 'afbinfo@afb.org ', '', '212-502-7642 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'American Printing House for the Blind', '', '1839 Frankfort Ave', '', 'Louisville ', 18, '40206 ', 'US', 'http://www.aph.org/', 'aph@iglou.com ', '', '800-223-1839 ', '502-895-2405 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'Association of Education and Rehabilitation for VI (AER)', '', '4600 Duke Street #430; PO Box 22397 ', '', 'Alexandria ', 47, '22397 ', 'US', 'http://aerbvi.org/', '', '', '703-823-9690 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'Associations of American Publishers', '', '71 Fifth Avenue ', '', 'New York ', 33, '10003 ', 'US', 'http://www.publishers.org/', '', '', '212-255-0200 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'Blinded Vetrans Association', '', '477 H Street, N.W. ', '', 'Washington ', 9, '20001 ', 'US', 'http://www.bva.org/', 'bva@bva.org ', '', '202-371-8880 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'Braille Authority of North America', '', '919 Walnut Street ', '', 'Philadelphia ', 39, '19107 ', 'US', 'http://www.brailleauthority.org/', 'dfdodz@asb.org ', '', '215-627-0600 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'Council for Exceptional Children', '', '1920 Association Drive ', '', 'Reston ', 47, '22091 ', 'US', 'http://www.cec.sped.org/', '', '', '888-CEC-SPED ', '703-620-3660 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'Foundation Fighting Blindness', '', 'Executive Plaza I, Suite 800, 11350 McCormick Road ', '', 'Hunt Valley ', 21, '21031 ', 'US', 'http://blindness.org/', '', '', '888-394-3937 ', '410-785-1414 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'Foundation for Blind Children', '', '1235 E. Harmont Dr. ', '', 'Phoenix ', 3, '85020 ', 'US', 'http://www.the-fbc.org/', 'mnelson@the-fbc.org ', '', '602-331-1470 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'Jewish Braille Institute', '', '110 East 30th Street ', '', 'New York', 33, '10016 ', 'US', 'http://www.jewishbraille.org/', 'admin@jewishbraille.org ', '', '800-433-1531 ', '212-889-2525 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'Lions Club International', '', '300 W 22nd Street ', '', 'Oak Brook ', 14, '60523 ', 'US', 'http://www10.lionsclub.org/lion', '', '', '630-571-5477 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'National Association for Parents of Children w Visual Impairments', '', 'PO Box 317 ', '', 'Watertown ', 22, '02272 ', 'US', 'http://www.spedex.com/napvi', '', '', '800-562-6265 ', '617-972-7441 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'National Association for the Visually Handicapped', '', '22 West 21st Street ', '', 'New York ', 33, '10010 ', 'US', 'http://www.navh.org/', 'staff@navh.org ', '', 'staff@navh.org ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'National Braille Press', '', '88 St. Stephen Street ', '', 'Boston ', 22, '02115 ', 'US', 'http://www.nbp.org/', 'ecurran@nbp.org ', '', '800-548-7343 ', '617-266-6160 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'National Federation of the Blind', '', '1800 Johnson St. ', '', 'Baltimore ', 21, '21230 ', 'US', 'http://www.nfb.org/', 'nfb@iamdigex.net ', '', '410-659-9314 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'National Library Service for the Blind & Physically Handicapped', '', 'Library of Congress, 1291 Taylor Street NW ', '', 'Washington ', 9, '20542 ', 'US', 'http://lcweb.loc.gov/nls', 'nls@loc.gov ', '', '202-707-5100 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'Prevent Blindness America', '', '500 East Remington Rd ', '', 'Schaumburg ', 14, '60173 ', 'US', 'http://www.prevent-blindness.org/', 'info@prevent-blindness.org ', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'Prevent Blindness in Premature Babies', '', 'PO Box 44792 ', '', 'Madison ', 50, '53744 ', 'US', 'http://www.brailleplanet.org/pbpb.html', 'prevent@execpc.com ', '', '608-845-6500 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (4, 'Recording for the Blind & Dyslexic', '', '20 Roszel Rd. ', '', 'Princeton ', 31, '08540 ', 'US', 'http://www.rfbd.org/', '', '', '800-221-4792 ', '609-452-0606 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Instituto Loaiza Cordero para Nimos Ciegos', '', 'Fernandez Juncos 1312 ', '', 'Santurce ', -51, '00919 ', 'US', '', '', '', '809-724-0893 ', '809-722-2498 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Boston Center for Blind Children Arbor Way School', '', '147 South Huntington Ave ', '', 'Boston ', 22, '02130 ', 'US', '', '', '', '617-232-1710 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Perkins School for the Blind', '', '175 North Beacon Street ', '', 'Watertown ', 22, '02172 ', 'US', '', '', '', '617-924-3434 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Walter E. Fernald State School', '', '9107 Trapella Road, P.O. Box 9108 ', '', 'Belmont ', 22, '02178 ', 'US', '', '', '617-894-3600 ', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Oak Hill School Connecticut Institute for the Blind', '', '120 Holcomb Street ', '', 'Hartford ', 7, '06112 ', 'US', 'http://www.ciboakhill.org/', '', '', '203-242-2274 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Matheny School', '', 'Main Street ', '', 'Peapack ', 31, '07977 ', 'US', '', '', '', '908-234-0011 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'St. Joseph''s School for the Blind', '', '235 Balwin Ave
', '', 'Jersey City', 31, '06112', 'US', '', '', '', '203-242-2274', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Lavelle School for the Blind', '', 'East 221st Street and Paulding Ave ', '', 'Bronx ', 33, '10469 ', 'US', '', '', '', '718-882-1212 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'New York Institute for Special Education', '', '999 Pelham Parkway ', '', 'Bronx ', 33, '10469 ', 'US', 'http://www.nyise.org/', 'nyise@aol.com', '', '718-519-7000 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'New York State School for the Blind, Resource Center', '', 'Richmond Ave ', '', 'Batavia ', 33, '14020 ', 'US', '', '', '', '716-343-5384 ', '716-343-8100 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Western Pennsylvania School for Blind Children', '', '201 North Bellefield Street ', '', 'Pittsburgh ', 39, '15213 ', 'US', 'http://www.wpsbc.org/', '', '', '412-621-0100 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'St. Lucy Day School', '', '130 Hampden Road ', '', 'Upper Darby ', 39, '19082 ', 'US', '', '', '', '610-352-4582 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Overbrook School for the Blind', '', '6333 Malvern Ave ', '', 'Philadephia ', 39, '19151 ', 'US', 'http://www.obs.org/', 'denise@obs.org', '', '215-877-0313 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Royer-Greaves School for the Blind', '', '118 South Valley Road ', '', 'Paoli ', 39, '19301 ', 'US', '', '', '', '610-644-1810 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Deaf-Blind Program The National Academy, Gallaudet University', '', 'Peet Hall, 3rd fl. 800 Florida Ave, N.E. ', '', 'Washington ', 9, '20002 ', 'US', '', '', '', '202-651-5096 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Virginia School for the Deaf and the Blind', '', 'PO Box 2069 ', '', 'Staunton ', 47, '2069 ', 'US', '', '', '', '703-332-9046 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Betsy Burnham; Maryland School for the Blind', '', '3501 Taylor Ave ', '', 'Baltimore ', 21, '21236 ', 'US', '', '', '', '410-444-5000 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Virginia School for the Deaf and Blind at Hampton', '', '700 Shell Road ', '', 'Hampton ', 47, '23661 ', 'US', '', '', '', '804-247-2075 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'West Virginia Schools for the Deaf and the Blind', '', '301 East Main Street ', '', 'Romney ', 49, '26757 ', 'US', '', '', '', '304-822-4800 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Governor Morehead School', '', '301 Ashe Ave ', '', 'Raleigh ', 34, '27606 ', 'US', '', '', '', '919-733-6381 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'South Carolina School for the Deaf and the Blind', '', '355 Cedar Spring Road', '', 'Spartanburg ', 41, '29302 ', 'US', 'http://www.scsdb.k12.sc.us 
', 'sgoolsby@scsdb.k12.sc.us', 'Sharon Goolsby', '864-577-7508', '864- 577-7506', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Georgia Academy for the Blind', '', '2895 Vineville Ave ', '', 'Macon ', 11, '31204 ', 'US', '', '', '', '912-751-6083 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Florida School for the Deaf and Blind', '', '207 North San Marco Ave ', '', 'St. Augustine ', 10, '32084 ', 'US', 'http://www.fsdb.k12.fl.us/', 'hesson_d@popmail.firn.edu', '', '904-823-4000 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Alabama School for the Blind', '', '803 East South Street, P.O. Box 698 ', '', 'Talladega ', 1, '35160 ', 'US', 'http://www.aidb.org/', '', '', '205-761-3259 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Southwest Alabama School for the Deaf and Blind', '', '8901 Airport Blvd. ', '', 'Mobile ', 1, '36608 ', 'US', '', '', '', '205-633-0241 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Tennessee School for the Blind', '', '115 Stewards Ferry Pike ', '', 'Nashville ', 43, '37214 ', 'US', 'http://volweb.utk.edu/school/tsb/', '', '', '615-231-7300 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Mississippi School for the Blind', '', '1252 Eastover Drive ', '', 'Jackson ', 25, '39211 ', 'US', '', '', '', '601-984-8200 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Kentucky School for the Blind', '', '1867 Frankfort Ave, P.O. Box 6005 ', '', 'Louisville ', 18, '40206 ', 'US', '', '', '', '502-897-1583 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Ohio State School for the Blind', '', '5220 North High Street ', '', 'Columbus ', 36, '43214 ', 'US', '', 'lmazzoli@ossb.ode.state.oh.us', 'William Bolsen', '614-752-1152 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Indiana Educational Resource Center, Indiana School for the Blind', '', '7725 North College Ave ', '', 'Indianapolis ', 15, '46240 ', 'US', 'http://isb.butler.edu/', '', '', '317-253-1481 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Michigan School for the Deaf and Blind', '', 'West Court & Miller ', '', 'Flint ', 23, '48503 ', 'US', '', '', '', '800-622-6730 ', '810-257-1420 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Iowa Braille and Sight Saving School', '', '1002 G Avenue ', '', 'Vinton ', 16, '52349 ', 'US', '', '', '', '319-472-5221 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Wisconsin School for the Visually Handicapped', '', '1700 West State Street ', '', 'Janesville ', 50, '53546 ', 'US', '', '', '', '608-758-6100 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Minnesota State Academy for the Blind', '', 'Highway 298, P.O. Box 68 ', '', 'Faribault ', 24, '55021 ', 'US', '', '', '', '507-332-3226 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'South Dakota School for the Visually Handicapped', '', '423 S.E. 17th Avenue ', '', 'Aberdeen ', 42, '57401 ', 'US', 'http://www.sdsbvi.sdbor.edu/', 'KaiserM@sdsvh.northern.edu', '', '605-622-2580 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'North Dakota School for the Blind', '', '500 Stanford Road ', '', 'Grand Forks ', 35, '58203 ', 'US', '', 'sowokino@sendit.sendit.nodak.edu', 'Janice Sowokinos', '701-795-2700 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Montana School for the Deaf and Blind', '', '3911 Central Ave ', '', 'Great Falls ', 27, '59405 ', 'US', '', '', '', '406-771-6000 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Hadley School for the Blind', '', '700 Elm Street ', '', 'Winnetka ', 14, '60093 ', 'US', 'http://www.hadley-school.org/', 'info@hadley-school.org', '', '847-446-8111 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Philip J. Rock Center and School', '', '818 DuPage Boulevard ', '', 'Glen Ellyn ', 14, '60137 ', 'US', '', '', '', '708-790-2474 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Philip H. Cohen Institute for the Visually Handicapped', '', '5200 South Hyde Park Blvd ', '', 'Chicago ', 14, '60615 ', 'US', '', '', 'Leonore Schechter', '312-643-9857 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Illinois School for the Visually Impaired', '', '658 East State Street ', '', 'Jacksonville ', 14, '62650 ', 'US', 'http://www.state.il.us/agency/dhs/isvi.htm', '', '', '217-479-4400 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Hope School', '', '50 Hazel Lane ', '', 'Springfield ', 14, '62703 ', 'US', '', '', '', '217-786-3350 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Missouri School for the Blind', '', '3815 Magnolia Ave ', '', 'St. Louis ', 26, '63110 ', 'US', 'http://www.msb.k12.mo.us/', '', '', '314-776-4320 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Kansas Instr. Resource Ctr. for the Blind & Visually Impaired', '', '1100 State Ave ', '', 'Kansas City ', 17, '66102 ', 'US', '', 'KIRC@OZ.sunflower.org', 'Jacqueline Denk', '913-281-3308 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Nebraska School for the Visually Handicapped', '', '824 Tenth Ave., P.O. Box 129 ', '', 'Nebraska City ', 28, '68410 ', 'US', '', '', '', '402-873-5513 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Louisiana School for the Visually Impaired', '', '1120 Government Street ', '', 'Baton Rouge ', 19, '70802 ', 'US', '', '', 'Warren Figuieredo', '504-342-8694 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Educational Services for the Visually Impaired, AR School f/t Blind', '', '2600 West Markham, P.O. Box 668 ', '', 'Little Rock ', 4, '72203 ', 'US', '', '', '', '501-296-1810 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Parkview School (Oklahoma School for the Blind)', '', '3300 Gibson Street ', '', 'Muskogee ', 37, '74403 ', 'US', 'http://www.osb.k12.ok.us/index.html/', 'osb@azalea.net', '', '918-682-6641 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Texas School for the Blind and Visually Handicapped', '', '1100 West 45th Street ', '', 'Austin ', 44, '78756 ', 'US', 'http://www.tsbvi.edu/', '', '', '512-454-8631 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Colorado School for the Deaf and Blind', '', '33 North Institute Street ', '', 'Colorado Springs ', 6, '80903 ', 'US', 'http://www.csdb.org/', '', '', '719-578-2102 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Idaho School for the Deaf and Blind', '', '1450 Main Street ', '', 'Gooding ', 13, '83330 ', 'US', '', '', '', '208-934-4457 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Ed Resource Center, Utah Schools for the Deaf & the Blind', '', '742 Harrison Boulevard ', '', 'Ogden ', 45, '84404 ', 'US', 'http://www.usdb.k12.ut.us/home/home.htm', '', 'Lorri Quigley', '801-629-4810 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Arizona State School for the Deaf and Blind', '', 'PO Box 85000 ', '', 'Tuscon ', 3, '85754 ', 'US', 'http://www.asdb.org
', '', '', '602-770-3700 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'New Mexico School for the Visually Handicapped', '', '1900 North White Sands Blvd ', '', 'Alamogordo ', 32, '88310 ', 'US', '', '', 'Patricia Harmon', '505-437-3505 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Frances Blend School', '', '5210 Clinton Street ', '', 'Los Angeles ', 5, '90004', 'US', '', '', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Braille Institute of America', '', '741 North Vermont Avenue ', '', 'Los Angeles ', 5, '90029-3594', 'US', 'http://www.brailleinstitute.org', 'info@brailleinstitute.org', 'Carol Morrison', '(323) 663-1111', '1-800-BRAILLE (272-4553)', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'California School for the Blind', '', '500 Walnut Ave ', '', 'Fremont ', 5, '94536 ', 'US', '', '', '', '510-794-3800 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Oregon School for the Blind', '', '700 Church Street S.E. ', '', 'Salem ', 38, '97301 ', 'US', '97301 ', '', '', '503-378-3820 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Washington State School for the Blind', '', '2214 East 13th Street ', '', 'Vancouver ', 48, '98661 ', 'US', 'http://www.wssb.org/', 'braille@wssb.org', 'Collen Lines', '360-696-6321 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Alaska Special Education Service Agency', '', '2217 East Tabor Road, Suite 1 ', '', 'Anchorage ', 2, '99507 ', 'US', '', '', '', '907-562-7372 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'South Georgia Regional Library', '', 'Subregional Library for the Blind and Physically Handicapped', '300 Woodrow Wilson Drive', 'Valdosta', 11, '31602-2592', 'US', '', 'petersb@mail.lowndes.public.lib.ga.us', 'Librarian: Ms. Beverly Speck Peters', '(912) 333-7658', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Hawaii State Library', '', 'Library for the Blind and Physically Handicapped', '402 Kapahulu Avenue', 'Honolulu', 12, '96815', 'US', 'http://www.hcc.hawaii.edu/hspls/oahu/lbph.html', 'olbcirc@lib.state.hi.us', 'Librarian: Ms. Fusako Miyashiro', '(808) 733-8444', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Guam Public Library for the Blind and Physically Handicapped', '', 'Nieves M. Flores Memorial Library', '254 Martyr Street', 'Agana', -51, '96910', 'US', '', 'csctsmth@kuentos.guam.net', 'Librarian: Ms. Christine Scott-Smith', '(671) 475-4753 ', '(671) 475-4754', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Idaho State Talking Book Library', '', '325 West State Street', '', 'Boise', 13, '83702', 'US', 'http://www.lili.org/isl/ls3.htm', 'atesti@isl.state.id.us', 'Librarian: Ms. Andrea Testi', '(208) 334-2117', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Illinois Regional Library for the Blind and Physically Handicapped', '', 'Illinois State Library', '300 South Second Street', 'Springfield', 14, '62701', 'US', 'sruda@library.sos.state.il.us', 'sruda@library.sos.state.il.us', 'Librarian: Ms. Sharon Ruda', '(217) 782-9435', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Southern Illinois Talking Book Center', '', 'Shawnee Library System', '607 Greenbriar Road', 'Carterville', 14, '62918-1600', 'US', '', 'dbrawley@shawls.lib.il.us', 'Librarian: Ms. Marcia Sorensen', '(618) 985-8375', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Harold Washington Library Center', '', 'Talking Book Center', '400 South State Street, Room 5N7', 'Chicago', 14, '60605', 'US', '', 'mgrady@chipublib.org', 'Librarian: Ms. Mamie Grady', '(312) 747-4001', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Talking Book Center of Northwest Illinois', '', 'P.O. Box 125', '', 'Coal Valley', 14, '61240', 'US', '', 'kodean@libby.rbls.lib.il.us', 'Librarian: Ms. Karen Odean', '(309) 799-3137', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Voices of Vision', '', 'Talking Book Center DuPage Library System', '127 South First Street', 'Geneva', 14, '60134', 'US', 'http://dupagels.lib.il.us/pages/voices.html', 'mharnden@dupagels.lib.il.us', 'Librarian: Ms. Mary Beth Harnden', '(630) 208-0398', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Mid-Illinois Talking Book Center', '', 'Pekin Office Alliance Library System', '845 Brenkman Drive', 'Pekin', 14, '61554', 'US', 'http://www.alliancelibrarysystem.com/mitbc ', 'mitbc@darkstar.rsa.lib.il.us', '', '(309) 353-4110', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Mid-Illinois Talking Book Center', '', 'Quincy Office Alliance Library System', '515 York', 'Quincy', 14, '62301', 'US', 'http://www.alliancelibrarysystem.com/mitbc ', 'lmuselman@alliancelibrarysystem.com ', '', '(217) 224-6619', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Indiana State Library', '', 'Special Services Division', '140 North Senate Avenue', 'Indianapolis', 15, '46204', 'US', 'http://www.statelib.lib.in.us/www//lbph/lbphO.html', 'lshanahan@statelib.lib.in.us', 'Librarian: Ms. Lissa Shanahan', '(317) 232-3684', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Bartholomew County Public Library', '', '536 Fifth Street', '', 'Columbus', 15, '47201', 'US', '', 'talkingbooks@barth.lib.in.us', 'Librarian: Ms. Sharon Thompson', '(812) 379-1277', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Blind and Physically Handicapped Services', '', 'Elkhart Public Library', '300 South Second', 'Elkhart', 15, '46516-3184', 'US', '', '', 'Librarian: Mrs. Pat Ciancio', '(219) 522-2665', 'ext. 52', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Talking Books Service', '', 'Evansville-Vanderburgh County Public Library', '22 South East Fifth Street', 'Evansville', 15, '47708-1694', 'US', '', 'tlknbks@hotmail.com', 'Librarian: Mrs. Barbara Shanks', '(812) 428-8235', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Northwest Indiana Subregional Library for the Blind ', '', 'Lake County Public Library', '1919 West 81st Avenue', 'Merrillville', 15, '46410-5382', 'US', '', 'tbooks@lakeco.lib.in.us', 'Librarian: Ms. Renee Lewis', '(219) 769-3541', 'ext. 323 and 390', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Library for the Blind and Physically Handicapped', '', 'Iowa Department for the Blind', '524 Fourth Street', 'Des Moines', 16, '50309-2364', 'US', 'http://www.blind.state.ia.us/library/', 'cathyf@blind.state.ia.us', 'Librarian: Ms. Catherine M. Ford', '(515) 281-1333', '(515) 281-1389 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Kansas State Library', '', 'Kansas Talking Book Service ESU Memorial Union', '1200 Commercial', 'Emporia', 17, '66801', 'US', 'http://skyways.lib.ks.us/KSL/talking/ksl_bph.html', 'pattilang@ink.org', 'Librarian: Ms. Patti Lang', '(316) 343-7124', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Talking Book Service', '', 'CKLS Headquarters', '1409 Williams', 'Great Bend', 17, '67530', 'US', '', 'jmasden@ckls.org', 'Librarian: Ms. Joanita Doll-Masden', '(316) 792-2393', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'South Central Kansas Library System', '', 'Talking Book Subregional', '901 North Main', 'Hutchinson', 17, '67501', 'US', 'http://www.hplsck.org/special.htm#talking', 'ksocha@hplsck.org', 'Librarian: Ms. Karen Socha', '(316) 663-5441', 'ext. 129', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Manhattan Public Library North Central Kansas Libraries ', '', 'Talking Book Service', '629 Poyntz Avenue', 'Manhattan', 17, '66502-6086', 'US', 'http://www.manhattan.lib.ks.us/bph.html', 'marionr@manhattan.lib.ks.us', 'Librarian: Ms. Marion Rice', '(785) 776-4741', 'ext. 152', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Northwest Kansas Library System', '', 'Talking Books', '2 Washington Square, P.O. Box 446', 'Norton', 17, '67654-0446', 'US', 'http://skyways.lib.ks.us/kansas/nwkls/howard/bph.html', 'tbook@ruraltel.net', 'Librarian: Ms. Clarice Howard', '(785) 877-5148', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Topeka and Shawnee County Public Library', '', 'Talking Books', '1515 SW 10th Avenue', 'Topeka', 17, '66604-1374', 'US', 'http://www.tscpl.org/library/tbook/tbooks1.htm', ' tbooks@tscpl.lib.ks.us', 'Librarian: Ms. Suzanne Bundy', '(785) 231-0574', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Wichita Public Library', '', 'Talking Books Section', '223 South Main', 'Wichita', 17, '67202', 'US', '', 'breha@wichita.lib.ks.us', 'Librarian: Mr. Brad Reha', '(316) 261-8500', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Kentucky Library for the Blind and Physically Handicapped', '', '300 Coffee Tree Road', 'P.O. Box 818', 'Frankfort', 18, '40602-0818', 'US', 'http://www.kdla.net/libserv/ktbl.htm', 'rfeindel@ctr.kdla.state.ky.us', 'Librarian: Mr. Richard Feindel', '(502) 564-8300', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Northern Kentucky Talking Book Library', '', '502 Scott Boulevard', '', 'Covington', 18, '41011', 'US', 'http://www.kenton.lib.ky.us/talking.html', 'jallegri@kenton.lib.ky.us', 'Librarian: Ms. Julia Allegrini', '(859) 491-7610', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Louisville Talking Book Library', '', '301 West York Street', '', 'Louisville', 18, '40203', 'US', '', '', 'Librarian: Mr. Tom Denning', '(502) 574-1625', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Louisiana State Library', '', 'Section for the Blind and Physically Handicapped', '701 North Fourth Street', 'Baton Rouge', 19, '70802', 'US', 'http://smt.state.lib.la.us/Dept/SpecServ/sbph.htm', 'sbph@pelican.state.lib.la.us', 'Librarian: Ms. Sharilynn Aucoin', '(225) 342-4944 ', '(225) 342-4943', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Maine State Library', '', 'Library Services for the Blind and Physically Handicapped', '64 State House Station', 'Augusta', 20, '04333-0064', 'US', '', 'benitad@ursus1.ursus.maine.edu', 'Librarian: Ms. Benita D. Davis', '(207) 287-5650 ', '(207) 947-8336', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Maryland State Library for the Blind and Physically Handicapped', '', '415 Park Avenue', '', 'Baltimore', 21, '21201-3603', 'US', 'http://www.lbph.lib.md.us/', 'recept@lbph.lib.md.us', 'Librarian: Ms. Sharron McFarland', '(410) 230-2424', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Montgomery County Department of Public Libraries', '', 'Special Needs Library', '', 'Bethesda', 21, '20817', 'US', 'http://www.mont.lib.md.us/sn.html', '', 'Librarian: Ms. Charlette Stinnett', '(301) 897-2212', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Perkins School for the Blind', '', 'Braille and Talking Book Library', '175 North Beacon Street', 'Watertown', 22, '02472', 'US', 'http://www.perkins.pvt.k12.ma.us/BTBL.htm', 'btbl@perkins.pvt.k12.ma.us', 'Librarian: Ms. Patricia Kirk', '(617) 972-7240', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Worcester Public Library', '', 'Talking Book Library', '160 Fremont Street', 'Worcester', 22, '01603-2362', 'US', 'http://www.worcpublic.org/talkingbook', 'talkbook@ultranet.com', 'Librarian: Mr. James Izatt', '(508) 799-1621', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Library of Michigan', '', 'Service for the Blind and Physically Handicapped', 'P.O. Box 30007', 'Lansing', 23, '48909', 'US', 'http://www.libofmich.lib.mi.sbph/sbphservices.html', 'info@sbph.libofmich.lib.mi.us', 'Librarian: Ms. Maggie Bacon', '(517) 373-5614', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Northland Library Cooperative', '', '316 East Chisholm Street', '', 'Alpena', 23, '49707', 'US', 'http://nlc.lib.mi.us/library/', 'nlclbph@northland.lib.mi.us', 'Librarian: Ms. Catherine Glomski', '(517) 356-1622', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Washtenaw County Library for the Blind and Physically Disabled', '', 'P.O. Box 8645', '', 'Ann Arbor', 23, '48107-8645', 'US', 'http://www.co.washtenaw.mi.us/DEPTS/LIBRARY.htm', 'wolfem@co.washtenaw.mi.us', 'Librarian: Ms. Margaret Wolfe', '(734) 971-6059', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Macomb Library for the Blind and Physically Handicapped', '', '16480 Hall Road', '', 'Clinton Township', 23, '48038-1132', 'US', '', 'champiol@libcoop.net', 'Librarian: Ms. Linda Champion', '(810) 286-1580', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Mideastern Michigan Library for the Blind and Physically ', '', 'Talking Book Center', 'G-4195 West Pasadena Avenue', 'Flint', 23, '48504', 'US', 'http://gdl.falcon.edu/talkingbookcenter/talkingbooks.htm', 'lking.gfn.org', 'Librarian: Ms. Deloris King', '(810) 732-1120', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Grandville Branch Library', '', 'Kent District Library for the Blind ', '4055 Maple Street SW', 'Grandville', 23, '49418', 'US', 'http://www.kdl.org/lbph.htm', 'screnshaw@kdl.org
screnshaw@kdl.org', 'Librarian: Ms. Cathy Neis', '(616) 530-6219', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Upper Peninsula Library for the Blind and Physically Handicapped', '', '1615 Presque Isle Avenue', '', 'Marquette', 23, '49855', 'US', 'http://www.unproc.lib.mi.us/uplbph/', 'uplbph@uproc.lib.mi.us', 'Librarian: Ms. Suzanne Dees', '(906) 228-7697', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Muskegon County Library for the Blind and Physically Handicapped', '', '97 East Apple Avenue', '', 'Muskegon', 23, '49442-3404', 'US', 'http://www.lakeland.lib.mi.us/muskegonco/lbph.html', 'star@sccl.lib.mi.us', 'Librarian: Ms. Sheila D. Miller', '(231) 724-6257', '(231) 727-6248 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Oakland County Library for the Blind and Physically Handicapped', '', '1200 North Telegraph, Dept. 482', '', 'Pontiac', 23, '48341-0482', 'US', 'http://tln.lib.mi.us/~oakl/oakllbph.htm', 'oakllbph@oakland.lib.mi.us', 'Librarian: Ms. Betty Ramey', '(248) 858-5050', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'St. Clair County Library', '', 'Special Technologies Alternative Resources (STAR)', '210 McMorran Boulevard', 'Port Huron', 23, '48060', 'US', 'http://www.sccl.lib.mi.us/star.html#', 'mjkoch@netra.sccl.lib.mi.us', 'Librarian: Ms. Mary Jo Koch', '(810) 982-3600', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Grand Traverse Area Library for the Blind and Physically Handicapped', '', '610 Woodmere Avenue', '', 'Traverse City', 23, '49686-3397', 'US', 'http://tadl.tcnet.org/index/lbph.htm', 'lbph@tcnet.org', 'Librarian: Ms. Kathy Kelto', '(231) 932-8558', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Wayne County Regional Library for the Blind and Physically Handicapped', '', '30555 Michigan Avenue', '', 'Westland', 23, '48186-5310', 'US', 'http://wayneregional.lib.mi.us/', 'wcrlbph@wayneregional.lib.mi.us', 'Librarian: Mr. Frederick Howkins', '(734) 727-7300', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Detroit Public Library', '', 'Douglass Branch Library', '3666 Grand River Avenue', 'Detroit', 23, '48208', 'US', 'http://www.detroit.lib.mi.us/special_services.htm', 'dmiddle@det.lib.mi.us', 'Librarian: Ms. Dori Middleton, acting', '(313) 833-5494 ', '(313) 833-5497', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Minnesota Library for the Blind and Physically Handicapped', '', '388 SE 6th Ave
', '', 'Faribault', 24, '55021-6340
', 'US', '', 'cfl.mlbph@state.mn.us
', 'Librarian: Ms. Catherine Durivage', '(507) 333-4828', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Mississippi Library for the Blind and Physically Handicapped', '', '5455 Executive Place', '', 'Jackson', 25, '39206-4104', 'US', 'http://www.mlc.lib.ms.us/lbph/lbph_index.html', 'lbph@mail.mcl.lib.ms.us', 'Librarian: Ms. Rahye Puckett', '(601) 713-3409', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Wolfner Library for the Blind and Physically Handicapped', '', 'P.O. Box 387', '', 'Jefferson City', 26, '65102-0387', 'US', 'http://mosl.sos.state.mo.us/lib-ser/wolf/wolfhome.html', 'wolfner@mail.sos.state.mo.us', 'Librarian: Ms. Sara Parker, acting', '(573) 751-8720', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Montana Talking Book Library', '', '1515 East Sixth Avenue', '', 'Helena', 27, '59620-1800', 'US', 'http://msl.state.mt.us/mtbl/tbl.html', 'cbriggs@state.mt.us', 'Librarian: Ms. Christie O. Briggs', '(406) 444-2064', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Nebraska Library Commission', '', 'The Atrium', '1200 N Street, Suite 120', 'Lincoln', 28, '68508-2023', 'US', 'http://www.nlc.state.ne.us/tbbs/tbbs1.html', 'doertli@neon.nlc.state.ne.us', 'Librarian: Mr. David Oertli', '(402) 471-4038', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Nevada Libraries for the Blind and Physically Handicapped', '', '100 North Stewart Street', '', 'Carson City', 29, '89701', 'US', 'http://dmla.clan.lib.nv.us/docs/nsla/tbooks/', 'putnam@equinox.unr.edu', 'Librarian: Mrs. Kerin E. Putnam', '(775) 684-3354', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Las Vegas-Clark County Library District', '', '1401 East Flamingo Road', '', 'Las Vegas', 29, '89119', 'US', '', 'mamorton@clan.lib.nv.us', 'Librarian: Ms. Mary Anne Morton', '(702) 733-1925', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'New Hampshire State Library', '', 'Library Services to Persons with Disabilities', '117 Pleasant Street', 'Concord', 30, '03301-3852', 'US', 'http://www.state.nh.us/nhsl/talkbks/index.html', 'ekeim@finch.nhsl.lib.nh.us', 'Librarian: Ms. Eileen Keim', '(603) 271-3429', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'New Jersey Library for the Blind and Handicapped', '', '2300 Stuyvesant Avenue
', '', 'Trenton', 31, '08618', 'US', 'http://www2.njstatelib.org/njlib/njlbh.htm', 'njlbh@njstatelib.org', 'Librarian: Ms. Vianne Connor', '(609) 530-4000', '800-792-8322
', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'New Mexico Talking Book Library', '', '1209 Camino Carlos Rey', '', 'Santa Fe', 32, '87501-2777', 'US', 'http://www.stlib.state.nm.us/tbl.prog-info/tblpage.html', 'jbrewstr@stlib.state.nm.us', 'Librarian: Mr. John Brewster', '(505) 476-9770', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'New York State Talking Book and Braille Library', '', 'Cultural Education Center', 'Empire State Plaza', 'Albany', 33, '12230', 'US', 'http://www.nysl.nysed.gov/talk.htm', 'tbbl@mail.nysed.gov', 'Librarian: Ms. Jane Somers', '(518) 474-5935', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Andrew Heiskell Library for the Blind and Physically ', '', '40 West 20th Street', '', 'New York', 33, '10011-4211', 'US', 'http://www.nypl.org/branch/central_units/lb/LB.html', 'ahlbph@nypl.org', 'Librarian: Ms. Kathleen V. Rowan', '(212) 206-5400 ', '(212) 206-5425', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Suffolk Cooperative Library System', '', '627 North Sunrise Service Road', '', 'Bellport', 33, '11713', 'US', 'http://www.suffolk.lib.ny.us/tbp', 'lbph@suffolk.lib.ny.us', 'Librarian: Ms. Julie Klauber', '(631) 286-1600', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Nassau Library System', '', '900 Jerusalem Avenue', '', 'Uniondale', 33, '11553', 'US', '', 'nls@nassaulibrary.org', 'Librarian: Dorothy Puryear (acting)', '(516) 292-8920', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'North Carolina Library for the Blind ', '', 'Department of Cultural Resources', '1811 Capital Boulevard', 'Raleigh', 34, '27635', 'US', 'http://statelibrary.dcr.state.nc.us/lbph/lbph.htm', 'nclbph@ncsl.dcr.state.nc.us', 'Librarian: Ms. Francine I. Martin', '(919) 733-4376', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'North Dakota State Library', '', 'Talking Book Services', '604 East Boulevard Avenue, Department 250', 'Bismarck', 35, '58505-0800', 'US', 'http://ndsl.lib.state.nd.us/hand.htm', '', '', '(701) 328-1408', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'The Public Library of Cincinnati and Hamilton County', '', 'Library for the Blind ', '800 Vine Street, Library Square', 'Cincinnati', 36, '45202-2071', 'US', 'http://plch.lib.oh.us/main/lb/index.html', '', 'Librarian: Ms. Donna Foust', '(513) 369-6999', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Library for the Blind and Physically Handicapped', '', '17121 Lake Shore Boulevard', '', 'Cleveland', 36, '44110-4006', 'US', 'http://www.cpl.org/policies.html#Talking', 'Barbara.Mates@cpl.org', 'Librarian: Ms. Barbara T. Mates', '(216) 623-2911', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Oklahoma Library for the Blind and Physically Handicapped', '', '300 NE 18th Street', '', 'Oklahoma City', 37, '73105', 'US', 'http://www.state.ok.us/~library/', 'olbph@oltn.odl.state.ok.us', 'Librarian: Ms. Geraldine Adams', '(405) 521-3514 ', '(405) 521-3833', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Oregon State Library', '', 'Talking Book and Braille Services', '250 Winter Street NE', 'Salem', 38, '97310-0645', 'US', 'http://www.osl.state.or.us/tbabs/tbabs.html', 'tbabs@sparkie.osl.state.or.us', '', '(503) 378-3849 ', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Free Library of Philadelphia', '', 'Library for the Blind and Physically Handicapped', '919 Walnut Street', 'Philadelphia', 39, '19107', 'US', 'http://www.library.phila.gov/central/plbph/plbph.htm', 'flpblind@library.phila.gov', 'Librarian: Ms. Vickie L. Collins', '(215) 683-3213', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Carnegie Library of Pittsburgh', '', 'Leonard C. Staisey Building', '4724 Baum Boulevard', 'Pittsburgh', 39, '15213-1389', 'US', 'http://www.clpgh.org/clp/LBPH', 'clbph@clpgh.org', 'Librarian: Mrs. Sue O. Murdock', '(412) 687-2440', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Puerto Rico Regional Library for the Blind and Physically Handicapped', '', '520 Ponce de Leon Avenue, Suite 2', '', 'San Juan', -51, '00901', 'US', '', 'ienrique@tld.net', 'Librarian: Ms. Igri Enriquez', '(787) 723-2519', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Office of Library and Information Services', '', 'Talking Books Plus', 'One Capitol Hill', 'Providence', 40, '02908', 'US', 'http://www.lori.state.ri.us/tbp/default.htm', 'tbplus@lori.state.ri.us', 'Librarian: Mr. Andrew Egan', '(401) 222-5800', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'South Carolina State Library', '', 'Department for the Blind ', 'P.O. Box 821', 'Columbia', 41, '29202-0821', 'US', 'http://www.state.sc.us/scsl/bph.html', 'guynell@leo.scsl.state.sc.us', 'Librarian: Ms. Guynell Williams', '(803) 898-5900', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'South Dakota Braille and Talking Book Library', '', 'State Library Building', '800 Governors Drive', 'Pierre', 42, '57501-2294', 'US', 'http://state.sd.us/library/talkbook/', 'dan.boyd@state.sd.us', 'Librarian: Mr. Daniel W. Boyd', '(605) 773-3131', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Tennessee Library for the Blind and Physically Handicapped', '', '403 Seventh Avenue North', '', 'Nashville', 43, '37243-0313', 'US', 'http://www.state.tn.us/sos/statelib/LBPH/lbph.htm', 'tlbph@mail.state.tn.us', 'Librarian: Ruth Hemphill', '(615) 741-3915', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Texas State Library and Archives Commission', '', 'Talking Book Program', 'P.O. Box 12927', 'Austin', 44, '78711-2927', 'US', 'http://www.tsl.state.tx.us/TBP/TBPhome.html', 'tbp.services@tsl.state.tx.us', 'Librarian: Ms. Jenifer O. Flaxbart', '(512) 463-5458', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Utah State Library Division', '', 'Program for the Blind and Physically Handicapped', '250 North 1950 West, Suite A', 'Salt Lake City', 45, '84116-7901', 'US', 'http://www.state.lib.ut.us/blind.html', 'gbuttars@state.lib.ut.us', 'Librarian: Mr. Gerald A. Buttars', '(801) 715-6789', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Vermont Department of Libraries', '', 'Special Services Unit', '578 Paine Turnpike North', 'Berlin', 46, '05602', 'US', '', 'ssu@dol.state.vt.us', 'Librarian: Mr. S. Francis Woods', '(802) 828-3273', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Virgin Islands Library for the Visually and Physically Handicapped', '', '3012 Golden Rock', '', 'Christiansted, St. Croix', -51, '00820', 'US', '', '', 'Librarian: Ms. Sarita Tucker', '(340) 772-2250', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Virginia Library and Resource Center', '', 'Virginia Department for the Visually Handicapped', '395 Azalea Avenue', 'Richmond', 47, '23227-3623', 'US', 'http://www.cns.state.va.us/dvh/library_and_resource_center.htm#Library', 'mccartbn@dvh.state.va.us', 'Librarian: Ms. Barbara McCarthy', '(804) 371-3661', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Alexandria Library - Beatley Center', '', 'Talking Book Service', '5005 Duke Street', 'Alexandria', 47, '22304', 'US', 'http://www.alexandria.lib.va.us/talkbook.html', '', 'Librarian: Ms. Ilze Cimermanis', '(703) 823-6152', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Arlington County Department of Libraries', '', 'Talking Book Service', '1015 North Quincy Street', 'Arlington', 47, '22201', 'US', 'http://www.co.arlington.va.us/outreach/special.htm', 'talkingbooks@co.arlington.va.us', 'Librarian: Ms. Roxanne Barnes', '(703) 228-6333', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Fairfax County Public Library', '', 'Access Services', '12000 Government Center Parkway suite 123', 'Fairfax', 47, '22035-0012', 'US', 'http://www.loc.gov/www.co.fairfax.va.us/library/access/access.htm', 'as@fcpl.co.fairfax.va.us', 'Librarian: Ms. Jeanette A. Studley', '(703) 324-8380', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Fredericksburg Area Subregional Library', '', 'Central Rappahannock Regional Library', '1201 Caroline Street', 'Fredericksburg', 47, '22401', 'US', 'http://www.crrl.org/reaching/osub.htm', 'nschiff@crrl.org', 'Librarian: Ms. Nancy Schiff', '(540) 372-1144', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Hampton Subregional Library for the Blind ', '', 'South Mallory Street', '', 'Hampton', 47, '23663', 'US', '', 'mwoolard@city.hampton.va.us', 'Librarian: Ms. Mary Sue Woolard', '(757) 727-1900', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Newport News Public Library System', '', 'Library for the Blind and Physically Handicapped', '110 Main Street', 'Newport News', 47, '23601', 'US', '', 'nnlph@ci.newport-news.va.us', 'Librarian: Ms. Sue Baldwin', '(757) 591-4858', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Roanoke City Public Library', '', 'Talking Book Services', '2607 Salem Turnpike NW', 'Roanoke', 47, '24017-5397', 'US', '', '', 'Librarian: Mrs. Rebecca Cooper', '(540) 853-2648', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Special Services Library', '', '930 Independence Boulevard', '', 'Virginia Beach', 47, '23455', 'US', '', 'awicher@city.virginia-beach.va.us', 'Librarian: Ms. Aleene Wicher', '(757) 464-9175', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Washington Talking Book and Braille Library', '', '2021 9th Avenue', '', 'Seattle', 48, '98121-2783', 'US', 'http://www.spl.lib.wa.us/wtbbl/wtbbl.html', 'wtbbl@wtbbl.org', 'Librarian: Ms. Jan Ames', '(206) 615-0400', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'West Virginia Library Commission', '', 'Special Services', '1900 Kanawha Boulevard East', 'Charleston', 49, '25305', 'US', 'http://wvlc.lib.wv.us/', 'calvertd@mail.wv1c.lib.wv.us', 'Librarian: Ms. Donna B. Calvert', '(304) 558-4061', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Cabell County Public Library', '', 'Services for the Blind and Physically Handicapped', '455 Ninth Street Plaza', 'Huntington', 49, '25701', 'US', '', '', 'Librarian: Ms. Suzanne L. Marshall', '(304) 528-5700', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Parkersburg and Wood County Public Library', '', 'Services for the Blind and Physically Handicapped', '3100 Emerson Avenue', 'Parkersburg', 49, '26104-2414', 'US', '', 'hickmanm@hp9k.park.lib.wv.us', 'Librarian: Mr. Michael Hickman', '(304) 420-4587', 'ext. 5', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'West Virginia School for the Blind Library', '', '301 East Main Street', '', 'Romney', 49, '26757', 'US', '', 'cjohnsonwv@hotmail.com', 'Librarian: Ms. Cynthia S. Johnson', '(304) 822-4894', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Ohio County Public Library', '', 'Services for the Blind and Physically Handicapped', '52 16th Street', 'Wheeling', 49, '26003-3696', 'US', '', '', 'Librarian: Ms. Lori Nicholson', '(304) 232-0244', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Wisconsin Regional Library for the Blind and Physically Handicapped', '', '813 West Wells Street', '', 'Milwaukee', 50, '53233-1436', 'US', 'http://www.dpi.state.wi.us/dpi/dlcl/rll/lbphinfo.html', 'mvalan@mpl.org', 'Librarian: Ms. Marsha Valance', '(414) 286-3045', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'National Library Service for the Blind and Physically Handicapped', '', 'Library of Congress', '', 'Washington', 9, '20542', 'US', 'http://www.loc.gov/nls', 'nls@loc.gov', 'Librarian: Mr. Yealuri Rathan Raj', '(202) 707-9261', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Association for the Blind and Visually Impaired (ABVI)', '', 'Goodwill Industries of Greater Rochester, Inc. ', '422 South Clinton Ave ', 'Rochester', 33, '14620', 'US', '', 'Laura_Townsend@abvi-goodwill.com', 'Laura Townsend', '716-232-1111', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Hawaii Center for the Deaf and Blind', '', '3440 Leahi Ave Bldg A', '', 'Honolulu', 12, '96815', 'US', '', '', '', '(808) 733-4999', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (1, 'Arizona State Library Archives and Public Records Braille and Talking Book Division', '', '1030 North 32nd Street ', '', 'Phoenix', 3, '85008 ', 'US', '', '', '', '', '', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (3, 'Ohana Paws Training Center ', '', 'PMB 105 590 ', 'Farrington Hwy #210 ', 'Kapolei', 12, '96707', 'US', '', '', '', '(808)696-5553 ', 'fax: (808)674-0536 ', '', 1);
INSERT INTO BRL_Resource (typeID, name, description, address1, address2, city, stateID, zip, country, web, email, contact, phone, phone2, tdd, Publish)
  VALUES (2, 'Stratford Library Association', '', '2203 Main Street', '', 'Stratford ', 7, '06615', 'US', 'http://www.Stratford.lib.ct.us', 'laura@stratford.lib.ct.us', 'Martha Simpson', '203-385-4165', '', '', 1);
 
GO

