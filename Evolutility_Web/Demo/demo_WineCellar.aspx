<%@ Page validateRequest="false"  Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" 
CodeFile="demo.aspx.cs" Inherits="demo" 
Title="Evolutility :: WineCellar Demo"
CodeFileBaseClass="BasePage"
Menus="demos"
SubMenuID="140"

 %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<h1><img hspace="5" src="../PixEvo/wine.gif" border="0"/> Wine Cellar</h1>
			<p><EVOL:UIServer id="evo1" runat="server" RowsPerPage="100" Height="374px" NavigationLinks="true"
					CollapsiblePanels="true" VirtualPathPictures="../pix/wine/" 
					ToolbarPosition="Top" 
					TabPosition="Top" 
					BackColorRowMouseOver="Beige" CssClass="main1" 
					DBAllowInsertDetails="true" 
					DBAllowUpdateDetails="true" 
					DBAllowExport="true" 
					DBAllowSelections="true" 
					Width="100%" VirtualPathToolbar="../PixEvo" XMLfile="XML/winecellar/winecellar.xml" BackColor="#EDEDED"
					DisplayModeStart="List" 
					UserComments="None" SecurityModel="Single_User" /></p> 

			<p><a href="XML/winecellar/winecellar.xml" target="db"><img src="../PixEvo/tag.png" class="icon" alt="XML"/> XML definition of this application</a>.</p>


</asp:Content>

