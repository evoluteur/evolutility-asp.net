<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="Doc.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation : Metamodel" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="100"
Meta_Description="Evolutility documentation: Meta-model covering UI and database mapping for CRUD applications" 
Meta_Keywords="documentation meta model metamodel meta data metadata CRUD code generation"
%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 

<h1>Evolutility Meta-model</h1>
		
	<p>Using Evolutility, components (i.e. an address book or a 
		task list), are totally defined by their metadata specifying the user 
		interface, its database mapping, and its behavior.
	</p> 
	
	<p>The metadata (information about the data) must use a specific structure, some kind of declarative language
	 to describe application models.
	This language is called "meta-model", it is the model of the metadata (used to describe applications).</p>
	
	<p>Each component is defined by a single XML document. This XML document (in its 
		simplest form) is composed of one 
		<a href="ED_Elem_Form.aspx"><span class="xmlElem">form</span>&nbsp;element</a> 
		containing one 
		<a href="ED_Elem_Form.aspx#data"><span class="xmlElem">data</span>&nbsp;element</a>, and one or more 
		<a href="ED_Elements_Groups.aspx#panel"><span class="xmlElem">
			panel</span>&nbsp;elements</a> containing one or more 
			<a href="ED_Elem_Field.aspx"><span class="xmlElem">field</span>&nbsp;elements</a>.
	</p>
	
	<p>Let's take a look at the first few lines of XML in the AddressBook sample:
	</p>
	<P><div class="code">
	
	&lt;<span class="xmlElem">form</span> <span class="xmlAttr">xmlns</span>="http://www.evolutility.com"&gt;
		<br/>
		<div style="margin-left:20px;">
		
		&lt;<span class="xmlElem">data</span> <span class="xmlAttr">dbtable</span>="contact"
		<span class="xmlAttr">dborder</span>="lastname,firstname" /&gt;
		<br/>
		&lt;<span class="xmlElem">panel</span> <span class="xmlAttr">label</span>="Name"
		<span class="xmlAttr">width</span>="50"&gt;
		<br/>
		
		<div style="margin-left:20px;">
		&lt;<span class="xmlElem">field</span>
		<span class="xmlAttr">label</span>="Lastname" <span class="xmlAttr">type</span>="text"
		<span class="xmlAttr">maxlength</span>="50" <span class="xmlAttr">dbcolumn</span>="LASTNAME"
		<span class="xmlAttr">dbcolumnread</span>="LASTNAME" <span class="xmlAttr">search</span>="1"
		<span class="xmlAttr">searchlist</span>="1" <span class="xmlAttr">searchadv</span>="1"
		<span class="xmlAttr">required</span>="1" <span class="xmlAttr">width</span>="100"
		<span class="xmlAttr">height</span>="1" /&gt;
		<br/>
		&lt;<span class="xmlElem">field</span>
		<span class="xmlAttr">label</span>="Firstname" <span class="xmlAttr">type</span>="text"
		<span class="xmlAttr">maxlength</span>="50" <span class="xmlAttr">dbcolumn</span>="FIRSTNAME"
		<span class="xmlAttr">dbcolumnread</span>="FIRSTNAME" <span class="xmlAttr">search</span>="1"
		<span class="xmlAttr">searchlist</span>="1" <span class="xmlAttr">searchadv</span>= 
		"1" <span class="xmlAttr">width</span>="100" <span class="xmlAttr">height</span>="1" 
		/&gt;
		<br/>
		. . .&nbsp;<br/>
		</div>
		
		&lt;/<span class="xmlElem">panel</span>&gt;
		<br/>
		
		</div>
		&lt;/<span class="xmlElem">form</span>&gt;
		<br/>
		
	</div></p>
	 
		
		<p>Additional elements are available to better organize information:
			<a href="ED_Elements_Groups.aspx#tab"><span class="xmlAttr">tab</span></a>,  
		<a href="ED_Elements_Groups.aspx#panel-details"><nobr><span class="xmlAttr">
			panel-details</span></nobr></a>, and
			<a href="ED_Elem_Queries.aspx#queries"><span class="xmlAttr">queries</span> and <span class="xmlAttr">
			query</span></a>  for 
		features like master-details, cross-references lists, predefined 
		queries, or more page structure.</p>
		
	<p>For a look at the minimal metadata needed (database mapping and user interface) for CRUD applications code generation using the example of a to do list, see our articles at the Code Project:
<a href="http://www.codeproject.com/KB/codegen/Metamodel_for_CRUD.aspx" target="cp1">Minimalist Meta-Model for CRUD Applications<span class="ExtWeb"></span></a>.
		</p>	

  
  <h2>Storing the metadata</h2>
  
	<p>XML validation can be done with the Evolutility XML schema Evolutility.XSD.</p>
  
	<p>With <a href="EvoDoc.aspx">Evolutility</a> the metadata is stored in XML.
	XML is advantageous for easier deployment, and more flexible as Evolutility currently supports more features than EvoDico.</p>
	
	<p><a href="EvoDico.aspx">Evolutility Dictionary</a> stores the metadata in a 
	<a href="EvoDico_DB.aspx">database dictionary</a> 
	and let you modify your applications in your browser. 
	It also comes with a set of <a href="EvoDico_Wizards.aspx">wizards to build or install applications</a> easily.</p>
		
	
	<h2>Using XML and EvoDico together</h2>	
	
	<p>Using EvoDico <a href="EvoDico_Wiz_XML2DB.aspx">Import XML wizard</a> and publish link, 
	it is possible to go back and forth between XML and EvoDico.</p>
	
	
</asp:Content>
