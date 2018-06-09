/* (c) 2011 Olivier Giulieri - www.evolutility.org */
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
/*  Metadata for :  Bookmarks */

DECLARE @FormID int;
DECLARE @PanelID int;

-- form --
INSERT INTO EvoDico_Form (Title, UserID, Publish, icon, entity, entities, dbtable, dborder, dbcolumnlead, dbcolumnpk, Description, sppaging, splogin)
  VALUES ('Bookmarks', 1, 0, 'favourites.gif', 'bookmark', 'bookmarks', 'EVOL_Bookmark', 'title', 'title', 'ID', 'Sample application: Bookmarks list.', 'EvoSP_PagedItem @SQLselect, @SQLtable, @SQLfrom, @SQLwhere, @SQLorderby, @SQLpk, @pageid, @pagesize', 'EvoSP_Login @login, @password');
SELECT @FormID = SCOPE_IDENTITY();

-- panel --
INSERT INTO EvoDico_Panel (FormID, UserID, TypeID, Label, Width, pPos)
  VALUES (@FormID, 1, 1, 'Name', '61', 10);
SELECT @PanelID = SCOPE_IDENTITY();

-- fields --
INSERT INTO EvoDico_Field (FormID, UserID, PanelID, Label, TypeID, dbcolumn, dbcolumnread, dbcolumnicon, maxlength, dbtablelov, dborderlov, dbcolumnreadlov, dbcolumndetails, dbwherelov, readonly, required, format, link, linklabel, linktarget, search, searchadv, searchlist, cssclass, height, width, CreationDate)
  VALUES (@FormID, 1, @PanelID, 'Title', 5, 'Title', 'Title', '', 200, '', '', '', '', '', 0, 1, '', '', '', '', 1, 1, 1, 'FieldMain', 1, 62, '2011-1-18 06:29:48 PM');
INSERT INTO EvoDico_Field (FormID, UserID, PanelID, Label, TypeID, dbcolumn, dbcolumnread, dbcolumnicon, maxlength, dbtablelov, dborderlov, dbcolumnreadlov, dbcolumndetails, dbwherelov, readonly, required, format, link, linklabel, linktarget, search, searchadv, searchlist, cssclass, height, width, CreationDate)
  VALUES (@FormID, 1, @PanelID, 'Url', 7, 'url', 'url', '', 300, '', '', '', '', '', 0, 1, '', '', '', 'link', 1, 1, 1, '', 2, 62, '2011-1-18 06:29:48 PM');
INSERT INTO EvoDico_Field (FormID, UserID, PanelID, Label, TypeID, dbcolumn, dbcolumnread, dbcolumnicon, maxlength, dbtablelov, dborderlov, dbcolumnreadlov, dbcolumndetails, dbwherelov, readonly, required, format, link, linklabel, linktarget, search, searchadv, searchlist, cssclass, height, width, CreationDate)
  VALUES (@FormID, 1, @PanelID, 'Category', 4, 'CategoryID', 'Category', '', 2, 'EVOL_BookmarkCategory', '', 'name', '', '', 0, 0, '', '', '', '', 1, 1, 1, '', 1, 38, '2011-1-18 06:29:48 PM');
INSERT INTO EvoDico_Field (FormID, UserID, PanelID, Label, TypeID, dbcolumn, dbcolumnread, dbcolumnicon, maxlength, dbtablelov, dborderlov, dbcolumnreadlov, dbcolumndetails, dbwherelov, readonly, required, format, link, linklabel, linktarget, search, searchadv, searchlist, cssclass, height, width, CreationDate)
  VALUES (@FormID, 1, @PanelID, 'Notes', 6, 'notes', 'notes', '', 8, '', '', '', '', '', 0, 0, '', '', '', '', 1, 1, 0, '', 4, 100, '2011-1-18 06:29:48 PM');
INSERT INTO EvoDico_Field (FormID, UserID, PanelID, Label, TypeID, dbcolumn, dbcolumnread, dbcolumnicon, maxlength, dbtablelov, dborderlov, dbcolumnreadlov, dbcolumndetails, dbwherelov, readonly, required, format, link, linklabel, linktarget, search, searchadv, searchlist, cssclass, height, width, CreationDate)
  VALUES (@FormID, 1, @PanelID, 'Creation date', 17, 'CreationDate', 'CreationDate', '', 0, '', '', '', '', '', 1, 0, '', '', '', '', 1, 1, 0, '', 1, 38, '2011-1-18 06:29:48 PM');
