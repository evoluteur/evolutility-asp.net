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
	static partial class EvoLang
	{

//### Variables - Language ### 
#region "Language Variables"

		static internal string _Locale, _LocaleEN;
		static internal string _LocaleCode;
        
        static internal bool R2L; // written from right to left
		static internal string Welcome;

		static internal string entity;
		static internal string entities;
		static internal string allEntities;
		static internal string AllEntities;

		static internal string InsertEntity;
		static internal string ModifyEntity;
		static internal string DownloadEntity;
		static internal string NoEntity;

		// --- export --- 
		static internal string ExportEntity;
		static internal string ExportHeader;
		static internal string ExportSeparator;
		static internal string ExportFirstLine;
		static internal string ExportFormat;
		static internal string ExportFields;
		static internal string IDkey;
		static internal string AllFields;
		static internal string ExportFormats;

		static internal string MHValidValue;
		static internal string NA;
		static internal string NoUpload;
		static internal string NoUpload2;
		static internal string Customize;
		
		// --- login --- 
		static internal string PleaseLogin;
		static internal string Logout;
		static internal string Login;
		static internal string LoginB;
		static internal string Password;
		static internal string InvalidLogin;
		static internal string InvalidLogin2;
		//friend Remember As String = "Remember me" 
		static internal string AddRow;
		static internal string DelRow;

		static internal string wPix;
		static internal string wDoc;
		static internal string wComments;
		static internal string yes;
		static internal string no;
		static internal string any;
		static internal string anyof;
		static internal string PubMine;
		//friend MyEntities As String = "My ~ENTITIES~" 

		// --- toolbar --- 
		static internal string View;
		static internal string Edit;
		// friend Login As String = "Login" 
		static internal string New;
		static internal string NewItem;
		static internal string NewUpload;
		static internal string Search;
		static internal string AdvSearch;
		static internal string NewSearch;
		static internal string Selection;
		static internal string Selections;
		static internal string Export;
		static internal string Charts;
		static internal string SearchRes;
		static internal string MassUpdate;
		static internal string Delete;
		static internal string ListAll;
		static internal string Print;
		static internal string DeleteEntity;
		static internal string Back2SearchResults;
		
		// --- navigation --- 
		static internal string pFirst, pPrev, pNext, pLast;
		static internal string sBefore, sAfter;

		static internal string sDateRange, sDateRangeLast, sDateRangeNext, sDateRangeWithin, sDateRangeAny;

		// --- search form dropdown --- 
		static internal string sEquals, sStart, sContain, sFinish;
		static internal string sIsNull, sIsNotNull;
		static internal string qEquals;
		static internal string qStart;
		static internal string qInList;
		static internal string qNot;
		static internal string qWith;

		// --- search result conditions --- 
		static internal string lEquals, lStart, lFinish, lContain;
		static internal string lIsNull, lIsNotNull;

		static internal string opAnd, opOr;

		static internal string cAt;
		static internal string sOn;
		static internal string sOf;
		static internal string Checked;
		static internal string Save;
		static internal string SaveAdd;
		static internal string Cancel;
		static internal string NoX;
		static internal string NoChange;
		static internal string NoGraph;
		static internal string chart_A_per_B;

		// --- user comments --- 
		static internal string ucPostedOn;
		static internal string ucPost;
		static internal string ucAdd;
		static internal string ucNoComments;
		static internal string ucNb;
		static internal string ucMissing;
		static internal string ucFrom;
		static internal string ucOn;

		// --- errors & warnings --- 
		static internal string err_NoPermission;
		static internal string err_NoDataDisp;
		static internal string err_NoData;
		static internal string err_NoQuery;
		static internal string err_Update;
		static internal string err_Delete;
		// ---  errors & warnings in ENGLISH (not translated yet) --- 
		internal const string err_DB = "Database error.";
		internal const string err_XML = "Invalid XML file.";
		internal const string err_DefDico = "Invalid application definition."; 
		internal const string err_NoDBColumn = "Invalid column name \"{0}\".";
		internal const string err_NoQuery4Req = "No query ID specified in request.";
		internal const string err_BadQuery = "Invalid query ID.";
		internal const string err_NoDataInTable = " Database table \"{0}\" is empty.";
		internal const string warn_NoAccessSelections = "Selections are not allowed for this object.";
		internal const string warn_NoQueryDef = "No queries defined.";

		// --- status --- 
		static internal string NewSave;
		static internal string NoUpdate;
		static internal string DeleteOK;
		static internal string Updated;
		static internal string DetailsUpdated;
		static internal string MassUpdated;

#endregion

		static internal bool LoadLanguage(string LanguageKey)
		{
            R2L = false;
			// Language-Key values follow the ISO 639-1 language code
			// http://en.wikipedia.org/wiki/List_of_ISO_639-1_codes
			switch (LanguageKey)
			{
				case "EN":// English
					SetLocale_EN(LanguageKey);
					break;
				case "ZH":// Chinese (simplified)
				case "CHS":// just in case not ISO
				case "CH":
					SetLocale_CHS(LanguageKey);
					break;
				case "ES":// Spanish
				case "SP":// just in case not ISO
					SetLocale_ES(LanguageKey);
					break;
                case "FR":// French
					SetLocale_FR(LanguageKey);
					break;
				case "HI":// Hindi
					SetLocale_HI(LanguageKey);
					break;
				case "PT":// Portuguese
					SetLocale_PT(LanguageKey);
					break;
				case "RO":// Romanian
					SetLocale_RO(LanguageKey);
					break;
				case "DE":// German
					SetLocale_DE(LanguageKey);
					break;
				case "DA":// Danish
				case "DK":// just in case not ISO
					SetLocale_DA(LanguageKey);
					break;
				case "IT":// Italian
					SetLocale_IT(LanguageKey);
					break;
				case "JP":// Japanese
					SetLocale_JP(LanguageKey);
					break;
                case "FA":// Persian (Farci)
                case "PES":// Iranian
                    SetLocale_FA(LanguageKey);
                    R2L = true;
                    break;
				case "TR":// Turkish
					SetLocale_TR(LanguageKey);
					break;
				case "CA":// Catalan
					SetLocale_CA(LanguageKey);
					break;
			}
			if (_LocaleCode != LanguageKey)
			{
				SetLocale_EN(LanguageKey);
				return false;
			}
			return true;
		}

		static internal void SetLocale_EN(string LanguageKey)
		{
			if (_LocaleCode != "EN")
			{
				_LocaleCode = "EN";
				_LocaleEN = "English";
				_Locale = _LocaleEN;

				Welcome = "Welcome {0}"; //{0}=login 

				entity = "item";
				entities = "items";
				AllEntities = "All {0}"; // {0}=entities 

				InsertEntity = "insert new {0}."; // {0}=entity 
				ModifyEntity = "modify {0}."; // {0}=entity 
				DownloadEntity = "Download {0}"; // {0}=entity 
				NoEntity = "No item found."; // not {0}=entity b/c of panel details 

				// --- export --- 
				ExportEntity = "Export this {0}"; // {0}=entity 
				ExportHeader = "Header";
				ExportSeparator = "Separator";
				ExportFirstLine = "First line for field names";
				ExportFormat = "Export Format";
				ExportFields = "Fields to include in the export";
				IDkey = "ID (Primary Key)";
				AllFields = "Show all fields";
				ExportFormats = "Comma separated (CSV, TXT, XLS...)-HTML-SQL Insert Statements (SQL)-Tab separated values (TXT)-XML-Javascript Object Notation (JSON)";

				// --- errors & warnings --- 
				err_NoPermission = "You are not allowed to ";
				err_NoDataDisp = "No Data to display.";
				err_NoData = "No data available.";
				err_NoQuery = "Cannot execute Database query.";
				err_Update = "Cannot update {0} #{1}."; // {0}=entity {1}=ID 
				err_Delete = "Cannot delete {0} #{1}."; // {0}=entity {1}=ID 
				MHValidValue = "{0} must have a valid value."; //{0}=field 
				NA = "N/A";
				NoUpload = "Cannot upload file.";
				NoUpload2 = "Only GIF, JPG, and PNG image formats are allowed.";

				// --- status --- 
				NewSave = "New {0} saved at {1}."; // {0}=entity {1}=now 
				NoUpdate = "No update necessary.";
				DeleteOK = "Record #{0} deleted at {1:t}."; // {0}=ID {1}=time 
				Updated = "{0} updated at {1:t}."; // {0}=entity {1}=time 
				DetailsUpdated = "Details updated.";
				MassUpdated = "{0} {1} updated at {2:t}."; // {0}=nb rec {1}=entities  {2}=time 

				// --- login --- 
				PleaseLogin = "Please log in.";
				Logout = "Logout";
				Login = "Login";
				LoginB = "Login";
				Password = "Password";
				InvalidLogin = "Invalid Login/Password.";
				InvalidLogin2 = "Please, try again.";
				//Remember = "Remember me" 

				// --- grid --- 
				AddRow = "Add row";
				DelRow = "Delete row";
				Customize = "Customize";

				// --- Search & LOVs ---
				wPix = " with picture";
				wDoc = " with attachment";
				wComments = "With User comments";
				yes = "Yes";
				no = "No";
				any = "Any";
				anyof = "Any of";
				PubMine = "All public and mine";
				//MyEntities = "My ~ENTITIES~" 

				// --- toolbar --- 
				View = "View";
				Edit = "Edit";
				// Login = "Login" 
				New = "New";
				NewItem = "New Item";
				NewUpload = "New Upload";
				Search = "Search";
				AdvSearch = "Advanced Search";
				NewSearch = "New Search";
				Selections = "Selections";
				Selection = "Selection";
				Export = "Export";
				Charts = "Charts";
				SearchRes = "Search Result";
				MassUpdate = "Mass Update";
				Delete = "Delete";
				ListAll = "List All";
				Print = "Print";
				DeleteEntity = "Delete this {0}?"; // {0}=entity 
				Back2SearchResults = "Back to search results";

				// --- navigation --- 
				pFirst = "First";
				pPrev = "Previous";
				pNext = "Next";
				pLast = "Last";

				// --- search form dropdown ---
				sBefore = "Before";
				sAfter = "After";
				sEquals = "Equals";
				sStart = "Starts with";
				sContain = "Contains";
				sFinish = "Finishes with";
				sIsNull = "Is empty";
				sIsNotNull = "Is not empty";
				sDateRangeLast = " in the last ";
				sDateRangeNext = " in the next ";
				sDateRangeWithin = " within ";
				sDateRangeAny = " any time ";
				sDateRange = "day|24 hours,week|1 week,month|1 month,year|1 year";

				qEquals = " equals ";
				qStart = " starts with ";
				qInList = " in list ";
				qNot = " not ";
				qWith = " with ";

				// --- search result conditions --- 
				lEquals = " = \"{0}\""; //{0}= FieldValue 
				lStart = " starts with \"{0}\""; //{0}= FieldValue 
				lFinish = " finishes with \"{0}\""; //{0}= FieldValue 
				lContain = " contains \"{0}\""; //{0}= FieldValue 
				lIsNull = "\"{0}\" is empty"; //{0}= FieldValue 
				lIsNotNull = "\"{0}\" is not empty"; //{0}= FieldValue 

				opAnd = " and ";
				opOr = " or ";

				cAt = "At";
				sOn = "On";
				sOf = " of ";
				Checked = "Checked";
				Save = "Save";
				SaveAdd = "Save and Add Another";
				Cancel = "Cancel";
				NoX = "No {0}";
				NoChange = "No change";
				NoGraph = "No graphs available.";
				chart_A_per_B = "{0} per {1}";
				// --- user comments --- 
				ucPostedOn = "Comments posted on {0:t}."; //{0}=time 
				ucPost = "Post your comments";
				ucAdd = "Add your own comments";
				ucNoComments = "No user comments for this {0} yet."; //{0}=entity 
				ucNb = "{0} user comments for this {1}."; //{0}=NB {1}=entity 
				ucMissing = "Some comments are missing.";
				ucFrom = "From ";
				ucOn = " on ";

			}
		} 
	}
}
