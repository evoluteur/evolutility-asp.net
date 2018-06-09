<%@ Page Language="C#" MasterPageFile="zmEvoDico.master" AutoEventWireup="true" 
CodeFile="EvoDoc_Export.aspx.cs" Inherits="EvoDicoWiz" 
CodeFileBaseClass="BasePage" 
Title="EvoDico :: Application Definition (design doc)"  
Menus="evodico"
SubMenuID="103"
%>

<%@ Register Assembly="Evolutility.ExportWizard" Namespace="Evolutility.ExportWizard"
	TagPrefix="EVOL" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<h1 id="docTitle" runat="server"><img src="../pixevo/db_go.png" class="icon" alt=""/> Export Wizard</h1>
			<p>		 
 <EVOL:ExportWizard ID="ExportWizard1" runat="server"  />
				&nbsp;</p> 

</asp:Content>

