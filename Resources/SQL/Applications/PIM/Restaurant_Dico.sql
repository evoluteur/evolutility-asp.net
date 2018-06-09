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
/*  Metadata for :  Restaurants */

DECLARE @FormID int;
DECLARE @PanelID int;

-- form --
INSERT INTO EvoDico_Form (UserID, Title, icon, entity, entities, dbtable, dbwhere, dborder, dbcolumnlead, Publish, [Description], sppaging, splogin)
  VALUES (1, 'Restaurants', 'resto.gif', 'restaurant', 'restaurants', 'EVOL_Restaurant', '', 't.name', 'name', 1, 'Sample application: Restaurants list.', 'EvoSP_PagedItem @SQLselect, @SQLtable, @SQLfrom, @SQLwhere, @SQLorderby, @SQLpk, @pageid, @pagesize', 'EvoSP_Login @login, @password');
SELECT @FormID = SCOPE_IDENTITY();

-- panel --
INSERT INTO EvoDico_Panel (FormID, UserID, TypeID, Label, Width, pPos)
  VALUES (@FormID, 1, 1, 'Name', '60', 1);
SELECT @PanelID = SCOPE_IDENTITY();

-- fields --
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, PanelID, PanelIndex, fpos, search, searchadv, searchlist, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'Name', 5, 'name', 'name', 50, 0, 1, @PanelID, 1, 10, 1, 1, 1, 'FieldMain', 1, 100, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, dbtablelov, dborderlov, [Readonly], required, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, options, Publish)
  VALUES (@FormID, 1, 'Food', 4, 'CategoryID', 'CategoryName', 100, 'EVOL_Restaurant_Category', 'id', 0, 1, @PanelID, 1, 10, 1, 1, 1, 0, '', 1, 40, '[Unfiled], French, Vietnamese, Chinese, Fusion, Japanese, Thai, Mexican, Mediterranean, American, Indian, Korean, Italian, Spanish', 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, dbtablelov, dborderlov, [Readonly], required, PanelID, PanelIndex, fpos, search, searchadv, searchlist, height, width, help, options, Publish)
  VALUES (@FormID, 1, 'Rating', 4, 'RatingID', 'RatingName', 100, 'EVOL_Restaurant_rating', 'id', 0, 0, @PanelID, 1, 10, 1, 1, 1, 1, 30, '', 'NR, ,*, **, ***, ****, *****', 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, LabelEdit, LabelList, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, PanelID, PanelIndex, fpos,search, searchadv, searchlist, height, width, Publish)
  VALUES (@FormID, 1, 'Web site', 'Web', 'Web', 7, 'url', 'url', 200, 0, 0, 1, @PanelID, 1, 10, 0, 1, 0, 1, 70, 1);


-- panel --   
INSERT INTO EvoDico_Panel (FormID, UserID, TypeID, Label, Width, pPos)
  VALUES (@FormID, 1, 1, 'Contact Info', '40', 2);
SELECT @PanelID = SCOPE_IDENTITY();

-- fields --
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'Phone', 5, 'phone', 'phone', 16, 0, 0, @PanelID, 1, 10, 1, 1, 1, 0, '', 1, 100, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'Phone 2', 5, 'phone2', 'phone2', 16, 0, 0, 0, '', @PanelID, 1, 10, 0, 1, 0, 0, '', 1, 50, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'Fax', 5, 'Fax', 'Fax', 16, 0, 0, 0, '', @PanelID, 1, 10, 0, 1, 0, 0, '', 1, 50, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'email', 3, 'email', 'email', 100, 0, 0, 1, '', @PanelID, 1, 10, 0, 1, 0, 0, '', 1, 70, 1);

-- panel --   
INSERT INTO EvoDico_Panel (FormID, UserID, TypeID, Label, Width, pPos)
  VALUES (@FormID, 1, 1, 'Notes', '60', 3);
SELECT @PanelID = SCOPE_IDENTITY();

-- fields --
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'Comments', 6, 'comments', 'comments', 300, 0, 0, @PanelID, 1, 10, 0, 1, 0, 0, '', 2, 100, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, LabelEdit, LabelList, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, link, linklabel, linktarget, search, searchadv, searchlist, lookup, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'Favorite Dishes', '', '', 6, 'favorite', 'favorite', 300, 0, 0, 1, '', @PanelID, 1, 10, '', '', '', 0, 1, 0, 0, '', 2, 100, 1);

-- panel --   
INSERT INTO EvoDico_Panel (FormID, UserID, TypeID, Label, Width, pPos)
  VALUES (@FormID, 1, 1, 'Address', '40', 4);
SELECT @PanelID = SCOPE_IDENTITY();

-- fields --
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, help, options, Publish)
  VALUES (@FormID, 1, 'Address', 5, 'Address1', 'Address1', 100, 0, 0, @PanelID, 1, 10, 0, 1, 0, 0, '', 1, 100, '', '', 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, LabelEdit, LabelList, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], [required],  PanelID, PanelIndex, fpos, search, searchadv, searchlist, height, width, Publish)
  VALUES (@FormID, 1, '', '', 'Address 2', 5, 'Address2', 'Address2', 100, 0, 0, @PanelID, 1, 10, 0, 1, 0, 1, 100, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, PanelID, PanelIndex, fpos, search, searchadv, searchlist, height, width, Publish)
  VALUES (@FormID, 1, 'City', 5, 'City', 'City', 0, 0, 0, @PanelID, 1, 10, 1, 1, 1, 1, 60, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly], required, optional, format, PanelID, PanelIndex, fpos, search, searchadv, searchlist, lookup, cssclass, height, width, Publish)
  VALUES (@FormID, 1, 'State', 5, 'State', 'State', 0, 0, 0, 0, '', @PanelID, 1, 10, 0, 1, 0, 0, '', 1, 20, 1);
INSERT INTO EvoDico_Field (FormID, UserID, Label, TypeID, dbcolumn, dbcolumnread, maxlength, [Readonly],  PanelID, PanelIndex, fpos, search, searchadv, searchlist, height, width, Publish)
  VALUES (@FormID, 1, 'Zip', 5, 'Zip', 'Zip', 12, 0, @PanelID, 1, 10, 0, 1, 0, 1, 20, 1);  
  

