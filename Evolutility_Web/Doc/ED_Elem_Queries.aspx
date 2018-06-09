<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="Doc.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation : Metamodel" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="106"
Meta_Description="Evolutility documentation: Meta-model covering UI and database mapping for CRUD applications" 
Meta_Keywords="documentation meta model metamodel meta data metadata CRUD code generation"
%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 

<h1>Meta-model optional elements</h1>

	 
	<p>Beside the required elements 
		<span class="xmlAttr">form</span>, 
		<span class="xmlAttr">data</span>, 
		<span class="xmlAttr">panel</span>, 
		and <span class="xmlAttr">field</span>, the 
	 additional optional elements 
		<a href="ED_Elements_Groups.aspx#panel-details"><nobr><span class="xmlAttr">
			panel-details</span></nobr></a>, 
			<a href="ED_Elem_Queries.aspx#queries"><span class="xmlAttr">queries</span> and <span class="xmlAttr">
			query</span></a>, 
			and 
			<a href="ED_Elements_Groups.aspx#tab"><span class="xmlAttr">tab</span></a>  
			can be used to add 
		functionalities like master-details, cross-references lists, predefined 
		queries, or more page structure. </p>
		
		<p>These elements are supported by Evolutility using XML but not yet by <a href="EvoDico.aspx">Evolutility Dictionary</a>.</p>
     
     
	<p>Notes: A <span class="xmlElem">field</span> element nested 
		in a <span class="xmlElem">panel-details</span> only requires a subset of the 
		attributes of a field nested in a <span class="xmlElem">panel</span>.</p>

<p><a name="queries"></a>
<h2>queries and query elements</h2>

    <p>It is possible to add canned queries as a "Selections" button on the toolbar.</p>
    
<P><div class="code">
&lt;<span class="xmlElem">queries</span> <span class="xmlAttr">label</span>="Selections 
		of restaurants"&gt;<br/>
		
	<div style="margin-left: 20px;">
	
		&lt;<span class="xmlElem">query</span> 
	<span class="xmlAttr">label</span>="Restaurant in San Francisco" 
		<span class="xmlAttr">dbwhere</span>="t.City = 'san francisco'"
		<span class="xmlAttr">url</span>="sfo" /&gt;<br/>
		&lt;<span class="xmlElem">query</span>
	<span class="xmlAttr">label</span>="Asian food"
		<span class="xmlAttr">dbwhere</span>=" 
		t.categoryID IN (3,4,5,6,7,11,12)"
		<span class="xmlAttr">url</span>="asian" 
		/&gt;<br/>
		...<br/>
		
	</div>		
	
		&lt;/<span class="xmlElem">queries</span>&gt;
</div></p>

<p>A <span class="xmlElem">queries</span> contains several 
		query. The <span class="xmlElem">queries</span> element uses the following two 
		optional attributes:</p>
		
		<div class="indent2">
	<P ><span class="xmlAttr">footer</span><br/>
		Footer text displayed below the list of queries.</p>
<p>
    <span style="color: #990000">label<br/>
    </span>
		    Introduction text displayed above the list of queries.</p>
	    <p>Attributes of <span class="xmlElem">query</span> are as 
		    follow:</p>
	    <P ><span class="xmlAttr">dbwhere</span><br/>
		    SQL Where clause for the query.</p>
<p>
    <span style="color: #990000">label<br/>
    </span>
		Query title.</p>
	<P ><span class="xmlAttr">url</span><br/>
		Key used to identity each query within the list. Each key can be used in links 
		within the page to force the control to display the corresponding query. 
		Example:</p>
		<P class="code">&lt;a 
				href="javascript:__doPostBack('evo1','q:sfo')"&gt;Restaurants 
				in San Francisco&lt;/a&gt; </p>
</div>

	<p>Note: In addition to specifying <span class="xmlElem">queries</span>
		and<span class="xmlElem"> query </span>in the metadata, the Web control 
		property <B>DBAllowSelections</B> must be 
		set to True.</p>
	
	<p></p>
</asp:Content>
