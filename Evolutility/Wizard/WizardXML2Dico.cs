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
	partial class Wizard //  XML 2 EvoDico
	{

		private string FormXML2DB()
		{
			StringBuilder myHTML = new StringBuilder();

			myHTML.Append("<p>Please provide the XML of your application component.</p><p>");
			myHTML.Append(HTMLFieldTextArea("XML", "XML"));
			myHTML.Append("</p>");
			return myHTML.ToString();
		}

		private string FormXML2DBNow(string sqlConnectionDico)
		{
			return dicoXML2DB(GetPageRequest("XML").ToString(), SqlConnectionDico);
		}

		private string dicoXML2DB(string XML, string sqlConnectionDico)
		{
			string sqlstart1, sqlstart2;
			int FormID = 0, PanelID;
			int maxLoopP = 0;
			string sqllog = null; 

			Data def_Data = new Data();
			StringBuilder allSQL = new StringBuilder();
			try
			{
				myDOM.LoadXml(XML);
			}
			catch
			{
				allSQL = new StringBuilder();
				allSQL.Append(EvoUI.HTMLMessage("<p>Invalid XML. Please validate your XML with Evolutility.xsd.</p>", EvoUI.MsgType.Error));
				//allSQL.Append(HttpUtility.HtmlEncode(XML)); 
				return allSQL.ToString();
			}
			XmlNamespaceManager nsManager = new XmlNamespaceManager(new NameTable());
			nsManager.AddNamespace("evo", xQuery.evoNameSpace);
			XmlNode aNode = myDOM.DocumentElement;
			if (aNode != null && aNode.Name == xElement.form)
			{
				if (aNode.Attributes[xAttribute.label] != null)
				{
					def_Data.title = aNode.Attributes[xAttribute.label].InnerXml;
				}
				try
				{
					def_Data = Data.Deserialize(aNode[xElement.data]);
				}
				catch
				{
					ErrorMsg = "Invalid XML file. The element 'data' must have attributes.";
				}
			}
			StringBuilder sbSQL = new StringBuilder();
			StringBuilder sqlFull = new StringBuilder();
			if (string.IsNullOrEmpty(ErrorMsg))
			{
				//######### Form ######### 
				sbSQL.Append("INSERT INTO EvoDico_Form(title,dbtable,dbcolumnpk,dbcolumnlead,dbwhere,dborder,sppaging,splogin,spget,spdelete,entity,entities,icon,description) ");
				sbSQL.Append("VALUES('").Append(EvoDB.SQLescape(def_Data.title)).AppendFormat("','{0}','{1}','{2}','{3}','{4}','", def_Data.dbtable, def_Data.dbcolumnpk, def_Data.dbcolumnlead, def_Data.dbwhere, def_Data.dborder);
				sbSQL.Append(EvoDB.SQLescape(def_Data.sppaging));
				sbSQL.Append("','").Append(EvoDB.SQLescape(def_Data.splogin)).Append("','").Append(EvoDB.SQLescape(def_Data.spget)).Append("','");
				sbSQL.Append(EvoDB.SQLescape(def_Data.spdelete)).AppendFormat("','{0}','{1}','{2}','", EvoDB.SQLescape(def_Data.entity), EvoDB.SQLescape(def_Data.entities), EvoDB.SQLescape(def_Data.icon));
				sbSQL.AppendFormat("XML import on {0}')", EvoTC.TextNowTime());
				sbSQL.Append(EvoDB.SQL_IDENTITY);
				FormID = EvoTC.String2Int(EvoDB.GetDataScalar(sbSQL.ToString(), sqlConnectionDico, ref ErrorMessage));
				if (FormID > 0 && string.IsNullOrEmpty(ErrorMsg))
				{
					//######### Panels ######### 
					XmlNodeList aNodeListPanels = myDOM.DocumentElement.SelectNodes(xQuery.panel, nsManager);
					maxLoopP = aNodeListPanels.Count - 1;
					if (maxLoopP == -1)
					{
						aNodeListPanels = myDOM.DocumentElement.SelectNodes("//evo:tab/evo:panel", nsManager);
						maxLoopP = aNodeListPanels.Count - 1;
					}
					sqlFull = new StringBuilder();
					for (int p = 0; p <= maxLoopP; p++)
					{
						XmlNode pNode = aNodeListPanels[p];
						sbSQL = new StringBuilder();
						sbSQL.Append("INSERT INTO EvoDico_Panel (FormID, label, Width, cssclass, cssclasslabel) VALUES(");
						sbSQL.AppendFormat("{0},", FormID);
						sbSQL.AppendFormat("'{0}',", EvoDB.SQLescape(pNode.Attributes[xAttribute.label].Value));
						sbSQL.AppendFormat("{0},", pNode.Attributes[xAttribute.width].Value);
						if (pNode.Attributes[xAttribute.cssClass] != null)
						{
							sbSQL.AppendFormat("'{0}',", EvoDB.SQLescape(pNode.Attributes[xAttribute.cssClass].Value));
						}
						else
						{
							sbSQL.Append("'',");
						}
						if (pNode.Attributes[xAttribute.cssClassLabel] != null)
							sbSQL.AppendFormat("'{0}')", EvoDB.SQLescape(pNode.Attributes[xAttribute.cssClassLabel].Value));
						else
							sbSQL.Append("'')");
						sbSQL.Append(EvoDB.SQL_IDENTITY);
						PanelID = EvoTC.String2Int(EvoDB.GetDataScalar(sbSQL.ToString(), sqlConnectionDico, ref ErrorMessage));
						if (PanelID > 0 && string.IsNullOrEmpty(ErrorMsg))
						{
							//######### Fields ######### 
							sqlstart1 = "INSERT INTO EvoDico_Field (FormID,";
							sqlstart2 = string.Format(") VALUES ({0},", FormID);
							foreach (XmlNode aNode2 in pNode.ChildNodes)
							{
								StringBuilder sbSQL1 = new StringBuilder();
								StringBuilder sbSQL2 = new StringBuilder();
								sbSQL1.Append(sqlstart1);
								sbSQL2.Append(sqlstart2);
								foreach (XmlAttribute aAttribute2 in aNode2.Attributes)
								{
									string buffer2 = aAttribute2.Name;
									string buffer = aAttribute2.Value;
									switch (buffer2)
									{
										case "panelid":
											break;
										default:
											if (EvoTC.isInteger(buffer))
											{
												sbSQL1.AppendFormat("[{0}],", buffer2);
												sbSQL2.AppendFormat("{0},", buffer);
											}
											else
											{
												if (buffer2 == xAttribute.type)
												{
													sbSQL1.Append("typeid,");
													sbSQL2.AppendFormat("{0},", FieldTypeID(buffer));
												}
												else if (buffer != string.Empty)
												{
													sbSQL1.AppendFormat("[{0}],", buffer2);
													sbSQL2.AppendFormat("'{0}',", EvoDB.SQLescape(buffer));
												}
											}
											break;
									}
								}
								sqlFull.Append(sbSQL1).Append("PanelID").Append(sbSQL2).Append(PanelID).Append(");\n");
							}
						}
					}
					string buff = EvoDB.RunSQL(sqlFull.ToString(), sqlConnectionDico, true);
					if (!string.IsNullOrEmpty(buff))
						ErrorMsg += buff;
					//'######### Panels Details ######### 
					//aNodeListPanels = myDOM.DocumentElement.SelectNodes("//panel-details", nsManager) 
					//maxLoopP = aNodeListPanels.Count - 1 
					//sqlstart1 = "INSERT INTO EvoDico_FieldDetails (FormID," 
					//For p = 0 To maxLoopP 
					// With aNodeListPanels[p] 
					// Sql = "INSERT INTO EvoDico_Panel (TypeID, FormID, label, Width, dbtabledetails, dbcolumndetails) VALUES(2," & CInt(FormID) & ",'" 
					// Sql += .Attributes(xAttribute.label).Value & "'," & .Attributes(xAttribute.width).Value & ",'" & .Attributes(xAttribute.dbTableDetails).Value & "','" & .Attributes(xAttribute.dbColumnDetails).Value & "')" 
					// Buffer = EvoDB.RunSQL(Sql, sqlConnectionDico, True) 
					// If Buffer = "" Then 
					// PanelID = CInt(GetDataScalar(EvoDB.BuildSQL("max(ID)", "EvoDico_Panel", "TypeID=2 AND FormID=" & CStr(FormID)), sqlConnectionDico)) 
					// '######### Fields Details ######### 
					// sqlstart2 = ") VALUES (" & FormID & coma 
					// sqlFull = "" 
					// For Each aNode In .ChildNodes 
					// Sql = sqlstart1 
					// sql2 = sqlstart2 
					// For Each aAttribute In aNode.Attributes 
					// Buffer = aAttribute.Value 
					// dbcolumn = aAttribute.Name 
					// If InStr("-type-panelid-panelindex-label-dbcolumn-dbcolumnread-dbcolumnimg-dbtablelov-dborderlov-dbcolumnreadlov-dblovcolumn-dbwherelov-validationrule-maxlength-readonly-required-optional-format-fpos-link-linklabel-linktarget-searchlist-cssclass-width-", "-" & LCase(dbcolumn) & "-") > 0 Then 
					// If IsNumeric(Buffer) AndAlso Not dbcolumn = xAttribute.format Then 
					// Sql += dbcolumn & coma 
					// sql2 += Buffer & coma 
					// Else 
					// If aAttribute.Name = xAttribute.type Then 
					// Sql += "typeid," 
					// sql2 += FieldTypeID(Buffer) & coma 
					// Else 
					// If Buffer <> String.Empty Then 
					// Sql += "[" & dbcolumn & "]," 
					// sql2 += "'" & EvoDB.SQLescape(Buffer) & "'," 
					// End If 
					// End If 
					// End If 
					// End If 
					// Next 
					// 'If InStr(sql, "panelid") = 0 Then 
					// ' sql += "PanelID," 
					// ' sql2 += CStr(Val(PanelID)) 
					// 'End If 
					// sqlFull += Sql & "userid" & sql2 & UserID & ");" 
					// Next 
					// End If 
					// End With 
					// Buffer = EvoDB.RunSQL(sqlFull, sqlConnectionDico, True) 
					// If Buffer <> String.Empty Then sqllog += Buffer & vbCrLf & Sql & vbCrLf2 
					//Next 
					//End If 
					if (!string.IsNullOrEmpty(sqllog))
						ErrorMsg = sqllog.Replace("\n\n\n", "");
				}
			}
			string retVal = String.Format("{0} {1}", def_Data.title, HTMLToolsLink(FormID.ToString(), ""));
			def_Data = null;
			nsManager = null;
			myDOM = null;
			return retVal;
		}

		private string FieldTypeID(string TypeName)
		{
			switch (TypeName)
			{
				case "text":
					return "5";
				case "boolean":
					return "1";
				case "date":
					return "2";
				case "email":
					return "3";
				case "lov":
					return "4";
				case "textmultiline":
					return "6";
				case "url":
					return "7";
				case "html":
					return "8";
				case "decimal":
					return "9";
				case "integer":
					return "10";
				case "image":
					return "11";
				case "document":
					return "12";
				case "hidden":
					return "13";
				default:
					return "5";
			}
		}

	}
}
