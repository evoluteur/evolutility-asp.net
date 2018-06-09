<%@ Page Language="C#" MasterPageFile="~/zmSimple.master" AutoEventWireup="true"  
Title="Evolutility :: Restaurants Demo" %>
<%@ Register Assembly="Evolutility.WebControls" Namespace="Evolutility.WebControls" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">			
<H1><img src="PixEvo/resto.gif" class="icon">Restaurants</H1>
			<P><EVOL:UIServer id="evo1" runat="server" CssClass="main1" BackColor="#EDEDED"
					BackColorRowMouseOver="Beige"  XMLfile="xml/restaurant.xml" VirtualPathToolbar="PixEvo"
					RowsPerPage="20" Width="100%" ToolbarPosition="Top" 
					SecurityModel="Single_User"
					dballowexport="true" DBAllowSelections="True" UserComments="None" /></P>
			<P>See the <A href="XML/restaurant.xml" target="db">Restaurant list XML 
					definition</A>.</P>

</asp:Content>

