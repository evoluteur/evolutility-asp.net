<%@ Page validateRequest="false"  Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" 
CodeFile="demo.aspx.cs" Inherits="demo" 
Title="Evolutility :: Wine Degustation Demo" 
CodeFileBaseClass="BasePage"
Menus="demos"
SubMenuID="141" %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<H1><IMG hspace="5" src="../PixEvo/wine.gif" border="0"> Wine Tasting</H1>
			<P><EVOL:UIServer id="evo1"  runat="server" RowsPerPage="100" Height="374px" NavigationLinks="true"
					CollapsiblePanels="False" VirtualPathPictures="../pix/wine/" ToolbarPosition="Top" BackColorRowMouseOver="Beige"
					Width="100%" VirtualPathToolbar="../PixEvo" XMLfile="XML/winecellar/winetasting.xml" BackColor="#EDEDED"
					DisplayModeStart="List" DesignDisplayMode="List" DesignWebPath="C:\Inetpub\wwwroot\Evolutility\"
					UserComments="None" SecurityModel="Single_User" /></P> 

			<p><a href="XML/winecellar/winetasting.xml" target="db"><img src="../PixEvo/tag.png" class="icon" alt="XML"/> XML definition of this application</a>.</p>


</asp:Content>

