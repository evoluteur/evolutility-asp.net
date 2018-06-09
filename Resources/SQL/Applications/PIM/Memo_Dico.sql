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
/*  Metadata for : Memo pad */

DECLARE @FormID int;
DECLARE @PanelID int;

-- form --
INSERT INTO EvoDico_Form (UserID, Title, icon, entity, entities, dbtable, dborder, dbcolumnlead, Publish, [Description], sppaging, splogin)
  VALUES (1, 'Memo pad', 'memo.gif', 'memo', 'memos', 'EVOL_Memo', '', 'Title', 1, 'Sample application: Memo pad.', 'EvoSP_PagedItem @SQLselect, @SQLtable, @SQLfrom, @SQLwhere, @SQLorderby, @SQLpk, @pageid, @pagesize', 'EvoSP_Login @login, @password');
SELECT @FormID = SCOPE_IDENTITY();

-- panel --
INSERT INTO EvoDico_Panel (FormID, UserID, TypeID, Label, Width, pPos)
  VALUES (@FormID, 1, 1, '', 100, 1);
SELECT @PanelID = SCOPE_IDENTITY();

-- fields --
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, required, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'Title', 5, 'Title', 'Title', 200, 1, @PanelID, 1, 10, 1, 1, 1, 1, 'FieldMain', 1, 100, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, dbtablelov, dborderlov, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, options, Publish)
  VALUES (@FormID, 1, 'Category', 4, 'CategoryID', 'Category', 100, 'EVOL_MemoCategory', 'id', @PanelID, 1, 10, 1, 1, 1, 0, '', 1, 62,  '[Unfiled], Hobby, Travel, Finances, Personal, Business, Restaurant, Health ', 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, PanelID, PanelIndex, fpos, search, searchadv, searchlist, height, width, Publish)
  VALUES (@FormID, 1, 'Date', 2, 'CreationDate', 'Creationdate', 100, @PanelID, 1, 10, 1, 1, 1, 1, 38, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, PanelID, PanelIndex, fpos, search, searchadv, searchlist, height, width, Publish)
  VALUES (@FormID, 1, 'Notes', 6, 'Notes', 'Notes', 500, @PanelID, 1, 10, 0, 1, 0, 6, 100, 1);
  
  