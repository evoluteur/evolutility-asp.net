<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="Doc.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation : Dependent fields" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="65"
Meta_Description="Evolutility documentation: Dependent fields" 
Meta_Keywords="documentation"
%> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1>Dependent fields (AJAX)</h1>
 
<p>This feature allows to make one field dependent of another (for dropdown fields - type "List of Value"). 
That way selecting a value in one list will automatically change the list of possible values 
in another list.</p>

<P>As a simple examples of dependent dropdowns we will use lists of countries  and cities.
Selecting a country limits the possible selection of cities to cities belonging to this country
 (as in this <a href="../demo/dev_dependent_fields.aspx">live example</a>). </P>
 
<h2>How it works</h2>

<p>In our example, selecting a country triggers an AJAX call getting the new list of cities from the server 
and updating the dropdown on the form. The server call is replied by Evolutility.DataServer, 
a new assembly for a httpHandler.</p>
	<p>Note: hard-coded for now, this assembly need to be modified for new lists
	of values will change in the next versions 
to become metadata-driven like the rest of Evolutility.
	</p>
	
<h2>Adding dependencies</h2>
		 
<p>To setup a dependency between 2 fields, you must add to the master field the attribute <span class="xmlAttr">dependency</span>
to specify the slave field (identified by its <span class="xmlAttr">dbcolumn</span> value). </p>
	
<p>In the XML<br /><div class="code">
		
      &lt;<span class="xmlElem">field</span> <span class="xmlAttr">type</span>="text" <span class="xmlAttr">label</span>="Name" ... 
	<br />
      &lt;<span class="xmlElem">field</span> <span class="xmlAttr">type</span>="lov" <span class="xmlAttr">label</span>="Country"
       <span class="Highlight"><span class="xmlAttr">dependency</span>="CityID"</span>
	<br />
			 <span class="xmlAttr">dbcolumn</span>="CountryID" <span class="xmlAttr">dbcolumnread</span>="Country" <span class="xmlAttr">dbtablelov</span>="dep_Country" ... /&gt;<br />
      &lt;<span class="xmlElem">field</span> <span class="xmlAttr">type</span>="lov" <span class="xmlAttr">label</span>="City" 
      <span class="Highlight"><span class="xmlAttr">dbcolumn</span>="CityID"</span> <span class="xmlAttr">dbcolumnread</span>="City" <span class="xmlAttr">dbtablelov</span>="dep_City" ... /&gt;<br />
		
			</div></p>

<h2>Custom dependencies with Javascript</h2>
		 
<p>For total control over the behavior, it is possible to specify the name of the JavaScript method called
when a value is selected in the master list. This is done in the metadata with the attribute <span class="xmlAttr">jsdependency</span>.
 Both <span class="xmlAttr">dependency</span> and <span class="xmlAttr">jsdependency</span> should not be used 
 at the same time the same field.

 	
<p>In the XML<br /><div class="code">
		
       &lt;<span class="xmlElem">field</span> <span class="xmlAttr">type</span>="lov" <span class="xmlAttr">label</span>="Country"
       <span class="Highlight"><span class="xmlAttr">jsdependency</span>="DoSomethingToTheUI"</span> .../&gt;
	<br />
		
			</div></p>



	 
</asp:Content>