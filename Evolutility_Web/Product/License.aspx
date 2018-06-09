<%@ Page AutoEventWireup="true" CodeFile="Product.aspx.cs" CodeFileBaseClass="BasePage"
    Inherits="ProductPage" Language="C#" MasterPageFile="zmProduct.master" 
Menus="product"
SubMenuID="10"
    Meta_Description="Evolutility licensing & open source" 
    Meta_Keywords="licensing open source affero gpl"
    Title="Evolutility Web Site" %>
<%@ Register Src="License.ascx" TagName="License" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<uc1:License ID="License1" runat="server" />

</asp:Content>