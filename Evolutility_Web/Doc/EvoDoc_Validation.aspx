<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="Doc.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation : Form validation" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="60"
Meta_Description="Evolutility documentation: Form validation" 
Meta_Keywords="documentation"
%> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1>Form validation</h1>

<p>Evolutility automatically generates Javascript for field validations on the client. 
This Javascript executes when the user clicks the "Save" button. 
 </p> 

<h2>Generic validation</h2> 

	<p>Validation is determined by the field properties (XML attributes):</p>

	<ul>
		<li><span class="xmlAttr">Type</span> - Text, Integer, Decimal, Date...</li>
		<li><span class="xmlAttr">Required</span> - Must have a value.</li>
		<li><span class="xmlAttr">Min</span> - Minimum value allowed.</li>
		<li><span class="xmlAttr">Max</span> - Maximum value allowed.</li>
		<li><span class="xmlAttr">MaxLength</span> - Maximum length.</li>
		<li><span class="xmlAttr">RegExp</span> - Regular expression to verify.</li>
	</ul>

<p>If the form doesn't pass validation, a messages is generated in the locale of choice 
(without additional text to enter) and shown to the user in a lightbox.</p>	
<p><img style="margin-left:30px;" alt="" src="pix/ui/msg-en.gif" class="shadow" /><br />
	<br />
	<img style="margin-left:30px;" alt="" src="pix/ui/msg-jp.gif" class="shadow" /></p>

<p>In addition, after the user clicks OK and the lightbox disappears, fields not passing the validation are flagged in red on the form.</p>
<p><img style="margin-left:30px;" alt="" src="pix/ui/val-red.gif" class="shadow" /></p> 
	 
<h2>Custom validation</h2>

	<p>It is possible to add custom validation to Evolutility forms by using Javascript.
	</p>
	
	<p>This is a 2 step process:
	<ul>
		<li>Add the hook to the field by using the <span class="xmlAttr">jsvalidation</span> attribute to specify the Javascript method name (only the name, no parameters).</li>
		<li>Add the Javascript to the ASP page or to Evolutility.JS library if you plan to re-use it on multiple pages.</li>
	</ul>
	</p>
	
	<p>The Javascript function will be passed 2 arguments: the field ID and the field label. 
	It needs to return a string with the error message if the validation is not correct, and return null otherwise.</p>
	
<p>For example to only allow company names starting with "E":</p>
	
<p>In the XML<br /><div class="code">			
&lt;<span class="xmlElem">field</span>
		<span class="xmlAttr">label</span>="Company" ...
		<span class="xmlAttr">jsvalidation</span>="<b>StartWithE</b>" /&gt;	</div></p>
	
<p>In Javascript<br /><div class="code">	
function <b>StartWithE</b>(fID,fLabel){<br />
&nbsp;&nbsp;&nbsp;var v = $(fID).value;<br />
&nbsp;&nbsp;&nbsp;if (v.length>0 && v.substr(0,1) != "E")<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return 'The field "'+fLabel+'" must start with "E".';<br />
}
</div></p>
	
	<p>Second example this time we want to force at least one of the 2 "Home phone" or "Mobile" to have a value.
	</p>
	
	<p>
		In the XML<br />
		<div class="code">
			&lt;<span class="xmlElem">field</span> <span class="xmlAttr">label</span>="Home phone"
			... <span class="xmlAttr">jsvalidation</span>="<b>TelHome</b>" /&gt;
		</div>
	</p>
<p>In Javascript<br />
<div class="code">
function <b>TelHome</b>(){<br />
&nbsp;&nbsp;&nbsp;if ($("EVOLU_phoneh").value == "" && $("EVOLU_phonem").value == "")<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return 'At least one of "Home phone" or "Mobile" must have a value.';
<br />
}
</div>
</p>
<p>In this second example, we used hard-coded field names and labels so we didn't give arguments to the custom function.
Also, even though 2 fields are involved in the validation, we only needed to declare the rule once in the XML (on the field that will be flagged in red if the test fails).</p>
 
<P>See examples of <a href="../demo/dev_validation.aspx">custom validation</a>.</P>
	 
</asp:Content>