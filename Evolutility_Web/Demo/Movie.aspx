<%@ Page validateRequest="false"  Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" 
CodeFile="demo.aspx.cs" Inherits="demo"
 Title="Evolutility :: Demo > Movie"
CodeFileBaseClass="BasePage"  
Menus="demo_movie" 
SubMenuID="200"
  %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<H1>Movie</H1>
	
	<EVOL:UIServer id="Evo1" runat="server" BackColor="#EDEDED"
		BackColorRowMouseOver="Beige" XMLfile="XML/Movies/movie.xml" VirtualPathToolbar="../PixEvo/" VirtualPathPictures="../Pix/Movies/"
		RowsPerPage="20" Width="100%" ToolbarPosition="Top" DBAllowInsertDetails="true" DBAllowUpdateDetails="true"
		DisplayModeStart="List" SecurityModel=Single_User DBAllowExport="True"  /> 

	<p><a href="XML/Movies/movie.xml" target="db"><img src="../PixEvo/tag.png" class="icon" alt="XML"/> XML definition of this application</a>.</p>



</asp:Content>

