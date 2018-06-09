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

Imports System.Drawing.ColorTranslator
Imports System.Web.UI.WebControls
Imports System.Text

'############# DBEXPORT VERSION ######################

Friend Module EvoUI

	Friend Const HTMLscript_begin As String = "<script language=""Javascript""><!--" & vbCrLf
	Friend Const HTMLscript_end As String = vbCrLf & "//--></script>" & vbCrLf

	Friend Const html_inputHidden As String = "<input type=""hidden"" name=""{0}"" value=""{1}"">" ' name, value)

	Friend Function HTMLInputSelect(ByVal FieldName As String, ByVal FieldID As String, ByVal FieldCSS As String, Optional ByVal enabled As Boolean = True) As String
		Dim HTML As New StringBuilder()

		HTML.Append("<select class=""").Append(FieldCSS).Append(""" name=""").Append(FieldName)
		If FieldID <> "" Then HTML.Append("""").Append(" id=""").Append(FieldID)
		HTML.Append(""">")
		Return HTML.ToString
	End Function

	Friend Function Text2HTML(ByVal myText As String, Optional ByVal CR2BR As Boolean = True) As String
		'transform text into displayable HTML
		Dim buffer As String

		If myText <> "" Then
			buffer = Replace(Replace(myText, "<", "&lt;"), ">", "&gt;")
			If CR2BR Then buffer = Replace(buffer, vbCrLf, "<br>")
		Else
			buffer = ""
		End If
		Return buffer
	End Function

	Friend Function HTMLFieldTitle(ByVal FieldName As String, ByVal FieldCaption As String, ByVal FieldLabelCSS As String) As String
		Dim HTML As New StringBuilder

		HTML.Append("<span class=""").Append(FieldLabelCSS).Append("""><label for=""").Append(FieldName).Append(""">").Append(FieldCaption).Append("</label></span><br>")
		Return HTML.ToString
	End Function

	Friend Function HTMLLink(ByVal URL As String, ByVal Caption As String, Optional ByVal target As String = "", Optional ByVal Img As String = "", Optional ByVal ID As String = "") As String
		Dim HTML As New StringBuilder

		HTML.Append("<a href=""").Append(URL)
		If target <> "" Then HTML.Append(""" target=""").Append(target)
		HTML.Append(""">")
		If Img <> "" Then
			HTML.Append("<img src=""").Append(Img)
			If ID <> "" Then HTML.Append(""" ID=""").Append(ID)
			HTML.Append(""" border=""0"">")
		End If
		HTML.Append(Caption).Append("</a>")
		Return HTML.ToString
	End Function

	Friend Function HTMLComments(ByVal Msg As String) As String
		Return String.Format("{1}<!-- {0} -->{1}", Msg, vbCrLf)
	End Function

	Friend Function HTMLInputButton(ByVal name As String, ByVal caption As String, Optional ByVal submit As Boolean = True, Optional ByVal onclick As String = "", Optional ByVal enabled As Boolean = True) As String
		Dim myHTML As New StringBuilder

		If submit Then
			myHTML.Append("<input type=""submit")
		Else
			myHTML.Append("<input type=""button")
		End If
		myHTML.Append(""" class=""Button"" name=""").Append(name).Append(""" value=""&nbsp;&nbsp;").Append(caption).Append("&nbsp;&nbsp;")
		If onclick <> "" Then
			myHTML.Append(""" onclick=""").Append(onclick)
		End If
		If Not enabled Then
			myHTML.Append(""" disabled=""disabled")
		End If
		myHTML.Append("""/>")
		Return myHTML.ToString
	End Function

	Function HTMLStep(ByVal stepID As Integer, ByVal maxStep As Integer, Optional ByVal minStep As Integer = 1) As String
		Dim c As String, mySpan As String = "<span class=""n{0}{1}""></span>"
		Dim myHTML As New StringBuilder
		Dim i As Integer
		myHTML.Append("<span class=""Steps"">")
		For i = 1 To maxStep 
			If i < stepID Then
				c = "d"
			ElseIf i = stepID Then
				c = "s"
			Else
				c = ""
			End If
			myHTML.AppendFormat(mySpan, c, i)
		Next
		myHTML.Append("</span>")
		Return myHTML.ToString()
	End Function

	Friend Function HTMLInputHidden(ByVal name As String, ByVal value As String) As String
		Return String.Format(html_inputHidden, name, value)
	End Function

	Friend Function HTMLInputOption(ByVal name As String, ByVal id As Integer, ByVal caption As String, Optional ByVal value As String = "1", Optional ByVal cssClass As String = "", Optional ByVal style As String = "", Optional ByVal enabled As Boolean = True, Optional ByVal selected As Boolean = False) As String
		Dim myHTML As New StringBuilder

		If cssClass <> "" Then
			myHTML.Append("<div class=""").Append(cssClass).Append(""">")
		End If
		myHTML.Append("<input type=""radio"" name=""").Append(name).Append(""" value=""").Append(value).Append(""" ")
		If id > -1 Then myHTML.Append(" ID=""").Append(name).Append(id).Append(""" ")
		If style <> "" Then myHTML.AppendFormat("style=""{0}"" ", style)
		If Not enabled Then
			myHTML.Append(" disabled=""disabled"" ")
		End If
		If selected Then
			myHTML.Append(" checked=""checked"" ")
		End If
		myHTML.Append(">")
		If caption <> "" Then
			myHTML.AppendFormat(" <small>{0}</small>", caption)
		End If
		If cssClass <> "" Then
			myHTML.Append("</div>")
		End If
		Return myHTML.ToString
	End Function

	Friend Function HTMLInputOption2(ByVal fieldValue As String, Optional ByVal fieldCaption As String = "", Optional ByVal selected As Boolean = False) As String
		Dim htmlField As New StringBuilder

		htmlField.Append("<option ")
		If fieldValue <> "" Then htmlField.Append("VALUE=""").Append(fieldValue).Append("""")
		If selected Then htmlField.Append(" selected >") Else htmlField.Append(">")
		htmlField.Append(fieldCaption)
		Return htmlField.ToString
	End Function

	Friend Function HTMLInputText(ByVal fieldName As String, ByVal fieldValue As String, Optional ByVal fieldCaption As String = "", Optional ByVal cssClass As String = "", Optional ByVal enabled As Boolean = True, Optional ByVal fieldMaxLength As Integer = 0, Optional ByVal xtra As String = "") As String
		Dim htmlField As New StringBuilder

		htmlField.Append("<input type=""text"" class=""").Append(cssClass).Append(""" name=""").Append(fieldName)
		If fieldMaxLength > 0 Then htmlField.Append(""" maxlength=""").Append(CInt(fieldMaxLength))
		If fieldValue <> "" Then htmlField.Append(""" VALUE=""").Append(fieldValue)
		If xtra.Length > 0 Then
			htmlField.Append(""" ").Append(xtra).Append(">")
		Else
			htmlField.Append(""">")
		End If
		Return htmlField.ToString
	End Function

	Friend Function HTMLInputCheckBox(ByVal name As String, ByVal value As String, Optional ByVal caption As String = "", Optional ByVal cssClass As String = "", Optional ByVal style As String = "", Optional ByVal enabled As Boolean = True, Optional ByVal selected As Boolean = False, Optional ByVal smallCaption As Boolean = True, Optional ByVal FieldID As String = "") As String
		Dim myHTML As New StringBuilder

		If cssClass <> "" Then myHTML.Append("<div class=""").Append(cssClass).Append(""">")
		myHTML.Append("<input type=""checkbox"" name=""").Append(name)
		If value <> "" Then myHTML.Append(""" value=""").Append(value)
		If FieldID <> "" Then myHTML.Append(""" ID=""").Append(FieldID)
		If style <> "" Then myHTML.Append(""" style=""").Append(style)
		If Not enabled Then myHTML.Append(""" disabled=""disabled")
		If selected Then myHTML.Append(""" checked=""checked")
		myHTML.Append(""">")
		If caption <> "" Then
			If smallCaption Then
				myHTML.Append("<small>" & caption & "</small>")
			Else
				myHTML.Append(caption)
			End If
		End If
		If cssClass <> "" Then myHTML.Append("</div>")
		HTMLInputCheckBox = myHTML.ToString
	End Function

	Friend Function StyleVisibleToggle(Optional ByVal Visible As Boolean = True, Optional ByVal ie As Boolean = False) As String
		If Visible Then
			If ie Then
				StyleVisibleToggle = " style=""display:inline;"" "
			Else
				StyleVisibleToggle = " style=""display:'';"" "
			End If
		Else
			StyleVisibleToggle = " style=""display:none;"" "
		End If
	End Function

	Friend Function TextNow() As String
		TextNow = Format(Now, "Short Time")
	End Function

	Friend Function CondiConcat(ByVal original As String, ByVal newItem As String, Optional ByVal separator As String = ", ") As String
		If original <> "" Then
			CondiConcat = original & separator & newItem
		Else
			CondiConcat = newItem
		End If
	End Function

	Friend Function Signature() As String
		'This signature is "invisible" to users and must not be removed from the source code nor the compiled version of Evolutility 
		Return String.Format("<div style=""display:none;"">Powered by {0}</div>", HTMLLink("http://www.evolutility.org", "Evolutility"))
	End Function

	Friend Function FormMessage(ByVal message As String, Optional ByVal icon As String = "") As String
		'Dim buffer As String = "<p>"
		'If icon = "error" Then buffer += "<b>ERROR</b>: "
		'buffer += Text2HTML(message, True) & "</p>"
		'Return buffer
		Dim myHTML As StringBuilder = New StringBuilder()

		myHTML.Append("<table class=""Msg"" id=""Msg""><tr><td>")
		If Not String.IsNullOrEmpty(icon) Then
			myHTML.Append("<div class=""Ico Msg").Append(icon).Append(""">&nbsp;</div></td><td width=""100%"">")
		End If
		myHTML.Append(message.Replace("\n", "<br/>"))  'leave < and > 
		myHTML.Append("</td></tr></table>")
		Return myHTML.ToString()
	End Function

End Module
