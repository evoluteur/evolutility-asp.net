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



Imports System.Xml

Public Class EvoXMLInfo

    Friend Const dbTable As String = "dbtable", dbWhere As String = "dbwhere", dborder As String = "dborder", icon As String = "icon"
    Friend Const type As String = "type", script As String = "script"
    Friend Const dbname As String = "name", dbColumn As String = "dbcolumn", dbColumnRead As String = "dbcolumnread"
    Friend Const dbColumnImg As String = "dbcolumnimg", dbColumnPix As String = "dbcolumnicon"
    Friend Const dbReadOnly As String = "readonly", required As String = "required", MaxLength As String = "maxlength", url As String = "url"

    Friend Const dbtablelov As String = "dbtablelov", dbcolumnreadlov As String = "dbcolumnreadlov", dbwherelov As String = "dbwherelov"
    Friend Const lovMany As String = "lovmany", lovEnumeration As String = "lovenumeration", lovSPlist As String = "splistlov"
    Friend Const label As String = "label", labelEdit As String = "labeledit", labelList As String = "labellist", Format As String = "format"
    Friend Const link As String = "link", linkTarget As String = "linktarget", linkLabel As String = "linklabel"
    Friend Const CSSclass As String = "cssclass", CSSclasslabel As String = "cssclasslabel", Height As String = "height", Width As String = "width", Img As String = "img"
    Friend Const Search As String = "search", SearchList As String = "searchlist"
    Friend Const dbTableDetails As String = "dbtabledetails", dbColumnDetails As String = "dbcolumndetails"

End Class

Public Class XPathQuery

	Friend Const tab As String = "//evo:tab"
	Friend Const panel As String = "//evo:panel"
    Friend Const panelField As String = "//evo:panel/evo:field"
    Friend Const panelDetails As String = "//evo:panel-details"
    Friend Const panelDetailsField As String = "//evo:panel-details/evo:field"
    Friend Const data As String = "//evo:form/evo:data"
    Friend Const query As String = "//evo:queries/evo:query"
    Friend Const queries As String = "//evo:queries"

    'Friend Const panelField As String = "//panel/field"
    'Friend Const panelDetails As String = "//panel-details"
    'Friend Const panelDetailsField As String = "//panel-details/field"
    'Friend Const data As String = "//form/data"

    Friend Shared Function XPath2True(ByVal attribute As String, Optional ByVal element As String = "//evo:panel/evo:field") As String
        Return String.Format("{0}[@{1}>0]", element, attribute)      '="true"
    End Function

    'Friend Function XPath2True(ByVal attribute As String, Optional ByVal element As String = "", Optional ByVal nsPrefix As String = "") As String
    '    If element.Length = 0 Then
    '        element = String.Format("//{0}panel/{0}field", nsPrefix)
    '    End If
    '    Return String.Format("{0}[@{1}>0]", element, attribute)      '="true"
    'End Function

    Friend Shared Function XPath2Equal(ByVal element As String, ByVal attribute As String, ByVal equalTo As String) As String
        Return String.Format("{0}[@{1}='{2}']", element, attribute, equalTo)
    End Function

End Class
