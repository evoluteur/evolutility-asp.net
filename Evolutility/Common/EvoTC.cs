//	Copyright (c) 2003-2011 Olivier Giulieri - olivier@evolutility.org 

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
using System.Text;
using System.Web;  

namespace Evolutility
{
	// ==================   Date formats, conversions and VB legacy   ==================   
	/* 
	This library is a dependency of : 
	 * Evolutility.DataServer 
	 * Evolutility.UIServer 
	 * Evolutility.Wizard 
	*/

	static class EvoTC
	{

//### Constants ######################################################################################## 
#region "Constants"

		static internal string DateFormatSD = "{0:d}";
		static internal string DateFormatST = "{0:t}";
		static internal string DateFormatSDT = "{0:d} {0:t}"; //"{0:G}";

		static internal string f_0_1 = "{0}{1}"; 
		static internal string f_0_1_2 = "{0}{1}{2}"; 

		static internal string fsp_0_1 = "{0} {1}"; 
		static internal string fsp_0_1_2 = "{0} {1} {2}"; 

#endregion

//### Dates ############################################################################################ 
#region "Dates"

		static internal void LoadFormats(string LanguageKey)
		{	
			/// <summary>Set default formats depending on locale.</summary> 
			DateFormatST = "{0:t}";
			switch (LanguageKey)
			{ 
				case "EN": // English
				case "ZH": // Chinese
					DateFormatSD = "{0:d}";
					DateFormatSDT = "{0:d} {0:t}"; //"{0:G}"; 
					break;
				case "DA": // Danish
					DateFormatSD = "{0:dd-MM-yyyy}";
					DateFormatSDT = "{0:dd-MM-yyyy} {0:t}";
				//    DateFormatSD = "{0:dd/MM-yyyy}";
				//    DateFormatSDT = "{0:dd/MM-yyyy} {0:t}";
					break;
                case "FA":
                    DateFormatSD = "{0:yy/MM/dd}";
                    DateFormatSDT = "{0:yy/MM/dd} {0:t}";
                    break;
				default: // All other languages
					DateFormatSD = "{0:dd/MM/yyyy}";
					DateFormatSDT = "{0:dd/MM/yyyy} {0:t}";
					break;
			}
		}

		static internal string DefaultDateFormat(string fType)
		{
			/// <summary>Returns DefaultDateFormat for fieldtype (date, time, datetime).</summary> 
			switch (fType)
			{
				case "date":
					return DateFormatSD;
				case "datetime":
					return DateFormatSDT;
				case "time":
					return DateFormatST;
			}
			return DateFormatSD;
		}

		static internal string TextNow()
		{
			return DateTime.Now.ToString(); //DateFormatST
		}

		static internal string TextNowTime()
		{
			return string.Format(DateFormatST, DateTime.Now);
		}

		static internal string formatedDateTime(System.DateTime aDate)
		{
			return string.Format(DateFormatSDT, aDate);
		}

		static internal string HTMLDateFormated(string fieldType, string fieldValue, string fieldFormat)
		{
			if (string.IsNullOrEmpty(fieldFormat))
				return string.Format(DefaultDateFormat(fieldType), String2DateTime(fieldValue));
			else
				return string.Format(fieldFormat, String2DateTime(fieldValue));
		}

#endregion

//### Conversions ###################################################################################### 
#region "Conversions"

		static internal string ToUpperLowers(string myString)
		{
			switch (myString.Length)
			{
				case 0:
					return string.Empty;
				case 1:
					return myString.ToUpper();
				default:
					return myString.Substring(0, 1).ToUpper() + myString.Substring(1).ToLower();
			}
		}

		static internal string Text2HTML(string myText)
		{
			if (string.IsNullOrEmpty(myText))
				return string.Empty;
			else 
				return HttpUtility.HtmlEncode(myText);
		}

		static internal string Text2HTMLwBR(string myText)
		{
			if (string.IsNullOrEmpty(myText))
				return string.Empty;
			else
				return HttpUtility.HtmlEncode(myText).Replace("\n", "<br/>"); // EvoUI.BR_tag
		}

		static internal string HTML2SQL(string myHTML)
		{
			if (myHTML.IndexOf("&") > -1)
				return System.Web.HttpUtility.HtmlDecode(myHTML);
			else
				return myHTML;
		}

		static internal int String2Int(string myString)
		{
			int i;
			if (Int32.TryParse(myString, out i))
				return i;
			else
				return 0;
		}

		static internal decimal String2Dec(string myString)
		{
			decimal dec;
			if (decimal.TryParse(myString, out dec))
				return dec;
			else
				return 0;
		}

		static internal DateTime String2DateTime(string myString)
		{
			DateTime d;
			if (DateTime.TryParse(myString, out d))
				return d;
			else
				return DateTime.Now; //BUG BUG
		}
		
		static internal int Bool2Int(bool myBool)
		{
			if (myBool)
				return 1;
			else
				return 0;
		}	
		
		static internal bool isInteger(string myString)
		{
			int result;
			return int.TryParse(myString, out result);
		}
		
		static internal bool isDecimal(string myString)
		{
			decimal result;
			return decimal.TryParse(myString, out result);
		}

		static internal bool isDate(string strDate)
		{
			DateTime d;
			return DateTime.TryParse(strDate, out d);
		}

		static internal string CondiConcat(string original, string newItem, string separator)
		{
			if (string.IsNullOrEmpty(original))
				return newItem;
			else
				return string.Format(f_0_1_2, original, separator, newItem);
		} 

#endregion

//### VB.net legacy ####################################################################################
#region "VB.net legacy"

		static internal string Right(string param, int length)
		{
			return param.Substring(param.Length - length, length);
		}

		static internal string StrVal(string s)
		{
			if (string.IsNullOrEmpty(s))
				return "0";
			else
				return String2Int(s).ToString();
		}

#endregion
	
	}

}