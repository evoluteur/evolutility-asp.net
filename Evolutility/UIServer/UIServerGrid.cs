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
using System.Data;
using System.Xml;
using System.Web;
using System.Drawing; 
//using System.Data.SqlClient;

namespace Evolutility
{
	partial class UIServer //  Grid : for list and edit
	{

		private string HTMLDataset(XmlNodeList aNodeList, string sql, string title, int ListMode, string LinkDetailNew, bool InsidePanel, int PanelDetailsIndex, int PanelDetailsID)
		{
			//ListMode: 0=list, 1=details, 2 details edit 
			string ctable, iconEntityDetails;
			bool UseComments = false, YesNo = false;
			string fieldType, fieldValue;
			string endTD = "&nbsp;</td>";
			int nbCommentsRow = 0, MaxLoopXML, MaxLoopSQL, MinLoop, TotalNbRecords = 0;
			string htmlRecCount = String.Empty, buffer, buffer1, buffer2;
			StringBuilder myHTML = new StringBuilder();
			StringBuilder myHTML2 = new StringBuilder();
			string[,] fieldFormats;
			DataSet ds;
			DataTable t = null;
			string myLabel;

			if (!_DBAllowUpdateDetails && ListMode == 2)
				ListMode = 1;
			//set record counts values 
			MaxLoopSQL = -1;
			if (string.IsNullOrEmpty(sql))
			{
				// PanelDetailsID??? bug qqq 
				if (detailsLoaded && PanelDetailsIndex > -1 && ds2 != null)
				{
					t = ds2.Tables[PanelDetailsIndex];
					MaxLoopSQL = t.Rows.Count - 1;
				}
			}
			else
			{
				ds = EvoDB.GetData(sql, _SqlConnection, ref ErrorMsg);
				if (ds != null)
				{
					t = ds.Tables[0];
					MaxLoopSQL = t.Rows.Count - 1;
				}
			}
			if (sql.Length > 0)
			{
				if (string.IsNullOrEmpty(ErrorMsg))
				{
					if (ListMode == 0 && !string.IsNullOrEmpty(def_Data.sppaging))
					{
						int nbRows = 0;
						if (MaxLoopSQL == _RowsPerPage - 1)
						{
							try
							{
								nbRows = Convert.ToInt32(t.Rows[0]["MoreRecords"]);
							}
							catch
							{ }
						}
						if (MaxLoopSQL > -1)
							TotalNbRecords = _RowsPerPage * (pageID - 1) + nbRows + MaxLoopSQL + 1;
						else
							TotalNbRecords = 0;
					}
					else
						TotalNbRecords = MaxLoopSQL + 1;
					htmlRecCount = HTMLrecordCount(TotalNbRecords, MaxLoopSQL);
				}
				else
				{
					MaxLoopSQL = -1;
					TotalNbRecords = MaxLoopSQL + 1;
					htmlRecCount = string.Empty;
				}
			}
			if (MaxLoopSQL > -1 || ListMode == 2)
			{
				MaxLoopXML = aNodeList.Count - 1;
				//display result 
				if (InsidePanel)
				{
					myHTML.Append("<table class=\"Holder\" cellpadding=\"4\"");
					if (PanelDetailsID > -1)
						myHTML.AppendFormat(" ID=\"{0}{1}P\">", UID, PanelDetailsID);
					else
						myHTML.Append(">");
				}
				if (ListMode == 0)
				{
					//If dbcolumnlead <> String.Empty Then myHTML.Append("<tr><td colspan=""2"">").Append(HTMLAlphabetLinks).Append("</td></tr>") 
					myHTML.Append("<tr valign=\"top\" class=\"RowInfo\"><td><p>").Append(title);
					if (_ShowDesigner && _FormID > 0)
						myHTML.Append(EvoUI.LinkDesigner(EvoUI.DesType.src, _FormID, title, _PathDesign));
					myHTML.Append("</p></td><td><p class=\"Right\">");
					if (TotalNbRecords > 0)
					{
						if (_DBAllowExport)
							htmlRecCount += String.Format(" - {0}&nbsp;", EvoUI.HTMLLinkEventRef("70", EvoLang.Export));
						myHTML.Append(htmlRecCount);
					}
					myHTML.Append("</p></td></tr><tr><td colspan=\"2\">");
				}
				else if (InsidePanel)
					myHTML.Append("<tr><td>");
				if (MaxLoopSQL > -1 || (ListMode == 2 && _DBAllowInsertDetails))
				{
					myHTML.AppendFormat("<span id=\"{0}{1}p\"><table id=\"EvoEditGrid{1}\"", UID, PanelDetailsID);
					myHTML.Append(" class=\"EvoEditGrid\" border=\"1\" bordercolor=\"#C9D6E9\" rules=\"all"); // attributes border, bordercolor and rules for Firefox
					if (ListMode == 0 && ColorTranslator.ToHtml(BackColorRowMouseOver) != string.Empty)
						myHTML.Append("\" style=\"behavior:url(").Append(_PathPixToolbar).Append("tablehl.htc);\" slcolor=\"#FFFFCC\" hlcolor=\"").Append(ColorTranslator.ToHtml(BackColorRowMouseOver));
					myHTML.Append("\">");
					//1 field only 
					if (MaxLoopXML == 0)
					{
						if (aNodeList[0].Attributes[xAttribute.labelList] == null)
							myLabel = aNodeList[0].Attributes[xAttribute.label].Value;
						else
							myLabel = aNodeList[0].Attributes[xAttribute.labelList].Value;
					}
					else
						myLabel = Tilda;

//##### table header 
#region "header"

					MaxLoopXML = aNodeList.Count - 1;
					fieldFormats = new string[MaxLoopXML + 1, 3];
					myHTML2.Append("<THEAD><tr class=\"RowHeader\">");
					//edit details 
					if (ListMode == 2)
					{
						myHTML2.Append("<td width=\"1%\">ID</td>");
						endTD = "</td>";
					}
					for (int j = 0; j <= MaxLoopXML; j++)
					{
						XmlNode cn = aNodeList[j];
						if (cn.NodeType == XmlNodeType.Element)
						{
							//cache field format 
							ctable = string.Format(".{0}", cn.Attributes[xAttribute.dbColumnRead].Value);
							switch (cn.Attributes[xAttribute.type].Value)
							{
								case EvoDB.t_text:
								case EvoDB.t_pix:
									if (ListMode < 2)
									{
										if (cn.Attributes[xAttribute.link] == null)
											fieldFormats[j, 0] = string.Empty;
										else
											fieldFormats[j, 0] = cn.Attributes[xAttribute.link].Value;
										if (cn.Attributes[xAttribute.linkTarget] == null)
											fieldFormats[j, 1] = string.Empty;
										else
											fieldFormats[j, 1] = cn.Attributes[xAttribute.linkTarget].Value;
									}
									else
										fieldFormats[j, 0] = string.Empty;
									break;
								case EvoDB.t_lov:
									if (ListMode < 2)
									{
										ctable = string.Format("@{0}", cn.Attributes[xAttribute.dbColumn].Value);
										if (cn.Attributes[xAttribute.link] == null)
											buffer = string.Empty;
										else
										{
											buffer = cn.Attributes[xAttribute.link].Value;
											if (buffer.Length > 0)
											{
												if (cn.Attributes[xAttribute.linkTarget] == null)
													fieldFormats[j, 1] = string.Empty;
												else
													fieldFormats[j, 1] = cn.Attributes[xAttribute.linkTarget].Value;
												if (cn.Attributes[xAttribute.linkLabel] == null)
													fieldFormats[j, 2] = string.Empty;
												else
													fieldFormats[j, 2] = cn.Attributes[xAttribute.linkLabel].Value;
											}
										}
									}
									else
										buffer = string.Empty;
									fieldFormats[j, 0] = buffer;
									break;
								case EvoDB.t_int:
								case EvoDB.t_dec:
									if (ListMode == 2)
									{
										if (cn.Attributes[xAttribute.dbReadOnly] == null)
											YesNo = false;
										else
											YesNo = cn.Attributes[xAttribute.dbReadOnly].Value.Equals(s1);
									}
									else
										YesNo = true;
									if (YesNo)
									{
										if (cn.Attributes[xAttribute.format] == null)
											fieldFormats[j, 0] = string.Empty;
										else
											fieldFormats[j, 0] = cn.Attributes[xAttribute.format].Value;
									}
									else
										fieldFormats[j, 0] = string.Empty;
									break;
								case EvoDB.t_date:
								case EvoDB.t_time:
								case EvoDB.t_datetime:
									if (cn.Attributes[xAttribute.format] == null)
										fieldFormats[j, 0] = EvoTC.DefaultDateFormat(cn.Attributes[xAttribute.type].Value);
									else
									{
										fieldFormats[j, 0] = cn.Attributes[xAttribute.format].Value;
										if (string.IsNullOrEmpty(fieldFormats[j, 0]))
											fieldFormats[j, 0] = EvoTC.DefaultDateFormat(cn.Attributes[xAttribute.type].Value);
										else if (fieldFormats[j, 0].IndexOf("{") < 0)
											fieldFormats[j, 0] = "{0:" + fieldFormats[j, 0] + "}";
									}
									break;
								case EvoDB.t_bool:
									if (cn.Attributes[xAttribute.imgList] != null)
										fieldFormats[j, 0] = cn.Attributes[xAttribute.imgList].Value;
									if (string.IsNullOrEmpty(fieldFormats[j, 0]))
									{
										if (cn.Attributes[xAttribute.img] == null)
											fieldFormats[j, 0] = EvoUI.PixCheck;
										else
										{
											fieldFormats[j, 0] = cn.Attributes[xAttribute.img].Value;
											if (fieldFormats[j, 0].Length == 0)
												fieldFormats[j, 0] = EvoUI.PixCheck;
										}
									}
									break;
								case EvoDB.t_url:
									buffer = string.Empty;
									if (cn.Attributes[xAttribute.imgList] != null)
										buffer = cn.Attributes[xAttribute.imgList].Value;
									if (buffer.Length == 0 && (cn.Attributes[xAttribute.img] != null))
										buffer = cn.Attributes[xAttribute.img].Value;
									if (buffer.Length > 0)
										buffer = _PathPixToolbar + buffer;
									fieldFormats[j, 0] = buffer;
									break;
								case EvoDB.t_formula:
									ctable = Tilda + cn.Attributes[xAttribute.dbColumnRead].Value;
									if (cn.Attributes[xAttribute.format] == null)
										fieldFormats[j, 0] = string.Empty;
									else
										fieldFormats[j, 0] = cn.Attributes[xAttribute.format].Value;
									break;
								default:
									fieldFormats[j, 0] = string.Empty;
									break;
							}
							// built header 
							if (cn.Attributes[xAttribute.labelList] == null)
								myLabel = cn.Attributes[xAttribute.label].Value;
							else
								myLabel = cn.Attributes[xAttribute.labelList].Value;
							myHTML2.Append("<td>");
							if (j == 0 && string.IsNullOrEmpty(myLabel))
								myHTML2.Append(HTMLlov(cn, cn.Attributes[xAttribute.dbColumnRead].Value, s0, LOVFormat.HTML, 0));
							else
							{
								// Label + Flag & Actions (required, designer, sorting)
								myHTML2.Append(myLabel);
								if (ListMode == 2 && cn.Attributes[xAttribute.required] != null && cn.Attributes[xAttribute.required].Value == s1)
									myHTML2.Append(EvoUI.FlagRequired);
								if (_ShowDesigner)
									myHTML2.Append(EvoUI.LinkDesigner(EvoUI.DesType.fld, Convert.ToInt32(cn.Attributes[xAttribute.id].Value), myLabel, _PathDesign));
								if (_AllowSorting && ListMode == 0 && TotalNbRecords > 2)
									myHTML2.Append(HTMLSortingArrows(ctable));
							}
							myHTML2.Append("</td>");
						}

					}
					myHTML2.Append("</tr></THEAD>");
					if (MaxLoopSQL == 0 || MaxLoopXML > 0 || !String.IsNullOrEmpty(myLabel))
						myHTML.Append(myHTML2);

#endregion 

					//##### table body 
					myHTML.Append("<TBODY>");
					if (ListMode == 2 && (_DBAllowInsertDetails || _DBAllowUpdateDetails) && !_DBReadOnly)
						genJS.Append(JSEditDetails(aNodeList, PanelDetailsID));     //sets nbFieldEditable 
					if (ListMode == 2 && MaxLoopSQL < 0)
						myHTML.Append(EvoUI.HTMLemptyRowEdit(MaxLoopXML + 2));
					else
						UseComments = EvoDB.ColumnExists(t, SQLColNbComments);
					if (string.IsNullOrEmpty(icon))
						iconEntityDetails = string.Empty;
					else
						iconEntityDetails = EvoUI.HTMLIcon(_PathPixToolbar, icon);
					for (int i = 0; i <= MaxLoopSQL; i++)
					{
						myHTML.Append(EvoUI.TRcssEvenOrOdd(YesNo));
						YesNo = !YesNo;
						DataRow ri = t.Rows[i];
						buffer2 = (ri[dbPrimaryKey].ToString());
						if (UseComments)
						{
							if (ri[SQLColNbComments] != null)
								try
								{
									nbCommentsRow = Convert.ToInt32(ri[SQLColNbComments]);
								}
								catch
								{
									nbCommentsRow = -1;
								}
							else
								nbCommentsRow = -1;
						}
						switch (ListMode)
						{
							case 2:
								//details edit 
								myHTML.AppendFormat("<td>{0}</td>", buffer2);
								MinLoop = 0;
								break;
							case 0:
								//search result (master) 
								myHTML.AppendFormat("<td><a href=\"javascript:EvPost('-{0}')\">", buffer2);
								//0=ID 
								fieldValue = Convert.ToString(ri[aNodeList[0].Attributes[xAttribute.dbColumnRead].Value]);
								if (string.IsNullOrEmpty(icon))
								{
									if (!String.IsNullOrEmpty(dbcolumnicon))
									{
										buffer = Convert.ToString(ri[dbcolumnicon]);
										if (!String.IsNullOrEmpty(buffer))
											myHTML.Append(EvoUI.HTMLIcon(_PathPix, buffer));
									}
								}
								else
									myHTML.Append(icon);
								if (String.IsNullOrEmpty(fieldValue))
									myHTML.AppendFormat("({0})</a>", buffer2);
								else
									myHTML.Append(HttpUtility.HtmlEncode(fieldValue)).Append("</a>");
								if (nbCommentsRow > 0)
									myHTML.Append(EvoUI.HTMLCommentFlag(nbCommentsRow));
								myHTML.Append("&nbsp;</td>");
								//If EditLink Then .Append("&nbsp;<small>[<a href=""Javascript:").Append(Page.GetPostBackEventReference(Me, "e" & buffer2)).Append(""">Edit</a>]<small>") 
								MinLoop = 1;
								break;
							default:
								//details list=1 
								MinLoop = 0;
								if (string.IsNullOrEmpty(icon) && dbcolumnicon.Length > 0)
								{
									if (object.ReferenceEquals(ri[dbcolumnicon], DBNull.Value))
										iconEntityDetails = string.Empty;
									else
										iconEntityDetails = EvoUI.HTMLIcon(_PathPix, Convert.ToString(ri[dbcolumnicon]));
								}
								break;
						}
						for (int j = MinLoop; j <= MaxLoopXML; j++)
						{
							XmlNode cn = aNodeList[j];
							if (cn.NodeType == XmlNodeType.Element)
							{
								myHTML.Append("<td>");
								string dbColumnReadName = cn.Attributes[xAttribute.dbColumnRead].Value;
								try
								{
									if (ri[dbColumnReadName] != null)
										fieldValue = ri[dbColumnReadName].ToString();
									else
										fieldValue = string.Empty;
								}
								catch
								{
									string newError = string.Format(EvoLang.err_NoDBColumn, dbColumnReadName);
									AddError(newError);
									fieldValue = string.Empty;
								}
								fieldType = cn.Attributes[xAttribute.type].Value;
								if (!String.IsNullOrEmpty(fieldValue))
								{
									switch (fieldType)
									{
										case EvoDB.t_text:
											fieldValue = HttpUtility.HtmlEncode(fieldValue);
											if (j == MinLoop)
											{
												if (ListMode == 1 && fieldFormats[j, 0].Length > 0)
												{
													if (!String.IsNullOrEmpty(iconEntityDetails))
														fieldValue = iconEntityDetails + fieldValue;
													myHTML.Append(EvoUI.HTMLLink(EvoUI.Link4itemid(fieldFormats[j, 0], ri[dbPrimaryKey].ToString()), fieldValue));
												}
												else
													myHTML.Append(fieldValue);
												if (UseComments && ListMode > 0 && ri[dbColumnReadName] != null)
												{
													nbCommentsRow = Convert.ToInt32(ri[SQLColNbComments]);
													if (nbCommentsRow > 0)
														myHTML.Append(EvoUI.HTMLCommentFlag(nbCommentsRow));
												}
											}
											else if (ListMode == 1 && fieldFormats[j, 0].Length > 0)
												myHTML.Append(EvoUI.HTMLLink(EvoUI.Link4itemid(fieldFormats[j, 0], ri[dbPrimaryKey].ToString()), fieldValue, fieldFormats[j, 1], null));
											else
												myHTML.Append(fieldValue);
											break;
										case EvoDB.t_lov:
											if (ListMode != 2)
											{
												if (fieldFormats[j, 0].Length > 0)
												{
													if (fieldFormats[j, 2].Length > 0)
														fieldValue = fieldFormats[j, 2];
													//linklabel 
													fieldValue = EvoUI.HTMLLink(EvoUI.Link4itemid(fieldFormats[j, 0], ri[cn.Attributes[xAttribute.dbColumn].Value].ToString()), fieldValue, fieldFormats[j, 1], null);
												}
												else if (cn.Attributes[xAttribute.dbColumnIcon] != null)
												{
													buffer1 = cn.Attributes[xAttribute.dbColumnIcon].Value;
													if (buffer1.Length > 0)
													{
														buffer2 = Convert.ToString(ri[buffer1]);
														if (!String.IsNullOrEmpty(buffer2))
															fieldValue = EvoUI.HTMLImg(_PathPix + buffer2) + EvoUI.HTMLSpace + fieldValue;
													}
												}
											}
											myHTML.Append(fieldValue);
											break;
										case EvoDB.t_bool: // format is a picture name here
											if (fieldValue.Equals(s1) || fieldValue.Equals("True"))
											{
												if (fieldFormats[j, 0].Length > 0)
													myHTML.Append(EvoUI.HTMLSpace).Append(EvoUI.HTMLImg(_PathPixToolbar + fieldFormats[j, 0], EvoLang.Checked));
												else
													myHTML.Append(EvoUI.HTMLImgCheckMark(IEbrowser, _PathPixToolbar));
											}
											break;
										case EvoDB.t_int:
											if (fieldFormats[j, 0].Length > 0 && EvoTC.isInteger(fieldValue))
												myHTML.Append(EvoUI.noBR(EvoTC.String2Int(fieldValue).ToString(fieldFormats[j, 0])));
											else
												myHTML.Append(fieldValue);
											break;
										case EvoDB.t_dec:
											if (fieldFormats[j, 0].Length > 0 && EvoTC.isDecimal(fieldValue))
												myHTML.Append(EvoUI.noBR(EvoTC.String2Dec(fieldValue).ToString(fieldFormats[j, 0])));
											else
												myHTML.Append(fieldValue);
											break;
										case EvoDB.t_date:
										case EvoDB.t_time:
										case EvoDB.t_datetime:
											if (fieldFormats[j, 0].Length > 0 && EvoTC.isDate(fieldValue))
												myHTML.Append(EvoUI.noBR(string.Format(fieldFormats[j, 0], EvoTC.String2DateTime(fieldValue))));
											else
												myHTML.Append(HttpUtility.HtmlEncode(fieldValue));
											break;
										case EvoDB.t_url:
											fieldValue = HttpUtility.HtmlEncode(fieldValue);
											if (fieldFormats[j, 0].Length > 0)
												myHTML.Append(EvoUI.HTMLLink(fieldValue, string.Empty, inNewBrowser, fieldFormats[j, 0]));
											else
												myHTML.Append(EvoUI.HTMLLink(fieldValue, fieldValue, inNewBrowser, null));
											break;
										case EvoDB.t_txtm:
											myHTML.Append(EvoTC.Text2HTMLwBR(fieldValue));
											break;
										case EvoDB.t_doc:
											myHTML.Append(EvoUI.HTMLLink(_PathPix + fieldValue, fieldValue, inNewBrowser, null));
											break;
										case EvoDB.t_pix:
											if (ListMode < 2 && fieldFormats[j, 0].Length > 0)
												myHTML.Append(EvoUI.HTMLLink(EvoUI.Link4itemid(fieldFormats[j, 0], ri[dbPrimaryKey].ToString()), String.Empty, fieldFormats[j, 1], _PathPix + fieldValue));
											else
												myHTML.Append(EvoUI.HTMLImg(_PathPix + fieldValue));
											break;
										case EvoDB.t_formula:
											if (fieldFormats[j, 0].Length > 0 && EvoTC.isDecimal(fieldValue))
												myHTML.Append(EvoUI.noBR(EvoTC.String2Dec(fieldValue).ToString(fieldFormats[j, 0])));
											else
												myHTML.Append(HttpUtility.HtmlEncode(fieldValue));
											break;
										case  EvoDB.t_html: // no escaping for html content
											// warning: using html fields may expose to cross-site scripting
											myHTML.Append(fieldValue);
											break;
										default:
											myHTML.Append(HttpUtility.HtmlEncode(fieldValue));
											break;
									}
								}
								myHTML.Append(endTD);
							}
						}
						myHTML.Append("</tr>");
					}
					myHTML.Append("</TBODY></table></span>");

					//--- FOOTER : buttons + summary + paging navigation --------------------------------- 
					if (ListMode == 2 & nbFieldEditable > 0)
						myHTML.Append(HTMLAddDeleteRows(PanelDetailsID));
					if (ListMode == 0)
						myHTML.Append("</td></tr><tr class=\"RowFoot\"><td colspan=\"2\">");
					if (!string.IsNullOrEmpty(def_Data.sppaging))
					{
						if (ListMode == 0)
							myHTML.Append(HTMLPagingFullNav(MaxLoopSQL, TotalNbRecords, htmlRecCount));
					}
					else if (TotalNbRecords > 0)
						myHTML.AppendFormat("<div>&nbsp;{0}</div>", htmlRecCount);
					if (ListMode == 1 && !(string.IsNullOrEmpty(LinkDetailNew) || _DBReadOnly))
						myHTML.Append("<p>").Append(EvoUI.HTMLLink(LinkDetailNew.Replace(EvoDB.p_itemid, s0) + "&tdLOVE=1N", EvoLang.NewItem)).Append("</p>");
				}
				if (InsidePanel)
					myHTML.Append(TdTrTableEnd);
			}
			else
			{
				myHTML.Append(HTMLNoGrid(ListMode, title, LinkDetailNew));
			}
			ds = null;
			return myHTML.ToString();
		}

		private string HTMLSortingArrows(string ColumnID)
		{ // sorting arrow links for grid header
			if (IEbrowser && IEbrowserVersion > 6)
				return string.Format("<a href=\"javascript:EvPost('a:{0}')\" class=\"Ico arrUp\"></a><a href=\"javascript:EvPost('d:{0}')\" class=\"Ico arrDown\"></a>", ColumnID);
			else
				return string.Format("&nbsp;<img src=\"{1}ordUp.gif\" alt=\"\" onclick=\"javascript:EvPost('a:{0}')\"/><img class=\"Tool\" src=\"{1}ordDown.gif\" onclick=\"javascript:EvPost('d:{0}')\" alt=\"\"/>", ColumnID, _PathPixToolbar);
		}

		private string HTMLAddDeleteRows(int gridID)
		{ // HTML = 1 or 2 icon links for Add & Delete rows
			StringBuilder myHTML = new StringBuilder();
			if (_DBAllowInsertDetails)
			{
				myHTML.Append("<div class=\"Paging\"><nobr>&nbsp;<a href=\"Javascript:EvoGrid.addRow");
				myHTML.AppendFormat("({0})\">{2}{1}", gridID, EvoLang.AddRow, EvoUI.PixAddRow);
				if (_DBAllowDelete) // inside => no delete if no insert!
				{
					myHTML.Append("</a>&nbsp;<a href=\"Javascript:EvoGrid.delRow");
					myHTML.AppendFormat("({0})\">{2}{1}", gridID, EvoLang.DelRow, EvoUI.PixDelRow);
				}
				myHTML.Append("</a></nobr></div>");
			}
			myHTML.Append("<span id=\"evoRO").Append(gridID).Append("-new\"></span><input type=\"hidden\" name=\"evoUDtls").AppendFormat("{0}\" id=\"evoUDtls{0}\">", gridID);
			return myHTML.ToString();
		}

		private string HTMLNoGrid(int ListMode, string title, string LinkDetailNew)
		{
			StringBuilder myHTML = new StringBuilder();

			myHTML.Append("<div class=\"Holder2\">");
			if (ListMode == 0)
			{
				if (!string.IsNullOrEmpty(title))
					myHTML.AppendFormat("<p>{0}</p>", title);
				if (string.IsNullOrEmpty(ErrorMsg))
					myHTML.Append("<p>").AppendFormat(EvoLang.NoEntity, def_Data.entity).Append("</p>");
				if (_DBAllowSearch)
					myHTML.Append("<p>").Append(EvoUI.HTMLLinkEventRef(s3, EvoLang.NewSearch)).Append(HTML_Sep).Append(EvoUI.HTMLLinkEventRef(s4, EvoLang.AdvSearch)).Append("</p>");
			}
			else
			{
				myHTML.AppendFormat("<p>{0}.&nbsp;", EvoLang.NoEntity);
				//item for sub panels 
				if (LinkDetailNew != string.Empty && !_DBReadOnly)
					myHTML.Append(EvoUI.HTMLLink(LinkDetailNew.Replace(EvoDB.p_itemid, s0) + "&tdLOVE=1N", EvoLang.NewItem));
				myHTML.Append("</p>");
			}
			myHTML.Append("</div>");
			return myHTML.ToString();
		}

		private StringBuilder HTMLNavLinksPaging()
		{
			StringBuilder myHTML = new StringBuilder();

			myHTML.Append(EvoUI.HTMLLinkEventRef("n" + (pageID - 1).ToString(), EvoLang.pPrev));
			for (int i = pageID - 5; i < pageID; i++)
			{
				if (i > 0)
					myHTML.Append(EvoUI.HTMLLinkEventRef("n" + i.ToString(), i.ToString())).Append(EvoUI.HTMLSpace);
			}
			return myHTML;
		}

		private StringBuilder HTMLPagingFullNav(int MaxLoopSQL, int TotalNbRecords, string htmlRecCount)
		{
			StringBuilder myHTML = new StringBuilder();

			if ((MaxLoopSQL == _RowsPerPage - 1) || pageID > 1)
			{
				myHTML.Append("<span class=\"Paging\">");
				if (pageID > 1)
					myHTML.Append(HTMLNavLinksPaging());
				myHTML.Append("<span class=\"PagingSel\">").Append(pageID).Append("</span>");
				if (MaxLoopSQL > 0 && pageID * _RowsPerPage <= TotalNbRecords)
				{
					for (int i = pageID + 1; i < pageID + 6; i++)
					{
						if ((i - 1) * _RowsPerPage >= TotalNbRecords)
							break;
						myHTML.Append(EvoUI.HTMLLinkEventRef("n" + i.ToString(), i.ToString()));
					}
					if (pageID * _RowsPerPage < TotalNbRecords)
						myHTML.Append(EvoUI.HTMLLinkEventRef("n" + (pageID + 1).ToString(), EvoLang.pNext));
				}
			}
			else if (TotalNbRecords > 0)
			{
				myHTML.AppendFormat("<div>&nbsp;{0}</div>", htmlRecCount);
			}
			myHTML.Append("</span>");
			return myHTML;
		}

		private string HTMLrecordCount(int TotalNbRecords, int MaxLoopSQL)
		{
			if ((TotalNbRecords > MaxLoopSQL + 1) || pageID > 1)
			{
				string htmlRecCount = null;
				int i = ((pageID - 1) * _RowsPerPage) + 1;
				if (MaxLoopSQL == 0)
					htmlRecCount = string.Format("{0} {1}", EvoTC.ToUpperLowers(def_Data.entity), i);
				else
					htmlRecCount = string.Format("{0} {1}{2}{3}", EvoTC.ToUpperLowers(def_Data.entities), i, HTML_Sep, (i + MaxLoopSQL));
				htmlRecCount += EvoLang.sOf + TotalNbRecords.ToString();
				return htmlRecCount;
			}
			else
			{
				switch (MaxLoopSQL)
				{
					case -1:
						return string.Empty;
					case 0:
						return string.Format("1 {0}", def_Data.entity);
					default:
						return string.Format("{0} {1}", TotalNbRecords, def_Data.entities);
				}
			}
		}

	}
}
