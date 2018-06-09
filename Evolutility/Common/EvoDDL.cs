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
using System.Data.SqlClient;
using System.Data;

namespace Evolutility
{

	static class EvoDDL
	{

		//### Constants ###################################################################################### 
		#region "Constants"

		//internal const string QueryError = "Cannot execute database query.";
		internal const string intIdentity = " [int] IDENTITY (1, 1) not null";

		internal const string dbText = "text";
		internal const string dbVarchar = "varchar";
		internal const string dbNvarchar = "nvarchar";
		internal const string dbInt = "[int]";
		internal const string dbNvarchar100 = "[nvarchar] (100)";

		#endregion

		//### Data Structure Manipulation ######################################################################################## 
		#region "SQL"

		static internal string SQLInsertColumn(string name, string type, string width, bool nullable)
		{
			StringBuilder buffer = new StringBuilder();

			buffer.AppendFormat("[{0}] [{1}] ", name, type);
			if (!string.IsNullOrEmpty(width))
				buffer.AppendFormat("({0}) ", width);
			switch (type)
			{
				case dbText:
				case dbNvarchar:
				case dbVarchar:
					buffer.Append("COLLATE SQL_Latin1_General_CP1_CI_AS ");
					break;
			}
			if (nullable)
				buffer.Append("NULL,");
			else
				buffer.Append("NOT NULL,");
			return buffer.ToString();
		}

		static internal string SQLCreateTable(string tableName)
		{
			return string.Format("CREATE TABLE [{0}]\n ([ID] {1},\n ", tableName, EvoDDL.intIdentity);
		}

		static internal string SQLAlterTable(string tableName)
		{
			return string.Format("ALTER TABLE [{0}] WITH NOCHECK ADD {1}", tableName, "\n");
		}

		static internal string SQLconstraint(string dbtable, string dbcolumn, string dbdefault, bool includeFOR)
		{
			string ret = string.Format("\n CONSTRAINT [DF_{0}_{1}] DEFAULT ({2})", dbtable, dbcolumn, dbdefault, dbcolumn);
			if (includeFOR)
				return string.Format("{0} FOR [{1}],", ret, dbcolumn);
			else
				return string.Format("{0},", ret);
		}

		#endregion

	}

}