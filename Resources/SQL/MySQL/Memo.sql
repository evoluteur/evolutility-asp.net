/* Memo pad  */
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
	GNU Affero General Public License for more details.

	You should have received a copy of the GNU Affero General Public License Version 3
	along with Evolutility. If not, see <http://www.fsf.org/licensing/licenses/agpl-3.0.html>.
*/

CREATE TABLE `EVOL_Memo` (
	`ID`  int(10) unsigned NOT NULL auto_increment,
	`UserID` int(10) unsigned default 1,
	`Publish` int(10) unsigned default 0,
	`CommentCount` int(10) unsigned default 0,
	`title`  varchar(200) default NULL,
	`CategoryID` int(10) unsigned default NULL,
	`Notes`  varchar(1000) default NULL,
	`creationdate` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP, 
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `EVOL_MemoCategory` (
	`ID`  int(10) unsigned NOT NULL auto_increment,
	`name`  varchar(50) DEFAULT 'Untitled',
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
 

INSERT INTO EVOL_MemoCategory (`Name`) VALUES ('Unfiled');
INSERT INTO EVOL_MemoCategory (`Name`) VALUES ('Quotes');
INSERT INTO EVOL_MemoCategory (`Name`) VALUES ('List');
INSERT INTO EVOL_MemoCategory (`Name`) VALUES ('Poetry'); 
INSERT INTO EVOL_MemoCategory (`Name`) VALUES ('Business');
INSERT INTO EVOL_MemoCategory (`Name`) VALUES ('Restaurant'); 
 


INSERT INTO EVOL_Memo (title, CreationDate, CategoryID, Notes)
  VALUES ('Books to read', '2005-11-27 12:00:00 AM', 3, 'Yoga for beginners

Clayton Christianson in his first book, Innovator’s Dilemma');

INSERT INTO EVOL_Memo (title, CreationDate, CategoryID, Notes)
  VALUES ('Quotes from Einstein', '2006-1-12 12:00:00 AM', 2, 'Imagination is more important than knowledge. Knowledge is limited. Imagination encircles the world.

I have no special talents. I am only passionately curious.

Anyone who has never made a mistake has never tried anything new.

A profusion of means and a confusion of goals is the symptom of our age.

Everything should be made as simple as possible, but not simpler. 

If we knew what it was we were doing, it would not be called research, would it? 

Study and, in general the pursuit of truth and beauty is a sphere of activity in which we are permitted to remain children all of our lives. 

Wisdom is not a product of schooling but of the life-long attempt to acquire it. 

Try not to become a man of success, but rather try to become a man of value. 
');

INSERT INTO EVOL_Memo (title, CreationDate, CategoryID, Notes)
  VALUES ('Quotes from Napoleon', '2005-12-27 11:17:16 PM', 2, 'The strong are good, only the weak are wicked. 

Forces is the law of animals, men are ruled by conviction. 

There is no strength without justice. ');

INSERT INTO EVOL_Memo (title, CreationDate, CategoryID, Notes)
  VALUES ('Quotes from Woody Allen', '2006-11-10 12:00:00 AM', 2, 'As the poet said, ''Only God can make a tree'' -- probably because it''s so hard to figure out how to get the bark on. 

Eighty percent of success is showing up. 

Eternal nothingness is fine if you happen to be dressed for it. 
 
His lack of education is more than compensated for by his keenly developed moral bankruptcy. 
 
How can I believe in God when just last week I got my tongue caught in the roller of an electric typewriter? 

How is it possible to find meaning in a finite world, given my waist and shirt size? 
 
I am at two with nature. 
 
I can''t listen to that much Wagner. I start getting the urge to conquer Poland. 

I don''t want to achieve immortality through my work... I want to achieve it through not dying. 

I tended to place my wife under a pedestal. 

I took a speed reading course and read ''War and Peace'' in twenty minutes. It involves Russia. 
 
I was thrown out of college for cheating on the metaphysics exam; I looked into the soul of the boy sitting next to me. ');

INSERT INTO EVOL_Memo (title, CreationDate, CategoryID, Notes)
  VALUES ('Movies to see', '2006-11-11 12:00:08 AM', 3, 'Deja Vu
Children of heaven
The corporation
Super size me');
