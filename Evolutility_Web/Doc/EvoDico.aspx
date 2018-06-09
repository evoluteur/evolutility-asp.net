<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="EvoDico.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="1500"
Meta_Description="Evolutility Documentation" 
Meta_Keywords="documentation developer user guide"
%> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<h1>Evolutility Dictionary</h1>

<p>"EvoDico" is a web application to manage other web applications... an application which can modify itself. 
No more XML, the applications descriptions are stored in a <a href="EvoDico_DB.aspx">database dictionary</a>    
and edited directly with <a href="EvoDico_App.aspx">a CRUD application to modify other CRUD applications</a>.</p>
 
	<img alt="" src="pix/EvoDico/splash.gif" style="margin-left: 40px;border:solid 1 #a0a0a0"  class="shadow" />
	 
 
 <p>EvoDico doesn't support all features of Evolutility with XML documents.
 </p>
 
<table class="SimpleGrid">
<tr class="RowHeader"> 
<td>Element</td>
<td>Supported in XML</td>
<td>Supported by EvoDico</td>
</tr>  
<tr>
	<td><a href="ED_Elem_Form.aspx">form</a></td> 
	<td><img src="pix/checkb.gif" class="icon" /></td>
	<td><img src="pix/checkg.gif" class="icon" /></td>
</tr> 
<tr class="RowOdd">
	<td><a href="ED_Elem_Form.aspx#data">data</a></td> 
	<td><img src="pix/checkb.gif" class="icon" /></td>
	<td><img src="pix/checkg.gif" class="icon" /></td>
</tr>
<tr>
	<td><a href="ED_Elements_Groups.aspx#panel">panel</a></td> 
	<td><img src="pix/checkb.gif" class="icon" /></td>
	<td><img src="pix/checkg.gif" class="icon" /></td>
</tr> 
<tr class="RowOdd">
	<td><a href="ED_Elem_Field.aspx">field</a></td> 
	<td><img src="pix/checkb.gif" class="icon" /></td>
	<td><img src="pix/checkg.gif" class="icon" /></td>
</tr> 
<tr>
	<td><a href="ED_Elements_Groups.aspx#panel-details">panel-details</a></td> 
	<td><img src="pix/checkb.gif" class="icon" /></td>
	<td></td>
</tr> 
<tr class="RowOdd">
	<td><a href="ED_Elements_Groups.aspx#tab">tab</a>	</td> 
	<td><img src="pix/checkb.gif" class="icon" /></td>
	<td></td>
</tr> 
<tr>
	<td><a href="ED_Elem_Queries.aspx#queries">queries and query</a></td> 
	<td><img src="pix/checkb.gif" class="icon" /></td>
	<td></td>
</tr>  
</table> 
   
</asp:Content>

