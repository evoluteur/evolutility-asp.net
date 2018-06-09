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
using System.Xml;

namespace Evolutility
{
	partial class UIServer //  JavaScript & Menus
	{

		private bool JSDetailsDone = false;
		private StringBuilder genJS;
		private bool JSinDetails;

//### JavaScript ###################################################################################### 
#region "JavaScript"

		private string JSFields2Validate()
		{
			//fields validation for edit (required + formats email, date... ) +image upload
			StringBuilder allFields = new StringBuilder();
			bool NeedValidation = false;

			XmlNodeList aNodeList = myDOM.DocumentElement.SelectNodes(xQuery.panelField, nsManager);
			allFields.Append("[");
			int maxLoop = aNodeList.Count;
			for (int i = 0; i < maxLoop; i++)
			{
				XmlNode cn = aNodeList[i];
				//--- not ReadOnly
				if (cn.Attributes[xAttribute.dbReadOnly] != null)
					NeedValidation = JSFieldNeedValidation(cn.Attributes[xAttribute.dbReadOnly].Value);
				else
					NeedValidation = true;
				if (NeedValidation)
				{
					StringBuilder aField = new StringBuilder();
					String fieldType = cn.Attributes[xAttribute.type].Value;
					String fieldLabel = xAttribute.GetFieldLabel(cn);
					if (fieldLabel.IndexOf("'") > -1)
						fieldLabel = fieldLabel.Replace("'", "\\'");
					if (fieldType != EvoDB.t_bool)
					{
						//--- Required
						if (cn.Attributes[xAttribute.required] == null)
							NeedValidation = false;
						else
							NeedValidation = cn.Attributes[xAttribute.required].Value == s1;
						if (NeedValidation)
							aField.Append(",r:1");
					}
					//        //--- Custom validation 
					//        //If Not .Attributes(Attr.Script) Is Nothing Then 
					//        // aField.Append(",c:").Append(.Attributes(Attr.Script).Value) 
					//        // myScript.Append("if(").Append(.Attributes(Attr.Script).Value.Replace("@fieldid", """" & fieldID & """").Replace("@fieldlabel", """" & fieldLabel & """")).Append(")return false;") 
					//        //End If 
					//--- Type Validation (after b/c null OK)
					switch (fieldType)
					{
						case EvoDB.t_date:// to force type validation
						case EvoDB.t_time:
						case EvoDB.t_datetime:
						case EvoDB.t_email:
						case EvoDB.t_html:// to flag for FCKeditor
							aField.Append(" ");	
							break;
						case EvoDB.t_int:
						case EvoDB.t_dec:
							//min value
							if (cn.Attributes[xAttribute.min] != null)
								aField.Append(EvoJSON.IntProp(xAttribute.min, cn.Attributes[xAttribute.min].Value));
							//max value
							if (cn.Attributes[xAttribute.max] != null)
								aField.Append(EvoJSON.IntProp(xAttribute.max, cn.Attributes[xAttribute.max].Value));
							break;
					}
					//--- Dependant drop downs
					if (cn.Attributes[xAttribute.jsDependency] != null)
						aField.Append(EvoJSON.StringProp("jsdep", cn.Attributes[xAttribute.jsDependency].Value));
					if (cn.Attributes[xAttribute.dependency] != null)
						aField.Append(EvoJSON.StringProp("dep", cn.Attributes[xAttribute.dependency].Value));
					//--- Custom validation
					if (cn.Attributes[xAttribute.jsValidation] != null)
						aField.Append(EvoJSON.StringProp("jsv", cn.Attributes[xAttribute.jsValidation].Value));
					if (cn.Attributes[xAttribute.regExp] != null)
						aField.Append(EvoJSON.StringProp("rg", cn.Attributes[xAttribute.regExp].Value.Replace("\\", "\\\\")));
					if (aField.Length > 0)
					{
						String fieldID = cn.Attributes[xAttribute.dbColumn].Value;
						allFields.Append("\n{").AppendFormat("id:'{0}',l:'{1}',t:'{2}'", fieldID, fieldLabel, fieldType);
						//if (listAllFields)
						//{
						//    if (cn.Attributes[xAttribute.search] != null && Convert.ToInt32(cn.Attributes[xAttribute.search].Value) > 0)
						//        allFields.Append(",s:1");
						//    if (cn.Attributes[xAttribute.searchAdv] != null && Convert.ToInt32(cn.Attributes[xAttribute.searchAdv].Value) > 0)
						//        allFields.Append(",sa:1");
						//    if (fieldType == EvoDB.t_lov)
						//        allFields.Append(",\nlst:").Append(HTMLlov(cn, "", s0, LOVFormat.JSON, 0));
						//    else
						//    {
						//        int ml = xAttribute.GetFieldMaxLength(cn);
						//        if (ml > 0)
						//            allFields.AppendFormat(",ml:{0}", ml);
						//    }
						//}
						allFields.Append(aField).Append("},");
					}
				}
			}
			if (allFields.Length > 3)
				allFields.Remove(allFields.Length - 1, 1);
			allFields.Append("]");
			return allFields.ToString();
		}

		private string JSEditDetails(XmlNodeList aNodeList, int gridID)
		{
			StringBuilder js2 = new StringBuilder();
			int nbFields;
			bool fieldReadOnly = false;

			//already checked _DBAllowUpdateDetails and _DBAllowInsertDetails before calling function ! 
			js2.AppendFormat("\n{0}:", gridID).Append("{flds:[ ");
			nbFieldEditable = 0; 	//nbFieldEditable is a global variable 
			nbFields = aNodeList.Count;
			for (int i = 0; i < nbFields; i++)
			{
				XmlNode cn = aNodeList[i];
				if (cn.NodeType == XmlNodeType.Element)
				{ 
					if (cn.Attributes[xAttribute.dbReadOnly] == null)
						fieldReadOnly = false;
					else
						fieldReadOnly = EvoTC.String2Int(cn.Attributes[xAttribute.dbReadOnly].Value) > 0;
					if (!fieldReadOnly)
					{
						nbFieldEditable += 1;
						String fieldType = cn.Attributes[xAttribute.type].Value;
						js2.Append("{").AppendFormat("i:{0},t:'{1}'", i + 1, fieldType);
						if (cn.Attributes[xAttribute.required] != null)
							js2.AppendFormat(",r:{0}", EvoTC.String2Int(cn.Attributes[xAttribute.required].Value));
						switch (fieldType)
						{
							case EvoDB.t_lov:
								js2.AppendFormat(",lov:{0}", HTMLlov(cn, String.Empty, s0, LOVFormat.JSON, 0));
								break;
							case EvoDB.t_bool:
								String fieldFormat = EvoUI.PixCheck;
								if (cn.Attributes[xAttribute.img] != null)
									fieldFormat = cn.Attributes[xAttribute.img].Value.Replace("'", "\\'");
								if (String.IsNullOrEmpty(fieldFormat))
									fieldFormat = EvoUI.PixCheck;
								js2.AppendFormat(",pix:'{0}'", fieldFormat);
								break;
							case EvoDB.t_text:
							case EvoDB.t_txtm:
							case EvoDB.t_email:
							case EvoDB.t_html:
							case EvoDB.t_url:
								int ml = xAttribute.GetFieldMaxLength(cn);
								if (ml > 0)
									js2.AppendFormat(",ml:{0}", ml);
								break;
						}
						js2.Append("},");
					}
				}
			}
			js2.Remove(js2.Length - 1, 1);
			js2.Append("]},");
			JSDetailsDone = (nbFieldEditable > 0);
			return js2.ToString();
		}

		private bool JSFieldNeedValidation(String dbReadOnly)
		{
			if (dbReadOnly == s1)
				return false;
			else if (dbReadOnly == s2)
				return _ItemID < 1;
			else
				return true;
		}

		private void JSRegisterScripts(string id)
		{
			if (_DisplayMode == 1)
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "FCKeditor", string.Format("<script src=\"{0}FCKeditor/FCKeditor.js\" type=\"text/javascript\"></script>", _PathPixToolbar));
			if (_ShowDesigner)
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "EvoDico", string.Format("<script src=\"{0}JS/EvoDico.js\" defer=\"defer\" type=\"text/javascript\"></script>", _PathPixToolbar));
			if (!(def_Data == null || string.IsNullOrEmpty(def_Data.js_script)))
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "EvoCustom", string.Format("<script src=\"{0}{1}\" defer=\"defer\" type=\"text/javascript\"></script>", _PathPixToolbar, def_Data.js_script));
			StringBuilder sbJS = new StringBuilder();
			sbJS.Append(EvoUI.JSIncludeEvoScripts(_PathPixToolbar, _Language));
			Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Evolutility", sbJS.ToString());
			genJS = new StringBuilder();
			if (XMLloaded)
			{
				genJS.Append("\n\nEvPost=function(EvEvent){").Append(JSPostBack("%$#@").Replace("'%$#@'", "EvEvent")).Append("};\n");
				genJS.Append("if(typeof Evol=='undefined'){alert('Evolutility Javascript library is invalid or not found.')}else{window.onload=function(EvEvent){Evol.setup()}};\n");
				genJS.Append("EvoGen={id:'").Append(id).Append("',entity:'").Append(def_Data.entity.Replace("'", "\\'"));
				if (_DisplayMode == 70) //Export
					genJS.Append("',entities:'").Append(def_Data.entities.Replace("'", "\\'"));
				genJS.AppendFormat("',path:'{0}'", _PathPixToolbar.Replace("'", "\\'"));
				genJS.AppendFormat(",lang:'{0}'",  _Language);
				if (_DBAllowHelp && _DisplayMode > 0 && _DisplayMode < 5)
					genJS.AppendFormat(",\nformid:'{0}'", _XMLfile.Replace("'", "\\'"));
				if (_DisplayMode == 1)
				{
					genJS.AppendFormat(",\nfields:{0}", JSFields2Validate());
					genJS.Append(",\ndetails:{lst:{ "); // need 1 space here
					JSinDetails = true;
				}
				// genJS.Append("}};");   //STAYS UNCLOSED, flagged by JSinDetails
			}
		}

		private void JSWrite(string js)
		{
			Page.Response.Write(EvoUI.JSscript);
			Page.Response.Write(js);
			Page.Response.Write(EvoUI.JSscriptClose);
		}

		private string JSPostBack(string EventParam)
		{
			return Page.ClientScript.GetPostBackEventReference(this, EventParam);
		}

#endregion

//### Menus ########################################################################################### 
#region "Menus"

		private string HTMLmenu(bool SecondIteration)
		{
			//HTML for the icon toolbar 
			StringBuilder zHTML = new StringBuilder();
			bool YesNo = true;
			const string sep = "<b>|</b>";
			bool higherTB = _Language == "JP";

			if (_ToolbarPosition != EvolToolbarPosition.None)
			{
				zHTML.Append("\n<div class=\"Toolbar\" id=\"Toolbar\"");
				if (higherTB)
					zHTML.Append(" style=\"height:22px\"");
				zHTML.Append("><nobr>");
				if (IEbrowser)
				{
					zHTML.Append("<span class=\"nav0\"></span>");
					//no transparent borders w/ IE 6 
					if (IEbrowserVersion < 7 || higherTB)
					{
						zHTML.Append("<style>.Toolbar span,.Toolbar a,.Toolbar a:hover,.Ico{");
						if (IEbrowserVersion < 7)
							zHTML.Append("border:0;");
						if (higherTB)
							zHTML.Append("height:22;");
						zHTML.Append("}</style>");
					}
				}
				//--- db menus 
				if (!_DBReadOnly)
				{
					if (_DBAllowInsert)
						zHTML.Append(EvoUI.menuItem(EvoLang.New, EvoUI.mNew, _DBAllowInsert));
					if (_DBAllowUpdate)
					{
						//item selected 
						if (_ItemID > 0)
						{
							if (_UserID > 0 && _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_Sharing))
							{
								try
								{
									YesNo = _UserID == Convert.ToInt32(ds.Tables[0].Rows[0][def_Data.dbcolumnuserid]);
								}
								catch
								{
									YesNo = false;
								}
							}
							//view 
							if (_DisplayMode == 0)
								zHTML.Append(EvoUI.menuItem(EvoLang.Edit, EvoUI.mEdit, YesNo));
							//edit 
							else
								zHTML.Append(EvoUI.menuItem(EvoLang.View, EvoUI.mView, YesNo));
						}
						else
							zHTML.Append(EvoUI.menuItem(EvoLang.Edit, EvoUI.mEdit, false));
					}
					if (_DBAllowDelete)
					{
						if ((_ItemID > 0 || nav > 0) && (_DisplayMode == 0 || _DisplayMode == 1 || _DisplayMode == 101))
						{
							if (_SecurityModel != EvolSecurityModel.Multiple_Users_Sharing)
								YesNo = true;
							else
							{
								//user is logged in 
								if (_UserID > 0)
								{
									try
									{
										YesNo = Convert.ToInt32(ds.Tables[0].Rows[0][def_Data.dbcolumnuserid]) == _UserID;
									}
									catch
									{
										YesNo = false;
									}
								}
								else
									YesNo = false;
							}
						}
						else
							YesNo = false;
						zHTML.Append(EvoUI.menuItem(EvoLang.Delete, "del", YesNo));
					}
					zHTML.Append(sep);
				}
				//--- other menus 
				if (_DBAllowSearch)
					zHTML.Append(EvoUI.menuItem(EvoLang.Search, (_DisplayMode == 3) ? "searchp" : EvoUI.mSearch, true));
				if (_DBAllowSelections)
					zHTML.Append(EvoUI.menuItem(EvoLang.Selections, "sel", true));
				zHTML.Append(EvoUI.menuItem(EvoLang.ListAll, "all", true));
				if (_DBAllowPrint || _DBAllowHelp)
				{
					zHTML.Append(sep);
					if (_DBAllowPrint)
						zHTML.Append(EvoUI.menuItem(EvoLang.Print, "print", true));
					if (_DBAllowHelp && _DisplayMode > 0 && _DisplayMode < 5)
						zHTML.Append(EvoUI.menuItem(EvoUI.HTMLSpace, "help", true));
				}
				if (_DBAllowDesign && !_ShowDesigner) // Customize 
					zHTML.Append(sep).Append(EvoUI.menuItemCustomize(_DisplayMode, EvoLang.Customize));
				if (_DBAllowLogout && _UserID > 0) //'--- login / logout 
					zHTML.Append(sep).Append(EvoUI.menuItem(EvoLang.Logout, "logout", true));
				if (_DisplayMode < 3) //--- prev, next... (only for edit and view mode) 
				{
					zHTML.Append(EvoUI.menuNavSep(IEbrowser));
					zHTML.Append("<span class=\"nav\" id=\"EvoRecPage\">&nbsp;");
					if (def_Data != null && string.IsNullOrEmpty(def_Data.spget))
					{
						bool isSame = PrevItemID == _ItemID;
						bool navBefore = nav == 1 || navd == 1 || (isSame && nav == 2);
						bool navAfter = nav == 4 || navd == 3 || (isSame && nav == 3);
						zHTML.Append(EvoUI.menuNav(_DisplayMode, navBefore, navAfter));
						navBar = (navBefore ? s0 : s1) + (navAfter ? s0 : s1);
					}
					zHTML.Append("</span>");
				}
				//--- footer menu 
				zHTML.Append("</nobr></div>\n");
			}
			//--- extra stuff 
			if (!SecondIteration)
			{
				if (_ShowTitle)
					zHTML.Append(HTMLTitle( ));
			}
			return zHTML.ToString();
		}

		private string HTMLTitle()
		{
			StringBuilder zHTML = new StringBuilder();

			zHTML.Append("<table class=\"Panel\" width=\"100%\" cellpadding=\"3\" cellspacing=\"0\" border=\"0\"><tr><td class=\"PanelLabel Header\" id=\"EVOL_Title\">");
			if (_ItemID > 0 && !def_Data.Equals(null) && !string.IsNullOrEmpty(def_Data.dbcolumnlead))
			{
				if (_DisplayMode == 0 || _DisplayMode == 1)
				{
					//--- show lead field above tabs 
					try
					{
						zHTML.Append(HttpUtility.HtmlEncode(ds.Tables[0].Rows[0][def_Data.dbcolumnlead].ToString()));
					}
					catch
					{ }
				}
				else
					zHTML.Append(ModeName(_DisplayMode));
			}
			else
				zHTML.Append(ModeName(_DisplayMode));
			if (_ShowDesigner && def_Data != null)
			{
				switch (_DisplayMode)
				{
					case 3: //search
					case 4: // adv search
						zHTML.Append(EvoUI.LinkDesigner(EvoUI.DesType.src, _FormID, def_Data.title, _PathDesign));
						break;
					default:
						zHTML.Append(EvoUI.LinkDesigner(EvoUI.DesType.frm, _FormID, def_Data.title, _PathDesign));
						break;
				}
			}
			zHTML.Append(EvoUI.HTMLSpace);
			string buffer = null;
			switch (DisplayMode)
			{
				case EvolDisplayMode.Search:
					buffer = EvoUI.HTMLLinkEventRef(s4, EvoLang.AdvSearch);
					break;
				case EvolDisplayMode.AdvancedSearch:
					buffer = EvoUI.HTMLLinkEventRef(s3, EvoLang.Search);
					break;
				default:
					buffer = string.Empty;
					break;
			}
			if (buffer != string.Empty)
				zHTML.Append("</td><td class=\"PanelLabel\"><p class=\"Right\">").Append(buffer).Append("</p>");
			zHTML.Append(TdTrTableEnd);

			return zHTML.ToString();
		}

		private string ModeName(int cDisplayMode)
		{
			StringBuilder buffer = new StringBuilder();
			const string s = "{0} {1}";
			if (def_Data != null)
			{
				switch (cDisplayMode)
				{
					case 0:
						buffer.AppendFormat(s, EvoLang.View, def_Data.entity);
						break;
					case 1:
						if (_ItemID > 0)
							buffer.AppendFormat(s, EvoLang.Edit, def_Data.entity);
						else
							buffer.AppendFormat(s, EvoLang.New, def_Data.entity);
						break;
					case 3:
						buffer.AppendFormat(s, EvoLang.Search, def_Data.entities);
						break;
					case 4:
						buffer.AppendFormat(s, EvoLang.AdvSearch, def_Data.entities);
						break;
					case 60:
						buffer.AppendFormat(s, EvoTC.ToUpperLowers(def_Data.entity), EvoLang.Selections);
						break;
					case 70:
						buffer.AppendFormat(s, EvoLang.Export, def_Data.entities);
						break;
					default:
						if (_DisplayMode > 101 && _DisplayMode < 111)
							buffer.AppendFormat(s, EvoTC.ToUpperLowers(def_Data.entity), EvoLang.SearchRes);
						else
							buffer.Append(string.Empty);
						break;
				}
			}
			return buffer.ToString();
		}

#endregion 

	}
}
