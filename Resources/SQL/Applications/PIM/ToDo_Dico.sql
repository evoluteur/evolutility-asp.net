/* (c) 2012 Olivier Giulieri - www.evolutility.org */
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
/*  Metadata for : To do list */

DECLARE @FormID int;
DECLARE @PanelID int;

-- form --
INSERT INTO EvoDico_Form (UserID, Title, icon, entity, entities, dbtable, dborder, dbcolumnlead, dbcolumnicon, Publish, CommentsID, [Description], sppaging, splogin)
  VALUES (1, 'To Do list', 'todo.gif', 'task', 'tasks', 'EVOL_ToDo', 'complete, duedate', '', '', 1, 0, 'Sample application: Tasks list.', 'EvoSP_PagedItem @SQLselect, @SQLtable, @SQLfrom, @SQLwhere, @SQLorderby, @SQLpk, @pageid, @pagesize', 'EvoSP_Login @login, @password');
SELECT @FormID = SCOPE_IDENTITY();

-- panel --
INSERT INTO EvoDico_Panel (FormID, UserID, TypeID, Label, Width, pPos)
  VALUES (@FormID, 1, 1, 'Task', 62, 1);
SELECT @PanelID = SCOPE_IDENTITY();

-- fields --
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, required, PanelID, PanelIndex, fpos, search, searchadv, searchlist, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'Title', 5, 'title', 'title', 255, 1, @PanelID, 1, 10, 1, 1, 1, 'FieldMain', 1, 100, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, PanelID, PanelIndex, fpos, search, searchadv, searchlist, height, width, Publish)
  VALUES (@FormID, 1, 'Due Date', 2, 'duedate', 'duedate', 10, @PanelID, 1, 10, 1, 1, 1, 1, 40, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, dbtablelov, dborderlov, dbcolumnreadlov, dbcolumndetails, dbwherelov, lovmany, lovsplist, img, imglist, defaultvalue, [Readonly], required, PanelID, PanelIndex, fpos, search, searchadv, searchlist, height, width, help, options, Publish)
  VALUES (@FormID, 1, 'Priority', 4, 'PriorityID', 'Priority', 100, 'EVOL_ToDoPriority', 'id', '', '', '', 0, '', '', '', '', 0, 0, @PanelID, 1, 10, 1, 1, 1, 1, 40, '', '1-ASAP, 2-Urgent, 3-Important, 4-Secondary, 5-Whenever', 1);

  
-- panel --
INSERT INTO EvoDico_Panel (FormID, UserID, TypeID, [Readonly], ModeID, TabID, Label, Width, pPos)
  VALUES (@FormID, 1, 1, 0, 1, 0, 'Status', '38', 1);
SELECT @PanelID = SCOPE_IDENTITY();

-- fields --  
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, dbtablelov, dborderlov, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, options, Publish)
  VALUES (@FormID, 1, 'Category', 4, 'CategoryID', 'Category', 100, 'EVOL_ToDoCategory', 'id', @PanelID, 1, 10, 1, 1, 1, 0, '', 1, 100, '[Unfiled], Hobby, Travel, Finances, Personal, Business, Restaurant, Health ', 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, LabelEdit, LabelList, TypeID, dbcolumn, dbcolumnread, maxlength, img, PanelID, PanelIndex, fpos, link, linklabel, linktarget, search, searchadv, searchlist, lookup, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'Complete', '', 'C.', 1, 'Complete', 'Complete', 100, 'checkb.gif', @PanelID, 1, 10, '', '', '', 1, 1, 1, 0, '', 1, 50, 1);

-- panel --
INSERT INTO EvoDico_Panel (FormID, UserID, TypeID, Label, Width, pPos)
  VALUES (@FormID, 1, 1, 'Notes', '100', 1);
SELECT @PanelID = SCOPE_IDENTITY();

-- fields --  
INSERT INTO EvoDico_Field (FormID, UserID, Label, LabelEdit, LabelList, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, link, linklabel, linktarget, search, searchadv, searchlist, height, width, Publish)
  VALUES (@FormID, 1, '', 'Notes', 'Notes', 6, 'notes', 'notes', 1000, 0, 0, 0, '', @PanelID, 1, 10, '', '', '', 0, 1, 0, 6, 100, 1);
  
  
  