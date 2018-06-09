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
using System.Data;
using System.Xml;

namespace Evolutility
{
	partial class Wizard //  Install
	{

		private string FormInstall()
		{

			StringBuilder myHTML = new StringBuilder();
			bool xmlOK = false;
			XmlDocument myDOM2 = new XmlDocument();
			try
			{
				myDOM2.Load(FileNameWithMask(_PathXML + "Wizard_Install.xml"));
				xmlOK = true;
			}
			catch (Exception ex)
			{
				myHTML.Append(EvoUI.HTMLMessage("XML not found or invalid.", EvoUI.MsgType.Error));//.Append(ex.Message);
			}
			if (xmlOK)
			{
				XmlNode nApps = myDOM2.SelectSingleNode("wizard_install");
				if (nApps != null)
				{
					foreach (XmlNode nGroup in nApps.ChildNodes)
					{
						if (nGroup.NodeType == XmlNodeType.Element && nGroup.Name == "appgroup")
						{
							myHTML.AppendFormat("<p>{0}:</p>", nGroup.Attributes[xAttribute.label].Value);
							foreach (XmlNode nApp in nGroup.ChildNodes)
							{
								string icon = String.Empty;
								if (nApp.Attributes[xAttribute.icon] != null)
									icon = nApp.Attributes[xAttribute.icon].Value;
								if (!String.IsNullOrEmpty(icon))
									icon = EvoUI.HTMLIcon(_PathPix, icon);
								string label = icon + nApp.Attributes[xAttribute.label].Value;
								string id = nApp.Attributes["file"].Value;
								if (id.Length > 0)
								{
									id = id.Substring(0, id.Length - 4);
									myHTML.Append(EvoUI.HTMLInputCheckBox("frmID", id, label, false, id));
									myHTML.Append(EvoUI.tag_BR);
								}
							}
						}
					}
				}
				else
					myHTML.Append(EvoUI.HTMLMessage("XML is invalid.", EvoUI.MsgType.Error));
			}
			myDOM = null;
			myHTML.Append(EvoUI.tag_BR);
			return myHTML.ToString();
		}

		private string FormInstall_Build()
		{
			StringBuilder myHTML = new StringBuilder();
			string[] a1;

			if (!string.IsNullOrEmpty(GetPageRequest("frmID")))
			{
				string appIDs = GetPageRequest("frmID").ToString();
				a1 = appIDs.Split(new char[] { ',' });
				int ml = a1.Length;
				for (int i = 0; i < ml; i++)
				{
					myHTML.Append(InstallApp(a1[i]));
				}
			}
			return myHTML.ToString();
		}

		private string InstallApp(string XMLDefFile)
		{
			StringBuilder myHTML = new StringBuilder();
			bool xmlOK = true;
			string sql, dbr = null, errorMsg = null;
			string checkMark = EvoUI.HTMLImgCheckMark(IEbrowser, _PathPix);

			myHTML.AppendFormat("<h2>{0}</h2>", XMLDefFile.Replace("_", ""));
			if (!string.IsNullOrEmpty(XMLDefFile))
			{
				XmlDocument myDOM2 = new XmlDocument();
				try
				{
					myDOM2.Load(FileNameWithMask(_PathXML + string.Format("Setup\\{0}.xml", XMLDefFile)));
				}
				catch (Exception ex)
				{
					errorMsg = "XML not found or invalid";
					xmlOK = false;
				}
				if (xmlOK)
				{
					string t = "";
					XmlNode aNode = myDOM2.DocumentElement;
					if (aNode.Name == "applicationcomponent")
					{
						sql = aNode["xml"].InnerText;
						if (sql.StartsWith("<?xml"))
							sql = xQuery.XMLHeader + sql;
						aNode = aNode["sql"];
						if (aNode.Attributes[xAttribute.dbTable] != null)
						{
							t = aNode.Attributes[xAttribute.dbTable].Value;
							myHTML.AppendFormat("<p>DB Table: {0}</p>", t);
						}
						dicoXML2DB(sql);
						myHTML.Append("<div class=\"indent1\">");
						if (string.IsNullOrEmpty(errorMsg))
							myHTML.Append(checkMark).Append("Metadata<br>");
						if (aNode.Attributes[xAttribute.dbTable] != null)
						{
							string tc = EvoDB.GetDataScalar(EvoDB.BuildSQL("count(*)", t), _SqlConnection, ref ErrorMsg);
							if (EvoTC.isInteger(tc))
								errorMsg = "Database table not created because it already exists.";
							else
							{
								sql = aNode["create"].InnerText;
								dbr = EvoDB.RunSQL(sql, _SqlConnection, false);
								if (string.IsNullOrEmpty(dbr))
									myHTML.Append(checkMark).Append("Database structure<br>");
								else
									errorMsg = dbr;
							}
							if (string.IsNullOrEmpty(dbr))
							{
								int i = EvoTC.String2Int(tc);
								if (i == 0)
								{
									sql = aNode["seed"].InnerText;
									dbr = EvoDB.RunSQL(sql, _SqlConnection, false);
									if (string.IsNullOrEmpty(dbr))
									{
										myHTML.Append(checkMark).Append("Seed data<br>");
										sql = aNode["sample"].InnerText;
										dbr = EvoDB.RunSQL(sql, _SqlConnection, false);
										if (string.IsNullOrEmpty(dbr))
											myHTML.Append(checkMark).Append("Sample data<br>");
										else
											errorMsg = dbr;
									}
								}
							}
							else
								errorMsg = dbr;
						}
						myHTML.Append("</div>");
					}
				}
				if (!string.IsNullOrEmpty(errorMsg))
					myHTML.Append(EvoUI.HTMLMessage(errorMsg, EvoUI.MsgType.Error));
			}
			myHTML.Append(HTMLToolsLink(FormID.ToString(), "")).Append("</p>");
			return myHTML.ToString();
		}

	}
}

