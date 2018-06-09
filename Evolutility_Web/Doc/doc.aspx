<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="Doc.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility Dictionary :: Documentation" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="2000"
Meta_Description="Evolutility Dictionary Documentation" 
Meta_Keywords="documentation developer user guide"
%> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
<table class="groupedColumns"><tr valign="top">
<td><h1>Evolutility documentation</h1></td>
<td align="right"><nobr>2/12/2013
<br /><b>Version 4.1</b></nobr>
</tr></table>  


<table class="groupedColumns" cellspacing="15"><tr valign="top">
<td>
	<a href="evodoc.aspx"><img src="pix/ui/a_evol.gif" style="border:solid 1 #a0a0a0" class="shadow" vspace="8"></a>
</td><td>
	<h2><a href="evodoc.aspx">Evolutility</a></h2> 
	<p>CRUD framework for ASP.net and SQL Server, using XML to store the applications metadata.</p>
</td></tr><tr><td>
	<a href="ed_metamodel.aspx"><img src="pix/ui/a_xml.gif" style="border:solid 1 #a0a0a0" class="shadow" vspace="8"></a>
</td><td>
	<h2><a href="ed_metamodel.aspx">Evolutility Meta-model</a></h2> 
	<p>The simple declarative language to describe application models (UI and database).  </p>
</td></tr><tr><td>
	<a href="EvoDico.aspx"><img src="pix/ui/a_evodico.gif" style="border:solid 1 #a0a0a0" class="shadow" vspace="8"></a>
</td><td>
	<h2><a href="EvoDico.aspx">Evolutility Dictionary</a></h2>  
	<p>By storing the metadata directly in the database (instead of XML), 
	it is now possible to modify the application with the application itself. </p> 
</td></tr></table> 

<p>For an overview of the metadata read the <a target="doc" href="EvoDoc2_Articles.aspx">articles about Evolutility</a>.</p>
 
<br />&nbsp;

</asp:Content>

