<%@ Page AutoEventWireup="true" CodeFile="EvoDico.aspx.cs" 
	CodeFileBaseClass="BasePage"
	Inherits="EvoDoc" Language="C#" MasterPageFile="zmDoc.master" 
 	Meta_Description="Evolutility Dictionary Database documentation"
	Meta_Keywords="documentation developer user guide" 
	Title="Evolutility :: Documentation : Dictionary Database"
	Menus="doc"	
SubMenuID="1510" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 
<h1>Evolutility Dictionary Database</h1>

<p>"EvoDico" is the database containing the definition of your web applications. 
With it, there is no more XML, the applications descriptions are stored in the database  
and edited directly with the application itself.</p>

<P>We ported the XML schema to the following database structure:<BR><BR><IMG style="MARGIN-LEFT: 40px" alt="" 
src="pix/EvoDico/EvoDicoDB.gif"> </P> 

<P>Technical details:</P>
<UL>
<LI>Creating one DB table per XML element and one DB column per XML 
attribute. We have 4 elements in the meta-model: <CODE>form</CODE>, 
<CODE>data</CODE>, <CODE>panel</CODE>, and <CODE>field</CODE>. 
These become 4 database tables. 
<LI>As the relationship between <CODE>form</CODE> and <CODE>data</CODE> is a 
1-to-1, we can normalize the schema by gathering them together in the same 
table. 
<LI>Let's also add a table for the necessary list of value (here field types). 
<LI>Let's prefix our tables with "EvoDico_" to indicate what the 
table set for "Evolutility Dictionary". 
<LI>Finally (not showing in the schema), let's add a trigger to automatically 
delete fields and panels of a form when it gets deleted. </LI></UL>

<table class="SimpleGrid">
<tr class="RowHeader">
<td width="38%">Database table</td>
<td width="62%">XML element</td>
</tr>
<tr class="RowOdd">
<td><img class="icon" src="pix/dbU.gif" />EvoDico_Form</td>
<td>form</td>
</tr>
<tr>
<td><img class="icon" src="pix/dbU.gif" />EvoDico_Form</td>
<td>data</td>
</tr>
<tr class="RowOdd">
<td><img class="icon" src="pix/dbU.gif" />EvoDico_Panel</td>
<td>panel</td>
</tr>
<tr>
<td><img class="icon" src="pix/dbU.gif" />EvoDico_Field</td>
<td>field</td>
</tr> 
<tr class="RowOdd">
<td><img class="icon" src="pix/dbU.gif" />EvoDico_FieldType</td>
<td>field attribute: type</td>
</tr>
</table>

<p></p>
	<table class="SimpleGrid">
		<tr class="RowHeader">
			<td width="38%">Stored procedures and views</td>
			<td width="62%">Description</td>
		</tr>
		<tr class="RowOdd">
			<td>
				<img class="icon" src="pix/dbP.gif" />EvoDico_Form_Get</td>
			<td>Stored procedure to retrieve application definitions by ID.</td>
		</tr>
		<tr>
			<td>
				<img class="icon" src="pix/dbP.gif" />EvoDico_Form_GetHelp</td>
			<td>
				Stored procedure to retrieve application help. </td>
		</tr>
		<tr class="RowOdd">
			<td>
				<img class="icon" src="pix/dbV.gif" />EvoDico_xField</td>
			<td>
				field
				</td>
		</tr>
		<tr>
			<td>
				<img class="icon" src="pix/dbV.gif" />EvoDico_xFormPanels</td>
			<td>
				panel
				 </td>
		</tr>
		<tr class="RowOdd">
			<td>
				<img class="icon" src="pix/dbV.gif" />EvoDico_vFieldType</td>
			<td>
				panel</td>
		</tr>
		<tr>
			<td>
				<img class="icon" src="pix/dbV.gif" />EvoDico_vPanel</td>
			<td>
				field</td>
		</tr>
		<tr class="RowOdd">
			<td>
				<img class="icon" src="pix/dbV.gif" />EvoDico_xPanel</td>
			<td>
				panel </td>
		</tr> 
		<tr>
			<td>
				<img class="icon" src="pix/dbV.gif" />EvoDocV_Field</td>
			<td>
				field</td>
		</tr> 
	</table>

 
<p>Evolutility Dictionary database also requires <a href="EvoDoc_DB.aspx">Evolutility database pre-requisites.</a></p>

</asp:Content>

