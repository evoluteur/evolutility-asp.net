<%@ Page Language="C#" MasterPageFile="zmEvoDico.master" AutoEventWireup="true" CodeFile="EvoDicoWiz.aspx.cs" Inherits="EvoWiz" 
 validateRequest="false"
CodeFileBaseClass="BasePage" 
Title="EvoDico :: App Wizard"  
Menus="evodico"
SubMenuID="120"
 %>

<%@ Register Assembly="Evolutility.Wizard" Namespace="Evolutility" TagPrefix="EVOL" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <EVOL:Wizard ID="EvoDicoWiz1" runat="server"  
        BuildDatabase="true" MustLogin="true" 
			PathWeb="XML/" PathXML="XML/" ShowASPX="false" ShowSkin="false"
			ShowSQL="true" ShowXML="true" VirtualPathPictures="../pixEvo/" WizardMode="Build"  />
    </div> 

</asp:Content>
