<%@ Page Language="C#" MasterPageFile="~/zmSimple.master" AutoEventWireup="true"  
Title="Evolutility :: Memopad Demo" %>
<%@ Register Assembly="Evolutility.WebControls" Namespace="Evolutility.WebControls" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">		
<H1><img src="PixEvo/memo.gif" class="icon">Memo pad</H1>
			<EVOL:UIServer id="evo1" runat="server" CssClass="main1" BackColor="#EDEDED" BackColorRowMouseOver="Beige"
				  XMLfile="xml/memo.xml" VirtualPathToolbar="PixEvo"  
				  RowsPerPage="50" Width="100%"
				dballowexport="true" ToolbarPosition="Top" DisplayModeStart="List"
				 DesignWebPath="C:\Inetpub\wwwroot\Evolutility\" SecurityModel="Single_User" />
		<P>See the <a href="XML/memo.xml" target="db">memo pad XML definition</a>.</P>
</asp:Content>

