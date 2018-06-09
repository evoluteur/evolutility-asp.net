<%@ Page AutoEventWireup="true" CodeFile="MyEvol.aspx.cs" Inherits="EvoDico_MyEvol" CodeFileBaseClass="BasePage" 
	Language="C#" MasterPageFile="zmMyEvolutility.master"  ValidateRequest="false" 
	Meta_Description="My Web Application powered by Evolutility CRUD framework."
	Title="My Applications" %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

			<EVOL:UIServer ID="Evo1" runat="server" BackColor="#EDEDED" BackColorRowMouseOver="Beige"
			 XMLfile="../xml/evodemo/todo.xml"
				CssClass="main1" DBAllowDesign="true" DBAllowExport="True" DisplayModeStart="List"  ShowTitle="true"
				Language="EN" NavigationLinks="true" RowsPerPage="20" SecurityModel="Single_User_Password"
				VirtualPathDesigner= "../EvoDico/"  SecurityKey="EvoDico"
				ToolbarPosition="top" VirtualPathPictures="../PixEvo/" VirtualPathToolbar="../PixEvo/"
				Width="100%"  /> 
	<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</asp:Content>
 