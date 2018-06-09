//	ROMANIAN translation from Cosmin Munteanu

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
	static partial class EvoLang // ROMANIAN - Translation from Cosmin Munteanu
	{
		static internal void SetLocale_RO(string LanguageKey)
		{
			if (_LocaleCode != "RO")
			{
				_LocaleCode = "RO";
				_LocaleEN = "Romanian"; // do not translate this line - this is the english name for the language
				_Locale = "Romanian"; // no babelfish

				Welcome = "Bine ai venit {0}"; //{0}=login 

				entity = "element";
				entities = "elemente";
				AllEntities = "Toate {0}"; // {0}=entities 

				InsertEntity = "inserează un nou {0}."; // {0}=entity 
				ModifyEntity = "modifică {0}."; // {0}=entity 
				DownloadEntity = "Descarcă {0}"; // {0}=entity 
				NoEntity = "Nici un element nu a fost găsit."; // not {0}=entity b/c of panel details 

				// --- export --- 
				ExportEntity = "Exportă acest {0}"; // {0}=entity 
				ExportHeader = "Titlu";
				ExportSeparator = "Separator";
				ExportFirstLine = "Prima linie pentru numele câmpurilor";
				ExportFormat = "Formatul de export";
				ExportFields = "Câmpuri incluse în export";
				IDkey = "ID (cheie primară)";
				ExportFormats = "Separate prin virgulă (CSV, TXT, XLS...)-HTML-SQL script (SQL)-Valori separate prin tabulatori (TXT)-XML";
				AllFields = "Toate câmpuri"; // no babelfish

				// --- errors & warnings --- 
				err_NoPermission = "Nu ai permisiunea să ";
				err_NoDataDisp = "Nici un fel de date pentru afişare.";
				err_NoData = "Nici un fel de date disponibile.";
				err_NoQuery = "Interogarea bazei de date nu poate fi executată.";
				err_Update = "{0} #{1} nu poate fi actualizat."; // {0}=entity {1}=ID 
				err_Delete = "{0} #{1} nu poate fi şters."; // {0}=entity {1}=ID 
				MHValidValue = "{0} trebuie să aibă o valoare validă."; //{0}=field 
				NA = "N/A";
				NoUpload = "Fişierul nu poate fi încărcat.";
				NoUpload2 = "Numai GIF, JPG, and PNG formaturi de imagine sunt admise.";

				// --- status --- 
				NewSave = "Un nou {0} salvat la {1}."; // {0}=entity {1}=now 
				NoUpdate = "Actualizarea nu este necesară.";
				DeleteOK = "Înregistrarea #{0} a fost ştearsă la {1:t}."; // {0}=ID {1}=time 
				Updated = "{0} a fost actualizat la {1:t}."; // {0}=entity {1}=time 
				DetailsUpdate = "Detaliile au fost actualizate.";

				// --- login --- 
				PleaseLogin = "Please log in.";
				Logout = "Ieşire";
				Login = "Nume utilizator";
				LoginB = "Intrare";
				Password = "Parolă";
				InvalidLogin = "Nume utilizator/Parolă nu sunt valide.";
				InvalidLogin2 = "Încercţi încă o dată vă rog.";
				//Remember = "Remember me" 

				// --- grid --- 
				AddRow = "Adaugă linie";
				DelRow = "Şterge linie";
				Customize = "Personalizează";

				// --- Search & LOVs ---
				wPix = " cu imagine";
				wDoc = " cu fişier ataşat";
				wComments = "Cu comentariile utilizatorului";
				yes = "Da";
				no = "Nu";
				any = "Oricare";
				anyof = "Oricare dintre";
				PubMine = "Toate fişierele publice sau personale";
				//MyEntities = "My ~ENTITIES~" 

				// --- toolbar --- 
				View = "Vedere";
				Edit = "Editare";
				// Login = "Login" 
				New = "Nou";
				NewItem = "Articol nou";
				NewUpload = "Fişier nou";
				Search = "Căutare";
				AdvSearch = "Căutare avansată";
				NewSearch = "Căutare nouă";
				Selections = "Selecţii";
				Selection = "Selecţie";
				Export = "Exportă";
				SearchRes = "Rezultatul căutării";
				Delete = "Şterge";
				ListAll = "Listează toate";
				Print = "Tipăreşte";
				DeleteEntity = "Şterg acest {0}?"; // {0}=entity 
				Back2SearchResults = "Înapoi la rezultatele căutării";

				// --- navigation --- 
				pFirst = "Primul";
				pPrev = "Anterior";
				pNext = "Urmatorul";
				pLast = "Ultimul";

				sBefore = "Înainte";
				sAfter = "După";

				sDateRangeLast = " în ultimele ";
				sDateRangeNext = " în urmatoarele ";
				sDateRangeWithin = " în intervalul ";
				sDateRangeAny = " oricând ";
				sDateRange = "zi|24 de ore,săptămână|1 săptămână,lună|1 lună,an|1 an";
				sEquals = "Egal";

				// --- search form dropdown --- 
				sStart = "Începe cu";
				sContain = "Conţine";
				sFinish = "Se termină cu";
				sIsNull = "Is empty"; // no babelfish
				sIsNotNull = "Is not empty"; // no babelfish
				qEquals = " egal ";
				qStart = " începe cu ";
				qInList = " în listă ";
				qNot = " nu ";
				qWith = " cu ";

				// --- search result conditions --- 
				lEquals = " = \"{0}\""; //{0}= FieldValue 
				lStart = " începe cu \"{0}\""; //{0}= FieldValue 
				lFinish = " se termină cu \"{0}\""; //{0}= FieldValue 
				lContain = " conţine \"{0}\""; //{0}= FieldValue 
				lIsNull = "\"{0}\" is empty"; //{0}= FieldValue // no babelfish
				lIsNotNull = "\"{0}\" is not empty"; //{0}= FieldValue  // no babelfish

				opAnd = " şi ";
				opOr = " sau ";

				cAt = "La";
				sOn = "Pe";
				sOf = " al ";

				Checked = "Verificat";
				Save = "Salvează";
				SaveAdd = "Salvează şi adaugă încă unul";
				Cancel = "Anulează";

				// --- user comments --- 
				ucPostedOn = "Comentarii postate la {0:t}."; //{0}=time 
				ucPost = "Postează comentariile tale";
				ucAdd = "Adaugă propriile tale comentarii";
				ucNoComments = "Încă nici un comentariu pentru acest {0}."; //{0}=entity 
				ucNb = "{0} comentarii pentru acest {1}."; //{0}=NB {1}=entity 
				ucMissing = "Câteva comentarii lipsesc.";
				ucFrom = "De la ";
				ucOn = " pe ";

			}
		}
	}
}
