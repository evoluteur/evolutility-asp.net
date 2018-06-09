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

Imports System.Web.UI.Design

Namespace Evolutility.WebControls

    Public Class UIServerDesigner
        Inherits System.Web.UI.Design.ControlDesigner

        Public Overrides Function GetDesignTimeHtml() As String
            Dim myHTML As New System.Text.StringBuilder
            Dim buffer As String = "", XMLOK As Boolean
            Dim ctl As UIServer = CType(Me.Component, UIServer)

            With ctl
                .atRunTime = False
                myHTML.Append("<div class=""evo"">")
                If Not (.XMLfile = "" And .SqlConnection = "") Then
                    XMLOK = .dicoLoadXML()
                    myHTML.Append(.HTMLmenu())
                    If XMLOK Then
                        Try
                            Select Case .DesignDisplayMode
                                Case UIServer.EvolDisplayMode.View
                                    myHTML.Append(.FormEdit(0))
                                Case UIServer.EvolDisplayMode.Edit, UIServer.EvolDisplayMode.NewItem
                                    myHTML.Append(.FormEdit(1))
                                Case UIServer.EvolDisplayMode.Search
                                    myHTML.Append(.FormSearch(3))
                                Case UIServer.EvolDisplayMode.AdvancedSearch
                                    myHTML.Append(.FormSearch(4))
                                Case UIServer.EvolDisplayMode.List
                                    myHTML.Append(.FormList(103))
                                Case UIServer.EvolDisplayMode.Login
                                    If .SecurityModel = EvolSecurityModel.Single_User Then
                                        myHTML.Append(.FormMessage("User Login is not be required when SecurityModel = Single_User."))
                                    End If
                                    myHTML.Append(.FormLogin())
                                Case UIServer.EvolDisplayMode.Selections
                                    If .DBAllowSelections Then
                                        myHTML.Append(.FormQueries)
                                    Else
                                        myHTML.Append(.FormMessage("For this mode to work, you must set 'DBAllowQueries' to True, and have valid queries in the XML definition."))
                                    End If
                                Case UIServer.EvolDisplayMode.Export
                                    myHTML.Append(.FormExport) 
                                Case Else 'Evolutility.DisplayMode.View
                                    myHTML.Append(.FormEdit(0))
                            End Select
                        Catch
                            buffer = "Error loading control. Please verify the application definition."
                            myHTML.Append(.FormMessage(buffer))
                        End Try
                    Else
                        myHTML.Append("<p><b>Invalid XML file.</b><br>").Append(.XMLfile).Append("</p>")
                    End If
                    '    If .ToolbarPosition = Evolutility.kToolbarPosition.Top_And_Bottom Then myHTML.Append(.HTMLmenu(True))
                    '    If ctl.XMLfile <> "" Then
                    '        myHTML.Append("<small>XML: ").Append(ctl.XMLfile).Append("</small>")
                    '    End If
                End If
                myHTML.Append("<table cellpadding=""0"" cellspacing=""4"" border=""0""  style=""border-width:1px;border-style:Solid;background-color:#E0E0E0;border-color:Gray;""><tr><td><i>Design Time</i><br>")
                myHTML.Append("<small>XMLFile = """).Append(.XMLfile).Append("""<br>")
                myHTML.Append("SqlConnection = """).Append(.SqlConnection).Append("""<br>")
                myHTML.Append("VirtualPathToolbar = """).Append(.VirtualPathToolbar).Append("""")
                myHTML.Append("SecurityModel = """).Append(.SecurityModel).Append(""", SecurityKey = """).Append(.SecurityKey).Append("""")
                myHTML.Append("</small></td></tr></table>")
                myHTML.Append(.RenderFooter())
            End With
            Return myHTML.ToString
        End Function

    End Class

End Namespace