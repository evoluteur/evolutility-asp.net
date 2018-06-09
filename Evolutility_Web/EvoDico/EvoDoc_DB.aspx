<%@ Page Language="C#" MasterPageFile="zmEvoDico.master" AutoEventWireup="true" 
CodeFile="EvoDoc_DB.aspx.cs" Inherits="EvoDicoWiz" 
CodeFileBaseClass="BasePage" 
Title="EvoDico :: App Database"  
Menus="evodico"
SubMenuID="105"
%>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<h1 id="docTitle" runat="server"><img src="../pixevo/db.gif" class="icon" alt=""/> Database objects</h1>
			<p><EVOL:UIServer ID="evo1" runat="server" 
					DBAllowDelete="False" DBAllowExport="True" DBAllowSelections="true" DBReadOnly="true"
					DisplayModeStart="List" RowsPerPage="100" 
					SecurityModel="Single_User_Password" SecurityKey="EvoDico"
					 ShowTitle="true" 
					SqlConnection="" ToolbarPosition="Top" VirtualPathPictures="../pixevo/" VirtualPathToolbar="../PixEvo"
					Width="100%" XMLfile="XML/evoDoc_DB.xml" /></p> 

</asp:Content>

