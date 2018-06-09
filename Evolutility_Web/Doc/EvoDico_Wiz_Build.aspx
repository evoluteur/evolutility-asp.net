<%@ Page AutoEventWireup="true" CodeFile="EvoDico.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="EvoDoc" Language="C#" MasterPageFile="zmDoc.master" Menus="doc" Meta_Description="Evolutility documentation : App Wizard"
	Meta_Keywords="documentation developer user guide" SubMenuID="1575" Title="Evolutility :: Documentation : Build Wizard" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <h1>App Wizard</h1>
   
<p>Build applications easilly with a step wizard to create Evolutility web application components and database tables without any hand-coding.</p> 

<p>With this wizard Evolutility will gather the minimum information necessary to build your application.
Here we will create a "to do list" by following simple steps.</p>

<h2>Step 1 - Application definition</h2>
<p>Gathers the application name, the entity name (as the user calls it) in singular and plural.<br /><br />
<img src="pix/wizards/build_1.gif" class="shadow"  alt="" style="margin-left:40px" />
</p>

<h2>Step 2 - Data definition</h2>
<p>Gathers the list of fields (labels and types).<br />
<br />	
<img src="pix/wizards/build_2.gif" class="shadow"  alt="" style="margin-left:40px" /></p>

<h2>Step 3 - Fields definition details</h2>
<p>Specifies additional informations necessary for each field. 
Field properties depend on the field type choosen in the previous step.<br />
<br />
<img src="pix/wizards/build_3.gif" class="shadow"  alt="" style="margin-left:40px" /></p>

<h2>Step 4 - Search options</h2>
<p>Decides which fields are included in the search, advanced search, and list result.<br />
<br />
<img src="pix/wizards/build_4.gif" class="shadow"  alt="" style="margin-left:40px" /></p>

<h2>Step 5 - Panels layout</h2>
<p>Gathers the list of panels used to visually group fields together (in View and Edit modes). 
Also specifies the relative width of each panel according to a "flow positioning" scheme.<br />
<br />
<img src="pix/wizards/build_5.gif" class="shadow"  alt="" style="margin-left:40px" /></p>

<h2>Step 6 - Fields layout</h2>
<p>Decides which field belong to which panel (specified in the previous step), and the relative width of each field inside its panel.<br />
<br />
<img src="pix/wizards/build_6.gif" class="shadow"  alt="" style="margin-left:40px" /></p>

<h2>Step 7 - The application is ready</h2>
<p>This step shows the XML and SQL necessary for the application.  
This page also contains links to try or customize the application right away.<br />
<br />
<img src="pix/wizards/build_7.gif" class="shadow"  alt="" style="margin-left:40px" /></p>

	 
	
</asp:Content>

