/* ** www.evolutility.org - (c) 2010 Olivier Giulieri  ** */
/*    SQL script using Evolutility in a multi-user environment  - using MySQL database   */

/* Users */

CREATE TABLE `EVOL_User` (
	`ID`  int(10) unsigned NOT NULL auto_increment,
	`Admin` int(1)   NULL ,
	`Publish` int(1)   NULL ,
	`Login`  varchar(50) DEFAULT  'Anonymous',
	`Password`  varchar(50) DEFAULT '',
	`TagLine`  varchar(100) NULL ,
	`Firstname`  varchar(50) NULL ,
	`Lastname`  varchar(50) NULL ,
	`email`  varchar(100) NULL ,
	`phone`  varchar(20) NULL ,
	`cell`  varchar(20) NULL ,
	`fax`  varchar(20) NULL ,
	`Address`  varchar(300) NULL,
	`City`  varchar(100) NULL ,
	`State`  varchar(2) NULL ,
	`Zip` varchar(15) NULL ,
	`Country` varchar(60) NULL ,
	`Notes` varchar(800) NULL ,
	`lastvisit`  datetime  DEFAULT NULL ,
	`nbVisits` int(10) DEFAULT 0,
	`CommentCount` int(10) default 0,
	`CreationDate`  timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
 
 
INSERT INTO EVOL_User (id,Publish, `Admin`, Login, TagLine, Password, Firstname, Lastname)
  VALUES (1, 0, 1, 'Evol', 'Evolutility Administrator', 'love', 'Admin', 'Admin');
INSERT INTO EVOL_User (Publish, Login, TagLine, Password, Firstname, Lastname, email)
  VALUES (1, 'John', 'DB guru', 'john', 'John', 'Smith', 'info@evolutility.com');
INSERT INTO EVOL_User (Publish, Login, TagLine, Password, Firstname, Lastname,  email )
  VALUES (1, 'Mary', 'Hi', 'mary', 'Mary', 'Johnson',  'mary@evolutility.com'  );
 

/*  User Login  */

DELIMITER //

CREATE PROCEDURE EvoSP_Login  (
  IN pLogin  varchar(50),
  IN pPassword varchar(50)
)
BEGIN

DECLARE pUserID INT default 0;
SELECT ID FROM `EVOL_User` WHERE login= pLogin AND password= pPassword INTO pUserID;
IF (pUserID>0) THEN
    	UPDATE `EVOL_User` SET lastvisit=CURDATE(), nbvisits=nbvisits+1  WHERE  ID=pUserID;
    	SELECT ID, login, firstname, 'Welcome ' + firstname AS welcome FROM `Evol_User`  WHERE  ID=pUserID;
END IF;
END //

DELIMITER ;





/*  User comments  */

CREATE TABLE `EVOL_Comment` (
	`ID`  int(10) unsigned NOT NULL auto_increment,
	`FormID` int(10) NULL ,
	`ItemID` int(10) NULL ,
	`UserID` int(10) NULL ,
	`message` varchar(1000) NULL ,
	`CreationDate`  timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

