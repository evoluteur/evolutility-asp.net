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

Imports System.ComponentModel
Imports System.Drawing
'Imports System.Drawing.Image
Imports System.Drawing.ColorTranslator
Imports System.Web.UI
Imports System.Web.UI.WebControls
'Imports System.Web.Caching
'Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Xml.Serialization
Imports System.IO
Imports System.Configuration
Imports System.Text
Imports System.Web.HttpUtility


Namespace Evolutility.WebControls

    '### UDT #################################################################################
#Region "UDT"
	Public Enum EvolSecurityModel	  'coupled w/ ReadOnly
		Single_User = 0	'everybody does everything
		Single_User_Password = 1 'need password to view data 
		Multiple_Users_RLS = 3 'each user only sees his own data
		Multiple_Users_Sharing = 4 'everybody sees all but updates his only
		'groups / MAYBE OTHER PROPERTY
		'Single_User = 0 'everybody does everything
		'Single_User_Password = 1 'need password to view data 
		'Multiple_Users_RLS = 3 'each user only sees his own data
		'Multiple_Users_Sharing = 4 'read all + update his own
		'Multiple_Users_Collaboration = 5 'read all + update all
		'Multi_Tenant = 6 'read group + update his own
		'Multi_Tenant_Collaboration = 7 'read group + update group
	End Enum
	Public Enum EvolCommentsMode
		None = -1
		Read_Only = 0
		Logged_Users = 1
		Anonymous = 2
	End Enum
#End Region

    <Designer("Evolutility.WebControls.UIServerDesigner"), LicenseProviderAttribute(),  DefaultEvent("DBChange" ), DefaultProperty("Text"), ToolboxData("<{0}:UIServer runat=server VirtualPathToolbar=PixEvo ></{0}:UIServer>"), AttributeUsageAttribute(AttributeTargets.Assembly, AllowMultiple:=False, Inherited:=False)> _
    Public Class UIServer
        Inherits WebControl
        Implements IPostBackEventHandler
        Implements IPostBackDataHandler


		'### Variables #######################################################################################
#Region "Variables"

#Region "UDTs"

		Public Enum EvolDisplayMode
			View = 0
			Edit = 1
			NewItem = 2
			Search = 3
			AdvancedSearch = 4
			'SelfHealing = 71
			List = 110 '102
			'ListEdit = 125
			'Credits = 1200
			'Specifications = 200
			'Templates = 500
			Design = 200
			Login = 50
			Selections = 60
			Export = 70
		End Enum

		Public Enum EvolToolbarPosition
			Top = 0
			'Left = 1
			Top_And_Bottom = 2
			None = -1
		End Enum
		'Public Structure SQLwithEnglish
		'    Public SQL As String, English As String

		'    Public Sub New(ByVal SQL As String, ByVal English As String)
		'        Me.SQL = SQL
		'        Me.English = English
		'    End Sub
		'End Structure

#End Region

		Friend atRunTime As Boolean = True, _DesignTabIndex As Integer = 0, _DesignDisplayMode As EvolDisplayMode, _DesignWebPath As String = ""
		Private UID As String, navBar As String, lockDbname As String, loclValue As String
		Private _FormID As Integer = 0, _DBApplicationKey As String = String.Empty, _Language As String = "EN"
		Private _ItemID As Integer = 0, _UserID As Integer = -1
		Private Const evoName As String = "Evolutility", dot As String = "."
		Private PostBackEventUsed As Boolean = False, ToDo As Integer = 0, ToDoString As String = String.Empty
		Private _Text As String, field_str As String = "field", data_str As String = "data"
		Private ValidationMsg As String, HeaderMsg As String = String.Empty	', ErrorMsg As String = "" 
		Private nav As Integer, navd As Integer = 2, _NavLinks As Boolean = True
		Private _ShowTitle As Boolean = True
		Private _PathPixToolbar As String = "", _PathPix As String = "", _PathDesign As String = ""
		Private _DisplayMode As Integer = 0, _DisplayModeStart As EvolDisplayMode
		Private Const dMode As String = "EVOL_Mode"
		Private ErrorMsg As String = String.Empty
		Private PixComments As String  '= "&nbsp;<img src=""" & _PathPixToolbar & "comments.gif"">"   
		Dim _Debug As Boolean = True ', DebugMsg As String = "" 

		'### Variables - DB & XML ###
#Region "Variables DB & XML"

		Private nsManager As XmlNamespaceManager
		Private useEvoNS As Boolean = False
		Private ds As DataSet, ds2 As DataSet
		Private _SqlConnection As String ', sqlConnectionDico As String     

		Private dbcolumnpix As String = String.Empty, dbcolumnpixdetails As String
		Private sqlISbuilt As Integer = -1, icon As String	  ' _ItemLock As Integer = -1, 

		Private def_Data As Data
		Private Const dbPrimaryKey As String = "ID"
		'Private dbuserlog As String = "", dbupdatelogs As String = "", dbmylanuserlogMSG As String = "" 
		Private dbwherelock As String  ', dbLockinglabel As String
		Private newOrderBy As String = dbPrimaryKey, _RowsPerPage As Integer = 20, pageID As Integer = 1, queryUrlParam As String = ""
		Private dbtabledetails As String = "", dbcolumndetails As String = "", nbDetailsEdit As Integer, detailsLoaded As Boolean = False
		Private JSValidDone As Boolean = False, JSDetailsDone As Boolean = False, nbFieldEditable As Integer = 0

		Private _DBReadOnly As Boolean, _DBAllowDesign As Boolean = False, _ShowDesigner As Boolean = False
		Private _DBAllowUpdate As Boolean = True, _DBAllowInsert As Boolean = True, _DBAllowDelete As Boolean = True
		Private _DBAllowUpdateDetails As Boolean = False, _DBAllowInsertDetails As Boolean = False
		Private _DBAllowSearch As Boolean = True, _DBAllowExport As Boolean = False, _DBAllowSelections As Boolean = False
		Private _DBAllowHelp As Boolean = False, _DBAllowPrint As Boolean = False ', _DBRecordAuditing As Boolean = False
		Private _DBAllowComments As EvolCommentsMode = EvolCommentsMode.None, noCommentsHere As Boolean = False

		Private SQLnbComments As String = "CommentCount"

		Private _SecurityModel As EvolSecurityModel = EvolSecurityModel.Single_User, _UseCache As Boolean = False

		Private Const mauid As String = "EVOLUserID", Tilda As String = "~", coma As String = ","
		Private Const SQL_and As String = " and "	   'lower case b/c used for description

		Private myDOM As New XmlDocument
		Private _XMLfile As String, _ItemKey As String
		Friend XMLloaded As Boolean = False

		Private HTML_Sep As String = " - "

#End Region

		'### Variables - HTML & Javascript ###
#Region "Variables HTML & Javascript"

		Private IEbrowser As Boolean = False
		Private _CollapsiblePanels As Boolean = True

		Private _ToolbarPosition As EvolToolbarPosition, _AllowSorting As Boolean = True
		Private _BackColorRowMouseOver As Color

		Private myRandomColor As String, mustUpgrade As String = ""
		Private imgProperties As String

		Private Const sStyleFields As String = " class=""Field"" "		  'style=""width:100%"" "   

		Private Const tableBeginTr As String = "<table class=""holder""><tr valign=""top"">"
		Private Const trTableEnd As String = "</tr></table>"
		Private Const TdTrTableEnd As String = "</td></tr></table>"

		Private Const pix_check As String = "check.gif", HTMLSpace As String = "&nbsp;", HTMLSpace3 As String = "&nbsp;&nbsp;&nbsp;"
		Private Const inNewBrowser As String = "_blank"
		Private Const SMALL_tag As String = "<small>", SMALL_tagClose As String = "</small>"

#End Region

#End Region

		'### Properties ######################################################################################
#Region "Properties"

#Region "Properties:Behavior"
		'------ Behavior ------------------
		<Bindable(True), Category("Behavior"), Description("Type of form displayed.")> Public Overridable ReadOnly Property DisplayMode() As EvolDisplayMode
			Get
				Select Case _DisplayMode
					Case 0
						Return EvolDisplayMode.View
					Case 1
						Return EvolDisplayMode.Edit
					Case 3
						Return EvolDisplayMode.Search
					Case 4
						Return EvolDisplayMode.AdvancedSearch
					Case Else
						Return EvolDisplayMode.List
				End Select
			End Get
		End Property
		<Bindable(True), Category("Behavior"), RefreshProperties(RefreshProperties.None), Description("Startup form mode.")> Public Overridable Property DisplayModeStart() As EvolDisplayMode
			Get
				Return _DisplayModeStart
			End Get
			Set(ByVal Value As EvolDisplayMode)
				_DisplayModeStart = Value
			End Set
		End Property
		<Bindable(True), Category("Behavior"), DefaultValue(True), RefreshProperties(RefreshProperties.Repaint), Description("Allow sorting of search results.")> Property AllowSorting() As Boolean
			Get
				Return _AllowSorting
			End Get
			Set(ByVal Value As Boolean)
				_AllowSorting = Value
			End Set
		End Property
		'<Bindable(True), Category("Behavior"), DefaultValue(False), RefreshProperties(RefreshProperties.Repaint), Description("Let users rate records.")> Property UserRating() As Boolean
		'    Get
		'        Return _DBAllowRating
		'    End Get
		'    Set(ByVal Value As Boolean)
		'        _DBAllowRating = Value
		'    End Set
		'End Property  
		<Bindable(True), Category("Behavior"), DefaultValue(True), RefreshProperties(RefreshProperties.Repaint), Description("Determine if users can hide and show panels.")> Property CollapsiblePanels() As Boolean
			Get
				Return _CollapsiblePanels
			End Get
			Set(ByVal Value As Boolean)
				_CollapsiblePanels = Value
			End Set
		End Property
#End Region

#Region "Properties:Data"
		'------ Data ------------------
		<Bindable(True), Category("Data"), DefaultValue(""), Description("XML definition file.")> Property XMLfile() As String
			Get
				Return _XMLfile
			End Get
			Set(ByVal Value As String)
				_XMLfile = Value
				If IsNumeric(_XMLfile) Then
					_FormID = CInt(_XMLfile)
				End If
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(""), Description("Connection string to the Database.")> Property SqlConnection() As String
			Get
				Return _SqlConnection
			End Get
			Set(ByVal Value As String)
				_SqlConnection = Value
				'sqlConnectionDico = _SqlConnection
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(False), RefreshProperties(RefreshProperties.Repaint), Description("Designer mode.")> Property DBAllowDesign() As Boolean
			Get
				Return _DBAllowDesign
			End Get
			Set(ByVal Value As Boolean)
				_DBAllowDesign = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(False), RefreshProperties(RefreshProperties.Repaint), Description("Read Only, disables New, Edit, Save...")> Property DBReadOnly() As Boolean
			Get
				Return _DBReadOnly
			End Get
			Set(ByVal Value As Boolean)
				_DBReadOnly = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(True), RefreshProperties(RefreshProperties.Repaint), Description("Enable record updates.")> Property DBAllowUpdate() As Boolean
			Get
				Return _DBAllowUpdate
			End Get
			Set(ByVal Value As Boolean)
				_DBAllowUpdate = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(True), RefreshProperties(RefreshProperties.Repaint), Description("Enable new records addition.")> Property DBAllowInsert() As Boolean
			Get
				Return _DBAllowInsert
			End Get
			Set(ByVal Value As Boolean)
				_DBAllowInsert = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), RefreshProperties(RefreshProperties.Repaint), Description("Enable record deletion.")> Property DBAllowDelete() As Boolean
			Get
				Return _DBAllowDelete
			End Get
			Set(ByVal Value As Boolean)
				_DBAllowDelete = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(True), RefreshProperties(RefreshProperties.Repaint), Description("Enable the search feature.")> Property DBAllowSearch() As Boolean
			Get
				Return _DBAllowSearch
			End Get
			Set(ByVal Value As Boolean)
				_DBAllowSearch = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(False), RefreshProperties(RefreshProperties.Repaint), Description("Enable export of the database.")> Property DBAllowExport() As Boolean
			Get
				Return _DBAllowExport
			End Get
			Set(ByVal Value As Boolean)
				_DBAllowExport = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(False), RefreshProperties(RefreshProperties.Repaint), Description("Enable the selections feature for predefined queries.")> Property DBAllowSelections() As Boolean
			Get
				Return _DBAllowSelections
			End Get
			Set(ByVal Value As Boolean)
				_DBAllowSelections = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(False), RefreshProperties(RefreshProperties.Repaint), Description("Provide help tooltips in edit mode.")> Property DBAllowHelp() As Boolean
			Get
				Return _DBAllowHelp
			End Get
			Set(ByVal Value As Boolean)
				_DBAllowHelp = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(True), RefreshProperties(RefreshProperties.Repaint), Description("Provide Print toolbar icon.")> Property DBAllowPrint() As Boolean
			Get
				Return _DBAllowPrint
			End Get
			Set(ByVal Value As Boolean)
				_DBAllowPrint = Value
			End Set
		End Property
		'<Bindable(True), Category("Data"), DefaultValue(False), Description("Provide optional help (if present in metadata).")> Property DBTrackHistory() As Boolean
		'    Get
		'        Return _DBTrackHistory
		'    End Get
		'    Set(ByVal Value As Boolean)
		'        _DBTrackHistory = Value
		'    End Set
		'End Property
		<Bindable(True), Category("Data"), Description("ID of the current record (ReadOnly).")> ReadOnly Property ItemID() As Integer
			Get
				Return _ItemID
			End Get
		End Property
		<Bindable(True), Category("Data"), Description("Number of records per page in lists.")> Property RowsPerPage() As Integer
			Get
				Return _RowsPerPage
			End Get
			Set(ByVal Value As Integer)
				_RowsPerPage = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(0), Description("ID of the current user.")> ReadOnly Property UserID() As Integer
			Get
				Return _UserID
			End Get
			'Set(ByVal Value As Integer)
			'    _UserID = Value
			'End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(0), Description("User access management: Single user, Single user with password identification, multiple users with row level security (each user only sees his own data), or multiple users sharing records (each user can view all data but only modify or delete his own).")> Property SecurityModel() As EvolSecurityModel
			Get
				Return _SecurityModel
			End Get
			Set(ByVal Value As EvolSecurityModel)
				_SecurityModel = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), Description("Application Key used to share login information among pages.")> Property SecurityKey() As String
			Get
				Return _DBApplicationKey
			End Get
			Set(ByVal Value As String)
				_DBApplicationKey = Value
			End Set
		End Property
		'<Bindable(True), Category("Data"), RefreshProperties(RefreshProperties.Repaint), Description("Log last updates date and author.")> Property DBRecordAuditing() As Boolean
		'    Get
		'        Return _DBRecordAuditing
		'    End Get
		'    Set(ByVal Value As Boolean)
		'        _DBRecordAuditing = Value
		'    End Set
		'End Property
		'<Bindable(True), Category("Data"), DefaultValue(0), Description("form ID (only useful if storing metadata in DB).")> Property DBApplicationID() As Integer
		'    Get
		'        '_FormID = CInt(ViewState("NoColorCode"))
		'        Return _FormID
		'    End Get
		'    Set(ByVal Value As Integer)
		'        _FormID = Value
		'        'ViewState("NoColorCode") = _FormID
		'    End Set
		'End Property
		'<Bindable(True), Category("Data"), DefaultValue(True), RefreshProperties(RefreshProperties.None), Description("Display log of SQL statements.")> Property DBDebug() As Boolean
		'    Get
		'        Return _Debug
		'    End Get
		'    Set(ByVal Value As Boolean)
		'        _Debug = Value
		'    End Set
		'End Property
		<Bindable(True), Category("Data"), RefreshProperties(RefreshProperties.Repaint), Description("Enable details record updates.")> Property DBAllowUpdateDetails() As Boolean
			Get
				Return _DBAllowUpdateDetails
			End Get
			Set(ByVal Value As Boolean)
				_DBAllowUpdateDetails = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), RefreshProperties(RefreshProperties.Repaint), Description("Enable new details records addition.")> Property DBAllowInsertDetails() As Boolean
			Get
				Return _DBAllowInsertDetails
			End Get
			Set(ByVal Value As Boolean)
				_DBAllowInsertDetails = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue(False), RefreshProperties(RefreshProperties.Repaint), Description("Let users post comments on specific records.")> Property UserComments() As EvolCommentsMode
			Get
				Return _DBAllowComments
			End Get
			Set(ByVal Value As EvolCommentsMode)
				_DBAllowComments = Value
			End Set
		End Property
		<Bindable(True), Category("Data"), DefaultValue("EN"), RefreshProperties(RefreshProperties.Repaint), Description("English (EN) or French (FR).")> Property Language() As String
			Get
				Return _Language
			End Get
			Set(ByVal Value As String)
				_Language = Value
			End Set

		End Property
		<Bindable(True), Category("Data"), DefaultValue(False), RefreshProperties(RefreshProperties.Repaint), Description("Enable caching of lists of values.")> Property UseCache() As Boolean
			Get
				Return _UseCache
			End Get
			Set(ByVal Value As Boolean)
				_UseCache = Value
			End Set
		End Property
#End Region

#Region "Properties:Appearance"
		'------ Appearance ------------------ 
		<Bindable(True), Category("Appearance"), Description("Introduction text only displayed the first time the form is called."), RefreshProperties(RefreshProperties.Repaint)> Property Text() As String
			Get
				Return _Text
			End Get
			Set(ByVal Value As String)
				_Text = Value
			End Set
		End Property
		<Bindable(True), Category("Appearance"), DefaultValue("#FFEABB"), Description("BackColor of table rows when the mouse is over it."), RefreshProperties(RefreshProperties.None)> Property BackColorRowMouseOver() As Color
			Get
				Return _BackColorRowMouseOver
			End Get
			Set(ByVal Value As Color)
				_BackColorRowMouseOver = Value
			End Set
		End Property
		<Bindable(True), Category("Appearance"), DefaultValue(EvolToolbarPosition.Top), Description("Position of the toolbar."), RefreshProperties(RefreshProperties.Repaint)> Property ToolbarPosition() As EvolToolbarPosition
			Get
				_ToolbarPosition = CType(ViewState("ToolbarPosition"), EvolToolbarPosition)
				Return _ToolbarPosition
			End Get
			Set(ByVal Value As EvolToolbarPosition)
				_ToolbarPosition = Value
				ViewState("ToolbarPosition") = _ToolbarPosition
			End Set
		End Property
		<Bindable(True), Category("Appearance"), DefaultValue(True), Description("Make title and mode name visible."), RefreshProperties(RefreshProperties.Repaint)> Property ShowTitle() As Boolean
			Get
				Return _ShowTitle
			End Get
			Set(ByVal Value As Boolean)
				_ShowTitle = Value
			End Set
		End Property
		<Bindable(True), Category("Appearance"), DefaultValue(""), Description("Virtual path to the toolbar pictures."), RefreshProperties(RefreshProperties.Repaint)> Property VirtualPathToolbar() As String
			Get
				Return _PathPixToolbar
			End Get
			Set(ByVal Value As String)
				If Value <> String.Empty AndAlso Not Value.EndsWith("/") Then
					_PathPixToolbar = Value & "/"
				Else
					_PathPixToolbar = Value
				End If
			End Set
		End Property
		<Bindable(True), Category("Appearance"), DefaultValue(""), Description("Virtual path to the records pictures.")> Property VirtualPathPictures() As String
			Get
				Return _PathPix
			End Get
			Set(ByVal Value As String)
				_PathPix = Value 'FormatedPath(Value)
			End Set
		End Property
		<Bindable(True), Category("Appearance"), DefaultValue(""), Description("Virtual path to the app designer.")> Property VirtualPathDesigner() As String
			Get
				Return _PathDesign
			End Get
			Set(ByVal Value As String)
				_PathDesign = Value	'FormatedPath(Value)
			End Set
		End Property
		<Bindable(True), Category("Appearance"), DefaultValue(""), Description("Show links for First, Previous, Next, and Last records (in Edit and View modes).")> Property NavigationLinks() As Boolean
			Get
				Return _NavLinks
			End Get
			Set(ByVal Value As Boolean)
				_NavLinks = Value
			End Set
		End Property
#End Region

#Region "Properties:Designer"
		'------ Designer ------------------
		<Bindable(True), Category("Designer"), DefaultValue(EvolDisplayMode.View), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), RefreshProperties(RefreshProperties.Repaint)> Property DesignDisplayMode() As EvolDisplayMode
			Get
				Return _DesignDisplayMode
			End Get
			Set(ByVal Value As EvolDisplayMode)
				_DesignDisplayMode = Value
			End Set
		End Property
		<Bindable(True), Category("Designer"), DefaultValue(0), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), RefreshProperties(RefreshProperties.Repaint)> Property DesignTabIndex() As Integer
			Get
				Return _DesignTabIndex
			End Get
			Set(ByVal Value As Integer)
				_DesignTabIndex = Value
			End Set
		End Property
		<Bindable(True), Category("Designer"), DefaultValue(0), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), RefreshProperties(RefreshProperties.Repaint)> Property DesignWebPath() As String
			Get
				Return _DesignWebPath
			End Get
			Set(ByVal Value As String)
				_DesignWebPath = Value
			End Set
		End Property
#End Region

#End Region

		'### Rendering #######################################################################################
#Region "Rendering"

		Protected Overrides Sub Render(ByVal output As System.Web.UI.HtmlTextWriter)

			'Dim startTime As DateTime = DateTime.Now 
			'Dim span As TimeSpan

			If _DisplayMode = 71 Or _DisplayMode = 72 Then
				RenderExport()
			Else
				With output
					.Write("<div class=""evo""><!-- Evolutility 2.3 - www.evolutility.org - (c) 2003-08 Olivier Giulieri -->")
					.Write(HTMLmenu())
					.Write(HTMLInputHidden("EVOL_ItemID", CStr(_ItemID)))
					If String.IsNullOrEmpty(ErrorMsg) Then
						If String.IsNullOrEmpty(HeaderMsg) Then
							If Not Page.IsPostBack AndAlso Not String.IsNullOrEmpty(_Text) Then
								.Write(FormMessage(_Text))
							End If
						Else
							.Write(FormMessage(HeaderMsg, "info"))
							HeaderMsg = String.Empty
						End If
					Else
						.Write(FormMessage(ErrorMsg, "error"))
						ErrorMsg = String.Empty
					End If
					If XMLloaded Then
						If _DisplayMode > 0 AndAlso _DisplayMode < 5 Then
							.Write(String.Format("<script src=""{0}DatePicker_{1}.js"" defer=""defer"" type=""text/javascript""></script>", _PathPixToolbar, _Language))
						End If
						'.Write(JSFields(True)) 
						.Write("<span id=""EVOL_Content"">")
						Select Case _DisplayMode
							Case 0, 1, 2  'view, edit, new 
								.Write(FormEdit(_DisplayMode))
							Case 3, 4 'search, advanced search 
								.Write(FormSearch(_DisplayMode))
							Case 102, 103, 104, 105, 106, 110  '102=query, 103=search result, 104=adv search result, 105=last query, 106=quick search, 110=all
								.Write(FormList(_DisplayMode))
							Case 50	'login
								.Write(FormLogin())
							Case 70	'export 
								.Write(FormExport())
							Case 60	'selections
								.Write(FormQueries())
								'Case 500 'Templates
								'    output.Write(FormTemplates()) 
								'Case -1
								'    output.Write(FormMessage(errormessage))
							Case Else
								.Write("<p>Invalid parameters.</p>")
						End Select
						.Write("</span>")
						.Write(HTMLInputHidden(dMode, CStr(_DisplayMode)))
					End If
					If ErrorMsg <> String.Empty AndAlso Not ErrorMsg.Equals("-") Then output.Write(FormMessage(ErrorMsg, "error"))
					'If _Debug andalso DebugMsg <> String.Empty Then output.Write(FormMessage(DebugMsg, _PathPixToolbar & "dbr.gif"))  
					If _ToolbarPosition = EvolToolbarPosition.Top_And_Bottom Then .Write(HTMLmenu(True))
					If _DBAllowDesign Then
						.Write("<div id='dhtmlwindowholder'><span style='display:none'>.</span></div>")
					End If
					.Write(RenderFooter())
				End With
			End If
			'If dbuserlog <> String.Empty Then
			'    dbuserlogMSG = ModeName() & " " & entity & vbCrLf & dbuserlogMSG
			'    UserEventLog(dbuserlogMSG)
			'End If
			'span = DateTime.Now.Subtract(startTime)
			'output.Write("Page rendered in " & span.Seconds & "." & Format(span.Milliseconds, "000 seconds"))
		End Sub

		Friend Function RenderFooter() As String
			Dim myHTML As New StringBuilder
			'This signature is"invisible" to users and must not be removed from the source code nor the compiled version of Evolutility
			myHTML.Append("<div style=""display:none;"">Powered by <b>").Append(HTMLLink(evoLink, "Evolutility", "E")).Append("</b></div>")
			myHTML.Append("</div>") 
			Return myHTML.ToString
		End Function

		Private Sub RenderExport()
			'Export
			Dim buffer As String, Filename As String
			Dim Encoding As New UTF8Encoding
			Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response

			response.Clear()
			buffer = Page.Request("evoZOut")
			If String.IsNullOrEmpty(buffer) Then buffer = "CSV"
			Select Case entities
				Case LG_entities, ""
					Filename = def_Data.dbtable
				Case Else
					Filename = entities.Replace(" ", "_")
			End Select
			response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}.{1}", Filename, buffer))
			If buffer.Equals("CSV") Then
				response.ContentType = "application/ms-excel"
			Else
				response.ContentType = "application/octet-stream"
			End If
			response.BinaryWrite(Encoding.GetBytes(GenerateExport(buffer)))
			'buffer = GenerateExport(buffer)
			'response.AddHeader("Content-Length", Encoding.GetByteCount(buffer).ToString())
			'response.BinaryWrite(Encoding.GetBytes(buffer))
			response.Charset = String.Empty
			If _DisplayMode = 71 Then
				_DisplayMode = 0
			ElseIf _DisplayMode = 72 Then
				_DisplayMode = 105
			End If
			response.End()
		End Sub

#End Region

		'### Life ############################################################################################
#Region "Life"

		Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
			Dim sql As New StringBuilder, sql2 As New StringBuilder, modeID As Integer
			Dim aSQL As String = "", dbcolumn As String, NbFileUploads As Int32 = 0, DBCommand As Integer = 0
			Dim fieldReadOnly As Integer, fieldRequired As Boolean
			Dim buffer As String, i As Integer, maxLoop As Integer, PrevItemID As Integer, MustTriggerInsert As Boolean = False, fieldType As String, fieldMaxLength As Integer
			Dim aNodeList As XmlNodeList, aNode As XmlNode

			MyBase.OnPreRender(e)
			UID = "EVOLU_"	 'UniqueID.Replace(":", "_")
			LoadLanguage(_Language)
			If String.IsNullOrEmpty(_SqlConnection) Then
				_SqlConnection = GetAppSetting("SQLConnection")
			End If
			If dicoLoadXML() Then 'dicoLoadObjDef() Then '
				If atRunTime Then
					'////// get parameters //////////////////////////////
					If _UserID < 1 Then
						If _SecurityModel <> EvolSecurityModel.Single_User OrElse _DBAllowComments.Equals(EvolCommentsMode.Logged_Users) Then
							_UserID = GetUserID()
						End If
					End If
					If Page.IsPostBack Then
						If Not String.IsNullOrEmpty(def_Data.dblockingcolumn) Then
							dbwherelock = CStr(Page.Cache("locking"))
						End If
						If ToDo <> 12 AndAlso _ItemID < 1 Then
							_ItemID = CInt(GetPageRequest("EVOL_ItemID", "0"))
						End If
						If Not PostBackEventUsed Then
							If ToDo <> 1 AndAlso _DisplayMode <> 105 AndAlso _DisplayMode <> 102 Then
								buffer = Page.Request(dMode)
								If IsNumeric(buffer) Then _DisplayMode = CInt(buffer)
								If _DisplayMode = 50 Then
									_DisplayMode += 1
								Else
									If _DisplayMode > 0 AndAlso _DisplayMode < 5 AndAlso Not _ShowDesigner Then _DisplayMode += 100
								End If
							End If
						End If
					Else
						_UseCache = False
						modeID = ModeRequestInt(Page.Request("MODE"))
						If modeID < 50 Then
							If modeID = 12 Then	'new
								ToDo = 25
								modeID = 1
								_ItemID = 0
							Else
								i = String2Int(Page.Request("ID"))
							End If
						Else
							i = 0
						End If
						If i > 0 Then
							_ItemID = i
							If modeID < 2 Then
								If _DisplayModeStart = EvolDisplayMode.Edit Then
									_DisplayMode = 1
								Else
									_DisplayMode = modeID
								End If
							Else
								_DisplayMode = 0
							End If
						Else
							If modeID = 0 Then
								queryUrlParam = Page.Request("QUERY")
								If String.IsNullOrEmpty(queryUrlParam) Then
									If _DisplayModeStart > -1 Then
										_DisplayMode = _DisplayModeStart
										If _DisplayMode = EvolDisplayMode.NewItem Then
											_ItemID = 0
											_DisplayMode = 1
											ToDo = 25
										End If
									Else
										_DisplayMode = 110
									End If
								Else
									_DisplayMode = 102
								End If
							Else
								_DisplayMode = modeID
							End If
						End If
						If ToDo <> 25 AndAlso Not _DBReadOnly Then
							If _UserID > 0 OrElse _SecurityModel.Equals(EvolSecurityModel.Single_User) Then
								If (_DBAllowUpdate AndAlso _ItemID > 0) OrElse (_DBAllowInsert AndAlso _ItemID < 1) Then
									buffer = Page.Request("tdLOVE")
									Select Case buffer
										Case "1"
											_DisplayMode = 1
										Case "1N"
											_DisplayMode = 1
											_ItemID = 0
											ToDo = 25
									End Select
								End If
							End If
						End If
					End If
					If _DisplayMode = 51 Then
						buffer = CheckLogin()
						If buffer <> String.Empty Then
							_DisplayMode = _DisplayModeStart ' 110 'list
							If _DisplayMode = 0 OrElse _DisplayMode = 1 Then nav = 1
						Else
							_DisplayMode = 50 'login
						End If
					End If
					If _UserID > 0 OrElse _SecurityModel.Equals(EvolSecurityModel.Single_User) Then
						'////// processing //////////////////////////////
						'-------- user comments --------
						If _DisplayMode = 0 AndAlso _DBAllowComments = EvolCommentsMode.Logged_Users Then
							PostUserComments()
						End If
						aSQL = String.Empty
						'If _DBReadOnly andalso Not (_DBAllowInsert orelse _DBAllowUpdate) Then  'check for readonly...
						'    If _DisplayMode = 1 Then _DisplayMode = 0
						'Else
						If _DBReadOnly Then	   'check for readonly...
							If _DisplayMode = 1 Then _DisplayMode = 0
						ElseIf _DBAllowInsert OrElse _DBAllowUpdate Then
							'-------- master table --------
							If ToDo > -1 Then
								NbFileUploads = 0
								If _DisplayMode = 101 Then	 'verification + save
									aNodeList = myDOM.DocumentElement.SelectNodes(XPathQuery.panelField, nsManager)
									ValidationMsg = String.Empty
									If _ItemID = 0 Then	'INSERT
										DBCommand = 1
										If _DBAllowInsert Then
											'If spInsert = "" Then
											maxLoop = aNodeList.Count - 1
											For i = 0 To maxLoop
												With aNodeList.Item(i)
													If .Attributes(EvoXMLInfo.dbReadOnly) Is Nothing Then
														fieldReadOnly = 0
													Else
														fieldReadOnly = CInt(Val(.Attributes(EvoXMLInfo.dbReadOnly).Value))
													End If
													If fieldReadOnly <> 1 Then
														dbcolumn = UID & .Attributes(EvoXMLInfo.dbColumn).Value
														buffer = Page.Request(dbcolumn)
														fieldType = .Attributes(EvoXMLInfo.type).Value
														Select Case fieldType
															Case t_date

															Case t_pix
																buffer = UploadDoc(NbFileUploads, UID & dbcolumn)
																NbFileUploads += 1
															Case t_doc
																buffer = UploadDoc(NbFileUploads, UID & dbcolumn, False)
																NbFileUploads += 1
															Case Else
																If .Attributes(EvoXMLInfo.MaxLength) Is Nothing Then
																	fieldMaxLength = 0
																Else
																	fieldMaxLength = CInt(Val(.Attributes(EvoXMLInfo.MaxLength).Value))
																End If
														End Select
														If String.IsNullOrEmpty(buffer) Then
															If Not fieldType = t_bool Then
																If Not .Attributes(EvoXMLInfo.required) Is Nothing Then
																	If .Attributes(EvoXMLInfo.required).Value = "1" Then ValidationMsg += .Attributes(EvoXMLInfo.label).Value & " required."
																End If
															End If
														Else
															If buffer <> Tilda AndAlso buffer <> String.Empty Then
																sql2.Append("[").Append(.Attributes(EvoXMLInfo.dbColumn).Value).Append("],")
																sql.Append(dbFormat(buffer, fieldType, fieldMaxLength, _Language)).Append(coma)
															End If
														End If
													End If
												End With
											Next
											If String.IsNullOrEmpty(ValidationMsg) Then
												If sql.Length > 0 Then
													If _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_RLS) OrElse _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_Sharing) Then
														sql2.Append(def_Data.dbcolumnuserid).Append(coma)
														sql.Append(_UserID).Append(coma)
													End If
													buffer = sql.ToString
													aSQL = sqlINSERT(def_Data.dbtable, sql2.ToString.Substring(0, sql2.Length - 1), sql.ToString.Substring(0, sql.Length - 1))
												End If
												If Not String.IsNullOrEmpty(aSQL) Then
													Dim cacheName As String = String.Format("LastInsert{0}", Me.UniqueID)
													If aSQL <> CStr(Page.Cache(cacheName)) Then
														buffer = RunSQL(aSQL, _SqlConnection, True)
														If String.IsNullOrEmpty(buffer) Then
															HeaderMsg = CondiConcat(HeaderMsg, String.Format(LG_NewSave, entity, TextNow()), vbCrLf)
															Page.Cache(cacheName) = aSQL
															MustTriggerInsert = True
														Else
															AddError(buffer)
															Page.Cache.Remove(cacheName)
														End If
													End If
												End If
											Else
												ErrorMsg = ValidationMsg
											End If
											If _DBAllowUpdate Then
												_DisplayMode = 1
											Else
												_DisplayMode = 0
											End If
											nav = 4
										Else
											AddError(LG_JSNoWay & String.Format(LG_InsertEntity, entity))
										End If
									Else 'UPDATE
										DBCommand = 2
										If _DBAllowUpdate Then
											'If spUpdate = "" Then
											maxLoop = aNodeList.Count - 1
											For i = 0 To maxLoop
												With aNodeList.Item(i)
													If .Attributes(EvoXMLInfo.dbReadOnly) Is Nothing Then
														fieldReadOnly = 0
													Else
														fieldReadOnly = CInt(Val(.Attributes(EvoXMLInfo.dbReadOnly).Value))
													End If
													If fieldReadOnly = 0 Then 'readonly=1, insert no edit =2
														dbcolumn = .Attributes(EvoXMLInfo.dbColumn).Value
														fieldType = .Attributes(EvoXMLInfo.type).Value
														If fieldType = t_pix OrElse fieldType = t_doc Then
															buffer = UploadDoc(NbFileUploads, UID & dbcolumn, fieldType = t_pix)
															NbFileUploads += 1
														Else
															buffer = Page.Request(UID & dbcolumn)
														End If
														If String.IsNullOrEmpty(buffer) Then
															If .Attributes(EvoXMLInfo.required) Is Nothing Then
																fieldRequired = False
															Else
																fieldRequired = .Attributes(EvoXMLInfo.required).Value = "1"
															End If
															If fieldRequired Then
																ValidationMsg += String.Format(LG_MHValidValue, .Attributes(EvoXMLInfo.label).Value)
															Else
																If fieldType <> t_lov AndAlso buffer <> CStr(Page.Server.UrlDecode(Page.Request(UID & dbcolumn & "_ov"))) Then
																	sql.Append("[").Append(dbcolumn).Append("]=").Append(dbFormat(buffer, fieldType, 0, _Language)).Append(coma)
																End If
															End If
														Else
															If Not buffer.Equals(Tilda) AndAlso buffer <> CStr(Page.Server.UrlDecode(Page.Request(UID & dbcolumn & "_ov"))) Then
																sql.Append("[").Append(dbcolumn).Append("]=").Append(dbFormat(buffer, fieldType, 0, _Language)).Append(coma)
															End If
														End If
													End If
												End With
											Next
											If String.IsNullOrEmpty(ValidationMsg) Then
												If sql.Length > 0 Then
													aSQL = String.Format("UPDATE {0} SET {1}", def_Data.dbtable, sql.ToString.Substring(0, sql.Length - 1))
													'If _DBRecordAuditing Then aSQL += ",lastupdate=getdate(),lastupdateby=" & _UserID
													aSQL += String.Format(" WHERE {0}={1}", def_Data.dbcolumnpk, _ItemID)
													If _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_RLS) OrElse _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_Sharing) Then
														aSQL += String.Format(" AND {0}={1} ", def_Data.dbcolumnuserid, _UserID)
													End If
												End If
												If aSQL.Length > 1 Then
													buffer = RunSQL(aSQL, _SqlConnection, True)
													AddError(buffer)
													If String.IsNullOrEmpty(ErrorMsg) Then
														HeaderMsg = CondiConcat(HeaderMsg, String.Format(LG_Updated, TextUcaseFirst(entity), TextNow()), vbCrLf)
														OnDBChange(New DatabaseEventArgs(DBAction.UPDATE, _ItemID))
													Else
														ErrorMsg = String.Format(LG_CannotUpdate, entity, Str(_ItemID))
													End If
												Else
													HeaderMsg = CondiConcat(HeaderMsg, LG_NoUpdate, vbCrLf)
												End If
											End If
										Else
											AddError(LG_JSNoWay & String.Format(LG_ModifyEntity, entity))
										End If
										nav = -1
										_DisplayMode = 1
									End If
								ElseIf _DisplayMode = 10 Then	'DELETE
									DBCommand = 3
									If BuildSQLDeleteItem() < 0 Then
										_DisplayMode = 1
									End If
								End If
							End If
							'-------- details table --------
							If _DisplayMode = 1 AndAlso _ItemID > 0 AndAlso (_DBAllowUpdateDetails OrElse _DBAllowInsertDetails) AndAlso Page.Request("evolNUD") = "1" Then
								aSQL = BuildSQLDetailsUpdate()
								If aSQL <> String.Empty Then
									aSQL = RunSQL(aSQL, _SqlConnection, False)
									If String.IsNullOrEmpty(aSQL) Then
										HeaderMsg += BR_tag & LG_DetailsUpdate
									Else
										AddError(aSQL)
									End If
								End If
							End If
						End If
						'////// get next record to display //////////////////////////////
						'If ErrorMsg = "" Then
						If Not (def_Data Is Nothing OrElse String.IsNullOrEmpty(def_Data.dblockingcolumn)) Then dbwherelock = CStr(Page.Cache("locking"))
						If ToDo = 25 Then
							_ItemID = 0
							_DisplayMode = 1
						Else
							If _DisplayMode = 0 OrElse _DisplayMode = 1 Then
								If nav = 0 AndAlso _ItemID = 0 Then
									If Page.IsPostBack Then
										If _DisplayMode = 0 Then
											nav = 4
										Else
											ds = Nothing
										End If
									Else
										nav = 1
									End If
								End If
								If _ItemID <> 0 OrElse nav > 0 Then
									PrevItemID = _ItemID
									ds = GetDataParameters(BuildSQLnav(nav), _SqlConnection, New SqlParameter(p_itemid, _ItemID))
									Try
										If ds.Tables(0).Rows.Count = 0 Then
											If _ItemID > 0 Then
												ds = GetDataParameters(BuildSQLnav(nav, False), _SqlConnection, New SqlParameter(p_itemid, _ItemID))
											Else
												_ItemID = 0
											End If
										Else
											i = CInt(ds.Tables(0).Rows(0)(dbPrimaryKey))
											If i = _ItemID Then
												If nav = 2 Then
													navd = 1
												ElseIf nav = 3 Then
													navd = nav
												End If
											Else
												_ItemID = i
											End If
										End If
										'If DBCommand = 1 Then 'INSERT
										'End If
									Catch ex As Exception
										_ItemID = 0
										If String.IsNullOrEmpty(ErrorMsg) Then
											ErrorMsg = LG_NoQuery
											If _Debug Then
												AddError(ex.ToString)
											End If
										End If
									End Try
								End If
								If String.IsNullOrEmpty(ErrorMsg) AndAlso MustTriggerInsert Then
									OnDBChange(New DatabaseEventArgs(DBAction.INSERT, _ItemID))
								End If
							End If
						End If
						'End If
						aNode = Nothing
						aNodeList = Nothing
					Else
						If _DisplayMode = 51 Then
							If CheckLogin() <> String.Empty Then
								_DisplayMode = _DisplayModeStart
							Else
								_DisplayMode = 50
							End If
						End If
					End If
				End If
				JSRegisterScripts()
				If _SecurityModel <> EvolSecurityModel.Single_User AndAlso _DisplayMode <> 51 AndAlso _UserID < 1 Then
					If String.IsNullOrEmpty(ErrorMsg) AndAlso String.IsNullOrEmpty(HeaderMsg) Then HeaderMsg = LG_PleaseLogin
					_DisplayMode = 50
				Else
					If _DisplayMode = 1 AndAlso atRunTime Then
						sql = New StringBuilder
						With sql
							.Append(JSscriptBegin)
							.Append(JSValidation())
							.Append(JSscriptEnd)
							Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "EVOL_Validation", .ToString)
						End With
					End If
				End If
				If Not (def_Data Is Nothing OrElse String.IsNullOrEmpty(def_Data.js_script)) Then
					Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "EVOL_Custom", String.Format("<script src=""{0}{1}"" defer=""defer"" type=""text/javascript""></script>", _PathPixToolbar, def_Data.js_script))
				End If
			End If
		End Sub

		Public Sub RaisePostBackEvent(ByVal eventArgument As String) Implements IPostBackEventHandler.RaisePostBackEvent
			Dim aDisplayMode As Integer, buffer As String

			If IsNumeric(eventArgument) Then
				aDisplayMode = CInt(Val(eventArgument))
				Select Case aDisplayMode
					Case 0, 1, 10 '0=view, 1=edit, 10=delete, 
						_DisplayMode = aDisplayMode
					Case 3, 4, 102, 105, 110	'1=edit, 3=search, 4=advsearch, ,105=last search result, 110=listall
						_DisplayMode = aDisplayMode
						ToDo = -1
					Case 201 To 204	 'nav view
						nav = aDisplayMode - 200
						_DisplayMode = 0
					Case 301 To 304	 'nav edit
						nav = aDisplayMode - 300
						_DisplayMode = 1
					Case 60	  'queries
						_DisplayMode = aDisplayMode
					Case 24	'save
						_DisplayMode = 101
					Case 25	 'save and add another
						_DisplayMode = 101
						ToDo = aDisplayMode
					Case 12	'12=new 
						ToDo = aDisplayMode
						_DisplayMode = 1
						_ItemID = 0
					Case 49	'logout
						_ItemID = GetUserID()
						SetUserID(-1)
						OnCredentialChange(New CredentialEventArgs(CredentialAction.Logout, _ItemID, "", _DBApplicationKey, ""))
						_ItemID = 0
						_DisplayMode = 50
					Case 70, 71, 72	'export
						_DisplayMode = aDisplayMode
						'Case 200 'customize 
						'	_DisplayMode = ModeRequestInt(Page.Request("MODE"))
						'	_ShowDesigner = _DBAllowDesign
					Case Is < 0
						ToDo = aDisplayMode
						_ItemID = -aDisplayMode
						_DisplayMode = 0
					Case Else
						_DisplayMode = 0
				End Select
				PostBackEventUsed = True
			Else
				buffer = eventArgument.Substring(0, 1)
				Select Case buffer
					Case "-" 'lookup w/ pkey string
						ToDo = -99
						_ItemKey = Right(eventArgument, eventArgument.Length - 1)
						_ItemID = 999 'need>0 b/c rec
						_DisplayMode = 0
						aDisplayMode = -1
						PostBackEventUsed = True
					Case "e" 'edit
						_DisplayMode = 1
						If eventArgument.Length > 1 Then
							_ItemID = CInt(Val(Right(eventArgument, eventArgument.Length - 1)))
							ToDo = 1
						End If
					Case "c"
						_ShowDesigner = _DBAllowDesign
						If eventArgument.Length > 2 Then
							_DisplayMode = CInt(Val(Mid(eventArgument, 3)))
							'1=edit, 3=search, 4=advsearch, 110=listall 
							ToDo = -1
							Return
						End If
					Case "q" 'query (selection)
						_DisplayMode = 102
						queryUrlParam = Mid(eventArgument, 3)
					Case "a", "d" 'sort ASC or DESC
						_DisplayMode = 105
						If eventArgument.Length > 2 Then newOrderBy = Mid(eventArgument, 3)
						If buffer.Equals("a") Then newOrderBy += " ASC " Else newOrderBy += " DESC"
						PostBackEventUsed = True
						ToDo = -1
					Case "n" 'next page in list
						_DisplayMode = 105
						newOrderBy = String.Empty
						pageID = CInt(Val(Right(eventArgument, eventArgument.Length - 1)))
						'todo=-1 ?
					Case Else
						If _SecurityModel <> EvolSecurityModel.Single_User AndAlso _UserID < 1 Then
							_UserID = GetUserID()
							If _UserID < 1 Then
								buffer = Page.Request(evoName & "login")
								If String.IsNullOrEmpty(buffer) Then
									_DisplayMode = 50 'login
								Else
									If Not _ShowDesigner Then
										_DisplayMode = 0
									End If
								End If
							End If
						End If
				End Select
			End If
		End Sub

		Protected Overrides Sub Finalize()
			myDOM = Nothing
			nsManager = Nothing
			ds = Nothing
			ds2 = Nothing
			def_Data = Nothing
			MyBase.Finalize()
		End Sub

		Public Overrides Sub Dispose()
			myDOM = Nothing
			nsManager = Nothing
			ds = Nothing
			ds2 = Nothing
			def_Data = Nothing
			MyBase.Dispose()
		End Sub

#End Region

		'### Forms ###########################################################################################
#Region "Forms"

		Friend Function FormExport() As String
			Dim i As Integer, expOut As String, maxLoopXML As Integer, buffer As String, fieldlabel As String
			Dim SelectTagBegin As String = "<select class=""Fieldnw"" name="""
			Dim myHTML As New StringBuilder, myLabels() As String
			Dim aNodeList As XmlNodeList
			Dim fieldName As String

			expOut = "CSV"
			aNodeList = myDOM.DocumentElement.SelectNodes(XPathQuery.panelField, nsManager)
			With myHTML
				.Append("<table class=""holder"" cellpadding=""5""><tr valign=""top""><td width=""62%""><p>")
				'##### export format ######################################## 
				.Append(HTMLFieldLabel("", LG_Selection))
				.Append("<div class=""FieldReadOnly"">")
				buffer = String.Format("sql{0}{1}{2}", _UserID, def_Data.dbtable, _FormID)
				buffer = CStr(Page.Cache(buffer & "_W2"))
				If String.IsNullOrEmpty(buffer) OrElse buffer.Equals(allEntities) Then
					.Append(allEntities)
				Else
					fieldName = "evoxQSE"
					.Append(HTMLInputRadio(fieldName, "1", allEntities, False, "evol_q1"))
					.Append(HTMLInputRadio(fieldName, "0", buffer, True, "evol_q0"))
				End If
				.Append("</div><p>")
				.Append(HTMLFieldLabel("evoZOut", LG_ExportFormat))
				.Append("<select class=""FieldReadOnly"" name=""evoZOut"" onChange=""Evol.exportFields('").Append(UID).Append("',this.value)"">")
				myLabels = Split(LG_ExportFormats, "-")
				.Append(HTMLOption("CSV", myLabels(0), True))
				If atRunTime Then
					.Append(HTMLOption("HTML", myLabels(1)))
					.Append(HTMLOption("SQL", myLabels(2)))
					.Append(HTMLOption("TAB", myLabels(3)))
					.Append(HTMLOption("XML", myLabels(4)))
				End If
				.Append("</select><blockquote>")
				'##### CSV, TAB - First line for field names #######
				.Append("<div id=""").Append(UID).Append("CSV""").Append(StyleVisibleToggle(expOut = "CSV" OrElse expOut = "TAB")).Append("><p>")
				'# field - header #######
				buffer = UID & "FLH"
				.Append(HTMLFieldLabel(buffer, LG_ExportHeader))
				.Append(HTMLInputCheckBox(buffer, "1", LG_ExportFirstLine, "", , True, buffer))
				'# field - separator
				'# - csv - any separator #######
				.Append("</p><div id=""").Append(UID).Append("csv2""").Append(StyleVisibleToggle(expOut = "CSV")).Append(">")
				fieldName = "FLS_evol"
				.Append(HTMLFieldLabel(fieldName, LG_ExportSeparator))
				.Append(HTMLInputText(fieldName, GetPageRequest(fieldName, coma), sStyleFields, 5))
				.Append("</div><div id=""").Append(UID).Append("tab2""").Append(StyleVisibleToggle(expOut = "TAB")).Append("><p>")
				'# - tab - hardcoded tab #######
				.Append(HTMLFieldLabel("", LG_ExportSeparator)).Append("TAB</p></div>")
				If atRunTime Then
					'# XML - Root element name #######
					.Append("</p></div><div id=""").Append(UID).Append("XML""").Append(StyleVisibleToggle(expOut = "XML")).Append("><p>")
					.Append(HTMLFieldLabel("evoRoot", LG_XMLroot))
					.Append(HTMLInputText("evoRoot", entities, sStyleFields, 30))
					buffer = "evoxpC2X"
					.Append("<p>").Append(HTMLFieldLabel(buffer, LG_ColMap))
					.Append(HTMLInputRadio(buffer, "2", LG_XMLAttr, True, "EVOLxr")).Append(BR_tag)
					.Append(HTMLInputRadio(buffer, "1", LG_XMLElem, False, "LOVExr"))
					'# HTML - Header color + Color odd rows + Color even rows #######
					.Append("</p></div><div id=""").Append(UID).Append("HTML""").Append(StyleVisibleToggle(expOut = "HTML")).Append("><p>")
					.Append("<table border=""0"">")
					myLabels = Split(LG_ExportColors, "-")
					.Append(HTMLInputColor("evoColRCT", myLabels(0), myLabels(1)))
					.Append(HTMLInputColor("evoColRCO", myLabels(2), myLabels(3)))
					.Append(HTMLInputColor("evoColRCE", myLabels(4), myLabels(5)))
					.Append("</table>")
					'# SQL - transaction #######
					.Append("</p></div><div id=""").Append(UID).Append("SQL""").Append(StyleVisibleToggle(expOut = "SQL")).Append("><p>")
					myLabels = Split(LG_ExportSQL, "-")
					.Append(HTMLFieldLabel(buffer, myLabels(0)))
					.Append(HTMLInputCheckBox("evoxpTRS", "1", myLabels(1), String.Empty, , , "evoxpTRS1")).Append(BR_tag)
					.Append(HTMLInputCheckBox("evoxpTRS2", "1", myLabels(2), String.Empty, , , "evoxpTRS2"))
					.Append("</p></div></blockquote>")
				End If
				'.Append("<p>&nbsp;&nbsp;<b>").Append(HTMLLinkEventRef("71", String.Format(LG_DownloadEntity, entities))).Append("</b><br/>&nbsp;</p>")
				.Append("<p>&nbsp;&nbsp;").Append(HTMLInputButton("XP", String.Format(LG_DownloadEntity, entities), False, "Javascript:EvPost('71')")).Append("<br/>&nbsp;</p>")
				.Append("</td><td width=""38%"">")
			End With

			'### list of columns to export #########################################  
			myHTML.Append(HTMLFieldLabel(String.Empty, LG_ExportFields))
			maxLoopXML = aNodeList.Count - 1
			For i = 0 To maxLoopXML
				With aNodeList(i)
					fieldlabel = .Attributes(EvoXMLInfo.label).Value
					If String.IsNullOrEmpty(fieldlabel) Then
						If Not .Attributes(EvoXMLInfo.labelEdit) Is Nothing Then
							fieldlabel = .Attributes(EvoXMLInfo.labelEdit).Value
						End If
						If String.IsNullOrEmpty(fieldlabel) AndAlso Not .Attributes(EvoXMLInfo.labelList) Is Nothing Then
							fieldlabel = .Attributes(EvoXMLInfo.labelList).Value
						End If
					End If
					buffer = .Attributes(EvoXMLInfo.dbColumn).Value
					If String.IsNullOrEmpty(fieldlabel) Then fieldlabel = buffer
					myHTML.Append(HTMLInputCheckBox(UID & buffer, "1", fieldlabel, " ", , True, buffer & CStr(i)))
				End With
			Next
			myHTML.Append(HTMLInputCheckBox("showID", "1", LG_IDkey, " ", , , "showID"))
			myHTML.Append("<br/>&nbsp;")
			myHTML.Append(TdTrTableEnd)
			myLabels = Nothing
			Return myHTML.ToString
		End Function

		Friend Function FormQueries() As String
			Dim myHTML As New StringBuilder
			Dim aNode As XmlNode
			Dim aNodeList As XmlNodeList, header As String

			myHTML.Append("<div class=""holder""><br/>")
			If Not _DBAllowSelections Then
				myHTML.Append("<p>Selections are not allowed for this object.</p>")
			Else
				aNodeList = myDOM.DocumentElement.SelectNodes(XPathQuery.queries, nsManager)
				If aNodeList.Count < 1 Then
					myHTML.Append("<p>Invalid application definition, no queries defined.</p>")
				Else
					If Not aNodeList(0).Attributes("header") Is Nothing Then
						header = aNodeList(0).Attributes("header").Value
						If header <> String.Empty Then myHTML.Append("<p>&nbsp;&nbsp;").Append(header).Append("</p>")
					End If
					aNodeList = aNodeList(0).ChildNodes
					If aNodeList.Count < 1 Then
						myHTML.Append("<p>Invalid application definition, no queries defined.</p>")
					Else
						myHTML.Append("<ul class=""queries"">")
						For Each aNode In aNodeList
							If aNode.NodeType = XmlNodeType.Element Then
								myHTML.Append("<li>")
								If Not (aNode.Attributes(EvoXMLInfo.url) Is Nothing OrElse aNode.Attributes(EvoXMLInfo.label) Is Nothing) Then
									myHTML.Append(HTMLLinkEventRef("q:" & aNode.Attributes(EvoXMLInfo.url).Value, aNode.Attributes(EvoXMLInfo.label).Value))
								Else
									myHTML.Append("- Invalid query -</li>")
									Exit For
								End If
							End If
						Next
						myHTML.Append("</ul>")
					End If
				End If
			End If
			myHTML.Append("<p>&nbsp;</p></div>")
			Return myHTML.ToString
		End Function

		Friend Function FormMessage(ByRef message As String, Optional ByVal icon As String = "") As String	', Optional ByVal cssclass As String = "")
			Dim myHTML As New StringBuilder

			With myHTML
				.Append("<table class=""Msg""><tr valign=""top""><td>")
				If icon.Length > 0 Then
					.Append("<img hspace=""10"" src=""").Append(_PathPixToolbar).AppendFormat("msg-{0}.gif"" alt=""{0}""/></td><td width=""100%"">", icon)
				End If
				'leave < and > 
				.Append(message)
				.Append(TdTrTableEnd)
			End With
			Return myHTML.Replace(vbCrLf, BR_tag).ToString
		End Function

		Friend Function FormSearch(ByRef sDisplayMode As Integer) As String
			Const SelectTagBegin As String = "<select class=""Field"" name="""
			Dim myCondition As String, buffer As String, fieldType As String, fieldlabel As String, fDisplayMode As Integer
			Dim myHTML As New StringBuilder
			Dim aNodeList As XmlNodeList
			Dim aNode As XmlNode

			_ItemID = 0
			If sDisplayMode = 3 Then buffer = XPathQuery.XPath2True(EvoXMLInfo.Search) Else buffer = XPathQuery.XPath2True("searchadv")
			aNodeList = myDOM.DocumentElement.SelectNodes(buffer, nsManager)
			myCondition = (New StringBuilder).Append("_c""><option value=""eq"">").Append(LG_cEquals).Append("<option value=""sw"" selected>").Append(LG_sStart).Append("<option value=""ct"">").Append(LG_sContain).Append("<option value=""fw"">").Append(LG_sFinish).ToString
			myHTML.Append("<table class=""holder"" cellpadding=""4"">")
			For Each aNode In aNodeList
				fieldType = aNode.Attributes(EvoXMLInfo.type).Value
				myHTML.Append("<tr valign=""top""><td width=""20%"" class=""right"">")
				fieldlabel = aNode.Attributes(EvoXMLInfo.label).Value
				If String.IsNullOrEmpty(fieldlabel) Then
					If aNode.Attributes(EvoXMLInfo.labelEdit) Is Nothing Then
						If Not aNode.Attributes(EvoXMLInfo.labelList) Is Nothing Then
							fieldlabel = aNode.Attributes(EvoXMLInfo.labelList).Value
						End If
					Else
						fieldlabel = aNode.Attributes(EvoXMLInfo.labelEdit).Value
					End If
				End If
				If _ShowDesigner Then
					fieldlabel += LinkDesigner("FD", CInt(aNode.Attributes("id").Value), fieldlabel)
				End If
				'BUG maybe can check that each field is only there once 
				buffer = UID & aNode.Attributes(EvoXMLInfo.dbColumn).Value
				myHTML.Append(HTMLFieldLabelSpan(buffer, fieldlabel)).Append("</td>")
				If sDisplayMode = 4 Then 'advanced search
					Select Case fieldType
						Case t_lov
							myHTML.Append("<td width=""20%""><div class=""FieldReadOnly"">")
							myHTML.Append(LG_anyof)
							myHTML.Append("</div></td><td width=""60%"">")
						Case t_bool, t_pix, t_doc
							myHTML.Append("<td width=""80%"" colspan=""2"">")
						Case Else
							myHTML.Append("<td width=""20%"">")
							myHTML.Append(SelectTagBegin).Append(buffer)
							Select Case fieldType
								Case t_text, t_txtm, t_html, t_email, t_url
									myHTML.Append(myCondition)
								Case t_date, t_datetime, t_time
									myHTML.Append("_c"">")
									If fieldType.Equals(t_time) Then
										myHTML.Append(HTMLOption("eq", LG_cAt))
									Else
										myHTML.Append(HTMLOption("eq", LG_sOn))
									End If
									myHTML.Append(HTMLOption("gt", LG_sAfter)).Append(HTMLOption("st", LG_sBefore))	'.Append(HTMLOption("", ""))
								Case t_dec, t_int
									myHTML.Append("_c"">").Append(HTMLOption("eq", "&#61;")).Append(HTMLOption("gt", "&#62;", True)).Append(HTMLOption("st", "&#60;"))
							End Select
							myHTML.Append("</option></select></td><td width=""60%"">")
					End Select
				Else 'regular search
					myHTML.Append("<td width=""80%"">")
				End If
				'all search
				Select Case fieldType
					Case t_text, t_txtm, t_html, t_email, t_url
						myHTML.Append(HTMLInputTextEmpty(buffer))
					Case t_lov
						If atRunTime Then
							If aNode.Attributes(EvoXMLInfo.dbColumnImg) Is Nothing Then
								fieldlabel = String.Empty
							Else
								fieldlabel = aNode.Attributes(EvoXMLInfo.dbColumnImg).Value
							End If
							If String.IsNullOrEmpty(fieldlabel) Then
								fDisplayMode = sDisplayMode
							Else
								fDisplayMode = 4
							End If
							If fDisplayMode = 3 Then
								myHTML.Append(SelectTagBegin).Append(buffer).Append("""><option value="""" selected>- ").Append(LG_any).Append(" -</option>").Append(HTMLlov(aNode)).Append("</select>")
							Else
								myHTML.Append(HTMLlov(aNode, buffer))
							End If
						Else
							If sDisplayMode = 3 Then
								myHTML.Append(SelectTagBegin).Append("a""><option value="""" selected>- ").Append(LG_any).Append(" -</option></select>")
							Else
								myHTML.Append(HTMLInputCheckBox("A", "", "A")).Append(HTMLInputCheckBox("B", "", "B")).Append(HTMLInputCheckBox("C", "", "C"))
							End If
						End If
					Case t_bool
						myHTML.Append(HTMLInputRadio(buffer, "1", LG_yes, False, buffer & "1")).Append(HTMLSpace)
						myHTML.Append(HTMLInputRadio(buffer, "0", LG_no, False, buffer & "0")).Append(HTMLSpace)
						myHTML.Append(HTMLInputRadio(buffer, "", LG_any, True, buffer & "-"))
					Case t_date, t_datetime, t_time
						If sDisplayMode.Equals(4) Then 'advanced search
							myHTML.Append(HTMLInputDate(buffer, "", _Language, _PathPixToolbar))
						Else 'regular search
							myHTML.Append("<nobr>").Append(SelectTagBegin).Append(buffer).Append("dir"" style=""width:50%""><option value="""">")
							myHTML.Append(LG_sDateRangeWithin).Append("<option value=""P"">").Append(LG_sDateRangeLast).Append("<option value=""F"">").Append(LG_sDateRangeNext).Append("</select>")
							myHTML.Append(SelectTagBegin).Append(buffer).Append(""" style=""width:50%""><option value="""">").Append(LG_sDateRangeAny)
							myHTML.Append(HTMLlovEnum(LG_sDateRange, "", , False)).Append("</select></nobr>")
						End If
						'Case t_int
					Case t_pix
						myHTML.Append(HTMLInputCheckBox(buffer, "1", LG_wPix, field_str, , , buffer))
					Case t_doc
						myHTML.Append(HTMLInputCheckBox(buffer, "1", LG_wDoc, field_str, , , buffer))
					Case t_int, t_dec
						myHTML.Append("<input class=""Field"" type=""text"" name=""").Append(buffer).Append(""" OnKeyUp=""Evol.checkNum(this,'").Append(fieldType.Substring(0, 1)).Append("')"">")
					Case Else
						myHTML.Append(HTMLInputTextEmpty(buffer))
				End Select
				myHTML.Append("</td></tr>")
			Next
			With myHTML
				''multiusers
				'If _SecurityMode.Equals(EvolSecurityMode.Multiple_Users_Sharing) Then
				'    .Append("<tr valign=""top""><td width=""20%""><p align=""right"">")
				'    .Append(HTMLFieldLabelSpan("", "Owner", "font", "")).Append("</p></td><td width=""")
				'    If sDisplayMode.Equals(4) Then
				'        .Append("20%""></td><td width=""60%"">")
				'    Else
				'        .Append("80%"">")
				'    End If
				'    buffer = UID & "OWNER"
				'    .Append(HTMLInputRadio(buffer, "1", LG_MyEntities.Replace("~ENTITIES~", entities), False, buffer & "1")).Append(HTMLSpace)
				'    .Append(HTMLInputRadio(buffer, "", LG_PubMine, True, buffer & "0")).Append(HTMLSpace)
				'    .Append("</td></tr>")
				'End If
				'With comments search box
				If _DBAllowComments <> EvolCommentsMode.None Then
					.Append("<tr valign=""top""><td><p class=""right"">")
					.Append(HTMLFieldLabelSpan(LG_wComments, PixComments)).Append("</p></td><td>")
					If sDisplayMode.Equals(4) Then
						.Append("</td><td>")
					End If
					.Append(HTMLInputCheckBox("EvoxUCM", "1", LG_wComments, field_str, String.Empty, , "EVOxucm"))
					.Append("</td></tr>")
				End If
				.Append("<tr class=""PanelLabel"" valign=""top""><td>&nbsp;</td><td>&nbsp;<input type=""submit"" name=""Search"" value="" ")
				.Append(LG_Search).Append(" "" class=""Button"">&nbsp;&nbsp;")
				If sDisplayMode.Equals(EvolDisplayMode.Search) Then
					.Append(SMALL_tag).Append(HTMLLinkEventRef("4", LG_AdvSearch)).Append(SMALL_tagClose)
				End If
				If sDisplayMode.Equals(4) Then .Append("</td><td>&nbsp;")
				.Append("</td></tr></table>")
			End With
			Return myHTML.ToString
		End Function

		Friend Function FormList(ByRef lDisplayMode As Integer) As String
			Dim i As Integer, sql As String, sql2 As String = String.Empty

			sql = BuildSQLlist(lDisplayMode)
			i = InStr(sql, "~$#12@~")
			If i > 0 Then
				sql2 = sql.Substring(i + 6, sql.Length - 6 - i)
				sql = sql.Substring(0, i - 1)
			End If
			Return HTMLDataset(myDOM.DocumentElement.SelectNodes(XPathQuery.XPath2True(EvoXMLInfo.SearchList), nsManager), sql, sql2, , , True, 0)
		End Function

		Friend Function FormEdit(ByVal eDisplayMode As Integer) As String
			Dim i As Integer, iTab As Integer, nbTabs As Integer = 0, MinLoopXML As Integer, MaxLoopXML As Integer
			Dim activeTab As Integer = 0, editOK As Boolean = False, YesNo As Boolean, extraJS As String = ""
			Dim PanelWidth As Integer, lineWidth As Integer, inTable As Boolean, TDww As String
			Dim buffer As String, buffer2 As String, bufferEnd As String, LinkDetailNew As String, useTabs As Boolean = False
			Dim nbFileUploads As Int32, dbmaxrows As Integer = 0, nbComments As Integer	', nbRating As Integer
			Dim PanelDetailsIndex As Integer = -1, PanelDetailsId As Integer = -1, PanelCSSclass As String, iconBuffer As String, iconColumnBuffer As String
			Dim myHTML As New StringBuilder
			Dim aNodeList As XmlNodeList, aNodeListTabs As XmlNodeList

			nbFileUploads = 0
			If Not (ds Is Nothing AndAlso _ItemID > 0) Then
				If (Not _DBReadOnly) AndAlso _DBAllowUpdate AndAlso atRunTime Then
					If _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_Sharing) Then
						Try : i = CInt(ds.Tables(0).Rows(0)(def_Data.dbcolumnuserid))
						Catch : i = -10 : End Try
					Else
						i = _UserID
					End If
					If i <> _UserID AndAlso _ItemID > 0 Then
						eDisplayMode = 0
						_DisplayMode = 0
					Else
						editOK = True
					End If
				End If
				iconBuffer = icon
				iconColumnBuffer = dbcolumnpix
				'##### HEADER #####
				'--- Tabs 
				aNodeListTabs = myDOM.DocumentElement.SelectNodes(XPathQuery.tab, nsManager)
				If Not aNodeListTabs Is Nothing Then
					nbTabs = aNodeListTabs.Count
					useTabs = nbTabs > 1
					If useTabs Then
						If atRunTime Then
							activeTab = String2Int(Page.Request("EvoActTab"))
							'-- Javascript for hidding and showing tabs
							extraJS = String.Format("var TABevo={0};", CStr(activeTab))
						Else
							activeTab = _DesignTabIndex
						End If
					End If
				End If
				For iTab = 0 To nbTabs + 1
					If useTabs Then
						aNodeList = aNodeListTabs(iTab).ChildNodes
						'-- tabs buttons
						If iTab = 0 Then
							If Not IEbrowser Then
								myHTML.Append("<div style=""height:10px""></div>")
							End If
							myHTML.Append("<div class=""TabBar""><span class=""tabholder"">")
							For i = 0 To nbTabs - 1
								myHTML.Append(HTMLbuttonTab("z", aNodeListTabs(i).Attributes(EvoXMLInfo.label).Value, i, i = activeTab))
							Next
							myHTML.Append("</span></div>")
							myHTML.Append(HTMLInputHidden("EvoActTab", CStr(activeTab)))
							If Not IEbrowser Then
								myHTML.Append("<div style=""height:5px""></div>")
							End If
						End If
						'begin tab
						myHTML.AppendFormat("<div id=""{0}Tab{1}""{2}>", "z", iTab, StyleVisibleToggle(iTab = activeTab))
					Else
						aNodeList = myDOM.DocumentElement.ChildNodes
					End If
					inTable = False
					lineWidth = 0
					MaxLoopXML = aNodeList.Count - 1
					'back to last search result
					buffer = String.Format("sql{0}{1}{2}", _UserID, def_Data.dbtable, _FormID)
					If Not Page.Cache(buffer & "_W") Is Nothing Then
						If (CStr(Page.Cache(buffer & "_W")).Length > 0) Then
							myHTML.AppendFormat("&nbsp;<a href=""javascript:EvPost('105')"">{0}</a>", LG_Back2SearchResults)
						End If
					End If
					'###--- Panels & PanelsDetails
					If MaxLoopXML > 0 Then
						If aNodeList(0).Name = "data" Then
							MinLoopXML = 1
						Else
							MinLoopXML = 0
						End If
					End If
					For i = MinLoopXML To MaxLoopXML
						buffer = aNodeList(i).Name
						If buffer.StartsWith("panel") Then
							If aNodeList(i).Attributes(EvoXMLInfo.Width) Is Nothing Then
								PanelWidth = 100
							Else
								PanelWidth = CInt(Val(aNodeList(i).Attributes(EvoXMLInfo.Width).Value))
								If PanelWidth = 0 Then PanelWidth = 100
							End If
							TDww = String.Format("<td valign=""top"" width=""{0}%"">", CStr(PanelWidth))
							buffer2 = String.Format("{0}p", (iTab + 1) * 100 + i)
							Select Case lineWidth + PanelWidth
								Case 100
									If PanelWidth < 100 Then
										myHTML.Append(TDww)
										buffer2 = String.Format("{0}p", i)
										inTable = False
									Else
										myHTML.Append(tableBeginTr).Append(TDww)
									End If
									bufferEnd = TdTrTableEnd
									lineWidth = 0
								Case Is > 100
									If inTable Then myHTML.Append(trTableEnd)
									myHTML.Append(tableBeginTr).Append(TDww)
									bufferEnd = "</td>"
									inTable = True
									lineWidth = PanelWidth
								Case Else
									If lineWidth = 0 Then
										If inTable Then myHTML.Append(trTableEnd)
										myHTML.Append(tableBeginTr)
										inTable = True
									End If
									myHTML.Append(TDww)
									bufferEnd = "</td>"
									lineWidth = lineWidth + PanelWidth
							End Select
							If buffer = "panel" Then
								'##--- panel
								myHTML.Append(HTMLPanelFields(aNodeList(i), eDisplayMode, buffer2, _ItemID > 0, i - MinLoopXML))
							Else
								'##--- panel details
								If Not detailsLoaded Then
									BuildSQLDetails(True)
								End If
								PanelDetailsIndex += 1
								With aNodeList(i)
									If .Attributes("panelid") Is Nothing Then
										PanelDetailsId = PanelDetailsIndex
									Else
										PanelDetailsId = CInt(Val(.Attributes("panelid").Value))
									End If
									If .Attributes("linknew") Is Nothing Then
										LinkDetailNew = String.Empty
									Else
										LinkDetailNew = .Attributes("linknew").Value
									End If
									If .Attributes(EvoXMLInfo.icon) Is Nothing Then
										icon = String.Empty
									Else
										icon = HTMLIcon(_PathPixToolbar & .Attributes(EvoXMLInfo.icon).Value)
									End If
									buffer = .Attributes(EvoXMLInfo.label).Value
									If .Attributes(EvoXMLInfo.CSSclass) Is Nothing Then
										PanelCSSclass = "Panel"
									Else
										PanelCSSclass = .Attributes(EvoXMLInfo.CSSclass).Value
										If String.IsNullOrEmpty(PanelCSSclass) Then PanelCSSclass = "Panel"
									End If
									myHTML.Append(HTMLDiv(String.Format("{0}pn{1}", UID, i), True))
									If Not ds2 Is Nothing Then
										YesNo = ds2.Tables(PanelDetailsIndex).Rows.Count > 0
									End If
									If YesNo Or (_DBAllowInsertDetails And _DisplayMode = 1 And Not _DBReadOnly) Then
										myHTML.AppendFormat("<table class=""{0} holder""><tr><td>", PanelCSSclass)
										If Not String.IsNullOrEmpty(buffer) Then
											myHTML.Append(HTMLPanelLabel(buffer, String.Format("{0}{1}P", UID, PanelDetailsId), "PanelLabel"))
											myHTML.Append("</td></tr><tr><td>")
										End If
										If .Attributes(EvoXMLInfo.icon) Is Nothing Then icon = String.Empty Else icon = .Attributes(EvoXMLInfo.icon).Value
										If .Attributes(EvoXMLInfo.dbColumnPix) Is Nothing Then dbcolumnpix = String.Empty Else dbcolumnpix = .Attributes(EvoXMLInfo.dbColumnPix).Value
										myHTML.Append(HTMLDataset(aNodeList(i).ChildNodes, "", buffer, eDisplayMode + 1, LinkDetailNew, False, PanelDetailsIndex, PanelDetailsId))
										myHTML.Append(TdTrTableEnd)
									End If
								End With
								myHTML.Append("</div>")
							End If
							myHTML.Append(bufferEnd)
						End If
					Next
					If i > 0 AndAlso inTable Then myHTML.Append(trTableEnd)
					'close tabs
					If useTabs Then
						myHTML.Append("</div>")
						If iTab > nbTabs - 2 Then Exit For
					Else
						Exit For
					End If
				Next
				'##### BUTTONS #####              'Save, Save and Add, Cancel / Edit ...and Next Last...
				myHTML.Append(FormButtons(eDisplayMode, editOK))
				If _DisplayMode = 0 AndAlso _ItemID > 0 Then
					'##### COMMENTS #####
					If _DBAllowComments <> EvolCommentsMode.None Then
						If atRunTime Then
							Try
								nbComments = CInt(ds.Tables(0).Rows(0)(SQLnbComments))
							Catch
								nbComments = 0
								noCommentsHere = True
							End Try
							If Not detailsLoaded Then BuildSQLDetails(True)
							myHTML.Append(FormComments(nbComments - 1))
						End If
						''##### RATING #####
						'If _DBAllowRating Then
						'    If Not atRunTime Then
						'        nbRating = CInt(ds.Tables(0).Rows(0)("maNbRatings"))
						'        myHTML.Append(nbRating & "Ratings")
						'        If nbComments > 0 Then
						'            sql = BuildSQL("*", "ka_Comments", "table='k_contact' and itemid=" & _ItemID, "macdate DESC")
						'            myHTML.Append(HTMLDataset(aNodeList, sql, "", 0))
						'        Else

						'        End If
						'    End If
						'End If
					End If
				End If
				icon = iconBuffer
				dbcolumnpix = iconColumnBuffer
				YesNo = _NavLinks And _DBAllowExport
				If YesNo Then myHTML.Append("<table class=""holder""><tr><td>")
				If _NavLinks Then myHTML.Append(HTMLNavLinks())
				If YesNo Then myHTML.Append("&nbsp;</td><td><p class=""right""><br/>&nbsp;")
				If _ItemID > 0 AndAlso _DBAllowExport Then myHTML.Append(HTMLLinkEventRef("72", String.Format(LG_ExportEntity, entity)))
				If YesNo Then myHTML.Append("&nbsp;</p></td></tr></table>")
				myHTML.Append(HTMLlightbox)
			Else
				myHTML.Append("<p>&nbsp;").Append(LG_NoDataDisp).Append("<br/>&nbsp;</p>")
			End If
			If _DisplayMode = 1 AndAlso JSValidDone AndAlso Not JSDetailsDone Then
				extraJS += "function evoJS82(){return false};"
			End If
			If extraJS.Length > 0 Then
				With Page.Response
					.Write(JSscriptBegin)
					.Write(extraJS)
					.Write(JSscriptEnd)
				End With
			End If
			If detailsLoaded Then
				ds2 = Nothing
				detailsLoaded = False
			End If
			Return myHTML.ToString
		End Function

		Friend Function FormLogin() As String
			Const fieldNameLogin As String = "EvoLogin"
			Dim myHTML As New StringBuilder

			With myHTML
				.Append("<p class=""center"">")
				.Append(HTMLPanelBegin("50%"))
				.Append("</td></tr><tr><td width=""100%"">")
				.Append(HTMLFieldLabel(fieldNameLogin, LG_Login))
				.Append(HTMLInputText(fieldNameLogin, Trim(Page.Request(fieldNameLogin)), sStyleFields, 50))
				.Append("</td></tr><tr><td>")
				.Append(HTMLFieldLabel("EvoPassword", LG_Password))
				.Append("<input type=""password"" class=""Field"" name=""EvoPassword"" maxlength=""50"">")
				.Append("</td></tr><tr><td class=""PanelLabel"">")
				.Append(HTMLInputButton("Login", LG_LoginB, True))
				.Append(TdTrTableEnd).Append("<br/></p>")
				.Append(JSscriptBegin).Append("document.getElementById('").Append(fieldNameLogin).Append("').focus();").Append(JSscriptEnd)
			End With
			Return myHTML.ToString
		End Function

		Private Function FormButtons(ByRef eDisplayMode As Integer, ByVal editOK As Boolean) As String
			'Buttons Save, Save and Add, Cancel / Edit
			Dim myHTML As New StringBuilder

			With myHTML
				If eDisplayMode > 0 Then '1=edit, 2=new
					.Append("<div width=""100%"" class=""PanelLabel"">&nbsp;&nbsp;&nbsp;")
					.Append("<input type=""button"" name=""save"" value="" ").Append(LG_Save).Append(" "" class=""Button"" onclick=""evoValidForm(1)"">")
					If _DBAllowInsert AndAlso _ItemID = 0 Then
						.Append("&nbsp;<input type=""button"" name=""saveadd"" value="" ").Append(LG_SaveAdd).Append(" "" class=""Button"" onclick=""evoValidForm(9)"">")
					End If
					.Append("&nbsp;<input type=""button"" name=""Cancel"" value="" ")
					If _ItemID = 0 Then
						.Append(LG_Cancel)
					Else
						.Append(LG_View)
					End If
					.Append(" "" class=""Button"" onclick=""EvPost('0')""></div>")
				Else 'view
					If (Not _DBReadOnly) AndAlso _DBAllowUpdate AndAlso editOK Then
						.Append("<div width=""100%"" class=""PanelLabel"">")
						.Append("&nbsp;<input type=""button"" name=""Edit"" value="" ").Append(LG_Edit).Append(" "" class=""Button"" onclick=""EvPost('1')""></div>")
					End If
				End If
			End With
			Return myHTML.ToString
		End Function

		'Friend Function FormTemplates() As String
		'    Dim i As Integer, maxLoopXML As Integer, fieldDBcolumn As String, fieldDBcolumn1 As String, fieldType As String, fieldlabel As String
		'    Dim myHTML As New StringBuilder
		'    Dim aNodeList As XmlNodeList

		'    myHTML.Append("<h1>Fields</h1><ul>")
		'    aNodeList = myDOM.DocumentElement.SelectNodes(XPathQuery.panelField, nsManager)
		'    maxLoopXML = aNodeList.Count - 1
		'    For i = 0 To maxLoopXML
		'        With aNodeList(i)
		'            fieldDBcolumn = .Attributes(Attr.dbColumn).Value
		'            fieldType = .Attributes(Attr.Type).Value
		'            fieldlabel = .Attributes(Attr.label).Value
		'            Try
		'                fieldlabel = .Attributes("labeledit").Value
		'            Catch : End Try
		'            myHTML.Append("<li>").Append(fieldlabel).Append(HTML_Sep).Append(fieldType).Append(", ").Append(fieldDBcolumn).Append(".")
		'        End With
		'    Next
		'    With myHTML
		'        .Append("</ul><hr><h1>View</h1>")
		'        .Append(FormEdit(0))

		'        .Append("<hr><h1>Edit</h1>")
		'        .Append(FormEdit(1))

		'        .Append("<hr><h1>List</h1>")
		'        .Append(FormList(110))

		'        .Append("<hr><h1>Search</h1>")
		'        .Append(FormSearch(3))

		'        .Append("<hr><h1>Advanced Search</h1>")
		'        .Append(FormSearch(4))

		'        .Append("<hr><h1>Export</h1>")
		'        .Append(FormExport()).Append("<hr>")
		'    End With

		'    Return myHTML.ToString

		'End Function

		'Friend Function FormScan() As String
		'    Dim myHTML As New StringBuilder

		'    Dim sql As String, buffer As String, buffer2 As String
		'    Dim MaxLoop As Integer, i As Integer, j As Integer
		'    Dim ds As New DataSet
		'    Dim prevTable As String, myTable As String, myFormID As Integer, myPanelID As Integer

		'    sql = "select sysobjects.name as dbtable "
		'    sql += "from sysobjects  "
		'    sql += "WHERE rtrim(sysobjects.xtype)='U' "
		'    sql += " AND sysobjects.status>0 "
		'    sql += " order by sysobjects.name"
		'    ds = GetData(sql, _SqlConnection) 'not connectiondico 
		'    If Not ds Is Nothing Then
		'        prevTable = ""
		'        '######### Form #########
		'        MaxLoop = ds.Tables(0).Rows.Count - 1
		'        For i = 0 To MaxLoop
		'            myTable = CStr(ds.Tables(0).Rows(i).Item(Attr.dbTable))
		'            myHTML.Append(HTMLInputCheckBox(myTable, myTable, myTable, , , , myTable)).Append(BR_tag)
		'        Next
		'    End If

		'    dicoDB2DB()

		'    'HTMLFieldLabel("", "Fields to include in the export", sStyleFieldLabels, , ""))
		'    'maxLoopXML = aNodeList.Count - 1
		'    'For i = 0 To maxLoopXML
		'    '    With aNodeList(i)
		'    '        fieldlabel = .Attributes("label").Value
		'    '        If fieldlabel = "" Then
		'    '            Try
		'    '                fieldlabel = .Attributes("labeledit").Value
		'    '            Catch : End Try
		'    '            If fieldlabel = "" Then
		'    '                Try
		'    '                    fieldlabel = .Attributes("labellist").Value
		'    '                Catch : End Try
		'    '            End If
		'    '        End If
		'    '        buffer = UID & .Attributes("dbcolumn").Value
		'    '        myHTML.Append(HTMLInputCheckBox(buffer, "1", fieldlabel, "field", , True, buffer & CStr(i)))
		'    '    End With
		'    'Next
		'    'buffer = "showID"
		'    'myHTML.Append(HTMLInputCheckBox(buffer, "1", "ID (unique key)", "field", , , buffer))

		'    FormScan = myHTML.ToString
		'End Function

		'Friend Function FormHeal() As String
		'    Dim i As Integer, expOut As String, maxLoopXML As Integer, buffer As String, fieldlabel As String, sql As String
		'    Dim SelectTagBegin As String = "<select" & sStyleFieldsNot100pc & "name="""
		'    Dim myHTML As New StringBuilder
		'    Dim aNodeList As XmlNodeList

		'    sql = "MA_FormDB '" & dbtable & "'"

		'    'if nothing then
		'    '   no table => create table + lovs + pop lov
		'    'else  
		'    '   for i=0 to nbfields-1
		'    '       check if exists => add field or increase maxlength
		'    '       + lov check if lov has exists & has at least one values
		'    '   next
		'    'end if
		'    'log it
		'    'propose 12 sample records

		'    expOut = "SQL"
		'    aNodeList = myDOM.DocumentElement.SelectNodes(XPathQuery.panelField, nsManager)

		'    myHTML.Append(TableBeginPanel)
		'    myHTML.Append("<tr><td valign=""top"" width=""40%""><p>")
		'    myHTML.Append("Fields to include in the export:<br/>")
		'    maxLoopXML = aNodeList.Count - 1
		'    For i = 0 To maxLoopXML
		'        With aNodeList(i)
		'            fieldlabel = .Attributes("label").Value
		'            If fieldlabel = "" Then
		'                Try
		'                    fieldlabel = .Attributes("labeledit").Value
		'                Catch
		'                End Try
		'            End If
		'            buffer = .Attributes("dbcolumn").Value
		'            buffer = UID & buffer
		'            ''myHTML.Append(HTMLInputCheckBox(buffer, "1", "", "field", , , True))
		'            myHTML.Append("<input type=""checkbox"" name=""").Append(buffer).Append(""" value=""1"" ")
		'            myHTML.Append("style=""").Append(Style).Append("""  checked=""checked"">")
		'            myHTML.Append("<").Append(TagName).Append(" ").Append(sStyleFieldLabels).Append("><label for=""").Append(buffer).Append(""">").Append(fieldlabel)
		'            myHTML.Append("</label></").Append(TagName).Append("><br/>")
		'            'myHTML.Append(HTMLInputCheckBox(buffer, "1", "", "field", , , True))
		'            'myHTML.Append(HTMLFieldLabel(buffer, fieldlabel, sStyleFieldLabels, "font", ""))
		'        End With
		'    Next
		'    myHTML.Append("</td><td valign=""top"" width=""60%"">")
		'    myHTML.Append(TdTrTableEnd)
		'    FormHeal = myHTML.ToString
		'End Function

#End Region

		'### Forms Support HTML ##############################################################################
#Region "Forms Support HTML"

		Private Function HTMLNavLinks() As String
			'first, prev., next, last navigation for records
			Dim myHTML As New StringBuilder, buffer As String
			Dim nav_first As Boolean, nav_last As Boolean

			If _DisplayMode.Equals(1) Then buffer = "30" Else buffer = "20"
			myHTML.Append("<br/>&nbsp;").Append(HTMLLinkEventRef("110", allEntities)).Append(":&nbsp; ")
			If String.IsNullOrEmpty(navBar) Then
				If String.IsNullOrEmpty(def_Data.spget) Then
					nav_first = nav <> 1 And navd <> 1
					nav_last = nav <> 4 And navd <> 3
				Else
					nav_first = True
					nav_last = True
				End If
			Else
				nav_first = navBar.Substring(0, 1).Equals("1")
				nav_last = Right(navBar, 1).Equals("1")
			End If
			If nav_first Then myHTML.Append(HTMLLinkEventRef(buffer & "1", LG_pFirst)).Append(HTMLSpace3).Append(HTMLLinkEventRef(buffer & "2", LG_pPrev)).Append(HTMLSpace3)
			If nav_last Then myHTML.Append(HTMLLinkEventRef(buffer & "3", LG_pNext)).Append(HTMLSpace3).Append(HTMLLinkEventRef(buffer & "4", LG_pLast))
			myHTML.Append("<br/>&nbsp;")
			Return myHTML.ToString
		End Function

		Friend Function TXTec(ByRef FieldType As String, ByRef FieldValue As String, ByRef [Operator] As String) As String
			'returns a "condition" in SQL or plain English

			If FieldType = t_text Then	'textmultiline is passed as text !
				Select Case [Operator]
					Case "eq"
						Return String.Format(LG_lEquals, FieldValue)
					Case "sw"
						Return String.Format(LG_lStart, FieldValue)
					Case "fw"
						Return String.Format(LG_lFinish, FieldValue)
					Case Else ' "ct" 
						Return String.Format(LG_lContain, FieldValue)
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

		'Private Function HTMLAlphabetLinks() As String
		'    Dim myHTML As New StringBuilder, buffer As String
		'    Dim i As Integer

		'    'not always "evo1"
		'    With myHTML
		'        .Append("<a href=""javascript:__doPostBack('evo1','q:~A')"">A")
		'        For i = 66 To 90
		'            .Append("</a> - <a href=""javascript:__doPostBack('evo1','q:~").Append(Chr(i)).Append("')"">").Append(Chr(i))
		'        Next
		'        .Append("</a> - <a href=").Append("javascript:__doPostBack('evo1','q:~0')>#")
		'    End With
		'    Return myHTML.ToString
		'End Function

		Private Function HTMLDataset(ByRef aNodeList As XmlNodeList, ByRef sql As String, ByRef title As String, Optional ByVal ListMode As Integer = 0, Optional ByVal LinkDetailNew As String = "", Optional ByVal InsidePanel As Boolean = True, Optional ByVal PanelDetailsIndex As Integer = -1, Optional ByVal PanelDetailsID As Integer = -1) As String
			'ListMode: 0=list, 1=details, 2 details edit
			Dim ctable As String, iconEntityDetails As String, UseComments As Boolean = False
			Dim YesNo As Boolean, fieldType As String, fieldValue As String, endTD As String = "&nbsp;</td>"  ', EditLink As Boolean = False
			Dim i As Integer, j As Integer, nbCommentsRow As Integer, MaxLoopXML As Integer, MaxLoopSQL As Integer, MinLoop As Integer, TotalNbRecords As Integer
			Dim htmlRecCount As String = String.Empty
			Dim trOdd As String, trEven As String
			Dim buffer As String, buffer1 As String = String.Empty, buffer2 As String = String.Empty
			Dim myHTML As New StringBuilder
			Dim myHTML2 As New StringBuilder
			Dim fieldFormats(0, 0) As String
			Dim ds As DataSet, t As DataTable
			Dim myLabel As String

			If atRunTime Then
				If Not _DBAllowUpdateDetails Then  '  and _DBAllowInsertDetails
					If ListMode = 2 Then ListMode = 1
				End If
				'set record counts values
				MaxLoopSQL = -1
				If String.IsNullOrEmpty(sql) Then
					If detailsLoaded Then  '       PanelDetailsID??? bug qqq 
						If PanelDetailsIndex > -1 Then
							t = ds2.Tables(PanelDetailsIndex)
							MaxLoopSQL = t.Rows.Count - 1  'sql = "*"
						Else
							t = Nothing
						End If
					Else
						t = Nothing
					End If
				Else
					ds = GetData(sql, _SqlConnection)
					If ds Is Nothing Then
						t = Nothing
					Else
						t = ds.Tables(0)
						MaxLoopSQL = t.Rows.Count - 1
					End If
				End If
				If sql.Length > 0 Then
					If String.IsNullOrEmpty(ErrorMsg) Then
						If ListMode = 0 AndAlso Not String.IsNullOrEmpty(def_Data.sppaging) Then
							j = 0
							If MaxLoopSQL = _RowsPerPage - 1 Then
								Try
									j = CInt(t.Rows(0)("MoreRecords"))
								Catch : End Try
							End If
							If MaxLoopSQL > -1 Then
								TotalNbRecords = _RowsPerPage * (pageID - 1) + j + MaxLoopSQL + 1
							Else
								TotalNbRecords = 0
							End If
						Else
							TotalNbRecords = MaxLoopSQL + 1
						End If
						htmlRecCount = HTMLrecordCount(TotalNbRecords, MaxLoopSQL)
					Else
						MaxLoopSQL = -1
						TotalNbRecords = MaxLoopSQL + 1
						htmlRecCount = String.Empty
					End If
				End If
			Else 'not atRunTime  
				ds = Nothing
				MaxLoopSQL = _RowsPerPage - 1
				TotalNbRecords = 2 * _RowsPerPage + 1
				htmlRecCount = (New StringBuilder).Append(TextUcaseFirst(entities)).Append(" 1 - ").Append(CStr(_RowsPerPage)).Append(LG_sOf).Append(CStr(TotalNbRecords)).ToString
			End If
			If MaxLoopSQL > -1 OrElse ListMode = 2 Then
				MaxLoopXML = aNodeList.Count - 1
				'display result
				If InsidePanel Then
					myHTML.Append("<table class=""holder"" cellpadding=""4""")
					If PanelDetailsID > -1 Then myHTML.Append(" ID=""").Append(UID).Append(PanelDetailsID).Append("P"">") Else myHTML.Append(">")
				End If
				If ListMode = 0 Then
					'If dbcolumnlead <> String.Empty Then myHTML.Append("<tr><td colspan=""2"">").Append(HTMLAlphabetLinks).Append("</td></tr>")
					myHTML.Append("<tr class=""FieldLabel"" valign=""top""><td><p>").Append(title)
					If _ShowDesigner AndAlso _FormID > 0 Then myHTML.Append(LinkDesigner("SD", _FormID, title))
					myHTML.Append("</p></td><td><p class=""right"">")
					If TotalNbRecords > 0 Then
						If _DBAllowExport Then
							htmlRecCount += HTML_Sep & HTMLLinkEventRef("70", LG_Export) & HTMLSpace
						End If
						myHTML.Append(htmlRecCount)
					End If
					myHTML.Append("</p></td></tr><tr><td colspan=""2"">")
				Else
					If InsidePanel Then myHTML.Append("<tr><td>")
				End If
				'If _Debug Then myHTML.Append(HTMLItalics(sql))  
				If MaxLoopSQL > -1 OrElse (ListMode = 2 AndAlso _DBAllowInsertDetails) Then
					myHTML.AppendFormat("<span id=""{0}{1}p"">", UID, PanelDetailsID)
					myHTML.Append("<table id=""EvoEditGrid"" cellSpacing=""0"" cellPadding=""3"" border=""1"" rules=""all"" bordercolor=""#808080"" style=""border-collapse:collapse;border-style:solid;border-color:#808080;")
					If ListMode = 0 Then
						If ToHtml(BackColorRowMouseOver) <> String.Empty Then myHTML.Append("behavior:url(").Append(_PathPixToolbar).Append("tablehl.htc);"" slcolor='#FFFFCC' hlcolor='").Append(ToHtml(BackColorRowMouseOver)).Append("'")
					End If
					myHTML.Append(""" width=""100%"">")
					If MaxLoopXML = 0 Then '1 field only
						If aNodeList(0).Attributes(EvoXMLInfo.labelList) Is Nothing Then
							myLabel = aNodeList(0).Attributes(EvoXMLInfo.label).Value
						Else
							myLabel = aNodeList(0).Attributes(EvoXMLInfo.labelList).Value
						End If
					Else
						myLabel = Tilda
					End If
					'##### table header 
					MaxLoopXML = aNodeList.Count - 1
					ReDim fieldFormats(MaxLoopXML, 2)
					myHTML2.Append("<THEAD><tr class=""RowHeader"">")
					If ListMode = 2 Then 'edit details  
						myHTML2.Append("<td width=""1%"">ID</td>")
						endTD = "</td>"
					End If
					For j = 0 To MaxLoopXML
						With aNodeList(j)
							'cache field format 
							ctable = String.Format(".{0}", .Attributes(EvoXMLInfo.dbColumnRead).Value)
							Select Case .Attributes(EvoXMLInfo.type).Value
								Case t_text, t_pix
									If ListMode < 2 Then
										If .Attributes(EvoXMLInfo.link) Is Nothing Then
											fieldFormats(j, 0) = String.Empty
										Else
											fieldFormats(j, 0) = .Attributes(EvoXMLInfo.link).Value
										End If
										If .Attributes(EvoXMLInfo.linkTarget) Is Nothing Then
											fieldFormats(j, 1) = String.Empty
										Else
											fieldFormats(j, 1) = .Attributes(EvoXMLInfo.linkTarget).Value
										End If
									Else
										fieldFormats(j, 0) = String.Empty
									End If
								Case t_lov
									If ListMode < 2 Then
										ctable = String.Format("@{0}", .Attributes(EvoXMLInfo.dbColumn).Value)
										If .Attributes(EvoXMLInfo.link) Is Nothing Then
											buffer = String.Empty
										Else
											buffer = .Attributes(EvoXMLInfo.link).Value
										End If
										If buffer.Length > 0 Then
											If .Attributes(EvoXMLInfo.linkTarget) Is Nothing Then
												fieldFormats(j, 1) = String.Empty
											Else
												fieldFormats(j, 1) = .Attributes(EvoXMLInfo.linkTarget).Value
											End If
											If .Attributes(EvoXMLInfo.linkLabel) Is Nothing Then
												fieldFormats(j, 2) = String.Empty
											Else
												fieldFormats(j, 2) = .Attributes(EvoXMLInfo.linkLabel).Value
											End If
										End If
									Else
										buffer = String.Empty
									End If
									fieldFormats(j, 0) = buffer
								Case t_int, t_dec
									If ListMode = 2 Then
										If .Attributes(EvoXMLInfo.dbReadOnly) Is Nothing Then
											YesNo = False
										Else
											YesNo = .Attributes(EvoXMLInfo.dbReadOnly).Value.Equals("1")
										End If
									Else
										YesNo = True
									End If
									If YesNo Then
										If .Attributes(EvoXMLInfo.Format) Is Nothing Then
											fieldFormats(j, 0) = String.Empty
										Else
											fieldFormats(j, 0) = .Attributes(EvoXMLInfo.Format).Value
										End If
									Else
										fieldFormats(j, 0) = String.Empty
									End If
								Case t_date, t_time, t_datetime
									If .Attributes(EvoXMLInfo.Format) Is Nothing Then
										fieldFormats(j, 0) = DefaultDateFormat(.Attributes(EvoXMLInfo.type).Value, _Language)
									Else
										fieldFormats(j, 0) = .Attributes(EvoXMLInfo.Format).Value
										If fieldFormats(j, 0) = "" Then
											fieldFormats(j, 0) = DefaultDateFormat(.Attributes(EvoXMLInfo.type).Value, _Language)
										End If
									End If
								Case t_bool
									If .Attributes("imglist") Is Nothing Then
										fieldFormats(j, 0) = String.Empty
									Else
										fieldFormats(j, 0) = .Attributes("imglist").Value
									End If
									If String.IsNullOrEmpty(fieldFormats(j, 0)) Then
										If .Attributes(EvoXMLInfo.Img) Is Nothing Then
											fieldFormats(j, 0) = String.Empty
										Else
											fieldFormats(j, 0) = .Attributes(EvoXMLInfo.Img).Value
										End If
									End If
									If fieldFormats(j, 0).Length = 0 Then fieldFormats(j, 0) = pix_check
								Case t_url
									If .Attributes("imglist") Is Nothing Then
										buffer = String.Empty
									Else
										buffer = .Attributes("imglist").Value
									End If
									If buffer.Length = 0 AndAlso Not .Attributes(EvoXMLInfo.Img) Is Nothing Then
										buffer = .Attributes(EvoXMLInfo.Img).Value
									End If
									If buffer.Length > 0 Then buffer = _PathPixToolbar & buffer
									fieldFormats(j, 0) = buffer
								Case t_formula
									ctable = Tilda & .Attributes(EvoXMLInfo.dbColumnRead).Value
									If .Attributes(EvoXMLInfo.Format) Is Nothing Then
										fieldFormats(j, 0) = String.Empty
									Else
										fieldFormats(j, 0) = .Attributes(EvoXMLInfo.Format).Value
									End If
								Case Else
									fieldFormats(j, 0) = String.Empty
							End Select
							'built header
							If .Attributes(EvoXMLInfo.labelList) Is Nothing Then
								myLabel = .Attributes(EvoXMLInfo.label).Value
							Else
								myLabel = .Attributes(EvoXMLInfo.labelList).Value
							End If
							If j = 0 AndAlso String.IsNullOrEmpty(myLabel) Then
								myHTML2.Append("<td>")
								myHTML2.Append(HTMLlov(aNodeList(j), .Attributes(EvoXMLInfo.dbColumnRead).Value))
							Else
								'If _AllowSorting AndAlso ListMode = 0 AndAlso TotalNbRecords > 2 Then
								'	myHTML2.AppendFormat("<td onmouseover=""javascript:Evol.showSort(this,'{0}')"">", ctable)
								'Else
								myHTML2.Append("<td>")
								'End If
								myHTML2.Append(myLabel)
								If _ShowDesigner Then myHTML2.Append(LinkDesigner("FD", CInt(.Attributes("id").Value), myLabel))
								If _AllowSorting AndAlso ListMode = 0 AndAlso TotalNbRecords > 2 Then
									If IEbrowser Then
										myHTML2.AppendFormat("<a href=""javascript:EvPost('a:{0}')"" class=""ico arrUp""></a><a href=""javascript:EvPost('d:{0}')"" class=""ico arrDown""></a>", ctable)
									Else
										myHTML2.AppendFormat("<img src=""{1}ordUp.gif"" alt="""" onclick=""javascript:EvPost('a:{0}')"" /><img class=""tool"" src=""{1}ordDown.gif"" onclick=""javascript:EvPost('d:{0}')"" alt=""""/>", ctable, _PathPixToolbar)
									End If
								End If
							End If
							myHTML2.Append("</td>")
						End With
					Next
					myHTML2.Append("</tr></THEAD>")
					'add or skip table header
					If MaxLoopSQL = 0 Then ' 1 row 
						If MaxLoopXML = 0 Then	 '1 col 
							If myLabel <> String.Empty Then myHTML.Append(HTMLFieldLabel("", myLabel))
						Else
							myHTML.Append(myHTML2)
						End If
					Else
						If MaxLoopXML > 0 OrElse myLabel <> String.Empty Then myHTML.Append(myHTML2)
					End If
					'#####  table body
					trEven = "<tr class=""RowEven"""
					trOdd = "<tr class=""RowOdd"""
					myHTML.Append("<TBODY>")
					If ListMode = 2 AndAlso (_DBAllowInsertDetails OrElse _DBAllowUpdateDetails) AndAlso Not _DBReadOnly Then
						With Page.Response
							.Write(JSscriptBegin)
							.Write(JSEditDetails(aNodeList)) 'sets nbFieldEditable
							.Write(JSscriptEnd)
						End With
					End If
					If atRunTime Then
						'### at run-time
						If ListMode = 2 AndAlso MaxLoopSQL < 0 Then
							myHTML.Append("<tr class=""RowEven"" height=""0"" style=""height:0"" id=""r0"">")
							For j = 0 To MaxLoopXML
								myHTML.Append("<td></td>")
							Next
							myHTML.Append("</tr>")
						Else
							If _DBAllowComments <> EvolCommentsMode.None Then
								Try
									nbCommentsRow = CInt(t.Rows(0)(SQLnbComments))
								Catch : nbCommentsRow = -1 : End Try
								UseComments = nbCommentsRow > -1
							End If
						End If
						If String.IsNullOrEmpty(icon) Then
							iconEntityDetails = String.Empty
						Else
							iconEntityDetails = HTMLIcon(_PathPixToolbar & icon)
						End If
						With myHTML
							For i = 0 To MaxLoopSQL
								If YesNo Then .Append(trEven) Else .Append(trOdd)
								YesNo = Not YesNo
								buffer2 = CStr(t.Rows(i).Item(dbPrimaryKey)).TrimEnd
								If UseComments Then
									Try
										nbCommentsRow = CInt(t.Rows(i)(SQLnbComments))
									Catch
										nbCommentsRow = -1
										UseComments = False
									End Try
								End If
								Select Case ListMode
									Case 2 'details edit 
										buffer1 = CStr(i + 1).TrimStart
										If nbFieldEditable > 0 Then .AppendFormat(" onclick=""evoEditRow({0})""", buffer1)
										.AppendFormat(" id=""r{0}""><td>{1}</td>", buffer1, buffer2)
										MinLoop = 0
									Case 0	 'search result (master)  
										.AppendFormat("><td><a href=""javascript:EvPost('-{0}')"">", buffer2)
										Try	'0=ID
											fieldValue = CStr(t.Rows(i)(aNodeList(0).Attributes(EvoXMLInfo.dbColumnRead).Value)).Trim
										Catch : fieldValue = String.Empty : End Try
										If String.IsNullOrEmpty(icon) Then
											Try : buffer = CStr(t.Rows(i)(dbcolumnpix))
											Catch : buffer = String.Empty : End Try
											If buffer.Length > 0 Then
												.Append(HTMLIcon(_PathPix & buffer))
											End If
										Else
											.Append(icon)
										End If
										If String.IsNullOrEmpty(fieldValue) Then
											.AppendFormat("({0})</a>", buffer2)
										Else
											.Append(fieldValue).Append("</a>")
										End If
										If nbCommentsRow > 0 Then .Append(HTMLCommentFlag(nbCommentsRow, PixComments))
										.Append("&nbsp;</td>")
										'If EditLink Then .Append("&nbsp;<small>[<a href=""Javascript:").Append(Page.GetPostBackEventReference(Me, "e" & buffer2)).Append(""">Edit</a>]<small>")
										MinLoop = 1
									Case Else	'details list=1
										.Append(">")
										MinLoop = 0
										If String.IsNullOrEmpty(icon) AndAlso dbcolumnpix.Length > 0 Then
											If t.Rows(i)(dbcolumnpix) Is DBNull.Value Then
												iconEntityDetails = String.Empty
											Else
												iconEntityDetails = HTMLIcon(_PathPix & CStr(t.Rows(i)(dbcolumnpix)))
											End If
										End If
								End Select
								For j = MinLoop To MaxLoopXML  'aNodeList.Count - 1
									.Append("<td>")
									Try
										fieldValue = CStr(t.Rows(i)(aNodeList(j).Attributes(EvoXMLInfo.dbColumnRead).Value)).Trim
									Catch : fieldValue = String.Empty : End Try
									fieldType = aNodeList(j).Attributes(EvoXMLInfo.type).Value
									If fieldValue <> String.Empty Then
										Select Case fieldType
											Case t_text
												If j = MinLoop Then
													If ListMode = 1 AndAlso fieldFormats(j, 0).Length > 0 Then
														If iconEntityDetails <> String.Empty Then fieldValue = iconEntityDetails & fieldValue
														.Append(HTMLLink(Link4itemid(fieldFormats(j, 0), CStr(t.Rows(i)(dbPrimaryKey))), fieldValue))
													Else
														.Append(fieldValue)
													End If
													If UseComments AndAlso ListMode > 0 Then
														Try
															nbCommentsRow = CInt(t.Rows(i)(SQLnbComments))
														Catch
															nbCommentsRow = 0
															UseComments = False
														End Try
														If nbCommentsRow > 0 Then .Append(HTMLCommentFlag(nbCommentsRow, PixComments))
													End If
												Else
													If ListMode = 1 AndAlso fieldFormats(j, 0).Length > 0 Then
														.Append(HTMLLink(Link4itemid(fieldFormats(j, 0), CStr(t.Rows(i)(dbPrimaryKey))), fieldValue, fieldFormats(j, 1)))
													Else
														.Append(fieldValue)
													End If
												End If
											Case t_lov
												If ListMode <> 2 Then
													If fieldFormats(j, 0).Length > 0 Then
														If fieldFormats(j, 2).Length > 0 Then fieldValue = fieldFormats(j, 2) 'linklabel
														fieldValue = HTMLLink(Link4itemid(fieldFormats(j, 0), CStr(t.Rows(i)(aNodeList(j).Attributes(EvoXMLInfo.dbColumn).Value))), fieldValue, fieldFormats(j, 1))
													Else
														If Not aNodeList(j).Attributes(EvoXMLInfo.dbColumnImg) Is Nothing Then
															buffer1 = aNodeList(j).Attributes(EvoXMLInfo.dbColumnImg).Value
															If buffer1.Length > 0 Then
																Try
																	buffer2 = CStr(t.Rows(i)(buffer1))
																Catch : buffer2 = String.Empty : End Try
																If buffer2.Length > 0 Then fieldValue = HTMLImg(_PathPix & buffer2) & HTMLSpace & fieldValue
															End If
														End If
													End If
												End If
												.Append(fieldValue)
											Case t_bool
												If fieldValue.Equals("1") OrElse fieldValue.Equals("True") Then
													If fieldFormats(j, 0).Length > 0 Then
														buffer1 = fieldFormats(j, 0)
													Else
														buffer1 = pix_check
													End If
													.Append(HTMLSpace).Append(HTMLImg(_PathPixToolbar & buffer1, LG_Checked))
												End If
											Case t_int
												If fieldFormats(j, 0).Length > 0 AndAlso IsNumeric(fieldValue) Then
													.Append(NoBR(Format(Val(fieldValue), fieldFormats(j, 0))))
												Else
													.Append(fieldValue)
												End If
											Case t_dec
												If fieldFormats(j, 0).Length > 0 Then
													Try
														fieldValue = NoBR(Format(CDec(fieldValue), fieldFormats(j, 0)))
													Catch : fieldValue = String.Empty : End Try
												End If
												.Append(fieldValue)
											Case t_date, t_time
												If fieldFormats(j, 0).Length > 0 AndAlso IsDate(fieldValue) Then
													Try
														fieldValue = NoBR(Format(CDate(fieldValue), fieldFormats(j, 0)))
													Catch : fieldValue = String.Empty : End Try
												End If
												.Append(fieldValue)
											Case t_datetime
												If IsDate(fieldValue) Then
													If fieldFormats(j, 0).Length > 0 Then
														formatedDateTime(CDate(fieldValue))
													Else
														.Append(Format(CDate(fieldValue), fieldFormats(j, 0)))
													End If
												End If
											Case t_url
												If fieldFormats(j, 0).Length > 0 Then
													.Append(HTMLLink(fieldValue, String.Empty, inNewBrowser, fieldFormats(j, 0)))
												Else
													.Append(HTMLLink(fieldValue, fieldValue, inNewBrowser))
												End If
											Case t_txtm
												.Append(Text2HTMLwBR(fieldValue))
											Case t_doc
												.Append(HTMLLink(_PathPix & fieldValue, fieldValue, inNewBrowser))
											Case t_pix
												If ListMode < 2 AndAlso fieldFormats(j, 0).Length > 0 Then
													.Append(HTMLLink(Link4itemid(fieldFormats(j, 0), CStr(t.Rows(i)(dbPrimaryKey))), "", fieldFormats(j, 1), _PathPix & fieldValue))
												Else
													.Append(HTMLImg(_PathPix & fieldValue))
												End If
											Case t_formula
												If fieldFormats(j, 0).Length > 0 AndAlso IsNumeric(fieldValue) Then
													Try
														fieldValue = NoBR(Format(CDec(fieldValue), fieldFormats(j, 0)))
													Catch : fieldValue = String.Empty : End Try
												End If
												.Append(fieldValue)
											Case Else
												.Append(fieldValue)
										End Select
									End If
									.Append(endTD)
								Next
								.Append("</tr>")
							Next
						End With
					Else
						'### Design mode
						buffer1 = aNodeList(0).Attributes(EvoXMLInfo.type).Value
						For i = 0 To MaxLoopSQL
							If YesNo Then myHTML.Append(trEven) Else myHTML.Append(trOdd)
							YesNo = Not YesNo
							myHTML.Append("><td>").Append(HTMLLink("#", RandomFieldValue(buffer1, "", _PathPixToolbar)))
							'If EditLink Then myHTML.Append("&nbsp;<small>[<a href=""#"">Edit</a>]<small>")
							myHTML.Append("&nbsp;</td>")
							For j = 1 To MaxLoopXML	 'aNodeList.Count - 1
								fieldType = aNodeList(j).Attributes(EvoXMLInfo.type).Value
								fieldValue = RandomFieldValue(fieldType, String.Empty, _PathPixToolbar)
								Select Case fieldType
									Case t_text
										If ListMode = 1 AndAlso fieldFormats(j, 0).Length > 0 Then
											fieldValue = HTMLLink("#", fieldValue)
										End If
									Case t_lov
										If fieldFormats(j, 0).Length > 0 Then fieldValue = HTMLLink("#", fieldValue)
									Case t_bool
										If fieldValue.Equals("1") Then
											If fieldFormats(j, 0).Length > 0 Then
												buffer1 = fieldFormats(j, 0)
											Else
												buffer1 = pix_check
											End If
											fieldValue = HTMLImg(_PathPixToolbar & buffer1, LG_Checked)
										Else
											fieldValue = String.Empty
										End If
									Case t_int
										If fieldValue.Length > 0 AndAlso fieldFormats(j, 0).Length > 0 Then
											If IsNumeric(fieldValue) Then
												fieldValue = NoBR(Format(Val(fieldValue), fieldFormats(j, 0)))
											Else
												fieldValue = String.Empty
											End If
										End If
									Case t_dec
										If fieldValue.Length > 0 AndAlso fieldFormats(j, 0).Length > 0 Then
											Try
												fieldValue = NoBR(Format(CDec(fieldValue), fieldFormats(j, 0)))
											Catch : fieldValue = String.Empty : End Try
										End If
									Case t_date, t_time
										If fieldValue.Length > 0 AndAlso fieldFormats(j, 0).Length > 0 Then
											If IsDate(fieldValue) Then
												fieldValue = Format(CDate(fieldValue), fieldFormats(j, 0))
											Else
												fieldValue = String.Empty
											End If
										End If
									Case t_datetime
										Try
											If fieldFormats(j, 0).Length > 0 Then
												fieldValue = formatedDateTime(CDate(fieldValue))
											Else
												fieldValue = Format(CDate(fieldValue), fieldFormats(j, 0))
											End If
										Catch : fieldValue = String.Empty : End Try
									Case t_url
										If fieldValue.Length > 0 Then
											If fieldFormats(j, 0).Length > 0 Then
												fieldValue = HTMLLink(fieldValue, "", inNewBrowser, fieldFormats(j, 0))
											Else
												fieldValue = HTMLLink(fieldValue, fieldValue, inNewBrowser)
											End If
										End If
									Case t_pix
										If fieldValue.Length > 0 Then
											If fieldFormats(j, 0).Length > 0 Then
												If fieldValue.Length > 0 Then fieldValue = HTMLImg(_PathPix & fieldValue)
											Else
												fieldValue = HTMLImg(_PathPixToolbar & pix_check, LG_Checked)
											End If
										End If
								End Select
								myHTML.Append("<td>").Append(RandomFieldValue(fieldType, fieldFormats(j, 0), _PathPixToolbar)).Append("&nbsp;</td>")
							Next
							myHTML.Append("</tr>")
						Next
					End If
					myHTML.Append("</TBODY></table></span>")
					If ListMode = 2 And nbFieldEditable > 0 Then
						If _DBAllowInsertDetails Then
							myHTML.Append(HTMLLink("Javascript:evoAddRow()", LG_AddRow))
							If _DBAllowDelete Then
								myHTML.Append(HTML_Sep).Append(HTMLLink("Javascript:evoDelRow()", LG_DelRow))
							End If
						End If
						'hidden fields for serialized row values
						myHTML.Append("<span id=""evoROz""></span><input type=""hidden"" name=""evolNUD"" id=""evolNUD"">")
					End If
					'--- SUMMARY AND Paging navigation ---------------------------------
					If Not String.IsNullOrEmpty(def_Data.sppaging) Then	'And MaxLoopSQL = _RowsPerPage 
						If ListMode = 0 Then
							If atRunTime Then
								If (MaxLoopSQL = _RowsPerPage - 1) OrElse pageID > 1 Then
									myHTML.Append("</td></tr><tr><td colspan=""2"">")
									If pageID > 1 Then
										myHTML.Append(GetPagingNav())
									End If
									myHTML.Append(pageID)
									If MaxLoopSQL > 0 AndAlso pageID * _RowsPerPage <= TotalNbRecords Then
										For i = pageID + 1 To pageID + 5
											If (i - 1) * _RowsPerPage >= TotalNbRecords Then Exit For
											myHTML.Append(HTMLSpace).Append(HTMLLinkEventRef("n" & CStr(i), CStr(i)))
										Next
										myHTML.Append("&nbsp;&nbsp;")
										If pageID * _RowsPerPage < TotalNbRecords Then
											myHTML.Append(HTMLLinkEventRef("n" & CStr(pageID + 1), LG_pNext))
										End If
									End If
								Else
									If TotalNbRecords > 0 Then
										myHTML.AppendFormat("<font class=""FieldLabel"">{0}</font>", htmlRecCount)
									End If
								End If
							Else 'design mode
								myHTML.Append("</td></tr><tr><td colspan=""2"">&nbsp;1&nbsp;").Append(HTMLLink("#", "2")).Append(HTMLSpace).Append(HTMLLink("#", "3"))
								myHTML.Append("&nbsp;&nbsp;").Append(HTMLLink("#", LG_pNext))
							End If
						End If
					Else
						If TotalNbRecords > 0 Then myHTML.AppendFormat("<p>{0}</p>", htmlRecCount)
					End If
					If ListMode = 1 AndAlso LinkDetailNew <> String.Empty AndAlso Not _DBReadOnly Then
						myHTML.Append("<p>").Append(HTMLLink(LinkDetailNew.Replace(p_itemid, "0") & "&tdLOVE=1N", LG_NewItem)).Append("</p>")
					End If
				End If
				If InsidePanel Then myHTML.Append(TdTrTableEnd)
			Else
				myHTML.Append("<div class=""holder2"">")
				If ListMode = 0 Then
					If Not String.IsNullOrEmpty(title) Then
						myHTML.Append("<p>").Append(title).Append("</p>") 
					End If
					If String.IsNullOrEmpty(ErrorMsg) Then myHTML.Append("<p>").AppendFormat(LG_NoEntity, entity).Append("</p>")
					If _DBAllowSearch Then myHTML.Append(" <p>").Append(HTMLLinkEventRef("3", LG_NewSearch)).Append(HTML_Sep).Append(HTMLLinkEventRef("4", LG_AdvSearch)).Append("</p>")
				Else
					myHTML.Append("<p>").Append(LG_NoEntity).Append(".&nbsp;") 'item for sub panels
					If LinkDetailNew <> String.Empty AndAlso Not _DBReadOnly Then
						myHTML.Append(HTMLLink(LinkDetailNew.Replace(p_itemid, "0") & "&tdLOVE=1N", LG_NewItem))
					End If
					myHTML.Append("</p>")
				End If
				myHTML.Append("</div>")
			End If
			ds = Nothing
			Return myHTML.ToString
		End Function

		Private Function GetPagingNav() As StringBuilder
			Dim i As Integer, myHTML As New StringBuilder

			myHTML.Append(HTMLLinkEventRef("n" & CStr(pageID - 1), LG_pPrev)).Append(HTMLSpace)
			For i = pageID - 5 To pageID - 1
				If i > 0 Then
					myHTML.Append(HTMLLinkEventRef("n" & CStr(i), CStr(i))).Append(HTMLSpace)
				End If
			Next
			Return myHTML
		End Function

		Private Function HTMLrecordCount(ByRef TotalNbRecords As Integer, ByRef MaxLoopSQL As Integer) As String
			Dim i As Integer, htmlRecCount As String

			If (TotalNbRecords > MaxLoopSQL + 1) OrElse pageID > 1 Then
				i = ((pageID - 1) * _RowsPerPage) + 1
				If MaxLoopSQL = 0 Then
					htmlRecCount = String.Format("{0} {1}", TextUcaseFirst(entity), i)
				Else
					htmlRecCount = (New StringBuilder).Append(TextUcaseFirst(entities)).Append(" ").Append(i).Append(HTML_Sep).Append(i + MaxLoopSQL).ToString
				End If
				htmlRecCount += LG_sOf & CStr(TotalNbRecords)
			Else
				Select Case MaxLoopSQL
					Case -1 : htmlRecCount = String.Empty
					Case 0 : htmlRecCount = String.Format("1 {0}", entity)
					Case Else : htmlRecCount = String.Format("{0} {1}", TotalNbRecords, entities)
				End Select
			End If
			Return htmlRecCount
		End Function

		Private Function LOVfromCache(ByRef CacheKey As String, ByRef ItemIDs As String) As String
			Dim ds As DataSet, i As Integer, j As Integer, p As Integer, MaxLoop1 As Integer, MaxLoop2 As Integer, buffer As String = "", iItemID As Integer
			Dim LOVtuples() As String

			'If SQLTable = "" Then
			'    Try
			'        SQLTable = LCase(aNode.Attributes(Attr.lovEnumeration).Value)
			'    Catch
			'    End Try
			'    If SQLTable <> String.Empty Then
			'        myHTML.Append(HTMLlovEnum(SQLTable, FieldName, ItemID))
			'        SQLTable = ""
			'    End If
			'End If 
			'cache key = LCase(t_lov & dbtable & A(Attr.dbtablelov) & (Attr.dbcolumnreadlov) & (Attr.dbColumnImg))
			ds = CType(Page.Cache(CacheKey.ToLower), DataSet)
			'SHOULD DO BINARY SEARCH WHEN ORDERED LISTS ??)
			'IS THERE A seek OR find FUNCTION
			If ds Is Nothing Then
				buffer = ItemIDs
			Else
				With ds.Tables(0)
					MaxLoop1 = .Rows.Count - 1
					p = InStr(ItemIDs, coma)
					If p > 0 Then
						LOVtuples = Split(ItemIDs, coma)
						MaxLoop2 = LOVtuples.Length - 1
						If MaxLoop2 > 4 Then
							MaxLoop2 = 4
						Else
							p = 0
						End If
						For j = 0 To MaxLoop2
							iItemID = String2Int(LOVtuples(j))
							If iItemID > 0 Then
								For i = 0 To MaxLoop1
									If CInt(.Rows(i).Item(0)).Equals(iItemID) Then
										buffer = CondiConcat(buffer, CStr(.Rows(i).Item(1)))
										Exit For
									End If
								Next
							End If
						Next
					Else
						iItemID = String2Int(ItemIDs)
						If iItemID > 0 Then
							For i = 0 To MaxLoop1
								If CInt(.Rows(i).Item(0)).Equals(iItemID) Then
									buffer = CStr(.Rows(i).Item(1))
									Exit For
								End If
							Next
						End If
					End If
				End With
				ds = Nothing
				If p > 0 Then buffer += "..."
			End If
			Return buffer
		End Function

		Private Function HTMLLinkEventRef(ByRef EventParam As String, ByRef Title As String, Optional ByVal pix As String = "") As String
			If pix.Length > 0 Then
				Return String.Format("<a href=""javascript:EvPost('{0}')"" class=""{2}"">{1}</a>", EventParam, Title, pix)
			Else
				Return String.Format("<a href=""javascript:EvPost('{0}')"">{1}</a>", EventParam, Title)
			End If
		End Function

		Private Function HTMLPanelFields(ByRef aPanelNode As XmlNode, ByRef gDisplayMode As Integer, ByRef panelID As String, ByRef withData As Boolean, Optional ByVal PanelPosition As Integer = -1) As String
			'build the html for 1 panel (in 'edit' or 'view' modes)
			Const RowID As Integer = 0
			Dim sql As String, stylePanel As String, buffer As String, buffer1 As String, buffer2 As String, target As String, dbcolumnAttribute As String = EvoXMLInfo.dbColumn
			Dim linewidth As Integer, inTable As Boolean, fWidth As Integer, fHeight As Integer, fMaxLength As Integer
			Dim fieldName As String, fieldValue As String, fieldMustShow As Boolean, fieldFormat As String, fieldReadOnly As Integer, fType As String, fieldLabel As String, fieldLabel2 As String, nbFileUploads As Integer
			Dim thisFieldStyle As String
			Dim maxLoopXML As Integer, i As Integer, fid As Integer
			Dim myResult As New StringBuilder
			Dim htmlField As New StringBuilder
			Dim hiddenFields As New StringBuilder

			If aPanelNode.Attributes(EvoXMLInfo.CSSclass) Is Nothing Then
				stylePanel = "Panel"
			Else
				stylePanel = aPanelNode.Attributes(EvoXMLInfo.CSSclass).Value
				If stylePanel.Equals("") Then stylePanel = "Panel"
			End If
			myResult.Append("<table class=""").Append(stylePanel).Append(""" width=""100%"" cellpadding=""3"" cellspacing=""0"" border=""0""><tr><td>")
			fieldName = aPanelNode.Attributes(EvoXMLInfo.label).Value
			If _ShowDesigner Then
				fieldName += LinkDesigner("PD", _FormID, fieldName)				
			End If
			If aPanelNode.Attributes(EvoXMLInfo.CSSclasslabel) Is Nothing Then
				stylePanel = "PanelLabel"
			Else
				stylePanel = aPanelNode.Attributes(EvoXMLInfo.CSSclasslabel).Value
				If String.IsNullOrEmpty(stylePanel) Then stylePanel = "PanelLabel"
			End If
			myResult.Append(HTMLPanelLabel(fieldName, "evoPnl" & panelID, stylePanel))
			inTable = False
			linewidth = 0
			myResult.AppendFormat("<span ID=""evoPnl{0}"">", panelID)
			maxLoopXML = aPanelNode.ChildNodes.Count - 1
			If gDisplayMode = 0 Then 'view 
				dbcolumnAttribute += "read"
				fieldReadOnly = 1
			Else
				fieldReadOnly = 0
			End If
			For i = 0 To maxLoopXML	'for each FIELD
				If aPanelNode.ChildNodes(i).NodeType = XmlNodeType.Element Then
					htmlField = New StringBuilder
					With aPanelNode.ChildNodes(i)
						fType = .Attributes(EvoXMLInfo.type).Value
						sql = .Attributes(dbcolumnAttribute).Value
						fieldName = UID & sql
						If fType <> "hidden" Then
							fWidth = CInt(.Attributes(EvoXMLInfo.Width).Value)
							htmlField.AppendFormat("<td width=""{0}%"">", fWidth)
							fieldlabel = .Attributes(EvoXMLInfo.label).Value
							'##### - edit mode
							If gDisplayMode > 0 Then
								If fType.Equals(t_formula) Then
									fieldReadOnly = 1
								Else
									If .Attributes(EvoXMLInfo.dbReadOnly) Is Nothing Then
										fieldReadOnly = 0
									Else
										If IsNumeric(.Attributes(EvoXMLInfo.dbReadOnly).Value) Then
											fieldReadOnly = CInt(.Attributes(EvoXMLInfo.dbReadOnly).Value)
											If _ItemID < 1 Then	'INSERT
												If fieldReadOnly = 2 Then fieldReadOnly = 0
											End If
										End If
									End If
								End If
								If String.IsNullOrEmpty(fieldlabel) AndAlso Not .Attributes(EvoXMLInfo.labelEdit) Is Nothing Then
									fieldlabel = .Attributes(EvoXMLInfo.labelEdit).Value
								End If
								fieldLabel2 = fieldLabel
								If fieldReadOnly.Equals(0) AndAlso Not .Attributes(EvoXMLInfo.required) Is Nothing Then
									If .Attributes(EvoXMLInfo.required).Value = "1" Then fieldlabel += "<span class=""required"">*</span>"
								End If 
								If fType.Equals(t_lov) Then
									If fieldReadOnly > 0 Then
										If .Attributes(EvoXMLInfo.dbColumnRead) Is Nothing Then
											sql = .Attributes(EvoXMLInfo.dbColumn).Value
										Else
											sql = .Attributes(EvoXMLInfo.dbColumnRead).Value
										End If
									Else
										sql = .Attributes(EvoXMLInfo.dbColumn).Value
									End If
								ElseIf fType.Equals(t_formula) Then
									sql = .Attributes(EvoXMLInfo.dbColumnRead).Value
								Else
									sql = .Attributes(dbcolumnAttribute).Value
								End If
							End If
							'##### - all modes
							If _ShowDesigner Then
								fid = CInt(.Attributes("id").Value)
								'fieldlabel = String.Format("<span ondblclick=""javascript:EvoDico.editLabel(this,{0})"">{1}</span>{2}", fid, fieldlabel, LinkDesigner("FD", fid, fieldLabel2))
								fieldLabel += LinkDesigner("FD", fid, fieldLabel2)
							End If
							'If _Debug Then fieldlabel += HTMLItalics(sql, HTML_Sep)
							If Not atRunTime Then
								fieldValue = RandomFieldValue(fType, String.Empty, _PathPixToolbar)
							ElseIf withData Then
								If sql <> String.Empty Then
									Try
										If ds.Tables(0).Rows(RowID)(sql) Is DBNull.Value Then
											fieldValue = String.Empty
										Else
											fieldValue = CStr(ds.Tables(0).Rows(RowID)(sql)).Trim
										End If
									Catch ex As Exception
										fieldValue = String.Empty
										AddError(ex.Message)
									End Try
								Else
									If fType.Equals(EvoXMLInfo.url) AndAlso Not .Attributes(EvoXMLInfo.link) Is Nothing Then
										fieldValue = .Attributes(EvoXMLInfo.link).Value
										If String.IsNullOrEmpty(sql) Then sql = " "
									Else
										fieldValue = String.Empty
									End If
								End If
							Else
								fieldValue = String.Empty
							End If
							fieldMustShow = True
							If (Not .Attributes("optional") Is Nothing) AndAlso String.IsNullOrEmpty(fieldValue) AndAlso gDisplayMode = 0 AndAlso fType <> t_lov Then
								fieldMustShow = Not .Attributes("optional").Value.Equals("1")
							End If
							'If (fieldMustShow OrElse gDisplayMode = 1) And fieldlabel.Length > 0 Then
							'	htmlField.Append("<font class=""FieldLabel"">").Append(fieldlabel)
							'	If fieldReadOnly = 0 AndAlso fType = t_txtm Then
							'		htmlField.AppendFormat("<a href=""javascript:Evol.sizeML('{0}',1)"" class=""ico MLbigger""></a><a href=""javascript:Evol.sizeML('{0}',-1)"" class=""ico MLsmaller""></a>", fieldName)
							'	End If
							'	htmlField.Append("</font><br/>")
							'End If  
							If (fieldMustShow OrElse gDisplayMode = 1) And fieldlabel.Length > 0 Then
								htmlField.Append("<span class=""FieldLabel""") 
								If fieldReadOnly = 0 AndAlso fType = t_txtm AndAlso Not _ShowDesigner Then
									htmlField.Append(" onmouseover=""javascript:Evol.showResize('").Append(fieldName).Append("',-1,this)"">")
								Else
									htmlField.Append(">")
								End If
								htmlField.Append(fieldlabel)
								htmlField.Append("</span>")
								'If fieldReadOnly = 0 AndAlso fType = t_txtm Then
								'	If Not _ShowDesigner Then
								'		buffer = " style=""display:none"""
								'	Else
								'		buffer = ""
								'	End If
								'	htmlField.AppendFormat("<span id=""gr{0}"" {1}></span>", fieldName, buffer)
								'	'htmlField.AppendFormat("<span id=""gr{0}"" {1}><a href=""javascript:Evol.sizeML('{0}',1)"" class=""ico MLbigger""></a><a href=""javascript:Evol.sizeML('{0}',-1)"" class=""ico MLsmaller""></a></span>", fieldName, buffer)
								'End If
								htmlField.Append("<br/>")
							End If
							If fieldReadOnly > 0 Then '  If gDisplayMode = 0 orelse fieldReadOnly Then
								'##### - VIEW mode ######################################################################3
								If fieldMustShow Then
									If Not fType.Equals(t_pix) Then
										If .Attributes(EvoXMLInfo.CSSclass) Is Nothing Then
											thisFieldStyle = String.Empty
										Else
											thisFieldStyle = .Attributes(EvoXMLInfo.CSSclass).Value
										End If
										If thisFieldStyle.Equals(String.Empty) Then
											htmlField.Append("<div class=""FieldReadOnly"">")
										Else
											htmlField.AppendFormat("<div class=""{0}"">", thisFieldStyle)
										End If
									End If
									If Not String.IsNullOrEmpty(sql) Then
										Select Case fType
											Case t_text
												If PanelPosition = 0 AndAlso i = 0 AndAlso icon <> String.Empty Then htmlField.Append(icon)
												htmlField.Append(Text2HTMLwBR(fieldValue))
											Case t_lov
												buffer1 = String.Empty : buffer2 = String.Empty
												If String.IsNullOrEmpty(def_Data.dblockingcolumn) Then
													If Not .Attributes(EvoXMLInfo.lovMany) Is Nothing Then buffer2 = .Attributes(EvoXMLInfo.lovMany).Value
												Else
													If withData AndAlso def_Data.dblockingcolumn = .Attributes(EvoXMLInfo.dbColumn).Value Then
														dbwherelock = def_Data.dblockingcolumn & "=" & CStr(ds.Tables(0).Rows(RowID)(def_Data.dblockingcolumn))
														'  dbLockinglabel = "????"
														Page.Cache("locking") = dbwherelock
													End If
												End If
												If buffer2 <> String.Empty Then
													If .Attributes(EvoXMLInfo.link) Is Nothing Then buffer2 = String.Empty Else buffer2 = .Attributes(EvoXMLInfo.link).Value
													htmlField.Append(HTMLlovMany(aPanelNode.ChildNodes(i), 0, buffer2))	'CInt(ds.Tables(0).Rows(RowID)(.Attributes(Attr.dbColumn).Value)), fieldFormat))
												Else
													If Not String.IsNullOrEmpty(fieldValue) Then
														If .Attributes(EvoXMLInfo.dbColumnImg) Is Nothing Then
															buffer1 = String.Empty
														Else
															buffer1 = .Attributes(EvoXMLInfo.dbColumnImg).Value
															Try
																If Not String.IsNullOrEmpty(buffer1) Then buffer1 = CStr(ds.Tables(0).Rows(RowID)(buffer1))
															Catch : buffer1 = String.Empty : End Try
														End If
														If buffer1.Length > 0 Then buffer1 = HTMLImg(_PathPix & buffer1) & HTMLSpace
														If .Attributes(EvoXMLInfo.link) Is Nothing Then
															fieldFormat = String.Empty
														Else
															fieldFormat = .Attributes(EvoXMLInfo.link).Value
														End If
														fieldValue = Text2HTML(fieldValue)
														If fieldFormat.Length > 0 Then
															If .Attributes(EvoXMLInfo.linkTarget) Is Nothing Then
																target = String.Empty
															Else
																target = .Attributes(EvoXMLInfo.linkTarget).Value
															End If
															fieldFormat = fieldFormat.Replace(p_itemid, CStr(ds.Tables(0).Rows(RowID)(.Attributes(EvoXMLInfo.dbColumn).Value)))
															If .Attributes(EvoXMLInfo.linkLabel) Is Nothing Then
																buffer2 = fieldValue
															Else
																buffer2 = .Attributes(EvoXMLInfo.linkLabel).Value
																buffer2 = Text2HTML(buffer2.Replace("@fieldvalue", fieldValue))	   'CStr(ds.Tables(0).Rows(RowID)(.Attributes(Attr.dbColumnRead).Value))
															End If
															fieldValue = HTMLLink(fieldFormat, buffer2, target)		'fieldValue
														End If
														htmlField.Append(buffer1).Append(fieldValue)
													End If
												End If
											Case t_dec, t_int, t_formula
												If .Attributes(EvoXMLInfo.Format) Is Nothing Then
													fieldFormat = String.Empty
												Else
													fieldFormat = .Attributes(EvoXMLInfo.Format).Value
												End If
												If String.IsNullOrEmpty(fieldFormat) Then
													htmlField.Append(fieldValue)
												Else
													Try
														buffer2 = Format(CDec(fieldValue), fieldFormat)
													Catch : buffer2 = fieldValue : End Try
													If buffer2.Length > 0 Then htmlField.Append(noBR_tag).Append(buffer2).Append(noBR_tagClose)
												End If
											Case t_txtm
												If .Attributes(EvoXMLInfo.Height) Is Nothing Then fHeight = 3 Else fHeight = String2Int(.Attributes(EvoXMLInfo.Height).Value)
												If Not String.IsNullOrEmpty(fieldValue) Then
													fieldValue = Text2HTMLwBR(fieldValue)
												End If
												htmlField.Append("<span style=""height:").Append(fHeight * 20).Append(""">").Append(fieldValue).Append("<br/></span>")
											Case t_bool
												If fieldValue.Equals("1") OrElse fieldValue.Equals("True") Then
													If .Attributes(EvoXMLInfo.Img) Is Nothing Then
														fieldFormat = pix_check
													Else
														fieldFormat = .Attributes(EvoXMLInfo.Img).Value
														If String.IsNullOrEmpty(fieldFormat) Then fieldFormat = pix_check
													End If
													htmlField.Append(HTMLSpace).Append(HTMLImg(_PathPixToolbar & fieldFormat, LG_Checked))
												End If
											Case t_date, t_datetime, t_time
												If IsDate(fieldValue) Then
													If .Attributes(EvoXMLInfo.Format) Is Nothing Then
														fieldFormat = String.Empty
													Else
														fieldFormat = .Attributes(EvoXMLInfo.Format).Value
													End If
													If String.IsNullOrEmpty(fieldFormat) Then
														fieldFormat = DefaultDateFormat(fType, _Language)
													End If
													htmlField.Append(HTMLDateFormated(fType, fieldValue, fieldFormat))
												End If
											Case t_email
												If fieldValue <> String.Empty Then htmlField.Append(HTMLEmail(fieldValue))
											Case t_url
												If fieldValue <> String.Empty Then
													If .Attributes(EvoXMLInfo.linkLabel) Is Nothing Then
														fieldFormat = String.Empty
													Else
														fieldFormat = .Attributes(EvoXMLInfo.linkLabel).Value.Trim
													End If
													If sql = " " Then
														If InStr(fieldValue, "@item") > 0 Then
															fieldValue = fieldValue.Replace(p_itemid, CStr(_ItemID))
														End If
														If String.IsNullOrEmpty(fieldFormat) Then fieldFormat = fieldValue
														fieldValue = HTMLLink(fieldValue, fieldFormat)
													Else
														If fieldFormat.Length > 0 Then
															fieldValue = HTMLLink(fieldValue, fieldFormat, inNewBrowser)
														Else
															If .Attributes(EvoXMLInfo.Img) Is Nothing Then
																fieldFormat = String.Empty
															Else
																fieldFormat = .Attributes(EvoXMLInfo.Img).Value.Trim
															End If
															If fieldFormat.Length = 0 Then
																Try
																	fieldFormat = .Attributes(EvoXMLInfo.dbColumnImg).Value.Trim
																	If fieldFormat <> String.Empty Then fieldFormat = CStr(ds.Tables(0).Rows(RowID)(fieldFormat))
																Catch : End Try
															End If
															If fieldFormat <> String.Empty Then
																fieldValue = HTMLLink(fieldValue, String.Empty, inNewBrowser, _PathPix & fieldFormat)
															Else
																fieldValue = HTMLLink(fieldValue, fieldValue, inNewBrowser)
															End If
														End If
													End If
													htmlField.Append(fieldValue)
												End If
											Case t_pix
												If String.IsNullOrEmpty(fieldValue) Then
													htmlField.Append(LG_NA)
												Else
													If .Attributes(EvoXMLInfo.link) Is Nothing Then
														fieldFormat = String.Empty
													Else
														fieldFormat = .Attributes(EvoXMLInfo.link).Value
														If fieldFormat <> String.Empty Then
															fieldFormat = fieldFormat.Replace(p_itemid, CStr(ds.Tables(0).Rows(RowID)("ID")))
															htmlField.Append("<a href=""").Append(fieldFormat)
															If Not .Attributes(EvoXMLInfo.linkTarget) Is Nothing Then
																target = .Attributes(EvoXMLInfo.linkTarget).Value
																If target.Length > 0 Then htmlField.Append(""" target=""").Append(target)
															End If
															htmlField.Append(""">")
														End If
													End If
													If Not .Attributes(EvoXMLInfo.CSSclass) Is Nothing Then
														buffer = .Attributes(EvoXMLInfo.CSSclass).Value
													Else
														buffer = "fieldImg"
													End If
													htmlField.Append("<img border=""0"" src=""").Append(_PathPix).Append(fieldValue).AppendFormat(""" class=""{0}"" />", buffer)
													If fieldFormat <> String.Empty Then htmlField.Append("</a>")
												End If
											Case t_doc
												If String.IsNullOrEmpty(fieldValue) Then
													htmlField.Append(LG_NA)
												Else
													If .Attributes(EvoXMLInfo.dbColumnImg) Is Nothing Then
														fieldFormat = String.Empty
													Else
														fieldFormat = .Attributes(EvoXMLInfo.dbColumnImg).Value.Trim
														Try
															If fieldFormat <> String.Empty Then fieldFormat = CStr(ds.Tables(0).Rows(RowID)(fieldFormat))
														Catch : fieldFormat = String.Empty : End Try
														If fieldFormat <> String.Empty Then
															htmlField.Append(HTMLLink(_PathPix & fieldValue, "", inNewBrowser, _PathPix & fieldFormat)).Append(BR_tag)
														End If
													End If
													htmlField.Append(HTMLLink(_PathPix & fieldValue, fieldValue, inNewBrowser))
												End If
											Case t_html
												If fieldValue <> String.Empty Then htmlField.Append(fieldValue.Replace(vbCrLf, BR_tag))
												'  Case t_html
												'  htmlField.Append("<span style=""height:").Append(fHeight * 12).Append(""">").Append(fieldValue.Replace(vbCrLf, BR_tag)).Append("<br/></span>")
											Case Else
												htmlField.Append(fieldValue)
										End Select
									End If
									If Not fType.Equals(t_pix) Then htmlField.Append("&nbsp;</div>")
								End If
							Else
								'##### - EDIT mode ######################################################################
								If .Attributes(EvoXMLInfo.MaxLength) Is Nothing Then
									fMaxLength = 0
								Else
									fMaxLength = CInt(.Attributes(EvoXMLInfo.MaxLength).Value)
								End If
								Select Case fType
									Case t_text, t_email, t_url, t_int, t_dec
										htmlField.Append("<input type=""text"" class=""Field"" name=""").AppendFormat("{0}"" id=""{0}", fieldName)
										If fMaxLength > 0 Then htmlField.Append(""" maxlength=""").Append(fMaxLength)
										If fType.Equals(t_int) OrElse fType.Equals(t_dec) Then
											htmlField.Append(""" OnKeyUp=""Evol.checkNum(this,'").Append(fType.Substring(0, 1)).Append("')")
										End If
										htmlField.Append(""" value=""").Append(HtmlEncode(fieldValue)).Append(""">")
									Case t_txtm, t_html
										If .Attributes(EvoXMLInfo.Height) Is Nothing Then fHeight = 3 Else fHeight = String2Int(.Attributes(EvoXMLInfo.Height).Value)
										htmlField.Append("<textarea class=""Field"" style=""height:").Append(fHeight * 20).Append(""" rows=""").Append(.Attributes(EvoXMLInfo.Height).Value)
										If fMaxLength > 0 Then htmlField.Append(""" onKeyUp=""Evol.checkMaxLen(this,").Append(fMaxLength).Append(")")
										htmlField.AppendFormat(""" name=""{0}"" id=""{0}", fieldName).Append(""" cols=""52"">").Append(HtmlEncode(fieldValue)).Append("</textarea>")
									Case t_lov
										If atRunTime Then
											'  edit lookup for lov
											If .Attributes("lookup") Is Nothing Then
												buffer = String.Empty
											Else
												buffer = .Attributes("lookup").Value
											End If
											If buffer.Length > 0 Then
												fieldValue = Page.Request(fieldName)	 'fieldName=MAtoid
												If Val(fieldValue) > 0 Then
													lockDbname = buffer
													loclValue = fieldName
													'If dbLockingColumn <> String.Empty Then
													'    If UID & sql = dbLockingColumn Then

													'    End If
													'End If
													htmlField.Append(HTMLInputHidden(fieldName, fieldValue))
													htmlField.Append("<div class=""FieldReadOnly"">").Append(HTMLlov(aPanelNode.ChildNodes(i), , fieldValue, CInt(fieldValue))).Append("</div>")
												Else
													buffer = String.Empty
												End If
												'buffer = ""
												'Try
												'    buffer = .Attributes(Attr.dbcolumnreadlov).Value
												'Catch
												'End Try 
												'buffer = LOVfromCache(t_lov & dbtable & buffer, "5")
												'htmlField.Append(buffer)
												'   cache(key = LCase(t_lov & dbtable & A(Attr.dbtablelov) & (Attr.dbcolumnreadlov) & (Attr.dbColumnImg)))
											Else
												htmlField.Append("<select class=""Field"" name=""").AppendFormat("{0}"" id=""{0}"">", fieldName)
												If withData Then
													If .Attributes(EvoXMLInfo.lovMany) Is Nothing Then
														buffer1 = String.Empty
													Else
														buffer1 = .Attributes(EvoXMLInfo.lovMany).Value
													End If
													If String.IsNullOrEmpty(buffer1) Then
														htmlField.Append("<option value=""0""> - </option>")
														htmlField.Append(HTMLlov(aPanelNode.ChildNodes(i), , fieldValue))
													Else
														htmlField.Append(HTMLlovMany(aPanelNode.ChildNodes(i), CInt(ds.Tables(0).Rows(RowID)(.Attributes(EvoXMLInfo.dbColumn).Value)), fieldFormat))
													End If
												Else
													htmlField.Append("<option value=""0""> - </option>")
													buffer = "0"
													If _ItemID < 1 AndAlso Not .Attributes("defaultvalue") Is Nothing Then
														buffer = CStr(Val(.Attributes("defaultvalue").Value))
													End If
													htmlField.Append(HTMLlov(aPanelNode.ChildNodes(i), , buffer))
												End If
												htmlField.Append("</option></select>")
											End If
										Else
											htmlField.Append("<select class=""Field"">").Append(HTMLOption(String.Empty, HtmlEncode(fieldValue))).Append("</option></select>")
										End If
										'edit LOV
										'maybe link to other window, maybe javascript only one item?
										'    htmlField.Append(HTMLLinkEventRef("1212", "<small>[Edit]</small>"))
									Case t_bool
										htmlField.Append("<input type=""checkbox"" name=""").AppendFormat("{0}"" id=""{0}", fieldName).Append(""" value=""1""")
										If fieldValue.Equals("True") OrElse fieldValue.Equals("1") Then
											htmlField.Append(" checked=""checked"">")
											fieldValue = "1" 'for caching
										Else
											htmlField.Append(">")
											fieldValue = String.Empty
										End If
									Case t_date, t_datetime, t_time
										If IsDate(fieldValue) Then
											If .Attributes(EvoXMLInfo.Format) Is Nothing Then
												fieldFormat = String.Empty
											Else
												fieldFormat = .Attributes(EvoXMLInfo.Format).Value
											End If
											If String.IsNullOrEmpty(fieldFormat) Then
												fieldFormat = DefaultDateFormat(fType, _Language)
											End If
											fieldValue = HTMLDateFormated(fType, fieldValue, fieldFormat)
										End If
										htmlField.Append(HTMLInputDate(fieldName, fieldValue, _Language, _PathPixToolbar))
									Case t_pix, t_doc
										htmlField.Append(SMALL_tag)
										If fieldValue <> String.Empty Then htmlField.Append("<span class=""FieldReadOnly"">").Append(fieldValue).Append("</span><br/>")
										If fType.Equals(t_pix) Then
											htmlField.Append("<br/><img src=""")
											If String.IsNullOrEmpty(fieldValue) Then
												htmlField.Append(_PathPixToolbar).Append("imgno.gif"" ID=""")
											Else
												htmlField.Append(_PathPix).Append(fieldValue).Append(""" ID=""")
											End If
											htmlField.Append(fieldName).Append("img"" alt="""" class=""fieldImg"" ><br/>")
										End If
										buffer = String.Format("UP-evol{0}", i)
										htmlField.Append(HTMLInputHidden(fieldName & "_dp", String.Empty))
										If IEbrowser Then
											htmlField.Append(HTMLLinkShowVanish(buffer, LG_NewUpload))
										End If
										If fieldValue <> String.Empty Then
											htmlField.Append("<br/>&nbsp;<a href=""Javascript:Evol.")
											If fType.Equals(t_pix) Then	'pix
												htmlField.Append("pixM('")
											Else 'doc
												htmlField.Append("docM('")
											End If
											htmlField.Append(fieldName).Append("')"">").Append(LG_Delete).Append("</a>")
										End If
										htmlField.Append("</small><br/>").Append(HTMLDiv(buffer, Not IEbrowser))
										htmlField.Append("<input type=""file"" class=""Field"" name=""").AppendFormat("{0}"" id=""{0}", fieldName)
										htmlField.Append(""" value=""").Append(HtmlEncode(fieldValue)).Append(""" width=""120"" onchange=""$('").Append(fieldName)
										If fType.Equals(t_pix) Then
											htmlField.Append("img').src='").Append(_PathPixToolbar).Append("imgupdate.gif';$('").Append(fieldName)
										End If
										htmlField.Append("_dp').value=''""><br/></div>")
										nbFileUploads += 1
									Case Else
										htmlField.Append("<input type=""text"" name=""").AppendFormat("{0}"" id=""{0}"">", fieldName).Append(""" value=""").Append(HtmlEncode(fieldValue)).Append(""" class=""Field"">")
								End Select
								If _DBAllowHelp AndAlso Not .Attributes("help") Is Nothing Then
									buffer = .Attributes("help").Value
									If buffer.Length > 0 Then
										htmlField.Append("<br/><span class=""helptip"">").Append(buffer).Append("</span>")
									End If
								End If
								If atRunTime AndAlso withData Then '(mode=edit already checked)
									If Not String.IsNullOrEmpty(fieldValue) Then htmlField.Append(HTMLInputHidden(fieldName & "_ov", Page.Server.UrlEncode(fieldValue)))
								End If
							End If
							htmlField.Append("</td>")
						Else
							hiddenFields.Append(HTMLInputHidden(fieldName, fieldValue))
						End If
					End With
					Select Case linewidth + fWidth
						Case 100
							If fWidth < 100 Then
								myResult.Append(htmlField).Append(trTableEnd)
								inTable = False
							Else
								myResult.Append(tableBeginTr).Append(htmlField).Append(trTableEnd)
							End If
							linewidth = 0
						Case Is > 100
							If inTable Then myResult.Append(trTableEnd)
							myResult.Append(tableBeginTr).Append(htmlField)
							inTable = True
							linewidth = fWidth
						Case Else
							If linewidth = 0 Then
								If inTable Then myResult.Append(trTableEnd)
								myResult.Append(tableBeginTr)
								inTable = True
							End If
							myResult.Append(htmlField)
							linewidth += fWidth
					End Select
				End If
			Next
			If i > 0 AndAlso inTable Then myResult.Append(trTableEnd)
			myResult.Append("</span>")
			If hiddenFields.Length > 0 Then myResult.Append(hiddenFields)
			myResult.Append(TdTrTableEnd)
			Return myResult.ToString
		End Function

		Private Function HTMLPanelLabel(ByRef PanelLabel As String, ByRef panelID As String, ByRef panelClassName As String) As String
			If String.IsNullOrEmpty(PanelLabel) Then
				Return String.Empty
			Else
				Dim HTML As New StringBuilder
				HTML.Append("<div class=""").Append(panelClassName).Append(""">&nbsp;").Append(PanelLabel)
				If _CollapsiblePanels Then
					HTML.AppendFormat("<span class=""navp""><a class=""ico panelclose"" href=""javascript:Evol.togglePanel('{0}',-1)"" ID=""{0}link""></a></span>", panelID)
				End If
				HTML.Append("</div>")
				Return HTML.ToString()
			End If
		End Function

		Private Function HTMLlov(ByVal aNode As XmlNode, Optional ByVal FieldName As String = "", Optional ByVal ItemID As String = "0", Optional ByVal Lookup As Integer = 0) As String
			'make a query and returns the HTML for a lov 
			Dim i As Integer, i2 As Integer, i3 As Integer, myID As Integer, pixname As String
			Dim myHTML As New StringBuilder
			Dim Source As DataSet, sql As String, curID As Integer, MaxLoop As Integer
			Dim cacheKey As String = String.Empty, wBR As Boolean = False, DynamicItemID As Boolean = False
			Dim SQLTable As String = String.Empty, SQLColumn As String, SQLColumnImg As String, SQLwhere As String = String.Empty, SQLOrderBy As String

			'cache key = LCase(t_lov & dbtable & A(Attr.dbtablelov) & (Attr.dbcolumnreadlov) & (Attr.dbColumnImg))

			If Not aNode.Attributes(EvoXMLInfo.dbtablelov) Is Nothing Then
				SQLTable = aNode.Attributes(EvoXMLInfo.dbtablelov).Value
			End If
			If String.IsNullOrEmpty(SQLTable) AndAlso Not aNode.Attributes(EvoXMLInfo.lovEnumeration) Is Nothing Then
				SQLTable = aNode.Attributes(EvoXMLInfo.lovEnumeration).Value
				If SQLTable.Length > 0 Then
					myHTML.Append(HTMLlovEnum(SQLTable, FieldName, CInt(ItemID)))
					SQLTable = String.Empty
				End If
			End If
			'If Left(SQLTable, 5) <> "list:" Then
			If atRunTime AndAlso SQLTable.Length > 0 Then
				If aNode.Attributes(EvoXMLInfo.dbcolumnreadlov) Is Nothing Then
					SQLColumn = String.Empty
				Else
					SQLColumn = aNode.Attributes(EvoXMLInfo.dbcolumnreadlov).Value
				End If
				If SQLColumn.Length = 0 Then SQLColumn = EvoXMLInfo.dbname
				If aNode.Attributes(EvoXMLInfo.dbColumnImg) Is Nothing Then
					SQLColumnImg = String.Empty
				Else
					SQLColumnImg = aNode.Attributes(EvoXMLInfo.dbColumnImg).Value
				End If
				If Not aNode.Attributes(EvoXMLInfo.dbwherelov) Is Nothing Then
					SQLwhere = aNode.Attributes(EvoXMLInfo.dbwherelov).Value
					If SQLwhere.Length > 0 Then
						If InStr(SQLwhere, p_itemid) > 0 Then
							DynamicItemID = True
							If _ItemID < 1 Then
								'If lockDbname <> String.Empty Then
								'    If aNode.Attributes("dbcolumn").Value = lockDbname Then
								'        SQLwhere = SQLwhere.Replace(itemid_str, CStr(_ItemID))
								'    End If
								'End If
								SQLColumnImg = loclValue
							End If
						End If
						SQLwhere = SQLwhere.Replace(p_itemid, CStr(_ItemID))
						If _SecurityModel <> EvolSecurityModel.Single_User Then
							SQLwhere = SQLwhere.Replace(p_userid, CStr(_UserID))
						End If
					End If
				End If
				If Lookup = 0 AndAlso _UseCache AndAlso Not DynamicItemID Then
					'bug - need to add 
					cacheKey = (New StringBuilder).Append(t_lov).Append(SQLTable).Append(SQLColumn).Append(SQLColumnImg).Append(SQLwhere).ToString.ToLower
					Source = CType(Page.Cache(cacheKey), DataSet)
				End If
				'fieldName = UID & aNode.Attributes(Attr.dbColumn).Value 'now para
				If Source Is Nothing Then
					If String.IsNullOrEmpty(ErrorMsg) Then
						'If required Then sql += "ID>0" Else sql = ""
						sql = String.Format("TOP 1000 ID,rtrim({0}) AS value", SQLColumn)
						If SQLColumnImg <> String.Empty Then sql += ",[pix]" ' ",[" & SQLColumnImg & "]"
						If Lookup > 0 Then SQLwhere = String.Format("ID={0}", Lookup)
						If aNode.Attributes("dborderlov") Is Nothing Then
							SQLOrderBy = "value"
						Else
							SQLOrderBy = aNode.Attributes("dborderlov").Value
							If String.IsNullOrEmpty(SQLOrderBy) Then SQLOrderBy = "value"
						End If
						Source = GetData(BuildSQL(sql, SQLTable, SQLwhere, SQLOrderBy), _SqlConnection)
						If Source Is Nothing Then
							SQLwhere = String.Empty
							Lookup = 0
							sql = BuildSQL(sql, SQLTable, SQLwhere, SQLOrderBy)
							Source = GetData(sql, _SqlConnection)
						End If
						If _UseCache AndAlso Lookup < 1 AndAlso Not DynamicItemID Then
							If Not Source Is Nothing Then Page.Cache(cacheKey) = Source
						End If
					End If
				End If
				If Source Is Nothing Then
					AddError(String.Format(" No data in table {0}.", SQLTable))
					If FieldName <> String.Empty Then myHTML.Append(LG_NoData)
				Else
					With Source.Tables(0)
						MaxLoop = .Rows.Count - 1
						If FieldName <> String.Empty Then
							myHTML.Append("<table border=""0""><tr valign=""top""><td>")
							If MaxLoop > 3 Then
								i2 = (MaxLoop + 3) \ 3
								i3 = 2 * i2
								wBR = True
							Else
								i2 = -1
								i3 = -1
								wBR = False
							End If
							If SQLColumnImg <> String.Empty Then
								For i = 0 To MaxLoop
									If i = i2 OrElse i = i3 Then myHTML.Append("</td><td>&nbsp;&nbsp;&nbsp;</td><td>")
									If .Rows(i).Item("pix") Is DBNull.Value Then
										myHTML.Append(HTMLInputCheckBox(FieldName, CStr(.Rows(i).Item(0)), CStr(.Rows(i).Item(1)), "", , , FieldName & CStr(i)))
									Else
										pixname = CStr(.Rows(i).Item(2))
										If pixname <> String.Empty Then
											pixname = HTMLImg(_PathPix & pixname) & HTMLSpace
										End If
										myHTML.Append(HTMLInputCheckBox(FieldName, CStr(.Rows(i).Item(0)), pixname & CStr(.Rows(i).Item(1)), "", , , FieldName & CStr(i)))
									End If
									If wBR Then myHTML.Append(BR_tag)
								Next
							Else
								If wBR Then
									For i = 0 To MaxLoop
										If i = i2 OrElse i = i3 Then myHTML.Append("</td><td>")
										myHTML.Append(HTMLInputCheckBox(FieldName, CStr(.Rows(i).Item(0)), CStr(.Rows(i).Item(1)), "", , , FieldName & CStr(i))).Append(BR_tag)
									Next
								Else
									myHTML.Append("<nobr>")
									For i = 0 To MaxLoop
										If i = i2 OrElse i = i3 Then myHTML.Append("</td><td>")
										myHTML.Append(HTMLInputCheckBox(FieldName, CStr(.Rows(i).Item(0)), CStr(.Rows(i).Item(1)), "", , , FieldName & CStr(i))).Append("</nobr> <nobr>")
									Next
									myHTML.Append("</nobr>")
								End If
							End If
							If MaxLoop > 999 Then myHTML.Append(" (1000 items maximum)")
							myHTML.Append(TdTrTableEnd)
						Else
							'DROPDOWN
							myID = String2Int(ItemID)
							If myID = 0 Then
								For i = 0 To MaxLoop
									myHTML.Append("<option value=""").Append(HtmlEncode(.Rows(i).Item(0).ToString)).Append(""">").Append(.Rows(i).Item(1))	   '& "</option>"  
								Next
							Else
								'If MaxLoop < 3 Then '3 records
								'    For i = 0 To MaxLoop
								'        curID = CInt(.Rows(i).Item("ID"))
								'        myHTML.Append(HTMLInputRadio(FieldName, CStr(curID), CStr(.Rows(i).Item("value")), , , , i = 0))
								'        'myHTML.Append(HTMLOption(CStr(curID), CStr(.Rows(i).Item("value")), myID = curID))         '& "</option>"  
								'    Next
								'Else
								For i = 0 To MaxLoop
									curID = CInt(.Rows(i).Item(0))
									myHTML.Append(HTMLOption(CStr(curID), HtmlEncode(CStr(.Rows(i).Item(1))), myID = curID))  '& "</option>"  
								Next
								'End If
							End If
							If MaxLoop > 999 Then myHTML.Append("<option>- 1000 items maximum -")
						End If
					End With
				End If
				Source = Nothing
			End If
			'Else
			'    If Mid(SQLTable, 6, 11) <> "range=" Then
			'        MinLoop = CInt(Val(Right(SQLTable, SQLTable.Length - 11)) 
			'        i = InStr(SQLTable, "-")
			'        MaxLoop = String2Int(Right(SQLTable, SQLTable.Length - 11 - i)))
			'        For i = MinLoop To MaxLoop
			'            myHTML.Append(HTMLInputCheckBox(fieldname, CStr(i), CStr(i))).Append(vbCrLf)
			'        Next
			'    End If
			''If Left(SQLTable, 5) <> "list:months" Then
			''    For i = 1 To 12
			''        myHTML.Append(HTMLInputCheckBox(fieldname, CStr(i), CStr(i))).Append(vbCrLf)
			''    Next
			''End If
			''split...
			''integer have lov like "1-31" or " lov="range:1-31" lov="enumeration:1=January,2=February..." lov="enumaration:CA=California,..."   
			'End If
			Return myHTML.ToString
		End Function

		Private Function HTMLlovMany(ByRef aNode As XmlNode, ByRef fieldLOVID As Integer, Optional ByVal Link As String = "") As String
			'make a query and returns the HTML for a lov 
			Dim i As Integer, j As Integer, myHTML As New StringBuilder
			Dim Source As DataSet, sql As String, MaxLoop As Integer, lovcolumnid As String = "ID"
			Dim SQLTable As String, SQLTables As String, SQLColumnMaster As String, SQLColumnDetails As String, SQLColumnRead As String = "", SP_LOV As String, SQLwhere As String = "", SQLOrderBy As String = "value"
			Dim buffer As String = "", r1 As Boolean = False

			If aNode.Attributes(EvoXMLInfo.dbtablelov) Is Nothing Then
				SQLTable = String.Empty
			Else
				SQLTable = aNode.Attributes(EvoXMLInfo.dbtablelov).Value
			End If
			'If Left(SQLTable, 5) <> "list:" Then
			If atRunTime AndAlso SQLTable <> String.Empty Then
				If aNode.Attributes(EvoXMLInfo.lovSPlist) Is Nothing Then
					SP_LOV = String.Empty
				Else
					SP_LOV = aNode.Attributes(EvoXMLInfo.lovSPlist).Value
				End If
				''fieldName = UID & aNode.Attributes(Attr.dbColumn).Value 'now para
				'cacheKey = ("lov2" & dbtable & SQLTable & fieldLOVID).tolower
				'Source = CType(Page.Cache(cacheKey), DataSet)
				'If Source Is Nothing Then
				'    If ErrorMsg = "" Then
				If String.IsNullOrEmpty(SP_LOV) Then
					With aNode
						If Not .Attributes(EvoXMLInfo.dbColumn) Is Nothing Then SQLColumnMaster = .Attributes(EvoXMLInfo.dbColumn).Value
						If Not .Attributes(EvoXMLInfo.dbcolumnreadlov) Is Nothing Then SQLColumnRead = .Attributes(EvoXMLInfo.dbcolumnreadlov).Value
						If String.IsNullOrEmpty(SQLColumnRead) Then SQLColumnRead = "name"
						If Not .Attributes(EvoXMLInfo.dbColumnDetails) Is Nothing Then SQLColumnDetails = .Attributes(EvoXMLInfo.dbColumnDetails).Value
						If .Attributes("lovcolumnid").Value Is Nothing Then
							lovcolumnid = "ID"
						Else
							lovcolumnid = .Attributes("lovcolumnid").Value
						End If
						'If required Then sql += "ID>0" Else sql = ""
						sql = String.Format("TOP 100 t.ID, rtrim(t.{0}) as value", SQLColumnRead)
						If Not .Attributes("lovorderby") Is Nothing Then SQLOrderBy = .Attributes("lovorderby").Value
					End With
					If String.IsNullOrEmpty(SQLOrderBy) Then SQLOrderBy = "value"
					If String.IsNullOrEmpty(SQLColumnMaster) Then
						SQLTables = SQLTable
					Else
						SQLwhere = String.Format("T.{0}=T1.{0} AND T1.ID<>T.ID AND T1.ID={1}", lovcolumnid, _ItemID)
						If Not aNode.Attributes(EvoXMLInfo.dbwherelov) Is Nothing Then buffer = aNode.Attributes(EvoXMLInfo.dbwherelov).Value
						If buffer <> String.Empty Then SQLwhere += " AND " & buffer
						SQLTables = String.Format("{0} T,{1} T1", SQLTable, def_Data.dbtable)
					End If
					sql = BuildSQL(sql, SQLTables, SQLwhere, SQLOrderBy)
				Else
					SP_LOV = SPcall_Get(SP_LOV, _ItemID, _UserID, fieldLOVID)
					sql = SP_LOV
				End If
				Source = GetData(sql, _SqlConnection)
				'        If Not Source Is Nothing Then Page.Cache(cacheKey) = Source
				'    End If
				'End If
				If Source Is Nothing Then
					ErrorMsg += String.Format("{0} (table {1}).", LG_NoData, SQLTable)
				Else
					Try
						MaxLoop = Source.Tables(0).Rows.Count - 1
					Catch
						MaxLoop = -1
					End Try
					If MaxLoop > -1 Then
						With Source.Tables(0)
							If String.IsNullOrEmpty(Link) Then
								myHTML.Append(.Rows(0).Item("value"))
								For i = 1 To MaxLoop
									myHTML.Append(HTML_Sep).Append(CStr(.Rows(i).Item("value")))
								Next
							Else
								r1 = InStr(Link, p_itemid) > 1
								For i = 0 To MaxLoop
									If i > 0 Then myHTML.Append(", ")
									If r1 Then
										j = CInt(.Rows(i).Item(0))
										If j <> _ItemID Then
											myHTML.Append(HTMLLink(Link.Replace(p_itemid, CStr(j)), CStr(.Rows(i).Item("value"))))
										Else
											myHTML.Append(CStr(.Rows(i).Item(1)))
										End If
									Else
										myHTML.Append(HTMLLink(Link, CStr(.Rows(i).Item(1))))
									End If
								Next
								If MaxLoop > 99 Then myHTML.Append(" (100 Items Maximum)")
							End If
						End With
					End If
				End If
				Source = Nothing
			End If
			Return myHTML.ToString
		End Function

#End Region

		'### Actions #########################################################################################
#Region "Actions"

		Private Function CheckLogin() As String
			'Check if login/password is valid
			'Returns ID, Login, Welcome message
			Dim dr As SqlDataReader
			Dim username As String = String.Empty, aSQL As String
			Dim cn As New SqlConnection(_SqlConnection)

			If String.IsNullOrEmpty(ErrorMsg) Then
				username = GetPageRequest("EvoLogin", "")
				If String.IsNullOrEmpty(def_Data.splogin) Then
					aSQL = BuildSQL("ID", def_Data.dbtableusers, "login=@login AND password=@password")
				Else
					aSQL = SQL_EXEC & def_Data.splogin.Replace("@application", _DBApplicationKey)
				End If
				cn.Open()
				Dim cmd As New SqlCommand(aSQL, cn)
				cmd.Parameters.Add(New SqlParameter("@login", username))
				cmd.Parameters.Add(New SqlParameter("@password", GetPageRequest("EvoPassword")))
				Try
					dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
				Catch DBerror As Exception
					AddError(HTMLtextMore(LG_NoQuery, DBerror.Message))
				End Try
				If String.IsNullOrEmpty(ErrorMsg) Then
					If dr.Read() Then
						Try
							_UserID = CInt(dr.GetValue(dr.GetOrdinal("ID")))
						Catch : _UserID = -1 : End Try
					Else
						_UserID = -1
					End If
					If _UserID > 0 Then
						SetUserID(_UserID)
						Try
							HeaderMsg = CStr(dr.GetValue(dr.GetOrdinal("welcome")))
						Catch
							HeaderMsg = String.Format(LG_Welcome, TextUcaseFirst(username))
						End Try 
						OnCredentialChange(New CredentialEventArgs(CredentialAction.Login, _UserID, username, _DBApplicationKey, HeaderMsg))
					Else
						ErrorMsg = LG_InvalidLogin
						HeaderMsg = LG_InvalidLogin2
						username = String.Empty
						OnCredentialChange(New CredentialEventArgs(CredentialAction.Invalid_Login, _UserID, username, _DBApplicationKey, HeaderMsg))
					End If
				Else
					ErrorMsg = "Invalid login stored procedure."
					username = String.Empty
					OnCredentialChange(New CredentialEventArgs(CredentialAction.DB_ERROR, 0, "", _DBApplicationKey, ErrorMsg))
				End If
				cmd.Dispose()
				cn.Close()
			End If
			Return username
		End Function

		Private Function UploadDoc(ByRef PixID As Int32, Optional ByVal fieldName As String = "", Optional ByVal Pix As Boolean = True, Optional ByVal thumbnail As Boolean = False) As String
			Dim objFile As System.Web.HttpPostedFile, DocValid As Boolean = True
			Dim strFileName As String, fullname As String = "", strFileExtension As String

			If Page.Request(fieldName & "_dp") = "1" Then
				strFileName = String.Empty
				'must delete physical file on server
			Else
				Try
					'Saving picture file to server
					objFile = System.Web.HttpContext.Current.Request.Files.Item(PixID)
					strFileName = System.IO.Path.GetFileName(objFile.FileName)
					If strFileName <> String.Empty Then
						strFileExtension = LCase(System.IO.Path.GetExtension(strFileName))
						If Pix Then
							DocValid = strFileExtension.Equals(".gif") OrElse strFileExtension.Equals(".jpg") OrElse strFileExtension.Equals(".png")
						End If
						If DocValid Then
							If _UserID > 0 Then
								strFileName = CStr(_UserID).TrimStart & "_" & strFileName
							Else
								strFileName = Right(CStr(Now.Ticks), 5) & strFileName
							End If
							fullname = Page.MapPath(_PathPix) & strFileName
							fullname = fullname.Replace("/", "\")
							objFile.SaveAs(fullname)
						Else
							HeaderMsg += LG_NoUpload & " " & LG_NoUpload2
						End If
					Else
						strFileName = Tilda
					End If
				Catch ex As Exception
					'use headermag and not errmsg b/c want to run db upload
					If String.IsNullOrEmpty(fullname) Then
						HeaderMsg += LG_NoUpload & " The form may not have the attribute enctype = ""multipart/form-data""." ' or the directory is not accessible."
						strFileName = Tilda
					Else
						HeaderMsg += LG_NoUpload & vbCrLf & ex.Message
					End If
					strFileName = String.Empty
				End Try
			End If
			Return strFileName
		End Function

		Private Function GenerateExport(ByRef outputType As String) As String
			Dim mySQL As New StringBuilder
			Dim sql As String, sqlw As String, sqlob As String = ""
			Dim i As Integer, j As Integer, tableIndex As Integer, separator As String = coma, hideID As Boolean = True
			Dim maxRow As Integer, maxColBL As Integer, minCol As Integer, maxCol As Integer, yesNo As Boolean = True, yesNo2 As Boolean
			Dim fieldValue As String, buffer As String = "", buffer1 As String, buffer2 As String, cTDcrlf As String = "</td>" & vbCrLf, cTRcrlf As String = "</tr>" & vbCrLf
			Dim Signature As String = "Exported by ", vbCrLf2 As String = vbCrLf & vbCrLf

			If CStr(Page.Request("evoxQSE")) = "0" Then
				buffer = String.Format("sql{0}{1}{2}", _UserID, def_Data.dbtable, _FormID)
				sqlw = CStr(Page.Cache(buffer & "_W"))
				sqlob = CStr(Page.Cache(buffer & "_O"))
			Else
				sqlw = def_Data.dbwhere
				buffer = BuildSQLwhereSecurity()
				If buffer <> String.Empty Then sqlw = CondiConcat(sqlw, buffer, SQL_and)
			End If
			If String.IsNullOrEmpty(sqlob) Then sqlob = "t.ID"
			sql = BuildSQLselect(True, 100, _FormID, def_Data.dbtable, String.Empty, sqlw, sqlob)
			If _DisplayMode = 72 Then 'single record, maybe details
				'If outputType = "CSV" Or outputType = "XML" Then
				sql += BuildSQLDetails(False)
				'End If
			End If
			ds = GetData(sql, _SqlConnection)
			If Not ds Is Nothing Then
				maxRow = ds.Tables(0).Rows.Count - 1
				maxColBL = ds.Tables(0).Columns.Count - 1
				maxCol = maxColBL
				minCol = 0
				hideID = Page.Request("showID") <> "1"
				If hideID Then minCol = 1
				Signature += evoName & LG_cmOn & HTMLDateFormated(t_datetime, CStr(Now), String.Empty)
				Select Case outputType
					Case "XML"
						ds.DataSetName = entities
						mySQL.Append("<?xml version=""1.0"" encoding=""UTF-8"" ?>").Append(vbCrLf)
						mySQL.Append("<!-- ").Append(def_Data.dbtable).Append(HTML_Sep).Append(Signature).Append(" --> ").Append(vbCrLf)
						For tableIndex = 0 To ds.Tables.Count - 1
							With ds.Tables(tableIndex)
								If tableIndex = 0 Then
									.TableName = entity
									buffer1 = GetPageRequest("evoRoot", "")
									If buffer1 <> String.Empty Then buffer1 = buffer1.Replace("<", String.Empty).Replace(">", String.Empty).Replace("""", "'")
									yesNo = Page.Request("evoxpC2X") = "2"
									If maxCol <> maxColBL Then
										For i = maxCol + 1 To maxColBL
											.Columns(i).ColumnMapping = MappingType.Hidden
										Next
									End If
								Else
									.TableName = String.Format("{0} details {1}", entity, tableIndex)
								End If
								If yesNo Then
									For i = minCol To maxCol
										.Columns(i).ColumnMapping = MappingType.Attribute
									Next
								End If
								If hideID Then .Columns(0).ColumnMapping = MappingType.Hidden
							End With
						Next
						mySQL.Append(ds.GetXml())
					Case "HTML"
						mySQL.Append("<table width=""100%"" ID=""Evolutility_Export"">").Append(vbCrLf)
						mySQL.Append(HTMLtrColor(Page.Request("evoColRCT"))).Append(vbCrLf)
						With ds.Tables(0)
							'header
							For j = minCol To maxCol
								mySQL.Append(" <th>").Append(CStr(ds.Tables(0).Columns(j).ColumnName)).Append("</th>").Append(vbCrLf)
							Next
							mySQL.Append("</tr>").Append(vbCrLf)
							'body  
							buffer1 = HTMLtrColor(Page.Request("evoColRCO")) & vbCrLf
							buffer2 = HTMLtrColor(Page.Request("evoColRCE")) & vbCrLf
							For i = 0 To maxRow
								If yesNo Then mySQL.Append(buffer1) Else mySQL.Append(buffer2)
								With .Rows(i)
									For j = minCol To maxCol
										mySQL.Append(" <td>")
										Try
											mySQL.Append(CStr(.Item(j)))
										Catch : End Try
										mySQL.Append(cTDcrlf)
									Next
								End With
								mySQL.Append(cTDcrlf)
								yesNo = Not yesNo
							Next
						End With
						mySQL.Append("</table>").Append(vbCrLf).Append(SMALL_tag).Append(Signature).Append(SMALL_tagClose)
					Case "SQL"
						mySQL.Append(vbCrLf).Append("/*** ").Append(def_Data.dbtable).Append(HTML_Sep).Append(Signature).Append(" ***/").Append(vbCrLf2)
						yesNo = Page.Request("evoxpTRS") <> String.Empty
						If yesNo Then mySQL.Append("BEGIN TRANSACTION").Append(vbCrLf2)
						yesNo2 = Page.Request("evoxpTRS2") <> String.Empty
						If yesNo2 Then mySQL.Append("SET IDENTITY_INSERT ").Append(def_Data.dbtable).Append(" ON;").Append(vbCrLf2)
						For tableIndex = 0 To ds.Tables.Count - 1
							With ds.Tables(tableIndex)
								If tableIndex > 0 Then
									maxCol = .Columns.Count - 1
									maxRow = .Rows.Count - 1
								End If
								buffer = "INSERT INTO "
								If tableIndex = 0 Then
									buffer += String.Format("{0} (", def_Data.dbtable)
								Else
									buffer += String.Format("{0}Details{1} (", def_Data.dbtable, CStr(tableIndex))
									maxCol = .Columns.Count - 1
									maxRow = .Rows.Count - 1
								End If
								For i = minCol To maxCol
									buffer += .Columns(i).ColumnName & ", "
								Next
								buffer = Mid(buffer, 1, buffer.Length - 2)
								buffer += String.Format("){0}  VALUES (", vbCrLf)
								For i = 0 To maxRow
									mySQL.Append(buffer)
									For j = minCol To maxCol
										Try
											fieldValue = CStr(.Rows(i)(j))
										Catch
											fieldValue = String.Empty
										End Try
										mySQL.Append(dbformat2(fieldValue, .Columns(j).DataType.ToString, _Language))
										If j < maxCol Then mySQL.Append(", ")
									Next
									mySQL.Append(");").Append(vbCrLf)
								Next
							End With
							mySQL.Append(vbCrLf2)
						Next
						If yesNo2 Then mySQL.Append("SET IDENTITY_INSERT ").Append(def_Data.dbtable).Append(" OFF;").Append(vbCrLf2)
						If yesNo Then mySQL.Append(vbCrLf).Append("COMMIT TRANSACTION").Append(vbCrLf2)
					Case Else '"CSV", "TAB", "TXT", "XLS"
						If outputType.Equals("TAB") Then
							separator = vbTab
						Else
							separator = GetPageRequest("FLS_evol", coma)
						End If
						For tableIndex = 0 To ds.Tables.Count - 1
							With ds.Tables(tableIndex)
								If tableIndex = 0 Then 'db header
									'need label from metadata
									If _DisplayMode = 71 Then 'many recs
										yesNo = GetPageRequest(UID & "FLH") = "1" 'FLH=First Line Header
									Else '1 rec + details
										yesNo = True
									End If
								Else
									maxCol = .Columns.Count - 1
									maxRow = .Rows.Count - 1
								End If
								If tableIndex = 0 OrElse (tableIndex > 0 AndAlso maxRow > -1) Then
									'- xport header
									If yesNo Then	'first line as header 
										For j = minCol To maxCol
											Try : fieldValue = .Columns(j).ColumnName
											Catch : fieldValue = String.Empty : End Try
											If InStr(fieldValue, separator) > 0 Then
												mySQL.Append(inQuote(fieldValue))
											Else
												mySQL.Append(fieldValue)
											End If
											If j < maxCol Then mySQL.Append(separator)
										Next
										mySQL.Append(vbCrLf)
									End If
									'- body
									For i = 0 To maxRow
										For j = minCol To maxCol
											Try : fieldValue = CStr(.Rows(i)(j))
											Catch : fieldValue = String.Empty : End Try
											If fieldValue <> String.Empty Then
												If InStr(fieldValue, separator) > 0 Then
													mySQL.Append(inQuote(fieldValue))
												Else
													If InStr(fieldValue, vbCrLf) > 0 Then
														fieldValue = fieldValue.Replace(vbCrLf, "|")
													End If
													mySQL.Append(fieldValue)
												End If
											End If
											If j < maxCol Then mySQL.Append(separator)
										Next
										mySQL.Append(vbCrLf)
									Next
								End If
							End With
							mySQL.Append(vbCrLf)
						Next
				End Select
				mySQL.Append(vbCrLf)
			Else
				mySQL.Append(LG_NoData)	'"No data to export")
			End If
			Return mySQL.ToString
		End Function

#End Region

		'### Database ########################################################################################
#Region "Database"

		Private Function BuildSQLDetails(ByRef LoadIt As Boolean) As String
			Dim i As Integer, foID As Integer, MaxRows As Integer, MaxLoopXML As Integer, dbcolumnpixDet As String
			Dim sqls As String, sqlw As String, splistlov As String, maxRow As Integer, buffer As String
			Dim XMLPanelID As Integer
			Dim aNodeList As XmlNodeList
			Dim sql As New StringBuilder

			If atRunTime Then
				If (LoadIt AndAlso Not (ds Is Nothing AndAlso _ItemID > 0)) OrElse Not LoadIt Then
					aNodeList = myDOM.DocumentElement.SelectNodes(XPathQuery.panelDetails, nsManager)
					MaxLoopXML = aNodeList.Count - 1
					For i = 0 To MaxLoopXML
						With aNodeList(i)
							Try
								MaxRows = CInt(aNodeList(i).Attributes("dbmaxrows").Value)
							Catch : MaxRows = -1 : End Try
							If .Attributes(EvoXMLInfo.lovSPlist) Is Nothing Then
								splistlov = String.Empty
							Else
								splistlov = .Attributes(EvoXMLInfo.lovSPlist).Value
							End If
							If String.IsNullOrEmpty(splistlov) Then
								If .Attributes(EvoXMLInfo.dbTableDetails) Is Nothing OrElse .Attributes(EvoXMLInfo.dbColumnDetails) Is Nothing Then
									ErrorMsg = "Invalid details definition in XML. Invalid or missing attributes '" & EvoXMLInfo.dbTableDetails & "' and '" & EvoXMLInfo.dbColumnDetails & "'."
									dbcolumndetails = String.Empty
								Else
									dbtabledetails = .Attributes(EvoXMLInfo.dbTableDetails).Value
									dbcolumndetails = .Attributes(EvoXMLInfo.dbColumnDetails).Value
								End If
								If dbtabledetails <> String.Empty Then
									If MaxRows > 0 Then
										sqls = String.Format("TOP {0} *", MaxRows)
									Else
										sqls = "*"
									End If
									If .Attributes(EvoXMLInfo.dbWhere) Is Nothing Then
										sqlw = String.Empty
									Else
										sqlw = .Attributes(EvoXMLInfo.dbWhere).Value
									End If
									If dbcolumndetails <> String.Empty Then
										If dbtabledetails = "-self-" Then
											Try : foID = CInt(ds.Tables(0).Rows(0)(dbcolumndetails))
											Catch : foID = -1 : End Try
											sqlw = CondiConcat(sqlw, String.Format("T.{0}={1}", dbcolumndetails, CStr(foID)), SQL_and)
										Else
											sqlw = CondiConcat(sqlw, String.Format("T.{0}={1}", dbcolumndetails, CStr(_ItemID)), SQL_and)
										End If
									End If
									If .Attributes("dbmaxrows") Is Nothing Then
										maxRow = 0
									Else
										maxRow = CInt(Val(.Attributes("dbmaxrows").Value))
									End If
									'    sql += BuildSQL(sqls, dbtabledetails & " T", sqlw, dborder)
									'element & "[@" & AttributeTrue & "[.>0]]"  
									If .Attributes() Is Nothing Then
										foID = _FormID
									Else
										XMLPanelID = CInt(Val(.Attributes("panelid").Value))
										foID = CInt(XMLPanelID)
									End If
									If .Attributes(EvoXMLInfo.dbColumnPix) Is Nothing Then
										dbcolumnpixDet = String.Empty
									Else
										dbcolumnpixDet = .Attributes(EvoXMLInfo.dbColumnPix).Value
									End If
									If .Attributes(EvoXMLInfo.dborder) Is Nothing Then
										buffer = String.Empty
									Else
										buffer = .Attributes(EvoXMLInfo.dborder).Value
									End If
									sql.Append(BuildSQLselect(False, 2, foID, dbtabledetails, String.Empty, sqlw, buffer, XPathQuery.panelDetailsField, maxRow, dbcolumnpixDet))
								Else
									sql.Append(SPcall_Get(splistlov, _ItemID, _UserID))
								End If
							End If
						End With
					Next
				End If
			End If
			aNodeList = Nothing
			If _DBAllowComments <> EvolCommentsMode.None AndAlso Not noCommentsHere Then
				Try : i = CInt(ds.Tables(0).Rows(0)(SQLnbComments))
				Catch : i = 0 : End Try
				If i > 0 Then
					buffer = String.Format("T.UserID={0}.ID AND T.{1}={2}", def_Data.dbtableusers, def_Data.dbcolumncomments, CStr(_ItemID))
					If def_Data.dbcommentsformid > 0 Then buffer += String.Format(" AND T.FormID={0}", def_Data.dbcommentsformid)
					sql.Append(BuildSQL("T.ID AS ID,T.message,T.CreationDate,t.UserID," & def_Data.dbtableusers & ".login AS login", def_Data.dbtablecomments & " T," & def_Data.dbtableusers, buffer, "T.ID DESC"))
					'SELECT Comment.*,User.login FROM Comment,User WHERE Comment.UserID=User.ID
				Else
					noCommentsHere = True
				End If
			End If
			If LoadIt Then
				If sql.Length > 0 AndAlso Not detailsLoaded Then
					ds2 = GetData(sql.ToString(), _SqlConnection)
					detailsLoaded = True
				End If
			Else
				Return sql.ToString()
			End If
		End Function

		Private Function BuildSQLDeleteItem() As Integer
			Dim retVal As Integer, aSQL As String
			Dim con As New SqlConnection(_SqlConnection)

			If _DBAllowDelete Then
				If String.IsNullOrEmpty(def_Data.spdelete) Then
					If _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_RLS) OrElse _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_Sharing) Then
						aSQL = String.Format("{0}=@itemid AND {1}=@userid", def_Data.dbcolumnpk, def_Data.dbcolumnuserid)
					Else
						aSQL = String.Format("{0}=@itemid", def_Data.dbcolumnpk)
					End If
					aSQL = sqlDELETE(def_Data.dbtable, aSQL)
				Else
					aSQL = SQL_EXEC & def_Data.spdelete
				End If
				Dim cmd As New SqlCommand(aSQL, con)
				cmd.Parameters.AddWithValue("@itemid", _ItemID)
				cmd.Parameters.AddWithValue("@userid", _UserID)
				Try
					con.Open()
					cmd.ExecuteNonQuery()
					retVal = _ItemID
				Catch ex As Exception
					retVal = -1
					ErrorMsg = HTMLtextMore(String.Format(LG_CannotDelete, entity, Str(_ItemID)), ex.ToString)
				Finally
					con.Close()
				End Try
				If retVal > 0 Then
					HeaderMsg = String.Format(LG_DeleteOK, Str(_ItemID), Now())
					OnDBChange(New DatabaseEventArgs(DBAction.DELETE, _ItemID))
				End If
				'nav = 3
				_DisplayMode = 110
			Else
				AddError(String.Format("{0}{1} {2}.", LG_JSNoWay, LG_Delete, entities))
				retVal = -1
			End If
			Return retVal
		End Function

		Private Function BuildSQLlist(ByRef lDisplayMode As Integer) As String
			Dim sql As String = "", sql2 As String = "", sqlw As String = "", sqlob As String, cacheKey As String
			Dim dbcolumn As String, dbcolumnf As String, fieldType As String, fieldlabel As String
			Dim i As Integer, MaxLoopXML As Integer
			Dim ClauseOperator As String, buffer As String, buffer1 As String, fieldValue As String, buffer3 As String, sqlOperator As String = SQL_and
			Dim myHTML As New StringBuilder
			Dim aNodeList As XmlNodeList, aNode As XmlNode

			'###### Generate SQL ########
			If atRunTime Then
				If String.IsNullOrEmpty(def_Data.dborder) Then
					If InStr(def_Data.dborder, dot) = 0 Then sqlob = String.Format("T.{0}", def_Data.dbcolumnpk)
				Else
					If def_Data.dborder.StartsWith(dot) Then def_Data.dborder = def_Data.dbtable & def_Data.dborder
					sqlob = def_Data.dborder
				End If
				If lDisplayMode = 110 Then
					sql2 = allEntities
				ElseIf lDisplayMode = 102 Then 'query form queries (selections)  "//panel/field[@" & AttributeTrue & "[.>0]]"
					'buffer = queryUrlParam
					If queryUrlParam <> String.Empty Then
						'If queryUrlParam.StartsWith("~") Then 'Alphabet links
						'    buffer = Right(queryUrlParam, 1)
						'    sqlw = "T." & dbcolumnlead & " LIKE '" & buffer & "%' "
						'    sql2 = LG_sStart & """" & buffer & """"
						'    sqlob = dbcolumnlead
						'Else
						aNode = myDOM.DocumentElement.SelectSingleNode(XPathQuery.XPath2Equal(XPathQuery.query, EvoXMLInfo.url, queryUrlParam), nsManager)
						buffer = sqlob
						If aNode Is Nothing Then
							ErrorMsg += "Invalid query ID."
						Else
							If Not aNode.Attributes(EvoXMLInfo.dbWhere) Is Nothing Then sqlw = aNode.Attributes(EvoXMLInfo.dbWhere).Value
							If Not aNode.Attributes(EvoXMLInfo.label) Is Nothing Then sql2 = aNode.Attributes(EvoXMLInfo.label).Value
							If aNode.Attributes(EvoXMLInfo.dborder) Is Nothing Then
								sqlob = buffer
							Else
								sqlob = aNode.Attributes(EvoXMLInfo.dborder).Value
							End If
							sqlw = HTML2SQL(sqlw)
							sqlw = sqlw.Replace("~pc~", "%").Replace("~s~", "*")
							If _UserID > 0 AndAlso sqlw.IndexOf("@userid") > -1 Then
								sqlw = sqlw.Replace("@userid", Str(_UserID))
							End If
						End If
						'End If
					Else
						AddError("No query ID specified in request.")
					End If
				ElseIf lDisplayMode = 105 Then
					buffer = String.Format("sql{0}{1}{2}", _UserID, def_Data.dbtable, _FormID)
					sqlw = CStr(Page.Cache(buffer & "_W"))
					sql2 = CStr(Page.Cache(buffer & "_W2"))
					If String.IsNullOrEmpty(newOrderBy) Then newOrderBy = CStr(Page.Cache(buffer & "_O"))
					If newOrderBy <> String.Empty Then
						Select Case newOrderBy.Substring(0, 1)
							Case dot 'driving table
								newOrderBy = String.Format("T{0}", newOrderBy)
							Case "@" 'lov table 
								i = newOrderBy.Length
								If i > 6 Then
									buffer = newOrderBy.Substring(1, i - 6)
									aNode = myDOM.SelectSingleNode(XPathQuery.XPath2Equal(XPathQuery.panelField, "dbcolumn", buffer), nsManager)
									If aNode.Attributes(EvoXMLInfo.dbtablelov) Is Nothing Then
										buffer = String.Empty
									Else
										buffer = aNode.Attributes(EvoXMLInfo.dbtablelov).Value
									End If
								Else
									buffer = String.Empty
								End If
								If String.IsNullOrEmpty(buffer) Then
									If aNode.Attributes(EvoXMLInfo.dbcolumnreadlov) Is Nothing Then
										buffer1 = EvoXMLInfo.dbname
									Else
										buffer1 = aNode.Attributes(EvoXMLInfo.dbcolumnreadlov).Value
									End If
									newOrderBy = (New StringBuilder).Append(buffer).Append(dot).Append(buffer1).Append(" ").Append(Right(newOrderBy, 4)).ToString
								Else
									newOrderBy = newOrderBy.Replace("@", "T.")
								End If
							Case Tilda 'formula
								i = newOrderBy.Length
								If i > 6 Then
									buffer = newOrderBy.Substring(1, i - 6)
									aNode = myDOM.SelectSingleNode(XPathQuery.XPath2Equal(XPathQuery.panelField, EvoXMLInfo.dbColumnRead, buffer), nsManager)
								End If
								If aNode.Attributes(EvoXMLInfo.dbColumn) Is Nothing Then
									buffer1 = EvoXMLInfo.dbname
								Else
									buffer1 = String.Format("({0})", aNode.Attributes(EvoXMLInfo.dbColumn).Value)
								End If
								newOrderBy = (New StringBuilder).Append(" ").Append(buffer1).Append(" ").Append(Right(newOrderBy, 4)).ToString
						End Select
						def_Data.dborder = newOrderBy
						sqlob = newOrderBy
					End If
				Else
					_ItemID = 0
					Select Case lDisplayMode
						Case 104 'adv search
							buffer = XPathQuery.XPath2True("searchadv") 
						Case 71, 72	'export generation
							buffer = XPathQuery.panelField
						Case Else 'search
							buffer = XPathQuery.XPath2True(EvoXMLInfo.Search)
					End Select
					aNodeList = myDOM.DocumentElement.SelectNodes(buffer, nsManager)
					'Generate WHERE clause
					MaxLoopXML = aNodeList.Count - 1
					For i = 0 To MaxLoopXML
						With aNodeList(i)
							dbcolumn = .Attributes(EvoXMLInfo.dbColumn).Value
							buffer = UID & dbcolumn
							If Page.Request(buffer) <> String.Empty Then
								fieldValue = Page.Request(buffer).Replace("'", "''")
								dbcolumnf = String.Format("T.[{0}]", dbcolumn)
								fieldlabel = .Attributes(EvoXMLInfo.label).Value
								fieldType = .Attributes(EvoXMLInfo.type).Value
								If String.IsNullOrEmpty(fieldlabel) Then
									If Not .Attributes(EvoXMLInfo.labelEdit) Is Nothing Then
										fieldlabel = .Attributes(EvoXMLInfo.labelEdit).Value
									End If
									If String.IsNullOrEmpty(fieldlabel) Then
										If Not .Attributes(EvoXMLInfo.labelList) Is Nothing Then
											fieldlabel = .Attributes(EvoXMLInfo.labelList).Value
										End If
									End If
								End If
								Select Case fieldType
									Case t_text, t_email, t_url, t_txtm, t_html
										sqlw += sqlOperator & dbcolumnf
										sql2 += sqlOperator & fieldlabel
										If lDisplayMode = 103 Then
											ClauseOperator = "ct"
										Else
											ClauseOperator = GetPageRequest(buffer & "_c")
										End If
										sqlw += SQLec(t_text, fieldValue, ClauseOperator)
										sql2 += TXTec(t_text, fieldValue, ClauseOperator)
									Case t_lov
										''LOV value from cache
										'' use cached LOV to display user value rather than ID
										If _UseCache Then
											cacheKey = .Attributes(EvoXMLInfo.dbtablelov).Value
											If .Attributes(EvoXMLInfo.dbcolumnreadlov) Is Nothing Then
												buffer3 = String.Empty
											Else
												buffer3 = .Attributes(EvoXMLInfo.dbcolumnreadlov).Value
											End If
											If String.IsNullOrEmpty(buffer3) Then buffer3 = EvoXMLInfo.dbname
											cacheKey += buffer3
											If Not .Attributes(EvoXMLInfo.dbColumnImg) Is Nothing Then
												cacheKey += .Attributes(EvoXMLInfo.dbColumnImg).Value
											End If
											If Not .Attributes(EvoXMLInfo.dbwherelov) Is Nothing Then
												cacheKey += .Attributes(EvoXMLInfo.dbwherelov).Value
											End If
											cacheKey = t_lov & cacheKey.ToLower
											buffer3 = LOVfromCache(cacheKey, fieldValue)
											If String.IsNullOrEmpty(buffer3) Then buffer3 = fieldValue
										Else
											buffer3 = fieldValue
										End If
										If InStr(fieldValue, coma) > 0 Then
											sqlw += (New StringBuilder).Append(sqlOperator).Append(dbcolumnf).Append(" IN (").Append(fieldValue).Append(")").ToString
											sql2 += (New StringBuilder).Append(sqlOperator).Append(fieldlabel).Append(LG_qInList).Append("(").Append(buffer3).Append(")").ToString
										Else
											sqlw += (New StringBuilder).Append(sqlOperator).Append(dbcolumnf).Append("=").Append(fieldValue).ToString
											sql2 += (New StringBuilder).Append(sqlOperator).Append(fieldlabel).Append(LG_qEquals).Append("""").Append(buffer3).Append("""").ToString
										End If
									Case t_bool
										If fieldValue = "0" Then
											sqlw += (New StringBuilder).Append(sqlOperator).Append("isnull(").Append(dbcolumnf).Append(",0)=0").ToString
											sql2 += (New StringBuilder).Append(sqlOperator).Append(LG_qNot).Append(fieldlabel).ToString
										Else
											sqlw += sqlOperator & dbcolumnf & ">0" ' "=" & fieldValue
											sql2 += sqlOperator & fieldlabel
										End If
									Case t_date, t_datetime, t_time
										If lDisplayMode = 103 Then 'search
											If InStr("dayweekmonthyear", fieldValue) > 0 Then
												sqlw += (New StringBuilder).Append(sqlOperator).Append(" (DATEDIFF(").Append(fieldValue).Append(coma).Append(dbcolumnf).Append(",getdate())<1").ToString
												sql2 += sqlOperator & fieldlabel ' & LG_sDateRangeLast & GetValFromCSVTuples(LG_sDateRange, fieldValue)
												Select Case Page.Request(buffer & "dir")
													Case ""	'Within
														sql2 += LG_sDateRangeWithin
													Case "P"  'Past 
														sql2 += LG_sDateRangeLast
														sqlw += String.Format(" AND getdate()>{0}", dbcolumnf)
													Case "F" 'Futur
														sql2 += LG_sDateRangeNext
														sqlw += String.Format(" AND getdate()<{0}", dbcolumnf)
												End Select
												sql2 += GetValFromCSVTuples(LG_sDateRange, fieldValue)
												sqlw += ") "
											Else
												If IsDate(fieldValue) Then
													sqlw += (New StringBuilder).Append(sqlOperator).Append(" DATEDIFF(day,").Append(dbFormat(fieldValue, t_date, 0, _Language)).Append(coma).Append(dbcolumnf).Append(")=0 ").ToString
													sql2 += (New StringBuilder).Append(sqlOperator).Append(fieldlabel).Append(" = ").Append(fieldValue).ToString
												End If
											End If
										Else 'advanced search   
											If IsDate(fieldValue) Then
												sqlw += (New StringBuilder).Append(sqlOperator).Append(" DATEDIFF(day,").Append(dbFormat(fieldValue, t_date, 0, _Language)).Append(coma).Append(dbcolumnf).Append(")=0 ").ToString
												sql2 += (New StringBuilder).Append(sqlOperator).Append(fieldlabel).Append(" = ").Append(fieldValue).ToString
											End If
										End If
									Case t_dec, t_int
										If IsNumeric(fieldValue) Then
											sqlw += sqlOperator & dbcolumnf
											sql2 += sqlOperator & fieldlabel
											If lDisplayMode = 103 Then
												sqlw += String.Format("={0}", fieldValue)
												sql2 += String.Format("={0}", fieldValue)
											Else
												buffer3 = SQLec(t_int, String.Empty, GetPageRequest(buffer & "_c"))
												sqlw += buffer3 & fieldValue
												sql2 += (New StringBuilder).Append(" ").Append(buffer3).Append(" ").Append(fieldValue).ToString
											End If
										End If
									Case t_pix, t_doc
										If fieldValue = "1" Then
											sqlw += String.Format("{0}{1}<>'' ", sqlOperator, dbcolumnf)
											sql2 += sqlOperator & LG_qWith & fieldlabel
										End If
								End Select
							End If
						End With
					Next
					'CommentsCount
					If _DBAllowComments <> EvolCommentsMode.None AndAlso Not Page.Request("EvoxUCM") Is Nothing Then 
						If CStr(Page.Request("EvoxUCM")).Trim.Length > 0 Then
							sqlw = CondiConcat(sqlw, String.Format("0<T.{0}", SQLnbComments), sqlOperator)
							sql2 = CondiConcat(sql2, LG_wComments, sqlOperator)
						End If
					End If
				End If
				If String.IsNullOrEmpty(sqlw) Then
					sql2 = allEntities
				Else
					If lDisplayMode <> 102 AndAlso sqlw.StartsWith(sqlOperator) Then '102 = query 
						i = sqlOperator.Length
						sqlw = sqlw.Substring(i)
						sql2 = sql2.Substring(i)
					End If
				End If
				'cache sql and sql2 values
				buffer = String.Format("sql{0}{1}{2}", _UserID, def_Data.dbtable, _FormID)
				If Page.IsPostBack Then
					If lDisplayMode < 71 OrElse lDisplayMode > 72 Then
						If lDisplayMode <> 105 Then
							Page.Cache(buffer & "_W2") = String.Empty & sql2
							Page.Cache(buffer & "_W") = String.Empty & sqlw
						End If
						Page.Cache(buffer & "_O") = String.Empty & sqlob
					End If
				Else
					If CStr(Page.Cache(buffer & "_W")) <> String.Empty Then
						Page.Cache(buffer & "_W2") = String.Empty
						Page.Cache(buffer & "_W") = String.Empty
						Page.Cache(buffer & "_O") = String.Empty
					End If
				End If
				sql = BuildSQLselect(True, 100, _FormID, def_Data.dbtable, def_Data.sppaging, sqlw, sqlob, XPathQuery.XPath2True(EvoXMLInfo.SearchList))
			Else
				sql = "*"
				sql2 = allEntities
			End If
			'ErrorMsg = sql ''debug mode?
			Return sql & "~$#12@~" & sql2
		End Function

		Private Function BuildSQLnav(ByRef NavID As Integer, Optional ByVal FirstTry As Boolean = True) As String
			'SQL WHERE clause for navigation w/ Parameter @itemID
			Dim Wsql As String = String.Empty
			Dim OBsql As String = String.Empty, Buffer As String

			If String.IsNullOrEmpty(def_Data.spget) Then
				Select Case NavID
					Case 1 'first
						OBsql = String.Format("T.{0}", def_Data.dbcolumnpk)
					Case 2 'previous
						If FirstTry Then
							If ItemID > 0 Then Wsql = String.Format("T.{0}<@itemid", def_Data.dbcolumnpk)
							OBsql = String.Format("T.{0} DESC", def_Data.dbcolumnpk)
						End If
					Case 3 'next
						If FirstTry Then
							Wsql = String.Format("T.{0}>@itemid", def_Data.dbcolumnpk)
						Else
							OBsql = String.Format("T.{0} DESC", def_Data.dbcolumnpk)
						End If
					Case 4 'last
						OBsql = String.Format("T.{0} DESC", def_Data.dbcolumnpk)
					Case Else
						OBsql = String.Format("T.{0}", def_Data.dbcolumnpk)
						Wsql = String.Format("T.{0}=@itemid", def_Data.dbcolumnpk)
				End Select

				'back to last search result
				Buffer = String.Format("sql{0}{1}{2}", _UserID, def_Data.dbtable, _FormID)
				If Not Page.Cache(Buffer & "_W") Is Nothing Then
					Buffer = CStr(Page.Cache(Buffer & "_W"))
					If Buffer.Length > 0 Then
						If Wsql.Length > 0 Then Wsql += " and "
						Wsql += Buffer
					End If
				End If
				Wsql = BuildSQLselect(True, 0, _FormID, def_Data.dbtable, String.Empty, Wsql, OBsql, , 1)
				Return Wsql
			Else
				Return SQL_EXEC & def_Data.spget.Replace(p_userid, CStr(_UserID)).Replace("@navid", CStr(NavID))
			End If
		End Function

		Private Function BuildSQLwhereSecurity() As String
			Select Case _SecurityModel
				Case EvolSecurityModel.Multiple_Users_RLS
					Return (New StringBuilder).Append("T.").Append(def_Data.dbcolumnuserid).Append("=").Append(_UserID).ToString()
				Case EvolSecurityModel.Multiple_Users_Sharing
					Return (New StringBuilder).Append("(T.Publish>0 OR T.").Append(def_Data.dbcolumnuserid).Append("=").Append(_UserID).Append(")").ToString()
				Case Else
					Return String.Empty
			End Select
		End Function

		Private Function BuildSQLDetailsUpdate() As String
			Dim i As Integer, j As Integer, fid As Integer, buffer As String, maxCol As Integer
			Dim id As Integer, sql1 As String = String.Empty, sql2 As String = String.Empty
			Dim dbTableDetails As String, dbcolumn As String, hasValue As Boolean
			Dim CellValues() As String, CellValuesOriginal() As String
			Dim aNodeList As XmlNodeList
			Dim SQL As New StringBuilder
			Dim fieldType As String, fieldColumn As String, fieldMaxLength As Integer, fieldIn As Boolean

			aNodeList = myDOM.DocumentElement.SelectNodes(XPathQuery.panelDetails, nsManager)
			With aNodeList(0)
				dbTableDetails = .Attributes(EvoXMLInfo.dbTableDetails).Value
				dbcolumn = .Attributes(EvoXMLInfo.dbColumnDetails).Value
				aNodeList = .ChildNodes
			End With
			maxCol = aNodeList.Count - 1
			For i = 1 To 100 'for each possible row
				buffer = CStr(Page.Request(String.Format("evoRO{0}", i)))
				If buffer <> String.Empty Then
					id = String2Int(buffer)
					CellValues = Split(buffer, "~!")
					buffer = CStr(Page.Request(String.Format("evoROC{0}", i)))
					If id > 0 Then
						If _DBAllowUpdateDetails Then
							If buffer <> String.Empty Then
								CellValuesOriginal = Split(buffer, "~!")
								sql1 = String.Empty
								If CellValues.Length = CellValuesOriginal.Length + 1 Then '+1 b/c no ID in original
									fid = -1
									For j = 0 To maxCol
										With aNodeList(j)
											If .Attributes(EvoXMLInfo.dbReadOnly) Is Nothing Then
												fieldIn = True
											Else
												buffer = .Attributes(EvoXMLInfo.dbReadOnly).Value
												fieldIn = Val(buffer) < 1
											End If
											If fieldIn Then
												fid += 1
												fieldType = .Attributes(EvoXMLInfo.type).Value
												fieldColumn = .Attributes(EvoXMLInfo.dbColumn).Value
												If fieldType = t_lov Then
													If Val(CellValues(fid + 1)) > 0 AndAlso CellValues(fid + 1) <> CellValuesOriginal(fid) Then
														sql1 += (New StringBuilder).Append(fieldColumn).Append("=").Append(CellValues(fid + 1)).Append(coma).ToString
													End If
												Else
													If CellValues(fid + 1) <> CellValuesOriginal(fid) Then
														If .Attributes(EvoXMLInfo.MaxLength) Is Nothing Then
															fieldMaxLength = 0
														Else
															fieldMaxLength = CInt(Val(.Attributes(EvoXMLInfo.MaxLength).Value))
														End If
														sql1 += (New StringBuilder).Append(fieldColumn).Append("=").Append(dbFormat(CellValues(fid + 1), fieldType, fieldMaxLength, _Language)).Append(coma).ToString
													End If
												End If
											End If
										End With
									Next
									If sql1.Length > 0 Then
										sql1 = sql1.Substring(0, sql1.Length - 1)
										SQL.Append(sqlUPDATE(dbTableDetails, sql1, String.Format("ID={0} AND {1}={2}", id, dbcolumn, _ItemID)) & ";")
									End If
								Else
									If CellValues.Length = 2 AndAlso CellValues(1) = "DEL" Then
										SQL.Append(sqlDELETE(dbTableDetails, String.Format("ID={0};", id)))
									Else
										AddError(String.Format("Invalid format for details ID #{0}", id))
									End If
								End If
							Else
								AddError(String.Format("Error updating details ID #{0}", id))
							End If
						Else
							Exit For
						End If
					Else
						If _DBAllowInsertDetails Then
							If Not String.IsNullOrEmpty(buffer) Then
								CellValuesOriginal = Split(buffer, "~!")
								If CellValues.Length = CellValuesOriginal.Length + 1 Then '+1 b/c no ID in original
									sql1 = String.Empty : sql2 = String.Empty
									For j = 0 To maxCol
										With aNodeList(j)
											If .Attributes(EvoXMLInfo.dbReadOnly) Is Nothing Then
												fieldIn = True
											Else
												fieldIn = Not (.Attributes(EvoXMLInfo.dbReadOnly).Value = "1")
											End If
											If fieldIn Then 
												fieldType = .Attributes(EvoXMLInfo.type).Value
												fieldColumn = .Attributes(EvoXMLInfo.dbColumn).Value
												fieldMaxLength = 0 ' CInt(.Attributes(Attr.MaxLength).Value)
												hasValue = (fieldType = t_lov AndAlso Val(CellValues(j + 1)) > 0) OrElse (CellValues(j + 1) <> String.Empty)
												If hasValue Then
													sql1 += String.Format("{0},", fieldColumn)
													sql2 += dbFormat(CellValues(j + 1), fieldType, fieldMaxLength, _Language) & coma
												End If
											End If
										End With
									Next
								End If
							End If
							If sql1.Length > 0 Then
								sql1 += dbcolumn
								sql2 += CStr(_ItemID)
								SQL.Append(sqlINSERT(dbTableDetails, sql1, sql2))
							End If
						End If
					End If
				End If
			Next
			Return SQL.ToString()
		End Function

		Private Function BuildSQLselect(ByRef Master As Boolean, ByRef MyMode As Integer, ByRef formid As Integer, ByRef Tsql As String, ByRef SPsql As String, ByVal Wsql As String, ByVal OBsql As String, Optional ByVal XPathMask As String = "", Optional ByVal TOPsql As Integer = 0, Optional ByVal dbcolumnpixDetails As String = "") As String	', Optional ByVal NoStar As Boolean = True) As String
			Dim i As Integer, maxLoopXML As Integer, buffer As String, fieldX As String
			Dim dbtablelov As String, PanelID As Integer
			Dim fieldType As String, fieldColumn As String, fieldColumnRead As String, fieldShows As Boolean = True, fieldTableID As Integer = 0
			Dim mySQL As New StringBuilder
			Dim Fsql As New StringBuilder
			Dim aNodeList As XmlNodeList

			If String.IsNullOrEmpty(XPathMask) Then
				If Master Then
					buffer = XPathQuery.panelField
				Else
					buffer = XPathQuery.panelDetailsField
				End If
			Else
				buffer = XPathMask
			End If
			aNodeList = myDOM.DocumentElement.SelectNodes(buffer, nsManager)
			maxLoopXML = aNodeList.Count - 1 
			If aNodeList.Count > 0 Then
				buffer = String.Format("{0} T", Tsql)
				Select Case _DisplayMode
					Case 71, 72	'71=export  72=export 1 rec / all fields 
						mySQL.AppendFormat(" T.{0} AS ID", def_Data.dbcolumnpk)
						If _DisplayMode = 72 Then
							If Master Then Wsql = CondiConcat(Wsql, String.Format("t.{0}={1}", def_Data.dbcolumnpk, _ItemID), SQL_and)
						End If
					Case Else
						If TOPsql > 0 Then
							mySQL.AppendFormat("TOP {0} T.", TOPsql)
						Else
							mySQL.AppendFormat("TOP {0} T.", _RowsPerPage)
						End If
						If Master Then
							mySQL.AppendFormat(def_Data.dbcolumnpk).Append(" as ID")
							If _DBAllowComments <> EvolCommentsMode.None Then mySQL.Append(",T.").Append(SQLnbComments)
							If dbcolumnpix.Length > 0 Then mySQL.Append(",T.").Append(dbcolumnpix)
							If _DBAllowUpdate AndAlso Not _DBReadOnly Then
								If _SecurityModel = EvolSecurityModel.Multiple_Users_Sharing Then
									mySQL.Append(",T.").Append(def_Data.dbcolumnuserid)
								End If
							End If
						Else
							mySQL.AppendFormat("ID")
							If Tsql = "-self-" Then
								buffer = def_Data.dbtable & " T"
								If dbcolumnpix.Length > 0 Then
									mySQL.Append(",T.").Append(dbcolumnpix)
								Else
									If dbcolumnpixDetails <> String.Empty Then mySQL.Append(",T.").Append(dbcolumnpixDetails)
								End If
							Else
								If dbcolumnpixDetails <> String.Empty Then mySQL.Append(",T.").Append(dbcolumnpixDetails)
							End If
						End If
				End Select
				Fsql.Append(buffer)
				fieldShows = True
				For i = 0 To maxLoopXML
					With aNodeList(i)
						fieldType = .Attributes(EvoXMLInfo.type).Value
						fieldColumn = .Attributes(EvoXMLInfo.dbColumn).Value
						If _DisplayMode = 71 Then	 'export generation
							fieldShows = CStr(Page.Request(UID & fieldColumn)) = "1"
							'Case 72 'export generation 1 rec all fields
							'    fieldShows = True
						Else
							If Not Master Then
								If .Attributes("panelid") Is Nothing Then
									PanelID = -1
								Else
									PanelID = CInt(.Attributes("panelid").Value)
								End If
								fieldShows = PanelID = formid
								If fieldShows Then
									Select Case MyMode
										Case 0, 1, 2 '0-view, 1-edit, 2-details
											fieldShows = True
										Case Else '100 list
											If .Attributes(EvoXMLInfo.SearchList) Is Nothing Then
												fieldShows = False
											Else
												fieldX = .Attributes(EvoXMLInfo.SearchList).Value
												fieldShows = Not (fieldX = String.Empty Or fieldX = "0")
											End If
									End Select
								End If
							End If
						End If
						If fieldShows AndAlso fieldColumn <> String.Empty Then
							Select Case fieldType
								Case t_lov
									fieldColumnRead = .Attributes(EvoXMLInfo.dbColumnRead).Value
									If fieldColumn.Length = 2 Then
										If fieldColumn.ToUpper <> "ID" Then
											mySQL.Append(",T.").Append(fieldColumn)
										End If
									Else
										mySQL.Append(",T.").Append(fieldColumn)
									End If
									If Not .Attributes(EvoXMLInfo.dbtablelov) Is Nothing Then
										dbtablelov = .Attributes(EvoXMLInfo.dbtablelov).Value
										If dbtablelov.Length > 0 Then
											Fsql.AppendFormat(" left join {0} on T.{1}={0}.ID", dbtablelov, fieldColumn)
											If Not .Attributes(EvoXMLInfo.dbwherelov) Is Nothing Then
												buffer = .Attributes(EvoXMLInfo.dbwherelov).Value
												If buffer.Length > 0 Then
													Fsql.Append(SQL_and).Append(buffer.Replace(p_userid, CStr(Val(_UserID))).Replace(p_itemid, CStr(Val(_ItemID))))
												End If
											End If
											If Not .Attributes(EvoXMLInfo.dbColumnImg) Is Nothing Then
												buffer = .Attributes(EvoXMLInfo.dbColumnImg).Value
												If buffer <> String.Empty Then mySQL.Append(coma).Append(dbtablelov).Append(".[Pix] AS ").Append(buffer)
											End If
											If .Attributes(EvoXMLInfo.dbcolumnreadlov) Is Nothing Then
												buffer = EvoXMLInfo.dbname
											Else
												buffer = .Attributes(EvoXMLInfo.dbcolumnreadlov).Value
												If String.IsNullOrEmpty(buffer) Then buffer = EvoXMLInfo.dbname
											End If
											mySQL.Append(coma).Append(dbtablelov).Append(dot).Append(buffer).Append(" AS ").Append(fieldColumnRead)
										End If
									End If
								Case t_formula
									fieldColumnRead = .Attributes(EvoXMLInfo.dbColumnRead).Value
									mySQL.Append(",(").Append(fieldColumn).Append(") AS ").Append(fieldColumnRead)
								Case t_url, t_doc
									If Not .Attributes(EvoXMLInfo.dbColumnImg) Is Nothing Then
										buffer = .Attributes(EvoXMLInfo.dbColumnImg).Value
										If buffer <> String.Empty Then mySQL.AppendFormat(",T.{0} AS {0}", buffer)
									End If
									mySQL.Append(",T.").Append(fieldColumn)
								Case Else
									mySQL.Append(",T.").Append(fieldColumn)
							End Select
						End If
					End With
				Next
				If Master Then
					buffer = BuildSQLwhereSecurity()
					If buffer.Length > 0 Then
						Wsql = CondiConcat(Wsql, buffer, SQL_and)
					End If
					If Not String.IsNullOrEmpty(def_Data.dbwhere) Then
						If String.IsNullOrEmpty(Wsql) Then
							Wsql = def_Data.dbwhere
						Else
							Wsql += String.Format(" AND ({0})", def_Data.dbwhere)
						End If
					End If
				End If
				''Locked parent
				'If dbwherelock <> String.Empty Then
				'    If sqlw <> String.Empty Then
				'        sqlw = dbwherelock & " AND (" & sqlw & ")"
				'    Else
				'        sqlw = " " & dbwherelock
				'    End If
				'End If  
				If _DBAllowComments <> EvolCommentsMode.None Then
					mySQL.Append(",T.").Append(SQLnbComments)
				End If
				If SPsql <> String.Empty Then
					'    'Query or stored procedure call
					Select Case MyMode
						Case 0, 1 'view, edit 
							buffer = SPcall_Get(def_Data.sppaging, _ItemID, _UserID)
							'Case 20 'details
						Case Else '100 list
							If String.IsNullOrEmpty(OBsql) Then
								If Not Master Then OBsql = "T.ID DESC"
							Else
								If Master AndAlso OBsql.ToUpper = "ID" Then
									OBsql = String.Format("T.{0} DESC", def_Data.dbcolumnpk)
								End If
							End If
							buffer = SPcall_Paging(def_Data.sppaging, mySQL.ToString, Fsql.ToString, Wsql, OBsql, def_Data.dbcolumnpk, pageID, _RowsPerPage, , Tsql)
					End Select
				Else
					buffer = BuildSQL(mySQL.ToString, Fsql.ToString, Wsql, OBsql)
				End If
			Else
				ErrorMsg += HTMLtextMore("<p>Invalid definition", XPathQuery.XPath2True(EvoXMLInfo.SearchList) & """ must return at least one element.")
			End If
			Return buffer
		End Function

		Private Function GetData(ByRef SQL As String, ByRef mySqlConnection As String) As DataSet
			'Run query and returns DataSet
			Dim myConnection As New SqlConnection(mySqlConnection)
			Dim myCommand As New SqlDataAdapter(SQL, myConnection)
			Dim ds As New DataSet

			Try
				myCommand.Fill(ds, SQL)
				Return ds
			Catch DBerror As Exception
				ErrorMsg += HTMLtextMore(LG_NoQuery, DBerror.Message)
				Return Nothing
			Finally
				myCommand.Dispose()
				myConnection.Dispose()
			End Try
		End Function

		Private Function GetDataParameters(ByRef SQL As String, ByRef mySqlConnection As String, ByRef P1 As SqlParameter, Optional ByVal P2 As SqlParameter = Nothing) As DataSet
			Dim myConnection As New SqlConnection(mySqlConnection)
			Dim myCommand As New SqlDataAdapter(SQL, myConnection)
			Dim ds As New DataSet

			Try
				With myCommand.SelectCommand
					.Parameters.Add(P1)
					If Not P2 Is Nothing Then .Parameters.Add(P2)
				End With
				myCommand.Fill(ds, SQL)
				Return ds
			Catch DBerror As Exception
				AddError(HTMLtextMore(LG_NoQuery, DBerror.Message))
				Return Nothing
			Finally
				myCommand.Dispose()
				myConnection.Dispose()
			End Try
		End Function

#End Region

		'### JavaScript ######################################################################################
#Region "JavaScript"

		Private Sub JSRegisterScripts()
			If _DBAllowDesign Then
				Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "EvoDico", String.Format("<script src=""{0}EvoDico.js"" defer=""defer"" type=""text/javascript""></script>", _PathPixToolbar))
				Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "DHTML Window Widget", String.Format("<script src=""{0}dhtmlwindow.js"" defer=""defer"" type=""text/javascript""></script>", _PathPixToolbar))
			End If
			Dim sbJS As StringBuilder = New StringBuilder
			sbJS.AppendFormat("<script type=""text/javascript"" src=""{0}EvolUtility.js""></script><script type=""text/javascript"">var EvolPATH='{0}',", _PathPixToolbar)
			sbJS.AppendFormat("EvolLANG='{0}',", _Language)
			sbJS.Append("EvPost=function(EvEvent){").Append(JSPostBack("%$#@").Replace("'%$#@'", "EvEvent")).Append("};</script>")
			Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "EvolUtility", sbJS.ToString)
		End Sub

		Private Function JSValidation() As String
			'fields validation for edit (required + formats email, date... ) +image upload
			Dim myScript As New StringBuilder
			Dim aNodeList As XmlNodeList
			Dim aField As StringBuilder
			Dim allFields As StringBuilder = New StringBuilder
			Dim buffer As String, fieldID As String, fieldLabel As String, fieldType As String
			Dim i As Integer, maxLoop As Integer, NeedValidation As Boolean

			'can be cached!!! or replaced
			'prob w/ tabs for focus?
			If XMLloaded Then
				myScript.Append("function evoValidForm(z){evoJS82();")
				aNodeList = myDOM.DocumentElement.SelectNodes(XPathQuery.panelField, nsManager)
				maxLoop = aNodeList.Count - 1
				allFields.Append("[")
				For i = 0 To maxLoop
					With aNodeList(i)
						'--- not ReadOnly
						If .Attributes(EvoXMLInfo.dbReadOnly) Is Nothing Then
							buffer = String.Empty
						Else
							buffer = .Attributes(EvoXMLInfo.dbReadOnly).Value
						End If
						Select Case buffer
							Case "", "0" : NeedValidation = True
							Case "1" : NeedValidation = False
							Case "2" : NeedValidation = _ItemID < 1
						End Select
						If NeedValidation Then
							fieldType = .Attributes(EvoXMLInfo.type).Value
							If fieldType <> t_bool Then
								fieldLabel = .Attributes(EvoXMLInfo.label).Value
								If String.IsNullOrEmpty(fieldLabel) Then
									If Not .Attributes(EvoXMLInfo.labelEdit) Is Nothing Then
										fieldLabel = .Attributes(EvoXMLInfo.labelEdit).Value
									End If
									If String.IsNullOrEmpty(fieldLabel) AndAlso Not .Attributes(EvoXMLInfo.labelList) Is Nothing Then
										fieldLabel = .Attributes(EvoXMLInfo.labelList).Value
									End If
								End If
								aField = New StringBuilder
								If InStr(fieldLabel, "'") > 0 Then fieldLabel = fieldLabel.Replace("'", "\'")
								'--- Required
								If .Attributes(EvoXMLInfo.required) Is Nothing Then
									NeedValidation = False
								Else
									NeedValidation = .Attributes(EvoXMLInfo.required).Value = "1"
								End If
								fieldID = UID & .Attributes(EvoXMLInfo.dbColumn).Value
								If NeedValidation Then
									aField.Append(",r:1")
								End If
								'--- Custom validation
								'If Not .Attributes(Attr.Script) Is Nothing Then
								'    aField.Append(",c:").Append(.Attributes(Attr.Script).Value)
								'    myScript.Append("if(").Append(.Attributes(Attr.Script).Value.Replace("@fieldid", """" & fieldID & """").Replace("@fieldlabel", """" & fieldLabel & """")).Append(")return false;")
								'End If
								'--- Type Validation (after b/c null OK)
								Select Case fieldType
									Case t_date, t_time, t_datetime, t_email
										aField.Append(" ") 'to force type validation
									Case t_int, t_dec
										'min value
										If Not .Attributes("min") Is Nothing Then
											buffer = .Attributes("min").Value
											If IsNumeric(buffer) Then
												aField.Append(",min:").Append(buffer)
											End If
										End If
										'max value
										If Not .Attributes("max") Is Nothing Then
											buffer = .Attributes("max").Value
											If IsNumeric(buffer) Then
												aField.Append(",max:").Append(buffer)
											End If
										End If
								End Select
								If aField.Length > 0 Then
									allFields.Append("{id:'").Append(fieldID).Append("',l:'").Append(fieldLabel).Append("',t:'")
									allFields.Append(fieldType).Append("'").Append(aField).Append("},")
								End If
							End If
						End If
					End With
				Next
				myScript.Append("if(Evol.checkAllFields(").Append(allFields).Append("{}])){EvPost((z==9)?'25':'24')}else{return false};")
				myScript.Append("};")
			End If
			JSValidDone = True
			Return myScript.ToString
		End Function

		Private Function JSEditDetails(ByRef aNodeList As XmlNodeList) As String
			Dim js As New StringBuilder, js2 As New StringBuilder, nbFields As Integer = 0
			Dim i As Integer, ColumnFocusFirst As Integer = 0, buffer As String, fieldName As String, fieldType As String, fieldFormat As String, colIdx As String, fieldReadOnly As Boolean = False
			Dim jsCells As String, jsInnerText As String
			Const jsInnerHTML As String = "innerHTML"

			If IEbrowser Then '
				jsCells = "cells"
				jsInnerText = "innerText"
			Else
				jsCells = "cells.item"
				jsInnerText = "textContent"
			End If
			'already checked  _DBAllowUpdateDetails and _DBAllowInsertDetails  before calling function !
			js.Append("var evoRLc=-1,evoRL=null;")

			js.Append("function evoEditRow(rIX){if(evoRLc==rIX)return;var jbuff,RF='',RFCE='',sep='~!',")
			js.Append("obj=$('EvoEditGrid').rows.item(rIX);if(evoRLc>0&&evoRLc!=rIX)evoJS82();")
			js.Append("if(evoRLc!=rIX){Evol.setSelected(obj,true);evoRL=obj;evoRLc=rIX;")
			js2.AppendFormat("RVs=evoRL.{0}(0).{1}+sep;", jsCells, jsInnerText)
			nbFieldEditable = 0	'nbFieldEditable is a global variable
			nbFields = aNodeList.Count - 1 'nbFields is a global variable
			For i = 0 To nbFields 'for each field
				If aNodeList(i).Attributes(EvoXMLInfo.dbReadOnly) Is Nothing Then
					fieldReadOnly = False
				Else
					buffer = aNodeList(i).Attributes(EvoXMLInfo.dbReadOnly).Value
					fieldReadOnly = Val(buffer) > 0
				End If
				If Not fieldReadOnly Then
					nbFieldEditable += 1
					With aNodeList(i)
						fieldName = .Attributes(EvoXMLInfo.dbColumn).Value
						fieldType = .Attributes(EvoXMLInfo.type).Value
					End With
					colIdx = (i + 1).ToString.TrimStart
					Select Case fieldType
						Case t_lov
							js.AppendFormat("jbuff=obj.{1}({0}).intnum;obj.{1}({0}).{2}=[Evol.bLOV,'{0}""><option value=""',jbuff,'"" selected>',obj.{1}({0}).{2},'</option>", colIdx, jsCells, jsInnerHTML)
							js.Append(HTMLlov(aNodeList(i)).Replace("'", "\'")).Append("</select>'].join("""");RF+=jbuff+sep;")
							js2.AppendFormat("myDList=$('evolGFE{0}');RFCE=evoRL.cells.item({0});", colIdx)
							js2.Append("RFCE.intnum=myDList.value;RVs+=myDList.value+sep;RFCE.innerHTML=myDList.options[myDList.selectedIndex].text;")
							If ColumnFocusFirst < 1 Then ColumnFocusFirst = i + 1
						Case t_date, t_datetime	', t_time 
							js2.AppendFormat("updateDateField('evolGFE{0}');", colIdx)
							js.AppendFormat("jbuff=obj.{1}({0}).{2};obj.{1}({0}).innerHTML=", colIdx, jsCells, jsInnerText)
							js2.AppendFormat("jFV=$('evolGFE{0}').value;evoRL.{1}({0}).{2}=jFV;RVs+=jFV+sep;", colIdx, jsCells, jsInnerText)
							js.AppendFormat("['<nobr>',Evol.bDate,'{0}"" value=""',jbuff,'"" name=""evolGFE{0}"">',Evol.bCalendar('evolGFE{0}'),'</nobr>'].join('');", colIdx)
							js.AppendFormat("RF+=jbuff+sep;")
							If ColumnFocusFirst < 1 Then ColumnFocusFirst = i + 1
							''Case t_int
							''js.Append("'<input type=""text"" ").Append(sStyleFields).Append(" style=""width:100%"" id=""evolGFE").Append(colIdx).Append(""" value=""' + obj.cells.item(").Append(colIdx).Append(").innerText + '"" onkeyup=""evoJS59(this)"">';")
							''js2.Append("evoRL.cells.item(").Append(colIdx).Append(").innerText=").Append(jsGetElem).Append("('evolGFE").Append(colIdx).Append("').value;")
							''If ColumnFocusFirst < 1 Then ColumnFocusFirst = i + 1
							''    "if (isNaN(aField.value)) {"
							'Case t_pix, t_doc, "hidden" '?????
							'    js.Append("obj.cells.item(").Append(colIdx).Append(").innerHTML='';")
							'    js2.Append("RVs+=sep;")
						Case t_bool
							js.AppendFormat("jbuff=Evol.bBool+'{0}""';if(obj.{1}({0}).{2}!='')", colIdx, jsCells, jsInnerText)
							js.Append("{jbuff+=' checked';RF+='1'+sep}else{RF+='0'+sep};obj.cells.item")
							js.AppendFormat("({0}).innerHTML=jbuff+'>';", colIdx)
							If aNodeList(i).Attributes(EvoXMLInfo.Img) Is Nothing Then
								fieldFormat = _PathPixToolbar & pix_check
							Else
								fieldFormat = aNodeList(i).Attributes(EvoXMLInfo.Img).Value
								If fieldFormat <> String.Empty Then fieldFormat = _PathPixToolbar & fieldFormat
							End If
							js2.Append("if($('evolGFE").Append(colIdx).Append("').checked){")
							js2.Append("evoRL.cells.item(").Append(colIdx).Append(").innerHTML='&nbsp;<img src=""").Append(fieldFormat).Append(""" alt=""Checked""/>';RVs+='1'+sep}else{evoRL.cells.item(").Append(colIdx).Append(").innerText='';RVs+='0'+sep};")
							If ColumnFocusFirst < 1 Then ColumnFocusFirst = i + 1
						Case Else ' t_text, t_url, t_email, t_date, t_datetime, t_time, t_int, t_dec, t_html, t_txtm   
							'if(aField.value!=''){if(isNaN(aField.value)){
							js.AppendFormat("jbuff=obj.{1}({0}).{2};obj.{1}({0}).innerHTML", colIdx, jsCells, jsInnerText)
							js.AppendFormat("=[Evol.bText,'{0}"" value=""',jbuff,'"">'].join("""");RF+=jbuff+sep;", colIdx)
							js2.AppendFormat("jFV=$('evolGFE{0}').value;evoRL.{1}.item({0}).{2}=jFV;RVs+=jFV+sep;", colIdx, jsCells, jsInnerText)
							If ColumnFocusFirst < 1 Then ColumnFocusFirst = i + 1
					End Select
					'If ColumnFocusFirst < 1 Then ColumnFocusFirst = i + 1
				End If
			Next
			If nbFieldEditable > 0 Then
				With js
					.Append("Evol.setRowInfo(evoRLc,RF);")
					If ColumnFocusFirst > 0 Then
						.Append("if(evoRLc>-1){Evol.focus('evolGFE").Append(ColumnFocusFirst).Append("',1)};")
					End If
					.Append("}};")

					.Append("function evoJS82(){var myDList,RVs='',sep='~!';if(evoRLc>0){Evol.setSelected(evoRL,0);")
					.Append(js2)
					.Append("$('evoRO'+evoRLc).value=RVs;evoRLc=-1;$('evolNUD').value='1';}return 1};")

					''-KeyUp
					'.Append(functionJS).Append("59(){var i=0,PR=evoRLc;switch(window.event.keyCode){")
					'.Append("case 38:if(PR>1)i=PR-1;break;case 40:if(PR<50)")
					'.Append("if(i>0)evoEditRow(i);}};")

					''-AddRow
					If _DBAllowInsertDetails Then
						.Append("function evoAddRow(){var tt=$('EvoEditGrid')")
						If IEbrowser Then
							.Append(".tBodies(0)")
						End If
						.Append(",i=tt.rows.length;if(i<100){tr=tt.insertRow(i);i++;tr.setAttribute('id','r'+i);")
						.Append("Evol.addRowCells(tr,").Append(nbFields + 2).Append(");")
						.Append("Evol.addRowInfo(i);")
						If IEbrowser Then
							.Append("tr['onclick']=new Function('evoEditRow('+i+')');")
						Else
							.Append("tr.setAttribute('onclick','evoEditRow('+i+')');")
						End If
						.Append("evoEditRow(i)}};")
						'evoRLc+=1;
						' .Append("tt=tt.rows.item(i)};evoRLc+=1;")
					End If
					''-DelRow must keep it but makes it invisible
					If _DBAllowDelete Then
						.Append("function evoDelRow(){if(evoRLc>-1){$('evoRO'+evoRLc).value=")
						If IEbrowser Then
							.Append("evoRL.cells(0).innerText+'~!DEL';evoRL.innerText='';")
						Else
							.Append("evoRL.cells.item(0).textContent+'~!DEL';evoRL.textContent='';")
						End If
						.Append("evoRL.style.display='none';evoRLc=-1;$('evolNUD').value='1';}}")
					End If
				End With
				JSDetailsDone = True
				Return js.ToString
			Else
				JSDetailsDone = False
				Return String.Empty
			End If
		End Function

		'Private Function JSFields(Optional ByVal JStag As Boolean = False) As String
		'	Dim allFields As StringBuilder = New StringBuilder
		'	Dim buffer As String, fieldID As String, fieldLabel As String
		'	Dim i As Integer, maxLoop As Integer
		'	Dim aNodeList As XmlNodeList

		'	If XMLloaded Then
		'		If JStag Then
		'			allFields.Append(JSscriptBegin)
		'		End If
		'		allFields.Append("Evol_formFields=")
		'		aNodeList = myDOM.DocumentElement.SelectNodes(XPathQuery.panelField, nsManager)
		'		maxLoop = aNodeList.Count - 1
		'		allFields.Append("[")
		'		For i = 0 To maxLoop
		'			With aNodeList(i)
		'				fieldID = UID & .Attributes(EvoXMLInfo.dbColumn).Value
		'				fieldLabel = .Attributes(EvoXMLInfo.label).Value
		'				allFields.Append("{id:'").Append(fieldID).Append("',l:'").Append(fieldLabel).Append("',t:'")
		'				allFields.Append(.Attributes(EvoXMLInfo.type).Value).Append("'")
		'				If Not .Attributes(EvoXMLInfo.dbReadOnly) Is Nothing Then
		'					buffer = .Attributes(EvoXMLInfo.dbReadOnly).Value
		'					allFields.Append(",r:").Append(buffer)
		'				End If
		'				allFields.Append(",s:").Append(.Attributes(EvoXMLInfo.Search).Value)
		'				allFields.Append("},")
		'			End With
		'		Next
		'		allFields.Append("{}];")
		'		If JStag Then
		'			allFields.Append(JSscriptEnd)
		'		End If
		'	End If
		'	Return allFields.ToString
		'End Function

		Private Function JSPostBack(ByRef EventParam As String) As String
			Return Page.ClientScript.GetPostBackEventReference(Me, EventParam)
		End Function

#End Region

		'### Comments & Rating ###############################################################################
#Region "Comments & Rating"

		Private Function FormComments(ByRef MaxLoopSQL As Integer) As String
			'display comments list and new comment form
			Dim Buffer As String, YesNo As Boolean, PanelGUIname As String = "evoCOMcfz"
			Dim i As Integer, PanelID As Integer = -1
			Dim myHTML As New StringBuilder

			myHTML.Append("<table class=""Panel"" width=""100%"" cellpadding=""5"" cellspacing=""0""><tr><td><p>")
			If atRunTime Then
				YesNo = _DBAllowComments.Equals(EvolCommentsMode.Logged_Users) AndAlso _UserID > 0
			Else
				YesNo = Not _DBAllowComments.Equals(EvolCommentsMode.None)
			End If
			If YesNo Then
				With myHTML
					.Append("<table cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0""><tr><td>")
					If MaxLoopSQL > -1 Then
						.Append(PixComments).AppendFormat(LG_cmNb, CStr(MaxLoopSQL + 1), entity).Append(" ")
						Buffer = LG_cmAdd
					Else
						.AppendFormat(LG_cmNo, entity).Append(" ")
						Buffer = LG_cmPost
					End If
					.Append(HTMLLinkShowVanish(PanelGUIname, Buffer))
					.Append(HTMLDiv(PanelGUIname, False)).Append("<hr>")
					.Append(HTMLFieldLabelResize("EVOLComPost", LG_cmMy))
					.Append(HTMLInputTextArea("EVOLComPost", "", sStyleFields, "4")).Append(BR_tag)
					.Append(HTMLInputButton("puc", LG_Post, False, String.Format("EvPost('{0}')", _DisplayMode)))
					.Append("<br/></div>")
					.Append(TdTrTableEnd)
				End With
				''list of comments
				If Not noCommentsHere Then
					If MaxLoopSQL > -1 Then
						If Not ds2 Is Nothing Then
							PanelID = ds2.Tables.Count - 1
							'MaxLoopSQL = ds2.Tables(PanelID).Rows.Count - 1
						End If
						If PanelID > -1 Then 
							For i = 0 To MaxLoopSQL
								myHTML.Append("<hr><font class=""FieldReadOnly"">")
								Try
									With ds2.Tables(PanelID).Rows(i)
										myHTML.Append(Text2HTMLwBR(CStr(.Item("message")).Trim))
										myHTML.Append("<br/>")
										myHTML.Append(LG_cmFrom).Append("<a href=""").Append(def_Data.userpage).Append("?ID=").Append(.Item("userid")).Append(""">").Append(.Item("login")).Append("</a>")
										myHTML.Append(LG_cmOn).Append(formatedDateTime(CDate(.Item("creationdate")))).Append(".</font>")
									End With
								Catch
									myHTML.Append(LG_cmMissing).Append("</font>")
									Exit For
								End Try
							Next
						End If
					End If
				End If
			End If
			myHTML.Append(TdTrTableEnd)
			Return myHTML.ToString
		End Function

		Private Sub PostUserComments()
			Dim buffer As String, aSQL As String

			buffer = Page.Request("EVOLComPost")
			If Not String.IsNullOrEmpty(buffer) Then
				aSQL = String.Format("{0},{1},{2}", _ItemID, _UserID, dbFormat(buffer, t_txtm, 1000, _Language))
				If CStr(Page.Cache("LASTCOM")) <> aSQL Then
					Page.Cache("LASTCOM") = aSQL
					If def_Data.dbcommentsformid > 0 Then
						aSQL = sqlINSERT(def_Data.dbtablecomments, def_Data.dbcolumncomments & ",userid,message,formid", aSQL & coma & CStr(def_Data.dbcommentsformid))
					Else
						aSQL = sqlINSERT(def_Data.dbtablecomments, def_Data.dbcolumncomments & ",userid,message", aSQL)
					End If
					aSQL += sqlUPDATE(def_Data.dbtable, String.Format("{0}={0}+1", SQLnbComments), String.Format("ID={0}", Val(_ItemID)))
					buffer = RunSQL(aSQL, _SqlConnection, True)
					If String.IsNullOrEmpty(buffer) Then
						HeaderMsg = CondiConcat(HeaderMsg, String.Format(LG_CommentsPostedOn, Now()), vbCrLf)
					Else
						ErrorMsg = buffer
					End If
				End If
			End If
		End Sub

#End Region

		'### Menus ###########################################################################################
#Region "Menus"

		Friend Function HTMLmenu(Optional ByVal SecondIteration As Boolean = False) As String
			'HTML for the icon toolbar
			Dim zHTML As New StringBuilder, YesNo As Boolean = True
			Dim buffer As String, sep As String, aNumString As String

			With zHTML
				If _ToolbarPosition <> EvolToolbarPosition.None Then
					'--- heading menu
					.Append("<div class=""Toolbar""><nobr>")
					If IEbrowser Then
						.Append("<span class=""nav0""></span>")
					End If
					sep = "<b>|</b>"
					'--- db menus
					If Not _DBReadOnly Then
						If _DBAllowInsert Then
							.Append(menuItem(LG_New, "12", "new", _DBAllowInsert))
						End If
						If _DBAllowUpdate Then
							If _ItemID > 0 Then	'item selected
								If _UserID > 0 AndAlso _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_Sharing) Then
									Try
										buffer = CStr(ds.Tables(0).Rows(0)(def_Data.dbcolumnuserid))
									Catch : buffer = String.Empty : End Try
									If String2Int(buffer) <> _UserID Then
										YesNo = False
									End If
								End If
								If _DisplayMode = 0 Then 'view
									.Append(menuItem(LG_Edit, "1", "edit", YesNo))
								Else 'edit
									.Append(menuItem(LG_View, "0", "view", YesNo))
								End If
							Else
								.Append(menuItem(LG_Edit, "1", "edit", False))
							End If
						End If
						If _DBAllowDelete Then
							If (_ItemID > 0 OrElse nav > 0) AndAlso (_DisplayMode = 0 OrElse _DisplayMode = 1 OrElse _DisplayMode = 101) Then
								If _SecurityModel <> EvolSecurityModel.Multiple_Users_Sharing Then
									YesNo = True
								Else
									If _UserID > 0 Then	'user is logged in 
										Try
											buffer = CStr(ds.Tables(0).Rows(0)(def_Data.dbcolumnuserid))
										Catch : buffer = String.Empty : End Try
										YesNo = String2Int(buffer) = _UserID
									Else
										YesNo = False
									End If
								End If
							Else
								YesNo = False
							End If
							If YesNo Then
								'.Append(menuItem(LG_Delete, "javascript:if(confirm('" & String.Format(LG_DeleteEntity, entity) & "'))EvPost('10')", "del", YesNo, False))
								.Append(menuItem(LG_Delete, String.Format("javascript:Evol.deleteItem('{0}')", entity), "del", YesNo, False))
							Else
								.Append(menuItem(LG_Delete, "#", "del", False, False))
							End If
						End If
						.Append(sep)
					End If
					'--- other menus
					If _DBAllowSearch Then
						If _DisplayMode = 3 Then
							.Append(menuItem(LG_Search, "4", "searchp")) 'adv search
						Else
							.Append(menuItem(LG_Search, "3", "search"))	'search
							'.Append(menuItemSearch(LG_Search, "3", "search"))	'search - showViewSearch
						End If
					End If
					If _DBAllowSelections Then .Append(menuItem(LG_Selections, "60", "sel"))
					.Append(menuItem(LG_ListAll, "110", "all"))
					If _DBAllowPrint Then
						.Append(sep)
						.Append(menuItem(LG_Print, "javascript:Evol.print()", "print", , False))
					End If 
					If _DBAllowDesign AndAlso Not _ShowDesigner Then ' Customize
						.Append(sep).Append(menuItemCustomize(_DisplayMode)) 
					End If
					''--- login / logout
					If _UserID > 0 Then
						.Append(sep).Append(menuItem(LG_Logout, "49", "logout"))
					End If
					'--- prev, next... (only for edit and view mode) 
					If Not _DisplayMode > 2 Then
						If IEbrowser Then
							.Append("<span class=""nav9""></span>")
						Else
							.Append("<span class=""nav9ff"">&nbsp;</span>")
						End If
						.Append("<span class=""nav"">")
						If Not def_Data Is Nothing AndAlso String.IsNullOrEmpty(def_Data.spget) Then
							If _DisplayMode = 1 Then aNumString = "30" Else aNumString = "20"
							buffer = "<a href=""javascript:EvPost('{0}{1}')"" class=""ico nav{1}""></a>"
							If nav = 1 OrElse navd = 1 Then
								.Append("<span class=""nav1""></span><span class=""nav2""></span>")
								navBar = "0"
							Else
								.AppendFormat(buffer, aNumString, "1")
								.AppendFormat(buffer, aNumString, "2")
								navBar = "1"
							End If
							If nav = 4 OrElse navd = 3 Then
								.Append("<span class=""nav3""></span><span class=""nav4""></span>")
								navBar += "0"
							Else
								.AppendFormat(buffer, aNumString, "3")
								.AppendFormat(buffer, aNumString, "4")
								navBar += "1"
							End If
						End If
						.Append("</span>")
					End If
					'--- footer menu
					.Append("</nobr></div>") 
				End If
				'--- extra stuff
				If Not SecondIteration Then
					If _ShowTitle Then
						.Append("<table class=""Panel"" width=""100%"" cellpadding=""3"" cellspacing=""0"" border=""0""><tr><td class=""PanelLabel Header"">")
						If _ItemID > 0 AndAlso Not String.IsNullOrEmpty(def_Data.dbcolumnlead) Then
							If _DisplayMode = 0 OrElse _DisplayMode = 1 Then
								'--- show lead field above tabs
								Try
									.Append(CStr(ds.Tables(0).Rows(0).Item(def_Data.dbcolumnlead)))
								Catch : End Try
							Else
								.Append(GetAppTitle())
							End If
						Else
							.Append(GetAppTitle())
						End If
						If _ShowDesigner Then
							If _DisplayMode = 3 Then
								.Append(LinkDesigner("SD", _FormID, GetAppTitle()))
							Else
								.Append(LinkDesigner("FAD", _FormID, GetAppTitle()))
							End If
						End If
						.Append(HTMLSpace)
						Select Case DisplayMode
							Case EvolDisplayMode.Search
								buffer = HTMLLinkEventRef("4", LG_AdvSearch)
							Case EvolDisplayMode.AdvancedSearch
								buffer = HTMLLinkEventRef("3", LG_Search)
							Case Else
								buffer = String.Empty
						End Select
						If buffer <> String.Empty Then
							.Append("</td><td class=""PanelLabel""><p class=""right"">").Append(buffer).Append("</p>")
						End If
						.Append(TdTrTableEnd)
					End If
				End If
			End With
			Return zHTML.ToString
		End Function


		Friend Function menuItemCustomize(ByRef DisplayMode As Integer) As String
			'HTML for a single menu item
			Dim htmlTR As New StringBuilder
			Dim label As String = LG_Customize, pix As String = "customize", enabled As Boolean = True, UseEventRef As Boolean = True

			If enabled Then
				If UseEventRef Then
					htmlTR.AppendFormat(HTMLLinkEventRef("c:" + DisplayMode.ToString, label, pix))	'"<a href=""javascript:EvPost('c:{0}')"" class=""{1}"">", DisplayMode, pix)
				Else
					htmlTR.AppendFormat("<a href=""{0}"" class=""{1}"">", DisplayMode, pix).Append(label).Append(" </a>")
				End If 
			Else
				htmlTR.AppendFormat("<span class=""{0}"">", pix).Append(label).Append(" </span>")
			End If
			Return htmlTR.ToString
		End Function

		Private Function ModeName() As String
			Dim buffer As StringBuilder = New StringBuilder

			If atRunTime Then
				Select Case _DisplayMode
					Case 0 : buffer.Append(LG_View).Append(" ").Append(entity)
					Case 1 : If _ItemID = 0 Then buffer.Append(LG_New).Append(" ").Append(entity) Else buffer.Append(LG_Edit).Append(" ").Append(entity)
					Case 3 : buffer.Append(LG_Search).Append(" ").Append(entities)
					Case 4 : buffer.Append(LG_AdvSearch).Append(" ").Append(entities)
					Case 60 : buffer.Append(TextUcaseFirst(entity)).Append(" ").Append(LG_Selections)
					Case 70 : buffer.Append(LG_Export).Append(" ").Append(entities)
					Case 102 To 110 : buffer.Append(TextUcaseFirst(entity)).Append(" ").Append(LG_SearchRes)
					Case Else : buffer.Append(String.Empty)
				End Select
			Else
				Select Case _DesignDisplayMode
					Case EvolDisplayMode.View : buffer.Append(LG_View).Append(" ").Append(entity) 'String.Format("{0} {1}", LG_View, entity)
					Case EvolDisplayMode.Edit : buffer.Append(LG_Edit).Append(" ").Append(entity)
					Case EvolDisplayMode.Search : buffer.Append(LG_Search).Append(" ").Append(entities)
					Case EvolDisplayMode.AdvancedSearch : buffer.Append(LG_AdvSearch).Append(" ").Append(entities)
					Case EvolDisplayMode.Selections : buffer.Append(TextUcaseFirst(entity)).Append(" ").Append(LG_Selections)
					Case EvolDisplayMode.Export : buffer.Append(LG_Export).Append(" ").Append(entities)
					Case Else : buffer.Append(TextUcaseFirst(entity)).Append(" ").Append(LG_SearchRes)
				End Select
			End If
			Return buffer.ToString
		End Function

		Private Function LinkDesigner(ByRef DesignerType As String, ByRef ItemID As Integer, ByVal ItemLabel As String) As String
			Return String.Format(" <a href=""Javascript:EvoDico.edit('{0}','{3}EvoDes{0}.aspx?ID={1}','{4}')"">{2}p-{0}.gif""></a>", DesignerType, ItemID, imgProperties, _PathDesign, HtmlEncode(ItemLabel))
		End Function

		Private Function GetAppTitle() As String
			If (def_Data Is Nothing OrElse String.IsNullOrEmpty(def_Data.title)) Then
				Return ModeName()
			Else
				Return String.Format("<span id=""EVOL_Title"">{0} : {1}</span>", def_Data.title, ModeName())
			End If
		End Function
#End Region

		'### Dico ############################################################################################
#Region "Dico"

		Friend Function dicoDB2XML(ByRef FormID As Integer, ByRef UserID As Integer, ByRef sqlConnectionDico As String) As String
			Dim sql As String, MaxLoop As Integer, i As Integer, j As Integer
			Dim UseTabs As Boolean = False, UseDetails As Boolean = False
			Dim ds As New DataSet

			If atRunTime Then
				'sqlw = "ID=" & CStr(FormID)
				'sql = BuildSQL("*", "EvoDico_xForm", sqlw)
				'sql += BuildSQL("*", "EvoDico_xData", sqlw)
				'sqlw = "FormID=" & CStr(FormID)
				'sql += BuildSQL("*", "EvoDico_xPanel", sqlw)
				'sql += BuildSQL("*", "EvoDico_xField", sqlw)
				'sql += BuildSQL("*", "EvoDico_xTab", sqlw)
				'sql += BuildSQL("*", "EvoDico_xPanelDetail", sqlw)
				'sql += BuildSQL("*", "EvoDico_xFieldDetail", sqlw)
				''     EvoDico_xActions
				sql = String.Format("EXEC EvoDico_Form_Get @FormID, {0}", p_userid)

				ds = GetDataParameters(sql, sqlConnectionDico, New SqlParameter("@FormID", FormID), New SqlParameter(p_userid, UserID))
				If Not ds Is Nothing Then
					'built hierarchical relationships between recordsets
					ds.DataSetName = "EvoL"
					MaxLoop = ds.Tables.Count - 1
					If MaxLoop > -1 Then
						''UseTabs = False
						''UseDetails = False
						''Select Case MaxLoop
						''    Case 4 'tabs - no details
						''        UseTabs = True
						''    Case 5 'no tabs - details
						''        UseDetails = True
						''    Case Is > 5 'tabs - details
						''        UseTabs = True
						''        UseDetails = True
						''End Select
						With ds
							.Tables(0).TableName = "form"
							.Tables(0).Namespace = evoLink
							.Tables(1).TableName = data_str
							.Tables(2).TableName = "panel"
							.Tables(3).TableName = field_str
							''If UseTabs Then
							''    .Tables(4).TableName = "tab"
							''    If UseDetails Then
							''        .Tables(5).TableName = "paneldetails"
							''        .Tables(6).TableName = "fielddetails"
							''    End If
							''Else
							''    If UseDetails Then
							''        .Tables(4).TableName = "paneldetails"
							''        .Tables(5).TableName = "fielddetails"
							''    End If
							''End If
						End With
						For i = 0 To MaxLoop
							With ds.Tables(i)
								For j = 0 To .Columns.Count - 1
									.Columns(j).ColumnName = LCase(.Columns(j).ColumnName)
									.Columns(j).ColumnMapping = MappingType.Attribute
								Next
								If i <> 3 Then
									.Columns("id").ColumnMapping = MappingType.Hidden
								End If
								Select Case i
									Case 2	'  panel
										.Columns("typeid").ColumnMapping = MappingType.Hidden
										.Columns("formid").ColumnMapping = MappingType.Hidden
										.Columns("tabid").ColumnMapping = MappingType.Hidden
									Case 3 'field
										'.Columns("options").ColumnMapping = MappingType.Hidden
										'.Columns("form").ColumnMapping = MappingType.Hidden
										.Columns("formid").ColumnMapping = MappingType.Hidden
										.Columns("panelid").ColumnMapping = MappingType.Hidden
										.Columns("userid").ColumnMapping = MappingType.Hidden
										'.Columns("typeid").ColumnMapping = MappingType.Hidden
										.Columns("typepix").ColumnMapping = MappingType.Hidden
										'Case 0 'form
										'    .Columns("userid").ColumnMapping = MappingType.Hidden
										'    .Columns("form").ColumnMapping = MappingType.Hidden
										'Case 4   'tab 
										'    If UseTabs Then
										'        .Columns("paneltypeid").ColumnMapping = MappingType.Hidden
										'    End If
								End Select
								'If i > 1 Then .Columns("formid").ColumnMapping = MappingType.Hidden
								'If i > 3 And i < 5 Then .Columns("panelid").ColumnMapping = MappingType.Hidden
							End With
						Next
						''ds.Tables("paneldetails").Columns("id").ColumnMapping = MappingType.Attribute
						''ds.Tables("paneldetails").Columns("id").ColumnName = "panelid"
						With ds
							Dim rel1 As DataRelation = .Relations.Add("form-data", .Tables("form").Columns("id"), .Tables(data_str).Columns("id"))
							rel1.Nested = True
							Dim rel2 As DataRelation = .Relations.Add("panel-field", .Tables("panel").Columns("id"), .Tables(field_str).Columns("panelid"))
							rel2.Nested = True
							''If UseTabs Then
							''    If UseDetails Then UseDetails = Not ds.Tables(6) Is Nothing
							''    If UseDetails Then UseDetails = ds.Tables(6).Rows.Count > 0
							''    UseTabs = Not ds.Tables(2) Is Nothing
							''Else
							''    If UseDetails Then UseDetails = Not ds.Tables(5) Is Nothing
							''    If UseDetails Then UseDetails = ds.Tables(5).Rows.Count > 0
							''End If
							''If UseTabs Then UseTabs = ds.Tables(2).Rows.Count > 0
							Dim rel3 As DataRelation
							''If UseTabs Then
							''    rel3 = .Relations.Add("form-tab", ds.Tables("form").Columns("id"), ds.Tables("tab").Columns("formid"))
							''    rel3.Nested = True
							''    Dim rel4 As DataRelation = .Relations.Add("tab-panel", ds.Tables("tab").Columns("id"), ds.Tables("panel").Columns("tabid"))
							''    rel4.Nested = True
							''Else
							rel3 = .Relations.Add("form-panel", .Tables("form").Columns("id"), .Tables("panel").Columns("formid"))
							rel3.Nested = True
							''End If
							''If UseDetails Then
							''    If UseTabs Then
							''        Dim rel5 As DataRelation = .Relations.Add("tab-paneldetails", ds.Tables("tab").Columns("id"), ds.Tables("paneldetails").Columns("tabid"))
							''        rel5.Nested = True
							''    Else
							''        Dim rel5 As DataRelation = .Relations.Add("form-paneldetails", ds.Tables("form").Columns("id"), ds.Tables("paneldetails").Columns("formid"))
							''        rel5.Nested = True
							''    End If
							''    Dim rel6 As DataRelation = .Relations.Add("paneldetails-fielddetails", ds.Tables("paneldetails").Columns("panelid"), ds.Tables("fielddetails").Columns("panelid"))
							''    rel6.Nested = True
							''End If
						End With
					End If
					sql = ds.GetXml()
					i = sql.Length
					If i > 20 Then
						sql = sql.Substring(7, i - 14)
						sql = sql.Replace("""true""", """1""").Replace("lookup=""false""", "").Replace("""false""", """""")
						sql = sql.Replace("readonly=""false""", "").Replace("required=""false""", "").Replace("optional=""false""", "").Replace("labellist=""""", "").Replace("dbcolumnreadlov=""""", "")
						''If UseDetails Then
						''    sql = sql.Replace("fielddetails", field_str).Replace("paneldetails", "panel-details")
						''End If
					End If
				Else
					sql = "Error"
				End If
				Return "<?xml version=""1.0"" encoding=""UTF-8""?>" & sql
			End If
		End Function

		Friend Function dicoLoadXML(Optional ByVal XML As String = "") As Boolean
			Dim LoadResult As Boolean = True, XMLfileName As String
			Dim aNode As XmlNode

			nsManager = New XmlNamespaceManager(New NameTable())
			nsManager.AddNamespace("evo", evoLink)
			If _FormID > 0 Then
				XML = dicoDB2XML(_FormID, _UserID, _SqlConnection)	'sqlConnectionDico
				If XML.Length < 50 Then XML = String.Empty
			Else
				DBAllowDesign = False
			End If
			If Not String.IsNullOrEmpty(XML) Then
				Try
					myDOM.LoadXml(XML)
					LoadResult = True
				Catch
					LoadResult = False
					ErrorMsg = "Database repository incorrect or unavailable."
				End Try
			Else
				If Not String.IsNullOrEmpty(_XMLfile) Then
					If _SqlConnection = "" AndAlso atRunTime Then
						ErrorMsg = "No database connection specified."
						LoadResult = False
					Else
						Try
							XMLfileName = FileNameWithMask(_XMLfile)
							myDOM.Load(XMLfileName)
							LoadResult = True
						Catch
							LoadResult = False
							If atRunTime Then
								'SHOULD CHECK IF FILE EXISTS AND HAVE SPECIFIC ERROR MSG
								ErrorMsg = HTMLtextMore("Cannot load definition.", String.Format("The file ""{0}"" is unaccessible or the XML is invalid.", XMLfileName))
							Else
								ErrorMsg = "Relative path to XML file will only work at run-time."
							End If
						End Try
					End If
				Else
					ErrorMsg = HTMLtextMore("No application definition specified.", "Invalid Application ID or missing XML file.")
					LoadResult = False
				End If
			End If
			'If atRunTime Then LicenseKey = GetKey()
			If LoadResult Then
				For Each aNode In myDOM.ChildNodes
					If aNode.NodeType = XmlNodeType.Element AndAlso aNode.Name = "form" Then
						If aNode.NamespaceURI <> evoLink Then
							LoadResult = False
							ErrorMsg = String.Format("Invalid application definition. Namespace ""http://www.evolutility.com"" is not set in file ""{0}"".", XMLfileName)
						End If
						Exit For
					End If
				Next
				If LoadResult Then
					Try
						aNode = myDOM.SelectSingleNode(XPathQuery.data, nsManager) 'must limit to text fields 
					Catch 'ex As Exception
						aNode = Nothing
					End Try
					If aNode Is Nothing Then
						LoadResult = False
						ErrorMsg = String.Format("Invalid application definition. The element ""data"" is not set.", XMLfileName)
					Else
						Dim serializer As New XmlSerializer(GetType(Data))
						Dim r As XmlNodeReader = New XmlNodeReader(aNode)
						Try
							def_Data = CType(serializer.Deserialize(r), Data)
						Catch ex As Exception
							ErrorMsg = "Invalid XML file. The element 'data' must have attributes."
						End Try
						aNode = Nothing
						r.Close()
						r = Nothing
						serializer = Nothing
						If def_Data Is Nothing Then
							LoadResult = False
							ErrorMsg = "Invalid XML file."
						Else
							If String.IsNullOrEmpty(def_Data.dbcolumnpk) Then def_Data.dbcolumnpk = "ID"
							If String.IsNullOrEmpty(def_Data.dbtableusers) Then def_Data.dbtableusers = "EVOL_USER"
							If String.IsNullOrEmpty(def_Data.dbtablecomments) Then def_Data.dbtablecomments = "evol_comment"
							If String.IsNullOrEmpty(def_Data.dbcolumncomments) Then def_Data.dbcolumncomments = "itemid"
							If String.IsNullOrEmpty(def_Data.dbcolumnuserid) Then def_Data.dbcolumnuserid = "userID"
							If def_Data.dbcommentsformid = Nothing Then def_Data.dbcommentsformid = _FormID
							If Not String.IsNullOrEmpty(def_Data.dbcolumnpix) Then dbcolumnpix = def_Data.dbcolumnpix
							If String.IsNullOrEmpty(def_Data.entity) Then
								entity = LG_entity
							Else
								entity = def_Data.entity
							End If
							If String.IsNullOrEmpty(def_Data.entities) Then
								entities = LG_entities
							Else
								entities = def_Data.entities
							End If
							allEntities = String.Format(LG_AllEntities, entities)
							If Not String.IsNullOrEmpty(def_Data.icon) Then icon = HTMLIcon(_PathPixToolbar & def_Data.icon)
							If Not String.IsNullOrEmpty(def_Data.dbcolumnpix) Then dbcolumnpix = def_Data.dbcolumnpix
						End If
						If atRunTime Then IEbrowser = CStr(Page.Request.Browser.Browser) = "IE"
					End If
				End If
			End If
			If _ShowDesigner AndAlso _FormID > 0 Then
				'_ShowDesigner = _DisplayMode = 200  BUG ? 
				imgProperties = "<img border=""0"" src=""" & _PathPixToolbar '& "prop.gif"">" 
			Else
				_ShowDesigner = False
			End If
			PixComments = HTMLIcon(_PathPixToolbar & "comments.gif")
			XMLloaded = LoadResult
			Return LoadResult
		End Function

		'Friend Function dicoLoadObjDef(Optional ByVal XML As String = "") As Boolean
		'    Dim LoadResult As Boolean = True, XMLfileName As String
		'    Dim aNode As XmlNode


		'    Dim objdef As New Form


		'    nsManager = New XmlNamespaceManager(New NameTable())
		'    nsManager.AddNamespace("evo", evoLink)
		'    If _FormID > 0 Then
		'        XML = dicoDB2XML(_FormID, _UserID, _SqlConnection)  'sqlConnectionDico
		'        If XML.Length < 50 Then XML = String.Empty
		'    End If
		'    If Not String.IsNullOrEmpty(XML) Then
		'        Try
		'            myDOM.LoadXml(XML)
		'            LoadResult = True
		'        Catch
		'            LoadResult = False
		'            ErrorMsg = "Database repository incorrect or unavailable."
		'        End Try
		'    Else
		'        If Not String.IsNullOrEmpty(_XMLfile) Then
		'            If _SqlConnection = "" AndAlso atRunTime Then
		'                ErrorMsg = "No database connection specified."
		'                LoadResult = False
		'            Else
		'                Try
		'                    XMLfileName = FileNameWithMask(_XMLfile)
		'                    myDOM.Load(XMLfileName)
		'                    LoadResult = True
		'                Catch
		'                    LoadResult = False
		'                    If atRunTime Then
		'                        'SHOULD CHECK IF FILE EXISTS AND HAVE SPECIFIC ERROR MSG
		'                        ErrorMsg = HTMLtextMore("Cannot load definition.", String.Format("The file ""{0}"" is unaccessible or the XML is invalid.", XMLfileName))
		'                    Else
		'                        ErrorMsg = "Relative path to XML file will only work at run-time."
		'                    End If
		'                End Try
		'            End If
		'        Else
		'            ErrorMsg = HTMLtextMore("No application definition specified.", "Invalid Application ID or missing XML file.")
		'            LoadResult = False
		'        End If
		'    End If
		'    'If atRunTime Then LicenseKey = GetKey()
		'    If LoadResult Then

		'        objdef = New Form

		'        For Each aNode In myDOM.ChildNodes
		'            If aNode.NodeType = XmlNodeType.Element Then
		'                If aNode.Name = "form" Then
		'                    If aNode.NamespaceURI <> evoLink Then
		'                        LoadResult = False
		'                        ErrorMsg = String.Format("Invalid application definition. Namespace ""http://www.evolutility.com"" is not set in file ""{0}"".", XMLfileName)
		'                    End If
		'                    Dim aNode1 As XmlNode
		'                    For Each aNode1 In aNode.ChildNodes
		'                        If aNode1.NodeType = XmlNodeType.Element Then
		'                            Select Case aNode1.Name
		'                                Case "data"
		'                                    def_Data = Data.Deserialize(aNode1)
		'                                    objdef.data = def_Data 
		'                                Case "tab"

		'                                Case "panel", "panel-details"
		'                                    Dim aNode2 As XmlNode

		'                                    Dim p As New Panel("", aNode1.Name, aNode1.Attributes("label").Value, aNode1.Attributes("width").Value)
		'                                    For Each aNode2 In aNode1.ChildNodes
		'                                        If aNode2.NodeType = XmlNodeType.Element Then
		'                                            Select Case aNode2.Name
		'                                                Case "field"
		'                                                    Dim f As Field = Field.Deserialize(aNode2)
		'                                                    If Not f Is Nothing Then
		'                                                        p.Fields.Add(f.ID, f)
		'                                                    End If 
		'                                            End Select
		'                                        End If
		'                                    Next
		'                                    objdef.FormElements.Add(p)
		'                            End Select
		'                        End If
		'                    Next
		'                    Exit For
		'                End If
		'            End If
		'        Next
		'        If LoadResult Then
		'            Try
		'                aNode = myDOM.SelectSingleNode(XPathQuery.data, nsManager) 'must limit to text fields 
		'            Catch 'ex As Exception
		'                aNode = Nothing
		'            End Try
		'            If aNode Is Nothing Then
		'                LoadResult = False
		'                ErrorMsg = String.Format("Invalid application definition. The element ""data"" is not set.", XMLfileName)
		'            Else
		'                'Dim serializer As New XmlSerializer(GetType(Form))
		'                'Dim fs As New FileStream("C:\inetpub\wwwroot\EvolutilityWeb2\XML\EvoDemo\aaa.xml", FileMode.Open)
		'                'Dim f As Form
		'                'f = CType(serializer.Deserialize(fs), Form)
		'                'fs.Close()
		'                Dim serializer As New XmlSerializer(GetType(Data))
		'                Dim r As XmlNodeReader = New XmlNodeReader(aNode)
		'                Try
		'                    def_Data = CType(serializer.Deserialize(r), Data)
		'                Catch ex As Exception
		'                    ErrorMsg = "Invalid XML file. The element 'data' must have attributes."
		'                End Try
		'                aNode = Nothing
		'                r.Close()
		'                r = Nothing
		'                serializer = Nothing
		'                If def_Data Is Nothing Then
		'                    LoadResult = False
		'                    ErrorMsg = "Invalid XML file."
		'                Else
		'                    If String.IsNullOrEmpty(def_Data.dbtableusers) Then def_Data.dbtableusers = "EVOL_USER"
		'                    If String.IsNullOrEmpty(def_Data.dbtablecomments) Then def_Data.dbtablecomments = "evol_comment"
		'                    If String.IsNullOrEmpty(def_Data.dbcolumncomments) Then def_Data.dbcolumncomments = "itemid"
		'                    If String.IsNullOrEmpty(def_Data.dbcolumnuserid) Then def_Data.dbcolumnuserid = "userID"
		'                    If def_Data.dbcommentsformid = Nothing Then def_Data.dbcommentsformid = _FormID
		'                    If Not String.IsNullOrEmpty(def_Data.dbcolumnpix) Then dbcolumnpix = def_Data.dbcolumnpix
		'                    If String.IsNullOrEmpty(def_Data.entity) Then
		'                        entity = LG_entity
		'                    Else
		'                        entity = def_Data.entity
		'                    End If
		'                    If String.IsNullOrEmpty(def_Data.entities) Then
		'                        entities = LG_entities
		'                    Else
		'                        entities = def_Data.entities
		'                    End If
		'                    allEntities = String.Format(LG_AllEntities, entities)
		'                    If Not String.IsNullOrEmpty(def_Data.icon) Then icon = HTMLIcon(_PathPixToolbar & def_Data.icon)
		'                    If Not String.IsNullOrEmpty(def_Data.dbcolumnpix) Then dbcolumnpix = def_Data.dbcolumnpix
		'                End If
		'                If atRunTime Then IEbrowser = CStr(Page.Request.Browser.Browser) = "IE"
		'            End If
		'        End If

		'    End If
		'    If _DBAllowDesign Then imgProperties = "<img border=""0"" src=""" & _PathPixToolbar '& "prop.gif"">"
		'    PixComments = HTMLIcon(_PathPixToolbar & "comments.gif")
		'    XMLloaded = LoadResult
		'    Return LoadResult
		'End Function

#End Region

        '### Misc ############################################################################################
#Region "Misc"

        Private Function FileNameWithMask(ByRef XMLFileName As String) As String
            Dim PhysicalPathXML As String, PhysicalPath As String = String.Empty

            If InStr(XMLFileName, ":") > 0 Then 'absolute path
                PhysicalPathXML = XMLFileName
            Else
                If atRunTime Then
                    If XMLFileName.Length > 14 Then
                        If XMLFileName.StartsWith("<assemblypath>") Then    'assembly path
                            PhysicalPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) & "\"
                            If PhysicalPath.Length > 6 Then PhysicalPath = PhysicalPath.Substring(6)
                            PhysicalPathXML = PhysicalPath & XMLFileName.Substring(14)
                        Else 'web path 
                            PhysicalPathXML = Page.MapPath(XMLFileName)
                        End If
                    Else 'web path 
                        PhysicalPathXML = Page.MapPath(XMLFileName)
                    End If
                Else
                    PhysicalPathXML = _DesignWebPath & XMLFileName
                End If
            End If
            Return PhysicalPathXML
        End Function

		Private Function GetPageRequest(ByRef RequestKey As String, Optional ByVal DefaultValue As String = "") As String
			Dim Buffer As String = Trim(Page.Request(RequestKey))

			If String.IsNullOrEmpty(Buffer) Then
				Return DefaultValue
			Else
				Return Buffer
			End If
		End Function

        'maybe want to have another session or cookie variable w/ checksum
        Private Function GetUserID() As Integer
			Return String2Int(CStr(Page.Session(_DBApplicationKey & mauid))) 
        End Function
		Private Sub SetUserID(ByRef ID As Integer)
			Page.Session(_DBApplicationKey & mauid) = ID.ToString
		End Sub

        Private Sub AddError(ByRef msg As String)
            If String.IsNullOrEmpty(ErrorMsg) Then
                ErrorMsg = msg
            Else
                If InStr(ErrorMsg, msg) = 0 Then
                    ErrorMsg += vbCrLf & msg
                End If
            End If
        End Sub

        Private Function GetAppSetting(ByRef key As String) As String
            If Not System.Configuration.ConfigurationManager.AppSettings(key) Is Nothing Then
                Return System.Configuration.ConfigurationManager.AppSettings(key).ToString
            Else
                Return ""
            End If
        End Function

#End Region

        '### Events ##########################################################################################
#Region "Events"

		Public Function LoadPostData(ByVal postDataKey As String, ByVal postCollection As System.Collections.Specialized.NameValueCollection) As Boolean Implements System.Web.UI.IPostBackDataHandler.LoadPostData
		End Function

		Public Sub RaisePostDataChangedEvent() Implements System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent
		End Sub

		Public Enum DBAction
			INSERT = 1
			UPDATE = 2
			DELETE = 3
			DB_ERROR = 4
		End Enum

		Public Enum CredentialAction
			Login = 1
			Invalid_Login = 2
			Logout = 3
			DB_ERROR = 4
		End Enum

		'/// DB Events ///////////////////////////////////////////////////////////////////////////////////
#Region "DB Events"

		Public Class DatabaseEventArgs
			Inherits EventArgs
			Private pAction As DBAction
			Private pID As Integer

			Public Sub New(ByVal Action As DBAction, ByVal ID As Integer)	'Constructor
				Me.pAction = Action
				Me.pID = ID
			End Sub
			Public ReadOnly Property ID() As Integer
				Get
					Return pID
				End Get
			End Property
			Public ReadOnly Property Action() As DBAction
				Get
					Return pAction
				End Get
			End Property
		End Class

		<Category("Database"), Description("Raised when a record is Inserted, Updated or Deleted.")> _
		Public Event DBChange(ByVal sender As Object, ByVal e As DatabaseEventArgs)

		Protected Overridable Sub OnDBChange(ByVal e As DatabaseEventArgs)
			RaiseEvent DBChange(Me, e)
		End Sub

#End Region

		'/// Login Events ///////////////////////////////////////////////////////////////////////////////////
#Region "Login Events"

		Public Class CredentialEventArgs
			Inherits EventArgs
			Private pAction As CredentialAction
			Private pUserID As Integer
			Private pUserName As String
			Private pDBApplicationKey As String
			Private pDescription As String

			Public Sub New(ByVal Action As CredentialAction, ByVal UserID As Integer, ByVal UserName As String, ByVal DBApplicationKey As String, ByVal Description As String)	'Constructor
				Me.pAction = Action
				Me.pUserID = UserID
				Me.pUserName = UserName
				Me.pDBApplicationKey = DBApplicationKey
				Me.pDescription = Description
			End Sub
			Public ReadOnly Property Action() As CredentialAction
				Get
					Return pAction
				End Get
			End Property
			Public ReadOnly Property UserID() As Integer
				Get
					Return pUserID
				End Get
			End Property
			Public ReadOnly Property UserName() As String
				Get
					Return pUserName
				End Get
			End Property
			Public ReadOnly Property DBApplicationKey() As String
				Get
					Return pDBApplicationKey
				End Get
			End Property
			Public ReadOnly Property Description() As String
				Get
					Return pDescription
				End Get
			End Property
		End Class

		<Category("Database"), Description("Raised when the user logs in or out.")> _
		Public Event CredentialChange(ByVal sender As Object, ByVal e As CredentialEventArgs)

		Protected Overridable Sub OnCredentialChange(ByVal e As CredentialEventArgs)
			RaiseEvent CredentialChange(Me, e)
		End Sub

#End Region

#End Region

    End Class

End Namespace