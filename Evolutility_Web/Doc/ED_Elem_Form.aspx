<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="Doc.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation : Metamodel elements" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="110"
Meta_Description="Evolutility documentation: Meta-model covering UI and database mapping for CRUD applications" 
Meta_Keywords="documentation meta model metamodel meta data metadata CRUD code generation"
%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 

<h1>Meta-model elements</h1>
	 
	 
	<p>To build applications with Evolutility the minimal set of required elements in the metadata are:	
 one <a href="#form">form</a>, one <a href="#data">data</a>, several <a href="ED_Elements_Groups.aspx#panel">panel</a>, and many <a href="ED_Elem_Field.aspx">field</a>.	
  This is enough to describes a simple CRUD (Create, Read, Update, Delete) application including all the necessary web forms for searching, viewing, 
		editing, deleting and exporting the data in a DRY (Don't Repeat Yourself) way. </p>
 
<p><a name="form"></a>	
<h2>form element</h2>

	<p><span class="xmlElem">form</span> is the root element. It doesn’t represent one 
		single web form, but all necessary web forms (Edit, View, List, Search, 
		Advanced Search, Export) at once. Fields are displayed or not on each web form based on
		the attributes <span class="xmlAttr">search</span>, <span class="xmlAttr">
			searchlist</span>, and <span class="xmlAttr">searchadv</span>.</p>
	
	<p>The <span class="xmlElem">form</span> element contains one element <span class="xmlElem">
			data</span> and one or more elements <span class="xmlElem">panel</span>.</p>
	
	<p>It has the optional attributes <span class="xmlAttr">label</span>, <span class="xmlAttr">
			description</span> and <span class="xmlAttr">version</span>, and the required namespace 
		<span class="xmlAttr">xmlns</span>.
		<br />
		Example: &lt;form xmlns="http://www.evolutility.com" label="To do list" &gt;</p>

<p><a name="data"></a>
<h2>data element</h2>

	<p>The <span class="xmlElem">data</span> element specifies the set of 
		database objects used by the component: driving table, stored procedures; and the 
		icon and screen name associated.</p>
		
		<div class="indent2">
	<p>
		<span class="xmlAttr">dbcolumnicon</span><br />
		Name of the database column containing the item icon file name.&nbsp;
		<br />
		Example: <span class="xmlAttr">dbcolumnicon</span>="icon"</p>
	<p><span class="xmlAttr">dbcolumnlead</span><br/>
		Database column used as record identifiyer for the user (not the primary key).&nbsp; 
		<br/>
		Example: <span class="xmlAttr">dbcolumnlead</span>="LASTNAME"</p>
	<p><span class="xmlAttr">dbcolumnpk</span><br />
		Name of the primary key column used as record identifiyer.&nbsp;
		<br />
		Example: <span class="xmlAttr">dbcolumnpk</span>="ID"</p>
	<p><span class="xmlAttr">dbcolumnuserid
			<br/>
		</span>In multiple-users environment, specify the column name of the “who is 
		column” designing the specific user author of the record. Default is UserID. </p>
	<p><span class="xmlAttr">dbcommentsformid<br/>
		</span>&lt;for future enhancements&gt;
	</p>
	<p><span class="xmlAttr">userpage<br/>
		</span>Page name for the user profile information.</p>
	<p><span class="xmlAttr">dborder</span><br/>
		List of column names to include in the "order by" SQL clause. It is the default 
		sort option.&nbsp; 
	</p>
	<p><span class="xmlAttr">dbtable</span><br/>
		Name of driving table for the component.&nbsp; 
	</p>
	<p><span class="xmlAttr">dbtablecomments</span> 
		<br/>
		Name of the table to store comments for this component. This table can be 
		shared between components.&nbsp; 
	</p>
	<p><span class="xmlAttr">dbtableusers</span> 
		<br/>
		Name of the user table which should have the following columns "ID", "login", 
		and "password". This property is only used if <span class="xmlAttr">splogin</span>
		is not set.&nbsp; 
	</p>
	<p><span class="xmlAttr">dbwhere</span> 
		<br/>
		SQL where clause to limit the dataset manipulated, must use the alias "T" for the driving table.&nbsp; 
		<br/>
		Example: <span class="xmlAttr">dbwhere</span>="T.publish=1"</p>
	<p><span class="xmlAttr">entities</span> 
		<br/>
		Plural for <span class="xmlAttr">entity</span>. Default value = "items".&nbsp; 
	</p>
	<p><span class="xmlAttr">entity</span> 
		<br/>
		User's object name for the database object (for example: "contact" for the 
		AddressBook application). Default value = "item". 
	</p>
	<p><span class="xmlAttr">icon</span><br/>
		Filename of the component icon (same one for all records).
		<br/>
		Example: <span class="xmlAttr">icon</span> ="memo.gif"
	</p>
	<p><span class="xmlAttr">spdelete</span> 
		<br/>
		Name and parameters of the stored procedure for deleting records (or flagging them as deleted). 
	</p>
	<p><span class="xmlAttr">spget</span> 
		<br/>
		Name and parameters of the stored procedure for loading a specific record.
        <br/>
        Example: <span class="xmlAttr">spget</span>="EvoSP_Get @itemid" or <span class="xmlAttr">
            spget</span>="EvoSP_Get @itemid, @userid"
    </p>
	<p><span class="xmlAttr">splogin</span> 
		 &nbsp; <br/>
		Name and parameters of the stored procedure for checking users login and 
		password.&nbsp; 
		<br/>
		Example: <span class="xmlAttr">splogin</span>="EvoSP_Login @login, @password"</p>
	<p><span class="xmlAttr">sppaging</span>&nbsp; 
		<br/>
		Name and parameters of the stored procedure for paging search results.&nbsp; 
		<br/>
		Example: <span class="xmlAttr">sppaging</span>="EvoSP_PagedItem @SQLselect, 
		@SQLtable, @SQLfrom, @SQLwhere, @SQLorderby, @pageid, @pagesize"</p>
	</div>
	
  
	<p>In addition to the former required elements, Evolutility provides the <a href="ED_Elem_Queries.aspx">optional elements</a>  
		 <nobr><span class="xmlAttr">
			panel-details</span></nobr>, 
			<span class="xmlAttr">queries</span> and <span class="xmlAttr">
			query</span>, 
			and 
			<span class="xmlAttr">tab</span>	to add 
		functionalities like master-details, cross-references lists, predefined 
		queries, or more page structure.</p>
     
		
</asp:Content>
