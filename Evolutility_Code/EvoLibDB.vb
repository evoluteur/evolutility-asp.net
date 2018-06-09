'    (c) 2003-2008 Olivier Giulieri - olivier@evolutility.com 

'    This program is open source software: you can redistribute it and/or modify
'    it under the terms of the GNU Affero General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.

'    This program is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU Affero General Public License for more details.

'    You should have received a copy of the GNU Affero General Public License
'    along with this program.  If not, see <http://www.gnu.org/licenses/>.


Option Explicit On 
Option Strict On

Imports System.Text
Imports System.Data.SqlClient
'Imports System.Data

Module EvoLibDB

	'### Constants ######################################################################################
#Region "Constants"

	'field types
	Friend Const t_text As String = "text"
	Friend Const t_txtm As String = "textmultiline"
	Friend Const t_bool As String = "boolean"
	Friend Const t_dec As String = "decimal"
	Friend Const t_int As String = "integer"
	Friend Const t_date As String = "date"
	Friend Const t_time As String = "time"
	Friend Const t_datetime As String = "datetime"
	Friend Const t_pix As String = "image"
	Friend Const t_doc As String = "document"
	Friend Const t_lov As String = "lov"
	Friend Const t_formula As String = "formula"
	Friend Const t_html As String = "html"
	Friend Const t_email As String = "email"
	Friend Const t_url As String = "url"

	Friend Const p_itemid As String = "@itemid", p_userid As String = "@userid"
	Friend Const SQL_WHERE As String = " WHERE "
	Friend Const SQL_EXEC As String = "EXEC "

#End Region

	'### Formatting & Escaping ######################################################################################
#Region "Formatting & Escaping"

	Friend Function dbformat2(ByVal myVal As String, ByVal myType As String, ByVal language As String) As String
		'Used for Export
		Select Case myType
			Case "System.String"
				Return "N'" & SQLescape(myVal) & "'"
			Case "System.Boolean"
				If myVal = "True" Then
					Return "1"
				Else
					Return CStr(Val(myVal))
				End If
			Case "System.DateTime"
				If language = "FR" Then
					Return GetFrenchDate(myVal)
				Else
					If IsDate(myVal) Then
						Return "'" & Format(CDate(myVal), "yyyy-M-d hh:mm:ss tt") & "'"
					Else
						Return "NULL"
					End If
				End If
			Case Else '"System.Int32", "System.Byte", "Sysem.Decimal"
				Return LTrim(CStr(Val(myVal)))
		End Select
	End Function

	Friend Function dbFormat(ByVal fieldValue As String, ByVal fieldType As String, ByVal fieldMaxLength As Integer, ByVal language As String) As String
		Dim buffer As String = String.Empty

		Select Case fieldType
			Case t_text, t_txtm, t_pix, t_doc, t_email, t_url, t_html
				If fieldMaxLength > 0 AndAlso fieldValue.Length > fieldMaxLength Then
					Return "N'" & fieldValue.Substring(0, fieldMaxLength).Replace("'", "''") & "'"
				Else
					Return String.Format("N'{0}'", fieldValue.Replace("'", "''"))
				End If
			Case t_lov
				If String.IsNullOrEmpty(fieldValue) Then
					Return String.Empty
				Else
					Return CStr(Val(fieldValue))
				End If
			Case t_bool, t_int
				Return CStr(Val(fieldValue))
			Case t_dec
				If language = "FR" Then
					fieldValue = fieldValue.Replace(",", ".")
				End If
				If IsNumeric(fieldValue) Then
					'MUST NOT USE FORMATTED NUMBER IN EDIT GRID
					Return LTrim(fieldValue)
				ElseIf fieldValue <> String.Empty Then
					Return LTrim(CStr(Val(fieldValue)))
				Else
					Return "NULL"
				End If
			Case t_date, t_datetime, t_time
				If language = "FR" Then
					Return GetFrenchDate(fieldValue)
				Else
					If IsDate(fieldValue) Then
						Select Case fieldType
							Case t_date
								Return String.Format("'{0}'", Format(CDate(fieldValue), "yyyy-M-d"))
							Case t_datetime
								Return String.Format("'{0}'", Format(CDate(fieldValue), "yyyy-M-d hh:mm:ss tt"))
							Case Else '"time"
								Return String.Format("'{0}'", Format(CDate(fieldValue), "hh:mm:ss tt"))
						End Select
					Else
						Return "NULL"
					End If
				End If
			Case Else
				Return SQLescape(fieldValue)
		End Select
	End Function

	Friend Function SQLec(ByVal FieldType As String, ByVal FieldValue As String, ByVal [Operator] As String) As String
		'returns a "condition" in SQL or plain English

		If FieldType = t_text Then	'textmultiline is passed as text !
			Select Case [Operator]
				Case "eq"
					Return (New StringBuilder).Append("=N'").Append(FieldValue).Append("'").ToString
				Case "sw"
					Return (New StringBuilder).Append(" LIKE N'").Append(FieldValue).Append("%'").ToString
				Case "fw"
					Return (New StringBuilder).Append(" LIKE N'%").Append(FieldValue).Append("'").ToString
				Case Else ' "ct" 
					Return (New StringBuilder).Append(" LIKE N'%").Append(FieldValue).Append("%'").ToString
			End Select
		Else
			Select Case [Operator]
				Case "gt"
					Return ">"
				Case "st"
					Return "<"
				Case Else
					Return "="
			End Select
		End If
	End Function

	Friend Function SQLescape(ByVal aString As String) As String
		'simple SQL escaping to avoid SQL injection attack
		If String.IsNullOrEmpty(aString) Then
			Return String.Empty
		Else
			Return aString.Replace("'", "''")
		End If
	End Function

	Friend Function SQLescape2(ByVal aString As String) As String
		'SQL escaping for WHERE clause w/ LIKE
		If String.IsNullOrEmpty(aString) Then
			Return String.Empty
		Else
			Return aString.Replace("[", "[[]").Replace("]", "[]]").Replace("%", "[%]").Replace("_", "[_]")
		End If
	End Function

#End Region

	'### SP & dates ######################################################################################
#Region "SP & dates"

	Friend Function SPcall_Paging(ByVal SPname As String, ByVal spSelect As String, ByVal spFrom As String, ByVal spWhere As String, ByVal spOrderBy As String, ByVal sqlPK As String, Optional ByVal spPageID As Integer = 1, Optional ByVal spPageSize As Integer = 10, Optional ByVal spUserID As Integer = 0, Optional ByVal myDBtable As String = "") As String
		'replace parameters by values in stored procedure SQL call
		Dim sql As New StringBuilder
		Dim parameters As String, buffer As String, i As Integer, j As Integer, k As Integer

		parameters = SPname
		i = InStr(parameters, "@")
		If InStr(parameters, "@") = 0 Then
			sql.Append(SPname)
		Else
			While i > 0
				sql.Append(Left(parameters, i - 1))
				parameters = Right(parameters, parameters.Length - i)
				j = 0
				buffer = LCase(Left(parameters, 10))
				'If Left(buffer, 6) = "field-" Then

				'Else
				k = InStr(buffer, ",")
				If k > 1 Then buffer = Left(buffer, k - 1)
				k = InStr(buffer, "+")
				If k > 1 Then buffer = Left(buffer, k - 1)
				buffer = Left(buffer & "          ", 10)
				If buffer = "sqlselect " Then
					sql.AppendFormat("'{0}'", spSelect)
					j = 9
				ElseIf buffer = "sqlfrom   " Then
					sql.AppendFormat("'{0}'", spFrom)
					j = 7
				ElseIf buffer = "sqlwhere  " Then
					If String.IsNullOrEmpty(spWhere) Then
						sql.Append("''")
					Else
						sql.Append("""")
						If InStr(spWhere, """") > 0 Then
							sql.Append(spWhere.Replace("""", """"""))
						Else
							sql.Append(spWhere)
						End If
						sql.Append("""")
					End If
					j = 8
				ElseIf buffer = "sqlorderby" Then
					sql.AppendFormat("'{0}'", spOrderBy)
					j = 10
				ElseIf buffer = "sqlpk     " Then
					sql.AppendFormat("'{0}'", sqlPK)
					j = 5
				ElseIf buffer = "pageid    " Then
					sql.Append(spPageID)
					j = 6
				ElseIf buffer = "pagesize  " Then
					sql.Append(spPageSize)
					j = 8
				ElseIf buffer = "userid    " Then
					sql.Append(spUserID)
					j = 6
				ElseIf buffer = "sqltable  " Then
					sql.Append("'").Append(myDBtable).Append("'")
					j = 8
				Else
					sql.Append("@")
					j = 1
				End If
				'End If
				If j > 0 Then parameters = Right(parameters, parameters.Length - j)
				i = InStr(parameters, "@")
			End While
		End If
		Return sql.ToString
	End Function

	Friend Function SPcall_Get(ByVal SPname As String, ByVal itemID As Integer, ByVal userID As Integer, Optional ByVal fieldID As Integer = 0) As String
		'replace parameters by values in stored procedure SQL call
		Return SPname.Replace(p_itemid, CStr(itemID)).Replace(p_userid, CStr(userID)).Replace("@fieldid", CStr(fieldID))
	End Function

	Friend Function GetFrenchDate(ByVal dateString As String) As String
		Dim ValidDate As Boolean
		Dim dateParts() As String = Split(dateString, "/")
		If dateParts.Length = 3 Then
			If Val(dateParts(2)) < 100 Then
				dateParts(2) = CStr(2000 + Val(dateParts(2)))
			End If
			If ServerUsesFrenchDates() Then
				ValidDate = IsDate(dateString)
			Else
				ValidDate = IsDate(String.Format("{1}/{0}/{2}", dateParts(0), dateParts(1), dateParts(2)))
			End If
			If ValidDate Then
				Return String.Format("convert(datetime,'{1}/{0}/{2}',101)", dateParts(0), dateParts(1), dateParts(2))
			Else
				Return "NULL"
			End If
		Else
			Return "NULL"
		End If
	End Function

	Friend Function ServerUsesFrenchDates() As Boolean
		Return IsDate("16/1/2008")
	End Function

#End Region

	'### SQL as usual ######################################################################################
#Region "SQL as usual"

	Friend Function BuildSQL(ByVal SQLselect As String, ByVal SQLfrom As String, Optional ByVal SQLwhere As String = "", Optional ByVal SQLorderby As String = "") As String
		Dim sql As New StringBuilder

		sql.Append("SELECT ").Append(SQLselect).Append(" FROM ").Append(SQLfrom)
		If SQLwhere <> String.Empty Then sql.Append(SQL_WHERE).Append(SQLwhere)
		If SQLorderby <> String.Empty Then sql.Append(" ORDER BY ").Append(SQLorderby)
		Return sql.Append(";").ToString
	End Function

	Friend Function sqlINSERT(ByVal SQLTable As String, ByVal SQLColumns As String, ByVal SQLvalues As String) As String
		Return (New StringBuilder).Append("INSERT INTO ").Append(SQLTable).Append("(").Append(SQLColumns).Append(") VALUES (").Append(SQLvalues).Append(");").ToString
	End Function

	Friend Function sqlUPDATE(ByVal SQLTable As String, ByVal SQLColumnsValuesTuples As String, ByVal SQLWhere As String) As String
		Return (New StringBuilder).Append("UPDATE ").Append(SQLTable).Append(" SET ").Append(SQLColumnsValuesTuples).Append(SQL_WHERE).Append(SQLWhere).ToString
		' NO ";"
	End Function

	Friend Function sqlDELETE(ByVal SQLTable As String, ByVal SQLWhere As String) As String
		Return (New StringBuilder).Append("DELETE FROM ").Append(SQLTable).Append(SQL_WHERE).Append(SQLWhere).ToString
		' NO ";"
	End Function

	Friend Function sqlTRANSACTION(ByVal mySQL As String) As String
		Return (New StringBuilder).Append("BEGIN TRANSACTION").Append(vbCrLf).Append(mySQL).Append(vbCrLf).Append("COMMIT TRANSACTION").Append(vbCrLf).ToString
	End Function

	Friend Function RunSQL(ByVal SQL As String, ByVal aSqlConnection As String, Optional ByVal InTransaction As Boolean = False) As String
		Dim NoError As Boolean = True
		Dim myCommand As SqlCommand
		Dim myConnection As New SqlConnection(aSqlConnection)
		Dim ErrorMsg1 As String = String.Empty

		If SQL <> String.Empty Then
			If InTransaction Then SQL = sqlTRANSACTION(SQL)
			myCommand = New SqlCommand(SQL, myConnection)
			Try
				myCommand.Connection.Open()
			Catch
				NoError = False
			End Try
			If NoError Then
				Try
					myCommand.ExecuteNonQuery()
				Catch ex As Exception
					ErrorMsg1 = HTMLtextMore("Cannot execute SQL.", ex.ToString()) ' & vbCrLf & SQL)
				Finally
					myCommand.Dispose()
					myConnection.Close()
					myConnection.Dispose()
				End Try
			End If
		End If
		Return ErrorMsg1
	End Function

#End Region

End Module
