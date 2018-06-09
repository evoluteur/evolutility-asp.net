<%@ Page AutoEventWireup="true" CodeFile="demo_addressbook.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="demo_addressbook" Language="C#" MasterPageFile="zmDemo.master" Menus="demos"
	Meta_Description="Evolutility Address Book Demo." 
    Meta_Keywords="open source object database driven addressbook address book contact management contacts list"
	SubMenuID="2" Title="Evolutility :: Address Book Demo" ValidateRequest="false"
	
	 %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

<table style="width:100%"><tr><td>
	<h1>
	<img src="../PixEvo/flags/en.gif" class="IconFlag" alt="" runat="server" id="flagpix"/><img src="../PixEvo/contact.gif" class="Icon16" alt=""/>
	 <asp:Label ID="Label1" runat="server" Text=""></asp:Label></h1>
</td><td align="right">
	<a href="demo_addressbook.aspx"><img src="../pixevo/flags/en.gif" class="IconFlag" alt="English"/></a>
	<a href="demo_addressbook.aspx?LNG=CA"><img alt="Catalan" class="IconFlag" src="../pixevo/flags/ca.gif" /></a> 
	<a href="demo_addressbook.aspx?LNG=ZH"><img alt="Chinese" class="IconFlag" src="../pixevo/flags/zh.gif" /></a>
	<a href="demo_addressbook.aspx?LNG=DA"><img alt="Danish" class="IconFlag" src="../pixevo/flags/da.gif" /></a> 
	<a href="demo_addressbook.aspx?LNG=FR"><img alt="French" class="IconFlag" src="../pixevo/flags/fr.gif" /></a> 
	<a href="demo_addressbook.aspx?LNG=DE"><img alt="German" class="IconFlag" src="../pixevo/flags/de.gif" /></a>
	<a href="demo_addressbook.aspx?LNG=HI"><img alt="Hindi" class="IconFlag" src="../pixevo/flags/hi.gif" /></a>
	<a href="demo_addressbook.aspx?LNG=IT"><img alt="Italian" class="IconFlag" src="../pixevo/flags/it.gif" /></a> 
	<a href="demo_addressbook.aspx?LNG=JP"><img alt="Japanese" class="IconFlag" src="../pixevo/flags/jp.gif"/></a> 
	<a href="demo_addressbook.aspx?LNG=FA"><img alt="Persian" class="IconFlag" src="../pixevo/flags/fa.gif" /></a>
	<a href="demo_addressbook.aspx?LNG=PT"><img alt="Portuguese" class="IconFlag" src="../pixevo/flags/pt.gif" /></a>
	<a href="demo_addressbook.aspx?LNG=RO"><img alt="Romanian" class="IconFlag" src="../pixevo/flags/ro.gif" /></a>
	<a href="demo_addressbook.aspx?LNG=ES"><img alt="Spanish" class="IconFlag" src="../pixevo/flags/es.gif" /></a>
	<a href="demo_addressbook.aspx?LNG=TR"><img alt="Turkish" class="IconFlag" src="../pixevo/flags/tr.gif" /></a>
</td></tr></table> 

			<EVOL:UIServer id="Evo1"   runat="server" CssClass="main1" BackColor="#EDEDED" Language="EN"
				BackColorRowMouseOver="Beige"  XMLfile="XML/PIM/AddressBook_EN.xml" VirtualPathToolbar="../PixEvo/"
				RowsPerPage="20" Width="100%" ToolbarPosition="top"  NavigationLinks="true" DBAllowDesign="true"
				DisplayModeStart="List" SecurityModel="Single_User" DBAllowExport="True"  />
 	
	<p>This demo can also work for <a href="demo_addressbook_sharing.aspx">multiple users</a> where 
				each user's data is password protected.</p>
	<p><a href="dev_LocXML.html" target="mdxml"><img src="../PixEvo/tag.png" class="Icon16" alt=""/> XML definition of this application</a>.</p>
	
</asp:Content>

