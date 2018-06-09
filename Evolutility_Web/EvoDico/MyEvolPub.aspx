<%@ Page Language="C#" MasterPageFile="zmMySite-green.master" AutoEventWireup="true" CodeFile="MyEvolPub.aspx.cs" 
Inherits="EvoDico_MyEvolPub" Title="Shared Applications" %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>  

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

			<EVOL:UIServer ID="Evo1" runat="server" BackColor="#EDEDED" BackColorRowMouseOver="Beige"
			 XMLfile="../xml/evodemo/todo.xml"
			  DBReadOnly="false" UserComments="Logged_Users" SecurityModel="Multiple_Users_Collaboration"
				 DBAllowDesign="false" DBAllowExport="True" DisplayModeStart="List"  ShowTitle="true"
				Language="EN" NavigationLinks="true" RowsPerPage="20"
				VirtualPathDesigner= "../EvoDico/"   SecurityKey="EvoDico" 
				ToolbarPosition="top" VirtualPathPictures="../PixEvo/" VirtualPathToolbar="../PixEvo/"
				Width="100%"  /> 
	<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
				
</asp:Content>
  
	
	