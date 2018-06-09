<%@ Page Language="C#" MasterPageFile="~/zmSimple.master" AutoEventWireup="true" 
 Title="Evolutility :: Address Book Demo" %>
<%@ Register Assembly="Evolutility.WebControls" Namespace="Evolutility.WebControls" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<H1><img src="PixEvo/contact.gif" class="icon">Address book</H1>
			<EVOL:UIServer id="Evo1" runat="server" CssClass="main1"
				BackColorRowMouseOver="Beige" XMLfile="xml/addressbook.xml" VirtualPathToolbar="PixEvo/" 
				RowsPerPage="20" Width="100%" ToolbarPosition="Top" DesignDisplayMode="View"
				DisplayModeStart="List" SecurityMode="Single_User" DBAllowExport="True" /> 
			<P>
				See the <A href="XML/addressbook.xml" target="db">address book XML 
					definition</A>.</P>

</asp:Content>

