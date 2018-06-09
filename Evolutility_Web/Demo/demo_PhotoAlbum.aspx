<%@ Page validateRequest="false"  Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" 
CodeFile="demo.aspx.cs" Inherits="demo" 
Title="Evolutility :: Photo Album Demo"  
CodeFileBaseClass="BasePage"  
Menus="demos"
SubMenuID="130"
  %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<H1><IMG hspace="5" src="../PixEvo/photo.gif" border="0"> Photo Albums</H1>
			<P>
				<EVOL:UIServer id="evo1" runat="server" 
				 XMLfile="XML/PIM/PhotoAlbum.xml" VirtualPathToolbar="../PixEvo" 
					Width="100%" ToolbarPosition="Top" DesignDisplayMode="View" DisplayModeStart="List"
					BackColor="#EDEDED" BackColorRowMouseOver="Beige" RowsPerPage="20" DesignWebPath-Length="25"
					DesignWebPath="C:\Inetpub\wwwroot\Evolutility\" CollapsiblePanels="False" VirtualPathPictures="../pix/photoalbum/"
					 DBAllowDelete="True" DBAllowInsert="True" DBAllowUpdate="False" ShowTitle="False"
					UseCache="True" DBAllowPrint="True" /></P> 

			<p><a href="XML/PIM/PhotoAlbum.xml" target="db"><img src="../PixEvo/tag.png" class="icon" alt="XML"/> XML definition of this application</a>.</p>


</asp:Content>

