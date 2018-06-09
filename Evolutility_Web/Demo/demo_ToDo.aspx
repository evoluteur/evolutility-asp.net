<%@ Page AutoEventWireup="true" CodeFile="demo.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="demo" Language="C#" MasterPageFile="zmDemo.master" Menus="demos"
	Meta_Description="Evolutility To do Demo." 
	SubMenuID="12" Title="Evolutility :: To do Demo" ValidateRequest="false" %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

<H1><img src="../PixEvo/todo.gif" class="icon" alt=""/>To do</H1>

			<EVOL:UIServer id="Evo1" runat="server" CssClass="main1" BackColor="#EDEDED" Language="EN"
				BackColorRowMouseOver="Beige"  XMLfile="XML/PIM/ToDo.xml" VirtualPathToolbar="../PixEvo/"
				RowsPerPage="20" Width="100%" ToolbarPosition="top"  NavigationLinks="true" DBAllowSelections="true"
				DisplayModeStart="List" SecurityModel="Single_User" DBAllowExport="True" 
				DBAllowMassUpdate=true  DBAllowCharts=true   />


			<p><a href="XML/PIM/todo.xml" target="db"><img src="../PixEvo/tag.png" class="icon" alt="XML"/> XML definition of this application</a>.</p>

</asp:Content>

