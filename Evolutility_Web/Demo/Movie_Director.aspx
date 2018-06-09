<%@ Page validateRequest="false"  AutoEventWireup="true" CodeFile="demo.aspx.cs" CodeFileBaseClass="BasePage"
    Inherits="demo" Language="C#" MasterPageFile="zmDemo.master"
    Menus="demo_movie" SubMenuID="220" Title="Evolutility :: Demo > Movie Director" %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

			<H1>Movie Director</H1>
			
			<EVOL:UIServer id="Evo1" title="AddressBook" runat="server" CssClass="main1" BackColor="#EDEDED"
				BackColorRowMouseOver="Beige" XMLfile="XML/Movies/MovieDirector.xml" VirtualPathToolbar="../PixEvo/" VirtualPathPictures="../Pix/Movies/"
				RowsPerPage="20" Width="100%" ToolbarPosition="Top" DesignDisplayMode="View"  
				DisplayModeStart="List" SecurityModel="Single_User" DBAllowExport="True"  
				 DBAllowInsertDetails=true DBAllowUpdateDetails=true
				DesignWebPath="C:\Inetpub\wwwroot\Evolutilityweb\"  /> 

			<p><a href="XML/Movies/MovieDirector.xml" target="db"><img src="../PixEvo/tag.png" class="icon" alt="XML"/> XML definition of this application</a>.</p>



</asp:Content>

