<%@ Page validateRequest="false"  Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" 
CodeFile="demo.aspx.cs" Inherits="demo" 
Title="Evolutility :: AddressBook Community Demo" 
CodeFileBaseClass="BasePage"  
Menus="demo_mu"
SubMenuID="2" 
 %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">			
<H1><IMG hspace="5" src="../PixEvo/contactshared.gif" border="0">Community Address Book
			</H1>
			<p>This demo is setup for record and comments sharing. Use <b>John/John</b>
			or <b>Mary/Mary</b> as your login/password to <a href="javascript:EvPost('49')">
				login</a>.</p>
				
<EVOL:UIServer ID="Evol" runat="server"  
 XMLfile="XML/PIM/addressbook_EN.xml" VirtualPathToolbar="../PixEvo"
					Width="100%" ToolbarPosition="Top" DisplayModeStart="List"
					  SecurityModel="Multiple_Users_Collaboration" UserComments=Logged_Users
					DBAllowInsert="True" DBAllowDelete="True" DBAllowUpdate="True" 
					 NavigationLinks="True" RowsPerPage="20"
					DBAllowExport="True"  SecurityKey="Shared" />
				 

</asp:Content>

