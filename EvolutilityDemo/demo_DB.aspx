<%@ Page Language="C#" MasterPageFile="~/zmSimple.master" AutoEventWireup="true"  
Title="Evolutility :: Database structure document" %>
<%@ Register Assembly="Evolutility.WebControls" Namespace="Evolutility.WebControls" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">			
<H1><img src="PixEvo/m-db.gif" class="icon">Database Structure</H1>
			<EVOL:UIServer id="evo1" runat="server" XMLfile="xml/DBObjects.xml" VirtualPathPictures="PixEvo/"
				VirtualPathToolbar="PixEvo" dballowexport="false" dballowselections="true" Width="100%" 
				DisplayModeStart="List" BackColorRowMouseOver="Beige"  DBReadOnly=true
				language="EN" />
		<P>See the <a href="XML/DBObjects.xml" target="db"> XML definition</a>.</P>
</asp:Content>

