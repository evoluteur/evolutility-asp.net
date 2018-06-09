<%@ Page validateRequest="false"   Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" CodeFile="dev_Events.aspx.cs" Inherits="demo_event"
 Title="Evolutility :: Demo : Server Events"
CodeFileBaseClass="BasePage"  
Menus="demo_dev"
SubMenuID="3300"
  %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Server Events</h1>

 <p>Server events tracked: Login, Logout, Create, Update, and Delete.</p>

<table width="100%">
<tr valign="top">
<td width="200px" class="LightGrey" >
<h3>Event log</h3>
	<small><asp:Label ID="Label1" runat="server" Text=""></asp:Label></small>
	<br /> 
</td>
<td >
 
	<EVOL:UIServer id="Evo1"   runat="server" CssClass="main1" BackColor="#EDEDED" Language="EN"
				BackColorRowMouseOver="Beige"  XMLfile="XML/PIM/AddressBook_EN.xml" VirtualPathToolbar="../PixEvo/"
				RowsPerPage="20" Width="100%" ToolbarPosition="Top" 
				DisplayModeStart="List" SecurityModel=Multiple_Users_RLS DBAllowExport="True"  DBAllowPrint="false"
				SecurityKey="DEMO"  OnDBChange="Evo1_DBChange" OnCredentialChange="Evo1_CredentialChange"  /> 

<p>Use John/John or Mary/Mary as your login/password.</p>
</td>
</tr></table>

</asp:Content>

