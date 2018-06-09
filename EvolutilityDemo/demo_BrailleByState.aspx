<%@ Page Language="C#" MasterPageFile="~/zmSimple.master" AutoEventWireup="true"  
Title="Evolutility :: Resources for the Blind Demo" %>
<%@ Register Assembly="Evolutility.WebControls" Namespace="Evolutility.WebControls" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<H1><img src="PixEvo/m-brl.gif" class="icon" alt=""> Resources for the Blind</H1>
			<P><EVOL:UIServer id="evo1" title="Resources for the Blind" runat="server" 
					ToolbarPosition="None" Width="100%" CssClass="main1" 
					SqlConnection="" VirtualPathToolbar="PixEvo" 
					BackColor="#EDEDED" BackColorRowMouseOver="Beige" 
					 UserComments="None" XMLfile="xml/Resource4BlindStates.xml"
					SecurityModel="Single_User" DBAllowDelete="False" DisplayModeStart="List" DBReadOnly="True" DBAllowSelections="True"
					ShowTitle="False" RowsPerPage="20" /></P>
			<P>See the <A href="XML/resource4blindstates.xml" target="db">Resource for the 
					Blind by State XML definition</A>.</P>

</asp:Content>

