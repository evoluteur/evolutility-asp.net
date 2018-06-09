<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="Doc.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation : Database pre-requisites" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="80"
Meta_Description="Evolutility documentation" 
Meta_Keywords="application repository dictionary documentation developer user guide Database pre-requisites"
%>  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




<h1>Database pre-requisites for Evolutility</h1> 

<p>Evolutility can adapt to different database structures and can map your database tables.
Its built-in ORM (Object Relational Mapper) features covers basic CRUD. It requires to create the following database objects:
  </p>

<h2>Evolutility database objects</h2>
  
<table class="SimpleGrid">
<tr class="RowHeader"> 
<td width="38%"> Object</td>
<td width="62%"> Description</td>
</tr> 
<tr class="RowOdd">
	<td><img class="icon" src="pix/dbU.gif" />EVOL_User</td> 
	<td>Table storing user information.</td>
</tr> 
<tr>
	<td><img class="icon" src="pix/dbU.gif" />EVOL_Comment</td> 
	<td>Table storing user comments.</td>
</tr>
<tr class="RowOdd">
	<td><img class="icon" src="pix/dbP.gif" />EvoSP_PagedItem</td>
	<td>Stored procedure for lists of records.
	</td>
</tr>
<tr>
	<td><img class="icon" src="pix/dbP.gif" />EvoSP_Login</td>
	<td>Stored procedure for user authentication. Different Stored procedures can be specified for different applications.</td>
</tr>
</table>  
  
	
<p>Installation SQL scripts:<br>
<ul>
<li>Evol-Common.sql - EvoSP_PagedItem.</li>
<li>Evol-MultiUsers.sql - EVOL_User, EVOL_Comment, and EvoSP_Login.</li>
</ul>
  
<h2>Evolutility ORM limitations and work-arounds</h2>

<p>
Some of these limitations can be overcome by creating database views (especially for LOVs).

</p>

<ul>
	<li>Primary keys must be single columns of type integer.</li>
	<li>Tables used for List of values (LOV) must use a primary key called "ID".</li>
	<li>Evolutility doesn't support having 2 fields using the same LOV table. </li>
	<li>Selections and paging must be handled by the stored procedure "EvoSP_PagedItem" (or an equivalent).</li>
</ul> 
	<p>Most of thee can be worked around by creation database views for list of values tables.
<br />Example: CREATE VIEW LOV_Category AS SELECT Category_ID AS ID, Title as Name
	</p> 

<h2>Database pre-requisite for collaboration</h2>

<p>To let users post comments on a database table, the table must have a column "CommentCount" (of type int).
</p>

<p>By default, comments are saved in the database table “Evol_Comment” 
but another table can be specified using the attribute dbtablecomments in the XML. 
Other specifications are possible with dbcommentsformid and dbcommentsuserpage.
</p>

<p>Notes: Additional <a href="EvoDico_DB.aspx">requirements for Evolutility Dictionary</a>.</p>


<h2>Support for MySQL database</h2>

<p>To use Evolutility with MySQL instead of Microsoft SQLServer, 
you must replace some of the files in "Evolutility.zip" with in the ones in "Evolutility_MySQL.zip".
</p>

<p>In the source code, database is specified using preprocessor directives: 
	</p>
	<ul>

<li>for Microsoft SQL Server or SQL Express:
	<P class="code"> 
	#undef DB_MySQL
		<br/> 
	</p></li>


		<li>for MySQL:</li>
	<p class="code">
		#define DB_MySQL
		<br />
	</p>

</ul>
	
</asp:Content>

