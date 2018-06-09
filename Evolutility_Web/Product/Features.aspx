<%@ Page AutoEventWireup="true" CodeFile="Product.aspx.cs" CodeFileBaseClass="BasePage"
    Inherits="ProductPage" Language="C#" MasterPageFile="zmProduct.master" 
Menus="product"
SubMenuID="2"
    Meta_Description="Evolutility feature list." 
    Meta_Keywords="evolutility open source object features master-details CRUD metadata xml database"
    Title="Evolutility : Features" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">  
<h1>Evolutility Features</h1>

<p>Evolutility is the only open source metadata-driven framework covering at the same time the UI and the database dynamically 
without requiring and hand-coding.
</p>
<p>All other products of that kind (SalesForce, Intuit QuickBase...) are SaaS platforms 
who do not let you host your applications on your own server, even less look at the source code.</p>
 
	<h2>Full UI for CRUD interaction</h2>
	<p>Full CRUD (Create, Read, Update, Delete)... plus charts, search and advanced search, login, export, and pre-defined selections on one single physical page.</p>
	   
 		
<h2>Multi-users environment</h2>
<p>Multiple security models for password protection, row level security, sharing... 
<br />
<a href="../demo/demo__security.aspx">Live demo</a>.</p>
	
<h2>Collaboration</h2>
<p>Information sharing, comments postings, forums...
<br />
<a href="../demo/demo_addressbook_sharing.aspx">Live demo</a>.</p>

<h2>Master-details</h2>
<p>CRUD functions, in-place editing grid for details.</p>

<h2>Integrated Rich Text Editor</h2>
	<p>WYSIWYG editor in the browser for HTML fields with NicEdit.<br />
		<br />
		<img src="..\Doc\pix\ft\html-edit.gif" class="shadow" /></p>
	
<h2>Multi-lingual</h2>
<p>Evolutility is available in 14 languages: 
<img alt="Catalan" class="icon" src="../pixevo/flags/ca.gif" />Catalan,
<img alt="Chinese" class="icon" src="../pixevo/flags/zh.gif" />Chinese (simplified),
<img alt="Danish" class="icon" src="../pixevo/flags/da.gif" />Danish,
<img src="../pixevo/flags/en.gif" class="icon" alt="English"/>English, 
<img alt="French" class="icon" src="../pixevo/flags/fr.gif" />French, 
<img alt="Farsi" class="icon" src="../pixevo/flags/fa.gif" />Farsi, 
<img alt="German" class="icon" src="../pixevo/flags/de.gif" />German,
<img alt="Hindi" class="icon" src="../pixevo/flags/hi.gif" />Hindi,
<img alt="Italian" class="icon" src="../pixevo/flags/it.gif" />Italian,
<img alt="Japanese" class="icon" src="../pixevo/flags/jp.gif"/>Japanese, 
<img alt="Portuguese" class="icon" src="../pixevo/flags/pt.gif" />Portuguese,
<img alt="Romanian" class="icon" src="../pixevo/flags/ro.gif" />Romanian,
<img alt="Spanish" class="icon" src="../pixevo/flags/es.gif" />Spanish, and 
<img alt="Turkish" class="icon" src="../pixevo/flags/tr.gif" />Turkish. 
<br />
<a href="../demo/dev_Localization.aspx">Live demo</a>.</p>

<h2>Multiple export formats</h2>
<p>Excel, CSV, HTML, XML, SQL, JSON...<br />
<a href="../demo/demo_addressbook.aspx?MODE=export">Live demo</a>.</p>

<h2>Source code available</h2> 
<p>Flexibility of customizations, easier debugging with break points.
<br /><img src="../pix/oss.gif" alt="" />	</p>

	
<h2>Fully metadata-driven</h2>
<p>No code is required, applications are fully defined by <a href="metamodel.aspx">their metadata</a>.</p> 
<p>Application definitions are stored in XML<br /><br />
<img src="pix/addressbook-xml.gif" class="shadow"/>
</p>
<p>or directly in the database.<br />
	<br />
	<img class="shadow" src="pix/addressbook-dico.gif" />
</p>
	
<h2>Cross-browser support</h2>
<p>Supports Internet Explorer, Chrome, Firefox, and Safari.</p>

<h2>Light-weight footprint</h2>
<p>Uses Javascript, CSS, and Sprites to reduce network traffic and response time.</p>
 
<h2>Easy to integrate</h2>
<p>Include Evolutility web control in any ASP.net page and start using custom CRUD on your database.</p>

</asp:Content>

