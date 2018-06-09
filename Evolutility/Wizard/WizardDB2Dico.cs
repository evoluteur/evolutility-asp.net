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

namespace Evolutility
{
	partial class Wizard //  DB 2 EvoDico
	{ 
		
		private string FormMapDB() 
		{ 
			StringBuilder myHTML = new StringBuilder(); 
			const string sqlSelect = "so.id, so.name as dbtable,(select count(syscolumns.id) from syscolumns where syscolumns.id=so.id) as nbfields"; 

			ds = EvoDB.GetData(EvoDB.BuildSQL(sqlSelect, "sysobjects as so", whereMapDB, "dbtable", 500), _SqlConnection, ref ErrorMsg); 
			DataTable t0 = ds.Tables[0];
			int ml = t0.Rows.Count;
			if (ml > 0)
			{
				myHTML.Append("<p>Please select the database tables to build UI for:</p><p>");
				for (int i = 0; i < ml; i++)
				{
					DataRow ri = t0.Rows[i];
					string cId = ri["ID"].ToString();
					myHTML.Append(EvoUI.HTMLInputCheckBox("table2map", cId, String.Format("{0} ({1} columns)", ri["dbtable"], ri["nbfields"]), false, cId));
					myHTML.Append("</br>");
				}
				myHTML.Append("</p>");
			}
			else
			{
				return EvoUI.HTMLMessage("No database objects available.", EvoUI.MsgType.Error);
			}
			return myHTML.ToString(); 
		} 
		
		private string FormMapDB_Objects() 
		{ 
			//Strategies (proposal universes like BO)
 
			StringBuilder myHTML = new StringBuilder(); 
			StringBuilder html = new StringBuilder(); 
			string sql, buffer, buffer2, pkName = "ID";  
			int j, f = 0;
			DataSet ds = new DataSet(); 
			string myTable, prevTable; 
			int myFormID = 0, myPanelID = 0; 
			StringBuilder sqlsb = new StringBuilder();
			const string coma = ","; 
			
			sqlsb.Append("SELECT sc.id,so.name as dbtable,sc.name as dbcolumn,sc.xtype,sc.length,sc.isnullable "); 
			sqlsb.Append("FROM sysobjects as so, syscolumns as sc (nolock) "); 
			sqlsb.Append("WHERE sc.id=so.id AND "); 
			sqlsb.Append(whereMapDB);
			if ((GetPageRequest("table2map") != null))
			{ 
				buffer = GetPageRequest("table2map").ToString();
				sqlsb.Append(" AND so.id in (").Append(EvoDB.SQLescape(buffer)).Append(") "); 
			} 
			sqlsb.Append(" order by so.name,sc.colid");
			ds = EvoDB.GetData(sqlsb.ToString(), _SqlConnection, ref ErrorMsg); 
			if (ds != null)
			{ 
				html.Append("<p>Your tables are roughly mapped. The Web UI is ready to use, refine, and tune.</p>"); 
				prevTable = ""; 
				//######### Form ######### 
				int MaxLoop = ds.Tables[0].Rows.Count; 
				html.Append("<ul>"); 
				for (int i = 0; i < MaxLoop; i++)
				{ 
					myTable = ds.Tables[0].Rows[i][xAttribute.dbTable].ToString();
					myTable = myTable.Replace("'", "");  
					if (myTable != prevTable)
					{ 
						//get PK name 
						sqlsb = new StringBuilder(); 
						sqlsb.Append("SELECT CU.Column_Name "); 
						sqlsb.Append("FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CU inner join "); 
						sqlsb.Append(" INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC on CU.Constraint_Name=TC.Constraint_Name "); 
						sqlsb.Append("WHERE CU.table_name = '").Append(myTable).Append("' AND TC.Constraint_Type='PRIMARY KEY' "); 
						sqlsb.Append("ORDER BY CU.Table_Name ");
						pkName = EvoDB.GetDataScalar(sqlsb.ToString(), _SqlConnection, ref ErrorMessage).ToUpper(); 
						if(string.IsNullOrEmpty(pkName))
							pkName = "ID"; 
						//+ dborder, spget, spdelete, entity, xmlfile) " 
						sqlsb = new StringBuilder();
						sqlsb.AppendFormat("'{0}','{1}','{2}',", EvoTC.ToUpperLowers(myTable.Replace("_", " ")), myTable, pkName); 
						sqlsb.AppendFormat("'Definition obtained from scan table ''{0}'' with Evolutility wizard on {1}.'", myTable, EvoTC.TextNow()); 
						sqlsb.Append(",'").Append(PagingSPCall);
						sqlsb.Append("','EvoSP_Login @login, @password'"); 
						sql = EvoDB.sqlINSERT("EvoDico_form", "title, dbtable, dbcolumnpk, description, sppaging, splogin", sqlsb.ToString());
						sql += EvoDB.SQL_IDENTITY;
						myFormID = EvoTC.String2Int(EvoDB.GetDataScalar(sql, _SqlConnectionDico, ref ErrorMessage));
						if (myFormID > 0) 
						{ 
							//######### Panel ######### 
							myPanelID = EvoTC.String2Int(EvoDB.GetDataScalar("SELECT max(ID) FROM EvoDico_Panel WHERE FormID=" + myFormID.ToString(), _SqlConnectionDico, ref ErrorMessage)); 
							if (myPanelID == 0) 
							{
								sqlsb = new StringBuilder();
								sqlsb.Append("INSERT INTO EvoDico_Panel (FormID, label, Width) VALUES(");
								sqlsb.AppendFormat("{0},'{1}',100)", myFormID, myTable);
								sqlsb.Append(EvoDB.SQL_IDENTITY);
								myPanelID = EvoTC.String2Int(EvoDB.GetDataScalar(sqlsb.ToString(), _SqlConnectionDico, ref ErrorMessage));
							}
						} 
						else 
						{
							ErrorMsg += "BAD SQL<br/>" + sql; 
						} 
						html.AppendFormat("<li>{0} : ",myTable);
						html.Append("&nbsp;<a href=\"evodicoTest.aspx?formID=").Append(myFormID).Append("\" target=\"r\">Run").Append(EvoUI.HTMLFlagPopup).Append("</a>");
						html.Append(" - <a href=\"evodicoForm.aspx?ID=").Append(myFormID).Append("\" target=\"d\">Design").Append(EvoUI.HTMLFlagPopup).Append("</a></li>"); 
						prevTable = myTable; 
						f = 0; 
					} 
					//######### Fields ######### 
					//'buffer = CStr(ds.Tables[0].Rows[i][xAttribute.dbColumn)) 
					buffer = ds.Tables[0].Rows[i][xAttribute.dbColumn].ToString(); 
					if (buffer.ToUpper() != pkName) 
					{ 
						f += 1; 
						sqlsb = new StringBuilder(); 
						sqlsb.Append("INSERT INTO EvoDico_Field (FormID, fpos, label, dbcolumn, dbcolumnread, typeid, height, width, maxlength, required, panelid, search, searchlist, searchadv)"); 
						sqlsb.AppendFormat(" VALUES ({0},", myFormID); 
						if (buffer == "title")
							sqlsb.Append("5,"); 
						else if (buffer == "name") 
							sqlsb.Append("1,"); 
						else 
							sqlsb.AppendFormat("{0},", i * 10);
						buffer2 = buffer.Replace("_", " ").Replace("'", "''"); 
						j = buffer2.Length - 1; 
						if (j > 0)
							buffer2 = EvoTC.ToUpperLowers(buffer2);  
						sqlsb.AppendFormat("'{0}','{1}','{1}',", buffer2, buffer); 
						j = -1;
						int maxlength = EvoTC.String2Int(ds.Tables[0].Rows[i]["length"].ToString()); 
						switch (EvoTC.String2Int(ds.Tables[0].Rows[i]["xtype"].ToString())) 
						{ 
							case 127: 
							case 56: 
							case 52: 
							case 48: 
							case 36: 
								sqlsb.Append("10,1,20");  //"integer" 
								maxlength = 12;
								break; 
							case 104: 
								sqlsb.Append("1,1,20");  //bool 
								maxlength = 0;
								break; 
							case 61: 
								sqlsb.Append("17,1,30");  // t_datetime 
								maxlength = 25;
								break; 
							case 106: 
							case 62: 
							case 60: 
							case 108: 
							case 59: 
							case 122: 
								sqlsb.Append("9,1,20");  //t_dec 
								maxlength = 12;
								break; 
							case 58: 
								sqlsb.Append("2,1,30");  //t_date 
								maxlength = 20;
								break; 
							case 189: 
								sqlsb.Append("18,1,30");  // t_time 
								maxlength = 12;
								break; 
							case 99: 
							case 35: 
								sqlsb.Append("6,3,100");  // t_txtm 
								maxlength = maxlength / 2; 
								break; 
							default: 
								//175, 239, 231, 167 
								maxlength = maxlength / 2;
								if (maxlength < 100)
									sqlsb.Append("5,1,50");  //"text" 
								else if (maxlength < 200)
									sqlsb.Append("5,1,100");  //"text" 
								else if (maxlength < 500)
									sqlsb.Append("6,3,100");  //"textmul" 
								else
									sqlsb.Append("6,5,100");  //"textmul"
								break; 
						}  
						sqlsb.AppendFormat(",{0},", maxlength);
						if (Convert.ToInt32(ds.Tables[0].Rows[i]["isnullable"]) > 0)
							sqlsb.Append("0,"); 
						else 
							sqlsb.Append("1,"); 
						sqlsb.Append(myPanelID + coma); 
						if (f < 6)
							sqlsb.Append("1,1");   //search, searchlist 
						else if (f < 20) 
							sqlsb.Append("1,0"); 
						else 
							sqlsb.Append("0,0"); 
						sqlsb.Append(",1)");
						buffer = EvoDB.RunSQL(sqlsb.ToString(), _SqlConnectionDico, false); 
						if (buffer != string.Empty)
							ErrorMsg += string.Format("BAD SQL<br/>{0}<br/>{1}", sqlsb.ToString(), buffer); 
					} 
				} 
				html.Append("</ul>"); 
			} 
			else 
				html.Append(EvoUI.HTMLMessage("No database tables were found. There is nothing to map." , EvoUI.MsgType.Warn)); 
			html.Append("<br/>&nbsp;"); 
			return html.ToString(); 
		} 		

	}
}
