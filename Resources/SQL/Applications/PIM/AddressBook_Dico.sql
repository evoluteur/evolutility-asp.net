/* www.evolutility.org - (c) 2009 Olivier Giulieri */
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
/*  Metadata for : Address book */

DECLARE @FormID int;
DECLARE @PanelID int;

-- form --
INSERT INTO EvoDico_Form (UserID, Title, icon, entity, entities, dbtable, dborder, dbcolumnlead, dbcolumnicon, Publish, CommentsID, [Description], sppaging, splogin)
  VALUES (1, 'Address book', 'contact.gif', 'contact', 'contacts', 'EVOL_Contact', 'lastname,firstname', 'firstname', '',  1, 0, 'Sample application: Contacts list.', 'EvoSP_PagedItem @SQLselect, @SQLtable, @SQLfrom, @SQLwhere, @SQLorderby, @SQLpk, @pageid, @pagesize', 'EvoSP_Login @login, @password');
SELECT @FormID = SCOPE_IDENTITY();

-- panel --
INSERT INTO EvoDico_Panel (FormID, UserID, TypeID, [Readonly], Label, Width, pPos)
  VALUES (@FormID, 1, 1, 0, 'Name', '61', 10);
SELECT @PanelID = SCOPE_IDENTITY();

-- fields --
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, PanelID, PanelIndex, fpos, search, searchadv, searchlist, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'Lastname', 5, 'Lastname', 'Lastname', 50, 0, 1, 0, @PanelID, 1, 10, 1, 1, 1, 'fieldmain', 1, 50, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'Firstname', 5, 'Firstname', 'Firstname', 50, 0, 0, 0, '', @PanelID, 1, 10, 1, 1, 1, 1, 'fieldmain', 1, 50, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'Title', 5, 'Title', 'Title', 50, 0, 0, 0, '', @PanelID, 1, 10, 0, 1, 0, 1, '', 1, 50, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'Company', 5, 'Company', 'Company', 50, 0, 0, 0, '', @PanelID, 1, 10, 1, 1, 1, 1, '', 1, 50, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, help, Publish)
  VALUES (@FormID, 1, 'email', 3, 'emailaddress', 'emailaddress', 255, 0, 0, 0, '', @PanelID, 1, 10, 0, 1, 0, 0, '', 1, 100, 'someone@somecompany.com', 1);
  
-- panel --    
INSERT INTO EvoDico_Panel (FormID, UserID, TypeID, [Readonly], Label, Width, pPos)
  VALUES (@FormID, 1, 1, 0, 'Contact Info', '39', 20);
SELECT @PanelID = SCOPE_IDENTITY();

-- fields --  
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, help, options, Publish)
  VALUES (@FormID, 1, 'Work Phone', 5, 'phone', 'phone', 20, 0, 0, 0, '', @PanelID, 1, 10, 0, 1, 0, 0, '', 1, 50, '', '', 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, help, options, Publish)
  VALUES (@FormID, 1, 'Home Phone', 5, 'phoneh', 'phoneh', 20, 0, 0, 0, '', @PanelID, 1, 10, 0, 1, 0, 0, '', 1, 50, '', '', 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, help, options, Publish)
  VALUES (@FormID, 1, 'Mobile', 5, 'phonem', 'phonem', 20, 0, 0, 0, '', @PanelID, 1, 10, 0, 1, 0, 0, '', 1, 50, '', '', 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, help, options, Publish)
  VALUES (@FormID, 1, 'Fax', 5, 'Fax', 'Fax', 20, 0, 0, 0, '', @PanelID, 1, 10, 0, 0, 0, 0, '', 1, 50, '', '', 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, help, options, Publish)
  VALUES (@FormID, 1, 'URL', 7, 'url', 'url', 255, 0, 0, 0, '', @PanelID, 1, 10, 0, 1, 0, 0, '', 1, 100, 'http://www.evolutility.com', '', 1);
 
-- panel --   
INSERT INTO EvoDico_Panel (FormID, UserID, TypeID, [Readonly], Label, Width, pPos)
  VALUES (@FormID, 1, 1, 0, 'Address', '61', 30);
SELECT @PanelID = SCOPE_IDENTITY();
  
-- fields --
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, help, options, Publish)
  VALUES (@FormID, 1, 'Address', 6, 'AddressLine1', 'AddressLine1', 255, 0, 0, 0, '', @PanelID, 1, 10, 0, 1, 0, 0, '', 3, 100, '', '', 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, help, options, Publish)
  VALUES (@FormID, 1, 'City', 5, 'City', 'City', 100, 0, 0, 0, '', @PanelID, 1, 10, 1, 1, 0, 1, '', 1, 60, '', '', 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, help, options, Publish)
  VALUES (@FormID, 1, 'State', 5, 'State', 'State', 3, 0, 0, 0, '', @PanelID, 1, 10, 0, 1, 0, 0, '', 1, 20, '', '', 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, help, options, Publish)
  VALUES (@FormID, 1, 'Zip', 5, 'Zip', 'Zip', 12, 0, 0, 0, '', @PanelID, 1, 10, 0, 1, 0, 0, '', 1, 20, '', '', 1);
  
-- panel --   
INSERT INTO EvoDico_Panel (FormID, UserID, TypeID, [Readonly], Label, Width, pPos)
  VALUES (@FormID, 1, 1, 0, 'Misc.', '39', 40);
SELECT @PanelID = SCOPE_IDENTITY();

-- fields --
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, dbtablelov, PanelID, PanelIndex, fpos, search, searchadv, searchlist, height, width, options, Publish)
  VALUES (@FormID, 1, 'Category', 4, 'CategoryID', 'Category', 100, 'EVOL_ContactCategory', @PanelID, 1, 10, 1, 1, 1, 1, 70, '[Unfiled], Hobby, Travel, Finances, Personal, Business, Restaurant, Health ', 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, help, options, Publish)
  VALUES (@FormID, 1, 'Notes', 6, 'notes', 'notes', 500, 0, 0, 0, '', @PanelID, 1, 10, 0, 0, 0, 0, '', 4, 100, '', '', 1);

  
  