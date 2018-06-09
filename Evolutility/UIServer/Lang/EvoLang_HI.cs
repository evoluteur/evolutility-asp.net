//	HINDI - Translation from P.K.Agarwal 

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
	static partial class EvoLang // Hindi - Translation from P.K.Agarwal -  http://www.nrldc.org/
	{
		static internal void SetLocale_HI(string LanguageKey)
		{
			if (_LocaleCode != "HI")
			{
				_LocaleCode = "HI";
				_LocaleEN = "Hindi"; // do not translate this line - this is the english name for the language
				_Locale = "हिन्दी";

				Welcome = "स्वागतम {0}"; // {0}=login 

				entity = "इकाई";
                entities = "इकाईयाँ";
                AllEntities = "सभी {0}"; // {0}=entities 

                InsertEntity = "नया {0} जोडें ."; // {0}=entity 
				ModifyEntity = "{0} वदलें."; // {0}=entity 
				DownloadEntity = "डाउनलोड {0}"; // {0}=entity 
				NoEntity = "आइटम नहीं मिला"; // {0}=entity 

				// --- export --- 
				ExportEntity = "{0} निर्यात करें"; // {0}=entity 
                ExportHeader = "हैडर";
                ExportSeparator = "विभाजक";
                ExportFirstLine = "फील्ड के लिये पहली पंक्ति";
                ExportFormat = "प्रारूप निर्यात करें";
                ExportFields = "फील्ड निर्यात करें";
                IDkey = "ID (मुख्य कुंजी)";
                AllFields = "सभी फील्ड दिखायें ";
                ExportFormats = "अल्पविराम पृथक (CSV, TXT, XLS...)-HTML-इन्सर्ट स्टेटमेंन्ट (SQL)-टैव सैपरेटेड वैल्यू (TXT)-XML";

				// --- errors & warnings --- 
                err_NoPermission = "तुम्हें करने की अनुमति नहीं है। ";
                err_NoDataDisp = "कोई डेटा प्रदर्शित करने के लिए नहीं है।";
                err_NoData = "कोई डेटा उपलब्ध नहीं है।";
                err_NoQuery = "डेटाबेस क्वेरी निष्पादित करना सम्भव नहीं";
				err_Update = "{0} #{1} को अपडेट करना सम्भव नहीं।"; // {0}=entity {1}=ID 
                err_Delete = "{0} #{1} को मिटाना सम्भव नहीं"; // {0}=entity {1}=ID 
                MHValidValue = "{0} वैध होना चाहिये।"; // {0}=field 
                NA = "लागू नहीं";
                NoUpload = "फाइल अपलोड नहीं कर सकते हैं।";
                NoUpload2 = "केवल GIF, JPG, और PNG छवि प्रारूप की अनुमति है!";

				// --- status --- 
				NewSave = "नया {0}, {1}पर बचाया"; // {0}=entity {1}=now 
                NoUpdate = "कोई अपडेट आवश्यक नहीं। ";
                DeleteOK = "अभिलेख #{0}, {1:t}पर मिटाया।"; // {0}=ID {1}=time 
                Updated = "{0} ,{1:t}अपडेट किया।";
                DetailsUpdate = "विवरण अपडेट किया।";

				// --- login --- 
                PleaseLogin = "कृपया लॉग करें।";
                Logout = "लॉगआउट";
                Login = "लॉग इन";
                LoginB = "लॉग इन";
                Password = "पासवर्ड";
                InvalidLogin = "अमान्य लॉग इन/पासवर्ड।";
                InvalidLogin2 = "कृपया पुन: प्रयास करें।";
				//Remember = "" 
                AddRow = "पंक्ति जोड़ें";
                DelRow = "पंक्ति मिटायें";
                Customize = "अनुकूलित करें।";

                wPix = " तस्वीर के साथ";
                wDoc = " संलग्नक के साथ";
                wComments = "प्रयोक्ता टिप्पणी के साथ";
				yes = "हाँ";
				no = "नहीं";
				any = "कोई भी";
                anyof = "किसी का ";
                PubMine = "सभी सार्वजनिक और मेरा";
				//MyEntities = "Mes fiches ~ENTITIES~" 

				// --- toolbar --- 
				View = "दृश्य";
                Edit = "संपादन";
				New = "नया";
				NewItem = "नई ईकाई";
				NewUpload = "नया अपलोड";
				Search = "खोज";
                AdvSearch = "उन्नत खोज";
                NewSearch = "नई खोज";
                Selections = "चयन";
                Selection = "चयन";
                Export = "निर्यात";
                SearchRes = "खोज परिणाम";
                Delete = "मिटाना";
                ListAll = "सूची की सभी";
                Print = "प्रिंट";
				DeleteEntity = "क्या {0} को मिटायें?"; //{0}=entity 
                Back2SearchResults = "खोज परिणामों पर वापस";

				// --- navigation --- 
                pFirst = "प्रथम";
                pPrev = "पिछला";
                pNext = "अगला";
                pLast = "अंतिम";

                sBefore = "से पहले";
                sAfter = "के बाद";

                sDateRangeLast = " में पिछले  ";
                sDateRangeNext = " में अगले  ";
                sDateRangeWithin = " के अंदर ";
                sDateRangeAny = " किसी भी समय  ";
                sDateRange = "दिन|24 घंटे ,सप्ताह|1 घंटे ,मास|1 मास,वर्ष|1 वर्ष"; //"day|24 hours,week|1 week,month|1 month,year|1 year" 
                sEquals = "बराबरी";

				// --- search form dropdown ---
                sStart = "से आरंभ";
                sContain = "समाविष्ट";
				sFinish = "कै साथ अन्त";
                sIsNull = "क्या खाली है";
                sIsNotNull = "क्या खाली नहीं है";
				qEquals = " के बराबर ";
                qStart = " से आरंभ ";
                qInList = " सूची में ";
                qNot = " नहीं ";
                qWith = " के साथ ";

				// --- search result conditions --- 
				lEquals = " = \"{0}\""; //{0}= FieldValue 
                lStart = " से आरंभ \"{0}\""; //{0}= FieldValue 
                lFinish = " कै साथ अन्त \"{0}\""; //{0}= FieldValue 
                lContain = " समाविष्ट \"{0}\""; //{0}= FieldValue 
                lIsNull = "\"{0}\" खाली है";
                lIsNotNull = "\"{0}\" खाली नहीं है";

				opAnd = " तथा ";
				opOr = " या ";

				cAt = "पर";
				sOn = "ऊपर";
				sOf = " का ";
                Checked = "जांचा हुआ";
                Save = "सहेजें";
                SaveAdd = "सहेजें और एक अन्य जोड़ें";
                Cancel = "रद्द करें";

				// --- user comments --- 
                ucPostedOn = "टिप्पणियाँ {0:t} पर पोस्ट की गईं."; //{0}=time 
                ucPost = "अपनी टिप्पणी पोस्ट करें";
                ucAdd = "अपनी खुद की टिप्पणी जोड़ें";
                ucNoComments = " {0} के लिये उपयोगकर्ता की कोई टिप्पणियां अभी तक नहीं।."; //{0}=entity 
                ucNb = "{1} के लिये उपयोगकर्ता की कोई {0} टिप्पणियां।"; //{0}=NB {1}=entity 
                ucMissing = "कुछ टिप्पणियाँ नीं हैं।";
                ucFrom = "से ";
                ucOn = " पर ";

			}
		}
	}
}
