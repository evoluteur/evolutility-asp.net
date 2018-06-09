<%@ Page validateRequest="false" Language="C#" MasterPageFile="zmEvoDico.master" AutoEventWireup="true" CodeFile="EvoDicoTest.aspx.cs" Inherits="EvoDicoTest" 
CodeFileBaseClass="BasePage" 
Title="My Applications"  
Menus="evodico"
SubMenuID="1200"
%>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
 	<h1 runat="server" id="txtp" visible="false">No application specified</h1>
 	
    <p id="evop" runat="server" >	
		<EVOL:UIServer ID="Evo1" runat="server"  DataIsMetadata=false
		BackColor="#EDEDED" BackColorRowMouseOver="Beige"
			CssClass="main1" DBAllowDelete="true" DBAllowDesign="true" DBAllowExport="True"
			DBAllowSelections="false" DisplayModeStart="List" RowsPerPage="20" 
			SecurityModel="Single_User_Password" SecurityKey="EvoDico"
			ShowTitle="true" SqlConnection="" ToolbarPosition="Top" UserComments="None" 
			VirtualPathPictures="../pixevo/"
			 VirtualPathToolbar="../pixevo/"  
			 Width="100%" XMLfile="" /> 
			</p> 
</asp:Content>

