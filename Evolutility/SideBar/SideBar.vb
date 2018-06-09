'//	Copyright (c) 2003-2013 Olivier Giulieri - olivier@evolutility.org 

'//	This file is part of Evolutility CRUD Framework.
'//	Source link <http://www.evolutility.org/download/download.aspx>

'//	Evolutility is open source software: you can redistribute it and/or modify
'//	it under the terms of the GNU Affero General Public License as published by
'//	the open source software Foundation, either version 3 of the License, or
'//	(at your option) any later version.

'//	Evolutility is distributed WITHOUT ANY WARRANTY;
'//	without even the implied warranty of MERCHANTABILITY
'//	or FITNESS FOR A PARTICULAR PURPOSE.  
'//	See the GNU Affero General Public License for more details.

'//	You should have received a copy of the GNU Affero General Public License
'//	along with Evolutility. If not, see <http://www.fsf.org/licensing/licenses/agpl-3.0.html>.


Option Explicit On 
Option Strict On

Imports System.ComponentModel
Imports System.Web.UI
Imports System.XML
Imports System.Text
Imports System.Configuration 
Imports System.Data.SqlClient
 
Imports System.Web.UI.WebControls
 
Namespace Evolutility.SideBar

	<DefaultProperty("Text"), ToolboxData("<{0}:SideBar runat=server></{0}:SideBar>")> Public Class SideBar
		Inherits System.Web.UI.WebControls.WebControl

		'### Variables ##########################################################################################
#Region "Variables"

		Private _M1 As String = "", _M2 As String = "", _MID As Integer
		Private _SqlConnection As String = "", _PathPix As String = ""
		Private Const beginRow As String = "<tr><td><IMG height=""1"" src=""pix/nav/spacery.gif"" width=""128"" border=""0""><br/>&nbsp;&nbsp;"
		Private Const endrow As String = "</a></td></tr>"
		Private Const HTMLmiddle As String = "</p></td><td width=""1%"">&nbsp;</td></tr></TABLE><TABLE cellSpacing=""0"" cellPadding=""0"" width=""780"" border=""0"" class=""content""><tr><td width=""120"" valign=""top""><ul id=""avmenu"">"

#End Region

		'### Properties ##########################################################################################
#Region "Properties"
		<Bindable(True), Category("Behavior"), DefaultValue("")> Property M1() As String
			Get
				Return _M1
			End Get

			Set(ByVal Value As String)
				_M1 = Value
			End Set
		End Property
		<Bindable(True), Category("Behavior"), DefaultValue("")> Property M2() As String
			Get
				Return _M2
			End Get

			Set(ByVal Value As String)
				_M2 = Value
			End Set
		End Property
		<Bindable(True), Category("Behavior"), DefaultValue(-1)> Property MID() As Integer
			Get
				Return _MID
			End Get

			Set(ByVal Value As Integer)
				_MID = Value
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
#End Region

		'### Render ##########################################################################################
		Protected Overrides Sub Render(ByVal output As System.Web.UI.HtmlTextWriter)
			Dim site As String = "evol"
			Static html3spaces As String = "&nbsp;&nbsp;"
			Dim LoggedUser As Boolean = False

			With output
				.Write("<div class=""links"">")
				If _M1.Length > 3 AndAlso _M1.Substring(0, 4) = "demo" Then
					.Write(MenuFullDemo(_MID))
				Else
					Select Case _M1
						Case "product"
							.Write(MenuRowHTML("Overview", "product.aspx", _MID = 1))
							.Write(MenuRowHTML("Features", "features.aspx", _MID = 2, , 1))
							.Write(MenuRowHTML("Metamodel", "metamodel.aspx", _MID = 5, , 1))
							.Write(MenuRowHTML("License", "License.aspx", _MID = 10))
							.Write(MenuRowHTML("Purchase", "Purchase.aspx", _MID = 20))
							If M2 = "@home" Then
								.Write(MenuRowHTML("Contribute", "Get_Involved.aspx", _MID = 500))
							End If
						Case "Chinook", "NorthWind"
							.Write(MenuFullChinook(_MID))
						Case "corp"
							'.Write(MenuRowHTML("Product", "product.aspx", _MID = 10))
							.Write(MenuRowHTML("About Me", "company.aspx", _MID = 0))
							'.Write(MenuRowHTML("Services", "services.aspx", _MID = 1))
							.Write(MenuRowHTML("Contact", "contact.aspx", _MID = 2))
						Case "down"             ' ## Download ###############
							.Write(MenuRowHTML("Download", "download.aspx", _MID = 1))
							.Write(MenuRowHTML("Source Code", "evolutility_source_code.aspx", _MID = 100))
							.Write(MenuRowHTML("License", "License.aspx", _MID = 10))
							.Write(MenuRowHTML("Purchase", "Purchase.aspx", _MID = 20))
						Case "evodico"			' ## Admin ###############
							.Write(MenuFullEvoDico(_MID))
						Case "doc", "doc1", "more"
							If _M1 <> "more" Then
								.Write(MenuFullDoc(_MID))
							Else
								.Write(MenuRowHTML("Documentation", "doc.aspx", False))
							End If
							'.Write(MenuRowHTML("News", "news.aspx", _MID = 100))
							.Write(MenuRowHTML("License", "License.aspx", _MID = 3010))
							.Write(MenuRowHTML("Purchase", "Purchase.aspx", _MID = 3020))
							.Write(MenuRowHTML("Release Log", "Release_Log.aspx", _MID = 400))
							.Write(MenuRowHTML("Get involved", "Get_Involved.aspx", _MID = 500))
							'Case "myusers"
							'	.Write(MenuRowHTML("Users", "Users.aspx", _MID = 104, "../pixEvo/mec2.gif"))
							'	.Write(MenuRowHTML("My Profile", "My_user_profiles.aspx", _MID = 105, "../pixEvo/mec.gif"))
							'	.Write(MenuRowHTML("My Information", "My_user_info.aspx", _MID = 106, "../pixEvo/folder_user.png"))
							'	'.Write(MenuRowHTML("User Logs", "userLogs.aspx", _MID = 104, "../mec.gif"))
						Case "public", "group", "me"
							.Write(GetMenu(_M1))
						Case "PIM"
							.Write(MenuFullPIM())
					End Select
				End If
				.Write("</br></br></br>&nbsp;</div>")
			End With
		End Sub

		'### Sub-Menus ##########################################################################################
#Region "Sub-Menus"

		Private Function MenuFullDemo(ByVal MenuID As Integer) As String
			Dim sb As New StringBuilder()

			sb.Append(MenuRowHTML("Demos", "demo.aspx", _MID = 0))
			sb.Append(MenuRowHTML("Simple Apps", "demo__Simple.aspx", _MID = 800))
			If _M1 = "demos" Then
				sb.Append(MenuRowHTML("Address book", "demo_Addressbook.aspx", _MID = 2, "contact.gif", 1))
				sb.Append(MenuRowHTML("To do", "demo_ToDo.aspx", _MID = 12, "todo.gif", 1))
				sb.Append(MenuRowHTML("Bookmarks", "demo_Bookmark.aspx", _MID = 16, "favourites.gif", 1))
				sb.Append(MenuRowHTML("Memo pad", "demo_MemoPad.aspx", _MID = 14, "memo.gif", 1))
				'If (_MID = 0 Or (_MID > 100 And _MID < 150)) Then
				sb.Append(MenuRowHTML("Photo album", "demo_Photo.aspx", _MID = 130, "photo.gif", 1))
				If _MID = 140 Or _MID = 141 Then
					sb.Append(MenuRowHTML("Wine cellar", "demo_WineCellar.aspx", _MID = 140, "wine.gif", 1))
					sb.Append(MenuRowHTML("Wine tasting", "demo_WineTasting.aspx", _MID = 141, , 2))
				Else
					sb.Append(MenuRowHTML("Wine cellar", "demo_WineCellar.aspx", False, "wine.gif", 1))
				End If
				sb.Append(MenuRowHTML("Restaurants", "demo_Restaurant.aspx", _MID = 110, "resto.gif", 1))
				'sb.Append(MenuRowHTML("Braille", "demo_Braille.aspx", _MID = 106, "brl.gif", 1))
				'If _MID = 106 Then
				'	sb.Append(BRLLinks())
				'End If
				sb.Append(MenuRowHTML("Database", "Demo_db.aspx", _MID = 180, "db.gif", 1))
				If _MID = 180 Then
					sb.Append(MenuRowHTML("Tables", "Demo_db.aspx?QUERY=U", False, , 2))
					sb.Append(MenuRowHTML("Views", "Demo_db.aspx?QUERY=V", False, , 2))
					sb.Append(MenuRowHTML("Stored Procedures", "Demo_db.aspx?QUERY=P", False, , 2))
					sb.Append(MenuRowHTML("Triggers", "Demo_db.aspx?QUERY=TR", False, , 2))
				End If
			End If
			sb.Append(MenuRowHTML("Master Details Demos", "demo__masterdetails.aspx", _MID = 250))
			If _MID > 199 AndAlso _MID < 270 Then
				sb.Append(MenuRowHTML("Movies", "movie.aspx", _MID = 200, "movie.gif", 1))
				sb.Append(MenuRowHTML("Actors", "movie_actor.aspx", _MID = 210, "moviedir.gif", 1))
				sb.Append(MenuRowHTML("Directors", "movie_director.aspx", _MID = 220, "mec2.gif", 1))
			End If 
			sb.Append(MenuRowHTML("Multi-user Demos", "demo__security.aspx", _MID = 12 AndAlso _M1 = "demo_mu"))
			If _M1 = "demo_mu" Then
				sb.Append(MenuRowHTML("Collaboration", "Demo_addressbook_sharing.aspx", _MID = 2, "contactShared.gif", 1))
				sb.Append(MenuRowHTML("Row level security", "Demo_addressbook_rls.aspx", _MID = 1, "contactRLS.gif", 1))
			End If
			sb.Append(MenuRowHTML("Technical Demos", "demo__techno.aspx", _MID = 3001))
			If _M1 = "demo_dev" Then
				sb.Append(MenuRowHTML("Rich Text Editor", "dev_RTF.aspx", _MID = 3100, , 1))
				sb.Append(MenuRowHTML("<nobr>Dependent fields</nobr>", "dev_Dependent_Fields.aspx", _MID = 3310, , 1))
				sb.Append(MenuRowHTML("Localization", "dev_Localization.aspx?LNG=FR", _MID = 3330, , 1))
				sb.Append(MenuRowHTML("<nobr>Custom Validation</nobr>", "dev_Validation.aspx", _MID = 3340, , 1))
				sb.Append(MenuRowHTML("Permissions", "dev_Permissions.aspx", _MID = 3200, , 1))
				sb.Append(MenuRowHTML("Navigation", "dev_Navigation.aspx", _MID = 3320, , 1))
				sb.Append(MenuRowHTML("Server Events", "dev_Events.aspx", _MID = 3300, , 1))
				'	'sb.Append(MenuRowHTML("Formula", "dev_Formula.aspx", _MID = 3310))
				'	'sb.Append(MenuRowHTML("Test Object", "dev_TestObject.aspx", _MID = 3400)) 
			End If
			Return sb.ToString()
		End Function

		Private Function MenuBugDB(ByVal MenuID As Integer) As String
			Dim sb As New StringBuilder()

			sb.Append(MenuRowHTML("Bugs Database", "Bug.aspx", _MID = 100, "bug.gif", 0, "../PixEvo/"))
			'If _MID = 100 Then
			sb.Append(MenuRowHTML("Open bugs", "bug.aspx", _MID = 100, , 1))
			sb.Append(MenuRowHTML("Submit a bug", "bug_new.aspx", _MID = 100, , 1))
			'End If
			sb.Append(MenuRowHTML("Enhancements", "Enhancement.aspx", _MID = 200, "enhancement.png", 0, "../PixEvo/"))
			'If _MID = 200 Then
			sb.Append(MenuRowHTML("Planned Enhancements", "Enhancement_request.aspx?MODE=list", False, , 1))
			sb.Append(MenuRowHTML("Propose an enhancement", "Enhancement_request.aspx", False, , 1))
			'End If
			Return sb.ToString()
		End Function

		Private Function MenuFullEvoDico(ByVal MenuID As Integer) As String
			Dim sb As New StringBuilder()

			'If LoggedUser Then
			sb.Append(MenuRowHTML3noLink("Designer", False, "edi_frm_edit.png"))
			sb.Append(MenuRowHTML("Forms", "EvoDicoForm.aspx", _MID = 1, "edi_frm.png", 1))
			sb.Append(MenuRowHTML("Panels", "EvoDicoPanel.aspx", _MID = 3, "edi_pnl.png", 1))
			sb.Append(MenuRowHTML("Fields", "EvoDicoField.aspx", _MID = 2, "edi_fld.png", 1))
			'sb.Append(MenuRowHTML3("Menus", "EvoDicoMenus.aspx", _MID = 0, "edi_mnu.png"))
			'sb.Append(MenuRowHTML3("Tabs", "EvoDicoTab.aspx", _MID = 5, "edi_tab.png"))
			'If _MID = 3 Then .Append(MenuRowHTML("Field Groups", "EvoDicopanels.aspx", _MID = 3)) 
			'sb.Append(MenuRowHTML3("Panels-details", "EvoDicoPanelDetails.aspx", _MID = 4, "edi_pnd.png"))
			'sb.Append(MenuRowHTML3("Searches", "EvoDicosearches.aspx", _MID = 5))
			sb.Append("<br/>")
			Dim buffer As String = ""
			If _MID = 120 And Not String.IsNullOrEmpty(Page.Request("WIZ")) Then
				buffer = Page.Request("WIZ").ToString()
			End If
			sb.Append(MenuRowHTML3noLink("App Wizards", False, "wand.png"))
			sb.Append(MenuRowHTML("New App", "EvoDicoWiz.aspx?WIZ=build", buffer = "build", "app_get.png", 1))
			sb.Append(MenuRowHTML("Installer", "EvoDicoWiz.aspx?WIZ=install", buffer = "install", "app_cascade.png", 1))
			''sb.Append(MenuRowHTML("Import CSV", "EvoDicoWiz.aspx?WIZ=csv2db", buffer = "csv2db", , 1))
			sb.Append("<br/>")
			sb.Append(MenuRowHTML3noLink("Manage Users", False, "user.gif"))
			sb.Append(MenuRowHTML("Users", "EvoUser.aspx", _MID = 150, "user.gif", 1))
			sb.Append("<br/>")
			'sb.Append(MenuRowHTML("Test", "EvoDicoTest.aspx", _MID = 1200))
			sb.Append(MenuRowHTML3noLink("Docs & Data", False, "cog.png"))
			sb.Append(MenuRowHTML("Forms", "EvoDoc.aspx", _MID = 102, "form_magnify.png", 1))
			'If _MID > 101 AndAlso _MID < 106 Then
			sb.Append(MenuRowHTML("Database", "EvoDoc_db.aspx", _MID = 105, "db.gif", 1))
			sb.Append(MenuRowHTML("Export Data", "EvoDoc_Export.aspx", _MID = 103, "db_go.png", 1))
			'sb.Append(MenuRowHTML("Map DB tables", "EvoDicoWiz.aspx?WIZ=dbscan", buffer = "dbscan", , 1))
			'sb.Append(MenuRowHTML("Import XML", "EvoDicoWiz.aspx?WIZ=xml2db", buffer = "xml2db", , 1))
			'End If
			'Else
			'	sb.Append(MenuRowHTML3noLink("Login", False, "key.png"))
			'End If
			Return sb.ToString()
		End Function

		Private Function MenuFullDoc(ByVal MenuID As Integer) As String
			Dim sb As New StringBuilder()

			sb.Append(MenuRowHTML("Documentation", "Doc.aspx", _MID = 2000))
			sb.Append(MenuRowHTML("Evolutility", "EvoDoc.aspx", _MID = 10, , 1))
			If _MID < 100 Then
				sb.Append(MenuRowHTML("Installation", "EvoDoc_Install.aspx", _MID = 50, , 2))
				sb.Append(MenuRowHTML("Properties", "EvoDoc_Properties.aspx", _MID = 44, "dn_prop.gif", 2, "pix/"))
				sb.Append(MenuRowHTML("Events", "EvoDoc_Events.aspx", _MID = 52, "dn_event.gif", 2, "pix/"))
				sb.Append(MenuRowHTML("Database", "EvoDoc_DB.aspx", _MID = 80, "db.gif", 2, "pix/"))
				sb.Append(MenuRowHTML("Security Models", "EvoDoc_Access.aspx", _MID = 46, , 2))
				sb.Append(MenuRowHTML("Collaboration", "EvoDoc_Collaboration.aspx", _MID = 48, , 2))
				sb.Append(MenuRowHTML("Dependent Fields", "EvoDoc_Dependent_Fields.aspx", _MID = 65, , 2))
				sb.Append(MenuRowHTML("Fields Validation", "EvoDoc_Validation.aspx", _MID = 60, , 2))
				sb.Append(MenuRowHTML("Navigation", "EvoDoc_Navigation.aspx", _MID = 47, , 2))
				sb.Append(MenuRowHTML("Skins", "EvoDoc_Skin.aspx", _MID = 70, , 2))
			End If
			sb.Append(MenuRowHTML("Meta-model", "ED_Metamodel.aspx", _MID = 100, , 1))
			If _MID > 99 AndAlso _MID < 200 Then
				sb.Append(MenuRowHTML("Form & Data", "ED_Elem_Form.aspx", _MID = 110, , 2))
				sb.Append(MenuRowHTML("Panel & Tab", "ED_Elements_Groups.aspx", _MID = 120, , 2))
				sb.Append(MenuRowHTML("Fields", "ED_Elem_Field.aspx", _MID = 130, , 2))
				sb.Append(MenuRowHTML("Field Types", "ED_Field_Types.aspx", _MID = 135, , 3))
				sb.Append(MenuRowHTML("Elements Positioning", "ED_Position.aspx", _MID = 150, , 2))
				sb.Append(MenuRowHTML("Panel-details", "ED_Elements_Groups2.aspx", _MID = 125, , 2))
				sb.Append(MenuRowHTML("Queries", "ED_Elem_Queries.aspx", _MID = 106, , 2))
				'sb.Append(MenuRowHTML("XML Reference", "EvoDocAtom.aspx", _MID = 42)) 
			End If
			If _M1 <> "doc1" Then
				sb.Append(MenuRowHTML("Evolutility Dictionary", "EvoDico.aspx", _MID = 1500, , 1))
				If _MID > 1499 AndAlso _MID < 1600 Then
					sb.Append(MenuRowHTML("Installation", "EvoDico_Install.aspx", _MID = 1550, , 2))
					sb.Append(MenuRowHTML("Designer", "EvoDico_App.aspx", _MID = 1505, , 2))
					sb.Append(MenuRowHTML("Wizards", "EvoDico_Wizards.aspx", _MID = 1599, "pix/wand.png", 2))
					If _MID > 1550 Then
						sb.Append(MenuRowHTML("Build Apps", "EvoDico_Wiz_Build.aspx", _MID = 1575, , 3))
						sb.Append(MenuRowHTML("Install Apps", "EvoDico_Wiz_Install.aspx", _MID = 1580, , 3))
						sb.Append(MenuRowHTML("Import XML", "EvoDico_Wiz_XML2DB.aspx", _MID = 1585, , 3))
						sb.Append(MenuRowHTML("Map DB", "EvoDico_Wiz_DBScan.aspx", _MID = 1560, , 3))
						'sb.Append(MenuRowHTML("Export Data", "EvoDico_Wiz_Export.aspx", _MID = 1590, , 3))
					End If
					sb.Append(MenuRowHTML("Applications Customization", "EvoDico_Customize.aspx", _MID = 1520, , 2))
					sb.Append(MenuRowHTML("Database", "EvoDico_DB.aspx", _MID = 1510, "db.gif", 2, "pix/"))
				End If
			End If
			sb.Append(MenuRowHTML("Articles", "EvoDoc2_Articles.aspx", _MID = 4000, , 1))
			Return sb.ToString()
		End Function

		Private Function MenuFullForums() As String
			Dim sb As New StringBuilder()

			sb.Append(MenuRowHTML("Forums", "Forum.aspx", _MID = 1000, "../PixEvo/forum.gif"))
			If _MID = 1000 Then
				sb.Append(MenuRowHTML("Discussions", "ForumDisc.aspx", False, "../PixEvo/file.gif", 1))
				sb.Append(MenuRowHTML("Start a discussion", "ForumDisc.aspx?MODE=new", False, , 1))
			End If
			Return sb.ToString()
		End Function

		Private Function MenuFullPIM() As String
			Dim sb As New StringBuilder()

			sb.Append(MenuRowHTML("PIM", "index.aspx", _MID = 0))
			If _M1 = "PIM" Then
				sb.Append(MenuRowHTML("Address book", "Addressbook.aspx", _MID = 2, "contact.gif", 1))
				sb.Append(MenuRowHTML("To do", "ToDo.aspx", _MID = 12, "todo.gif", 1))
				sb.Append(MenuRowHTML("Memo pad", "MemoPad.aspx", _MID = 14, "memo.gif", 1))
				sb.Append(MenuRowHTML("Bookmarks", "Bookmark.aspx", _MID = 16, "favourites.gif", 1))
				sb.Append(MenuRowHTML("Photo album", "Photo.aspx", _MID = 130, "photo.gif", 1))
				sb.Append(MenuRowHTML("Restaurants", "Restaurant.aspx", _MID = 110, "resto.gif", 1))
				If _MID = 140 Or _MID = 141 Then
					sb.Append(MenuRowHTML("Wine cellar", "WineCellar.aspx", _MID = 140, "wine.gif", 1))
					sb.Append(MenuRowHTML("Wine Tasting", "WineTasting.aspx", _MID = 141, , 2))
				Else
					sb.Append(MenuRowHTML("Wine cellar", "WineCellar.aspx", False, "wine.gif", 1))
				End If
			End If
			Return sb.ToString()
		End Function

		Private Function MenuFullChinook(ByVal MenuID As Integer) As String
			Dim sb As New StringBuilder()

			sb.Append(MenuRowHTML("Chinook Demo", "Chinook_by_Evolutility.aspx", MenuID = 12))
			sb.Append("<br/>")
			sb.Append(MenuRowHTML("Albums", "Album.aspx", MenuID = 10, "cd.png", 1))
			sb.Append(MenuRowHTML("Artists", "Artist.aspx", MenuID = 20, "user_orange.png", 1))
			sb.Append(MenuRowHTML("Tracks", "Track.aspx", MenuID = 30, "music.png", 1))
			sb.Append("<br/>")
			sb.Append(MenuRowHTML("PlayLists", "PlayList.aspx", MenuID = 50, "page_lightning.png", 1))
			sb.Append(MenuRowHTML("Music Genres", "LOV_Genre.aspx", MenuID = 60, , 1))
			sb.Append(MenuRowHTML("Media Types", "LOV_MediaType.aspx", MenuID = 70, , 1))
			sb.Append("<br/>")
			sb.Append(MenuRowHTML("Employees", "Employee.aspx", MenuID = 100, "User.png", 1))
			sb.Append(MenuRowHTML("Customers", "Customer.aspx", MenuID = 110, "user_gray.png", 1))
			sb.Append(MenuRowHTML("Invoices", "Invoice.aspx", MenuID = 120, "calculator_edit.png", 1))
			sb.Append("<br/>")
			sb.Append(MenuRowHTML("Database", "Chinook_DB.aspx", MenuID = 200, "db.gif", 1))
			sb.Append("<br/><br/>")
			sb.Append(MenuRowHTML("More demos", "../demo/demo.aspx", False))
			Return sb.ToString()
		End Function

#End Region

		Private Function GetAppSetting(ByRef key As String) As String
			If Not System.Configuration.ConfigurationManager.AppSettings(key) Is Nothing Then
				Return System.Configuration.ConfigurationManager.AppSettings(key).ToString
			Else
				Return ""
			End If
		End Function

		Private Function MenuRowHTML3(ByVal menuTitle As String, ByVal menuLink As String, Optional ByVal selected As Boolean = False, Optional ByVal Icon As String = "") As String
			Dim buffer As New System.Text.StringBuilder

			If selected Then
				buffer.Append("<div class=""sele""><a href=""")
			Else
				buffer.Append("<div><a href=""")
			End If
			buffer.Append(menuLink).Append(""">")
			If Not String.IsNullOrEmpty(Icon) Then
				buffer.Append("<img src=""../pixevo/").Append(Icon).Append(""" border=0>&nbsp;")
			End If
			buffer.Append(menuTitle).Append("</a>")
			If selected Then
				If menuLink = "EvoDicoForm.aspx" Then
					buffer.Append("<br/><a href=""EvoDicoWiz.aspx?MODE=new"" class=""Indent1"">new</a>")
				Else
					buffer.Append("<br/><a href=""").Append(menuLink).Append("?MODE=new"" class=""Indent1"">new</a>")
				End If
				buffer.Append("<br/><a href=""").Append(menuLink).Append("?MODE=search"" class=""Indent1"">search</a>")
				'   buffer.Append(MenuRowHTML("Selections", "demo_braille.aspx?MODE=selections", False))
			End If
			buffer.Append("</div>")
			Return buffer.ToString
		End Function
		Private Function MenuRowHTML3noLink(ByVal menuTitle As String, Optional ByVal selected As Boolean = False, Optional ByVal Icon As String = "") As String
			Dim buffer As New System.Text.StringBuilder

			buffer.Append("<div class=""")
			If selected Then
				buffer.Append("sele mt")
			Else
				buffer.Append("mt")
			End If
			buffer.Append("""><b>")
			If Not String.IsNullOrEmpty(Icon) Then
				buffer.Append("<img src=""../pixevo/").Append(Icon).Append(""" border=0>&nbsp;")
			End If
			buffer.Append(menuTitle).Append("</b></div>")
			Return buffer.ToString
		End Function

		Private Function MenuRowHTML(ByVal menuTitle As String, ByVal menuLink As String, Optional ByVal selected As Boolean = False, Optional ByVal icon As String = "", Optional ByVal indent As Short = 0, Optional ByVal iconPath As String = "") As String
			Dim buffer As New System.Text.StringBuilder

			buffer.AppendFormat("<div class=""")
			If indent > 0 Then
				buffer.AppendFormat("indent{0} ", indent)
			End If
			If selected Then
				buffer.Append("sele""><a href=""")
			Else
				buffer.Append("""><a href=""")
			End If
			buffer.Append(menuLink).Append(""">")
			If Not String.IsNullOrEmpty(icon) Then
				buffer.Append("<img src=""")
				If String.IsNullOrEmpty(iconPath) Then
					buffer.Append(_PathPix)
				Else
					buffer.Append(iconPath)
				End If
				buffer.Append(icon).Append(""" class=""icon"">&nbsp;")
			End If
			buffer.Append(menuTitle).Append("</a></div>")
			Return buffer.ToString
		End Function

		Private Function GetMenu(ByVal target As String) As String
			If String.IsNullOrEmpty(_SqlConnection) Then
				_SqlConnection = GetAppSetting("SQLConnectionDico")
			End If
			If String.IsNullOrEmpty(_SqlConnection) Then
				_SqlConnection = GetAppSetting("SQLConnection")
			End If
			If String.IsNullOrEmpty(_SqlConnection) Then
				Return ""
			End If
			Dim i As Integer
			Dim ds As DataSet
			Dim t As DataTable
			Dim MaxLoopSQL As Integer = 0
			Dim menuLink As String, pageURL As String
			Dim icon As String = ""
			Dim sb As New System.Text.StringBuilder
			Dim formID As Double = Val(Page.Request("formID"))

			Dim sql As String = "SELECT id,title,icon FROM evodico_form "
			Select Case target
				'Case "group"
				'	pageURL = "MyEvolGroup.aspx"
				'	sql += " WHERE publish IN (2,3)"
				Case "me"
					pageURL = "MyEvol.aspx"
					'sql += ""
				Case Else
					'Case "public"
					pageURL = "MyEvolPub.aspx"
					sql += " WHERE publish>0" '" WHERE publish=2"
			End Select
			sql += " ORDER BY title"
			ds = GetData(sql, _SqlConnection)
			If ds Is Nothing Then
				't.Dispose()
				ds.Dispose()
				Return String.Empty
			Else
				t = ds.Tables(0)
				MaxLoopSQL = t.Rows.Count - 1
				For i = 0 To MaxLoopSQL
					If Not t.Rows(i).Item("icon") Is Nothing Then
						icon = String.Format("<img border=0 src=""{0}{1}"" />", _PathPix, t.Rows(i).Item("icon"))
					Else
						icon = ""
					End If
					If target = "User" Then
						sql += " WHERE publish>0"
						'Case 1 'admin 
					End If
					menuLink = String.Format("{0}?formID={1}", pageURL, t.Rows(i).Item("id"))

					If Val(t.Rows(i).Item("id")) = formID Then
						sb.AppendFormat("<div class=""sele""><a href=""{0}"">{2}&nbsp;{1}</a>", menuLink, CStr(t.Rows(i).Item("title")), icon)
						sb.Append("<br/><a href=""").Append(menuLink).Append("&MODE=new"" class=""Indent1"">new</a>")
						sb.Append("<br/><a href=""").Append(menuLink).Append("&MODE=search"" class=""Indent1"">search</a>")
						sb.Append("</div>")
					Else
						sb.AppendFormat("<div><a href=""{0}"">{2}&nbsp;{1}</a></div>", menuLink, CStr(t.Rows(i).Item("title")), icon)
					End If
				Next
				sb.Append("</ul>")
				t.Dispose()
				ds.Dispose()
				Return sb.ToString()
			End If
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
				'ErrorMsg += DBerror.Message 'HTMLtextMore(LG_NoQuery, DBerror.Message)
				Return Nothing
			Finally
				myCommand.Dispose()
				myConnection.Dispose()
			End Try
		End Function

	End Class

End Namespace