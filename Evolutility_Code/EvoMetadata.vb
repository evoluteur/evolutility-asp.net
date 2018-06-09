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


Imports System
Imports System.Xml
Imports System.Xml.Serialization 
Imports System.Collections


'<XmlRootAttribute("form")> _
Public Class Form
    Public ID As Integer

    '<XmlElement("data")> _
    Public data As Data
    Public FormElements As ArrayList


    Public Sub New()
        FormElements = New ArrayList()

    End Sub
  
    <XmlElement("queries")> _
    Public queries As Queries

End Class

<XmlRootAttribute("data", Namespace:="http://www.evolutility.com")> _
Public Class Data

    <XmlAttribute()> Public dbtable As String 'required
	<XmlAttribute()> Public dbcolumnlead As String
	<XmlAttribute()> Public dbcolumnpk As String
    <XmlAttribute()> Public dbtablecomments As String
    <XmlAttribute()> Public dbtableusers As String
    <XmlAttribute()> Public dbwhere As String
    <XmlAttribute()> Public dborder As String
    <XmlAttribute()> Public dbcommentsformid As Integer

    <XmlAttribute()> Public sppaging As String
    <XmlAttribute()> Public spget As String
    <XmlAttribute()> Public splogin As String
    <XmlAttribute()> Public spdelete As String

    <XmlAttribute()> Public icon As String
    <XmlAttribute()> Public entity As String
    <XmlAttribute()> Public entities As String
    <XmlAttribute()> Public script As String

    'added
    <XmlAttribute()> Public title As String
    <XmlAttribute()> Public js_script As String
    <XmlAttribute()> Public dblockingcolumn As String
    <XmlAttribute()> Public dbcolumncomments As String
    <XmlAttribute()> Public userpage As String
    <XmlAttribute()> Public dbcolumnuserid As String
    <XmlAttribute()> Public dbcolumnpix As String

    Public Shared Function Deserialize(ByVal aNode As XmlNode) As Data
        Dim d As Data
        Dim serializer As New XmlSerializer(GetType(Data))
        Dim r As XmlNodeReader = New XmlNodeReader(aNode)
        Try
            d = CType(serializer.Deserialize(r), Data)
            Return d
        Catch ex As Exception
            'ErrorMsg = "Invalid XML file. The element 'field' must have attributes."
            Return Nothing
        End Try
    End Function

End Class

'<XmlInclude(GetType(Panel)), XmlInclude(GetType(Tab)), Serializable()> _
Public Class Holder
    <XmlAttribute()> Public ID As String
    <XmlAttribute()> Public cssclass As String
    <XmlAttribute()> Public cssclasslabel As String
    <XmlAttribute()> Public label As String 'required 
End Class

<Serializable()> _
Public Class Tab : Inherits Holder 
    Public Panels As Hashtable
    Public Sub New()
        Panels = New Hashtable()
    End Sub
End Class

<Serializable()> _
Public Class Panel : Inherits Holder
    Public Sub New()
        Fields = New Hashtable()
        type = ""
    End Sub
    Public Sub New(ByVal nid As String, ByVal nType As String, ByVal nlabel As String, ByVal nwidth As String)
        ID = nid
        type = nType
        label = nlabel
        width = nwidth
        Fields = New Hashtable()
    End Sub
    <XmlAttribute()> Public type As String
    <XmlAttribute()> Public width As String
    <XmlAttribute()> Public img As String
    <XmlAttribute()> Public [optional] As String
    <XmlArrayItem("field")> Public Fields As Hashtable

    'Public Shared Function Deserialize(ByVal aNode As XmlNode) As Panel
    '    Dim p As Panel
    '    Dim serializer As New XmlSerializer(GetType(Panel))
    '    Dim r As XmlNodeReader = New XmlNodeReader(aNode)
    '    Try
    '        p = CType(serializer.Deserialize(r), Panel)
    '        Return p
    '    Catch ex As Exception
    '        'ErrorMsg = "Invalid XML file. The element 'field' must have attributes."
    '        Return Nothing
    '    End Try
    'End Function
End Class

<Serializable()> _
Public Class Queries 
    <XmlAttribute()> Public label As String
    '<XmlArray("queries"), XmlArrayItem("query")> Public query() As Query
End Class

<Serializable()> _
Public Class Query
    <XmlAttribute()> Public url As String 'required
    <XmlAttribute()> Public label As String 'required
    <XmlAttribute()> Public dbwhere As String 'required
    <XmlAttribute()> Public dborder As String
End Class

<XmlRootAttribute("field", Namespace:="http://www.evolutility.com")> _
Public Class Field
    Private _ID As String
    Property ID() As String
        Get
            If String.IsNullOrEmpty(_ID) Then
                Return "EVOL" & Me.dbcolumn
            Else
                Return _ID 
            End If
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property
    <XmlAttribute()> Public cssclass As String
    <XmlAttribute()> Public dbcolumn As String 'required
    <XmlAttribute()> Public dbcolumnimg As String
    <XmlAttribute()> Public dbcolumnpix As String
    <XmlAttribute()> Public dbcolumnread As String 'required
    <XmlAttribute()> Public dbcolumnreadlov As String
    <XmlAttribute()> Public dbtablelov As String
    <XmlAttribute()> Public dborderlov As String
    <XmlAttribute()> Public dbwherelov As String
    <XmlAttribute()> Public splistlov As String
    <XmlAttribute()> Public format As String
    <XmlAttribute()> Public height As Integer 'default="1"  
    <XmlAttribute()> Public help As String
    <XmlAttribute()> Public img As String
    <XmlAttribute()> Public imglist As String
    <XmlAttribute()> Public label As String 'required
    <XmlAttribute()> Public labeledit As String
    <XmlAttribute()> Public labellist As String
    <XmlAttribute()> Public link As String
    <XmlAttribute()> Public linklabel As String
    <XmlAttribute()> Public linktarget As String
    <XmlAttribute()> Public lookup As String
    <XmlAttribute()> Public lovenumeration As String
    <XmlAttribute()> Public maxlength As String
    <XmlAttribute()> Public max As String
    <XmlAttribute()> Public min As String
    <XmlAttribute()> Public [optional] As String
    <XmlAttribute()> Public [readonly] As Integer
    <XmlAttribute()> Public required As Integer
    <XmlAttribute()> Public script As String 'UNDOCUMENTED
    <XmlAttribute()> Public search As Integer
    <XmlAttribute()> Public searchlist As Integer
    <XmlAttribute()> Public searchadv As Integer
    <XmlAttribute()> Public [type] As String
    <XmlAttribute()> Public width As Integer 'default="100" 
    <XmlAttribute()> Public validationrule As String

    Public Shared Function Deserialize(ByVal aNode As XmlNode) As Field
        Dim f As Field
        Dim serializer As New XmlSerializer(GetType(Field))
        Dim r As XmlNodeReader = New XmlNodeReader(aNode)
        Try
            f = CType(serializer.Deserialize(r), Field)
            Return f
        Catch ex As Exception
            'ErrorMsg = "Invalid XML file. The element 'field' must have attributes."
            Return Nothing
        End Try
    End Function

End Class

 

