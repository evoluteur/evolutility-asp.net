<%@ Page validateRequest="false"   Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" 
CodeFile="demo.aspx.cs" Inherits="demo"
 Title="Evolutility :: Demo : Integrated Rich Text editor for HTML fields"
CodeFileBaseClass="BasePage"  
Menus="demo_dev"
SubMenuID="3100"
Meta_Keywords="CKeditor rich text editor html WYSIWYG"
  %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1>Rich Text Editor demo</h1> 
      
	<EVOL:UIServer id="Evo1" runat="server" CssClass="main1" BackColor="#EDEDED" Language="EN"
				BackColorRowMouseOver="Beige"  XMLfile="XML/RTF.xml" VirtualPathToolbar="../PixEvo/"
				RowsPerPage="20" Width="100%" ToolbarPosition="Top"
				DisplayModeStart="List" SecurityModel="Single_User" 
				DBAllowExport="True"  DBAllowPrint=true DBAllowSelections=false  /> 
  
</asp:Content>

