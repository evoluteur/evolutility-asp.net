<%@ Page AutoEventWireup="true" CodeFile="Product.aspx.cs" CodeFileBaseClass="BasePage"
    Inherits="ProductPage" Language="C#" MasterPageFile="zmProduct.master" 
Menus="product"
SubMenuID="1"
    Meta_Description="Evolutility is an open source generic web UI for database applications. 
    With it you can build web applications for ASP .NET and Microsoft SQL Server easily without writing code. Simply describe your application structure and Evolutility will provide the necessary web forms and the corresponding database CRUD (Create, Read, Update, Delete) functionality based on a simple mapping." 
    Meta_Keywords="evolutility open source object relational mapper database driven web application CRUD ORM metadata ASP.net Microsoft.net SQL server no code"
    Title="Evolutility : Product" %>

<%@ Register Src="../Doc/EvoModes.ascx" TagName="EvoModes" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

<h1>Evolutility Overview</h1>
							
	<p>Evolutility is a generic CRUD web applications running on Microsoft ASP.net, and SQL Server or MySQL databases. 
	</p>
	 
	<p>You may think of it as a "<b>dynamic scaffolding</b>" or "<b>metadata-driven MVC</b>" that generates all web pages at run-time,
	 and can be modified by editing metadata (screen definitions and database mapping) instead of code.</p>
	 
	<p>With Evolutility the user interface (e.g. 
	fields titles, positions, visual groups, CSS classes) and its database mapping 
	(e.g. tables, columns, stored procedures) are not defined in the code but in 
	external metadata (stored as XML files or in the database). Evolutility web control can be 
	nested into any ASP.net page. It will generate at run-time all necessary web 
	forms, manage user interaction, and database CRUD (create, read, update, delete) operations automatically.
	</p>
	

					<p><a href="../Download/Download.aspx">Download</a> - <a href="License.aspx">License / Pricing</a><br />&nbsp;</p> 
	 
<h2>Different views of the data</h2>
<p>To perform the full range of CRUD operations on any database table, we need the following forms. Using Evolutility, these forms are generated at run-time:</p>
	<p>  
        <uc1:EvoModes ID="EvoModes1" runat="server" />
	</p>
	 
	<p>With Evolutility, these web forms are not physical pages anymore but simply different modes of the 
		same page. Most of your application screens can be built automatically by mapping 
		different set of database tables to the former set of "virtual pages".
	</p>
			
		
</asp:Content>

