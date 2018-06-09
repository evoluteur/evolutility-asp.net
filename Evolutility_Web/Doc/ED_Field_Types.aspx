<%@ Page AutoEventWireup="true" CodeFile="Doc.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="EvoDoc" Language="C#" MasterPageFile="zmDoc.master" Menus="doc"
	Meta_Description="Evolutility documentation: Field types" Meta_Keywords="documentation"
	SubMenuID="135" Title="Evolutility :: Documentation : Field Types" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0"><tr valign="top">
<td>				

<h1>Field types</h1>

	<p>With Evolutility field types are not only data types but also "behavioral types" or "UI 
		types". </p>
		
	<p>For example fields for a URL, an email, or a phone number are of 
	different types for the user as they behave differently, while they have the 
	same data type in the database (varchar or nvarchar).</p>
	
	<p>Most fields types map to a single database column in the driving table; 
	"lov" (list of values) maps to a foreign key column and another table (using additional attributes); 
	and "formula" may be a SQL formula like a simple concatenation of different column values,
	 or a full a sub-query.</p>
	 
	 <p>Changing a field type usually requires also changing the field type of the database column associated to the field. It also determines <a href="EvoDoc_Validation.aspx">field validation</a> rules.</p>
	 
	<p>As seen below, each field type looks and behaves differently in different views.<br />&nbsp;</p>	
</td>
<td>
<div class="LightGrey">
<ul class="indent">
<li><a href="#boolean"><img src="../PixEvo/dico/ft-bool.gif" alt="" class="Icon" />boolean</a></li>
<li><nobr><a href="#date"><img src="../PixEvo/dico/ft-date.gif" alt="" class="Icon" />date, datetime, time</a></nobr></li>
<li><a href="#decimal"><img src="../PixEvo/dico/ft-dec.gif" alt="" class="Icon" />decimal, integer</a></li>
<li><a href="#document"><img src="../PixEvo/dico/ft-doc.gif" alt="" class="Icon" />document</a></li> 
<li><a href="#email"><img src="../PixEvo/dico/ft-email.gif" alt="" class="Icon" />email, url</a></li>
<li><a href="#formula"><img src="../PixEvo/dico/ft-fn.gif" alt="" class="Icon" />formula</a></li>
<li><a href="#html"><img alt="" class="Icon" src="../PixEvo/dico/ft-htm.gif" />html (RTF)</a></li>
<li><a href="#image"><img src="../PixEvo/dico/ft-img.gif" alt="" class="Icon" />image</a></li>
<li><nobr><a href="#lov"><img src="../PixEvo/dico/ft-lov.gif" alt="" class="Icon" />lov (list of values)</a></nobr></li>
<li><a href="#text"><img src="../PixEvo/dico/ft-txt.gif" alt="" class="Icon" />text</a></li>
<li><a href="#textmultiline"><img src="../PixEvo/dico/ft-txtml.gif" alt="" class="Icon" />textmultiline</a></li>
</ul> 
</div>
</td>
</tr></table> 

<h2 id="boolean"><img src="../PixEvo/dico/ft-bool.gif" alt="" class="Icon" />boolean</h2>

			<p>Boolean fields are Yes/No values displayed as checkboxes. A Boolean field is 
				stored as a numeric value (Yes=1, No=0 or null). The most efficient database column type 
				for it is bit.</p>
<table class="DocPad12"> 
	  <tr valign="top"> 
		  <td>edit<br /><img alt="" src="pix/ft/bool-edit.gif" />
		  </td>
		  <td>view<br />
			  <img alt="" src="pix/ft/bool-view.gif" />
		  </td>
	  </tr> 
	<tr valign="top">
		  <td>search & adv. search<br />
			  <img alt="" src="pix/ft/bool-search.gif" /> 
		  </td>
		  <td>list<br />
			  <img alt="" src="pix/ft/bool-list.gif" />
		  </td>
	  </tr> 
	</table><br/> 
	
<h2 id="date"><img src="../PixEvo/dico/ft-date.gif" alt="" class="Icon" />date, datetime, time</h2>
			<p>Dates are displayed as an input box with a date picker in edit mode, and as a formatted string in other modes.
			 The Javascript for the date picker is an external JS 
				file which can be customized. Possible database column types are datetime or 
				smalldatetime.</p>
				
<table class="DocPad12">  
	  <tr valign="top"> 
		  <td>edit<br> 
		  <img alt="" src="pix/ft/date-edit2.gif" />
		  </td>
		  <td>view<br>
			  <img alt="" src="pix/ft/date-view.gif" />
		  </td>
	  </tr> 
	<tr valign="top">
		  <td>search<br>
			  <img alt="" src="pix/ft/date-search.gif" />
		  </td>
		  <td>list<br>
			  <img alt="" src="pix/ft/date-list.gif" />
		  </td>
	</tr>
	<tr valign="top">
		  <td colspan="2">adv. search<br>
			  <img alt="" src="pix/ft/date-searchadv.gif" />
		  </td>
	  </tr> 
	</table>
	<br>
	
<h2 id="decimal"><img src="../PixEvo/dico/ft-dec.gif" alt="" class="Icon" />decimal, integer</h2>
			<p>These types are used for numeric values. Decimal can be stored as data type 
				money or decimal. Integer can be smallint, int, bigint…</p>
				
<table class="DocPad12">  
	  <tr valign="top">
		  <td>edit<br><img alt="" src="pix/ft/decimal-edit.gif" />
		  </td>
		  <td>view<br>
			  <img alt="" src="pix/ft/decimal-view.gif" />
		  </td>
	  </tr> 
	<tr valign="top">
		  <td>search<br> 
			  <img alt="" src="pix/ft/decimal-search.gif" />
		  </td>
		  <td>list<br>
			  <img alt="" src="pix/ft/decimal-list.gif" />
		  </td>
	</tr>
	<tr valign="top">
		  
		  <td colspan="2">adv. search<br>
			  <img alt="" src="pix/ft/decimal-searchadv.gif" />
		  </td>
	  </tr> 
	</table>
	
<h2 id="document"><img src="../PixEvo/dico/ft-doc.gif" alt="" class="Icon" />document</h2>
			<p>Documents are displayed as a link for download in view mode, as a text box with 
				a browse button for upload in edit mode, as a checkbox in the search and 
				advanced search modes. Like images, documents are stored on the file server and 
				only the filename is stored in the database.</p>
				
<table class="DocPad12"> 
	  <tr valign="top"> 
		  <td>edit<br />
		  <img alt="" src="pix/ft/document-edit.gif" />
		  </td>
		  <td>view<br />
			  <img alt="" src="pix/ft/document-view.gif" />
		  </td>
	  </tr>
	  <tr>
  <td></td>
  </tr>
	<tr valign="top">
		  <td>search & adv. search 
			  <br />
			  <img alt="" src="pix/ft/document-search.gif" />
		  </td> 
		  <td>list<br />
			  <img alt="" src="pix/ft/document-list.gif" />
		  </td>
	  </tr> 
	</table>
	
	
<h2 id="email"><img src="../PixEvo/dico/ft-email.gif" alt="" class="Icon" />email, url</h2>
			<p>Text value displayed as a text box in edit mode and hyperlink in other modes. 
				These can be stored as varchar, or nvarchar.</p>
<table class="DocPad12"> 
	  <tr valign="top"> 
		  <td>edit<br />
		  <img alt="" src="pix/ft/email-edit.gif" />
		  </td>
		  <td>view<br />
			  <img alt="" src="pix/ft/email-view.gif" />
		  </td>
	  </tr> 
	<tr>
		  <td>search 
			  <br />
			  <img alt="" src="pix/ft/email-search.gif" />
		  </td> 
		  <td>list<br />
			  <img alt="" src="pix/ft/email-list.gif" />
		  </td>
	  </tr>
	<tr> 
		<td colspan="2">adv. search<br />
			<img alt="" src="pix/ft/email-searchadv.gif" />
		</td>
	</tr>
</table>
	
<h2 id="formula"><img src="../PixEvo/dico/ft-fn.gif" alt="" class="Icon" />formula</h2>
			<p>SQL formula or sub-query. The calculation SQL is entered in the dbcolumn 
				attribute of the field. Fields of type formula cannot be edited by users.</p>
			<p>Example of formula field:
			</p>
			<P class="code">&lt;<span class="xmlElem">field</span> <span class="xmlAttr">type</span>="formula"
				<span class="xmlAttr">readonly</span>="1" <span class="xmlAttr">label</span>="Photos"
				<span class="xmlAttr">format</span>="0 'photos'" <span class="xmlAttr">dbcolumnread</span>="NBphotos"
				<span class="xmlAttr">dbcolumn</span>="SELECT COUNT(*) FROM EVOL_Photo P WHERE 
				P.albumid=T.id" ...&nbsp; &gt;
				<br/>
			</p>
<table class="DocPad12"> 
	  <tr valign="top"> 
		  <td>edit<br />
			  <img alt="" src="pix/ft/formula-view.gif" />
		  </td>
		  <td>view<br />
			  <img alt="" src="pix/ft/formula-view.gif" />
		  </td>
	  </tr>
	  <tr valign="top">
  <td></td>
  </tr>
	<tr valign="top">
		  <td>search & adv. search<br /> 
			  <img alt="" src="pix/ft/formula-search.gif" />
		  </td> 
		  <td>list<br />
			  <img alt="" src="pix/ft/formula-list.gif" />
		  </td>
	  </tr> 
	</table>
	
<h2 id="image"><img src="../PixEvo/dico/ft-img.gif" alt="" class="Icon" />image</h2>
			<p>Images are displayed as such in view mode, as a box with a browse button for 
				upload in edit mode, as a checkbox in the search and advanced search modes. 
				Images are stored on the file server, only the filename is stored in the 
				database, as a varchar or nvarchar.</p>
<table class="DocPad12"> 
	  <tr valign="top"> 
		  <td>edit<br />
			  <img alt="" src="pix/ft/image-edit.gif" />
		  <br />
			  <img alt="" src="pix/ft/image-edit2.gif" />
		  </td>
		  <td>view<br />
			  <img alt="" src="pix/ft/image-view.gif" />
		  </td>
	  </tr> 
	<tr valign="top">
		  <td>search & adv. search<br />
			  <img alt="" src="pix/ft/image-search.gif" />
		  </td>
		  <td>list<br />
			  <img alt="" src="pix/ft/image-list.gif" />
		  </td> 
	  </tr> 
	</table>
	

	
<h2 id="html"><img src="../PixEvo/dico/ft-htm.gif" alt="" class="Icon" />html (rich text format)</h2>
			<p>The "html" field type is used to display Rich Text Format (RTF) or HTML. It uses CKeditor widget 
			for WYSIWYG edition in the browser. </p>
				
<table class="DocPad12"> 
	<tr valign="top"> 
		<td>edit<br />
			<img alt="" src="pix/ft/html-edit.gif" />
		</td>
		<td>view<br />
		<img alt="" src="pix/ft/html-view.gif" />
		</td>
	</tr> 
		<tr valign="top">
		<td> search<br />
		<img alt="" src="pix/ft/textml-search.gif" />
		</td>
		<td>list<br />
		<img alt="" src="pix/ft/html-list.gif" />
		</td>
		</tr>
	<tr>
		<td colspan="2">adv. search<br />
		<img alt="" src="pix/ft/textml-searchadv.gif" />
		</td>
	</tr> 
	</table>

	
	

<h2 id="lov"><img src="../PixEvo/dico/ft-lov.gif" alt="" class="Icon" />lov (list of values)</h2>
			<p>Lists of values are choices of values displayed as drop-down lists in edit mode 
				or as the string of the selected value in view mode. They correspond to joins 
				to secondary tables in the database and are stored in the driving table as a 
				number which is the primary key of the value in the secondary table.</p>
			<p>Using certain attributes of the field it can become a many-to-many relationship 
				instead of a one-to-many.</p>
<table class="DocPad12"> 
	  <tr valign="top"> 
		  <td>edit<br />
		  <img alt="" src="pix/ft/lov-edit.gif" />
		  </td>
		  <td>view<br />
			  <img alt="" src="pix/ft/lov-view.gif" />
		  </td>
	  </tr> 
	<tr valign="top">
		  <td>search 
			  <br />
			  <img alt="" src="pix/ft/lov-search.gif" />
		  </td>
		  <td>list<br />
			  <img alt="" src="pix/ft/lov-list.gif" />
		  </td>
	</tr>
	<tr valign="top">
		  <td colspan="2">adv. search<br />
			  <img alt="" src="pix/ft/lov-searchadv.gif" />
		  </td>
	  </tr> 
	</table>
	
	
<h2 id="text"><img src="../PixEvo/dico/ft-txt.gif" alt="" class="Icon" />text</h2>
			<p>This type is the most commonly used one. It is displayed as a text box in edit 
				mode. It is a string stored as varchar or nvarchar.</p>

<table class="DocPad12"> 
	  <tr valign="top"> 
		  <td>edit<br />
		  <img alt="" src="pix/ft/text-edit.gif" />
		  </td>
		  <td>view<br />
			  <img alt="" src="pix/ft/text-view.gif" />
		  </td>
	  </tr> 
	<tr valign="top">
		  <td>search 
			  <img alt="" src="pix/ft/text-search.gif" />
		  </td>
		  <td>list<br />
			  <img alt="" src="pix/ft/text-list.gif" />
		  </td>
	</tr>
	<tr valign="top">
		  <td colspan="2">adv. search<br />
			  <img alt="" src="pix/ft/text-searchadv.gif" />
		  </td>
	  </tr> 
	</table>
	
<h2 id="textmultiline"><img src="../PixEvo/dico/ft-txtml.gif" alt="" class="Icon" />textmultiline</h2>
			<p>Fields of these types are displayed as big text boxes (HTML "textarea") and can 
				spread over several rows. They can be stored as text, varchar, or nvarchar.</p>
<table class="DocPad12"> 
	  <tr valign="top"> 
		  <td>edit<br /><img alt="" src="pix/ft/textml-edit.gif" />
		  </td>
		  <td>view<br />
			  <img alt="" src="pix/ft/textml-view.gif" />
		  </td>
	  </tr> 
	<tr valign="top">
		  <td> search<br />
			  <img alt="" src="pix/ft/textml-search.gif" />
		  </td>
		  <td>list<br />
			  <img alt="" src="pix/ft/textml-list.gif" />
		  </td>
	</tr>
	<tr valign="top">
		  <td colspan="2">adv. search<br />
			  <img alt="" src="pix/ft/textml-searchadv.gif" />
		  </td>
	  </tr> 
	</table>
		
</asp:Content>