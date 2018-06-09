<%@ Page Language="C#" MasterPageFile="~/zmSimple.master" AutoEventWireup="true"  
Title="Evolutility :: ToDo Demo" %>
<%@ Register Assembly="Evolutility.WebControls" Namespace="Evolutility.WebControls" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">			
<H1><img src="PixEvo/m-todo.gif" class="icon">To do</H1>
			<EVOL:UIServer id="evo1" title="ToDo" runat="server" XMLfile="xml/ToDo.xml"
				VirtualPathToolbar="PixEvo" dballowexport="true" dballowselections="true" Width="100%" 
				DisplayModeStart="List" BackColorRowMouseOver="Beige"  />
		<P>See the <a href="XML/todo.xml" target="db">to do XML definition</a>.</P>
</asp:Content>

