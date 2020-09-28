//	DANISH translation from Henrik Holm 

//	Copyright (c) 2003-2013 Olivier Giulieri - olivier@evolutility.org 

//	This file is part of Evolutility CRUD Framework.
//	Source link <http://www.evolutility.org/download/download.aspx>

//	Evolutility is open source software: you can redistribute it and/or modify
//	it under the terms of the GNU Affero General Public License as published by
//	the open source software Foundation, either version 3 of the License, or
//	(at your option) any later version.

//	Evolutility is distributed WITHOUT ANY WARRANTY;
//	without even the implied warranty of MERCHANTABILITY
//	or FITNESS FOR A PARTICULAR PURPOSE.
//	See the GNU Affero General Public License for more details.

//	You should have received a copy of the GNU Affero General Public License
//	along with Evolutility. If not, see <http://www.fsf.org/licensing/licenses/agpl-3.0.html>.

//  Commercial license may be purchased at www.evolutility.org <http://www.evolutility.org/product/Purchase.aspx>.


using System;
using System.Collections.Generic;
using System.Text;

namespace Evolutility
{
	static partial class EvoLang // DANISH - Translation from Henrik Holm 
	{
		static internal void SetLocale_DA(string LanguageKey)
		{
			if (_LocaleCode != "DA")
			{
				_LocaleCode = "DA";
				_LocaleEN = "Danish"; // do not translate this line - this is the english name for the language
				_Locale = "Dansk"; 

				Welcome = "Velkommen {0}"; //{0}=login 

				entity = "post";
				entities = "poster";
				AllEntities = "Alle {0}"; // {0}=entities

				InsertEntity = "Tilføj ny {0}."; // {0}=entity 
				ModifyEntity = "Revider {0}."; // {0}=entity 
				DownloadEntity = "Download {0}"; // {0}=entity 
				NoEntity = "Ingen poster fundet."; // not {0}=entity b/c of panel details 

				// --- export --- 
				ExportEntity = "Eksporter denne {0}"; // {0}=entity 
				ExportHeader = "Header";
				ExportSeparator = "Skilletegn";
				ExportFirstLine = "Første linie til felt navne";
				ExportFormat = "Eksport Format";
				ExportFields = "Felter der skal medtages i Eksporten";
				IDkey = "ID (Primær Nøgle)";
				AllFields = "Vis alle områder"; // googletranslate
				ExportFormats = "komma separeret (CSV, TXT, XLS...)-HTML-SQL Insert Statements (SQL)-Tab separerede værdier (TXT)-XML-JSON";

				// --- errors & warnings --- 
				err_NoPermission = "Du har ikke tilladelse at at ";
				err_NoDataDisp = "Igen data at vise.";
				err_NoData = "Ingen data tilgængelig.";
				err_NoQuery = "Kan ikke udføre Database forespørgsel.";
				err_Update = "Kan ikke opdatere {0} #{1}."; // {0}=entity {1}=ID 
				err_Delete = "Kan ikke slette {0} #{1}."; // {0}=entity {1}=ID 
				MHValidValue = "{0} skal have en valid værdi."; //{0}=field 
				NA = "N/A";
				NoUpload = "Filen kan ikke uploades.";
				NoUpload2 = "Kun GIF, JPG, og PNG billede formater er tilladt.";

				// --- status --- 
				NewSave = "Ny {0} gemt den {1}."; // {0}=entity {1}=now 
				NoUpdate = "Opdatering ikke nødvendig.";
				DeleteOK = "Post #{0} slettet kl. {1:t}."; // {0}=ID {1}=time 
				Updated = "{0} opdateret kl. {1:t}."; // {0}=entity {1}=time 
				DetailsUpdated = "Detaljer opdateret.";
				MassUpdated = "{0} {1} updated at {2:t}."; // {0}=nb rec {1}=entities  {2}=time // googletranslate

				// --- login --- 
				PleaseLogin = "Venligst log ind.";
				Logout = "Log ud";
				Login = "Log ind";
				LoginB = "Log ind";
				Password = "Adgangskode";
				InvalidLogin = "Forkert Log ind/Adgangskode.";
				InvalidLogin2 = "Venligst, prøv igen.";
				//Remember = "Remember me" 

				// --- grid --- 
				AddRow = "Tilføj række";
				DelRow = "Slet række";
				Customize = "Modificer";

				// --- Search & LOVs ---
				wPix = " med billede";
				wDoc = " med tilføjelse";
				wComments = "Med Bruger kommentarer";
				yes = "Ja";
				no = "Nej";
				any = "Nogen";
				anyof = "Nogen af";
				PubMine = "Alle offentlige og mine";
				//MyEntities = "My ~ENTITIES~" 
				
				// --- toolbar --- 
				View = "Vis";
				Edit = "Rediger";
				// Login = "Login" 
				New = "Ny";
				NewItem = "Ny Post";
				NewUpload = "Ny Upload";
				Search = "Søg";
				AdvSearch = "Udvidet søgning"; 
				NewSearch = "Ny Søgning";
				Selections = "Valg";
				Selection = "Valg";
				Export = "Eksport";
				SearchRes = "Søge Resultat";
				Charts = "Diagrammer";// googletranslate
				MassUpdate = "Masse Update"; //"Mass Update" googletranslate
				Delete = "Slet";
				ListAll = "List Alle";
				Print = "Udskriv";
				DeleteEntity = "Slet denne {0}?"; // {0}=entity 
				Back2SearchResults = "Tilbage til søge resultat";
				
				// --- navigation --- 
				pFirst = "Første";
				pPrev = "Forrige";
				pNext = "Næste";
				pLast = "Sidste";

				sBefore = "Før";
				sAfter = "Efter";

				sDateRangeLast = " i de sidste ";
				sDateRangeNext = " i de næste ";
				sDateRangeWithin = " mellem ";
				sDateRangeAny = " any time ";
				sDateRange = "dag|24 timer,uge|1 uge,måned|1 måned,år|1 år";
				sEquals = "Lig med";

				// --- search form dropdown --- 
				sStart = "Starter med";
				sContain = "Indeholder";
				sFinish = "Slutter med";
				sIsNull = "Er tom"; // googletranslate
				sIsNotNull = "Er ikke tom"; // googletranslate
				qEquals = " lig med ";
				qStart = " starter med ";
				qInList = " i listen ";
				qNot = " ikke ";
				qWith = " med ";

				// --- search result conditions --- 
				lEquals = " = \"{0}\""; //{0}= FieldValue 
				lStart = " starter med \"{0}\""; //{0}= FieldValue 
				lFinish = " slutter med \"{0}\""; //{0}= FieldValue 
				lContain = " indeholder \"{0}\""; //{0}= FieldValue 
				lIsNull = "\"{0}\" er tom"; //{0}= FieldValue - googletranslate
				lIsNotNull = "\"{0}\" er ikke tom"; //{0}= FieldValue  - googletranslate

				opAnd = " og ";
				opOr = " eller "; 
				 
				cAt = "At";
				sOn = "På";
				sOf = " af ";
				Checked = "Markeret";
				Save = "Gem";
				SaveAdd = "Gem og Tilføj en ny";
				Cancel = "Annuller";
				NoX = "Ingen {0}"; // googletranslate
				NoChange = "Ingen ændring"; //"No Change" googletranslate
				NoGraph = "Ingen grafer tilgængelig."; // "No graphs available." googletranslate
				chart_A_per_B = "{0} / {1}"; // to be reviewed

				// --- user comments --- 
				ucPostedOn = "Kommentarer opdateret {0:t}."; //{0}=time 
				ucPost = "Indtast dine kommentarer";
				ucAdd = "Tilføj dine egne kommentarer";
				ucNoComments = "Ingen bruger kommentarer for denne {0} endnu."; //{0}=entity 
				ucNb = "{0} bruger kommentarer for denne {1}."; //{0}=NB {1}=entity 
				ucMissing = "Der mangler nogle kommentarer.";
				ucFrom = "Fra ";
				ucOn = " på ";

			}
		}
	}
}
