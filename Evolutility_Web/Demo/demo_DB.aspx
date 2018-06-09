<%@ Page validateRequest="false"  Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" 
CodeFile="demo.aspx.cs" Inherits="demo" 
Title="Evolutility :: Database Design Documents Demo" CodeFileBaseClass="BasePage"  
Menus="demos"
SubMenuID="180"
  %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<h1><img class="icon" src="../PixEvo/db.gif" alt=""> Database Design Documents</h1>
			<p>This document is generated on the fly (therefore always up-to-date and 
				available), using Evolutility to query SQLServer dictionary.</p>
			<p><EVOL:UIServer id="evo1"  runat="server" UserComments="None" 
					DBAllowDelete="False" 
					DBAllowInsert="False" 
					DBAllowUpdate="False"
					DBAllowExport="true"
					
					 BackColorRowMouseOver="Beige" BackColor="#EDEDED"  
					  XMLfile="xml/dbobjects.xml" VirtualPathToolbar="../PixEvo"  
					 CssClass="main1" Width="100%"  
					 DBAllowSelections=true 
					VirtualPathPictures="../PixEvo/" 
					DisplayModeStart="List" RowsPerPage="100" DBReadOnly="True" SecurityModel="Single_User"
					SecurityKey="DBdocs" />
			</p>
			<p>The data used on this page comes from the following SQLServer system tables 
				SYSOBJECTS, SYSCOLUMNS, and SYSTYPES.</p>

			<p><a href="XML/dbobjects.xml" target="db"><img src="../PixEvo/tag.png" class="icon" alt="XML"/> XML definition of this application</a>.</p>


</asp:Content>

