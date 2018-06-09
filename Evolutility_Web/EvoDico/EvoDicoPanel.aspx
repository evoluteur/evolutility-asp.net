<%@ Page Language="C#" MasterPageFile="zmEvoDico.master" AutoEventWireup="true" CodeFile="EvoDicoPanel.aspx.cs" Inherits="EvoDicoWiz" 
CodeFileBaseClass="BasePage" 
Title="EvoDico :: Panels"  
Menus="evodico"
SubMenuID="3"
%>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<h1>Panels</h1>
			<p><EVOL:UIServer id="evo1" runat="server"  DataIsMetadata=true
					 ToolbarPosition="Top" Width="100%" CssClass="main1" 
					BackColor="#EDEDED" BackColorRowMouseOver="Beige"
					 UserComments="None" SqlConnection=""
					XMLfile="XML/evoDico_Panel.xml" 
					 VirtualPathToolbar="../PixEvo/"
					 VirtualPathPictures="../PixEvo/"
					 DBAllowSelections="false" 
					 SecurityModel="Single_User_Password" SecurityKey="EvoDico"
					DBAllowDelete="true" DisplayModeStart="List" ShowTitle="true" DBAllowExport="True" RowsPerPage="20" /></p>

</asp:Content>

