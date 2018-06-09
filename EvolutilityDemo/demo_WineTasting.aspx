<%@ Page Language="C#" MasterPageFile="~/zmSimple.master" AutoEventWireup="true"  
Title="Evolutility :: Wine Degustation Demo" %>
<%@ Register Assembly="Evolutility.WebControls" Namespace="Evolutility.WebControls" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<H1><IMG alt="" src="PixEvo/m-wine.gif" class="icon"> Wine Tasting</H1>
			<P><EVOL:UIServer id="evo1"  runat="server" RowsPerPage="100" NavigationLinks="true"
					CollapsiblePanels="False" VirtualPathPictures="pix/wine/" ToolbarPosition="Top" 
					Width="100%" VirtualPathToolbar="PixEvo" XMLfile="xml/winetasting.xml"  
					UserComments="None" SecurityModel="Single_User" /></P> 
</asp:Content>

