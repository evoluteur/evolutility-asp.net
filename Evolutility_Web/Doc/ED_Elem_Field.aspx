<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="Doc.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation : Metamodel element Field" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="130"
Meta_Description="Evolutility documentation: Meta-model covering UI and database mapping for CRUD applications" 
Meta_Keywords="documentation meta model metamodel meta data metadata CRUD code generation"
%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 

<h1>field element</h1>
	

<p><a name="field"></a>

	<p>The elements <span class="xmlElem">field</span> represent fields on the screen, and  database columns at once. It is 
		the most used element and the element with the most attributes. Database 
		columns hidden to the user (like the primary key of the driving table) are not 
		declared.</p>
		
		<div class="indent2">
	<p><span class="xmlAttr">cssclass</span>
		<br/>
		CSS class name for the specific field (in Edit mode). The default value is "Field".
	</p>
	<p>
		<span class="xmlAttr">cssclasslabel</span>
		<br />
		CSS class name for the specific field label (in modes Edit and View). The default value is "FieldLabel".
	</p>
	<p>
		<span class="xmlAttr">cssclassview</span>
		<br />
		CSS class name for the specific field (in View mode). The default value is "Field".
	</p>
	<p><span class="xmlAttr">dbcolumn</span> 
		<br/>
		Database column (SQL name) for the field.&nbsp; 
	</p>
	<p><span class="xmlAttr">dbcolumnimg</span> 
		<br/>
		Database column (SQL name) containing the filename of the image to display. 
	</p>
	<p><span class="xmlAttr">dbcolumnpix</span> 
		<br/>
		Database column (SQL name) containing the image filename. 
	</p>
	<p><span class="xmlAttr">dbcolumnread</span> 
		<br/>
		Database column alias. Only useful for field of type LOV, otherwise <span class="xmlAttr">
			dbcolumnread</span> must be the same as <span class="xmlAttr">dbcolumn</span>
		for the field.</p>
	<p><span class="xmlAttr">dbcolumnreadlov</span>
		<br/>
		Column to show as value in lists. Default value is "name". 
	</p>
	<p><span class="xmlAttr">dborderlov</span> 
		<br/>
		Column (or coma separated list) to sort the LOV by (SQL where clause). 
	</p>
	<p><span class="xmlAttr">dbtablelov</span> 
		<br/>
		Database table with the list of values.&nbsp; 
	</p>
	<p><span class="xmlAttr">dbwherelov</span> 
		<br/>
		Extra where clause to limit the list of value. 
	</p>
	<p><span class="xmlAttr">defaultvalue</span> 
		<br/>
		Default value for the field displayed while creating a new record (only for LOV fields).</p>
	<p><span class="xmlAttr">dependency</span>
		<br />
		Allows to specify a "slave dropdown" (dependent dropdown identified by its "dbcolumn") 
		which will dynamically updates it's list of value when the users selects a different value int the master-field.
	<br/>
		More information about <a href="EvoDoc_Dependent_fields.aspx">dependent fields</a>.</p>	 

	<p><span class="xmlAttr">format</span> 
		<br/>
		Field format (for fields of type boolean, date, decimal, or integer).
		<br/>
		Example: <span class="xmlAttr">format</span>="'$'#,##0.00"
	</p>
	<p><span class="xmlAttr">height</span> 
		<br/>
		Height of the field, in number of rows (default to 1 for all field except 
		fields of type TextMultilines).&nbsp; 
	</p>
	<p><span class="xmlAttr">help</span> 
		<br/>
		Help for the field (in Edit mode).&nbsp; 
	</p>
	<p><span class="xmlAttr">img</span> 
		<br/>
		Image to display (for fields of type "boolean" or "url") in Edit or View modes. 
	</p>
	<p><span class="xmlAttr">imglist</span> 
		<br/>
		Image to display (for fields of type "boolean" or "url") in List mode. 
	</p>
	<p><span class="xmlAttr">jsvalidation</span>
		<br />
		Allows to specify the name of a Javascript method for custom validation.
	<br/>
		More information about <a href="EvoDoc_Validation.aspx">fields validation</a>.</p>	 
	<p><span class="xmlAttr">jsdependency</span>
		<br />
		Allows to specify the name of a Javascript method called when the user select a field value (using the "onclick" event).
	<br/>
		More information about <a href="EvoDoc_Dependent_fields.aspx">dependent fields</a>.</p>	 
	<p><span class="xmlAttr">label</span> 
		<br/>
		Field title. Can be empty but then <span class="xmlAttr">labeledit</span> and<span class="xmlAttr">
			labellist</span> should be 
		provided.</p>
	<p><span class="xmlAttr">labeledit</span> 
		<br/>
		used for edition and search (only used when label is empty). 
	</p>
	<p><span class="xmlAttr">labellist</span> 
		<br/>
		Field title in list headers, useful to display an abbreviated header in lists. 
	</p>
	<p><span class="xmlAttr">link</span> 
		<br/>
		Force the field to be displayed as a link to another dynamic page. It can use 
		the following variables @itemid, @userid, @fieldid. 
	</p>
	<p><span class="xmlAttr">linklabel</span> 
		<br/>
		Display a sentence or an image as the link. @fieldvalue in the string is 
		replaced by the field value at runtime.&nbsp; 
	</p>
	<p><span class="xmlAttr">linktarget</span> 
		<br/>
		Direct link click to a new browser.&nbsp; 
	</p>
	<p><span class="xmlAttr">lookup</span> 
		<br/>
		Automatically set the field value in edit mode to a specified page request 
		parameter.&nbsp; 
	</p>
	<p>
		<span class="xmlAttr">max</span>
		<br />
		Maximum value allowed for the field.
	</p>
	<p><span class="xmlAttr">maxlength</span> 
		<br/>
		Maximum number of characters allowed for the field. 
	</p>
	<p>
		<span class="xmlAttr">min</span>
		<br />
		Minimum value allowed for the field.
	</p>
	<p><span class="xmlAttr">optional</span>
		<br/>
		Determines if the field is displayed when empty (apply to View mode only). 
	</p>
	<p><span class="xmlAttr">readonly</span> 
		<br/>
		<span class="xmlAttr">readonly</span>=1 presents edition of the field. <span class="xmlAttr">
			readonly</span>=2 presents edition of the field, but allows typing in 
		insertion.&nbsp; 
	</p>
	<p><span class="xmlAttr">regexp</span><br/>
		Regular expression used for validating the field value.&nbsp; 
	</p>
	<p><span class="xmlAttr">required</span><br/>
		Determines if the field is required for saving. The Javascript for client-side 
		validation is automatically generated.&nbsp; 
	</p>
	<p><span class="xmlAttr">search</span> 
		<br/>
		Determines if the field appear in the search form. 
	</p>
	<p><span class="xmlAttr">searchadv</span> 
		<br/>
		Determines if the field appear in the advanced search form. 
	</p>
	<p><span class="xmlAttr">searchlist</span> 
		<br/>
		Determines if the field appears as a column of the search results list. 
	</p>
	<p><span class="xmlAttr">splistlov</span> 
		<br/>
		Stored Procedure name (with parameters and @itemid, @fieldid, @userid assigned 
		by the control) for listing LOV values when lovmany. 
	</p>
	<p><span class="xmlAttr">type</span><br/>
		The type of the field as described in more details in <a href="ED_Field_Types.aspx">field types</a>. Possible values are:
    </p>
		<ul>
			<li>boolean (yes/no)</li>
			<li>date</li>
			<li>datetime</li>
			<li>decimal</li>
			<li>document</li>
			<li>email</li>
			<li>formula (SQL formula)</li>
			<li>html (Rich Text Format)</li>
			<li>image</li>
			<li>integer</li>
			<li>lov (list of values)</li>
			<li>text</li>
			<li>textmultiline</li>
			<li>time</li>
			<li>url</li>
		</ul>
			 
	
	<p><span class="xmlAttr">width</span>
		<br/>
		Width of the field in percentage of the Panel it belongs to.</p> 
  
  </div>
  
		
</asp:Content>
