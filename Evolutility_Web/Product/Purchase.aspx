<%@ Page AutoEventWireup="true" CodeFile="Product.aspx.cs" CodeFileBaseClass="BasePage"
    Inherits="ProductPage" Language="C#" MasterPageFile="zmProduct.master" 
	Menus="product"
	SubMenuID="20"
	Meta_Description="Purchase Evolutility" 
	Meta_Keywords="buy purchase get evolutility license"
	Title="Purchase Evolutility" %>
<%@ Register Src="../Product/Purchase.ascx" TagName="Purchase" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h1>Purchase Evolutility</h1>
  
	<p>Purchase Evolutility today!</p>
<uc1:Purchase ID="Purchase1" runat="server" />

</asp:Content>

