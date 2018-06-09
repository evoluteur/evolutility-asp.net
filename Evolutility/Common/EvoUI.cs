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
using System.Text;
using System.Web;
//using System.Xml;

namespace Evolutility
{

	/* 
	This library is a dependency of : 
	 * Evolutility.UIServer 
	 * Evolutility.Wizard 
	*/

	static class EvoUI
	{

//### Constants ######################################################################################## 
#region "Constants"

		internal const string evoName = "Evolutility";  
		internal const string evoLink = "http://www.evolutility.org";
		internal const string CRE = "\n<!-- Evolutility 3.0 - www.evolutility.org - (c) 2009 Olivier Giulieri -->\n";

		internal const string JSscript = "\n<script type=\"text/javascript\">//<![CDATA[\n";
		internal const string JSscriptClose = "\n //]]></script>\n";

		internal const string tag_BR = "<br/>";

		internal const string tag_cTDcTRoTRoTD = "</td></tr><tr><td>";
		internal const string qChecked = "\" checked=\"checked";
		internal const string PixCheck = "check.gif"; 
		internal const string PixCheckCSS = "<span class=\"CheckBlack\"> </span>";
		internal const string PixCheckBlueCSS = "<span class=\"CheckBlue\"> </span>";
		//internal const string PixCheckGreenCSS = "<span class=\"CheckGreen\"> </span>"; 
		//internal const string PixCheckRedCSS = "<span class=\"CheckBlue\"> </span>"; 
		internal const string PixEndTag = "\" class=\"Icon\" alt=\"\"/>";

		internal const string PixComment = "<span class=\"Ico Comment\">&nbsp;</span>";
		internal const string PixCommentAdd = "<span class=\"Ico CommentAdd\">&nbsp;</span>";
		internal const string PixCommentUser = "<span class=\"Ico CommentUser\">&nbsp;</span>";
		private const string PixComment1 = "<span class=\"Ico Comment\" title=\"1\">&nbsp;</span>";
		private const string PixComments = "<span class=\"Ico Comments\" title=\"{0}\">&nbsp;</span>";

		internal const string PixAddRow = "<span class=\"AddRow\">&nbsp;</span>";
		internal const string PixDelRow = "<span class=\"DelRow\">&nbsp;</span>";

		internal const string FlagRequired = "<span class=\"Required\">*</span>";
		internal const string FlagPopup = "<span class=\"ExtWeb\">&nbsp;</span>";

		internal const string HTMLoptionZero = "<option value=\"0\"> - </option>";
		internal const string HTMLSpace = "&nbsp;";
		internal const string HTMLCheckBoxBegin = "<input type=\"checkbox\" name=\"";
		internal const string HTMLNameIdClass = " name=\"{0}\" id=\"{1}\" class=\"{2}\"";

		internal const string fNameLogin = "EvoLogin";
		internal const string fNamePassword = "EvoPassword";

		internal const string cssPanel = "Panel", cssPanelLabel = "PanelLabel";

		internal const string mNew = "new", mEdit = "edit", mView = "view", mSearch = "search";
		//internal const string mDelete = "del", mSelections = "sel", mSearchAdv = "searchp";
		//internal const string mAll = "all", mPrint = "print", mHelp = "help", mLogout = "logout";


		internal enum MsgType
		{
			Info, 
			Error,
			Warn,
			Del,
			x 
		}

#endregion

//### HTML simple elements ####################################################################################
#region "HTML simple elements"

		static internal string noBR(string myText)
		{
			return String.Format("<nobr>{0}</nobr>", myText);
		}

		static internal string HTMLFieldLabel(string fName, string fLabel)
		{
			return String.Format("<div class=\"FieldLabel\"><label for=\"{0}\">{1}</label></div>", fName, fLabel);
		}

		static internal string HTMLFieldLabelSpan(string fName, string fLabel)
		{
			return (new StringBuilder()).Append("<span class=\"FieldLabel\"><label for=\"").Append(fName).Append("\">").Append(fLabel).Append("</label></span>").ToString();
		}

		static internal string HTMLHelpTip(string id, string help)
		{
			return String.Format("<br/><span id=\"tip{0}\" class=\"HelpTip\">{1}</span>", id, help);
		}

		static internal string HTMLOption(string fieldValue, string fieldlabel, bool selected)
		{
			if (selected)
				return String.Format("<option value=\"{0}\" selected>{1}", fieldValue, fieldlabel);
			else
				return HTMLOption(fieldValue, fieldlabel);
		}
		static internal string HTMLOption(string fieldValue, string fieldlabel)
		{
			return string.Format("<option value=\"{0}\">{1}", fieldValue, fieldlabel);
		}

		static internal string HTMLLinkCSS(string URL, string label, string target, string Img)
		{
			bool popup = ! string.IsNullOrEmpty(target);
			bool useImg = ! string.IsNullOrEmpty(Img);
			StringBuilder HTML = new StringBuilder();

			HTML.Append("<a class=\"FieldLabel\" href=\"").Append(URL);
			if (popup)
				HTML.Append("\" target=\"").Append(target);
			HTML.Append("\">");
			if (useImg) 
				HTML.Append("<img src=\"").Append(Img).Append(PixEndTag).Append(label); 
			else
			{
				HTML.Append(label);
				if (popup)
					HTML.Append(FlagPopup);
			}
			HTML.Append("</a>");
			return HTML.ToString();
		}
		static internal string HTMLLink(string URL, string label, string target, string Img)
		{
			bool popup = ! string.IsNullOrEmpty(target);
			StringBuilder HTML = new StringBuilder();

			HTML.Append("<a href=\"").Append(URL);
			if (popup)
				HTML.Append("\" target=\"").Append(target); 
			HTML.Append("\">");
			if (! string.IsNullOrEmpty(Img))
				HTML.Append("<img src=\"").Append(Img).Append(PixEndTag);
			HTML.Append(label);
			if (popup)
				HTML.Append(FlagPopup);
			HTML.Append("</a>");
			return HTML.ToString();
		}
		static internal string HTMLLink(string URL, string label)
		{
			return string.Format("<a href=\"{0}\">{1}</a>", URL, label);
		}

		static internal string HTMLEmail(string email)
		{
			return string.Format("<a href=\"mailto:{0}\">{0}</a>", HttpUtility.HtmlEncode(email));
		}

		static internal string HTMLImg(string PixName)
		{
			return string.Format("<img src=\"{0}\" class=\"Icon FieldImg\" alt=\"\"/>", PixName);
		}
		static internal string HTMLImg(string PixName, string AltTag)
		{
			return string.Format("<img src=\"{0}\" class=\"Icon\" alt=\"{1}\"/>", PixName, AltTag);
		}

		static internal string HTMLImgCheckMark(bool IEbrowser, string PixPath)
		{
			if (IEbrowser)
				return EvoUI.PixCheckCSS;
			else
			{
				StringBuilder myHTML = new StringBuilder();
				myHTML.Append(EvoUI.HTMLSpace).Append(EvoUI.HTMLImg(PixPath + EvoUI.PixCheck));
				return myHTML.ToString();
			}
		}

		static internal string HTMLDiv(string divID, bool visible)
		{
			return string.Format("<div id=\"{0}\" {1}>", divID, StyleVisibleToggle(visible));
		}
		static internal string HTMLDivClosed(string divID, bool visible)
		{
			return string.Format("<div id=\"{0}\" {1}></div>\n", divID, StyleVisibleToggle(visible));
		}

		static internal string HTMLIcon(string filePath, string fileName)
		{
			return string.Format("<img src=\"{0}{1}\" class=\"Icon\" alt=\"\"/>", filePath, fileName);
		}
		static internal string HTMLIcon(string filePath, string fileName, string AltTag)
		{
			return string.Format("<img src=\"{0}{1}\" class=\"Icon\" alt=\"{0}\"/>", filePath, fileName, AltTag);
		}

#endregion

//### HTML input elements ############################################################################# 
#region "HTML input elements"

		static internal string HTMLInputText(string FieldName, string FieldValue, int maxLength)
		{
			return String.Format("<input type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{1}\" maxlength=\"{2}\" class=\"Field\">", FieldName, FieldValue, maxLength);
		}

		static internal string HTMLInputTextEmpty(string FieldName)
		{
			return string.Format("<input type=\"text\" name=\"{0}\" id=\"{0}\" class=\"Field\">", FieldName);
		}

		static internal string HTMLInputTextArea(string fName, int fRows)
		{
			StringBuilder b = new StringBuilder();
			b.AppendFormat("<textarea name=\"{0}\" id=\"{0}\" rows=\"{1}", fName, fRows);
			b.Append("\" cols=\"52\" class=\"Field\"></textarea>");
			return b.ToString();
		}
		//static internal string HTMLInputTextArea(string fName, int fRows, int fMaxLength, string fCSS)
		//{
		//    StringBuilder b = new StringBuilder();
		//    b.AppendFormat("<textarea name=\"{0}\" id=\"{0}\" class=\"{1}\" rows=\"{2}", fName, fCSS, fRows);
		//    if (fMaxLength > 0)
		//        b.Append("\" onKeyUp=\"EvoVal.checkMaxLen(this,").Append(fMaxLength).Append(")");
		//    b.Append("\" cols=\"52\"></textarea>");
		//    return b.ToString();
		//}

		static internal string HTMLInputButton(string name, string label, bool submit, string onclick)
		{
			StringBuilder zHTML = new StringBuilder();

			zHTML.Append("<input type=\"");
			if (submit)
				zHTML.Append("submit");
			else
				zHTML.Append("button");
			if (!string.IsNullOrEmpty(onclick))
				zHTML.Append("\" onclick=\"").Append(onclick);
			zHTML.Append("\" class=\"Button\" name=\"").Append(name).Append("\" value=\" ").Append(label).Append("\">");
			return zHTML.ToString();
		}

		static internal string HTMLInputHidden(string name, string value)
		{
			return string.Format("<input type=\"hidden\" name=\"{0}\" id=\"{0}\" value=\"{1}\">", name, value);
		}

		static internal string HTMLInputCheckBox(string name, string value, string label, bool selected, string id)
		{
			StringBuilder zHTML = new StringBuilder();

			{
				//zHTML.Append("<div class=\"FieldCheckBox\">");
				zHTML.Append(HTMLCheckBoxBegin).Append(name).Append("\" value=\"").Append(value);
				if (!string.IsNullOrEmpty(id))
					zHTML.Append("\" ID=\"").Append(id);
				if (selected)
					zHTML.Append(qChecked);
				zHTML.Append("\">");
				if (!string.IsNullOrEmpty(label))
				{
					zHTML.Append("<label for=\"");
					if (string.IsNullOrEmpty(id))
						zHTML.Append(name);
					else
						zHTML.Append(id);
					zHTML.Append("\"><small>").Append(label).Append("</small></label>");
				}
				//zHTML.Append("</div>");
			}
			return zHTML.ToString();
		}
		static internal string HTMLInputCheckBox(string nameAndID, string value, string label)
		{
			StringBuilder zHTML = new StringBuilder();

			zHTML.Append(HTMLCheckBoxBegin).Append(nameAndID).Append("\" id=\"").Append(nameAndID).Append("\" value=\"").Append(value).Append("\">");
			if (!string.IsNullOrEmpty(label))
				zHTML.Append("<label for=\"").Append(nameAndID).Append("\"><small>").Append(label).Append("</small></label>");
			return zHTML.ToString();
		}
		static internal string HTMLInputCheckBox(string nameAndID, string value, bool selected)
		{
			StringBuilder zHTML = new StringBuilder();

			zHTML.Append(HTMLCheckBoxBegin).Append(nameAndID).Append("\" id=\"").Append(nameAndID).Append("\" value=\"").Append(value);
			if (selected)
				zHTML.Append(qChecked); 
			zHTML.Append("\">");
			return zHTML.ToString();
		}

		static internal string HTMLInputRadio(string fName, string fValue, string fLabel, bool selected, string id)
		{
			StringBuilder myHTML = new StringBuilder();

			myHTML.AppendFormat("<label for=\"{0}\"><input ID=\"{0}\" name=\"{1}\" value=\"{2}", id, fName, fValue);
			if (selected)
				myHTML.Append(qChecked);
			myHTML.Append("\" type=\"radio\"><small>").Append(fLabel).Append("</small></label>&nbsp;");
			return myHTML.ToString();
		}
		
		static internal string HTMLInputDate(string fName, string fValue, string locale, string pixDir)
		{
			StringBuilder myHTML = new StringBuilder();

			myHTML.Append("<nobr><input type=\"text\" class=\"Field Field80\" size=\"15\" maxlength=\"22\"");
			myHTML.AppendFormat(" name=\"{0}\" id=\"{0}\" value=\"{1}", fName, fValue);
			myHTML.Append("\">&nbsp;<a href=\"javascript:ShowDatePicker('").Append(fName);
			if (locale.Equals("FR") || locale.Equals("IT"))
				myHTML.Append("', false, 'dmy', '/");
			else if (locale.Equals("DA"))
				myHTML.Append("', false, 'dmy', '-");
			myHTML.Append("');\" class=\"Ico Calendar\"></a></nobr>");
			return myHTML.ToString();
		}

#endregion

//### HTML custom elements ############################################################################# 
#region "HTML custom elements"

		static internal string TRcssEvenOrOdd(bool b)
		{
			if (b)
				return "<tr class=\"RowEven\">";
			else
				return "<tr class=\"RowOdd\">";
		}

		static internal string HTMLemptyRowEdit(int MaxLoop)
		{
			StringBuilder myHTML = new StringBuilder();

			myHTML.Append("<tr class=\"RowEven\" height=\"0\" style=\"height:0\" id=\"r0\">");
			for (int j = 0; j < MaxLoop; j++)
				myHTML.Append("<td></td>");
			myHTML.Append("</tr>");
			return myHTML.ToString();
		}

		static internal string HTMLtextMore(string myText, string myOptions)
		{ // has a copy in EvoLibDB to avoid dependency
			StringBuilder zHTML = new StringBuilder();
			//zHTML.Append("<span>").Append(myText);
			//string ID = System.Guid.NewGuid().ToString(); 
			//zHTML.Append("<span class=\"navp\"><a class=\"ico panelopen\" href=\"javascript:");
			//zHTML.AppendFormat("Evol.togglePanel('{0}',0)\" ID=\"{0}link\"></a></span>", ID); 
			//zHTML.Append(HTMLDiv(ID, false)).Append(myOptions).Append("</div></span>");  
			zHTML.Append(myText).Append("<div class=\"Foot\">").Append(myOptions).Append("</div>");
			return zHTML.ToString();
		}

		static internal string menuItem(string label, string css, bool enabled)
		{
			//HTML for a single menu item 
			if (enabled)
				return String.Format("<a href=\"#\" class=\"{0} act\">{1} </a>", css, label);
			else
				return String.Format("<a href=\"#\" class=\"{0}Z\">{1} </a>", css, label);
		}

		static internal string menuNav(int displayMode, bool navBefore, bool navAfter)
		{
			StringBuilder zHTML = new StringBuilder();
			string buffer = "<a href=\"javascript:EvPost('{0}{1}')\" class=\"Ico nav{1}\"></a>";
			string aNumString = (displayMode == 1) ? "30" : "20";

			if (navBefore)
				zHTML.Append("<span class=\"nav1\"></span><span class=\"nav2\"></span>");
			else
				zHTML.AppendFormat(buffer, aNumString, "1").AppendFormat(buffer, aNumString, "2");
			if (navAfter)
				zHTML.Append("<span class=\"nav3\"></span><span class=\"nav4\"></span>");
			else
				zHTML.AppendFormat(buffer, aNumString, "3").AppendFormat(buffer, aNumString, "4");
			return zHTML.ToString();
		}

		static internal string menuNavSep(bool IEbrowser)
		{
			if (IEbrowser) 
				return "<span class=\"nav9\"></span>"; 
			else 
				return "<span class=\"nav9ff\">&nbsp;</span>";
		}

		static internal string GetValFromCSVTuples(string myCSVTuples, string myKey)
		{
			//myCSVTuples= "day|24 hours,week|week,month|month,year|year" 
			string buffer = string.Empty;

			string [] NVpairs = myCSVTuples.Split(new char[] { ',' });
			for (int i = 0; i < NVpairs.Length; i++)
			{
				if(NVpairs[i].StartsWith(myKey))
				{
					buffer = NVpairs[i].Substring(myKey.Length + 1);
					break;
				}
			}
			return buffer;
		}

		static internal string HTMLlovEnum(string enumerationList, string FieldName, int ItemID, bool abbrev)
		{
			//make a query and returns the HTML for a lov 
			StringBuilder myHTML = new StringBuilder();
			int curID = 0;
			string currentEnum = null;
			string bufferID = null;
			string bufferValue = null;

			string[] LOVenumerations = enumerationList.Split(new char[] { ',' });
			int MaxLoop = LOVenumerations.Length;
			if (MaxLoop == 0)
			{
				//ErrorMsg += "XML Error: element 'field' of type 'lov' with no attribute 'dbtablelov' or 'lovenumeration'." 
				if (! string.IsNullOrEmpty(FieldName))
					myHTML.Append(" - No list available -");
			}
			else
			{
				for (int i = 0; i < MaxLoop; i++)
				{
					currentEnum = LOVenumerations[i];
					int j = currentEnum.IndexOf("|");
					if (j < 0)
					{
						bufferID = currentEnum;
						bufferValue = currentEnum;
					}
					else
					{
						bufferID = currentEnum.Substring(0, j);
						try
						{
							bufferValue = currentEnum.Substring(j+1);
						}
						catch
						{
							bufferValue = string.Empty;
						}
					}
					if (!(string.IsNullOrEmpty(FieldName) || abbrev))
					{
						myHTML.Append(HTMLInputCheckBox(FieldName, bufferID, HttpUtility.HtmlEncode(bufferValue), false, FieldName + i.ToString())).Append("&nbsp; ");
					}
					else
					{
						if (ItemID == 0)
						{
							myHTML.Append(HTMLOption(bufferID, HttpUtility.HtmlEncode(bufferValue)));
						}
						else
						{
							curID = Convert.ToInt32(bufferID);
							myHTML.Append(HTMLOption(curID.ToString(), HttpUtility.HtmlEncode(bufferValue), ItemID == curID));
						}
					}
				}
			}
			return myHTML.ToString();
		}

		static internal string HTMLbuttonTab(string Label, bool selected)
		{
			return String.Format("<a class=\"{1}\" href=\"#\"><nobr>{0}</nobr></a>", Label, (selected)?"TabSelected":"Tab");
		}

		static internal string HTMLLinkShowVanish(string divID, string linkLabel)
		{
			return string.Format("<a id=\"{0}link\" href=\"javascript:Evol.showMore('{0}',1)\">{1}</a>", divID, linkLabel);
		}

		static internal string HTMLCommentFlag(int nbCommentsRow)
		{
			switch (nbCommentsRow)
			{
				case 0:
					return string.Empty;
				case 1:
					return PixComment1;
				default:
					return String.Format(PixComments, nbCommentsRow);
			}
		}

		static internal string StyleVisibleToggle(bool Visible)
		{
			if (Visible)
				return " style=\"display:inline;\" ";
			else
				return " style=\"display:none;\" ";
		}

		static internal string Link4itemid(string OriginalURL, string ItemID)
		{
			return OriginalURL.Replace(EvoDB.p_itemid, ItemID);
		}

		static internal string HTMLtrColor(string aColor)
		{
			if (string.IsNullOrEmpty(aColor))
				return "<tr>";
			else
				return string.Format("<tr bgcolor=\"{0}\">", aColor);
		}

		static internal string HTMLPanelLabel(string PanelLabel, string panelID, string panelClassName, bool CollapsiblePanels)
		{
			if (string.IsNullOrEmpty(PanelLabel))
			{
				return string.Empty;
			}
			else
			{
				StringBuilder HTML = new StringBuilder();
				HTML.Append("<div class=\"").Append(panelClassName).Append("\">&nbsp;").Append(PanelLabel);
				if (CollapsiblePanels)
				{
					HTML.Append("<span class=\"navp\"><a class=\"Ico PanelClose\" href=\"javascript:");
					HTML.AppendFormat("Evol.togglePanel('{0}',-1)\" ID=\"{0}link\"></a></span>", panelID);
				}
				HTML.Append("</div>");
				return HTML.ToString();
			}
		}

		static internal string HTMLLinkEventRef(string EventParam, string Title)
		{
			return string.Format("<a href=\"javascript:EvPost('{0}')\">{1}</a>", EventParam, Title);
		}
		static internal string HTMLLinkEventRef(string EventParam, string Title, string pix)
		{
			return string.Format("<a href=\"javascript:EvPost('{0}')\" class=\"{2}\">{1}</a>", EventParam, Title, pix);
		}

		static internal string HTMLMessage(string message, MsgType icon)
		{
			StringBuilder myHTML = new StringBuilder();
			string css = "Msg"+icon.ToString("G");
			myHTML.Append("<table id=\"Msg\" class=\"Msg ").Append(css).Append("\"><tr><td>");
			if (icon != MsgType.x)
			{
				myHTML.Append("<div class=\"Ico ").Append(css).Append("\">&nbsp;</div></td><td width=\"100%\">");
			}
			myHTML.Append(message.Replace("\n", tag_BR)); //leave < and > 
			myHTML.Append("</td></tr></table>");
			return myHTML.ToString();
		}

		static internal string HTMLSearchOperators(string sEquals, string sStart, string sContain, string sFinish)
		{
			StringBuilder sb = new StringBuilder(); 

			sb.Append("_c\">");
			sb.Append("<option value=\"eq\">").Append(sEquals);
			sb.Append("<option value=\"sw\" selected>").Append(sStart);
			sb.Append("<option value=\"ct\">").Append(sContain);
			sb.Append("<option value=\"fw\">").Append(sFinish);
			return sb.ToString();
		}

		static internal string Signature()
		{
			//This signature is "invisible" to users and must not be removed from the source code nor the compiled version of Evolutility 
			return string.Format("<div style=\"display:none;\">Powered by {0}</div>", HTMLLink(evoLink, evoName));
		}
		
		static internal string FormLogin(string labelLogin, string labelPassword, string labelButton, string DefaultLogin)
		{
			StringBuilder myHTML = new StringBuilder();

			myHTML.Append("<p align=\"Center\"><br/><table class=\"FormLogin\" border=\"0\" width=\"62%\"><tr><td><div class=\"Key\"></div></td><td width=\"90%\">");
			myHTML.Append(HTMLFieldLabel(fNameLogin, labelLogin));
			myHTML.Append(HTMLInputText(fNameLogin, DefaultLogin, 50));
			myHTML.Append("</td></tr><tr><td></td><td>");
			myHTML.Append(HTMLFieldLabel(fNamePassword, labelPassword));
			myHTML.Append("<input type=\"password\" class=\"Field\" name=\"").Append(fNamePassword).Append("\" maxlength=\"50\">");
			myHTML.Append("</td></tr><tr><td class=\"PanelLabel\"></td><td class=\"PanelLabel\">");
			myHTML.Append(HTMLInputButton("Login", labelButton, true, ""));
			myHTML.Append("</td></tr></table><br/></p>");
			myHTML.Append(JSscript).Append("document.getElementById('").Append(fNameLogin).Append("').focus();").Append(JSscriptClose);
			return myHTML.ToString();
		}
	
#endregion

//### EvoDico Designer ############################################################################################ 
#region "EvoDico Designer"

		internal enum DesType
		{
			frm, // form
			pnl, // panel
			src, // search & list
			fld  // field
		}

		static internal string LinkDesigner(DesType DesignerType, int ItemID, string ItemLabel, string pathDesigner)
		{
			return string.Format(" <a href=\"Javascript:EvoDico.edit('{0}','{2}ed_{0}.aspx?ID={1}','{3}')\" class=\"des-{0}\"></a>", DesignerType.ToString("G"), ItemID, pathDesigner, HttpUtility.HtmlEncode(ItemLabel));
		}

		static internal string menuItemCustomize(int DisplayMode, string CustomizeLabel)
		{
			return EvoUI.HTMLLinkEventRef("c:" + DisplayMode.ToString(), CustomizeLabel + EvoUI.HTMLSpace, "customize act");
		}

#endregion
		
//### Misc. ############################################################################################ 
#region "Misc."

		static internal string JSIncludeEvoScripts(string path, string language)
		{
			StringBuilder sbJS = new StringBuilder();
			sbJS.Append("<script type=\"text/javascript\" src=\"").Append(path).Append("JS/EvolUtility.js\"></script>\n");
			sbJS.Append("<script type=\"text/javascript\" src=\"").AppendFormat("{0}JS/lang/{1}.js", path, language).Append("\"></script>\n");
			sbJS.Append("<script type=\"text/javascript\" defer=\"defer\" src=\"").Append(path).Append("JS/EvolDate.js\"></script>\n"); 
			return sbJS.ToString();
		}

		//static internal string JSIncludeScriptDate(string path)
		//{
		//    return string.Format("<script src=\"{0}JS/EvolDate.js\" defer=\"defer\" type=\"text/javascript\"></script>", path); 
		//}

		//static internal string JSIncludeScript(string path)
		//{
		//    StringBuilder sbJS = new StringBuilder();
		//    sbJS.Append("<script type=\"text/javascript\" src=\"").Append(path).Append("\"></script>\n");
		//    return sbJS.ToString();
		//}

		static internal int ModeRequestInt(string strMode)
		{
			if (string.IsNullOrEmpty(strMode))
				return 0;
			else
			{
				switch (strMode.ToLower())
				{
					case mView:
						return 0;
					case mEdit:
						return 1;
					case mNew:
						return 12;
					case "selections":
						return 60;
					case mSearch:
						return 3;
					case "searchadv":
						return 4;
					case "login":
						return 50;
					case "export":
						return 70;
					case "list":
						return 110;
					//Case "lastlist" 
					// Return 105 
					default:
						return 0;
				}
			}
		}

		static internal string inQuote(string myString)
		{
			return string.Format("\"{0}\"", myString.Replace("\"", "\"\""));
		}

		static internal string GetAppSetting(string key)
		{
			if (System.Configuration.ConfigurationManager.AppSettings[key] != null)
				return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
			else
				return string.Empty;
		}

#endregion
	
	}

}
