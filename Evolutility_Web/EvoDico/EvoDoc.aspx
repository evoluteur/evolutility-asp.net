<%@ Page Language="C#" MasterPageFile="zmEvoDico.master" AutoEventWireup="true" 
CodeFile="EvoDoc.aspx.cs" Inherits="EvoDicoWiz" 
CodeFileBaseClass="BasePage" 
Title="EvoDico :: App Wizard"  
Menus="evodico"
SubMenuID="102"
%>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<h1 id="docTitle" runat="server">Design document: entities/forms</h1>
			<p><EVOL:UIServer ID="evo1" runat="server"  DataIsMetadata=true 
					DBAllowDelete="False" DBAllowExport="True" DBAllowSelections="false" DBReadOnly="true"
					DisplayModeStart="List" RowsPerPage="100" 
					SecurityModel="Single_User_Password" SecurityKey="EvoDico"
					 ShowTitle="true" 
					SqlConnection="" ToolbarPosition="Top" VirtualPathPictures="../pixevo/" VirtualPathToolbar="../PixEvo"
					Width="100%" XMLfile="XML/evoDoc_Form.xml" /></p> 

</asp:Content>

