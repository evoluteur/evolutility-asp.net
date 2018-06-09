<%@ Page AutoEventWireup="true" CodeFile="demo.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="demo" Language="C#" MasterPageFile="zmDemo.master" Menus="demos"
	Meta_Description="Evolutility Bookmarks Demo." 
	SubMenuID="16" Title="Evolutility :: Bookmarks" ValidateRequest="false" %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

<H1><img src="../PixEvo/favourites.gif" class="icon" alt=""/>Bookmarks</H1>

			<EVOL:UIServer id="Evo1" runat="server" CssClass="main1" BackColor="#EDEDED" Language="EN"
				BackColorRowMouseOver="Beige"  XMLfile="XML/PIM/Bookmark.xml" VirtualPathToolbar="../PixEvo/"
				RowsPerPage="20" Width="100%" ToolbarPosition="top"  NavigationLinks="true" DBAllowSelections="false"
				DisplayModeStart="List" SecurityModel="Single_User" DBAllowExport="True"   />


			<p><a href="XML/PIM/Bookmark.xml" target="db"><img src="../PixEvo/tag.png" class="icon" alt="XML"/> XML definition of this application</a>.</p>

</asp:Content>

