<%@ Page validateRequest="false"   Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" 
CodeFile="demo.aspx.cs" Inherits="demo"
 Title="Evolutility :: Demo : Navigation"
CodeFileBaseClass="BasePage"  
Menus="demo_dev"
SubMenuID="3320"
  %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Navigation</h1>
  
    
    <table width=100%>
<tr valign=top>
<td width="200px"> 

<p> 
<a href="dev_Navigation.aspx?MODE=new">New record</a>
<br />
<small>MODE=new</small>
</p>
    <p> 
<a href="dev_Navigation.aspx?MODE=edit">Edit first record</a>
<br />
<small>MODE=edit</small>
</p>

    <p> 
<a href="dev_Navigation.aspx?MODE=search">Search</a>
<br />
<small>MODE=search</small>
</p>
    <p>
         
<a href="dev_Navigation.aspx?MODE=searchadv">Advanced search</a>
<br />
<small>MODE=searchadv</small>
</p>
    <p> 
<a href="dev_Navigation.aspx?MODE=list">List of all records</a>
<br />
<small>MODE=list</small>
</p>
    <p> 
<a href="dev_Navigation.aspx?MODE=selections">Selections</a>
<br />
<small>MODE=selections</small>
</p>
    <p>
        <a href="dev_Navigation.aspx?MODE=charts">Charts</a>
        <br />
        <small>MODE=charts</small>
    </p>
	<p>
		<a href="dev_Navigation.aspx?MODE=export">Exports all records</a>
		<br />
		<small>MODE=export</small>
	</p>
    <p> 
<a href="dev_Navigation.aspx?QUERY=open">List result for "open"</a>
<br />
<small>QUERY=open</small>
</p>


</td>
<td >


	<EVOL:UIServer id="Evo1"   runat="server" CssClass="main1" BackColor="#EDEDED" Language="EN"
				BackColorRowMouseOver="Beige"  XMLfile="XML/PIM/todo.xml" VirtualPathToolbar="../PixEvo/"
				RowsPerPage="20" Width="100%" ToolbarPosition="Top"
				DisplayModeStart="List" SecurityModel="Single_User" DBAllowExport="True"  DBAllowPrint=true DBAllowSelections=true
				DesignWebPath="C:\Inetpub\wwwroot\Evolutilityweb\"  /> 


</td>
</tr></table>


	
</asp:Content>

