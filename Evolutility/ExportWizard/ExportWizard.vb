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

Imports System.ComponentModel
Imports System.Drawing.ColorTranslator
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Xml
Imports System.Configuration
Imports System.Text

'[Assembly: TagPrefix("Evolutility", "Evol")] 

Namespace Evolutility.ExportWizard

	<Designer("Evolutility.ExportWizard.ExportWizardDesigner"), LicenseProviderAttribute(), DefaultEvent("Show"), ToolboxData("<{0}:ExportWizard runat=server ></{0}:ExportWizard>")> Public Class ExportWizard
		Inherits WebControl
		Implements IPostBackEventHandler


		'### UDT & Variables #################################################################################
#Region "UDT & Variables"
#Region "UDTs"
		Public Enum ewStep
			step1_Source = 0
			step2_SourceDetails = 1
			step3_OutputType = 2
		End Enum
		Public Enum ewOutputType
			CSV = 0
			TAB = 1
			XML = 2
			SQL = 3
			HTML = 4
		End Enum
		Public Enum ewParameter
			ObjectType = 0
			ObjectName = 1
			ObjectColumns = 2
			ExportType = 3
			Export = 4
		End Enum
#End Region

		Private firstTable As String, UID As String, _Enabled As Boolean = True
		Private _StepIndex As Integer = 1, expObjType As String = "", expObj As String = "", expObjDetails As String = "", expOut As String = ""
		Private _sourceObjectType As String = "", _sourceObject As String = "", _sourceColumns As String = "", separator As String, SQLQ As String

		Friend atRunTime As Boolean = True, _DesignStep As ewStep = ewStep.step1_Source
		Private PostBackEventUsed As Boolean = False
		Private ds As DataSet
		Private _SqlConnection As String, _MaxRecords As Integer
		Private _Title As String = "", _TitleCurrent As String, _HeaderSteps(2) As String, ParametersLoaded As Boolean = False
		Private _dbTable As String = "", _dbColumns As String
		Private _Step As Integer = 0

		Private myRandomColor As String, ie As Boolean
		Private ColorHeader As String, ColorOdd As String, ColorEven As String, XMLp1 As String, XMLp2 As String

		Private Const TableEnd As String = "</tr></table>"
		Private Const TrTd2Begin As String = "<tr valign=""top""><td colspan=""2"">"
		Private Const TrTd2BeginPanel As String = "<tr><td colspan=""2"" class=""Panel"">"
		Private Const TrTdEnd As String = "</td></tr>"
		Private Const pix_check As String = "check.gif", inNewBrowser As String = "_blank"
		Private Const ToolbarPixAttributes As String = " border=""0"" hspace=""2"" vspace=""2"""

		Private Const cssField As String = "Field"
		Private Const cssFieldLabel As String = "FieldLabel"
		Private Const cssPanelLabel As String = "PanelLabel"

		Private ErrorMsg As String = "", HeaderMsg As String = ""

#End Region

		'### Properties ######################################################################################
#Region "Properties"
#Region "Properties: Data"
		<Bindable(True), Category("Data"), DefaultValue(""), Description("Database connection string.")> Property SqlConnection() As String
			Get
				Return _SqlConnection
			End Get
			Set(ByVal Value As String)
				_SqlConnection = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(""), Description("Currently displayed step.")> ReadOnly Property StepIndex() As Integer
			Get
				If Not ParametersLoaded Then GetAllParameters()
				Return _StepIndex
			End Get
		End Property
		<Bindable(True), Category("Data"), DefaultValue(""), Description("Connection string to the Database.")> Property SourceObject() As String
			Get
				Return _sourceObject
			End Get
			Set(ByVal Value As String)
				_sourceObject = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(""), Description("Connection string to the Database.")> Property SourceColumns() As String
			Get
				Return _sourceColumns
			End Get
			Set(ByVal Value As String)
				_sourceColumns = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(10000), Description("Currently displayed step.")> Property MaxRecords() As Integer
			Get
				Return _MaxRecords
			End Get
			Set(ByVal Value As Integer)
				_MaxRecords = Value
			End Set
		End Property
#End Region

#Region "Properties: Appearance"
		<Bindable(True), Category("Appearance"), DefaultValue(""), Description("Application Title."), RefreshProperties(RefreshProperties.Repaint)> Property Title() As String
			Get
				Return _Title
			End Get
			Set(ByVal Value As String)
				_Title = Value
			End Set
		End Property
		<Bindable(True), Category("Appearance"), DefaultValue(""), Description("Introduction text for Step 1: DataSet."), RefreshProperties(RefreshProperties.Repaint)> Property HeaderStep1() As String
			Get
				Return _HeaderSteps(0)
			End Get
			Set(ByVal Value As String)
				_HeaderSteps(0) = Value
			End Set
		End Property
		<Bindable(True), Category("Appearance"), DefaultValue(""), Description("Introduction text for Step 2: Columns."), RefreshProperties(RefreshProperties.Repaint)> Property HeaderStep2() As String
			Get
				Return _HeaderSteps(1)
			End Get
			Set(ByVal Value As String)
				_HeaderSteps(1) = Value
			End Set
		End Property
		<Bindable(True), Category("Appearance"), DefaultValue(""), Description("Introduction text for Step 3: Output Type."), RefreshProperties(RefreshProperties.Repaint)> Property HeaderStep3() As String
			Get
				Return _HeaderSteps(2)
			End Get
			Set(ByVal Value As String)
				_HeaderSteps(2) = Value
			End Set
		End Property

#End Region

#Region "Properties: Designer"
		<Bindable(True), Category("Designer"), DefaultValue(ewStep.step1_Source), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), RefreshProperties(RefreshProperties.Repaint)> Property DesignStep() As ewStep
			Get
				Return _DesignStep
			End Get
			Set(ByVal Value As ewStep)
				_DesignStep = Value
			End Set
		End Property
#End Region
#End Region

		'### Events ##########################################################################################
#Region "Events"
		'------- DB events ---------------------
		Public Class ShowEventArgs
			Inherits EventArgs
			Private pTitle As String
			Private pStepID As Integer

			Public Sub New(ByVal Title As String, ByVal StepID As Integer) 'Constructor
				Me.pTitle = Title
				Me.pStepID = StepID
			End Sub
			Public ReadOnly Property StepID() As Integer
				Get
					Return pStepID
				End Get
			End Property
			Public ReadOnly Property Title() As String
				Get
					Return pTitle
				End Get
			End Property
		End Class

		<Category("Database"), Description("Raised on each step.")> _
		Public Event Show(ByVal sender As Object, ByVal e As ShowEventArgs)

		Protected Overridable Sub OnStepped(ByVal e As ShowEventArgs)
			RaiseEvent Show(Me, e)
		End Sub

#End Region

		'### Rendering #######################################################################################
#Region "Rendering"

		Protected Overrides Sub Render(ByVal output As System.Web.UI.HtmlTextWriter)
			Dim myScript As New StringBuilder

			If _StepIndex <> 4 Then
				output.Write(HTMLComments("ExportWizard - www.evolutility.org - (c) 2010 Olivier Giulieri"))
				output.Write(String.Format("<table ID=""{0}"" {1}", UID, " cellpadding=""10"" cellspacing=""0"" width=""100%"" class=""Panel"">"))
				If Not (String.IsNullOrEmpty(ErrorMsg) Or String.IsNullOrEmpty(HeaderMsg)) Then
					output.Write(TrTd2Begin)
					If Not String.IsNullOrEmpty(ErrorMsg) Then
						output.Write(FormMessage(ErrorMsg, "error"))
						ErrorMsg = "-"
					Else
						If Not String.IsNullOrEmpty(HeaderMsg) Then
							output.Write(FormMessage(HeaderMsg, "info"))
							HeaderMsg = ""
						End If
					End If
					output.Write(TrTdEnd)
				End If
				output.Write(FormStep(_StepIndex))
				If Not String.IsNullOrEmpty(_SqlConnection) OrElse Not atRunTime Then
					output.Write(TrTd2Begin)
					If _StepIndex > 1 Then
						output.Write(HTMLInputHidden(UID & "Obj", expObj))
						output.Write(HTMLInputHidden(UID & "ObjType", expObjType))
						If expObjType = "Q" Then output.Write(HTMLInputHidden(UID & "SQLQ", SQLQ))
						If _StepIndex <> 3 Then output.Write(HTMLInputHidden(UID & "RMX", GetPageRequest(UID & "RMX", CStr(_MaxRecords))))
					End If
					If _StepIndex > 2 Then
						output.Write(HTMLInputHidden(UID & "ObjD", expObjDetails))
						output.Write(HTMLInputHidden(UID & "ObjSort", Page.Request(UID & "ObjSort")))
					End If
					If _StepIndex = 4 Then
						output.Write(HTMLInputHidden(UID & "Out", expOut)) 'ready for "< Back"  
						output.Write(HTMLInputHidden(UID & "FLH", separator))
						output.Write(HTMLInputHidden(UID & "XMLP", XMLp1 & "~" & XMLp2))
						If expOut = "HTML" Then
							output.Write(HTMLInputHidden(UID & "C3", String.Format("{0}~{1}~{2}", ColorHeader, ColorOdd, ColorEven)))
						End If
					End If
					output.Write(HTMLInputHidden(UID & "StepID", CStr(_StepIndex)))
					If ErrorMsg <> "" And ErrorMsg <> "-" Then
						output.Write(FormMessage(ErrorMsg, "error"))
					End If
					output.Write(TrTdEnd)
				Else
					ErrorMsg = "No database connection specified."
				End If
				If ErrorMsg <> "" And ErrorMsg <> "-" Then
					output.Write(TrTd2Begin)
					output.Write(FormMessage(ErrorMsg, "error"))
					output.Write(TrTdEnd)
					ErrorMsg = "-"
				End If
				output.Write(FormButtons())
			Else
				output.Write(TrTd2Begin)
				FormStep4()
				output.Write(TrTdEnd)
			End If
			output.Write("</table>")
			output.Write(Signature())
		End Sub

		Friend Function FormButtons() As String
			Dim b As New StringBuilder()
			Const spaces2 As String = "&nbsp;&nbsp;"

			With b
				.Append("<tr class=""PanelLabel""><td>&nbsp;&nbsp;")
				If atRunTime Then
					.Append(HTMLInputButton(UID & "cancel", "Cancel", False, EventRef("1"), _Enabled And _StepIndex > 1))
				Else
					.Append(HTMLInputButton("", "Cancel", False, , _DesignStep > 0))
				End If
				.Append("</td><td><p align=""right"">&nbsp;&nbsp;")
				If atRunTime Then
					.Append(HTMLInputButton(UID & "back", "&lt; Back", False, EventRef(CStr(_StepIndex - 1)), _Enabled And _StepIndex > 1))
					.Append(spaces2)
					If _StepIndex < 3 Then
						.Append(HTMLInputButton(UID & "submit", "Next &gt;", True, , _Enabled))
					Else
						.Append(HTMLInputButton(UID & "finish", "Finish", False, EventRef("4"), _Enabled And _StepIndex = 3 And ErrorMsg = ""))
					End If
				Else
					.Append(HTMLInputButton("", "&lt; Back", False, , _Enabled And _DesignStep > 0))
					.Append(spaces2).Append(HTMLInputButton("", "Next &gt;", True, , _Enabled And _DesignStep < 3))
					.Append(spaces2).Append(spaces2).Append(HTMLInputButton("", "Finish", False, , _Enabled And _DesignStep < 3))
				End If
				.Append("&nbsp;&nbsp;</p></td></tr>")
			End With
			FormButtons = b.ToString
		End Function

#End Region

		'### Life ########################################################################################
#Region "Life"

		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			MyBase.OnLoad(e)
		End Sub

		Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
			Dim sql As New StringBuilder, sql2 As New StringBuilder
			Dim aSQL As String = "", NbFileUploads As Int32 = 0, DBCommand As Integer = 0
			Dim buffer As String, MustTriggerInsert As Boolean = False

			MyBase.OnPreRender(e)
			_Enabled = Me.Enabled
			If String.IsNullOrEmpty(_SqlConnection) Then
				_SqlConnection = GetAppSetting("SQLConnection")
			End If
			GetAllParameters()
			If atRunTime Then
				ie = Page.Request.Browser.Browser = "IE"
				If _Enabled Then
					'--- Javascript for hidding and showing panels
					buffer = "Evolew"
					sql.Remove(0, sql.Length)
					sql.Append(HTMLscript_begin)
					sql.Append(JSShowHidePanel())
					If _StepIndex = 1 Then
						sql.Append(JSExportSourceType())
					ElseIf _StepIndex > 2 Then
						sql.Append(JSExportSelectText())
						sql.Append(JSExportOutput())
						buffer += "O"
					End If
					sql.Append(HTMLscript_end)
					Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), buffer, sql.ToString)
				End If
				OnStepped(New ShowEventArgs(_TitleCurrent, _StepIndex))
			End If
		End Sub

		Public Overrides Sub Dispose()
			ds = Nothing
		End Sub

		Public Sub RaisePostBackEvent(ByVal eventArgument As String) Implements IPostBackEventHandler.RaisePostBackEvent
			If IsNumeric(eventArgument) Then
				_StepIndex = CInt(Val(eventArgument))
				PostBackEventUsed = True
			End If
		End Sub

		Friend Function GetAllParameters() As Boolean
			If Not ParametersLoaded Then
				UID = Replace(UniqueID, ":", "_")
				'////// get parameters //////////////////////////////
				expObjType = GetPageRequest(UID & "ObjType", "U")
				If expObjType = "Q" Then SQLQ = GetPageRequest(UID & "SQLQ")
				expObjDetails = GetPageRequest(UID & "ObjD", _sourceColumns)
				If Page.IsPostBack Then
					If Not PostBackEventUsed Then _StepIndex = CInt(Val(Page.Request(UID & "StepID"))) + 1
					expObj = Page.Request(UID & "Obj")
					If expObj = "" Then
						Select Case expObjType
							Case "V"
								expObj = Page.Request(UID & "Obj1")
							Case "U"
								expObj = Page.Request(UID & "Obj0")
						End Select
					End If
					expOut = GetPageRequest(UID & "Out", "CSV")
				Else
					_StepIndex = 1
					expObjDetails = _sourceColumns
					If _sourceObject <> "" Then
						expObj = _sourceObject
						If InStr(expObj, ",") < 1 Then
							If _StepIndex < 2 Then _Step = 2
						End If
					End If
				End If
				'skip step 2 (columns) for queries
				If expObjDetails = "" AndAlso expObjType = "Q" Then
					If _StepIndex = 2 Then
						If Page.Request(UID & "submit") <> "" Then
							_StepIndex = 3
						Else
							_StepIndex = 1
						End If
					End If
				End If
				_TitleCurrent = StepTitleLOV()
				If _Title <> "" Then _TitleCurrent = _Title & " " & _TitleCurrent
				ParametersLoaded = True
			End If
			Return ParametersLoaded
		End Function

#End Region

		'### Forms ###########################################################################################
#Region "Forms"

		Friend Function FormStep(ByVal StepID As Integer) As String
			Dim myHTML As New StringBuilder
			Dim lastj As Integer = 0, sql As String, sqlw As String = " sysobjects.xtype in ('U','V') AND ID", HTMLStepPanel As String
			Dim expObjID As String

			If atRunTime AndAlso InStr(_SqlConnection, "=") = 0 Then
				ErrorMsg = "Invalid database connection string."
			Else
				Select Case StepID
					'Case 1 'object     
					Case 2 'columns
						If Not atRunTime Then
							_TitleCurrent = "Columns to export"
						Else
							expObjID = expObj
							If expObjType <> "Q" Then
								If IsNumeric(expObj) Then
									If expObj.IndexOf(",") > 0 Then
										sqlw += String.Format(" in ({0})", expObj)
									Else
										sqlw += String.Format("={0}", expObj)
									End If
									sql = BuildSQL("name", "sysobjects", sqlw, "name")
									ds = GetData(sql, _SqlConnection)
									If Not ds Is Nothing Then
										expObj = CStr(ds.Tables(0).Rows(0)(0))
									End If
								End If
								_TitleCurrent = expObj & " Columns to export"
							Else
								_TitleCurrent = "Query"
							End If
						End If
						'Case 3 'output (w/ sample)  
					Case 4 'sometimes 4
						If expObjType = "Q" Then
							_TitleCurrent = "ExportWizard for custom query"
						Else
							_TitleCurrent = "ExportWizard for " & expObj
						End If
				End Select
				If expObjType = "Q" Then
					If StepID > 2 Then
						HTMLStepPanel = HTMLStep(StepID - 1, 2)
					Else
						HTMLStepPanel = HTMLStep(StepID, 2)
					End If
				Else
					HTMLStepPanel = HTMLStep(StepID, 3)
				End If
				If Not String.IsNullOrEmpty(_Title) Then _TitleCurrent = String.Format("{0} {1}", _Title, _TitleCurrent)
				myHTML.Append(HTMLPanelTitle(_TitleCurrent, "DBO", HTMLStepPanel))
				myHTML.Append("<tr valign=""top""><td colspan=""2"">")
				If _HeaderSteps(StepID - 1) <> "" Then	  'introduction text
					myHTML.Append("<p>").Append(_HeaderSteps(StepID - 1)).Append("</p>")
					myHTML.Append("</td></tr><tr valign=""top""><td colspan=""2"">")
				End If
				myHTML.Append("<table width=""100%"" border=""0"" cellspacing=""6""><tr valign=""top""><td>")
				Select Case StepID
					Case 1 'object  
						myHTML.Append(FormStep1())
					Case 2 'columns 
						myHTML.Append(FormStep2())
					Case 3 'output  
						myHTML.Append(FormStep3())
				End Select
				myHTML.Append("</td></tr></table>")
				myHTML.Append(TrTdEnd)
			End If
			Return myHTML.ToString
		End Function

		Private Function FormStep1() As String
			'Tables Views Query
			Dim myHTML As New StringBuilder, myHTML2 As New StringBuilder
			Dim i As Integer, lastj As Integer = 0, MaxLoop As Integer, sql As String, fieldName As String, buffer As String
			Dim iTab As Integer, nbTabs As Integer = 0, activeTab As Integer = 0

			nbTabs = 3
			myHTML.Append(_sourceObject)
			fieldName = UID & "ObjType"
			If _sourceObject <> "" Then
				'list of tables from property
				myHTML.Append(HTMLFieldTitle(UID, "", cssFieldLabel))
				If atRunTime Then
					myHTML.Append(HTMLInputSelect(UID & "Obj0", UID & "Obj0", cssField, _Enabled))
					myHTML.Append(HTMLlovEnum(_sourceObject))
					myHTML.Append("</option></select><p><br></p>")
				Else
					myHTML.Append("<select class=""Field""><option>- Select Table -</option></select>")
				End If
			Else
				If atRunTime Then
					'list of tables from database
					sql = BuildSQL("id, name,xtype", "sysobjects", " (xtype='U' OR  xtype='V')", "xtype,name")
					ds = GetData(sql, _SqlConnection)
					Try
						MaxLoop = ds.Tables(iTab).Rows.Count - 1
					Catch
						MaxLoop = -1
					End Try
					Select Case expObjType
						Case "V"
							activeTab = 1
						Case "Q"
							activeTab = 2
						Case Else
							activeTab = 0
					End Select
				Else
					activeTab = 0
				End If
				For iTab = 0 To nbTabs - 1
					'tabs buttons
					If iTab = 0 Then
						myHTML.Append(HTMLFieldTitle(UID, "Data source type", cssFieldLabel))
						For i = 0 To nbTabs - 1
							myHTML.Append("<span style=""cursor:hand"" ID=""").Append(UID).Append("TabB").Append(CStr(i))
							myHTML.Append(""" onClick=""EvoShowDBObj('").Append(UID).Append("', '").Append(fieldName).Append("', '").Append(CStr(i)).Append("');"">")
							Select Case i
								Case 0
									myHTML.Append(HTMLInputOption(fieldName, i, "Table", "U", , , _Enabled, expObjType = "" Or expObjType = "U"))
									If expObjType = "Q" Then activeTab = 0
								Case 1
									myHTML.Append(HTMLInputOption(fieldName, i, "View", "V", , , _Enabled, expObjType = "V"))
								Case 2
									myHTML.Append(HTMLInputOption(fieldName, i, "Query", "Q", , , _Enabled, expObjType = "Q"))
							End Select
							Select Case expObjType
								Case "U" : activeTab = 0
								Case "V" : activeTab = 1
								Case "Q" : activeTab = 2
							End Select
							myHTML.Append("<br></span>")
						Next
						'for outside panel
						myHTML.Append("<p><br><br><br></p></td><td width=""68%"">")	'for 2 columns in the main panel
					End If
					'begin tab
					myHTML.Append("<div id=""").Append(UID).Append("Tab").Append(CStr(iTab)).Append("""")
					myHTML.Append(StyleVisibleToggle(iTab = activeTab, ie))
					myHTML.Append(">")
					If iTab = 2 Then
						'Query
						myHTML.Append(HTMLFieldTitle(UID & "SQLQ", "SQL Query", cssFieldLabel))
						myHTML.Append("<textarea class=""Field"" rows=""8"" name=""").Append(UID).Append("SQLQ"" wrap=""soft"">")
						myHTML.Append(vbCrLf).Append(BuildSQL("*", firstTable)).Append(vbCrLf).Append("</textarea>")
					Else
						'Tables 'Views 
						If iTab = 0 Then buffer = "Table" Else buffer = "View"
						myHTML.Append(HTMLFieldTitle(UID, buffer, cssFieldLabel))
						If Not atRunTime Then
							If iTab = 0 Then myHTML.Append("<select class=""Field""><option>- Select Table -</option></select>")
						Else
							If ds Is Nothing Then
								ErrorMsg = "Cannot query database for lists of tables and views."
							Else
								If (iTab = 0 And MaxLoop > -1) Or (iTab = 1 And (MaxLoop - lastj) > -1) Then
									myHTML.Append(HTMLInputSelect(UID & "Obj" & iTab, UID & "Obj0", cssField, _Enabled))
									fieldName = UID & "Obj"
									Try
										firstTable = CStr(ds.Tables(0).Rows(0).Item("name"))
									Catch
									End Try
									If iTab = 0 Then 'tables
										For i = lastj To MaxLoop
											If Trim(CStr(ds.Tables(iTab).Rows(i).Item("xtype"))) = "V" Then
												Exit For
											End If
											myHTML.Append("<option value=""").Append(CStr(ds.Tables(iTab).Rows(i).Item("ID"))).Append(""">")
											myHTML.Append(CStr(ds.Tables(iTab).Rows(i).Item("name")))	 '& "</option>"  
										Next
										lastj = i
									Else 'views
										For i = lastj To MaxLoop
											myHTML.Append("<option value=""").Append(CStr(ds.Tables(0).Rows(i).Item("ID"))).Append(""">")
											myHTML.Append(CStr(ds.Tables(0).Rows(i).Item("name")))	  '& "</option>" 
										Next
									End If
									myHTML.Append("</option></select>")
								Else
									myHTML.Append("No ").Append(buffer)
								End If
							End If
						End If
					End If
					myHTML.Append("</div>")
					If Not atRunTime Then
						Exit For
					End If
				Next
			End If
			myHTML.Append(HTMLInputHidden(UID & "FT", firstTable))
			Return myHTML.ToString
		End Function

		Private Function FormStep2() As String
			Dim myHTML As New StringBuilder, OptionList As New StringBuilder
			Dim fieldName As String, fieldID As String, i As Integer, MaxLoop As Integer, sql As String, buffer As String
			Dim yesNo As Boolean, inSpan As Boolean = False

			myHTML.Append(HTMLFieldTitle("", SQLObjType(expObjType), cssFieldLabel))
			myHTML.Append(expObj).Append("<br><p>")
			If expObjType = "Q" AndAlso atRunTime Then
				sql = SQLQ
				If sql = "" Then
					ErrorMsg = "No Query provided."
				End If
			End If
			If expObjType <> "Q" Then 'Or Not atRunTime
				'Sort by
				myHTML.Append(HTMLFieldTitle(UID & "ObjSort", "Sort by", cssFieldLabel))
				myHTML.Append("<select class=""Field"" name=""").Append(UID).Append("ObjSort"">")
				If atRunTime Then
					If expObj.IndexOf(",") > 0 Then
						buffer = ""
						sql = String.Format(" AND id IN ({0})", expObj)

					Else
						If IsNumeric(expObj) Then
							buffer = ""
							sql = String.Format(" AND id={0}", expObj)
						Else
							buffer = ", sysobjects (nolock)"
							sql = " AND sysobjects.name='" & expObj & "' AND sysobjects.ID=syscolumns.ID"
						End If
					End If
					sql = BuildSQL("syscolumns.name as value", " systypes (nolock), syscolumns (nolock)" & buffer, "syscolumns.xtype=systypes.xtype AND systypes.length<>256 " & sql, "colid")
					ds = GetData(sql, _SqlConnection)
					If Not ds Is Nothing Then
						With ds.Tables(0)
							MaxLoop = .Rows.Count - 1
							For i = 0 To MaxLoop
								buffer = CStr(.Rows(i).Item("value"))
								OptionList.Append("<option value=""").Append(buffer).Append(""">")
								OptionList.Append(buffer)	 '& "</option>"  
							Next
						End With
					End If
				Else
					OptionList.Append("<option>ID")
				End If
				With myHTML
					.Append(OptionList)
					.Append("<br><br>").Append("</option></select>&nbsp;")
					'Sort by (2)
					.Append("<span id=""").Append(UID).Append("OS2LK"" style=""display:;""><small>[").Append(HTMLLink("javascript:EvoShowPanelLD('" & UID & "OS2','LK')", "More...")).Append("]</small></span>")
					.Append("</p><p><span id=""").Append(UID).Append("OS2"" style=""display:none;"">")
					.Append(HTMLFieldTitle(UID & "ObjSort", "Second Sort", cssFieldLabel))
					.Append("<select class=""Field"" name=""").Append(UID).Append("ObjSort2"">")
					.Append("<option value="""" selected>- None -")
					.Append(OptionList).Append("</option></select></span>")
					.Append("<br>&nbsp;</p>")
					'for outside panel
					.Append("</td><td width=""50%"">")
				End With
				'Columns 
				If expObjType <> "Q" Then
					myHTML.Append(HTMLFieldTitle("", "Source details", cssFieldLabel))
					fieldName = UID & "ObjD"
					yesNo = True
					If atRunTime Then
						If expObjDetails = "*" Then expObjDetails = ""
						expObjDetails = LCase(expObjDetails)
						If expObjDetails <> "" Then expObjDetails += ","
						'JavaScript for "Select All Fields"
						myHTML.Append(HTMLscript_begin)
						myHTML.Append("function EvolSAF(orNot){var t1;var t2;for (c=0;c<").Append(MaxLoop + 1).Append(";c++) {document.getElementById('").Append(UID).Append("F'+c).checked=orNot;};")
						myHTML.Append("if(orNot){t1='';t2='none'}else{t1='none';t2=''};EvoShowPanelS('").Append(UID).Append("LAF',t2);EvoShowPanelS('evoLNF',t1);")
						myHTML.Append("}")
						myHTML.Append(HTMLscript_end)
						fieldID = UID & "F"
						For i = 0 To MaxLoop
							buffer = CStr(ds.Tables(0).Rows(i).Item("value"))
							If expObjDetails <> "" Then yesNo = InStr(expObjDetails, LCase(buffer) & ",") > 0
							myHTML.Append(HTMLInputCheckBox(fieldName, buffer, buffer, , , _Enabled, yesNo, False, fieldID & i))  '.Append("<br>")
							If i = 0 Then
								If MaxLoop > 4 Then
									myHTML.Append("&nbsp;&nbsp;&nbsp;<span id=""").Append(UID).Append("LAF"" style=""display:;"">&nbsp;<small>[")
									myHTML.Append(HTMLLink("javascript:EvolSAF(-1)", "Select All")).Append("]</small></span>")
									myHTML.Append("<span id=""evoLNF"" style=""display:;"">&nbsp;<small>[")
									myHTML.Append(HTMLLink("javascript:EvolSAF(0)", "None")).Append("]</small></span>")
								End If
							ElseIf i = 7 Then
								If MaxLoop > 12 Then
									If expObjDetails = "" Then
										myHTML.Append("<span id=""MFTSLK"" style=""display:;""><br>...&nbsp;&nbsp;<small>[").Append(HTMLLink("javascript:EvoShowPanelLD('MFTS','LK')", "Show all fields")).Append("]</small></span>")
										myHTML.Append("<span id=""MFTS"" style=""display:none;"">")
										inSpan = True
									End If
								End If
							End If
							myHTML.Append("<br>")
						Next
					Else
						'hard coded dummy data for display in designer mode
						myHTML.Append(HTMLInputCheckBox("", "", "ID", , , _Enabled, True, False))
						myHTML.Append("&nbsp;&nbsp;&nbsp;<small>[").Append(HTMLLink("#", "Select All")).Append("]</small>")
						myHTML.Append("<br>")
						Dim myFields() As String = Split("Firstname,Lastname,Title,Company,Phone1,Phone2,email", ",")
						For i = 0 To myFields.Length - 1
							myHTML.Append(HTMLInputCheckBox("", "", myFields(i), , , _Enabled, True, False)).Append("<br>")
						Next
						myHTML.Append("&nbsp;&nbsp;...&nbsp; <small>[").Append(HTMLLink("#", "Show all fields")).Append("]&nbsp;[").Append(HTMLLink("#", "None")).Append("]</small>")
					End If
				End If
				ds = Nothing
				If inSpan Then myHTML.Append("</span>")
			End If
			Return myHTML.ToString
		End Function

		Private Function FormStep3() As String
			'Output type
			Dim myHTML As New StringBuilder
			Dim RequestValues() As String, fieldName As String

			'##### Output Type (dropdown)
			fieldName = UID & "Out"
			With myHTML
				.Append("<p>").Append(HTMLFieldTitle("", "Output type", cssFieldLabel))
				.Append("<select class=""Field"" name=""").Append(fieldName).Append(""" onChange=""javascript:EvoDBExptOpts('").Append(UID).Append("',this.value)"">")
				.Append(HTMLInputOption2("CSV", "Comma separated (CSV, TXT, XLS...)", expOut = "CSV"))
				If atRunTime Then
					.Append(HTMLInputOption2("HTML", "HTML", expOut = "HTML"))
					.Append(HTMLInputOption2("SQL", "SQL Insert Statements (SQL)", expOut = "SQL"))
					.Append(HTMLInputOption2("TAB", "Tab separated values (TXT)", expOut = "TAB"))
					.Append(HTMLInputOption2("XML", "XML", expOut = "XML"))
				End If
				.Append("</select></p><p>")
				'# Maximum number of rows
				.Append(HTMLFieldTitle("", "Max. number of rows", cssFieldLabel))
				.Append(HTMLInputText(UID & "RMX", GetPageRequest(UID & "RMX", CStr(_MaxRecords)), , cssField, _Enabled, 12, " style=""width:90;"" "))
				.Append("</p><p>")
				'# object
				.Append(HTMLFieldTitle("", SQLObjType(expObjType), cssFieldLabel))
				.Append(expObj)
				'# other fields
				.Append("</p></td><td>") 'for outside panel 
				If expOut = "" Then expOut = "CSV"
				'##### CSV, TAB - First line for field names
				.Append("<div id=""").Append(UID).Append("CSV""").Append(StyleVisibleToggle(expOut = "CSV" Or expOut = "TAB", ie)).Append("><p>")
				'# field - header
				.Append(HTMLFieldTitle(UID & "FLH", "Header", cssFieldLabel))
				.Append(HTMLInputCheckBox(UID & "FLH", "1", "First line for field names", , , _Enabled, True, False))
				'# field - separator
				'# - csv - any separator
				.Append("</p><div id=""").Append(UID).Append("csv2""").Append(StyleVisibleToggle(expOut = "CSV", ie)).Append("><p>")
				fieldName = UID & "FLS"
				.Append(HTMLFieldTitle(fieldName, "Separator", cssFieldLabel))
				.Append(HTMLInputText(fieldName, GetPageRequest(fieldName, ","), "", cssField, _Enabled, 5, " style=""width:30;"" "))
				.Append("</p></div><div id=""").Append(UID).Append("tab2""").Append(StyleVisibleToggle(expOut = "TAB", ie)).Append("><p>")
				'# - tab - hardcoded tab
				.Append(HTMLFieldTitle("", "Separator", cssFieldLabel))
				.Append("TAB")
				.Append("</p></div>")
				If atRunTime Then
					'# XML - Root element name
					.Append("</p></div><div id=""").Append(UID).Append("XML""").Append(StyleVisibleToggle(expOut = "XML", ie)).Append("><p>")
					RequestValues = Split(GetPageRequest(UID & "XMLP", expObj & "List~E"), "~")
					If XMLp1 = "" Then XMLp1 = expObj & "List"
					XMLp2 = RequestValues(1)
					.Append(HTMLFieldTitle(UID & "REM", "Root element name", cssFieldLabel))
					.Append(HTMLInputText(UID & "REM", XMLp1, , cssField, _Enabled, 30))
					.Append("</p><p>")
					.Append(HTMLFieldTitle(UID & "C2X", "Columns map to", cssFieldLabel))
					.Append(HTMLInputOption(UID & "C2X", 1, "Elements", "E", , , _Enabled, XMLp2 <> "A")).Append("<br>")
					.Append(HTMLInputOption(UID & "C2X", 2, "Attributes", "A", , , _Enabled, XMLp2 = "A"))
					'# HTML - Header color + Color odd rows + Color even rows
					.Append("</p></div><div id=""").Append(UID).Append("HTML""").Append(StyleVisibleToggle(expOut = "HTML", ie)).Append("><p>")
					.Append("<table cellpadding=""0"" cellspacing=""0"" border=""0"">")
					RequestValues = Split(GetPageRequest(UID & "C3", "#D5D5D5~#EDEDED~#F3F3F3"), "~")
					.Append(HTMLColorField(UID & "RCT", RequestValues(0), "Header color"))
					.Append(HTMLColorField(UID & "RCO", RequestValues(1), "Color odd rows"))
					.Append(HTMLColorField(UID & "RCE", RequestValues(2), "Color even rows"))
					.Append("</table>")
					'# SQL - transaction 
					.Append("</p></div><div id=""").Append(UID).Append("SQL""").Append(StyleVisibleToggle(expOut = "SQL", ie)).Append("><p>")
					.Append(HTMLFieldTitle(UID & "TRS", "SQL Options", cssFieldLabel))
					.Append(HTMLInputCheckBox(UID & "TRS", "1", "Inside Transaction", , , _Enabled, False, False))
					.Append("<br>").Append(HTMLInputCheckBox(UID & "TRS2", "1", "Identity Insert", , , _Enabled, False, False))
					.Append("</p></div>")
				End If
			End With
			Return myHTML.ToString
		End Function

		Private Sub FormStep4()
			Dim Filename As String
			Dim i As Integer, buffer As String, OrderBy As String, sql As String

			If expOut = "" Then expOut = "CSV"
			If atRunTime Then
				i = CInt(Val(GetPageRequest(UID & "RMX", CStr(_MaxRecords))))
				If expObjType <> "Q" Then
					sql = ""
					If i > 0 Then sql += String.Format("TOP {0} ", i) 'leave the space!
					sql += expObjDetails
					OrderBy = GetPageRequest(UID & "ObjSort")
					buffer = Page.Request(UID & "ObjSort2")
					If buffer <> "" Then OrderBy = String.Format("{0},{1}", OrderBy, buffer)
					sql = BuildSQL(sql, expObj, , OrderBy)
					buffer = expObj
				Else
					sql = SQLQ
					buffer = String.Empty
				End If
				If sql.Length < 35 Then
					Filename = sql
				Else
					Filename = "ExportWizard"
				End If

				Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
				response.Clear()

				response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}.{1}", Filename, expOut))
				If expOut = "CSV" Then
					response.ContentType = "application/ms-excel"
				Else
					response.ContentType = "application/octet-stream"
				End If
				response.Charset = ""
				'buffer = GenerateExport(sql, buffer, expOut, )
				'Dim Encoding As New UTF8Encoding
				'response.AddHeader("Content-Length", Encoding.GetByteCount(buffer).ToString())
				response.BinaryWrite(New UTF8Encoding().GetBytes(GenerateExport(sql, buffer, expOut)))
				response.End()
			End If
		End Sub

		Private Function GenerateExport(ByVal sql As String, ByVal tableName As String, Optional ByVal outputType As String = "CSV", Optional ByVal maxRow As Integer = 0) As String
			Dim i As Integer, j As Integer, maxLoop As Integer, fieldValue As String
			Dim buffer As String = "", buffer1 As String, buffer2 As String, maxCol As Integer, yesNo As Boolean = True, yesNo2 As Boolean = False
			Dim mySQL As New StringBuilder
			Const vbCrLf2 As String = vbCrLf & vbCrLf

			If outputType = "" Then outputType = "CSV"
			ds = GetData(sql, _SqlConnection)
			If Not ds Is Nothing Then
				maxLoop = ds.Tables(0).Rows.Count - 1
				If maxRow > 0 Then
					If maxRow < maxLoop Then
						maxLoop = maxRow
					End If
				End If
				maxCol = ds.Tables(0).Columns.Count - 1
				Select Case outputType
					Case "XML"
						With ds.Tables(0)
							If tableName = "" Then tableName = "Query"
							.TableName = tableName
							XMLp1 = GetPageRequest(UID & "REM", "ExportWizard")
							If XMLp1 <> "" Then
								XMLp1 = XMLp1.Replace("<", "").Replace(">", "").Replace("""", "'")
							End If
							ds.DataSetName = XMLp1
							XMLp2 = Page.Request(UID & "C2X")
							If XMLp2 = "A" Then
								For i = 0 To maxCol
									.Columns(i).ColumnMapping = MappingType.Attribute
								Next
								If maxCol <> maxCol Then
									For i = maxCol + 1 To maxCol
										.Columns(i).ColumnMapping = MappingType.Hidden
									Next
								End If
							Else
								If maxCol <> maxCol Then
									For i = maxCol + 1 To maxCol
										.Columns(i).ColumnMapping = MappingType.Hidden
									Next
								End If
							End If
						End With
						mySQL.Append(ds.GetXml())
					Case "HTML"
						mySQL.Append("<table width=""100%"" ID=""ExportWizard"">").Append(vbCrLf)
						'header 
						ColorHeader = Page.Request(UID & "RCT")
						If ColorHeader = "" Then
							mySQL.Append("<tr>")
						Else
							mySQL.Append("<tr bgcolor=""").Append(ColorHeader).Append(""">")
						End If
						mySQL.Append(vbCrLf)
						With ds.Tables(0)
							For j = 0 To maxCol
								mySQL.Append(" <th>").Append(CStr(ds.Tables(0).Columns(j).ColumnName)).Append("</th>").Append(vbCrLf)
							Next
							mySQL.Append("</tr>").Append(vbCrLf)
							'body 
							ColorOdd = Page.Request(UID & "RCO")
							If ColorOdd <> "" Then buffer1 = "<tr bgcolor=""" & ColorOdd & """>" Else buffer1 = "<tr>"
							ColorEven = Page.Request(UID & "RCE")
							If ColorEven <> "" Then buffer2 = "<tr bgcolor=""" & ColorEven & """>" Else buffer2 = "<tr>"
							buffer1 += vbCrLf
							buffer2 += vbCrLf
							For i = 0 To maxLoop
								If yesNo Then mySQL.Append(buffer1) Else mySQL.Append(buffer2)
								With .Rows(i)
									For j = 0 To maxCol
										mySQL.Append(" <td>")
										Try
											mySQL.Append(.Item(j))
										Catch
										End Try
										mySQL.Append("</td>").Append(vbCrLf)
									Next
								End With
								mySQL.Append("</tr>").Append(vbCrLf)
								yesNo = Not yesNo
							Next
						End With
						mySQL.Append("</table>").Append(vbCrLf)
					Case "SQL"
						mySQL.Append(vbCrLf).Append("/*** ExportWizard ").AppendFormat("{0} - {1} {2} ***/", tableName, Format(Now, "Short Date"), Format(Now, "Short Time")).Append(vbCrLf2)
						yesNo = Page.Request(UID & "TRS") <> ""
						If yesNo Then mySQL.Append("BEGIN TRANSACTION").Append(vbCrLf2)
						yesNo2 = Page.Request(UID & "TRS2") <> ""
						If yesNo2 Then mySQL.Append("SET IDENTITY_INSERT ").Append(tableName).Append(" ON;").Append(vbCrLf2)
						If tableName = "" Then tableName = "YOUR_TABLE"
						Dim sb As StringBuilder = New StringBuilder
						sb.AppendFormat("INSERT INTO {0} (  ", tableName)
						For i = 0 To maxCol	'i=1 not ID
							sb.AppendFormat("{0}, ", ds.Tables(0).Columns(i).ColumnName)
						Next
						sb.Remove(sb.Length - 2, 2)
						sb.Append(")").Append(vbCrLf).Append("  VALUES (  ")
						buffer = sb.ToString()
						With ds.Tables(0)
							For i = 0 To maxLoop
								mySQL.Append(buffer)
								For j = 0 To maxCol
									Try
										fieldValue = CStr(.Rows(i)(j))
									Catch
										fieldValue = ""
									End Try
									mySQL.Append(dbformat2(fieldValue, .Columns(j).DataType.ToString))
									mySQL.Append(", ")
								Next
								mySQL.Remove(mySQL.Length - 2, 2)
								mySQL.Append(");").Append(vbCrLf)
							Next
						End With
						mySQL.Append(vbCrLf)
						If yesNo2 Then mySQL.Append("SET IDENTITY_INSERT ").Append(tableName).Append(" OFF;").Append(vbCrLf2)
						If yesNo Then mySQL.Append(vbCrLf).Append("COMMIT TRANSACTION").Append(vbCrLf2)
					Case Else '"CSV", "TAB", "TXT", "XLS"
						If outputType = "TAB" Then
							separator = vbTab
						Else
							separator = GetPageRequest(UID & "FLS", ",")
						End If
						With ds.Tables(0)
							'- header
							If GetPageRequest(UID & "FLH") = "1" Then
								If separator <> "" Then
									For j = 0 To maxCol
										Try
											fieldValue = .Columns(j).ColumnName
										Catch
											fieldValue = ""
										End Try
										mySQL.AppendFormat("""{0}""", fieldValue)
										If j < maxCol Then mySQL.Append(separator)
									Next
								End If
								mySQL.Append(vbCrLf)
							End If
							'- body
							For i = 0 To maxLoop
								For j = 0 To maxCol
									Try
										fieldValue = CStr(.Rows(i)(j))
									Catch
										fieldValue = ""
									End Try
									If fieldValue <> "" Then
										If InStr(fieldValue, separator) > 0 Then
											mySQL.AppendFormat("""{0}""", fieldValue.Replace("""", """"""))
										Else
											mySQL.Append(fieldValue)
										End If
									End If
									If j < maxCol Then mySQL.Append(separator)
								Next
								mySQL.Append(vbCrLf)
							Next
						End With
				End Select
				mySQL.Append(vbCrLf)
			Else
				mySQL.Append("No data to export")
			End If
			Return mySQL.ToString
		End Function

#End Region

		'### Forms Support ###################################################################################
#Region "Forms Support"

		Friend Function HTMLColorField(ByVal fieldName As String, ByVal fieldValue As String, ByVal fieldTitle As String) As String
			Dim myHTML As New StringBuilder

			myHTML.Append("<tr><td colspan=""3"">")
			myHTML.Append(HTMLFieldTitle(fieldName, fieldTitle, cssFieldLabel))
			myHTML.Append("</td></tr><tr><td>")
			myHTML.Append(HTMLInputText(fieldName, fieldValue, , cssField, , 20, " style=""width:120;"" onKeyUp=""javascript:EvolCS('" & fieldName & "')"""))
			myHTML.Append("</td><td>&nbsp;</td><td style=""border: solid 1px Gray;"" ID=""")
			myHTML.Append(fieldName).Append("COL""><table border='0'><tr><td bgcolor=""").Append(fieldValue).Append(""">&nbsp;&nbsp;&nbsp;</td></tr></table></td></tr>")
			Return myHTML.ToString
		End Function

		Private Function HTMLlovEnum(ByVal enumerationList As String, Optional ByVal FieldName As String = "", Optional ByVal ItemID As Integer = 0) As String
			'make a query and returns the HTML for a lov 
			Dim i As Integer, j As Integer, MaxLoop As Integer, bufferID As String, bufferValue As String
			Dim myHTML As New StringBuilder
			Dim currentEnum As String, LOVenumerations() As String

			If atRunTime Then
				LOVenumerations = Split(enumerationList, ",")
				MaxLoop = LOVenumerations.Length
				If MaxLoop = 0 Then
					If FieldName <> "" Then myHTML.Append(" - No list available -")
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
							Catch
								bufferValue = ""
							End Try
						End If
						If ItemID = 0 Then
							myHTML.Append(HTMLInputOption2(bufferID, bufferValue))
						Else
							myHTML.Append(HTMLInputOption2(bufferID, bufferValue, ItemID = CInt(bufferID)))
						End If
					Next
				End If
			End If
			Return myHTML.ToString
		End Function

		Private Function EventRef(ByVal EventParam As String) As String
			Return Page.ClientScript.GetPostBackEventReference(Me, EventParam)
		End Function

		Private Function HTMLPanelTitle(ByVal panelTitle As String, ByVal panelID As String, ByVal alternateCell2 As String) As String
			Dim HTML As New StringBuilder

			If panelTitle <> "" Then
				HTML.Append("<tr><td class=""PanelLabel"">&nbsp;").Append(panelTitle).Append("")
				If alternateCell2 <> "~" Then
					If alternateCell2 <> "" Then
						HTML.Append("</td><td class=""PanelLabel""><p align=""right"">")
						HTML.Append(alternateCell2).Append("</p>")
					End If
				End If
				HTML.Append(TrTdEnd)
			End If
			Return HTML.ToString
		End Function

		Private Function StepTitleLOV() As String
			Select Case _StepIndex
				Case 1 'object     
					Return "Data Source to export"
				Case 2 'columns
					Return "Source details"
				Case 3 'output 
					Return "Output Type"
				Case 4 'Finished
					Return "Export"
				Case Else
					Return ""
			End Select
		End Function

		Private Function GetPageRequest(ByVal ReqKey As String, Optional ByVal DefaultValue As String = "") As String
			Dim Buffer As String

			If atRunTime Then
				Buffer = Page.Request(ReqKey)
				If Buffer = "" Then Buffer = DefaultValue
			Else
				Buffer = DefaultValue
			End If
			Return Buffer
		End Function

		Private Function GetAppSetting(ByRef key As String) As String
			If Not System.Configuration.ConfigurationManager.AppSettings(key) Is Nothing Then
				Return System.Configuration.ConfigurationManager.AppSettings(key).ToString
			Else
				Return ""
			End If
		End Function
#End Region

		'### JavaScript ######################################################################################
#Region "JavaScript"
		'JSShowHidePanel    'EvoShowPanel - ShowHidePanel
		'JSValidation       'ckob - CheckRec 
		'JSShowTabs         'EvoShowDBObj - ShowHideTab
		'JSEditDetails      'erw -  EditRow
		'JSEditImages       'EvolDPX -  DelPix

		Private Function JSExportSourceType() As String
			'Used in Step 1
			Dim i As Integer
			Dim js As New StringBuilder

			Select Case expObjType
				Case "V"
					i = 1
				Case "Q"
					i = 2
				Case Else
					i = 0
			End Select
			js.AppendFormat("var EvoTS='{0}';", i)
			js.Append("function EvoShowDBObj(o1,o2,o3){var tn=o1+'Tab';document.getElementById(tn+o3).style.display='';")
			js.Append("if(EvoTS!=o3){document.getElementById(tn+EvoTS).style.display='none';EvoTS=o3;document.getElementById(o2+o3).checked=""checked"";}}")
			Return js.ToString
		End Function

		Private Function JSExportOutput() As String
			'Used in Step 3
			Dim js As New StringBuilder, buffer As String

			buffer = expOut
			If buffer = "" Then buffer = "CSV"
			js.Append("var EvoDBEOC='").Append(buffer).Append("';")
			'### Output ### EvolDBEO(UID,ObjectID)
			js.Append("function EvoDBExptOpts(UID,oID){if(oID=='TAB'){oID='CSV';EvolOST(UID,'none','')}")
			js.Append("else{if(oID=='CSV'){EvolOST(UID,'','none')}};")
			js.Append("if(EvoDBEOC=='TAB')EvoDBEOC='CSV';EvoShowPanelS(UID+EvoDBEOC,'none');EvoShowPanelS(UID+oID,'');EvoDBEOC=oID};")
			'show details
			js.Append("function EvolOST(UID,a,b){EvoShowPanelS(UID+'csv2',a);EvoShowPanelS(UID+'tab2',b);};")
			'### ColorShow ### EvolCS(ObjectID)
			js.Append("function EvolCS(oID){var c=document.getElementById(oID).value;var cc=document.getElementById(oID+'COL');cc.innerHTML='<table colspan=0 cellspan=0><tr><td bgcolor=""'+c+'"">&nbsp;&nbsp;&nbsp;</td></tr></table>'};")
			Return js.ToString
		End Function

		Private Function JSExportSelectText() As String
			'Used in Steps 2 and 4
			Dim js As New StringBuilder

			js.Append("function EvolFSA(objID){var e=document.getElementById(objID);if(e){e.select()}};")
			Return js.ToString
		End Function

		Private Function JSShowHidePanel() As String
			'Used in Steps 1, 2, 3, 4
			Dim js As New StringBuilder
			'### ShowHidePanelSimple ### EvoShowPanelS(ObjectID, newStyle)
			js.Append("function EvoShowPanelS(oID,sD){document.getElementById(oID).style.display=sD};")
			'### ShowHidePanelLinkDisapear ### EvoShowPanelLD(ObjectID, linkFix)
			js.Append("function EvoShowPanelLD(oID,linkFix){EvoShowPanelS(oID,'');EvoShowPanelS(oID+linkFix,'none')}")
			Return js.ToString
		End Function

#End Region

		'### Public Methods #########################################################################################
#Region "PublicMethods"

		'Public Function GetExport(ByVal Table As String, Optional ByVal Columns As String = "*", Optional ByVal exportType As String = "CSV") As String
		'	Return GenerateExport(BuildSQL(Columns, Table), Table, exportType)
		'End Function

		Public Function GetParameterValue(ByVal Parameter As ewParameter) As String

			GetAllParameters()
			' _StepIndex As Integer = 1, expObjType As String = "", expOut As String = ""
			Select Case Parameter
				Case ewParameter.ObjectName
					Return expObj
				Case ewParameter.ObjectColumns
					Return expObjDetails
				Case ewParameter.ExportType
					Return expObjType
				Case ewParameter.Export
					Return GenerateExport("", "")
			End Select
			Return ""
		End Function

#End Region

	End Class

End Namespace
