<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="MoreStuff" 
CodeFileBaseClass="BasePage"
	Title="Purchase Evolutility" 
	Menus="more"		
	SubMenuID="3020"
	Meta_Description="Purchase Evolutility" 
	Meta_Keywords="buy purchase get evolutility license" %>
<%@ Register Src="../Product/Purchase.ascx" TagName="Purchase" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
				<h1>Purchase Evolutility</h1>
  
	<p>Purchase Evolutility today!</p>
<uc1:Purchase ID="Purchase1" runat="server" />

</asp:Content>

