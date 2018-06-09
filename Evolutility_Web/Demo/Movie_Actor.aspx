<%@ Page validateRequest="false"  AutoEventWireup="true" CodeFile="demo.aspx.cs" CodeFileBaseClass="BasePage"
    Inherits="demo" Language="C#" MasterPageFile="zmDemo.master"
    Menus="demo_movie" SubMenuID="210" Title="Evolutility :: Demo > Movie Actor" %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

			<H1>Actor</H1>
			
			<EVOL:UIServer id="Evo1" runat="server" CssClass="main1" BackColor="#EDEDED"
				BackColorRowMouseOver="Beige" XMLfile="XML/Movies/MovieActor.xml" VirtualPathToolbar="../PixEvo/" 
				VirtualPathPictures="../Pix/Movies/" DBAllowInsertDetails=true DBAllowUpdateDetails=true
				RowsPerPage="20" Width="100%" ToolbarPosition="Top"  
				DisplayModeStart="List" SecurityModel="Single_User" DBAllowExport="True"   /> 

			<p><a href="XML/Movies/MovieActor.xml" target="db"><img src="../PixEvo/tag.png" class="icon" alt="XML"/> XML definition of this application</a>.</p>



</asp:Content>

