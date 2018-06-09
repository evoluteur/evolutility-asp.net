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


//#define DB_MySQL
#undef DB_MySQL

using System;
using System.Text;
using System.Xml;
using System.Web;

using System.Data;

#if DB_MySQL

//using MySql.Data;
using MySql.Data.MySqlClient;

#else

using System.Data.SqlClient;

#endif

namespace Evolutility
{
	// ==================   SQL and LOVs   ==================   
	// generate SQL statements
	// manage LOVs and cache them

	public enum EvolSecurityModel
	{
		//coupled w/ ReadOnly 
		Single_User = 0, //everybody does everything 
		Single_User_Password = 1, //need password to view/edit data 
		Multiple_Users_RLS = 3, //each user only sees/edit his own data 
		Multiple_Users_Sharing = 4, //everybody sees all but updates his only 
		//groups / MAYBE OTHER PROPERTY  
		Multiple_Users_Collaboration = 5 //read all + update all 
		//Multi_Tenant = 6, //read group + update his own 
		//Multi_Tenant_Collaboration = 7 //read group + update group 
	}

	partial class UIServer
	{
		// set maximum number of items for LOVs
		const int maxItem = 2000;   // lov dropdown
		const int maxItem2 = 10000; // lov in multi-select list

		//### Master ######################################################################################## 
		#region "Master"

		private string BuildSQLselect(bool Master, int MyMode, int formid, string Tsql, string SPsql, string Wsql, string OBsql, string XPathMask, int TOPsql, string dbcolumniconDetails)
		{
			string buffer, fieldX, dbtablelov;
			int PanelID = 0;
			string fieldType, fieldColumn, fieldColumnRead;
			bool fieldShows = true;
			StringBuilder mySQL = new StringBuilder();
			StringBuilder Fsql = new StringBuilder();
			XmlNodeList aNodeList;
			int cTOPsql = TOPsql;
			if (cTOPsql < 1)
				cTOPsql = _RowsPerPage;

			if (string.IsNullOrEmpty(XPathMask))
			{
				if (Master)
					buffer = xQuery.panelField;
				else
					buffer = xQuery.panelDetailsField;
			}
			else
				buffer = XPathMask;
			aNodeList = myDOM.DocumentElement.SelectNodes(buffer, nsManager);
			int maxLoopXML = aNodeList.Count;
			if (maxLoopXML > 0)
			{
				buffer = string.Format("{0} T", Tsql);
				switch (_DisplayMode)
				{
					case 71: // export search result
					case 72: // export 1 rec / all fields  
						mySQL.AppendFormat(" T.{0} AS ID", def_Data.dbcolumnpk);
						if (_DisplayMode == 72 && Master)
						{
							Wsql = EvoTC.CondiConcat(Wsql, string.Format(EvoDB.SQLf_T01, def_Data.dbcolumnpk, _ItemID), SQL_and);
						}
						break;
					default:
						if (cTOPsql < 1)
							cTOPsql = _RowsPerPage;
						mySQL.Append("");
						if (Master)
						{
							mySQL.AppendFormat("T.{0} as ID", def_Data.dbcolumnpk);
							if (_DBAllowComments != EvolCommentsMode.None)
								mySQL.Append(",T.").Append(SQLColNbComments);
							if (dbcolumnicon.Length > 0)
								mySQL.Append(",T.").Append(dbcolumnicon);
							if (_DBAllowUpdate && !_DBReadOnly && _SecurityModel == EvolSecurityModel.Multiple_Users_Sharing)
								mySQL.Append(",T.").Append(def_Data.dbcolumnuserid);
						}
						else
						{
							mySQL.Append("T.ID");
							if (Tsql == "-self-")
							{
								buffer = def_Data.dbtable + " T";
								if (dbcolumnicon.Length > 0)
									mySQL.Append(",T.").Append(dbcolumnicon);
								else if (dbcolumniconDetails != string.Empty)
									mySQL.Append(",T.").Append(dbcolumniconDetails);
							}
							else if (dbcolumniconDetails != string.Empty)
								mySQL.Append(",T.").Append(dbcolumniconDetails);
						}
						break;
				}
				Fsql.Append(buffer);
				fieldShows = true;
				for (int i = 0; i < maxLoopXML; i++)
				{
					XmlNode cn = aNodeList[i];
					fieldType = cn.Attributes[xAttribute.type].Value;
					fieldColumn = cn.Attributes[xAttribute.dbColumn].Value;
					//export generation 
					if (_DisplayMode == 71)
						fieldShows = (string)Page.Request[UID + fieldColumn] == s1;
					//Case 72 'export generation 1 rec all fields 
					// fieldShows = True 
					else if (!Master)
					{
						if (cn.Attributes["panelid"] == null)
							PanelID = -1;
						else
							PanelID = Convert.ToInt32(cn.Attributes["panelid"].Value);
						fieldShows = PanelID == formid;
						if (fieldShows)
						{
							switch (MyMode)
							{
								case 0:
								case 1:
								case 2:
									//0-view, 1-edit, 2-details 
									fieldShows = true;
									break;
								default:
									//100 list 
									if (cn.Attributes[xAttribute.searchList] == null)
										fieldShows = false;
									else
									{
										fieldX = cn.Attributes[xAttribute.searchList].Value;
										fieldShows = !(fieldX == string.Empty | fieldX == s0);
									}
									break;
							}
						}
					}
					if (fieldShows && !string.IsNullOrEmpty(fieldColumn))
					{
						switch (fieldType)
						{
							case EvoDB.t_lov:
								fieldColumnRead = cn.Attributes[xAttribute.dbColumnRead].Value;
								if (fieldColumn.Length == 2)
								{
									if (fieldColumn.ToUpper() != "ID")
										mySQL.Append(",T.").Append(fieldColumn);
								}
								else
									mySQL.Append(",T.").Append(fieldColumn);
								if (cn.Attributes[xAttribute.dbTableLOV] != null)
								{
									dbtablelov = cn.Attributes[xAttribute.dbTableLOV].Value;
									if (dbtablelov.Length > 0)
									{
										Fsql.AppendFormat(" left join {0} on T.{1}={0}.ID", dbtablelov, fieldColumn);
										if (cn.Attributes[xAttribute.dbWhereLOV] != null)
										{
											buffer = cn.Attributes[xAttribute.dbWhereLOV].Value;
											if (buffer.Length > 0)
												Fsql.Append(SQL_and).Append(buffer.Replace(EvoDB.p_userid, _UserID.ToString()).Replace(EvoDB.p_itemid, _ItemID.ToString()));
										}
										if (cn.Attributes[xAttribute.dbColumnIcon] != null)
										{
											buffer = cn.Attributes[xAttribute.dbColumnIcon].Value;
											if (buffer != string.Empty)
												mySQL.AppendFormat(",{0}.{1} AS {1}", dbtablelov, buffer);
										}
										if (cn.Attributes[xAttribute.dbColumnReadLOV] == null)
											buffer = xAttribute.dbName;
										else
										{
											buffer = cn.Attributes[xAttribute.dbColumnReadLOV].Value;
											if (string.IsNullOrEmpty(buffer))
												buffer = xAttribute.dbName;
										}
										mySQL.AppendFormat(",{0}.{1} AS {2}", dbtablelov, buffer, fieldColumnRead);
									}
								}
								break;
							case EvoDB.t_formula:
								fieldColumnRead = cn.Attributes[xAttribute.dbColumnRead].Value;
								mySQL.AppendFormat(",({0}) AS {1}", fieldColumn, fieldColumnRead);
								break;
							case EvoDB.t_url:
							case EvoDB.t_doc:
								if (cn.Attributes[xAttribute.dbColumnIcon] != null)
								{
									buffer = cn.Attributes[xAttribute.dbColumnIcon].Value;
									if (buffer != string.Empty)
										mySQL.AppendFormat(",T.{0} AS {0}", buffer);
								}
								mySQL.AppendFormat(EvoDB.SQL_NAME_cT0, fieldColumn);
								break;
							default:
								mySQL.AppendFormat(EvoDB.SQL_NAME_cT0, fieldColumn);
								break;
						}
					}
				}
				if (Master)
				{
					buffer = BuildSQLwhereSecurity();
					if (buffer.Length > 0)
						Wsql = EvoTC.CondiConcat(Wsql, buffer, SQL_and);
					if (!string.IsNullOrEmpty(def_Data.dbwhere))
					{
						if (string.IsNullOrEmpty(Wsql))
							Wsql = def_Data.dbwhere;
						else
							Wsql += string.Format(" AND ({0})", def_Data.dbwhere);
					}
				}
				//'Locked parent 
				//If dbwherelock <> String.Empty Then 
				// If sqlw <> String.Empty Then 
				// sqlw = dbwherelock & " AND (" & sqlw & ")" 
				// Else 
				// sqlw = " " & dbwherelock 
				// End If 
				//End If 
				if (_DBAllowComments != EvolCommentsMode.None)
					mySQL.Append(",T.").Append(SQLColNbComments);
				if (!string.IsNullOrEmpty(SPsql))
				{
					// 'Query or stored procedure call 
					switch (MyMode)
					{
						case 0:
						case 1: //view, edit 
							buffer = EvoDB.SPcall_Get(def_Data.sppaging, _ItemID, _UserID);
							break;
						//Case 20 'details 
						default: //100 list 
							if (string.IsNullOrEmpty(OBsql))
							{
								if (!Master)
									OBsql = "T.ID DESC";
							}
							else if (Master && OBsql.ToUpper() == "ID")
								OBsql = string.Format("T.{0} DESC", def_Data.dbcolumnpk);
							buffer = EvoDB.SPcall_Paging(def_Data.sppaging, mySQL.ToString(), Fsql.ToString(), Wsql, OBsql, def_Data.dbcolumnpk, pageID, _RowsPerPage, _UserID, Tsql);
							break;
					}
				}
				else
				{
					buffer = EvoDB.BuildSQL(mySQL.ToString(), Fsql.ToString(), Wsql, OBsql, cTOPsql);
				}
			}
			else
				ErrorMsg += EvoUI.HTMLtextMore("<p>Invalid definition", "\"" + xQuery.qPanelFields(xAttribute.searchList) + "\" must return at least one element.");
			return buffer;
		}

		private string[] BuildSQLlist(int lDisplayMode)
		{
			string dbcolumn, dbcolumnf, cacheKey;
			string fieldType, fieldlabel, fieldValue;
			String wSQL = String.Empty, wTXT = String.Empty, oSQL = String.Empty;
			string ClauseOperator;
			string buffer, buffer1, buffer3;
			string txtOperator = EvoLang.opAnd;
			XmlNodeList aNodeList;
			XmlNode aNode = null;
			StringBuilder myHTML = new StringBuilder();

			//###### Generate SQL ######## 
			if (string.IsNullOrEmpty(def_Data.dborder))
				oSQL = string.Format("T.{0}", def_Data.dbcolumnpk);
			else
			{
				if (def_Data.dborder.StartsWith(dot))
					def_Data.dborder = def_Data.dbtable + def_Data.dborder;
				oSQL = def_Data.dborder;
			}
			if (lDisplayMode == 110)
				wTXT = EvoLang.allEntities;
			//query form queries (selections) "//panel/field[@" & AttributeTrue & "[.>0]]" 
			else if (lDisplayMode == 102)
			{
				//buffer = queryUrlParam 
				if (queryUrlParam != string.Empty)
				{
					//If queryUrlParam.StartsWith("~") Then 'Alphabet links 
					// buffer = Right(queryUrlParam, 1) 
					// sqlW = "T." & dbcolumnlead & " LIKE '" & buffer & "%' " 
					// txtW = EvoLang.sStart & """" & buffer & """" 
					// oSQL = dbcolumnlead 
					//Else 
					aNode = myDOM.DocumentElement.SelectSingleNode(xQuery.qEquals(xQuery.query, xAttribute.url, queryUrlParam), nsManager);
					buffer = oSQL;
					if (aNode == null)
						ErrorMsg += EvoLang.err_BadQuery;
					else
					{
						if (aNode.Attributes[xAttribute.dbWhere] != null)
						{
							wSQL = String.Format("({0})", EvoTC.HTML2SQL(aNode.Attributes[xAttribute.dbWhere].Value));
							wSQL = wSQL.Replace("~pc~", "%").Replace("~s~", "*");
							if (_UserID > 0 && wSQL.IndexOf(EvoDB.p_userid) > 0)
								wSQL = wSQL.Replace(EvoDB.p_userid, _UserID.ToString());
						}
						if (aNode.Attributes[xAttribute.label] != null)
							wTXT = aNode.Attributes[xAttribute.label].Value;
						if (aNode.Attributes[xAttribute.dbOrder] == null)
							oSQL = buffer;
						else
							oSQL = aNode.Attributes[xAttribute.dbOrder].Value;
					}
				}
				//End If 
				else
					AddError(EvoLang.err_NoQuery4Req);
			}
			else if (lDisplayMode == 105)
			{
				string cacheKey2 = GetCacheKey(def_Data.dbtable);
				wSQL = Convert.ToString(Page.Cache[cacheKey2 + "_W"]);
				wTXT = Convert.ToString(Page.Cache[cacheKey2 + "_W2"]);
				if (string.IsNullOrEmpty(newOrderBy))
					newOrderBy = Convert.ToString(Page.Cache[cacheKey2 + "_O"]);
				if (!string.IsNullOrEmpty(newOrderBy))
				{
					switch (newOrderBy.Substring(0, 1))
					{
						case dot: //driving table 
							newOrderBy = string.Format("T{0}", newOrderBy);
							break;
						case "@": //lov table 
							int l = newOrderBy.Length;
							if (l > 6)
							{
								buffer = newOrderBy.Substring(1, l - 6);
								aNode = myDOM.SelectSingleNode(xQuery.qEquals(xQuery.panelField, xAttribute.dbColumn, buffer), nsManager);
								if (aNode.Attributes[xAttribute.dbTableLOV] == null)
									buffer = string.Empty;
								else
									buffer = aNode.Attributes[xAttribute.dbTableLOV].Value;
							}
							else
								buffer = string.Empty;
							if (string.IsNullOrEmpty(buffer))
							{
								if (aNode.Attributes[xAttribute.dbColumnReadLOV] == null)
									buffer1 = xAttribute.dbName;
								else
									buffer1 = aNode.Attributes[xAttribute.dbColumnReadLOV].Value;
								newOrderBy = String.Format("{0}.{1} {2}", buffer, buffer1, EvoTC.Right(newOrderBy, 4));
							}
							else
								newOrderBy = newOrderBy.Replace("@", "T.");
							break;
						case Tilda: //formula 
							int ln = newOrderBy.Length;
							if (ln > 6)
							{
								buffer = newOrderBy.Substring(1, ln - 6);
								aNode = myDOM.SelectSingleNode(xQuery.qEquals(xQuery.panelField, xAttribute.dbColumnRead, buffer), nsManager);
							}
							if (aNode.Attributes[xAttribute.dbColumn] == null)
								buffer1 = xAttribute.dbName;
							else
								buffer1 = String.Format("({0})", aNode.Attributes[xAttribute.dbColumn].Value);
							newOrderBy = String.Format(" {0} {1}", buffer1, EvoTC.Right(newOrderBy, 4));
							break;
					}
					def_Data.dborder = newOrderBy;
					oSQL = newOrderBy;
				}
			}
			else
			{
				_ItemID = 0;
				switch (lDisplayMode)
				{
					case 104: // adv search 
						buffer = xQuery.qPanelFields(xAttribute.searchAdv);
						break;
					case 71:
					case 72: // export generation 
						buffer = xQuery.panelField;
						break;
					default: // search 
						buffer = xQuery.qPanelFields(xAttribute.search);
						break;
				}
				aNodeList = myDOM.DocumentElement.SelectNodes(buffer, nsManager);
				//Generate WHERE clause 
				StringBuilder sbSQL = new StringBuilder();
				StringBuilder sbTXT = new StringBuilder();
				int MaxLoopXML = aNodeList.Count;
				for (int i = 0; i < MaxLoopXML; i++)
				{
					XmlNode cn = aNodeList[i];
					dbcolumn = cn.Attributes[xAttribute.dbColumn].Value;
					buffer = UID + dbcolumn;
					fieldValue = GetPageRequest(buffer);
					ClauseOperator = GetPageRequest(buffer + "_c");
					if (!(string.IsNullOrEmpty(fieldValue) && string.IsNullOrEmpty(ClauseOperator)))
					{
						fieldValue = fieldValue.Replace("'", "''");
						dbcolumnf = string.Format(EvoDB.SQL_NAME_T0, dbcolumn);
						fieldType = cn.Attributes[xAttribute.type].Value;
						fieldlabel = xAttribute.GetFieldLabel(cn);
						if (ClauseOperator == EvoDB.soIsNull || ClauseOperator == EvoDB.soIsNotNull)
						{
							sbSQL.Append(EvoDB.SQLec(dbcolumnf, EvoDB.t_text, fieldValue, ClauseOperator));
							sbTXT.AppendFormat(EvoTC.fsp_0_1_2, fieldlabel, txtOperator, TXTec(fieldlabel, EvoDB.t_text, fieldValue, ClauseOperator));
							fieldValue = "";
						}
						if (!(String.IsNullOrEmpty(fieldValue)))
						{
							switch (fieldType)
							{
								case EvoDB.t_lov:
									//'LOV value from cache 
									//' use cached LOV to display user value rather than ID 
									buffer3 = GetPageRequest(buffer + "_lbl");
									if (string.IsNullOrEmpty(buffer3))
									{
										if (_UseCache)
										{
											cacheKey = cn.Attributes[xAttribute.dbTableLOV].Value;
											if (cn.Attributes[xAttribute.dbColumnReadLOV] == null)
												buffer3 = string.Empty;
											else
												buffer3 = cn.Attributes[xAttribute.dbColumnReadLOV].Value;
											if (string.IsNullOrEmpty(buffer3))
												buffer3 = xAttribute.dbName;
											cacheKey += buffer3;
											if (cn.Attributes[xAttribute.dbColumnIcon] != null)
												cacheKey += cn.Attributes[xAttribute.dbColumnIcon].Value;
											if (cn.Attributes[xAttribute.dbWhereLOV] != null)
												cacheKey += cn.Attributes[xAttribute.dbWhereLOV].Value;
											cacheKey = EvoDB.t_lov + cacheKey.ToLower();
											buffer3 = LOVfromCache(cacheKey, fieldValue);
											if (string.IsNullOrEmpty(buffer3))
												buffer3 = fieldValue;
										}
										else
											buffer3 = fieldValue;
									}
									if (fieldValue.IndexOf(coma) > -1)
									{
										sbSQL.AppendFormat("{0}{1} IN ({2})", SQL_and, dbcolumnf, fieldValue);
										sbTXT.AppendFormat("{0}{1}{2}({3})", txtOperator, fieldlabel, EvoLang.qInList, buffer3);
									}
									else
									{
										sbSQL.AppendFormat("{0}{1}={2}", SQL_and, dbcolumnf, fieldValue);
										sbTXT.AppendFormat("{0}{1}{2}\"{3}\"", txtOperator, fieldlabel, EvoLang.qEquals, buffer3);
									}
									break;
								case EvoDB.t_bool:
									if (fieldValue == s0)
									{
										sbSQL.AppendFormat(EvoTC.f_0_1, SQL_and, EvoDB.wBoolIsFalse(dbcolumnf));
										sbTXT.AppendFormat(EvoTC.f_0_1_2, txtOperator, EvoLang.qNot, fieldlabel);
									}
									else
									{
										sbSQL.AppendFormat("{0}{1}>0", SQL_and, dbcolumnf);
										sbTXT.AppendFormat(EvoTC.f_0_1, txtOperator, fieldlabel);
									}
									break;
								case EvoDB.t_date:
								case EvoDB.t_datetime:
								case EvoDB.t_time:
									//search 
									if (lDisplayMode == 103)
									{
										if ("dayweekmonthyear".IndexOf(fieldValue) > -1)
										{
											sbSQL.AppendFormat("{0} (DATEDIFF({1},{2},{3})<1", SQL_and, fieldValue, dbcolumnf, EvoDB.SQL_NAME_NOW);
											sbTXT.AppendFormat(EvoTC.f_0_1, txtOperator, fieldlabel);
											// & EvoLang.sDateRangeLast & GetValFromCSVTuples(EvoLang.sDateRange, fieldValue) 
											switch (Page.Request[buffer + "dir"])
											{
												case "": //Within  
													sbTXT.Append(EvoLang.sDateRangeWithin);
													break;
												case "P": //Past 
													sbSQL.AppendFormat(" AND {0}<{1}", dbcolumnf, EvoDB.SQL_NAME_NOW);
													sbTXT.Append(EvoLang.sDateRangeLast);
													break;
												case "F": //Future 
													sbSQL.AppendFormat(" AND {0}>{1}", dbcolumnf, EvoDB.SQL_NAME_NOW);
													sbTXT.Append(EvoLang.sDateRangeNext);
													break;
											}
											sbSQL.Append(") ");
											sbTXT.Append(EvoUI.GetValFromCSVTuples(EvoLang.sDateRange, fieldValue));
										}
										else if (EvoTC.isDate(fieldValue))
										{
											sbSQL.Append(SQL_and).Append(EvoDB.wDateIsValue(dbcolumnf, EvoDB.dbFormat(fieldValue, EvoDB.t_date, 0, _Language), "="));
											sbTXT.AppendFormat("{0}{1} = {2}", txtOperator, fieldlabel, fieldValue);
										}
									}
									//advanced search 
									else if (EvoTC.isDate(fieldValue))
									{
										buffer3 = EvoDB.SQLec(null, EvoDB.t_date, string.Empty, ClauseOperator);
										sbSQL.Append(SQL_and).Append(EvoDB.wDateIsValue(dbcolumnf, EvoDB.dbFormat(fieldValue, EvoDB.t_date, 0, _Language), buffer3));
										sbTXT.AppendFormat("{0}{1} {3} {2}", txtOperator, fieldlabel, fieldValue, buffer3);
									}
									break;
								case EvoDB.t_dec:
								case EvoDB.t_int:
									if (EvoTC.isInteger(fieldValue))
									{
										sbSQL.AppendFormat(EvoTC.f_0_1, SQL_and, dbcolumnf);
										sbTXT.AppendFormat(EvoTC.f_0_1, txtOperator, fieldlabel);
										if (lDisplayMode == 103)
										{
											sbSQL.AppendFormat("={0}", fieldValue);
											sbTXT.AppendFormat("={0}", fieldValue);
										}
										else
										{
											buffer3 = EvoDB.SQLec(null, EvoDB.t_int, string.Empty, ClauseOperator);
											sbSQL.AppendFormat(EvoTC.f_0_1, buffer3, fieldValue);
											sbTXT.AppendFormat(" {0} {1}", buffer3, fieldValue);
										}
									}
									break;
								case EvoDB.t_pix:
								case EvoDB.t_doc:
									if (fieldValue == s1)
									{
										sbSQL.AppendFormat("{0}{1}<>'' ", SQL_and, dbcolumnf);
										sbTXT.AppendFormat(EvoTC.f_0_1_2, txtOperator, EvoLang.qWith, fieldlabel);
									}
									break;
								default:
									if (lDisplayMode == 103)
										ClauseOperator = EvoDB.soContain;
									//else
									//{ 
									//    //if (fieldType == EvoDB.t_formula)
									//    //    dbcolumnf = cn.Attributes[xAttribute.dbColumnRead].Value;
									//}
									sbSQL.Append(EvoDB.SQLec(dbcolumnf, EvoDB.t_text, fieldValue, ClauseOperator));
									sbTXT.AppendFormat(EvoTC.fsp_0_1_2, txtOperator, fieldlabel, TXTec(fieldlabel, EvoDB.t_text, fieldValue, ClauseOperator));
									break;
							}
						}
					}
				}
				//CommentsCount 
				if (_DBAllowComments != EvolCommentsMode.None && !string.IsNullOrEmpty(GetPageRequest("EvoxUCM")))
				{
					if (sbSQL.Length > 0)
					{
						sbSQL.Append(SQL_and);
						sbTXT.Append(txtOperator);
					}
					sbSQL.AppendFormat("0<T.{0}", SQLColNbComments);
					sbTXT.Append(EvoLang.wComments);
					if (_Language != "JP")
						sbTXT.Append(".");
				}
				wSQL = sbSQL.ToString();
				wTXT = sbTXT.ToString();
			}
			if (String.IsNullOrEmpty(wSQL))
				wTXT = EvoLang.allEntities;
			else if (lDisplayMode != 102 && wSQL.StartsWith(SQL_and))  //102 = query 
			{
				wSQL = wSQL.Substring(SQL_and.Length);
				wTXT = wTXT.Substring(txtOperator.Length);
			}
			//cache sql and txtW values 
			buffer = GetCacheKey(def_Data.dbtable);
			if (Page.IsPostBack || lDisplayMode == 102)
			{
				if (lDisplayMode < 71 || lDisplayMode > 72)
				{
					if (lDisplayMode != 105)
					{
						if (lDisplayMode == 110)
							clearSQLCache(buffer);
						else
						{
							Page.Cache[buffer + "_W2"] = wTXT;
							Page.Cache[buffer + "_W"] = wSQL;
						}
					}
					Page.Cache[buffer + "_O"] = string.Empty + oSQL;
				}
			}
			else
				clearSQLCache(buffer);
			string[] fSQL = new string[2];
			fSQL[0] = BuildSQLselect(true, 100, _FormID, def_Data.dbtable, def_Data.sppaging, wSQL, oSQL, xQuery.qPanelFields(xAttribute.searchList), 0, String.Empty);
			fSQL[1] = wTXT;
			return fSQL;
		}

		private string BuildSQLnav(int NavID, bool FirstTry)
		{
			//SQL WHERE clause for navigation w/ Parameter @itemID 
			string Wsql = string.Empty, OBsql = string.Empty, Buffer;

			if (string.IsNullOrEmpty(def_Data.spget))
			{
				switch (NavID)
				{
					case 1: //first 
						OBsql = string.Format("T.{0}", def_Data.dbcolumnpk);
						break;
					case 2: //previous 
						if (FirstTry)
						{
							if (ItemID > 0)
								Wsql = string.Format("T.{0}<@itemid", def_Data.dbcolumnpk);
							OBsql = string.Format("T.{0} DESC", def_Data.dbcolumnpk);
						}
						break;
					case 3: //next 
						if (FirstTry)
							Wsql = string.Format("T.{0}>@itemid", def_Data.dbcolumnpk);
						else
							OBsql = string.Format("T.{0} DESC", def_Data.dbcolumnpk);
						break;
					case 4: //last 
						OBsql = string.Format("T.{0} DESC", def_Data.dbcolumnpk);
						break;
					default:
						OBsql = string.Format("T.{0}", def_Data.dbcolumnpk);
						Wsql = string.Format("T.{0}=@itemid", def_Data.dbcolumnpk);
						break;
				}
				// add SQL WHERE clause from last search result 
				Buffer = GetCacheKey(def_Data.dbtable);
				if (Page.Cache[Buffer + "_W"] != null)
				{
					Buffer = Page.Cache[Buffer + "_W"].ToString();
					if (Buffer.Length > 0)
					{
						if (String.IsNullOrEmpty(Wsql))
							Wsql = Buffer;
						else
							Wsql = string.Format("{0} AND {1}", Buffer, Wsql);
					}
				}
				return BuildSQLselect(true, 0, _FormID, def_Data.dbtable, string.Empty, Wsql, OBsql, String.Empty, 1, String.Empty);
			}
			else
				return EvoDB.SQL_EXEC + def_Data.spget.Replace(EvoDB.p_userid, _UserID.ToString()).Replace("@navid", NavID.ToString());
		}

		private string BuildSQLwhereSecurity()
		{
			switch (_SecurityModel)
			{
				case EvolSecurityModel.Multiple_Users_RLS:
					return String.Format(EvoDB.SQLf_T01, def_Data.dbcolumnuserid, _UserID);
				case EvolSecurityModel.Multiple_Users_Sharing:
					return String.Format(EvoDB.SQLw_Sharing, def_Data.dbcolumnuserid, _UserID);
				default:
					return string.Empty;
			}
		}

		private int BuildSQLDeleteItem()
		{
			int retVal = 0;
			string aSQL = null;

			if (_DBAllowDelete)
			{
#if DB_MySQL
		MySqlConnection	con = new MySqlConnection(SqlConnection);

				if (string.IsNullOrEmpty(def_Data.spdelete))
				{
					if (_SecurityModel.Equals(EvolSecurityModel.Multiple_Users_RLS) || _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_Sharing))
						aSQL = string.Format("{0}={2} AND {1}={3}", def_Data.dbcolumnpk, def_Data.dbcolumnuserid, _ItemID, _UserID);
					else
						aSQL = string.Format("{0}={1}", def_Data.dbcolumnpk,_ItemID);
					aSQL = EvoDB.sqlDELETE(def_Data.dbtable, aSQL);
				}
				else
					aSQL = EvoDB.SQL_EXEC + def_Data.spdelete;
				MySqlCommand cmd = new MySqlCommand(aSQL, con); 
 
#else
				SqlConnection con = new SqlConnection(SqlConnection);

				if (string.IsNullOrEmpty(def_Data.spdelete))
				{
					if (_SecurityModel.Equals(EvolSecurityModel.Multiple_Users_RLS) || _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_Sharing))
						aSQL = string.Format("{0}=@itemid AND {1}=@userid", def_Data.dbcolumnpk, def_Data.dbcolumnuserid);
					else
						aSQL = string.Format("{0}=@itemid", def_Data.dbcolumnpk);
					aSQL = EvoDB.sqlDELETE(def_Data.dbtable, aSQL);
				}
				else
					aSQL = EvoDB.SQL_EXEC + def_Data.spdelete;
				SqlCommand cmd = new SqlCommand(aSQL, con);
				cmd.Parameters.AddWithValue(EvoDB.p_itemid, _ItemID);
				cmd.Parameters.AddWithValue(EvoDB.p_userid, _UserID);

#endif

				try
				{
					con.Open();
					cmd.ExecuteNonQuery();
					retVal = _ItemID;
				}
				catch //(Exception ex)
				{
					retVal = -1;
					AddError(string.Format(EvoLang.err_Delete, def_Data.entity, _ItemID.ToString()));
				}
				finally
				{
					con.Close();
				}
				if (retVal > 0)
				{
					HeaderMsg = string.Format(EvoLang.DeleteOK, _ItemID.ToString(), EvoTC.TextNowTime());
					OnDBChange(new DatabaseEventArgs(DBAction.DELETE, _ItemID));
				}
				//nav = 3 
				_DisplayMode = 110;
			}
			else
			{
				AddError(string.Format("{0}{1} {2}.", EvoLang.err_NoPermission, EvoLang.Delete, def_Data.entities));
				retVal = -1;
			}
			return retVal;
		}

		#endregion

		//### Details ######################################################################################## 
		#region "Details"

		private string BuildSQLDetails(bool LoadIt)
		{
			int i, maxRow, foID, MaxRows, MaxLoopXML = 0, XMLPanelID = 0;
			string dbcolumniconDet = null;
			string sqlw, splistlov;
			string buffer = null;
			XmlNodeList aNodeList;
			StringBuilder sql = new StringBuilder();

			if ((LoadIt && !(ds == null && _ItemID > 0)) || !LoadIt)
			{
				aNodeList = myDOM.DocumentElement.SelectNodes(xQuery.panelDetails, nsManager);
				MaxLoopXML = aNodeList.Count - 1;
				for (i = 0; i <= MaxLoopXML; i++)
				{
					XmlNode cn = aNodeList[i];
					try
					{
						MaxRows = Convert.ToInt32(cn.Attributes[xAttribute.dbMaxRows].Value);
					}
					catch
					{
						MaxRows = -1;
					}
					if (cn.Attributes[xAttribute.lovSPlist] == null)
						splistlov = string.Empty;
					else
						splistlov = cn.Attributes[xAttribute.lovSPlist].Value;
					if (string.IsNullOrEmpty(splistlov))
					{
						if (cn.Attributes[xAttribute.dbTableDetails] == null || cn.Attributes[xAttribute.dbColumnDetails] == null)
						{
							AddError(EvoLang.err_DefDico + "Invalid or missing panel-details attributes.");
							dbcolumndetails = string.Empty;
						}
						else
						{
							dbtabledetails = cn.Attributes[xAttribute.dbTableDetails].Value;
							dbcolumndetails = cn.Attributes[xAttribute.dbColumnDetails].Value;
						}
						if (dbtabledetails != string.Empty)
						{
							if (cn.Attributes[xAttribute.dbWhere] == null)
								sqlw = string.Empty;
							else
								sqlw = cn.Attributes[xAttribute.dbWhere].Value;
							if (dbcolumndetails != string.Empty)
							{
								if (dbtabledetails == "-self-")
								{
									try
									{
										foID = Convert.ToInt32(ds.Tables[0].Rows[0][dbcolumndetails]);
									}
									catch
									{
										foID = -1;
									}
									sqlw = EvoTC.CondiConcat(sqlw, string.Format(EvoDB.SQLf_T01, dbcolumndetails, foID.ToString()), SQL_and);
								}
								else
									sqlw = EvoTC.CondiConcat(sqlw, string.Format(EvoDB.SQLf_T01, dbcolumndetails, _ItemID.ToString()), SQL_and);
							}
							if (cn.Attributes[xAttribute.dbMaxRows] == null)
								maxRow = 0;
							else
								maxRow = EvoTC.String2Int(cn.Attributes[xAttribute.dbMaxRows].Value);
							if (cn.Attributes["panelid"] == null)
								foID = _FormID;
							else
							{
								XMLPanelID = Convert.ToInt32(cn.Attributes["panelid"].Value);
								foID = Convert.ToInt32(XMLPanelID);
							}
							if (cn.Attributes[xAttribute.dbColumnIcon] == null)
								dbcolumniconDet = string.Empty;
							else
								dbcolumniconDet = cn.Attributes[xAttribute.dbColumnIcon].Value;
							if (cn.Attributes[xAttribute.dbOrder] == null)
								buffer = string.Empty;
							else
								buffer = cn.Attributes[xAttribute.dbOrder].Value;
							sql.Append(BuildSQLselect(false, 2, foID, dbtabledetails, string.Empty, sqlw, buffer, xQuery.panelDetailsField, maxRow, dbcolumniconDet));
						}
						else
							sql.Append(EvoDB.SPcall_Get(splistlov, _ItemID, _UserID));
					}
				}
			}
			aNodeList = null;
			if (_DBAllowComments != EvolCommentsMode.None && !noCommentsHere)
			{
				try
				{
					i = Convert.ToInt32(ds.Tables[0].Rows[0][SQLColNbComments]);
				}
				catch
				{
					i = 0;
				}
				if (i > 0)
				{
					buffer = string.Format("T.UserID={0}.ID AND T.{1}={2}", def_Data.dbtableusers, def_Data.dbcolumncomments, _ItemID);
					if (def_Data.dbcommentsformid > 0)
						buffer += string.Format(" AND T.FormID={0}", def_Data.dbcommentsformid);
					sql.Append(EvoDB.BuildSQL("T.ID AS ID,T.message,T.CreationDate,t.UserID," + def_Data.dbtableusers + ".login AS login", def_Data.dbtablecomments + " T," + def_Data.dbtableusers, buffer, "T.ID DESC", 100));
				}
				else
					noCommentsHere = true;
			}
			if (LoadIt)
			{
				if (sql.Length > 0 && !detailsLoaded)
				{
					ds2 = EvoDB.GetData(sql.ToString(), SqlConnection, ref ErrorMsg);
					detailsLoaded = true;
				}
				return null;
			}
			else
				return sql.ToString();
		}

		private string BuildSQLDetailsUpsert()
		{
			int fid, maxCol, id;
			bool hasValue = false;
			string[] CellValues, CellValuesOriginal;
			StringBuilder SQL = new StringBuilder();
			StringBuilder sbSQL1;
			string fieldType, fieldColumn;
			string r0e1c = "{0}={1},";
			int fieldMaxLength = 0;
			bool fieldIn = false;

			XmlNodeList panelNodeList = myDOM.DocumentElement.SelectNodes(xQuery.panelDetails, nsManager);
			//for each details 
			for (int p = 0; p < panelNodeList.Count; p++)
			{
				XmlNode cn0 = panelNodeList[p];
				string dbTableDetails = cn0.Attributes[xAttribute.dbTableDetails].Value;
				string dbcolumn = cn0.Attributes[xAttribute.dbColumnDetails].Value;
				string gridID = cn0.Attributes["panelid"].Value;
				string[] sep = new string[] { "~!" };
				XmlNodeList aNodeList = cn0.ChildNodes;
				maxCol = aNodeList.Count - 1;
				//for each row 
				for (int i = 1; i < 101; i++)
				{
					string newVals = GetPageRequest(string.Format("evoRO{0}-{1}", gridID, i));
					if (!string.IsNullOrEmpty(newVals))
					{
						CellValues = newVals.Split(sep, StringSplitOptions.None);
						id = EvoTC.String2Int(CellValues[0]);
						if (id > 0)
						{
							if (_DBAllowUpdateDetails)
							{
								string oldVals = (string)Page.Request[string.Format("evoRO{0}-C{1}", gridID, i)];
								if (oldVals != string.Empty && oldVals != newVals)
								{
									CellValuesOriginal = oldVals.Split(sep, StringSplitOptions.None);
									sbSQL1 = new StringBuilder();
									if (CellValues.Length == CellValuesOriginal.Length)
									{
										fid = 0;
										for (int j = 0; j <= maxCol; j++)
										{
											XmlNode cn = aNodeList[j];
											if (cn.NodeType == XmlNodeType.Element)
											{
												if (cn.Attributes[xAttribute.dbReadOnly] == null)
													fieldIn = true;
												else
												{
													string buffer = cn.Attributes[xAttribute.dbReadOnly].Value;
													fieldIn = EvoTC.String2Int(buffer) < 1;
												}
												if (fieldIn)
												{
													fid += 1;
													fieldType = cn.Attributes[xAttribute.type].Value;
													fieldColumn = cn.Attributes[xAttribute.dbColumn].Value;
													if (fieldType == EvoDB.t_lov)
													{
														if (EvoTC.String2Int(CellValues[fid]) > 0 && CellValues[fid] != CellValuesOriginal[fid])
															sbSQL1.AppendFormat(r0e1c, fieldColumn, CellValues[fid]);
													}
													else
													{
														if (CellValues[fid] != CellValuesOriginal[fid])
														{
															if (fieldType == EvoDB.t_bool)
																sbSQL1.AppendFormat(r0e1c, fieldColumn, CellValues[fid]);
															else
															{
																fieldMaxLength = xAttribute.GetFieldMaxLength(cn);
																sbSQL1.AppendFormat(r0e1c, fieldColumn, EvoDB.dbFormat(CellValues[fid], fieldType, fieldMaxLength, _Language));
															}
														}
													}
												}
											}
										}
										if (sbSQL1.Length > 0)
										{
											sbSQL1.Remove(sbSQL1.Length - 1, 1);
											SQL.Append(EvoDB.sqlUPDATE(dbTableDetails, sbSQL1.ToString(), string.Format("ID={0} AND {1}={2}", id, dbcolumn, _ItemID)));
										}
									}
									else
									{
										if (CellValues.Length == 2 && CellValues[1] == "DEL")
											SQL.Append(EvoDB.sqlDELETE(dbTableDetails,EvoDB.IDequals(id)));
										else
											AddError(string.Format("Invalid format for details ID #{0}", id));
									}
								}
							}
						}
						else if (_DBAllowInsertDetails)
						{
							sbSQL1 = new StringBuilder();
							StringBuilder sbSQL2 = new StringBuilder();
							for (int j = 0; j <= maxCol; j++)
							{
								XmlNode cn = aNodeList[j];
								if (cn.NodeType == XmlNodeType.Element)
								{
									if (cn.Attributes[xAttribute.dbReadOnly] == null)
										fieldIn = true;
									else
										fieldIn = !(cn.Attributes[xAttribute.dbReadOnly].Value == s1);
									if (fieldIn)
									{
										fieldType = cn.Attributes[xAttribute.type].Value;
										fieldColumn = cn.Attributes[xAttribute.dbColumn].Value;
										int c = j + 1;
										fieldMaxLength = 0;
										// CInt(.Attributes(Attr.MaxLength).Value) 
										hasValue = (fieldType == EvoDB.t_lov && EvoTC.String2Int(CellValues[c]) > 0) || (CellValues[c] != string.Empty);
										if (hasValue)
										{
											sbSQL1.AppendFormat("{0},", fieldColumn);
											sbSQL2.AppendFormat("{0},", EvoDB.dbFormat(CellValues[c], fieldType, fieldMaxLength, _Language));
										}
									}
								}
							}
							if (sbSQL1.Length > 0)
							{
								sbSQL1.Append(dbcolumn);
								sbSQL2.Append(_ItemID);
								SQL.Append(EvoDB.sqlINSERT(dbTableDetails, sbSQL1.ToString(), sbSQL2.ToString()));
							}
						}
					}
				}
			}
			return SQL.ToString();
		}

		#endregion

		//### Lists of Values LOVs ############################################################################## 
		#region "LOVs"

		private string HTMLlov(XmlNode aNode, string FieldName, string ItemID, LOVFormat format, int Lookup)
		{
			// returns HTML or JSON for a lov (+ query DB)
			int i2, i3, myID;
			string sql, pixname;
			StringBuilder myHTML = new StringBuilder();
			DataSet Source = null;
			int curID = 0, MaxLoop;
			string cacheKey = string.Empty;
			bool wBR = false;
			bool DynamicItemID = false;
			string SQLTable = string.Empty, SQLColumn, SQLColumnImg, SQLwhere = string.Empty, SQLOrderBy = null;

			bool SingleSelection = String.IsNullOrEmpty(FieldName);
			if (SingleSelection)
				MaxLoop = maxItem;
			else
				MaxLoop = maxItem2;
			//cache key = LCase(EvoDB.t_lov & dbtable & A(Attr.dbtablelov) & (Attr.dbcolumnreadlov) & (Attr.dbColumnImg)) 
			if (aNode.Attributes[xAttribute.dbTableLOV] != null)
				SQLTable = aNode.Attributes[xAttribute.dbTableLOV].Value;
			if (string.IsNullOrEmpty(SQLTable) && (aNode.Attributes[xAttribute.lovEnumeration] != null))
			{
				SQLTable = aNode.Attributes[xAttribute.lovEnumeration].Value;
				if (SQLTable.Length > 0)
				{
					myHTML.Append(EvoUI.HTMLlovEnum(SQLTable, FieldName, Convert.ToInt32(ItemID), true));
					SQLTable = string.Empty;
				}
			}
			if (SQLTable.Length > 0)
			{
				if (aNode.Attributes[xAttribute.dbColumnReadLOV] == null)
					SQLColumn = string.Empty;
				else
					SQLColumn = aNode.Attributes[xAttribute.dbColumnReadLOV].Value;
				if (SQLColumn.Length == 0)
					SQLColumn = xAttribute.dbName;
				if (aNode.Attributes[xAttribute.dbColumnIcon] == null)
					SQLColumnImg = string.Empty;
				else
					SQLColumnImg = aNode.Attributes[xAttribute.dbColumnIcon].Value;
				if (aNode.Attributes[xAttribute.dbWhereLOV] != null)
				{
					SQLwhere = aNode.Attributes[xAttribute.dbWhereLOV].Value;
					if (SQLwhere.Length > 0)
					{
						if (SQLwhere.IndexOf(EvoDB.p_itemid) > -1)
						{
							DynamicItemID = true;
							if (_ItemID < 1)
							{
								//If lockDbname <> String.Empty && aNode.Attributes("dbcolumn").Value == lockDbname Then 
								// SQLwhere = SQLwhere.Replace(itemid_str, CStr(_ItemID)) 
								//End If 
								SQLColumnImg = loclValue;
							}
						}
						SQLwhere = SQLwhere.Replace(EvoDB.p_itemid, _ItemID.ToString());
						if (_SecurityModel != EvolSecurityModel.Single_User)
							SQLwhere = SQLwhere.Replace(EvoDB.p_userid, _UserID.ToString());
					}
				}
				if (Lookup == 0 && _UseCache && !DynamicItemID)
				{
					//bug - need to add 
					cacheKey = string.Format("{0}{1}{2}{3}{4}", EvoDB.t_lov, SQLTable, SQLColumn, SQLColumnImg, SQLwhere).ToLower();
					if (Page.Cache[cacheKey] != null)
						Source = (DataSet)Page.Cache[cacheKey];
				}
				if (Source == null)
				{
					if (string.IsNullOrEmpty(ErrorMsg))
					{
						sql = string.Format(EvoDB.SQL_SELECT_LOV, SQLColumn);
						if (!String.IsNullOrEmpty(SQLColumnImg))
							sql += string.Format(EvoDB.SQL_NAME_c0, SQLColumnImg);  // ",[pix]";    
						if (Lookup > 0)
							SQLwhere = EvoDB.IDequals(Lookup);
						if (aNode.Attributes[xAttribute.dbOrderLOV] == null)
							SQLOrderBy = sValue;
						else
						{
							SQLOrderBy = aNode.Attributes[xAttribute.dbOrderLOV].Value;
							if (string.IsNullOrEmpty(SQLOrderBy))
								SQLOrderBy = sValue;
						}
						Source = EvoDB.GetData(EvoDB.BuildSQL(sql, SQLTable, SQLwhere, SQLOrderBy, MaxLoop + 1), SqlConnection, ref ErrorMsg);
						if (Source == null)
						{
							SQLwhere = string.Empty;
							Lookup = 0;
							sql = EvoDB.BuildSQL(sql, SQLTable, SQLwhere, SQLOrderBy, MaxLoop + 1);
							Source = EvoDB.GetData(sql, SqlConnection, ref ErrorMsg);
						}
						if (_UseCache && Lookup < 1 && !DynamicItemID && Source != null)
							Page.Cache[cacheKey] = Source;
					}
				}
				if (Source == null)
				{
					AddError(string.Format(EvoLang.err_NoDataInTable, SQLTable));
					if (FieldName != string.Empty)
						myHTML.Append(EvoLang.err_NoData);
				}
				else
				{
					DataTable t = Source.Tables[0];
					MaxLoop = t.Rows.Count;
					// returns HTML for single or multi-select
					if (format == LOVFormat.HTML)
					{
						// returns HTML for DropDown (for 1 single value)
						if (SingleSelection)
						{
							//DROPDOWN w/out "select" tag around
							myID = EvoTC.String2Int(ItemID);
							if (myID == 0)
								for (int i = 0; i < MaxLoop; i++)
								{
									myHTML.AppendFormat("<option value=\"{0}\">{1}", t.Rows[i][0].ToString(), HttpUtility.HtmlEncode(Convert.ToString(t.Rows[i][1])));
									//& "</option>" 
								}
							else
							{
								for (int i = 0; i < MaxLoop; i++)
								{
									curID = Convert.ToInt32(t.Rows[i][0]);
									myHTML.Append(EvoUI.HTMLOption(curID.ToString(), HttpUtility.HtmlEncode(Convert.ToString(t.Rows[i][1])), myID == curID));
									//& "</option>" 
								}
							}
							if (MaxLoop > maxItem)
								myHTML.AppendFormat("<option>- {0} items maximum -", maxItem);
						}
						// returns HTML for list or list of checkboxes (for multiple values)
						else
						{
							if (MaxLoop > 9) // MultiSelect list
							{
								myHTML.Append("<select multiple size=\"6\" class=\"Field\" onblur=\"javascript:Evol.addFldLabel(this,1)\" name=\"").AppendFormat("{0}\" id=\"{0}\">", FieldName);
								for (int i = 0; i < MaxLoop; i++)
								{
									myHTML.Append(EvoUI.HTMLOption(t.Rows[i][0].ToString(), Convert.ToString(t.Rows[i][1])));
								}
								myHTML.Append("</select>");
							}
							else
							{
								myHTML.Append("<table border=\"0\"><tr valign=\"top\"><td>");
								if (MaxLoop > 4) // 3 columns of checkboxes
								{
									i2 = (MaxLoop + 2) / 3;
									i3 = 2 * i2;
									wBR = true;
								}
								else // line of checkboxes
								{
									i2 = -1;
									i3 = -1;
									wBR = false;
								}
								if (SQLColumnImg != string.Empty)
								{
									for (int i = 0; i < MaxLoop; i++)
									{
										DataRow r = t.Rows[i];
										if (i == i2 || i == i3)
											myHTML.Append("</td><td>&nbsp;&nbsp;&nbsp;</td><td>");
										if (object.ReferenceEquals(r[SQLColumnImg], DBNull.Value))
											myHTML.Append(EvoUI.HTMLInputCheckBox(FieldName, r[0].ToString(), Convert.ToString(r[1]), false, FieldName + i.ToString()));
										else
										{
											pixname = Convert.ToString(r[2]);
											if (!String.IsNullOrEmpty(pixname))
												pixname = EvoUI.HTMLImg(_PathPix + pixname) + EvoUI.HTMLSpace;
											myHTML.Append(EvoUI.HTMLInputCheckBox(FieldName, r[0].ToString(), pixname + Convert.ToString(r[1]), false, FieldName + i.ToString()));
										}
										if (wBR)
											myHTML.Append(EvoUI.tag_BR);
									}
								}
								else
								{
									string CheckboxEnd = wBR ? EvoUI.tag_BR : "</nobr> <nobr>";
									if (!wBR)
										myHTML.Append("<nobr>");
									for (int i = 0; i < MaxLoop; i++)
									{
										if (i == i2 || i == i3)
											myHTML.Append("</td><td>");
										myHTML.Append(EvoUI.HTMLInputCheckBox(FieldName, t.Rows[i][0].ToString(), Convert.ToString(t.Rows[i][1]), false, FieldName + i.ToString())).Append(CheckboxEnd);
									}
									if (!wBR)
										myHTML.Append("</nobr>");
								}
								if (MaxLoop > maxItem2)
									myHTML.AppendFormat(" ({0} items maximum)", maxItem2);
								myHTML.Append(TdTrTableEnd);
							}
						}
					}
					// returns JSON
					else if (format == LOVFormat.JSON)
					{
						myHTML.Append("[");
						for (int i = 0; i < MaxLoop; i++)
						{
							myHTML.Append("{").AppendFormat("id:{0},v:'{1}'", t.Rows[i][0].ToString(), EvoJSON.JSONEncode(Convert.ToString(t.Rows[i][1]))).Append("},");
						}
						if (myHTML.Length > 5)
							myHTML.Remove(myHTML.Length - 1, 1);
						myHTML.Append("]");
					}
				}
				Source = null;
			}
			return myHTML.ToString();
		}

		private string HTMLlovMany(XmlNode aNode, int fieldLOVID, string Link)
		{
			//make a query and returns the HTML for a lov 
			int MaxLoop = 0;
			StringBuilder myHTML = new StringBuilder();
			DataSet Source;
			string sql, lovcolumnid = "ID";
			string SQLTable, SQLTables, SQLColumnMaster = String.Empty, SQLColumnDetails, SQLColumnRead = String.Empty;
			string SP_LOV = null;
			string SQLwhere = String.Empty;
			string SQLOrderBy = sValue;
			string buffer = String.Empty;
			bool r1 = false;

			if (aNode.Attributes[xAttribute.dbTableLOV] == null)
				SQLTable = string.Empty;
			else
				SQLTable = aNode.Attributes[xAttribute.dbTableLOV].Value;
			if (SQLTable != string.Empty)
			{
				if (aNode.Attributes[xAttribute.lovSPlist] == null)
					SP_LOV = string.Empty;
				else
					SP_LOV = aNode.Attributes[xAttribute.lovSPlist].Value;
				//'fieldName = UID & aNode.Attributes(Attr.dbColumn).Value 'now para 
				//cacheKey = ("lov2" & dbtable & SQLTable & fieldLOVID).ToLower() 
				//Source = CType(Page.Cache(cacheKey), DataSet) 
				//If Source Is Nothing Then 
				// If ErrorMsg = String.Empty Then 
				if (string.IsNullOrEmpty(SP_LOV))
				{
					if (aNode.Attributes[xAttribute.dbColumn] != null)
						SQLColumnMaster = aNode.Attributes[xAttribute.dbColumn].Value;
					if (aNode.Attributes[xAttribute.dbColumnReadLOV] != null)
						SQLColumnRead = aNode.Attributes[xAttribute.dbColumnReadLOV].Value;
					if (string.IsNullOrEmpty(SQLColumnRead))
						SQLColumnRead = "name";
					if (aNode.Attributes[xAttribute.dbColumnDetails] != null)
						SQLColumnDetails = aNode.Attributes[xAttribute.dbColumnDetails].Value;
					if (aNode.Attributes["lovcolumnid"] == null)
						lovcolumnid = "ID";
					else
						lovcolumnid = aNode.Attributes["lovcolumnid"].Value;
					if (string.IsNullOrEmpty(lovcolumnid))
						lovcolumnid = "ID";
					//If required Then sql += "ID>0" Else sql = String.Empty 
					sql = string.Format(EvoDB.SQL_SELECT_LOV_T, 100, SQLColumnRead);
					if (aNode.Attributes[xAttribute.dbOrderLOV] != null)
						SQLOrderBy = aNode.Attributes[xAttribute.dbOrderLOV].Value;
					if (string.IsNullOrEmpty(SQLOrderBy))
						SQLOrderBy = sValue;
					if (string.IsNullOrEmpty(SQLColumnMaster))
						SQLTables = SQLTable;
					else
					{
						SQLwhere = string.Format("T.{0}=T1.{0} AND T1.ID<>T.ID AND T1.ID={1}", lovcolumnid, _ItemID);
						if (aNode.Attributes[xAttribute.dbWhereLOV] != null)
							buffer = aNode.Attributes[xAttribute.dbWhereLOV].Value;
						if (buffer != string.Empty)
							SQLwhere += String.Format(" AND {0}", buffer);
						SQLTables = string.Format("{0} T,{1} T1", SQLTable, def_Data.dbtable);
					}
					sql = EvoDB.BuildSQL(sql, SQLTables, SQLwhere, SQLOrderBy, 101);
				}
				else
				{
					SP_LOV = EvoDB.SPcall_Get(SP_LOV, _ItemID, _UserID, fieldLOVID);
					sql = SP_LOV;
				}
				Source = EvoDB.GetData(sql, SqlConnection, ref ErrorMsg);
				// If Not Source Is Nothing Then Page.Cache(cacheKey) = Source 
				// End If 
				//End If 
				if (Source == null)
					ErrorMsg += string.Format("{0} (table {1}).", EvoLang.err_NoData, SQLTable);
				else
				{
					if (Source.Tables[0] != null)
					{
						DataTable t = Source.Tables[0];
						MaxLoop = t.Rows.Count;
						if (string.IsNullOrEmpty(Link))
						{
							myHTML.Append(t.Rows[0][sValue]);
							for (int i = 1; i < MaxLoop; i++)
							{
								myHTML.Append(HTML_Sep).Append(Convert.ToString(t.Rows[i][sValue]));
							}
						}
						else
						{
							r1 = Link.IndexOf(EvoDB.p_itemid) > -1;
							for (int i = 0; i < MaxLoop; i++)
							{
								if (i > 0)
									myHTML.Append(", ");
								if (r1)
								{
									int j = Convert.ToInt32(t.Rows[i][0]);
									if (j != _ItemID)
										myHTML.Append(EvoUI.HTMLLink(Link.Replace(EvoDB.p_itemid, j.ToString()), Convert.ToString(t.Rows[i][sValue])));
									else
										myHTML.Append(t.Rows[i][1]);
								}
								else
									myHTML.Append(EvoUI.HTMLLink(Link, Convert.ToString(t.Rows[i][1])));
							}
							if (MaxLoop > 100)
								myHTML.Append(" (100 Items Maximum)");
						}
					}
				}
				Source = null;
			}
			return myHTML.ToString();
		}

		private string LOVfromCache(string CacheKey, string ItemIDs)
		{
			DataSet ds = null;
			int p, iItemID = 0;
			string buffer = String.Empty;
			string[] LOVtuples;

			string key = CacheKey.ToLower();
			if (Page.Cache[key] != null)
				ds = (DataSet)Page.Cache[key];
			//SHOULD DO BINARY SEARCH WHEN ORDERED LISTS ??) 
			//IS THERE A seek OR find FUNCTION 
			if (ds == null)
				buffer = ItemIDs;
			else
			{
				int MaxLoop1 = ds.Tables[0].Rows.Count;
				p = ItemIDs.IndexOf(coma);
				if (p > -1)
				{
					LOVtuples = ItemIDs.Split(new char[] { ',' });
					int MaxLoop2 = LOVtuples.Length;
					if (MaxLoop2 > 5)
						MaxLoop2 = 5;
					else
						p = -1;
					for (int j = 0; j < MaxLoop2; j++)
					{
						iItemID = Convert.ToInt32(LOVtuples[j]);
						if (iItemID > 0)
						{
							DataTable t = ds.Tables[0];
							for (int i = 0; i < MaxLoop1; i++)
							{
								if (Convert.ToInt32(t.Rows[i][0]) == iItemID)
								{
									buffer = EvoTC.CondiConcat(buffer, Convert.ToString(t.Rows[i][1]), ", ");
									break;
								}
							}
						}
					}
				}
				else
				{
					iItemID = Convert.ToInt32(ItemIDs);
					if (iItemID > 0)
					{
						DataTable t = ds.Tables[0];
						for (int i = 0; i < MaxLoop1; i++)
						{
							if (Convert.ToInt32(t.Rows[i][0]) == iItemID)
							{
								buffer = Convert.ToString(t.Rows[i][1]);
								break;
							}
						}
					}
				}
				ds = null;
				if (p > -1)
					buffer += "...";
			}
			return buffer;
		}

		#endregion

		//### Misc ######################################################################################## 
		#region "Misc"

		private string TXTec(string fLabel, string fType, string fValue, string Operator)
		{
			//returns a "condition" in SQL or plain English 

			//textmultiline is passed as text ! 
			if (fType == EvoDB.t_text)
				switch (Operator)
				{
					case EvoDB.soEqual:
						return string.Format(EvoLang.lEquals, fValue);
					case EvoDB.soStartWith:
						return string.Format(EvoLang.lStart, fValue);
					case EvoDB.soFinishWith:
						return string.Format(EvoLang.lFinish, fValue);
					case EvoDB.soIsNull:
						return string.Format(EvoLang.lIsNull, fLabel);
					case EvoDB.soIsNotNull:
						return string.Format(EvoLang.lIsNotNull, fLabel);
					default: // EvoDB.soContain 
						return string.Format(EvoLang.lContain, fValue);
				}
			else
				switch (Operator)
				{
					case EvoDB.soGreaterThan:
						return ">";
					case EvoDB.soSmallerThan:
						return "<";
					case EvoDB.soIsNull:
						return string.Format(EvoLang.lIsNull, fLabel);
					case EvoDB.soIsNotNull:
						return string.Format(EvoLang.lIsNotNull, fLabel);
					default:
						return "=";
				}
		}

		private void clearSQLCache(string cacheKey)
		{
			if (Convert.ToString(Page.Cache[cacheKey + "_W"]) != string.Empty)
			{
				Page.Cache.Remove(cacheKey + "_W2");
				Page.Cache.Remove(cacheKey + "_W");
				Page.Cache.Remove(cacheKey + "_O");
			}
		}

		#endregion

	}
}