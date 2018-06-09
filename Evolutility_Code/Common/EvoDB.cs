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

using System;
using System.Text;

using System.Data;
#if DB_MySQL
using MySql.Data;
using MySql.Data.MySqlClient;
#else
using System.Data.SqlClient;
#endif


namespace Evolutility
{
	// ==================   General SQL generation & DB access   ==================   
	/* 
	This library is a dependency of : 
	 * Evolutility.DataServer 
	 * Evolutility.UIServer 
	 * Evolutility.Wizard 
	*/

	static class EvoDB
	{

//### Constants ###################################################################################### 
#region "Constants"

		// field types 
		internal const string t_text = "text";
		internal const string t_txtm = "textmultiline";
		internal const string t_bool = "boolean";
		internal const string t_dec = "decimal";
		internal const string t_int = "integer";
		internal const string t_date = "date";
		internal const string t_time = "time";
		internal const string t_datetime = "datetime";
		internal const string t_pix = "image";
		internal const string t_doc = "document";
		internal const string t_lov = "lov";
		internal const string t_formula = "formula";
		internal const string t_html = "html";
		internal const string t_email = "email";
		internal const string t_url = "url";

		// SQL parameter names
		internal const string p_itemid = "@itemid";
		internal const string p_userid = "@userid";

		// SQL
		internal const string SQL_WHERE = " WHERE ";
		internal const string SQL_EXEC = "EXEC ";
		internal const string SQL_NULL = "NULL";
		internal const string SQL_INSERT = "INSERT INTO ";


		 
#if DB_MySQL

		internal const string SQL_NAME_NOW = "CURTIME()";
		internal const string SQL_DATEDIFFe012 = " DATEDIFF(day,{0},{1}){2}0 "; // bug ?
		internal const string SQL_DATEstr012 = "convert(datetime,'{1}/{0}/{2}',101)"; // bug ?
		internal const string SQL_NAME_0c = "`{0}`,";
		internal const string SQL_NAME_0e1c = "`{0}`={1},";
		internal const string SQL_NAME_c0 = ",`{0}`";
		internal const string SQL_NAME_T0 = "T.`{0}`";
		internal const string SQL_NAME_cT0 = ",T.`{0}`";
		internal const string SQL_ISNULL_0 = "ISNULL({0})=0";

		internal const string SQL_IDENTITY = " SELECT LAST_INSERT_ID()"; 
		internal const string SQL_ROWCOUNT = " SELECT FOUND_ROWS();"; // bug ?
		internal const string SQL_ID_INSERT = "SET IDENTITY_INSERT {0} {1};\n\n"; // OK b/c only used in export wizard
		internal const string SQL_BEGIN_TRANS = "START TRANSACTION; \r\n";
		internal const string SQL_COMMIT_TRANS = ";\r\n COMMIT ";
		internal const string SQL_INCREMENT = "{0}={0}+1";  // bug ? check SQLServer version for null values handling

		internal const string SQL_SELECT_LOV_T = "t.ID,rtrim(t.{1}) AS value"; // need "LIMIT {0}" at end of query

#else
		internal const string SQL_NAME_NOW = "getdate()";
		internal const string SQL_DATEDIFFe012 = " DATEDIFF(day,{0},{1}){2}0 ";
		internal const string SQL_DATEstr012 = "convert(datetime,'{1}/{0}/{2}',101)";
		internal const string SQL_NAME_0c = "[{0}],";
		internal const string SQL_NAME_0e1c = "[{0}]={1},";
		internal const string SQL_NAME_c0 = ",[{0}]";
		internal const string SQL_NAME_T0 = "T.[{0}]";
		internal const string SQL_NAME_cT0 = ",T.[{0}]";
		internal const string SQL_ISNULL_0 = "isnull({0},0)=0";

		internal const string SQL_IDENTITY = " SELECT SCOPE_IDENTITY()"; // @@IDENTITY
		internal const string SQL_ROWCOUNT = " SELECT @@ROWCOUNT";
		internal const string SQL_ID_INSERT = "SET IDENTITY_INSERT {0} {1};\n\n";
		internal const string SQL_BEGIN_TRANS = "BEGIN TRANSACTION \r\n";
		internal const string SQL_COMMIT_TRANS = "\r\n COMMIT TRANSACTION \r\n";
		internal const string SQL_INCREMENT = "{0}=CASE WHEN({0} is null)THEN 1 ELSE {0}+1 END";  // will work for null values

		internal const string SQL_SELECT_LOV_T = "TOP {0} t.ID,rtrim(t.{1}) AS value";

#endif

		internal const string SQL_SELECT_LOV = " ID,rtrim({0}) AS value";

		internal const string SQLf_T01 = "T.{0}={1}";
		internal const string SQLw_Sharing = "(T.Publish>0 OR T.{0}={1})";

		internal const string QueryError = "Cannot execute database query.";
		private const string SQL_and = " AND ";

		// url parameters
		internal const string soEqual = "eq", soStartWith = "sw", soFinishWith = "fw", soContain = "ct";
		internal const string soIsNull = "null", soIsNotNull = "nn";
		internal const string soGreaterThan = "gt", soSmallerThan = "st";

#endregion

//### Formatting & Escaping ###################################################################################### 
#region "Formatting & Escaping"

		static internal string dbformat2(string myVal, string myType, string language)
		{
			//Used for Export 
			switch (myType)
			{
				case "System.String":
					return string.Format("N'{0}'", SQLescape(myVal));
				case "System.Boolean":
					return (myVal == "True") ? "1" : EvoTC.StrVal(myVal);
				case "System.DateTime":
					switch (language)
					{
						case "EN": // English
						case "ZH": // Chinese
							// Date format = Month/Day/Year, example 4/24/2008
							if (EvoTC.isDate(myVal))
								return string.Format("'{0}'", EvoTC.String2DateTime(myVal).ToString("yyyy-M-d hh:mm:ss tt"));
							else
								return SQL_NULL;
						case "DA": // Danish
							// Date format = Month-Day-Year, example 4-24-2008
							if (EvoTC.isDate(myVal.Replace("-", "/")))
								return string.Format("'{0}'", EvoTC.String2DateTime(myVal).ToString("G"));
							else
								return SQL_NULL;
						default:
							// Date format = Day/Month/Year, example 24/4/2008
							return GetDateDMY(myVal);
					}
				default: //"System.Int32", "System.Byte", "System.Decimal" 
					return EvoTC.StrVal(myVal);
			}
		}

		static internal string dbFormat(string fieldValue, string fieldType, int fieldMaxLength, string language)
		{
			switch (fieldType)
			{
				case t_text:
				case t_txtm:
				case t_pix:
				case t_doc:
				case t_email:
				case t_url:
				case t_html:
					if (fieldMaxLength > 0 && fieldValue.Length > fieldMaxLength)
						return string.Format("N'{0}'", fieldValue.Substring(0, fieldMaxLength).Replace("'", "''"));
					else
						return string.Format("N'{0}'", fieldValue.Replace("'", "''"));
				case t_lov:
					return string.IsNullOrEmpty(fieldValue) ? string.Empty : EvoTC.StrVal(fieldValue);
				case t_bool:
				case t_int:
					return string.IsNullOrEmpty(fieldValue)? SQL_NULL : EvoTC.StrVal(fieldValue);
				case t_dec:
					string tDecStr;
					if (EvoTC.isInteger(fieldValue))
					{
						//MUST NOT USE FORMATTED NUMBER IN EDIT GRID 
						tDecStr = fieldValue.TrimStart();
					}
					else if (!string.IsNullOrEmpty(fieldValue))
					{
						tDecStr = EvoTC.String2Dec(fieldValue).ToString();
					}
					else
						tDecStr = SQL_NULL;
					if (language == "FR" || language == "DA")
						tDecStr = tDecStr.Replace(",", ".");
					return tDecStr;
				case t_date:
				case t_datetime:
				case t_time:
					switch (language)
					{
						case "EN":
						case "ZH":
							if (EvoTC.isDate(fieldValue))
							{
								string df;
								switch (fieldType)
								{
									case t_date:
										df = "yyyy-M-d";
										break;
									case t_datetime:
										df = "yyyy-M-d hh:mm:ss tt";
										break;
									default:	//"time" 
										df = "hh:mm:ss tt";
										break;
								}
								return string.Format("'{0}'", EvoTC.String2DateTime(fieldValue).ToString(df));
							}
							else
								return SQL_NULL; 
						case "DA":
							if (EvoTC.isDate(fieldValue))
							{
								string df;
								switch (fieldType)
								{
									case t_date:
										df = "yyyy-M-d";
										break;
									case t_datetime:
										df = "yyyy-M-d HH:mm:ss";
										break;
									default:	//"time" 
										df = "HH:mm:ss";
										break;
								}
								return string.Format("'{0}'", EvoTC.String2DateTime(fieldValue).ToString(df));
							}
							else
								return SQL_NULL; 
						default:
							return GetDateDMY(fieldValue);
					} 
				default:
					return SQLescape(fieldValue);
			}
		}

		static internal string SQLec(string SQLColumn, string FieldType, string FieldValue, string Operator)
		{//returns a "condition" in SQL or plain English 
			StringBuilder sb = new StringBuilder();

			if (!string.IsNullOrEmpty(SQLColumn))
				sb.Append(SQL_and);
			if (Operator == soIsNull)
				sb.AppendFormat("({0} IS NULL OR {0}='')", SQLColumn);
			else if (Operator == soIsNotNull)
				sb.AppendFormat("NOT({0} IS NULL OR {0}='')", SQLColumn);
			else
			{
				if (!string.IsNullOrEmpty(SQLColumn))
					sb.Append(SQLColumn);
				//textmultiline is passed as text ! 
				if (FieldType == t_text)
				{
					switch (Operator)
					{
						case soEqual:
							sb.AppendFormat("=N'{0}'", FieldValue);
							break;
						case soStartWith:
							sb.AppendFormat(" LIKE N'{0}%'", FieldValue);
							break;
						case soFinishWith: 
							sb.AppendFormat(" LIKE N'%{0}'", FieldValue);
							break;
						default:	// soContain
							sb.AppendFormat(" LIKE N'%{0}%'", FieldValue);
							break;
					}
				}
				else
					switch (Operator)
					{
						case soGreaterThan:
							return ">";
						case soSmallerThan:
							return "<";
						default:
							return "=";
					}
			}
			return sb.ToString();
		}

		static internal string SQLescape(string aString)
		{
			//simple SQL escaping to avoid SQL injection attack 
			return string.IsNullOrEmpty(aString) ? string.Empty : aString.Replace("'", "''");
		}

		static internal string SQLescape2(string aString)
		{
			//SQL escaping for WHERE clause w/ LIKE 
			if (string.IsNullOrEmpty(aString))
				return string.Empty;
			else
				return aString.Replace("[", "[[]").Replace("]", "[]]").Replace("%", "[%]").Replace("_", "[_]");
		}

#endregion

//### SP & dates ###################################################################################### 
#region "SP & dates"

		static internal string SPcall_Paging(string SPname, string spSelect, string spFrom, string spWhere, string spOrderBy, string sqlPK, int spPageID, int spPageSize, int spUserID, string myDBtable)
		{
			return SPname.Replace("@SQLselect", quotedVar(spSelect))
				.Replace("@SQLtable", quotedVar(myDBtable))
				.Replace("@SQLfrom", quotedVar(spFrom))
				.Replace("@SQLwhere", quotedVar(spWhere))
				.Replace("@SQLorderby", quotedVar(spOrderBy))
				.Replace("@SQLpk", quotedVar(sqlPK))
				.Replace("@pageid", string.Format("'{0}'", spPageID))
				.Replace("@pagesize", string.Format("'{0}'", spPageSize))
				.Replace("@userid", string.Format("'{0}'", spUserID));
		}

		static internal string quotedVar(string myVar)
		{
			return string.Format("N'{0}'", myVar.Replace("'", "''"));
		}

		static internal string SPcall_Get(string SPname, int itemID, int userID)
		{
			//replace parameters by values in stored procedure SQL call 
			return SPname.Replace(p_itemid, itemID.ToString()).Replace(p_userid, userID.ToString());
		}
		static internal string SPcall_Get(string SPname, int itemID, int userID, int fieldID)
		{
			return SPcall_Get(SPname, itemID, userID).Replace("@fieldid", fieldID.ToString());
		}

		static internal string GetDateDMY(string dateString)
		{
			bool ValidDate = false;
			if (string.IsNullOrEmpty(dateString))
				return SQL_NULL;
			char[] sepChars = {'/', '-'};
			string[] dateParts = dateString.Split(sepChars);
			if (dateParts.Length == 3)
			{
				if (EvoTC.String2Int(dateParts[2]) < 100)
					dateParts[2] = (2000 + EvoTC.String2Int(dateParts[2])).ToString();
				if (ServerUsesFrenchDates())
					ValidDate = EvoTC.isDate(dateString);
				else
					ValidDate = EvoTC.isDate(string.Format("{1}/{0}/{2}", dateParts[0], dateParts[1], dateParts[2]));
				if (ValidDate)
					return string.Format(SQL_DATEstr012, dateParts[0], dateParts[1], dateParts[2]);
				else
					return SQL_NULL;
			}
			else
				return SQL_NULL;
		}

		static internal bool ServerUsesFrenchDates()
		{
			return EvoTC.isDate("16/1/2008");
		}

		static internal string wDateIsValue(string DBcolumn, string DBformatedDate, string DBoperator)
		{
			if (DBoperator==null)
				DBoperator = "=";
			return String.Format(SQL_DATEDIFFe012, DBformatedDate, DBcolumn, DBoperator);
		}

		static internal string wBoolIsFalse(string DBcolumn)
		{
			return String.Format(SQL_ISNULL_0, DBcolumn);
		}

#endregion

//### SQL generation ###################################################################################### 
#region "SQL generation"

		static internal string BuildSQL(string SQLselect, string SQLfrom)
		{
			return string.Format("SELECT {0} FROM {1};", SQLselect, SQLfrom);
		}
		static internal string BuildSQL(string SQLselect, string SQLfrom, string SQLwhere, string SQLorderby, int top)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("SELECT ");
#if (!DB_MySQL)
			if(top>0)
				sql.AppendFormat("TOP {0} ", top); 
#endif
			sql.Append(SQLselect).Append(" FROM ").Append(SQLfrom);
			if (! string.IsNullOrEmpty(SQLwhere))
				sql.Append(SQL_WHERE).Append(SQLwhere);
			if (! string.IsNullOrEmpty(SQLorderby)) 
				sql.Append(" ORDER BY ").Append(SQLorderby);
#if DB_MySQL
			if (top > 0)
				sql.AppendFormat(" LIMIT {0};", top);	
#else
			sql.Append(";");
#endif 

			return sql.ToString();
		}
		static internal string BuildSQL(string SQLselect, string SQLfrom, string SQLwhere, string SQLgroupby,string SQLhaving, string SQLorderby, int top)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("SELECT ");
#if (!DB_MySQL)
			if(top>0)
				sql.AppendFormat("TOP {0} ", top); 
#endif
			sql.Append(SQLselect).Append(" FROM ").Append(SQLfrom);
			if (! string.IsNullOrEmpty(SQLwhere))
				sql.Append(SQL_WHERE).Append(SQLwhere);
			if (!string.IsNullOrEmpty(SQLgroupby))
			{
				sql.Append(" GROUP BY ").Append(SQLgroupby);
				if (!string.IsNullOrEmpty(SQLhaving))
					sql.Append(" HAVING ").Append(SQLhaving);
			}
			if (! string.IsNullOrEmpty(SQLorderby)) 
				sql.Append(" ORDER BY ").Append(SQLorderby);
#if DB_MySQL
			if (top > 0)
				sql.AppendFormat(" LIMIT {0};", top);	
#else
			sql.Append(";");
#endif 

			return sql.ToString();
		}

		static internal string sqlINSERT(string SQLTable, string SQLColumns, string SQLvalues)
		{
			return new StringBuilder().Append(SQL_INSERT).Append(SQLTable).Append("(").Append(SQLColumns).Append(") VALUES (").Append(SQLvalues).Append(");").ToString();
		}

		static internal string sqlUPDATE(string SQLTable, string SQLColumnsValuesTuples, string SQLWhere)
		{
			return String.Format("UPDATE {0} SET {1}{2}{3};", SQLTable, SQLColumnsValuesTuples, SQL_WHERE, SQLWhere); 
		}

		static internal string sqlDELETE(string SQLTable, string SQLWhere)
		{
			return new StringBuilder().AppendFormat("DELETE FROM {0}{1}{2};", SQLTable, SQL_WHERE, SQLWhere).ToString();
		}

		static internal string sqlTRANSACTION(string mySQL)
		{
			return new StringBuilder().Append(SQL_BEGIN_TRANS).Append(mySQL).Append(SQL_COMMIT_TRANS).ToString();
		}

		static internal  string IDequals(int ID)
		{
			return string.Format("ID={0}", ID);
		} 

#endregion

//### DB Access ###################################################################################### 
#region "DB Access"

		static internal DataSet GetData(string SQL, string mySqlConnection, ref string ErrorMsg)
		{
			//Run query and returns DataSet 
#if DB_MySQL
			MySqlConnection myConnection = new MySqlConnection(mySqlConnection);
			MySqlDataAdapter myCommand = new MySqlDataAdapter(SQL, myConnection);
#else
			SqlConnection myConnection = new SqlConnection(mySqlConnection);
			SqlDataAdapter myCommand = new SqlDataAdapter(SQL, myConnection);
#endif
			DataSet ds = new DataSet();

			try
			{
				myCommand.Fill(ds, SQL);
				return ds;
			}
			catch (Exception DBerror)
			{
				ErrorMsg += HTMLtextMore(QueryError, DBerror.Message);
				return null;
			}
			finally
			{
				myCommand.Dispose();
				myConnection.Close();
			}
		}

#if DB_MySQL
		static internal DataSet GetDataParameters(string SQL, string mySqlConnection, MySqlParameter[] Params, ref string ErrorMsg)
		{
			MySqlConnection myConnection = new MySqlConnection(mySqlConnection);
			MySqlDataAdapter myCommand = new MySqlDataAdapter(SQL, myConnection);
			DataSet ds = new DataSet();

			try
			{
				MySqlParameterCollection pc = myCommand.SelectCommand.Parameters;
				foreach (MySqlParameter param in Params)
				{
					pc.Add(param);
				} 
				myCommand.Fill(ds, SQL);
				return ds;
			}
			catch (Exception DBerror)
			{
				ErrorMsg += HTMLtextMore(QueryError, DBerror.Message);
				return null;
			}
			finally
			{
				myCommand.Dispose();
				myConnection.Close();
			}
		}
#else
		static internal DataSet GetDataParameters(string SQL, string mySqlConnection, SqlParameter[] Params, ref string ErrorMsg)
		{
			SqlConnection myConnection = new SqlConnection(mySqlConnection);
			SqlDataAdapter myCommand = new SqlDataAdapter(SQL, myConnection);
			DataSet ds = new DataSet();

			try
			{
				SqlParameterCollection pc = myCommand.SelectCommand.Parameters;
				foreach (SqlParameter param in Params)
				{
					pc.Add(param);
				} 
				myCommand.Fill(ds, SQL);
				return ds;
			}
			catch (Exception DBerror)
			{
				ErrorMsg += HTMLtextMore(QueryError, DBerror.Message);
				return null;
			}
			finally
			{
				myCommand.Dispose();
				myConnection.Close();
			}
		}
#endif

		static internal string GetDataScalar(string SQL, string mySqlConnection, ref string ErrorMsg)
		{
			string ReturnValue = null;

#if DB_MySQL
			MySqlConnection myConnection = new MySqlConnection(mySqlConnection);
			MySqlCommand myCommand = new MySqlCommand(SQL, myConnection);
#else
			SqlConnection myConnection = new SqlConnection(mySqlConnection);
			SqlCommand myCommand = new SqlCommand(SQL, myConnection); 
#endif

			try
			{
				myConnection.Open();
				ReturnValue = Convert.ToString(myCommand.ExecuteScalar());
			}
			catch (Exception DBerror)
			{
				ErrorMsg += HTMLtextMore(QueryError, DBerror.Message);
				ReturnValue = string.Empty;
			}
			finally
			{
				myCommand.Dispose();
				myConnection.Close();
			} 
			return ReturnValue;
		}

		static internal string RunSQL(string SQL, string aSqlConnection, bool InTransaction)
		{
#if DB_MySQL
			MySqlConnection myConnection = new MySqlConnection(aSqlConnection);
#else
			SqlConnection myConnection = new SqlConnection(aSqlConnection);
#endif
			string ErrorMsg1 = string.Empty;

			if (!string.IsNullOrEmpty(SQL))
			{
				if (InTransaction)
					SQL = sqlTRANSACTION(SQL);
#if DB_MySQL
				MySqlCommand myCommand = new MySqlCommand(SQL, myConnection);
#else
				SqlCommand myCommand = new SqlCommand(SQL, myConnection);
#endif
				try
				{
					myCommand.Connection.Open(); 
					myCommand.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					ErrorMsg1 = HTMLtextMore(QueryError, ex.ToString());
				}
				finally
				{
					myCommand.Dispose();
					myConnection.Close();
					myConnection.Dispose();
				}
			}
			return ErrorMsg1;
		}

		static internal bool ColumnExists(DataTable Table, string ColumnName)
		{
			bool isThere = false;
			foreach(DataColumn col in Table.Columns)
			{
				if (col.ColumnName == ColumnName)
				{
					isThere = true;
					break;
				}
			} 
			return isThere;
		} 

#endregion

//### Misc. ###################################################################################### 
#region "Misc"

	// Having this function here saves the dependency on EvoLibUI which makes it easier to include.
	static private String HTMLtextMore(string myText, string myOptions)
	{
		return new StringBuilder()
			.Append(myText).Append("<div class=\"Foot\">").Append(myOptions).Append("</div>")
			.ToString();
	}

#endregion

	}
} 
