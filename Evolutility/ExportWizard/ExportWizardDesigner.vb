'  Copyright (c) 2003-2013 Olivier Giulieri - olivier@evolutility.org 

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

Imports System.Web.UI.Design

Namespace Evolutility.ExportWizard

	Public Class ExportWizardDesigner
		Inherits System.Web.UI.Design.ControlDesigner

		Public Overrides Function GetDesignTimeHtml() As String
			Dim myHTML As New System.Text.StringBuilder, buffer As String = ""
			Dim ctl As ExportWizard = CType(Me.Component, ExportWizard)

			With ctl
				.atRunTime = False
				myHTML.Append("<table cellpadding=""10"" cellspacing=""0"" width=""100%"" class=""Panel""><tr><td>")
				Try
					myHTML.Append(.FormStep(.DesignStep + 1))
				Catch
					myHTML.Append(EvoUI.FormMessage("Error loading Evolutility.ExportWizard control designer.", "error"))
				End Try
				myHTML.Append("</td></tr>")
				myHTML.Append(.FormButtons())
				myHTML.Append("</table>")
			End With
			GetDesignTimeHtml = myHTML.ToString()
		End Function

	End Class

End Namespace