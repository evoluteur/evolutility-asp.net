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
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Xml; 

namespace Evolutility.DataServer
{

	public class DataServer : IHttpHandler
	{

 //### Variables ### 
#region "Variables" 

		private string ErrorMsg = string.Empty;
		private string _SqlConnection;

#endregion

		// Override the ProcessRequest method.
		public void ProcessRequest(HttpContext context)
		{
			CheckSqlConnections();
			String action = "";
			if (!string.IsNullOrEmpty(context.Request.Params["action"]))  
			{ 
				action = context.Request.Params["action"].ToString();
				switch (action)
				{
					case "getlov":
						context.Response.Write(getLOV(context));
						break;
					case "gethelp":
						context.Response.Write(getHelp(context));
						break;
				}
			}
		}

		// Override the IsReusable property.
		public bool IsReusable
		{
			get { return true; }
		}

		//### LOV ############################################################################################ 
		#region "LOV"

		private string getLOV(HttpContext context)
		{
			string fm="", fs="", cid="0", sqlw = "";
			if (context.Request["fs"] != null)
			{
				fs = TrimEvolu(context.Request["fs"].ToString());
				if (context.Request["fm"] != null)
					fm = TrimEvolu(context.Request["fm"].ToString());
				if (context.Request["id"] != null)
					cid = context.Request["id"].ToString();
			}
			string sql="";
			if (fm == "FORMID" && fs == "PANELID")
			{
				sqlw = String.Format("t.formID={0}", cid);
				sql = EvoDB.BuildSQL(string.Format(EvoDB.SQL_SELECT_LOV, "label"), "EvoDico_Panel t", sqlw, "", 300);
			}
			else if (fm == "COUNTRYID" && fs == "CITYID")
			{
				sqlw = String.Format("t.{1}={0}", cid, fm);
				sql = EvoDB.BuildSQL(string.Format(EvoDB.SQL_SELECT_LOV, "name"), "dep_City t", sqlw, "", 300);
			} 
			StringBuilder sb = new StringBuilder();
			if (!String.IsNullOrEmpty(sql))
			{
				DataSet ds = EvoDB.GetData(sql, _SqlConnection, ref ErrorMsg);
				if (ds != null)
				{
					DataTable t = ds.Tables[0];
					int MaxLoop1 = t.Rows.Count; 
					if(MaxLoop1>0){
						sb.Append("[{id:'0',v:' - '},");
						for (int i = 0; i < MaxLoop1; i++)
						{
							sb.Append("{").AppendFormat("id:'{0}',", t.Rows[i][0].ToString());
							if (t.Rows[i][1] != null)
								sb.AppendFormat("v:'{0}'", HttpUtility.HtmlEncode(t.Rows[i][1].ToString()).Replace("\n\r", "\\n").Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
							else
								sb.Append("v:''");
							sb.Append("},"); 		
						}
						sb.Remove(sb.Length - 1, 1);
						sb.Append("]");
					}
					else
						sb.Append("[]");
			}
			if (ds != null)
				ds.Dispose();
			}
			return sb.ToString();
		}

		#endregion

		//### Help ############################################################################################ 
		#region "Help"

		private string getHelp(HttpContext context)
		{
			string frmID = "";
			if (context.Request["formid"] != null)
			{
				frmID = TrimEvolu(context.Request["formid"].ToString());
				if (EvoTC.isInteger(frmID))
					return ""; //for now
				else
					return HelpXML2JSON(context.Server.MapPath(frmID));
					//return "[{id:'Title',help:'example:\"Address book\"'},{id:'entity', help:'example: \"contact\"'},{id:'entities', help:'example: \"contacts\"'},{id:'Help', help:'Help on the field (for edition)'},{id:'icon', help:'example=\"contact.gif\"'},{id:'dbtable', help:'Driving table'},{id:'dbwhere', help:'Example \"CategoryID=3\"'},{id:'dborder', help:'Use \"T\" for driving table alias. Example \"T.lastname,T.firstname\"'},{id:'dbcolumnlead', help:'Title column'},{id:'dbcolumnpk', help:'Usually ID'},{id:'dbColumnicon', help:'Column used to store the name of a thumbnail or icon specific to each record'},{id:'dbtableusers', help:'Table storing users'},{id:'dbtablecomments', help:'Table storing user comments'},{id:'spPaging', help:'Stored Procedure used for displaying selection lists'},{id:'spLogin', help:'Stored Procedure used for user login'},{id:'spGet', help:'Stored Procedure used to get a single record'},{id:'spDelete', help:'Stored Procedure used to delete or disable a record'}]";
			}
			return "Invalid ID";
		}

		private string HelpXML2JSON(string PathXML)
		{
			StringBuilder sb = new StringBuilder();
			if (!string.IsNullOrEmpty(PathXML))
			{
				XmlDocument myDOM = new XmlDocument();
				myDOM.Load(PathXML);
				XmlNamespaceManager nsManager = new XmlNamespaceManager(new NameTable());
				nsManager.AddNamespace("evo", xQuery.evoNameSpace); 
				XmlNodeList aNodeList = myDOM.DocumentElement.SelectNodes(xQuery.panelField+"[@help!='']", nsManager);
				int maxLoop = aNodeList.Count;
				sb.Append("[ ");
				for (int i = 0; i < maxLoop; i++)
				{
					XmlNode cn = aNodeList[i];
					if (cn.Attributes[xAttribute.help] != null){
						sb.Append("{").AppendFormat("id:'{0}',", EscapedJSON1(cn.Attributes[xAttribute.dbColumn].Value));
						sb.AppendFormat("help:'{0}'", EscapedJSON2(cn.Attributes[xAttribute.help].Value)).Append("},");
					}
				}
				sb.Remove(sb.Length - 1, 1);
				sb.Append("]");
				myDOM = null; 
			}
			return sb.ToString();
		}

		private string HelpDB2JSON(int FormID)
		{
			StringBuilder sb = new StringBuilder();
			if (FormID>0)
			{
				DataSet ds = EvoDB.GetData("EXEC EvoDico_Form_GetHelp 147,1", _SqlConnection, ref ErrorMsg);
				sb.Append("[ ");
				if (ds != null)
				{
					DataTable t = ds.Tables[0];
					int MaxLoop1 = t.Rows.Count;
					if (MaxLoop1 > 0)
					{
						for (int i = 0; i < MaxLoop1; i++)
						{							
							if (t.Rows[i][1] != null){
								sb.Append("{").AppendFormat("id:'{0}',", EscapedJSON1(t.Rows[i][0].ToString()));
								sb.AppendFormat("help:'{1}'", EscapedJSON2(t.Rows[i][1].ToString())).Append("},");
							}
						}
						sb.Remove(sb.Length - 1, 1);
					}
				}
				sb.Append("]");
				if (ds != null)
					ds.Dispose();
			}
			return sb.ToString();
		}

		#endregion

		//### Misc. ############################################################################################ 
		#region "Misc"

		private void CheckSqlConnections()
		{
			if (string.IsNullOrEmpty(_SqlConnection))
			{
				_SqlConnection = GetAppSetting("SQLConnection");
			}
		}

		static internal string TrimEvolu(string s)
		{
			if (s.Length > 6 && s.Substring(0, 6) == "EVOLU_")
				return s.Substring(6, s.Length - 6).ToUpper();
			else
				return s.ToUpper();
		}

		static internal string GetAppSetting(string key)
		{
			if (System.Configuration.ConfigurationManager.AppSettings[key] != null)
				return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
			else
				return string.Empty;
		}

		private string EscapedJSON1(string v)
		{
			return HttpUtility.HtmlEncode(v).Replace("'", "\\'");
		} 
		private string EscapedJSON2(string v)
		{
			return HttpUtility.HtmlEncode(v).Replace("\n\r", "\\n").Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
		}  

		#endregion
	}
}
