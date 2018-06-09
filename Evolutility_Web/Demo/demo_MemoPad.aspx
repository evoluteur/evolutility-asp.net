<%@ Page AutoEventWireup="true" CodeFile="demo.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="demo" Language="C#" MasterPageFile="zmDemo.master" Menus="demos"
	Meta_Description="Evolutility Memo pad Demo." 
	SubMenuID="14" Title="Evolutility :: Memo pad Demo" ValidateRequest="false" %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

<h1><img src="../PixEvo/memo.gif" class="icon" alt=""/>Memo pad</h1>

	<EVOL:UIServer id="Evo1"   runat="server" CssClass="main1" BackColor="#EDEDED" Language="EN"
		BackColorRowMouseOver="Beige"  XMLfile="XML/PIM/memo.xml" VirtualPathToolbar="../PixEvo/"
		RowsPerPage="20" Width="100%" ToolbarPosition="top"  NavigationLinks="true" DBAllowDesign="true"
		DisplayModeStart="List" SecurityModel="Single_User" DBAllowExport="True"   />
		
	<p><a href="XML/PIM/memo.xml" target="db"><img src="../PixEvo/tag.png" class="icon" alt="XML"/> XML definition of this application</a>.</p>

</asp:Content>

