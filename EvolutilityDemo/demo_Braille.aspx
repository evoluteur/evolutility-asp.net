<%@ Page Language="C#" MasterPageFile="~/zmSimple.master" AutoEventWireup="true"   
Title="Evolutility :: Resources for the Blind Demo" %>
<%@ Register Assembly="Evolutility.WebControls" Namespace="Evolutility.WebControls" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<H1><img src="PixEvo/m-brl.gif" class="icon" alt="">Resources for the Blind</H1>
			<P><EVOL:UIServer id="evo1" runat="server" VirtualPathPictures="pix/"
					 ToolbarPosition="Top" Width="100%" CssClass="main1"  
					 SqlConnection="" VirtualPathToolbar="PixEvo"
					XMLfile="xml/Resource4Blind.xml" DBAllowSelections="True" SecurityModel="Single_User"
					DBAllowDelete="False" DisplayModeStart="List" ShowTitle="true" DBAllowExport="True" RowsPerPage="20" /></P>
			<P>See the <a href="XML/resource4blind.xml" target="db">Resource for the 
					Blind XML definition</a>.</P>
</asp:Content>

