<%@ Page validateRequest="false"  Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" 
CodeFile="demo.aspx.cs" Inherits="demo" 
Title="Evolutility :: AddressBook RLS Demo" 
CodeFileBaseClass="BasePage"  
Menus="demo_mu"
SubMenuID="1"
 %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">			
<H1><IMG hspace="5" src="../PixEvo/contactrls.gif" border="0"> Address Book RLS
			</H1>
			<p>This demo is setup for row-level security (RLS). All users share the same 
			database tables but can only see and modify their own data. Use <b>John/John</b>
			or <b>Mary/Mary</b> as your login/password to <A href="javascript:EvPost('49')">
				login</A>.</p>
				
<EVOL:UIServer ID="Evolutility1" runat="server"  
 XMLfile="XML/PIM/addressbook_EN.xml" VirtualPathToolbar="../PixEvo"
					Width="100%" ToolbarPosition="Top" DisplayModeStart="List"
					  SecurityModel="Multiple_Users_RLS" SecurityKey="RLS" 
					DBAllowInsert="True" DBAllowDelete="True" DBAllowUpdate="True" NavigationLinks="True" RowsPerPage="20"
					DBAllowExport="True" />
				


</asp:Content>

