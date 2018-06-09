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
using System.Text;
using System.Web;

namespace Evolutility
{
	// ==================   HTML for elements UI   ==================   
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
		internal const string CRE = "\n<!-- Evolutility 4.1 - www.evolutility.org - (c) 2013 Olivier Giulieri -->\n";

		internal const string JSscript = "\n<script type=\"text/javascript\">//<![CDATA[\n";
		internal const string JSscriptClose = "\n //]]></script>\n";

		internal const string tag_BR = "<br/>";
		internal const string clearer = "<div class=\"clear\"></div>";
		internal const string tag_cTDcTRoTRoTD = "</td></tr><tr><td>";
		internal const string qChecked = "\" checked=\"checked";
		internal const string PixCheck = "check.gif";
		internal const string HTMLPixCheckCSS = "<span class=\"CheckBlack\"> </span>";
		internal const string HTMLPixCheckBlueCSS = "<span class=\"CheckBlue\"> </span>";
		//internal const string HTMLPixCheckGreenCSS = "<span class=\"CheckGreen\"> </span>"; 
		//internal const string HTMLPixCheckRedCSS = "<span class=\"CheckBlue\"> </span>"; 
		internal const string HTMLPixEndTag = "\" class=\"Icon\" alt=\"\"/>";

		internal const string HTMLPixComment = "<span class=\"Ico Comment\">&nbsp;&nbsp;&nbsp;&nbsp;</span>";
		internal const string HTMLPixCommentAdd = "<span class=\"Ico CommentAdd\">&nbsp;&nbsp;&nbsp;&nbsp;</span>";
		internal const string HTMLPixCommentUser = "<span class=\"Ico CommentUser\">&nbsp;&nbsp;&nbsp;&nbsp;</span>";
		private const string HTMLPixComment1 = "<span class=\"Ico Comment\" title=\"1\">&nbsp;&nbsp;&nbsp;&nbsp;</span>";
		private const string HTMLPixComments = "<span class=\"Ico Comments\" title=\"{0}\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";

		internal const string HTMLPixAddRow = "<span class=\"AddRow\">&nbsp;</span>";
		internal const string HTMLPixDelRow = "<span class=\"DelRow\"></span>";

		internal const string HTMLFlagRequired = "<span class=\"Required\">*</span>";
		internal const string HTMLFlagPopup = "<span class=\"ExtWeb\">&nbsp;</span>";

		internal const string HTMLOptionZero = "<option value=\"0\"> - </option>";
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
				return String.Format("<option value=\"{0}\" selected>{1}</option>", fieldValue, fieldlabel);
			else
				return HTMLOption(fieldValue, fieldlabel);
		}
		static internal string HTMLOption(string fieldValue, string fieldlabel)
		{
			return string.Format("<option value=\"{0}\">{1}</option>", fieldValue, fieldlabel);
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
				HTML.Append("<img src=\"").Append(Img).Append(HTMLPixEndTag).Append(label); 
			else
			{
				HTML.Append(label);
				if (popup)
					HTML.Append(HTMLFlagPopup);
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
				HTML.Append("<img src=\"").Append(Img).Append(HTMLPixEndTag);
			HTML.Append(label);
			if (popup)
				HTML.Append(HTMLFlagPopup);
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
				return EvoUI.HTMLPixCheckCSS;
			else
			{
				return new StringBuilder()
					.Append(EvoUI.HTMLSpace).Append(EvoUI.HTMLImg(PixPath + EvoUI.PixCheck))
					.ToString();
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
			return new StringBuilder()
				.AppendFormat("<textarea name=\"{0}\" id=\"{0}\" rows=\"{1}", fName, fRows)
				.Append("\" cols=\"52\" class=\"Field\"></textarea>")
				.ToString();
		}

		static internal string HTMLInputButton(string name, string label, bool submit, string onclick)
		{
			StringBuilder zHTML = new StringBuilder();

			zHTML.Append("<input type=\"").Append(submit?"submit":"button");
			if (!string.IsNullOrEmpty(onclick))
				zHTML.Append("\" onclick=\"").Append(onclick);
			zHTML.Append("\" class=\"Button\" name=\"").Append(name).Append("\" value=\" ").Append(label).Append(" \">");
			return zHTML.ToString();
		}

		static internal string HTMLInputHidden(string name, string value)
		{
			return string.Format("<input type=\"hidden\" name=\"{0}\" id=\"{0}\" value=\"{1}\">", name, value);
		}

		static internal string HTMLInputCheckBox(string name, string value, string label, bool selected, string id)
		{
			StringBuilder zHTML = new StringBuilder();

			zHTML.Append(HTMLCheckBoxBegin).Append(name).Append("\" value=\"").Append(value);
			if (!string.IsNullOrEmpty(id))
				zHTML.Append("\" id=\"").Append(id);
			if (selected)
				zHTML.Append(qChecked);
			zHTML.Append("\">");
			if (!string.IsNullOrEmpty(label))
			{
				zHTML.Append("<label for=\"").Append(string.IsNullOrEmpty(id)?name:id)
					.Append("\" class=\"smallTxt\">").Append(label).Append("</label>");
			}
			return zHTML.ToString();
		}
		static internal string HTMLInputCheckBox(string nameAndID, string value, string label)
		{
			StringBuilder zHTML = new StringBuilder();

			zHTML.Append(HTMLCheckBoxBegin).Append(nameAndID).Append("\" id=\"").Append(nameAndID).Append("\" value=\"").Append(value).Append("\">");
			if (!string.IsNullOrEmpty(label))
				zHTML.Append("<label for=\"").Append(nameAndID).Append("\" class=\"smallTxt\">").Append(label).Append("</label>");
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

			myHTML.AppendFormat("<label for=\"{0}\" class=\"smallTxt\"><input id=\"{0}\" name=\"{1}\" value=\"{2}", id, fName, fValue);
			if (selected)
				myHTML.Append(qChecked);
			myHTML.Append("\" type=\"radio\">").Append(fLabel).Append("</label>&nbsp;");
			return myHTML.ToString();
		}
		
		static internal string HTMLInputDate(string fName, string fValue, string locale, string pixDir)
		{
			StringBuilder myHTML = new StringBuilder();

			myHTML.Append("<nobr><input type=\"text\" class=\"Field Field80\" size=\"15\" maxlength=\"22\"")
				.AppendFormat(" name=\"{0}\" id=\"{0}\" value=\"{1}", fName, fValue)
				.Append("\"><a href=\"javascript:ShowDatePicker('").Append(fName);
			if (locale.Equals("FR") || locale.Equals("IT"))
				myHTML.Append("', false, 'dmy', '/");
			else if (locale.Equals("DA"))
				myHTML.Append("', false, 'dmy', '-");
			myHTML.Append("');\" class=\"Ico Calendar\">&nbsp;&nbsp;&nbsp;&nbsp;</a></nobr>");
			return myHTML.ToString();
		}

#endregion

//### HTML custom elements ############################################################################# 
#region "HTML custom elements"

		static internal string TRcssEvenOrOdd(bool b)
		{
			return b ? "<tr class=\"RowEven\">" : "<tr class=\"RowOdd\">";
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
			return new StringBuilder()
				.Append(myText).Append("<div class=\"Foot\">").Append(myOptions).Append("</div>")
				.ToString();
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
			/// <summary>make a query and returns the HTML for a lov.</summary>
			StringBuilder myHTML = new StringBuilder();
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
				int curID = 0;
				string currentEnum = null;
				string bufferID = null;
				string bufferValue = null;
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
			return string.Format("<a id=\"{0}link\" href=\"javascript:Evol.showMore('{0}',1)\" class=\"smallTxt\">{1}</a>", divID, linkLabel);
		}

		static internal string HTMLCommentFlag(int nbCommentsRow)
		{
			switch (nbCommentsRow)
			{
				case 0:
					return string.Empty;
				case 1:
					return HTMLPixComment1;
				default:
					return String.Format(HTMLPixComments, nbCommentsRow);
			}
		}

		static internal string StyleVisibleToggle(bool Visible)
		{
			return Visible ? " style=\"display:inline;\" " : " style=\"display:none;\" ";
		}

		static internal string Link4itemid(string OriginalURL, string ItemID)
		{
			return OriginalURL.Replace(EvoDB.p_itemid, ItemID);
		}

		static internal string HTMLtrColor(string aColor)
		{
			return string.IsNullOrEmpty(aColor) ? "<tr>" : string.Format("<tr bgcolor=\"{0}\">", aColor);
		}

		static internal string HTMLPanelLabel(string PanelLabel, string panelID, string panelClassName, bool CollapsiblePanels, bool IEbrowser)
		{
			if (string.IsNullOrEmpty(PanelLabel))
			{
				return string.Empty;
			}
			else
			{
				StringBuilder HTML = new StringBuilder();
				if (IEbrowser)
				{
					HTML.Append("<table class=\"").Append(panelClassName).Append("\" style=\"width:99%\"><tr><td>");
					if (CollapsiblePanels)
					{
						HTML.Append("<a class=\"Ico PanelClose\" href=\"javascript:")
							.AppendFormat("Evol.togglePanel('{0}',-1)\" ID=\"{0}link\"><div class=\"navp\"></div></a>", panelID);
					}
					HTML.Append(PanelLabel).Append("</td></tr></table>");
				}
				else
				{
					HTML.Append("<div class=\"").Append(panelClassName).Append("\">&nbsp;").Append(PanelLabel);
					if (CollapsiblePanels)
					{
						HTML.Append("<a class=\"Ico PanelClose\" style=\"float:right\" href=\"javascript:")
							.AppendFormat("Evol.togglePanel('{0}',-1)\" ID=\"{0}link\"><div class=\"navp\"></div></a>", panelID).Append(EvoUI.clearer);
					}
					HTML.Append("</div>");
				}
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

			myHTML.Append("<div id=\"Msg\" class=\"Msg bg").Append(css).Append("\">");
			if (icon != MsgType.x)
			{
				myHTML.Append("<div class=\"Ico ").Append(css).Append("\">&nbsp;</div>");
			}
			myHTML.Append(message.Replace("\n", tag_BR)); //leave < and > 
			myHTML.Append("</div><div class=\"clear\"></div>");
			return myHTML.ToString();
		}

		static internal string HTMLSearchOperators(string sEquals, string sStart, string sContain, string sFinish)
		{
			/// <summary>Search Operator for dropdown.</summary>
			/// <remarks>operators = equals, start with, contains, finishes with.</remarks>
			
			return new StringBuilder()
				.Append("_c\">")
				.Append("<option value=\"eq\">").Append(sEquals).Append("</option>")
				.Append("<option value=\"sw\" selected>").Append(sStart).Append("</option>")
				.Append("<option value=\"ct\">").Append(sContain).Append("</option>")
				.Append("<option value=\"fw\">").Append(sFinish).Append("</option>")
				.ToString();
		}

		static internal string Signature()
		{
			//This signature is "invisible" to users and must not be removed from the source code nor the compiled version of Evolutility 
			return string.Format("<div style=\"display:none;\">Powered by {0}</div>", HTMLLink(evoLink, evoName));
		}
		
		static internal string FormLogin(string labelLogin, string labelPassword, string labelButton, string DefaultLogin)
		{
			/// <summary>Login form (HTML).</summary>
			/// <remarks>Shared between Evolutility, and Wizards.</remarks>
			return new StringBuilder()
				.Append("<p align=\"center\"><table class=\"FormLogin\" width=\"62%\"><tr><td><div class=\"Key\"></div></td><td width=\"90%\">")
				.Append(HTMLFieldLabel(fNameLogin, labelLogin))
				.Append(HTMLInputText(fNameLogin, DefaultLogin, 50))
				.Append("</td></tr><tr><td></td><td>")
				.Append(HTMLFieldLabel(fNamePassword, labelPassword))
				.Append("<input type=\"password\" class=\"Field\" name=\"").Append(fNamePassword).Append("\" maxlength=\"50\">")
				.Append("<br/>&nbsp;</td></tr><tr><td></td><td class=\"PanelButtons\">")
				.Append(HTMLInputButton("Login", labelButton, true, ""))
				.Append("</td></tr></table><br/></p>")
				.Append(JSscript).Append("document.getElementById('").Append(fNameLogin).Append("').focus();").Append(JSscriptClose)
				.ToString();
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

		internal static string LinkDesigner(DesType DesignerType, int ItemID, string ItemLabel, string pathDesigner)
		{
			return string.Format(" <a href=\"Javascript:EvoDico.edit('{0}','{2}ed_{0}.aspx?ID={1}','{3}')\" class=\"des-{0}\">&nbsp;</a>", DesignerType.ToString("G"), ItemID, pathDesigner, HttpUtility.HtmlEncode(ItemLabel));
		}

#endregion

//### Misc. ############################################################################################ 
#region "Misc."

		static internal string JSIncludeEvoScripts(string path, string language)
		{
			/// <summary>Script tags for Evolutility JS libraries.</summary>
			/// <remarks>Used by Evolutility, and EvoDico Wizards.</remarks>
			const String bScript = "<script type=\"text/javascript\" src=\"";

			return new StringBuilder()
				.Append(bScript).Append(path).Append("JS/EvolUtility.js\"></script>\n")
				.Append(bScript).AppendFormat("{0}JS/lang/{1}.js", path, language).Append("\"></script>\n")
				.Append(bScript).Append(path).Append("JS/EvolDate.js\" defer=\"defer\"></script>\n")
				.ToString();
		}

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
					case "chart":
					case "charts":
						return 90;
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
