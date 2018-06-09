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


//#define DB_MySQL
#undef DB_MySQL

using System.Xml;
using System.Data;

#if DB_MySQL
using MySql.Data;
using MySql.Data.MySqlClient;
#else
using System.Data.SqlClient;
#endif


namespace Evolutility
{
	// ==================   Evolutility Dictionary EvoDico   ==================   
	// UNDER CONSTRUCTION
	/* 
	This library is a dependency of : 
	 * Evolutility.UIServer 
	 * Evolutility.Wizard 
	*/

	static class EvoDico
	{
		

//### EvoDico DB 2 XML ############################################################################################ 
#region "EvoDico Form"

		internal static DataSet GetForm(int FormID, int UserID, string sqlConnectionDico)
		{
			/// <summary>Generate XML from DB dico query.</summary>

			string errorMsg = null;
			DataSet ds = new DataSet();

			string sql = string.Format("EXEC EvoDico_Form_Get @FormID, {0}", EvoDB.p_userid);
#if DB_MySQL
			ds = EvoDB.GetDataParameters(sql, sqlConnectionDico, new MySqlParameter[] { new MySqlParameter("@FormID", FormID), new MySqlParameter(EvoDB.p_userid, UserID) }, ref errorMsg);
#else
			ds = EvoDB.GetDataParameters(sql, sqlConnectionDico, new SqlParameter[] { new SqlParameter("@FormID", FormID), new SqlParameter(EvoDB.p_userid, UserID) }, ref errorMsg);
#endif
			return ds;
		}

		internal static string dicoDB2XML(int FormID, int UserID, bool showIDs, string sqlConnectionDico)
		{
			/// <summary>Generate XML from DB dico query.</summary>
 
			string errorMsg = null;
			DataSet ds = new DataSet();

			ds = GetForm(FormID, UserID, sqlConnectionDico);
 
			if (ds != null)
			{
				//built hierarchical relationships between recordsets 
				ds.DataSetName = "EvoL";
				//bool UseTabs = false;
				//bool UseDetails = false;
				int MaxLoop = ds.Tables.Count;
				if (MaxLoop > 0)
				{
					//'UseTabs = false 
					//'UseDetails = false 
					//'Select Case MaxLoop 
					//' Case 3 'tabs - no details 
					//' UseTabs = True 
					//' Case 4 'no tabs - details 
					//' UseDetails = True 
					//' Case Is > 4 'tabs - details 
					//' UseTabs = True 
					//' UseDetails = True 
					//'End Select 
					ds.Tables[0].TableName = xElement.form;
					ds.Tables[0].Namespace = xQuery.evoNameSpace;
					ds.Tables[1].TableName = xElement.data;
					ds.Tables[2].TableName = xElement.panel;
					ds.Tables[3].TableName = xElement.field;
					//'If UseTabs Then 
					//' .Tables(4).TableName = "tab" 
					//' If UseDetails Then 
					//' .Tables(5).TableName = "paneldetails" 
					//' .Tables(6).TableName = "fielddetails" 
					//' End If 
					//'Else 
					//' If UseDetails Then 
					//' .Tables(4).TableName = "paneldetails" 
					//' .Tables(5).TableName = "fielddetails" 
					//' End If 
					//'End If  
					for (int i = 0; i < MaxLoop; i++)
					{
						DataTable t = ds.Tables[i];
						int cMaxLoop = t.Columns.Count - 1;
						for (int j = 0; j <= cMaxLoop; j++)
						{
							t.Columns[j].ColumnName = t.Columns[j].ColumnName.ToLower();
							t.Columns[j].ColumnMapping = MappingType.Attribute;
						}
						if (i != 3)
							t.Columns["id"].ColumnMapping = MappingType.Hidden;
						switch (i)
						{
							case 2: // panel 
								t.Columns["typeid"].ColumnMapping = MappingType.Hidden;
								t.Columns["formid"].ColumnMapping = MappingType.Hidden;
								t.Columns["tabid"].ColumnMapping = MappingType.Hidden;
								t.Columns["ppos"].ColumnMapping = MappingType.Hidden;
								t.Columns["userid"].ColumnMapping = MappingType.Hidden;
								break;
							case 3: //field 
								//t.Columns["options"].ColumnMapping = MappingType.Hidden;
								//t.Columns[xAttribute.elem_form].ColumnMapping = MappingType.Hidden;
								t.Columns["formid"].ColumnMapping = MappingType.Hidden;
								t.Columns["panelid"].ColumnMapping = MappingType.Hidden;
								t.Columns["userid"].ColumnMapping = MappingType.Hidden;
								t.Columns["typeid"].ColumnMapping = MappingType.Hidden;
								t.Columns["typepix"].ColumnMapping = MappingType.Hidden;
								t.Columns["fpos"].ColumnMapping = MappingType.Hidden;
								break;
							//Case 0 'form 
							// t.Columns["userid"].ColumnMapping = MappingType.Hidden;
							// t.Columns[xAttribute.elem_form].ColumnMapping = MappingType.Hidden;
							//Case 4 'tab 
							// If UseTabs Then 
							// .Columns("paneltypeid").ColumnMapping = MappingType.Hidden 
							// End If 
						}
						//If i > 1 Then .Columns("formid").ColumnMapping = MappingType.Hidden 
						//If i > 3 And i < 5 Then .Columns("panelid").ColumnMapping = MappingType.Hidden 
					}
					//'ds.Tables("paneldetails").Columns("id").ColumnMapping = MappingType.Attribute 
					//'ds.Tables("paneldetails").Columns("id").ColumnName = "panelid" 
					{
						DataRelation rel1 = ds.Relations.Add("form-data", ds.Tables[xElement.form].Columns["id"], ds.Tables[xElement.data].Columns["id"]);
						rel1.Nested = true;
						DataRelation rel2 = ds.Relations.Add("panel-field", ds.Tables[xElement.panel].Columns["id"], ds.Tables[xElement.field].Columns["panelid"]);
						rel2.Nested = true;
						//'If UseTabs Then 
						//' If UseDetails Then UseDetails = Not ds.Tables(6) Is Nothing 
						//' If UseDetails Then UseDetails = ds.Tables(6).Rows.Count > 0 
						//' UseTabs = Not ds.Tables(2) Is Nothing 
						//'Else 
						//' If UseDetails Then UseDetails = Not ds.Tables(5) Is Nothing 
						//' If UseDetails Then UseDetails = ds.Tables(5).Rows.Count > 0 
						//'End If 
						//'If UseTabs Then UseTabs = ds.Tables(2).Rows.Count > 0 
						DataRelation rel3;
						//'If UseTabs Then 
						//' rel3 = .Relations.Add("form-tab", ds.Tables(xAttribute.elem_form).Columns("id"), ds.Tables("tab").Columns("formid")) 
						//' rel3.Nested = True 
						//' Dim rel4 As DataRelation = .Relations.Add("tab-panel", ds.Tables("tab").Columns("id"), ds.Tables(xElement.panel).Columns("tabid")) 
						//' rel4.Nested = True 
						//'Else 
						rel3 = ds.Relations.Add("form-panel", ds.Tables[xElement.form].Columns["id"], ds.Tables[xElement.panel].Columns["formid"]);
						rel3.Nested = true;
					}
					//'End If 
					//'If UseDetails Then 
					//' If UseTabs Then 
					//' Dim rel5 As DataRelation = .Relations.Add("tab-paneldetails", ds.Tables("tab").Columns("id"), ds.Tables("paneldetails").Columns("tabid")) 
					//' rel5.Nested = True 
					//' Else 
					//' Dim rel5 As DataRelation = .Relations.Add("form-paneldetails", ds.Tables(xAttribute.elem_form).Columns("id"), ds.Tables("paneldetails").Columns("formid")) 
					//' rel5.Nested = True 
					//' End If 
					//' Dim rel6 As DataRelation = .Relations.Add("paneldetails-fielddetails", ds.Tables("paneldetails").Columns("panelid"), ds.Tables("fielddetails").Columns("panelid")) 
					//' rel6.Nested = True 
					//'End If 
				}
				string sql = ds.GetXml();
				int cp = sql.Length;
				if (cp > 20)
				{
					sql = sql.Substring(7, cp - 14).Replace("\"true\"", "\"1\"").Replace("\"false\"", "\"\"").Replace("labellist=\"\"", "").Replace("dbcolumnreadlov=\"\"", "");
					//sql = sql.Replace("lookup=\"\"", "").Replace("readonly=\"\"", "").Replace("required=\"\"", "").Replace("optional=\"\"", "");
					//'If UseDetails Then 
					//' sql = sql.Replace("fielddetails", field_str).Replace("paneldetails", "panel-details") 
					//'End If 
				}
				return xQuery.XMLHeader + sql;
			}
			else
				return "ERROR" + errorMsg;
		}

		//static internal bool DicoExists(string SqlConnection)
		//{
		//    string ErrorMsg = null; 
		//    EvoDB.GetData(EvoDB.BuildSQL("top 1 ID", "evodico_Form"), SqlConnection, ref ErrorMsg);
		//    return string.IsNullOrEmpty(ErrorMsg);
		//} 

#endregion

	}
 
}
