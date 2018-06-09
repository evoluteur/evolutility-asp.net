<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="MoreStuff" 
Title="Evolutility :: License"
CodeFileBaseClass="BasePage" 
Menus="more"
SubMenuID="3010"
 %>
<%@ Register Src="../Product/License.ascx" TagName="License" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<uc1:License ID="License1" runat="server" />

</asp:Content>

