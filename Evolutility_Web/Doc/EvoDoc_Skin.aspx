<%@ Page AutoEventWireup="true" CodeFile="Doc.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="EvoDoc" Language="C#" MasterPageFile="zmDoc.master" Menus="doc"
	Meta_Description="Evolutility documentation: Skins" Meta_Keywords="documentation"
	SubMenuID="70" Title="Evolutility :: Documentation: Skins" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1>Skins (fonts, colors, sizes)</h1>

	<p>
	Evolutility skins are composed of a stylesheet with associated pictures. This section explains how it all connects together. 
	</p> 
	   
<h2>Stylesheet</h2>
	<p>
	Evolutility can uses stylesheets for the colors, fonts, sizes of its visual 
	elements. Styles can be defined in the HTML or in external stylesheet documents 
	(CSS files).
	</p>
	
	<h3>Fields and groups</h3>
	
	<p>
	.Field, .FieldView, .FieldReadOnly, .FieldComments, .FieldMain
	
	</p>
	
	<p>
	Stylesheet class names used by Evolutility are as follow:</p>
	<p>
	</p>
	<p><img src="pix/ui/css-elements1.gif"></p>
	<p><img src="pix/ui/css-elements2.gif"></p>
	<p> 
	</p>
	<p>In addition to the former style classes, it is possible to set the class of each 
		specific field or panel by using the "cssclass" attribute in the metadata for 
		any field or panel.</p>
	<p>Also, specific class names are used for some of the modes: "FormLogin", "FormSearch", and "FormExport".</p>
	
	<p>To attach a stylesheet to your ASP page, use the "LINK" tag in the header 
		declaration.
	</p>
	
	
	<h3>Validation</h3>
	<p>"FieldInvalid" is used for fields not passing validation (in conjonction with the "Field" class).</p>
	<p><img src="pix/ui/css-invalid.gif"  alt="" style="margin-left:40px"></p>
	
	<P class="code">&lt;html&gt;<br/>
		&nbsp; &lt;head&gt;<br/>
		&nbsp;&nbsp;&nbsp; &lt;title&gt;Evolutility Demo&lt;/title&gt;<br/>
		&nbsp;&nbsp;&nbsp; <span class="Highlight"><b>&lt;LINK&nbsp;href="evolutility.css" 
				rel="stylesheet" &gt;</b> </span>
		<br/>
		&nbsp;...<br/>
	</p>
	
	<h3>Toolbar (using sprites)</h3>
	<p>To be as lightweight as possible, Evolutility is using a single file for all icons of its UI.
	
	This single file is specified in the style sheet. It is essential to the toolbar and other UI elements.
	</p>
	<p>
	<img alt="" src="pix/ui/tb-sprites.gif" style="margin-left: 40px" />
	</p>
	
	<h3>right-to-left justified text</h3>
	
	<p>Evolutility can be setup for right-to-left justified languages.</p>
	
	<p>
.FieldR2L{
    font-family:Ezra SIL;
    direction:rtl;
	color:#000000;
	background-color:#FFFFFF; 
    width:100%;
}
	</p>
	
<h2>Application Icons and pictures</h2>
	<p>Entities icons are specified in the metadata by the attribute 
			<span class="xmlElem">icon</span> of the element <a href="ED_Elem_form.aspx#data">data</a>. Other 
		attributes related to pictures are: <span class="xmlAttr">dbcolumnicon</span>,
		<span class="xmlAttr">img</span>, and <span class="xmlAttr">imglist</span>.&nbsp;</p>
	<p>The path to the toolbar icons is specified by the <A href="EvoDoc_Properties.aspx?ID=82">property</A> 
        <span class="xmlAttr">VirtualPathToolbar</span> of the control.&nbsp;</p>

	<p>Many customizable skin templates are available at <a href="http://www.oswd.org" target="_blank">Open Source Web Design<span class="ExtWeb"></span></a>.</p>
	

<p><b>Note</b>: Evolutility generated HTML doesn't fully support XHTML transitional or strict yet.
</p>
		

</asp:Content>