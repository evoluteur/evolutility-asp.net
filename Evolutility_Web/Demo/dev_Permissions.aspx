<%@ Page validateRequest="false"   Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" CodeFile="dev_Permissions.aspx.cs" Inherits="demo_toolbar"
 Title="Evolutility :: Permissions"
CodeFileBaseClass="BasePage"  
Menus="demo_dev"
SubMenuID="3200"
  %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<H1>Permissions</H1>
 
    <table width=100%>
<tr valign=top>
<td width="200"> 


	
	<asp:CheckBox ID="RO" runat="server" AutoPostBack="true" Text="Read Only" OnCheckedChanged="RO_CheckedChanged" />
	<br />
	<div style="margin-left:20px;margin-top:5px">
	<asp:CheckBox ID="C" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="CUD_CheckedChanged"
		Text="Create" />
	<br />
	<asp:CheckBox ID="U" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="CUD_CheckedChanged"
		Text="Update" />
	<br />
	<asp:CheckBox ID="D" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="CUD_CheckedChanged"
		Text="Delete" /> 
	<br />
	</div>
	<br />
	<asp:CheckBox ID="R" runat="server" AutoPostBack="true" Checked="true" Text="Search" />
	<br />
	<asp:CheckBox ID="SEL" runat="server" AutoPostBack="true" Checked="true" Text="Selections" />
	<br />
	<asp:CheckBox ID="XPT" runat="server" AutoPostBack="true" Checked="true" Text="Export" />
	<br />
	<asp:CheckBox ID="PRT" runat="server" AutoPostBack="true" Checked="true" Text="Print" />
	<br />
</td>
<td >


	<EVOL:UIServer id="Evo1"   runat="server" CssClass="main1" BackColor="#EDEDED" Language="EN"
				BackColorRowMouseOver="Beige"  XMLfile="XML/PIM/todo.xml" VirtualPathToolbar="../PixEvo/"
				RowsPerPage="20" Width="100%" ToolbarPosition="Top" DesignDisplayMode="View" 
				DisplayModeStart="List" SecurityModel="Single_User" DBAllowExport="True"  DBAllowPrint=true
				DesignWebPath="C:\Inetpub\wwwroot\Evolutilityweb\"  /> 

</td>
</tr></table>




</asp:Content>

