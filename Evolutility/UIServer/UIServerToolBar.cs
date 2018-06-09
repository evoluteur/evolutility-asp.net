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
using System.Xml;

namespace Evolutility
{
	//   ==================   Menus   ==================   
	// generate HTML for the toolbar

	partial class UIServer 
	{

//### Toolbar ########################################################################################### 
#region "Toolbar"

		private string HTMLmenu(bool SecondIteration)
		{
			/// <summary>HTML for the icon toolbar.</summary>
			StringBuilder zHTML = new StringBuilder();
			bool YesNo = true;
			const string sep = "<b>|</b>";

			if (_ToolbarPosition != EvolToolbarPosition.None)
			{
				zHTML.Append("\n<div class=\"evo-Toolbar\" id=\"Toolbar\"");
				if(IEbrowser)
					zHTML.Append(" style=\"height:32px\"");
				else if (_Language == "JP")
					zHTML.Append(" style=\"height:22px\"");
				zHTML.Append(">");
				if (!_DBReadOnly)
				{
					if (_DBAllowInsert)
						zHTML.Append(menuItem(EvoLang.New, EvoUI.mNew, _DBAllowInsert));
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
								zHTML.Append(menuItem(EvoLang.Edit, EvoUI.mEdit, YesNo));
							//edit 
							else
								zHTML.Append(menuItem(EvoLang.View, EvoUI.mView, YesNo));
						}
						else
							zHTML.Append(menuItem(EvoLang.Edit, EvoUI.mEdit, false));
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
						zHTML.Append(menuItem(EvoLang.Delete, "del", YesNo));
					}
					zHTML.Append(sep);
				}
				//--- other menus 
				if (_DBAllowSearch)
					zHTML.Append(menuItem(EvoLang.Search, (_DisplayMode == 3) ? "searchp" : EvoUI.mSearch, true));
				zHTML.Append(menuItem(EvoLang.ListAll, "all", true));
				if (_DBAllowSelections)
					zHTML.Append(menuItem(EvoLang.Selections, "sel", true));
				if (_DBAllowCharts)
					zHTML.Append(menuItem(EvoLang.Charts, "chart", true));
				/*
				if (_DBAllowExport)
				{
					if (_DisplayMode == 0 || _DisplayMode == 1)
					{
						zHTML.Append(menuItem(EvoLang.Export, "export1", true));
					}
					else if (_DisplayMode == 0 || _DisplayMode == 1)
					{
						zHTML.Append(menuItem(EvoLang.Export, "export", true));
					}
					else
					{
						zHTML.Append(menuItem(EvoLang.Export, "export", false));
					}
				} */
				if (_DBAllowPrint)
					zHTML.Append(menuItem(EvoLang.Print, "print", true));
				if (_DBAllowHelp && _DisplayMode > 0 && _DisplayMode < 5)
					zHTML.Append(menuItem(EvoUI.HTMLSpace, "help", true));
				if (_DBAllowDesign && !_ShowDesigner) // Customize 
					zHTML.Append(sep).Append(menuItemCustomize(_DisplayMode, EvoLang.Customize));
				if (_DBAllowLogout && _UserID > 0) //'--- login / logout 
					zHTML.Append(sep).Append(menuItem(EvoLang.Logout, "logout", true));
				if (_DisplayMode < 3) //--- prev, next... (only for edit and view mode) 
				{
					if (def_Data != null && string.IsNullOrEmpty(def_Data.spget))
					{
						bool isSame = PrevItemID == _ItemID;
						bool navBefore = nav == 1 || navd == 1 || (isSame && nav == 2);
						bool navAfter = nav == 4 || navd == 3 || (isSame && nav == 3);
						zHTML.Append(menuNav(_DisplayMode, navBefore, navAfter));
						navBar = (navBefore ? s0 : s1) + (navAfter ? s0 : s1);
					}
				}
				zHTML.Append("</div>\n");
			}
			return zHTML.ToString();
		}

		private string HTMLTitle()
		{
			/// <summary>Form title (mode name or title field).</summary>
			StringBuilder zHTML = new StringBuilder();

			zHTML.Append("<h2 class=\"Header\" id=\"EVOL_Title\">");
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
			zHTML.Append("</h2>");
			return zHTML.ToString();
		}

		private string ModeName(int cDisplayMode)
		{
			/// <summary>Form/view/mode name.</summary>
			StringBuilder buffer = new StringBuilder();
			const string s = "{0} {1}";
			if (def_Data != null)
			{
				switch (cDisplayMode)
				{
					case 0:  // View
						buffer.AppendFormat(s, EvoLang.View, def_Data.entity);
						break;
					case 1:  // Edit, New
						if (_ItemID > 0)
							buffer.AppendFormat(s, EvoLang.Edit, def_Data.entity);
						else
							buffer.AppendFormat(s, EvoLang.New, def_Data.entity);
						break;
					case 3:  // Search
						buffer.AppendFormat(s, EvoLang.Search, def_Data.entities);
						break;
					case 4:  // AdvSearch
						buffer.AppendFormat(s, EvoLang.AdvSearch, def_Data.entities);
						break;
					case 60:  // Selections
						buffer.AppendFormat(s, EvoTC.ToUpperLowers(def_Data.entity), EvoLang.Selections);
						break;
					case 70:  // Export
						buffer.AppendFormat(s, EvoLang.Export, def_Data.entities);
						break;
					case 80:  // MassUpdate
						buffer.AppendFormat(s, EvoLang.MassUpdate, def_Data.entities);
						break;
					case 90:
						buffer.AppendFormat("{0}: {1}", EvoLang.Charts, string.Format(EvoLang.AllEntities, def_Data.entities));
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

		private string menuItem(string label, string css, bool enabled)
		{
			/// <summary>HTML for a single menu item .</summary> 
			if (enabled)
				return String.Format("<a href=\"#\" class=\"{0} act\"><div class=\"{0}\"></div>{1} </a>", css, label);
			else
				return String.Format("<a href=\"#\" class=\"{0}Z\"><div class=\"{0}Z\"></div>{1} </a>", css, label); 
		}

		private string menuNav(int displayMode, bool navBefore, bool navAfter)
		{
			StringBuilder zHTML = new StringBuilder();
			string aLinkbuffer = "<a href=\"javascript:EvPost('{0}{1}')\" class=\"Ico nav{1}\"><div>&nbsp;</div></a>";
			string aNumString = (displayMode == 1) ? "30" : "20";

			if (navBefore)
				zHTML.Append("<div class=\"nav1\"></div><div class=\"nav2\"></div>");
			else
			{
				zHTML.AppendFormat(aLinkbuffer, aNumString, "1")
					.AppendFormat(aLinkbuffer, aNumString, "2");
			}
			if (navAfter)
				zHTML.Append("<div class=\"nav3\"></div><div class=\"nav4\"></div>");
			else
			{
				zHTML.AppendFormat(aLinkbuffer, aNumString, "3")
					.AppendFormat(aLinkbuffer, aNumString, "4");
			}
			zHTML.Append("<span class=\"clear\"></span>");
			return zHTML.ToString();
		}

		private static string menuItemCustomize(int DisplayMode, string CustomizeLabel)
		{
			string label = "<div class=\"customize\"></div> " + CustomizeLabel + EvoUI.HTMLSpace;
			return EvoUI.HTMLLinkEventRef("c:" + DisplayMode.ToString(), label, "customize act");
		}

#endregion 

	}
}
