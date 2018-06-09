<%@ Page validateRequest="false"  Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" 
CodeFile="demo.aspx.cs" Inherits="demo" 
Title="Evolutility :: Photo Album Demo" 
CodeFileBaseClass="BasePage"  
Menus="demos"
SubMenuID="130"
  %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<h1><img hspace="5" src="../PixEvo/photo.gif" border="0"> Photos</h1>
			<p>
				<EVOL:UIServer id="evo1" runat="server" 
				 XMLfile="XML/PIM/Photo.xml" VirtualPathToolbar="../PixEvo" 
			 CssClass="main1" BackColor="#EDEDED" Language="EN"
			VirtualPathPictures="../pix/photoalbum/" CollapsiblePanels="False" DisplayModeStart="View"
				BackColorRowMouseOver="Beige" 
				RowsPerPage="20" Width="100%" ToolbarPosition="top"  NavigationLinks="true" 
				 SecurityModel="Single_User" DBAllowExport="True"   />
</p>
			<p><a href="XML/PIM/Photo.xml" target="db"><img src="../PixEvo/tag.png" class="icon" alt="XML"/> XML definition of this application</a>.</p>


</asp:Content>

