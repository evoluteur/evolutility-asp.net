//	GERMAN translation from Joachim Seidel

//	Copyright (c) 2003-2009 Olivier Giulieri - olivier@evolutility.org 

//	This file is part of Evolutility CRUD Framework.
//	Source link <http://www.evolutility.org/download/download.aspx>

//	Evolutility is free software: you can redistribute it and/or modify
//	it under the terms of the GNU Affero General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.

//	Evolutility is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//	GNU Affero General Public License for more details.

//	You should have received a copy of the GNU Affero General Public License
//	along with Evolutility. If not, see <http://www.gnu.org/licenses/>.


using System;
using System.Collections.Generic;
using System.Text;

namespace Evolutility
{
	static partial class EvoLang // GERMAN - Translation from Joachim Seidel 
	{
 
		static internal void SetLocale_DE(string LanguageKey)
		{
			if (_LocaleCode != "DE")
			{
				_LocaleCode = "DE";
				_LocaleEN = "German";
				_Locale = "Deutsch";

				Welcome = "Willkommen {0}"; //{0}=login 

				entity = "Objekt";
				entities = "Objekte";
				AllEntities = "Alle {0}"; // {0}=entities 

				InsertEntity = "neues {0}."; // {0}=entity 
				ModifyEntity = "{0} ändern."; // {0}=entity 
				DownloadEntity = "Download {0}"; // {0}=entity 
				NoEntity = "Keine Objekte gefunden."; // not {0}=entity b/c of panel details 

				// --- export --- 
				ExportEntity = "Exportiere {0}"; // {0}=entity 
				ExportHeader = "Kopf";
				ExportSeparator = "Separator";
				ExportFirstLine = "Erste Zeile enthält Spaltennamen";
				ExportFormat = "Export Format";
				ExportFields = "Spalten für den Export";
				IDkey = "ID (Primary Key)";
				AllFields = "Alle Spalten anzeigen";
				ExportFormats = "Comma separated (CSV, TXT, XLS...)-HTML-SQL Insert Statements (SQL)-Tab separated values (TXT)-XML";

				// --- errors & warnings --- 
				err_NoPermission = "Es ist Ihnen nicht erlaubt ";
				err_NoDataDisp = "Keine Daten zum anzeigen.";
				err_NoData = "Keine Daten vorhanden.";
				err_NoQuery = "Datenbankabfrage kann nicht ausgeführt werden.";
				err_Update = "Kann {0} nicht speichern #{1}."; // {0}=entity {1}=ID 
				err_Delete = "Kann {0} nicht löschen #{1}."; // {0}=entity {1}=ID 
				MHValidValue = "{0} muss einen korrekten Wert besitzen."; //{0}=field 
				NA = "N/A";
				NoUpload = "Kann Datei nicht hochladen.";
				NoUpload2 = "Nur GIF, JPG und PNG sind erlaubte Bildformate.";

				// --- status --- 
				NewSave = "{0} neu gespeichert um {1}."; // {0}=entity {1}=now 
				NoUpdate = "Keine Aktualisierung notwendig.";
				DeleteOK = "Datensatz Nr.{0} gelöscht um {1:t}."; // {0}=ID {1}=time 
				Updated = "{0} aktualisiert um {1:t}."; // {0}=entity {1}=time 
				DetailsUpdate = "Details aktualisiert.";

				// --- login --- 
				PleaseLogin = "Bitte einloggen.";
				Logout = "Logout";
				Login = "Login";
				LoginB = "Login";
				Password = "Passwort";
				InvalidLogin = "Ungültiger Benutzer/Passwort.";
				InvalidLogin2 = "Bitte versuchen Sie es erneut.";
				//Remember = "Remember me" 

				// --- grid --- 
				AddRow = "Zeile hinzu";
				DelRow = "Zeile löschen";
				Customize = "Anpassen";

				// --- Search & LOVs ---
				wPix = " mit Bild";
				wDoc = " mit Dateianhang";
				wComments = "Mit Benutzer Kommentaren";
				yes = "Ja";
				no = "Nein";
				any = "Einige";
				anyof = "Einige von";
				PubMine = "Alle öffentlichen und meine";
				//MyEntities = "My ~ENTITIES~" 

				// --- toolbar --- 
				View = "Ansicht";
				Edit = "Bearbeiten";
				// Login = "Login" 
				New = "Neu";
				NewItem = "Neues Objekt";
				NewUpload = "Neuer Upload";
				Search = "Suche";
				AdvSearch = "erweiterte Suche";
				NewSearch = "Neue Search";
				Selections = "Abfragen";
				Selection = "Abfrage";
				Export = "Export";
				SearchRes = "Suchergebnis";
				Delete = "Löschen";
				ListAll = "Alle Anzeigen";
				Print = "Drucken";
				DeleteEntity = "Möchten Sie {0} löschen?"; // {0}=entity 
				Back2SearchResults = "Zurück zu den Suchergebnissen";

				// --- navigation --- 
				pFirst = "erster";
				pPrev = "zurück";
				pNext = "vor";
				pLast = "letzter";

				sBefore = "Davor";
				sAfter = "Danach";

				sDateRangeLast = " in den letzten ";
				sDateRangeNext = " in den nächsten ";
				sDateRangeWithin = " im Zeitraum ";
				sDateRangeAny = " jederzeit ";
				sDateRange = "Tag|24 Stunden,Woche|1 Woche,Monat|1 Monat,Jahr|1 Jahr";
				sEquals = "gleich";

				// --- search form dropdown ---
				sStart = "Fängt an mit";
				sContain = "Enthält";
				sFinish = "Endet mit";
				sIsNull = "Ist leer";
				sIsNotNull = "Ist nicht leer";
				qEquals = " ist gleich ";
				qStart = " fängt an mit ";
				qInList = " in Liste ";
				qNot = " nicht ";
				qWith = " mit ";

				// --- search result conditions --- 
				lEquals = " = \"{0}\""; //{0}= FieldValue 
				lStart = " fängt an mit \"{0}\""; //{0}= FieldValue 
				lFinish = " endet mit \"{0}\""; //{0}= FieldValue 
				lContain = " enthält \"{0}\""; //{0}= FieldValue 
				lIsNull = "\"{0}\" ist leer"; //{0}= FieldValue 
				lIsNotNull = "\"{0}\" ist nicht leer"; //{0}= FieldValue 

				opAnd = " und ";
				opOr = " oder ";

				cAt = "Ab";
				sOn = "An";
				sOf = " von ";
				Checked = "Markiert";
				Save = "speichern";
				SaveAdd = "Speichern und neuen hinzufügen";
				Cancel = "Abbrechen";

				// --- user comments --- 
				ucPostedOn = "Kommentar vom {0:t}."; //{0}=time 
				ucPost = "Kommentar abgeben";
				ucAdd = "Eigene Kommentare hinzufügen";
				ucNoComments = "Keine Benutzerkommentare für {0} vorhanden."; //{0}=entity 
				ucNb = "{0} Benutzerkommentar(e)) {1}."; //{0}=NB {1}=entity 
				ucMissing = "Einige Kommentare fehlen.";
				ucFrom = "Von ";
				ucOn = " an ";

			}
		} 
	
	}
}
