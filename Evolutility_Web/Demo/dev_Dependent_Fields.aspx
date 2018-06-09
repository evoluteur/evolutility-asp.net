<%@ Page AutoEventWireup="true" CodeFile="demo.aspx.cs" 
CodeFileBaseClass="BasePage" Inherits="demo" 
	Language="C#" MasterPageFile="zmDemo.master" Menus="demo_dev"
	Meta_Description="Evolutility Demo custom form validation" SubMenuID="3310" Title="Evolutility :: Demo : Custom Validation"
	ValidateRequest="false" %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

<h1>Dependent fields</h1>

<p>In this example, selecting a country updates the list of cities.</p> 

	<EVOL:UIServer id="Evo1" runat="server" CssClass="main1" BackColor="#EDEDED" Language="EN" DisplayModeStart="NewItem"
	BackColorRowMouseOver="Beige"  XMLfile="xml/dependency_cc.xml" VirtualPathToolbar="../PixEvo/"
	RowsPerPage="20" Width="100%" ToolbarPosition="top"  NavigationLinks="true" DBAllowSelections="false"
	SecurityModel="Single_User" DBAllowExport="false" 
	  />
	 
<p>Learn more about <a href="../doc/EvoDoc_Dependent_Fields.aspx">dependent fields</a> in the documentation.</p>
</asp:Content>

