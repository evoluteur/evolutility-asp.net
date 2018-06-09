/*    www.evolutility.org - (c) 2009 Olivier Giulieri     */
/*    EvoDico 2.5 - Evolutility database dictionary    */
/*    last updated  02/14/2009   */ 
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

/****** Object:  StoredProcedure [EvoDico_Form_Clone]  ************************************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [EvoDico_Form_Clone] (
    @FormID int,
	@UserID int 
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

    -- =============================================
    -- CLONE form
    -- =============================================
    
    DECLARE @nFormID int;   
     
    insert into EvoDico_Form (Title, publish, icon, entity, entities, Help, script, 
		dbtable, dbwhere, dborder, dbcolumnpk, dbcolumnlead, dbwherelock, dbcolumnicon, 
		dbtablecomments, CommentsID,
		spPaging, spLogin, spGet, spDelete, Description)
	select Title, 0, icon, entity, entities, Help, script, 
		dbtable, dbwhere, dborder, dbcolumnpk, dbcolumnlead, dbwherelock, dbcolumnicon, 
		dbtablecomments, CommentsID,
		spPaging, spLogin, spGet, spDelete, 'Cloned from '+ Title + '(ID ' + CONVERT(nvarchar, ID, 0) + ')'
    from EvoDico_Form where id=@FormID  
     
    SET @nFormID=(SELECT @@IDENTITY);

    update EvoDico_Form set Title=Title + ' (' + cast(@nFormID as nvarchar) + ')' WHERE ID=@nFormID and len(rtrim(title))<90;

    -- =============================================
    -- CLONE panels & fields
    -- =============================================
	
	DECLARE @PanelID int;
	DECLARE @nPanelID int;
	
	DECLARE c1 CURSOR READ_ONLY
	FOR
		SELECT ID
		FROM EvoDico_Panel 
		where formid=@FormID 

	OPEN c1

	FETCH NEXT FROM c1 INTO @PanelID

	WHILE @@FETCH_STATUS = 0
	BEGIN

		INSERT INTO EvoDico_Panel (FormID, Label, ppos, Width, Optional, cssclass, Summary)
		select @nFormID, Label, ppos, Width, Optional, cssclass, Summary 
		from EvoDico_Panel where formid=@FormID AND ID=@PanelID;

		SET @nPanelID=(SELECT @@IDENTITY);
		
		INSERT INTO EvoDico_Field (formID, panelid, Label, labeledit, labellist, 
			TypeID, maxlength, readonly, required, 
			fpos, Width, Height, format, cssclass, searchlist, Optional, 
			search, searchadv, jsvalidation, jsdependency, dependency, 
			[min], [max], regexp, help, link, linklabel, linktarget, 
			dbcolumn, dbcolumnread, dbcolumnicon, dbtablelov, dbcolumnreadlov, dbwherelov, dborderlov)
		select @nFormID, @nPanelID, Label, labeledit, labellist, 
			TypeID, maxlength, readonly, required, fpos, 
			Width, Height, format, cssclass, searchlist, Optional, 
			search, searchadv, jsvalidation, jsdependency, dependency, 
			[min], [max], regexp, help, link, linklabel, linktarget, 
			dbcolumn, dbcolumnread, dbcolumnicon, dbtablelov, dbcolumnreadlov, dbwherelov, dborderlov
		from EvoDico_Field where formid=@FormID AND PanelID=@PanelID;
		
		FETCH NEXT FROM c1 INTO @PanelID;

	END

	CLOSE c1
	DEALLOCATE c1
 
    print @nFormID;

END

GO





