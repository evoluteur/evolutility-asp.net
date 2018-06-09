//	ITALIAN - Translation from Pier Giuseppe Meo

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
	static partial class EvoLang // ITALIAN - Translation from Pier Giuseppe Meo
	{
		static internal void SetLocale_IT(string LanguageKey)
		{
			if (_LocaleCode != "IT")
			{ 	
				_LocaleCode = "IT";
				_LocaleEN = "Italian"; // do not translate this line - this is the english name for the language
				_Locale = "Italiano";

				Welcome = "Benvenuti {0}"; //{0}=login 

				entity = "campo";
				entities = "campi";
				AllEntities = "Tutti {0}"; // {0}=entities 


				InsertEntity = "Aggiungi nuovo {0}."; // {0}=entity 
				ModifyEntity = "modifica {0}."; // {0}=entity 
				DownloadEntity = "Download {0}"; // {0}=entity 
				NoEntity = "Nessun campo trovato."; // not {0}=entity b/c of panel details 

				// --- export --- 
				ExportEntity = "Esporta questo {0}"; // {0}=entity 
				ExportHeader = "Testata";
				ExportSeparator = "Separatore";
				ExportFirstLine = "Prima linea con nomi dei campi";
				ExportFormat = "Formato di esportazione";
				ExportFields = "campi da includere nella esportazione";
				IDkey = "ID (Chiave principale)";
				AllFields = "Tutti i campi";
				ExportFormats = "Separati da virgola (CSV, TXT, XLS...)-HTML-SQL Insert Statement (SQL)-Tab separati da tabulazioni (TXT)-XML-JSON";

				// --- errors & warnings --- 
				err_NoPermission = "Non hai i permessi per ";
				err_NoDataDisp = "Nessun dato da visualizzare.";
				err_NoData = "Nessun dato presente.";
				err_NoQuery = "Non posso eseguire la query.";
				err_Update = "Non posso modificare {0} #{1}."; // {0}=entity {1}=ID 
				err_Delete = "Non posso cancellare {0} #{1}."; // {0}=entity {1}=ID 
				MHValidValue = "{0} deve avere un valore valido."; //{0}=field 
				NA = "N/A";
				NoUpload = "Impossibile caricare il file.";
				NoUpload2 = "Solo immagini di tipo  GIF, JPG, and PNG sono consentite.";
				
				// --- status --- 
				NewSave = "Nuovo {0} salvato alle {1}."; // {0}=entity {1}=now 
				NoUpdate = "Nessuna modifica necessaria.";
				DeleteOK = "Record #{0} cancellato alle {1:t}."; // {0}=ID {1}=time 
				Updated = "{0} Modificato alle {1:t}."; // {0}=entity {1}=time 
				DetailsUpdated = "Righe modificate.";
				MassUpdated = "{0} {1} updated at {2:t}."; // {0}=nb rec {1}=entities  {2}=time // googletranslate

				// --- login --- 
				PleaseLogin = "Autenticarsi.";
				Logout = "Logout";
				Login = "Login";
				LoginB = "Login";
				Password = "Password";
				InvalidLogin = "Login/Password errata.";
				InvalidLogin2 = "Riprovare";
				//Remember = "Remember me" 	

				// --- grid --- 
				AddRow = "Aggiungi riga";
				DelRow = "Cancella riga";
				Customize = "Personalizza";	

				// --- Search & LOVs ---
				wPix = " con immagine";
				wDoc = " con allegato";
				wComments = "con annotazioni";
				yes = "Si";
				no = "No";
				any = "Qualsiasi";
				anyof = "Qualsiasi di";
				PubMine = "Tutti i comuni ed i miei";
				//MyEntities = "My ~ENTITIES~" 

				// --- toolbar --- 		
				View = "Visualizza";
				Edit = "Modifica";
				// Login = "Login" 
				New = "Nuovo";
				NewItem = "Nuovo campo";
				NewUpload = "Nuovo Upload";
				Search = "Ricerca";
				AdvSearch = "Ricerca avanzata";
				NewSearch = "Nuova ricerca";
				Selections = "Selezioni";
				Selection = "Selezione";
				Export = "Esporta";
				SearchRes = "Risultati ricerca";
				Charts = "Grafici";// googletranslate
				MassUpdate = "Mass Update"; //"Mass Update" googletranslate
				Delete = "Cancella";
				ListAll = "Elenca tutti";
				Print = "Stampa";
				DeleteEntity = "Cancello {0}?"; // {0}=entity 
				Back2SearchResults = "Ritorna ai risultati di ricerca";

				// --- navigation --- 
				pFirst = "Primo";
				pPrev = "Precedente";
				pNext = "Successivo";
				pLast = "Ultimo";

				sBefore = "Prima";
				sAfter = "Dopo";
				
				sDateRangeLast = " Negli ultimi "; 
				sDateRangeNext = " nei prossimi ";
				sDateRangeWithin = " entro ";
				sDateRangeAny = " qualsiasi ";
				sDateRange = "giorno|24 ore,settimana|1 settimana,mese|1 mese,anno|1 anno";
				sEquals = "Uguale";
				
				// --- search form dropdown --- 
				sStart = "Inizia con";
				sContain = "Contiene";
				sFinish = "Finisce con";
				sIsNull = "E vuoto";
				sIsNotNull = "Non è vuoto";
				qEquals = " uguale ";
				qStart = " inizia con ";
				qInList = " in elenco ";
				qNot = " non ";
				qWith = " con ";

				// --- search result conditions --- 
				lEquals = " = \"{0}\""; //{0}= FieldValue 
				lStart = " inizia con \"{0}\""; //{0}= FieldValue 
				lFinish = " finisce con \"{0}\""; //{0}= FieldValue 
				lContain = " contiene \"{0}\""; //{0}= FieldValue 
				lIsNull = "\"{0}\" non è vuoto"; //{0}= FieldValue 
				lIsNotNull = "\"{0}\" è vuoto"; //{0}= FieldValue 
				opAnd = " e ";
				opOr = " o ";
				cAt = "Al";
				sOn = "O";
				sOf = " di ";

				Checked = "Spuntato";
				Save = "Salva";
				SaveAdd = "Salva ed aggiungi altro";
				Cancel = "Cancella";
				NoX = "Nessun {0}"; // googletranslate
				NoChange = "Nessun cambiamento"; //"No Change" googletranslate
				NoGraph = "Nessun grafico disponibile."; // "No graphs available." googletranslate
				chart_A_per_B = "{0} / {1}"; // to be reviewed

				// --- user comments --- 
				ucPostedOn = "Note inserite il {0:t}."; //{0}=time 
				ucPost = "Registra il commento";
				ucAdd = "Aggiungi commento";
				ucNoComments = "Non vi sono commenti per {0}."; //{0}=entity 
				ucNb = "{0} commenti epr {1}."; //{0}=NB {1}=entity 
				ucMissing = "Alcuni commenti sono assenti.";
				ucFrom = "Da ";
				ucOn = " on ";

			}
		}
	}
}
