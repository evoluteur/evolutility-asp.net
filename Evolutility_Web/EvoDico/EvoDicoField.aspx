<%@ Page Language="C#" MasterPageFile="zmEvoDico.master" AutoEventWireup="true" CodeFile="EvoDicoField.aspx.cs" Inherits="EvoDicoWiz" 
CodeFileBaseClass="BasePage" 
Title="EvoDico :: Fields"  
Menus="evodico"
SubMenuID="2"
%>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script src="../PixEvo/JS/EvoDicoRules.js" type="text/javascript"></script> 

			<h1><img src="../pixevo/edi_fld.png" class="icon" alt=""/> Fields</h1>
			<p><EVOL:UIServer ID="evo1" runat="server"  DataIsMetadata=true 
					CssClass="main1" 
					DBAllowDelete="true" 
					DBAllowExport="True" 
					DBAllowSelections="false"
					DisplayModeStart="List" 
					DBAllowHelp="true"
					RowsPerPage="20" 
					
					SecurityModel="Single_User_Password" SecurityKey="EvoDico"
					ShowTitle="true"
					SqlConnection="" ToolbarPosition="Top" UserComments="None" 
					VirtualPathPictures="../pixevo/"
					 VirtualPathToolbar="../pixevo/" 
					 Width="100%" 
					 XMLfile="XML/EvoDico_Field.xml" /></p>
			 
</asp:Content>

