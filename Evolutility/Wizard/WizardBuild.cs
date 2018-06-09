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
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml; 

namespace Evolutility
{
	partial class Wizard //  Build
	{

		private string FormBuild()
		{
			StringBuilder myHTML = new StringBuilder();

			myHTML.Append("<table width=\"100%\" class=\"Panel Holder\" cellSpacing=\"1\" cellpadding=\"4\"><tr><td colspan=\"2\">");
			myHTML.Append(HTMLInputTextRequiredFormatted("appname", "", "Application title", 100, "Example: \"Address book\""));
			myHTML.Append("</td></tr><tr valign=\"top\"><td width=\"50%\">");
			myHTML.Append(HTMLInputTextRequiredFormatted("entity", String.Empty, "Object name", 50, "Example: \"contact\""));
			myHTML.Append("</td><td width=\"50%\">");
			myHTML.Append(HTMLInputTextRequiredFormatted("entities", String.Empty, "name plural", 50, "Example: \"contacts\""));
			myHTML.Append("</td></tr><tr><td colspan=\"2\">");
			myHTML.Append(EvoUI.HTMLInputTextArea("Description", 5));
			myHTML.Append("</td></tr></table>");
			//.Append(JSscriptBegin) 
			//.Append("function evoValidForm(){if(Evol.checkAllFields([") 
			//.Append("{id:'appname',l:'Component Name',t:'text',r:1},{id:'entity',l:'Object name',t:'text',r:1},{id:'entities',l:'name plural',t:'text',r:1}])){return true}else{return false}};") 
			//.Append("function EvPost(EvEvent){").Append(Page.ClientScript.GetPostBackEventReference(Me, "%$#@").Replace("'%$#@'", "EvEvent")).Append("};") 
			//.Append(JSscriptEnd) 
			return myHTML.ToString();
		}

		private string FormBuild_DataDef()
		{
			StringBuilder myHTML = new StringBuilder();
			bool YesNo = false;
			string lov = HTMLlov("EvoDico_vFieldType", 5, "name");
			myHTML.Append(HTMLStepTableHeader(1));
			for (int k = 0; k < 3; k++)
			{
				if (k > 0)
				{
					myHTML.Append("\n<a class=\"AddRow\" href=\"Javascript:Evol.showMore");
					myHTML.AppendFormat("('{0}',1)\" ID=\"{0}link\">More fields</a></span><span id=\"{0}", "fldset" + k);
					myHTML.Append("\" style=\"display:none\">");
					myHTML.Append(t1);
				}
				for (int i = 1; i < 11; i++)
				{
					string rClass = ClassEvenOrOdd(YesNo);
					myHTML.Append("\n<tr").Append(rClass).Append(" valign=\"top\">");
					int r = i + k * 10;
					myHTML.Append("<td width=\"30%\"><select style=\"width:100%\"").Append(rClass).Append(" name=\"f_t").Append(r).Append("\">").Append(lov).Append("</select></td>");
					myHTML.Append("<td><input style=\"width:100%\"").Append(rClass).Append(" name=\"f_n").Append(r).Append("\" style=\"width:100%\" class=\"Fieldnw\" value=\"\"></td></tr>");
					YesNo = !YesNo;
				}
				myHTML.Append("</table>");
			}
			myHTML.Append("</span>");
			return myHTML.ToString();
		}

		private string FormBuild_DataDefDetails()
		{
			StringBuilder myHTML = new StringBuilder();

			string buffer2, buffer3;
			bool YesNo = false;


			ds = EvoDB.GetData(EvoDB.BuildSQL("ID,label,typeid,typepix,type", "EvoDico_xfield", "formID=" + AppID, "ID", 0), _SqlConnectionDico, ref ErrorMsg);
			if (ds != null)
			{

				// ORDER BY positionlist 
				DataTable t0 = ds.Tables[0];
				{
					for (int i = 0; i < t0.Rows.Count; i++)
					{
						myHTML.Append(EvoUI.HTMLInputHidden("f_id" + (i + 1).ToString(), t0.Rows[i]["ID"].ToString()));
					}
					myHTML.Append(HTMLStepTableHeader(2));
					for (int i = 0; i < t0.Rows.Count; i++)
					{
						buffer3 = ClassEvenOrOdd(YesNo);
						myHTML.Append(EvoUI.TRcssEvenOrOdd(YesNo));
						myHTML.Append("<tr").Append(buffer3).Append(" valign=\"top\"><td>");
						myHTML.Append(EvoUI.HTMLIcon(_PathPix, t0.Rows[i]["typepix"].ToString(), t0.Rows[i]["type"].ToString()));
						if (t0.Rows[i][xAttribute.label] != null)
						{
							myHTML.Append(t0.Rows[i][xAttribute.label].ToString());
						}
						myHTML.Append("</td><td>");
						buffer2 = (i + 1).ToString();
						int fieldTypeID = Convert.ToInt32(t0.Rows[i]["typeid"]);
						switch (fieldTypeID)
						{
							case 5: //txt 
								myHTML.Append("Max.Length ");
								myHTML.Append(EvoUI.HTMLInputText("F_len" + buffer2, "100", 3));
								break;
							case 6: //txt multiline 
								myHTML.Append("Max.Length ");
								myHTML.Append(EvoUI.HTMLInputText("F_len" + buffer2, "100", 5));
								myHTML.Append("Height ");
								myHTML.Append(EvoUI.HTMLInputText("f_h" + buffer2, "3", 2));
								break;
							case 4: //lov 
								myHTML.Append("List of Values (comma separated) <textarea style=\"width:100%;\" class=\"Field\" rows=\"3\" cols=\"52\" name=\"f_op").Append(buffer2);
								myHTML.Append("\" onKeyUp=\"EvoVal.checkMaxLen(this,1000)\"></textarea>");
								break;
							case 2: //date 
							case 17: //date-time 
							case 18: //time 
								myHTML.Append("Format <select ").Append(buffer3).Append(" name=\"f_ft").Append(buffer2).Append("\">");
								myHTML.Append(HTMLOptionsDateFormats(fieldTypeID));
								myHTML.Append("</select>");
								break;
							case 9: //decimal
							case 10: //integer
								myHTML.Append("Format <input class=\"Field\" ").Append(buffer3).Append(" name=\"f_ft").Append(buffer2).Append("\" value=\"\" maxlength=\"12\">");
								break;
							case 1: //boolean 
								myHTML.Append("Picture <select ").Append(buffer3).Append(" name=\"f_ft").Append(buffer2).Append("\">");
								myHTML.Append("<option value=\"\" selected>- Default -");
								myHTML.Append(EvoUI.HTMLOption("checkr.gif", "Red checkmark"));
								myHTML.Append(EvoUI.HTMLOption("checkg.gif", "Green checkmark"));
								myHTML.Append(EvoUI.HTMLOption(EvoUI.PixCheck, "Black checkmark"));
								myHTML.Append("</select>");
								break;
							default:
								myHTML.Append(lang_NA);
								break;
						}
						if (fieldTypeID == 1)
						{
							// booleans cannot be required
							myHTML.Append("<td></td></tr>");
						}
						else
						{
							myHTML.Append("<td><input type=\"checkbox\" name=\"f_rq").Append(buffer2);
							if (i < 4)
								myHTML.Append(EvoUI.qChecked);
							myHTML.Append("\" value=\"1\"></td></tr>");
						}
						YesNo = !YesNo;
					}
					myHTML.Append("</table></span>");
				}
			}
			else 
			{
				myHTML.Append(EvoUI.HTMLMessage("There was an error.", EvoUI.MsgType.Info));
			}
			return myHTML.ToString();
		}

		private string FormBuild_Searches()
		{
			StringBuilder myHTML = new StringBuilder();
			string buffer, buffer2, buffer3;

			ds = EvoDB.GetData(EvoDB.BuildSQL("ID,label,typeid,typepix,type,search,searchadv,searchlist", "EvoDico_xField", "formID=" + AppID, "fpos,id", 0), _SqlConnectionDico, ref ErrorMsg);
			DataTable t0 = ds.Tables[0];
			int NbRow = t0.Rows.Count;
			for (int i = 0; i < NbRow; i++)
			{
				myHTML.Append(EvoUI.HTMLInputHidden("f_id" + (i + 1).ToString(), t0.Rows[i]["ID"].ToString()));
			}
			myHTML.Append(HTMLStepTableHeader(3));
			bool YesNo = false;
			for (int i = 0; i < NbRow; i++)
			{
				DataRow ri = t0.Rows[i];
				buffer3 = ClassEvenOrOdd(YesNo);
				myHTML.Append("<tr").Append(buffer3).Append(" valign=\"top\"><td>");
				myHTML.Append(EvoUI.HTMLIcon(_PathPix, t0.Rows[i]["typepix"].ToString(), t0.Rows[i]["type"].ToString()));
				myHTML.Append(ri[xAttribute.label]);
				buffer2 = (i + 1).ToString();
				buffer = CheckedOrNot((bool)ri["search"]);
				myHTML.Append("</td><td><input type=\"checkbox\" name=\"f_s").Append(buffer2).Append("\" ").Append(buffer).Append(" value=\"1\"></td>\n");
				buffer = CheckedOrNot((bool)ri["searchadv"]);
				myHTML.Append("<td><input type=\"checkbox\" name=\"f_sa").Append(buffer2).Append("\" ").Append(buffer).Append(" value=\"1\"></td>\n");
				buffer = CheckedOrNot((bool)ri["searchlist"]);
				myHTML.Append("<td><input type=\"checkbox\" name=\"f_sr").Append(buffer2).Append("\" ").Append(buffer).Append(" value=\"1\"></td>\n");
				myHTML.Append("</tr>\n");
				YesNo = !YesNo;
			}
			myHTML.Append("</table></span>");
			return myHTML.ToString();
		}

		private string FormBuild_LayoutPanels()
		{
			StringBuilder myHTML = new StringBuilder();
			bool YesNo = false;
			myHTML.Append(HTMLStepTableHeader(4));
			for (int k = 0; k < 3; k++)
			{
				if (k > 0)
				{
					myHTML.AppendFormat("\n<a class=\"AddRow\" ");
					myHTML.AppendFormat("ID=\"pnlset{0}link\" href=\"Javascript:Evol.showMore('pnlset{0}',1)\">More fields</a></span><span id=\"pnlset{0}", k);
					myHTML.Append("\" style=\"display:none\">");
					myHTML.Append(t1);
				}
				for (int i = 1; i < 5; i++)
				{
					string rClass = ClassEvenOrOdd(YesNo);
					myHTML.Append("\n<tr").Append(rClass).Append(" valign=\"top\">");
					int r = i + k * 4;
					myHTML.Append("<td width=\"80%\"><input style=\"width:100%\" ").Append(rClass).Append("name=\"pn_n").Append(r).Append("\" value=\"");
					if (r == 1)
						myHTML.Append(appTitle);
					myHTML.Append("\"></td><td width=\"20%\"><input ").Append(rClass);
					myHTML.Append("name=\"pn_w").Append(r).Append("\" value=\"100\" style=\"width:100%\"></td></tr>");
					YesNo = !YesNo;
				}
				myHTML.Append("</table>");
			}
			myHTML.Append("</span>");
			return myHTML.ToString();
		}

		private string FormBuild_LayoutFields()
		{
			StringBuilder myHTML = new StringBuilder();
			string buffer, buffer2, buffer3;
			bool YesNo = false;
			ds = EvoDB.GetData(EvoDB.BuildSQL("ID,label,type,typepix", "EvoDico_xfield", string.Format("formID={0}", AppID), " fpos,id", 0), _SqlConnectionDico, ref ErrorMsg);
			DataTable t0 = ds.Tables[0];
			int ml = t0.Rows.Count;
			for (int i = 0; i < ml; i++)
			{
				myHTML.Append(EvoUI.HTMLInputHidden(string.Format("f_id{0}", (i + 1)), t0.Rows[i]["id"].ToString()));
			}
			myHTML.Append(HTMLStepTableHeader(5));
			buffer = HTMLlov("EvoDico_panel WHERE formID=" + AppID.ToString(), 0, xAttribute.label);
			for (int i = 0; i < ml; i++)
			{
				buffer3 = ClassEvenOrOdd(YesNo);
				myHTML.Append("<tr").Append(buffer3).Append(" valign=\"top\"><td>");
				buffer2 = (i + 1).ToString();
				myHTML.Append(EvoUI.HTMLIcon(_PathPix, t0.Rows[i]["typepix"].ToString(), t0.Rows[i]["type"].ToString()));
				myHTML.Append(t0.Rows[i][xAttribute.label]).Append("</td>");
				myHTML.Append("<td><input type=\"text\" name=\"f_w").Append(buffer2).Append("\" value=\"100\" ").Append(buffer3).Append("></td><td>");
				if (!string.IsNullOrEmpty(buffer))
				{
					myHTML.Append("<select name=\"f_ft").Append(buffer2).Append("\" ").Append(buffer3).Append(" style=\"width:100%\">");
					myHTML.Append(buffer).Append("</select>");
				}
				else
					myHTML.Append(lang_NA);
				myHTML.Append(tag_cTDcTR);
				YesNo = !YesNo;
			}
			myHTML.Append("</table></span>");
			return myHTML.ToString();
		}

		private string FormBuild_BuildApp()
		{
			const String ta = "<textarea style=\"width:100%;\" class=\"Field\" rows=\"10\" cols=\"52\" ";
			string buffer;
			StringBuilder myHTML = new StringBuilder();
			if (_BuildDB)
			{
				myHTML.Append("<p align=center>");
				buffer = AppID.ToString();
				myHTML.Append(HTMLToolsLink(buffer, String.Empty));
			}
			if (_ShowXML)
			{
				myHTML.Append(HTMLFieldLabelResize("XML", "XML"));
				myHTML.Append(ta).Append("id=\"XML\">").Append(appXML).Append("</textarea>");
			}
			if (_ShowSQL)
			{
				myHTML.Append(HTMLFieldLabelResize("SQL", "SQL"));
				myHTML.Append(ta).Append("id=\"SQL\">").Append(appSQL).Append("</textarea>");
			}
			if (_ShowASPX)
			{
				myHTML.Append(HTMLFieldLabelResize("ASPX", "ASPX"));
				myHTML.Append(ta).Append("id=\"ASPX\">").Append(appASPX).Append("</textarea>");
			}
			return myHTML.ToString();
		}

	}
}

