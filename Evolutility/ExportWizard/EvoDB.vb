'  Copyright (c) 2003-2011 Olivier Giulieri - olivier@evolutility.org 

'  This file is part of Evolutility CRUD Framework.

'  Source link <http://www.evolutility.org/download/download.aspx>

'  Evolutility is open source software: you can redistribute it and/or modify
'  it under the terms of the GNU Affero General Public License as published by
'  the Free Software Foundation, either version 3 of the License, or
'  (at your option) any later version.

'  Evolutility is distributed WITHOUT ANY WARRANTY; 
'  without even the implied warranty of MERCHANTABILITY 
'  or FITNESS FOR A PARTICULAR PURPOSE.  
'  See the GNU Affero General Public License for more details.

'  You should have received a copy of the GNU Affero General Public License
'  along with Evolutility. If not, see <http://www.fsf.org/licensing/licenses/agpl-3.0.html>.

'  Commercial licenses may be purchased at www.evolutility.org <http://www.evolutility.org/product/Purchase.aspx>.


Option Explicit On
Option Strict On

Imports System.Data.SqlClient
Imports System.Data
Imports System.Text

'############# DBEXPORT VERSION ######################

Friend Module EvoDB

	Friend Function GetData(ByVal SQL As String, ByVal aSqlConnection As String) As DataSet
		'Run query and returns recordset
		Dim myConnection As New SqlConnection(aSqlConnection)
		Dim myCommand As New SqlDataAdapter(SQL, myConnection)
		Dim ds As New DataSet

		Try
			myCommand.Fill(ds, SQL)
			GetData = ds
		Catch DBerror As Exception
			GetData = Nothing
		Finally
			myCommand = Nothing
			myConnection.Close()
			myConnection = Nothing
		End Try
	End Function

	Friend Function dbformat2(ByVal myval As String, ByVal mytype As String) As String
		Select Case mytype
			Case "System.String"
				Return String.Format("'{0}'", Replace(myval, "'", "''"))
			Case "System.Boolean"
				If myval = "True" Then
					Return "1"
				Else
					Return CStr(Val(myval))
				End If
			Case "System.DateTime"
				If IsDate(myval) Then
					Return "'" + Format(CDate(myval), "yyyy-M-d hh:mm:ss tt") + "'"
				Else
					Return "NULL"
				End If
			Case Else '"System.Int32", "System.Byte", "Sysem.Decimal"
				Return LTrim(CStr(Val(myval)))
		End Select
	End Function

	Friend Function BuildSQL(ByVal SQLselect As String, ByVal SQLfrom As String, Optional ByVal SQLwhere As String = "", Optional ByVal SQLorderby As String = "") As String
		Dim sql As New StringBuilder

		sql.Append("SELECT ").Append(SQLselect).Append(" FROM ").Append(SQLfrom)
		If Not String.IsNullOrEmpty(SQLwhere) Then
			sql.Append(" WHERE ").Append(SQLwhere)
		End If
		If Not String.IsNullOrEmpty(SQLorderby) Then
			sql.Append(" ORDER BY ").Append(SQLorderby)
		End If
		Return sql.ToString
	End Function

	Friend Function SQLObjType(ByVal ObjType As String) As String
		Select Case ObjType
			Case "U"
				Return "Table"
			Case "V"
				Return "View"
			Case "Q"
				Return "Query"
			Case Else
				Return "Database Object"
		End Select
	End Function

End Module
