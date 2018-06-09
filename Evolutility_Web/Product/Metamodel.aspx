<%@ Page AutoEventWireup="true" CodeFile="Product.aspx.cs" CodeFileBaseClass="BasePage"
    Inherits="ProductPage" Language="C#" MasterPageFile="zmProduct.master" 
Menus="product"
SubMenuID="5"
    Meta_Description="Evolutility Meta-model" 
    Meta_Keywords="evolutility metadata open source object Meta-model CRUD metadata xml database"
    Title="Evolutility : Meta-model" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
 
<h1>Evolutility Meta-model</h1>
		
		
	<p>Evolutility is a generic Web UI for CRUD. It generates all web forms at run-time based on external metadata.
	This metadata is structured in a unique way as defined in the <b>Meta-model</b>. 
	
	<p>The meta-model can be understood as the structure of 
	the application definitions. It can be used as a declarative language
	 to describe application models. For any components (i.e. an address book or a 
		task list), the user interface, its database mapping, and its behavior are totally defined outside of the code. </p>
	 
	<p>Evolutility meta-model is powerful yet simple. 
	It allows for the description of CRUD web application structuring metadata in very compact ways 
	using only 7 elements and 30 attributes (less than 40 words)...
		</p>	
		
			
	<h2>An example: a do to list</h2>

	<p>Let's look at the code (or rather the absence of code) behind our <a href="../demo/demo_todo.aspx">to do demo application</a>.</p>


	<p>The object (database entity and UI) is defined by a single XML document. 
	This XML document is composed of one <span class="xmlElem">form</span>&nbsp;element 
		containing one <span class="xmlElem">data</span>&nbsp;element specifying the driving table name, 
		some <span class="xmlElem">panel</span>&nbsp;elements determining the visual grouping (in edit and view modes), 
		and as many <span class="xmlElem">field</span>&nbsp;elements.
	</p>
	
	<p>Let's take a look at the XML for the To Do application:
	</p> 
	<div class="code">
	
&lt;<span class="xmlElem">form</span> <span class="xmlAttr">label</span>="To Do" <span
	class="xmlAttr">xmlns</span>="http://www.evolutility.com"&gt;<br/><br/>
	
<div style="margin-left:20px;">
&lt;<span class="xmlElem">data</span> <span class="xmlAttr">entity</span>="task" <span
	class="xmlAttr">entities</span>="tasks" <span class="xmlAttr">icon</span>="todo.gif"  
	<span class="xmlAttr">dbtable</span>="EVOL_ToDo" <span class="xmlAttr">dborder</span>="PriorityID, duedate"   
 <span class="xmlAttr">dbcolumnlead</span>="title"  /&gt;<br/><br/>
	
	&lt;<span class="xmlElem">panel</span> <span class="xmlAttr">label</span>="Task" 
	<span class="xmlAttr">width</span>="62" &gt;<br/><br/>
<div style="margin-left:20px;">	

		&lt;<span class="xmlElem">field</span> <span class="xmlAttr">type</span>="text" <span class="xmlAttr">label</span>="Title" 
	<span class="xmlAttr">cssclass</span>="fieldmain" <span class="xmlAttr">width</span>="100" 
	<span class="xmlAttr">dbcolumn</span>="title" <span class="xmlAttr">dbcolumnread</span>="title" <span class="xmlAttr">required</span>="1" <span class="xmlAttr">maxlength</span>="255"
			   <span class="xmlAttr">search</span>="1" <span class="xmlAttr">searchlist</span>="1" <span class="xmlAttr">searchadv</span>="1" /&gt;<br/><br/>
		
		&lt;<span class="xmlElem">field</span> <span class="xmlAttr">type</span>="date" <span class="xmlAttr">label</span>="Due Date" <span class="xmlAttr">maxlength</span>="10" <span class="xmlAttr">width</span>="40"
			   <span class="xmlAttr">dbcolumn</span>="duedate" <span class="xmlAttr">dbcolumnread</span>="duedate" 
			   <span class="xmlAttr">search</span>="1" <span class="xmlAttr">searchlist</span>="1" <span class="xmlAttr">searchadv</span>="1"  /&gt;<br/><br/>
		
		&lt;<span class="xmlElem">field</span> <span class="xmlAttr">type</span>="lov" <span class="xmlAttr">required</span>="1" <span class="xmlAttr">label</span>="Priority" <span class="xmlAttr">width</span>="60" 
			   <span class="xmlAttr">dbcolumn</span>="PriorityID" <span class="xmlAttr">dbcolumnread</span>="Priority" <span class="xmlAttr">dbtablelov</span>="EVOL_ToDoPriority" <span class="xmlAttr">dborderlov</span>="id" 
			   <span class="xmlAttr">search</span>="1" <span class="xmlAttr">searchlist</span>="1" <span class="xmlAttr">searchadv</span>="1"/&gt;<br/><br/>
			   
</div>	&lt;/<span class="xmlElem">panel</span>&gt;<br/><br/>
	
	&lt;<span class="xmlElem">panel</span> <span class="xmlAttr">label</span>="Status" <span class="xmlAttr">width</span>="38"&gt;<br/><br/>
<div style="margin-left:20px;">	
		   
		&lt;<span class="xmlElem">field</span> <span class="xmlAttr">type</span>="lov" <span class="xmlAttr">label</span>="Category" <span class="xmlAttr">width</span>="100" 
			   <span class="xmlAttr">dbcolumn</span>="CategoryID" <span class="xmlAttr">dbcolumnread</span>="Category" <span class="xmlAttr">dbtablelov</span>="EVOL_ToDoCategory" <span class="xmlAttr">dborderlov</span>="name" 
			   <span class="xmlAttr">search</span>="1" <span class="xmlAttr">searchlist</span>="1" <span class="xmlAttr">searchadv</span>="1"  /&gt;<br/><br/>
		
		&lt;<span class="xmlElem">field</span> <span class="xmlAttr">type</span>="boolean" <span class="xmlAttr">label</span>="Complete" <span class="xmlAttr">labellist</span>="C." <span class="xmlAttr">img</span>="checkb.gif" <span class="xmlAttr">width</span>="50"
			   <span class="xmlAttr">dbcolumn</span>="Complete" <span class="xmlAttr">dbcolumnread</span>="Complete"  
			   <span class="xmlAttr">search</span>="1" <span class="xmlAttr">searchlist</span>="1" <span class="xmlAttr">searchadv</span>="1"/&gt;<br/><br/>
			   
</div>	&lt;/<span class="xmlElem">panel</span>&gt;<br/><br/>
	
	&lt;<span class="xmlElem">panel</span> <span class="xmlAttr">label</span>="Notes" <span class="xmlAttr">width</span>="100"&gt;<br/><br/>
<div style="margin-left:20px;">
		   
		&lt;<span class="xmlElem">field</span> <span class="xmlAttr">type</span>="textmultiline" <span class="xmlAttr">label</span>="" <span class="xmlAttr">labeledit</span>="Notes" <span class="xmlAttr">labellist</span>="Notes" <span class="xmlAttr">width</span>="100" <span class="xmlAttr">height</span>="6" 
			   <span class="xmlAttr">dbcolumn</span>="notes" <span class="xmlAttr">dbcolumnread</span>="notes" <span class="xmlAttr">maxlength</span>="1000"			   
			   <span class="xmlAttr">search</span>="0" <span class="xmlAttr">searchlist</span>="0" <span class="xmlAttr">searchadv</span>="1" /&gt;<br/><br/>
			   
</div>		&lt;/<span class="xmlElem">panel</span>&gt;<br/><br/>
</div>	
&lt;/<span class="xmlElem">form</span>&gt;


 	</div><br/> 
	
	
		<p>This is it! As you can see it is much smaller than the HTML for each form needed for CRUD.</p>
	 
	<p>In addition, it is extremely easy to add fields or modify your applications later.</p>
			 	
		<p>Read our award winning article on 
<a href="http://www.codeproject.com/KB/codegen/Metamodel_for_CRUD.aspx" target="cp1">Minimalist Meta-Model for CRUD Applications<span class="ExtWeb"></span><img src="../Doc/pix/cp_prize.gif" border=0 /></a>
 at CodeProject.
</p>
  
  <h2>Storing and managing metadata</h2>
    <p>Using Evolutility, metadata can be stored in XML, or directly in the database.</p>
    
    <table width="100%">
    <tr valign=top>
		<td width="50%"><h3>XML</h3>
		 XML is advantageous for easier deployment, 
	and more flexible as Evolutility currently supports more features than EvoDico.
		<br /><br /><img src="pix/addressbook-xml.gif" class="shadow"/>	</td>
  
			<td width="50%"><h3>Database dictionary</h3>
		 Storing the metadata directly in the database ("EvoDico" set of tables) instead of XML, 
		 you can modify your applications in your browser. 
	 <br /><br /><img src="pix/addressbook-dico.gif" class="shadow" />	
	 
	 	<p>It also comes with a set of wizards to build or install new applications easily.
        </p>
	 	</td>
		</tr>
    </table> 
	
	<p></p>
		 
</asp:Content>

