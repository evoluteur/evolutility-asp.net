<%@ Page AutoEventWireup="true" CodeFile="EvoDico.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="EvoDoc" Language="C#" MasterPageFile="zmDoc.master" Menus="doc" Meta_Description="Evolutility documentation : App Wizard"
	Meta_Keywords="documentation developer user guide" SubMenuID="1590" Title="Evolutility :: Documentation : Build Wizard" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <h1>Export Wizard</h1>
    
 
 
 <p><b></b> is a step wizard for database exports.
 </p>
				
<p>With ExportWizard you can export data from  table, view, or query in any of the 
				standard formats (Microsoft Excel, XML, CSV, TXT, or HTML) or generate the 
				SQL script with "INSERT" statements. </p>


	<h2>Step 1 - Choose data source</h2>	
	<p>Select table, view or SQL query to export data from.<br><br>
		<img src="pix/ExportWizard/1.gif" class="shadow"  style="margin-left: 40px"></p>

	<h2>Step 2 - Select columns to include</h2>	
	<p>Select columns to include in the export and sorting options.<br>
		<br>
		<img src="pix/ExportWizard/2.gif" class="shadow"  style="margin-left: 40px"></p> 

	<h2>Step 3 - Choose export format and options</h2>	
	<p>This step will perform the last 2 previously identified tasks: selecting the export format and the specific options for that format. 
	It could have been 2 different steps in the wizard but they are simple and colse enough to belong together.
	
	...drop down to select format, Javascript to switch option panel without posting the page.<br>
		<br>
		<img src="pix/ExportWizard/3-csv.gif" class="shadow"  style="margin-left: 40px"><br>
		<br>
		<img src="pix/ExportWizard/3o-csv.gif" class="shadow"  style="margin-left: 40px">
	</p>
	 
<div style="margin-left: 40px"> 
<table border="0">
<tr valign="top">
	<td><h3>CSV, TXT, Excel</h3>
	<img src="pix/ExportWizard/3p-csv.gif" class="shadow" ><br />Options: Header <br />and Separator.
	</td>
	<td><h3>HTML</h3>
	<img src="pix/ExportWizard/3p-html.gif" class="shadow" ><br />Options: Colors.<br />&nbsp;
	</td>
</tr>
<tr valign="top">
	<td><h3>XML</h3>
	<img src="pix/ExportWizard/3p-xml.gif" class="shadow" ><br />Options: Root element, <br />attributes/elements format.
	</td>
	<td><h3>SQL</h3>
	<img src="pix/ExportWizard/3p-sql.gif" class="shadow" ><br />Options: Transaction, identity.
	</td>
</tr>
</table>
</div>
	
	<p><b>Notes</b>: This wizard is extremely similar to the export feature of Evolutility web control but doesn't require external metadata.</p>
	
</asp:Content>

