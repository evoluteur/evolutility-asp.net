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
Imports System.Web.HttpUtility
'Imports System.Web.UI.WebControls

Imports evolutility.WebControls.EvoLang

Module EvoLibUI

	'### Constants ######################################################################################
#Region "Constants"

	Friend Const JSscriptBegin As String = "<script type=""text/javascript""><!-- // (c) 2008 Olivier Giulieri" & vbCrLf ' defer=""defer""
	Friend Const JSscriptEnd As String = vbCrLf & "//--></script>"
	Friend Const evoLink As String = "http://www.evolutility.com"
	Friend Const DateFormatSD As String = "Short Date", DateFormatST As String = "Short Time"
	Friend Const BR_tag As String = "<BR/>", noBR_tag As String = "<nobr>", noBR_tagClose As String = "</nobr>"
	Friend Const HTMLlightbox As String = "<div id=""lightEVOL"" class=""LB_content""></div><div id=""fadeEVOL"" class=""LB_overlay""></div>"

#End Region

	'### HTML elements ######################################################################################
#Region "HTML elements"

	Friend Function NoBR(ByVal myText As String) As String
		Return (New StringBuilder).Append(noBR_tag).Append(myText).Append(noBR_tagClose).ToString
	End Function

	Friend Function HTMLInputText(ByVal FieldName As String, ByVal FieldValue As String, ByVal FieldStyle As String, ByVal maxLength As Integer) As String
		Return (New StringBuilder).Append("<input type=""text"" ").Append(FieldStyle).AppendFormat(" name=""{0}"" id=""{0}", FieldName).Append(""" maxlength=""").Append(maxLength).Append(""" value=""").Append(FieldValue).Append(""">").ToString
	End Function
	Friend Function HTMLInputTextEmpty(ByVal FieldName As String) As String
		Return String.Format("<input class=""Field"" type=""text"" name=""{0}"" id=""{0}"">", FieldName)
	End Function

	Friend Function HTMLInputTextArea(ByVal FieldName As String, ByVal Fieldlabel As String, ByVal FieldStyle As String, Optional ByVal rows As String = "4") As String
		Return (New StringBuilder).AppendFormat("<textarea name=""{0}"" id=""{0}", FieldName).Append(""" style=""width:100%;"" ").Append(FieldStyle).Append(" rows=""").Append(rows).Append(""" cols=""52""></textarea>").ToString
	End Function
	Friend Function HTMLInputTextArea(ByVal FieldName As String, ByVal Fieldlabel As String, ByVal FieldStyle As String, ByVal rows As String, ByVal FieldMaxLength As Integer) As String
		Dim b As New StringBuilder
		b.AppendFormat("<textarea name=""{0}"" id=""{0}", FieldName)
		If FieldMaxLength > 0 Then
			b.Append(""" onKeyUp=""Evol.checkMaxLen(this,").Append(FieldMaxLength).Append(")")
		End If
		b.Append(""" style=""width:100%;"" ").Append(FieldStyle).Append(" rows=""").Append(rows).Append(""" cols=""52""></textarea>").ToString()
		Return b.ToString()
	End Function

	Friend Function HTMLFieldLabel(ByVal fName As String, ByVal fLabel As String) As String
		Return (New StringBuilder).Append("<div class=""FieldLabel""><label for=""").Append(fName).Append(""">").Append(fLabel).Append("</label></div>").ToString
	End Function
	Friend Function HTMLFieldLabelResize(ByVal fName As String, ByVal fLabel As String) As String
		Dim htmlField As StringBuilder = New StringBuilder
		htmlField.Append("<div class=""FieldLabel"" onmouseover=""javascript:Evol.showResize('").Append(fName).Append("',-1,this)"">")
		htmlField.Append("<label for=""").Append(fName).Append(""">").Append(fLabel).Append("</label></div>")
		Return htmlField.ToString()
	End Function
	Friend Function HTMLFieldLabelSpan(ByVal fName As String, ByVal fLabel As String) As String
		Return (New StringBuilder).Append("<span class=""FieldLabel""><label for=""").Append(fName).Append(""">").Append(fLabel).Append("</label></span>").ToString
	End Function

	Friend Function HTMLOption(ByVal fieldValue As String, ByVal fieldlabel As String, ByVal selected As Boolean) As String
		If selected Then
			Return (New StringBuilder).Append("<option value=""").Append(fieldValue).Append(""" selected>").Append(fieldlabel).ToString()
		Else
			Return HTMLOption(fieldValue, fieldlabel)
		End If
	End Function
	Friend Function HTMLOption(ByVal fieldValue As String, ByVal fieldlabel As String) As String
		Return (New StringBuilder).Append("<option value=""").Append(fieldValue).Append(""">").Append(fieldlabel).ToString
	End Function

	Friend Function HTMLLinkCSS(ByRef URL As String, ByRef label As String, ByRef target As String, ByRef Img As String) As String
		Dim HTML As New StringBuilder

		HTML.Append("<a class=""FieldLabel"" href=""").Append(URL)
		If target.Length > 0 Then HTML.Append(""" target=""").Append(target)
		HTML.Append(""">")
		If Img <> String.Empty Then HTML.Append("<img src=""").Append(Img).Append(""" border=""0""/>")
		HTML.Append(label).Append("</a>")
		Return HTML.ToString
	End Function
	Friend Function HTMLLink(ByRef URL As String, ByRef label As String, ByRef target As String, ByRef Img As String) As String
		Dim HTML As New StringBuilder

		HTML.Append("<a href=""").Append(URL)
		If target.Length > 0 Then HTML.Append(""" target=""").Append(target)
		HTML.Append(""">")
		If Img <> String.Empty Then HTML.Append("<img src=""").Append(Img).Append(""" border=""0""/>")
		HTML.Append(label).Append("</a>")
		Return HTML.ToString
	End Function
	Friend Function HTMLLink(ByRef URL As String, ByRef label As String, ByRef target As String) As String
		Dim HTML As New StringBuilder

		HTML.Append("<a href=""").Append(URL)
		If target.Length > 0 Then HTML.Append(""" target=""").Append(target)
		HTML.Append(""">").Append(label).Append("</a>")
		Return HTML.ToString
	End Function
	Friend Function HTMLLink(ByRef URL As String, ByRef label As String) As String
		Return String.Format("<a href=""{0}"">{1}</a>", URL, label)
	End Function

	Friend Function HTMLEmail(ByRef email As String) As String
		Return String.Format("<a href=""mailto:{0}"">{0}</a>", email)
	End Function
	'Friend Function HTMLComments(ByVal Msg As String) As String
	'    HTMLComments = (New StringBuilder).Append(vbCrLf).Append("<!-- ").Append(Msg).Append(" -->").Append(vbCrLf).ToString
	'End Function

	Friend Function HTMLImg(ByRef PixName As String) As String
		Return String.Format("<img src=""{0}"" border=""0"" class=""fieldImg"" >", PixName)
	End Function
	Friend Function HTMLImg(ByRef PixName As String, ByRef AltTag As String) As String
		Return String.Format("<img src=""{0}"" alt=""{1}"" border=""0""/>", PixName, AltTag)
	End Function

	Friend Function HTMLInputButton(ByVal name As String, ByVal label As String, Optional ByVal submit As Boolean = True, Optional ByVal onclick As String = "") As String
		Dim zHTML As New StringBuilder

		zHTML.Append("<input type=""")
		If submit Then zHTML.Append("submit") Else zHTML.Append("button")
		If onclick <> String.Empty Then zHTML.Append(""" onclick=""").Append(onclick)
		zHTML.Append(""" class=""button"" name=""").Append(name).Append(""" value="" ").Append(label).Append(" "">")
		Return zHTML.ToString
	End Function

	Friend Function HTMLInputHidden(ByVal name As String, ByVal value As String) As String
		Return String.Format("<input type=""hidden"" name=""{0}"" id=""{0}"" value=""{1}"">", name, value)
	End Function

	Friend Function HTMLInputCheckBox(ByVal name As String, ByVal value As String, ByVal label As String, ByVal cssClass As String, Optional ByVal style As String = "", Optional ByVal selected As Boolean = False, Optional ByVal id As String = "") As String
		Dim zHTML As New StringBuilder

		With zHTML
			If cssClass.Length > 0 Then .Append("<div class=""").Append(cssClass).Append(""">")
			.Append("<input type=""checkbox"" name=""").Append(name).Append(""" value=""").Append(value)
			If style <> String.Empty Then .Append(""" style=""").Append(style)
			If id <> String.Empty Then .Append(""" ID=""").Append(id)
			If selected Then .Append(""" checked=""checked")
			'If Not enabled Then .Append(""" disabled=""disabled")
			.Append(""">")
			If label <> String.Empty Then .Append("<label for=""").Append(id).Append("""><small>").Append(label).Append("</small></label>")
			If cssClass.Length > 0 Then .Append("</div>")
		End With
		Return zHTML.ToString
	End Function
	Friend Function HTMLInputCheckBox(ByVal name As String, ByVal value As String, ByVal label As String) As String
		Dim zHTML As New StringBuilder

		zHTML.Append("<input type=""checkbox"" name=""").Append(name).Append(""" id=""").Append(name).Append(""" value=""").Append(value).Append(""">")
		If label <> String.Empty Then zHTML.Append("<label for=""").Append(name).Append("""><small>").Append(label).Append("</small></label>")
		Return zHTML.ToString
	End Function

	Friend Function HTMLInputRadio(ByVal fName As String, ByVal fValue As String, ByVal fLabel As String, ByVal selected As Boolean, ByVal id As String) As String
		Dim myHTML As New StringBuilder

		myHTML.Append("<label for=""").Append(id).Append("""><input type=""radio"" name=""").Append(fName).Append(""" value=""").Append(fValue)
		If selected Then myHTML.Append(""" checked=""checked")
		myHTML.Append(""" ID=""").Append(id).Append("""><small>").Append(fLabel).Append("</small></label>")
		Return myHTML.ToString
	End Function

	Friend Function HTMLDiv(ByVal divID As String, Optional ByVal visible As Boolean = False) As String
		Return (New StringBuilder).Append("<div id=""").Append(divID).Append(""" ").Append(StyleVisibleToggle(visible)).Append(">").ToString
	End Function

	Friend Function HTMLIcon(ByVal fileName As String) As String
		Return String.Format("<img src=""{0}"" class=""icon""/>", fileName)
	End Function

#End Region

	'### HTML custom elements ######################################################################################
#Region "HTML custom elements"

	Friend Function HTMLtextMore(ByVal myText As String, ByVal myOptions As String) As String
		Dim zHTML As New StringBuilder ', i As Integer
		'zHTML.Append(HTMLscript_begin)
		'zHTML.Append("function MI(objID){var pa=$(objID).style;if (pa.display==""none"") pa.display="""" else pa.display=""none""};")
		'zHTML.Append(HTMLscript_end)
		'zHTML.Append("<p>").Append(myText).Append("<a href=""javascript:MI('T2')"">More</a><span id=""T2"" name=""T2"" style=""display=none"">")
		''   js.Append("function evolSHP(objID,UpdatePix){var img;var pa=").Append(jsGetElem).Append("(objID).style;if (pa.display==""none""){pa.display="""";img=""close.gif"";}else{pa.display=""none"";img='open.gif';};if(UpdatePix) ").Append(jsGetElem).Append("(objID+'img').src='").Append(_PathPixToolbar).Append("panel'+img;}")
		'zHTML.Append(myOptions & "</span>")
		' zHTML.ToString
		zHTML.Append(myText).Append("<div class=""foot"">").Append(myOptions).Append("</div>")
		Return zHTML.ToString
	End Function

	Friend Function menuItem(ByVal label As String, ByVal url As String, ByVal pix As String, Optional ByVal enabled As Boolean = True, Optional ByVal UseEventRef As Boolean = True) As String
		'HTML for a single menu item
		Dim htmlTR As New StringBuilder

		If enabled Then
			If UseEventRef Then
				htmlTR.AppendFormat("<a href=""javascript:EvPost('{0}')"" class=""{1}"">", url, pix)
			Else
				htmlTR.AppendFormat("<a href=""{0}"" class=""{1}"">", url, pix)
			End If
			htmlTR.Append(label).Append(" </a>")
		Else
			htmlTR.AppendFormat("<span class=""{0}"">", pix).Append(label).Append(" </span>")
		End If
		Return htmlTR.ToString
	End Function

	Friend Function menuItemSearch(ByVal label As String, ByVal url As String, ByVal pix As String, Optional ByVal enabled As Boolean = True, Optional ByVal UseEventRef As Boolean = True) As String
		'HTML for a single menu item
		Dim htmlTR As New StringBuilder

		If enabled Then
			If UseEventRef Then
				htmlTR.AppendFormat("<a href=""javascript:Evol.showViewSearch('{0}')"" class=""{1}"">", url, pix)
			Else
				htmlTR.AppendFormat("<a href=""{0}"" class=""{1}"">", url, pix)
			End If
			htmlTR.Append(label).Append(" </a>")
		Else
			htmlTR.AppendFormat("<span class=""{0}"">", pix).Append(label).Append(" </span>")
		End If
		Return htmlTR.ToString
	End Function

	Friend Function GetValFromCSVTuples(ByVal myCSVTuples As String, ByVal myKey As String) As String
		'LG_sDateRange 
		'myCSVTuples= "day|24 hours,week|week,month|month,year|year"
		Dim i As Integer, j As Integer, buffer As String = String.Empty

		i = InStr(myCSVTuples, myKey & "|")
		If i > 0 Then
			i = i + myKey.Length
			j = InStr(i, myCSVTuples, ",")
			If j = i Then
				buffer = myCSVTuples.Substring(i)
			Else
				If i < j Then
					buffer = myCSVTuples.Substring(i, j - i - 1)
				End If
			End If
		End If
		Return buffer
	End Function

	Friend Function HTMLlovEnum(ByVal enumerationList As String, ByVal FieldName As String, Optional ByVal ItemID As Integer = 0, Optional ByVal abbrev As Boolean = True) As String
		'make a query and returns the HTML for a lov 
		Dim i As Integer, j As Integer, myHTML As New StringBuilder
		Dim curID As Integer, MaxLoop As Integer
		Dim currentEnum As String, bufferID As String, bufferValue As String
		Dim LOVenumerations() As String

		LOVenumerations = Split(enumerationList, ",")
		MaxLoop = LOVenumerations.Length
		If MaxLoop = 0 Then
			'ErrorMsg += "XML Error: element 'field' of type 'lov' with no attribute 'dbtablelov' or 'lovenumeration'."
			If FieldName <> String.Empty Then myHTML.Append(" - No list available -")
		Else
			For i = 0 To MaxLoop - 1
				currentEnum = LOVenumerations(i)
				j = InStr(currentEnum, "|")
				If j < 1 Then
					bufferID = currentEnum
					bufferValue = currentEnum
				Else
					bufferID = Left(currentEnum, j - 1)
					Try
						bufferValue = Right(currentEnum, currentEnum.Length - j)
					Catch : bufferValue = String.Empty : End Try
				End If
				If FieldName <> String.Empty AndAlso Not abbrev Then
					myHTML.Append(HTMLInputCheckBox(FieldName, bufferID, HtmlEncode(bufferValue), String.Empty, , , FieldName & CStr(i))).Append("&nbsp; ")
				Else
					If ItemID = 0 Then
						myHTML.Append(HTMLOption(bufferID, HtmlEncode(bufferValue)))
					Else
						curID = CInt(bufferID)
						myHTML.Append(HTMLOption(CStr(curID), HtmlEncode(bufferValue), ItemID = curID))
					End If
				End If
			Next
		End If
		Return myHTML.ToString
	End Function

	Friend Function HTMLbuttonTab(ByVal UID As String, ByVal Label As String, ByVal Index As Integer, ByVal selected As Boolean) As String
		Dim myHTML As New StringBuilder

		If selected Then
			myHTML.Append("<a class=""TabSelected""")
		Else
			myHTML.Append("<a class=""Tab""")
		End If
		myHTML.AppendFormat(" ID=""{0}TabB{1}"" href=""javascript:Evol.selTab('{0}','{1}');"" onClick=""javascript:Evol.selTab('{0}','{1}');""><nobr>", UID, Index)
		myHTML.Append(Label).Append("</nobr></a>")
		Return myHTML.ToString
	End Function

	Friend Function HTMLLinkShowVanish(ByVal divID As String, ByVal linklabel As String) As String
		Return String.Format("<a id=""{0}link"" href=""javascript:Evol.togglePanelOnce('{0}')"">{1}</a>", divID, linklabel)
	End Function

	Friend Function HTMLCommentFlag(ByVal nbCommentsRow As Integer, ByVal PixComments As String) As String
		Select Case nbCommentsRow
			Case 0
				Return String.Empty
			Case 1
				Return PixComments
			Case Else
				Return (New StringBuilder).Append(noBR_tag).Append(PixComments).Append("<font class=""sec"">(").Append(nbCommentsRow).Append(")</font></nobr>").ToString
		End Select
	End Function

	Friend Function StyleVisibleToggle(Optional ByVal Visible As Boolean = True) As String
		If Visible Then
			Return " style=""display:inline;"" "
		Else
			Return " style=""display:none;"" "
		End If
	End Function

	Friend Function Link4itemid(ByVal OriginalURL As String, ByVal ItemID As String) As String
		Return OriginalURL.Replace(p_itemid, ItemID)
	End Function

	Friend Function HTMLInputColor(ByVal fName As String, ByVal fValue As String, ByVal fLabel As String) As String
		Dim myHTML As New StringBuilder

		myHTML.Append("<tr><td colspan=""3"">").Append(HTMLFieldLabel(fName, fLabel))
		myHTML.Append("</td></tr><tr><td valign=""top""><input class=""Field"" type=""text"" style=""width:120;"" maxlength=""20""")
		myHTML.AppendFormat(" onKeyUp=""Evol.color('{0}')"" name=""{0}"" id=""{0}"" value=""{1}""></td><td>&nbsp;</td><td ID=""{0}COL""><div class=""ColorBox"" style=""background:{1}", fName, fValue)
		myHTML.Append("""> </div></td></tr>")
		Return myHTML.ToString
	End Function

	Friend Function HTMLtrColor(ByVal aColor As String) As String
		If String.IsNullOrEmpty(aColor) Then
			Return "<tr>"
		Else
			Return String.Format("<tr bgcolor=""{0}"">", aColor)
		End If
	End Function

	Friend Function HTMLInputDate(ByVal fName As String, ByVal fValue As String, ByVal locale As String, ByVal pixDir As String) As String
		Dim myHTML As New StringBuilder

		myHTML.Append("<nobr><input type=""text"" class=""Field Field80"" size=""15"" maxlength=""22""")
		myHTML.AppendFormat(" name=""{0}"" id=""{0}""", fName)
		myHTML.Append(" value=""").Append(fValue).Append(""">&nbsp;<a href=""javascript:ShowDatePicker('").Append(fName)
		If locale.Equals("FR") Then myHTML.Append("', false, 'dmy', '/")
		'myHTML.Append("');""><img src=""").Append(pixDir).Append("calendar.gif"" alt=""Date Picker"" class=""calendar""/></a></nobr>")
		myHTML.Append("');"" class=""ico Calendar""></a></nobr>")
		Return myHTML.ToString
	End Function

	Friend Function HTMLPanelBegin(ByVal panelWidth As String) As String   ', ByVal panelSummary As String
		Return String.Format("<table class=""Panel"" cellpadding=""6"" cellspacing=""0"" width=""{0}"" border=""0""><tr><td>", panelWidth)
	End Function

#End Region

	'### Dates ######################################################################################
#Region "Dates"

	Friend Function DefaultDateFormat(ByVal fType As String, ByVal locale As String) As String
		If locale.Equals("FR") Then
			Return "dd/MM/yyyy"
		Else
			Return "MM/dd/yyyy"
		End If
	End Function

	Friend Function TextNow() As String
		Return Format(Now, DateFormatST)
	End Function

	Friend Function formatedDateTime(ByVal aDate As DateTime) As String
		Return Format(aDate, DateFormatSD) & " " & Format(aDate, DateFormatST)
	End Function

	Friend Function HTMLDateFormated(ByVal fieldType As String, ByVal fieldValue As String, ByVal fieldFormat As String) As String
		If String.IsNullOrEmpty(fieldFormat) Then
			Select Case fieldType
				Case t_date
					Return Format(CDate(fieldValue), DateFormatSD)
				Case t_datetime
					Return formatedDateTime(CDate(fieldValue))
				Case t_time
					Return Format(CDate(fieldValue), DateFormatST)
			End Select
		Else
			Return Format(CDate(fieldValue), fieldFormat)
		End If
		Return String.Empty
	End Function

#End Region

	'### Conversions ######################################################################################
#Region "Conversions"

	Friend Function TextUcaseFirst(ByVal myString As String) As String
		Select Case myString.Length()
			Case 0
				Return String.Empty
			Case 1
				Return myString.ToUpper
			Case Else
				Return myString.Substring(0, 1).ToUpper & myString.Substring(1).ToLower
		End Select
	End Function

	Friend Function CondiConcat(ByVal original As String, ByVal newItem As String, Optional ByVal separator As String = ", ") As String
		If String.IsNullOrEmpty(original) Then
			Return newItem
		Else
			Return String.Format("{0}{1}{2}", original, separator, newItem)
		End If
	End Function

	Friend Function ValueOrDefault(ByVal Value As String, ByVal defaultValue As String) As String
		If String.IsNullOrEmpty(Value) Then
			Return defaultValue
		Else
			Return Value
		End If
	End Function

	Friend Function Text2HTML(ByVal myText As String) As String
		If String.IsNullOrEmpty(myText) Then
			Return String.Empty
		Else
			'Return myText.Replace(">", "&gt;").Replace("<", "&lt;")
			Return System.Web.HttpUtility.HtmlEncode(myText)
		End If
	End Function

	Friend Function Text2HTMLwBR(ByVal myText As String) As String
		If String.IsNullOrEmpty(myText) Then
			Return String.Empty
		Else
			'Return myText.Replace(">", "&gt;").Replace("<", "&lt;").Replace(vbCrLf, "<BR/>")
			Return HtmlEncode(myText).Replace(vbCrLf, "<BR/>")
		End If
	End Function

	Friend Function HTML2SQL(ByVal myHTML As String) As String
		If InStr(myHTML, "&") > -1 Then
			'Return myHTML.Replace("&gt;", ">").Replace("&lt;", "<")
			Return System.Web.HttpUtility.HtmlDecode(myHTML)
		Else
			Return myHTML
		End If
	End Function

	Friend Function String2Int(ByVal myString As String) As Integer
		Return CInt(Val(myString))
	End Function

#End Region

	'### Misc. ######################################################################################
#Region "Misc."

	Friend Function ModeRequestInt(ByVal strMode As String) As Integer
		If String.IsNullOrEmpty(strMode) Then
			Return 0
		Else
			Select Case strMode.ToLower()
				Case "view"
					Return 0
				Case "edit"
					Return 1
				Case "new"
					Return 12
				Case "selections"
					Return 60
				Case "search"
					Return 3
				Case "searchadv"
					Return 4
				Case "login"
					Return 50
				Case "export"
					Return 70
				Case "list"
					Return 110
					'Case "lastlist"
					'    Return 105
				Case Else
					Return 0
			End Select
		End If
	End Function

	Friend Function inQuote(ByVal myString As String) As String
		Return String.Format("""{0}""", myString.Replace("""", """"""))
	End Function

	Friend Function RandomHTMLColor() As String
		Select Case Int((4 * Rnd()) + 1)   ' Generate random value between 1 and 6.
			Case 1
				Return "#ffc222"  'viafone orange
			Case 2
				Return "#7BE57B"  '"#C0FFC0" 'light green
			Case 3
				Return "#BFDFFF"  'light blue    
			Case Else 'more often
				Return "#E0E0E0"  'light grey
		End Select
	End Function

	Friend Function RandomFieldValue(ByVal fieldType As String, Optional ByVal fieldFormat As String = "", Optional ByVal pixPath As String = "") As String
		Dim fieldValue As String = String.Empty, i As Integer
		'CInt(Int((6 * Rnd()) + 1)) ' Generate random value between 1 and 6.

		Select Case fieldType
			Case t_text, t_txtm	   ', "picture"
				Select Case CInt(Int((4 * Rnd()) + 1))
					Case 1 : fieldValue = "Text"
					Case 2 : fieldValue = "ABCDEF"
					Case 3 : fieldValue = "ABC"
					Case Else : fieldValue = "Some text"
				End Select
			Case t_lov
				Select Case CInt(Int((3 * Rnd()) + 1))
					Case 1 : fieldValue = "A"
					Case 2 : fieldValue = "B"
					Case Else : fieldValue = "C"
				End Select
			Case t_email
				Select Case CInt(Int((3 * Rnd()) + 1))
					Case 1 : fieldValue = "somebody@somewhere.com"
					Case 2 : fieldValue = "info@evolutility.org"
					Case Else : fieldValue = "me@myname.com"
				End Select
			Case t_url
				Select Case CInt(Int((3 * Rnd()) + 1))
					Case 1 : fieldValue = "http://www.google.com"
					Case 2 : fieldValue = "http://www.microsoft.com"
					Case Else : fieldValue = evoLink
				End Select
			Case t_bool
				If CInt(Int((2 * Rnd()) + 1)) > 1 Then
					If fieldFormat <> String.Empty Then
						fieldValue = "&nbsp;<img src=""" & pixPath & fieldFormat & """/>"
					Else
						fieldValue = "X"
					End If
				End If
			Case t_date
				i = CInt(Int((100 * Rnd()) + 1)) - 50
				fieldValue = Format(Now.AddDays(i), DateFormatSD)
				If fieldFormat <> String.Empty Then
					fieldValue = Format(Now.AddDays(i), fieldFormat)
				Else
					fieldValue = Format(Now.AddDays(i), DateFormatSD)
				End If
			Case t_time
				i = CInt(Int((8 * Rnd()) + 1))
				fieldValue = Format(Now.AddHours(i).AddMinutes(i + 5), DateFormatST)
			Case t_datetime
				i = CInt(Int((100 * Rnd()) + 1)) - 50
				fieldValue = Format(Now.AddDays(i), DateFormatSD) & " " & Format(Now.AddHours(i), DateFormatST)
			Case t_int
				i = CInt(Int((100 * Rnd()) + 1))
				fieldValue = Str(i).TrimStart
			Case t_dec
				Select Case CInt(Int((3 * Rnd()) + 1))
					Case 1 : fieldValue = "3.14"
					Case 2 : fieldValue = "12.34"
					Case Else : fieldValue = "2.00"
				End Select
			Case Else
				fieldValue = String.Empty
		End Select
		Return fieldValue
	End Function

#End Region

End Module

