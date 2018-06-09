<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="Doc.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation : Overview" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="10"
Meta_Description="Evolutility documentation" 
Meta_Keywords="documentation developer user guide"
%>

<%@ Register Src="EvoModes.ascx" TagName="EvoModes" TagPrefix="uc1" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1>Overview &amp; Concepts</h1> 

	<p>Evolutility is a generic web user interface for CRUD (Create, Read, Update, Delete) applications running on
	ASP.net, and SQL Server or MySQL. 

	It is entirely metadata driven, and can adapt to different 
	database structures. It may behave like an address book, a task 
	list, a photo album, or anything you may want to build. It is 
	ideal to quickly build functional components for database web applications or 
	web site administration pages.
	</p>

	<p>With Evolutility the user interface (e.g. 
	fields titles, positions, visual groups, CSS classes) and its database mapping 
	(e.g. tables, columns, stored procedures) are not defined in the code but in 
	external metadata (stored as XML files or in the database). Evolutility web control can be 
	nested into any ASP.net page. It will generate at run-time all necessary web 
	forms, manage user interaction, and database CRUD (create, read, update, delete) operations automatically.
	</p>

	<p>
        <uc1:EvoModes ID="EvoModes1" runat="server" />
	</p>
	 
	<p>With Evolutility, these web forms are not physical pages anymore but simply different modes of the 
		same page. Most of your application screens can be built by mapping 
		different set of database tables to the former set of "virtual pages".
	</p>
	
</asp:Content>

