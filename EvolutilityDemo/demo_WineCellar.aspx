<%@ Page Language="C#" MasterPageFile="~/zmSimple.master" AutoEventWireup="true"  
Title="Evolutility :: Wine Cellar Demo" %>
<%@ Register Assembly="Evolutility.WebControls" Namespace="Evolutility.WebControls" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<h1><img src="PixEvo/m-wine.gif" class="icon"/>Wine Cellar</h1>
			<P><EVOL:UIServer id="evo1" runat="server" RowsPerPage="100" Height="374px"  
					CollapsiblePanels="False" VirtualPathPictures="pix/wine/" ToolbarPosition="Top"  
					 CssClass="main1" DBAllowInsertDetails="true" DBAllowUpdateDetails="true" 
					Width="100%" VirtualPathToolbar="PixEvo" XMLfile="xml/winecellar.xml" NavigationLinks="true" dballowexport="true"
					DisplayModeStart="List" UserComments="None" SecurityModel="Single_User" /></P> 
</asp:Content>

