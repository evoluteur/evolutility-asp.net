<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="EvoDico.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="1550"
Meta_Description="Evolutility Documentation" 
Meta_Keywords="documentation developer user guide install installation setup configuration"
%> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
 
<h1>Installation</h1>

	<p>Evolutility Dictionary is an application built with Evolutility. It will have the same requirements,
	 and a few additional ones.	</p> 

<h2>Installing the download</h2>

<p>To setup EvoDico:</p>

    <ol>
        <li>Copy the directory "EvoDico" which contains the web site to your web server.</li>
        <li>Create a new SQL Server database</li>
        <li>Change the database connection string in the "appSettings" section of the Web.config file (or in every ASPX page).</li>
        <li>Run the SQL scripts which are in the "SQL" directory on your database in the following order:
            <ol>
				<li>EVOL-Common.sql</li>
				<li>EVOL-MultiUsers.sql</li>
				<li>EvoDico.sql</li>
				<li>DBObjects.sql</li>
            </ol>
        </li>
    </ol>
    
<p>To login to EvoDico: 
	</p>    

	<ul>
		<li>The default administrator login/password is Evol/Love</li>
		<li>Valid users login/password are John/John and Mary/Mary 
			<br />
		</li>
	</ul>


<h2>Integrating Evolutility in your web site</h2>

	
	<h3>Assemblies</h3>	
	
	<p>
	<div class="indent2">
		<div class=""><img class="icon" src="pix/folder.png" /> Bin</div>
		<div class="indent1"><img class="icon" src="pix/cog.png" /> Evolutility.UIServer.dll <span class="GreyText">- Evolutility Web control</span></div> 
		<div class="indent1"><img class="icon" src="pix/cog.png" />	Evolutility.DataServer.dll <span class="GreyText">- Http handler for dependent fields</span></div>
		<div class="indent1"><img class="icon" src="pix/cog.png" />	Evolutility.Toolbar.dll <span class="GreyText">- Toolbar (optional)</span></div> 
		<div class="indent1"><span class="Highlight"><img class="icon" src="pix/cog.png" /> Evolutility.ExportWizard.dll <span class="GreyText">- Data export (optional)</span></span></div> 	
		<div class="indent1"><span class="Highlight"><img class="icon" src="pix/cog.png" /> Evolutility.Wizards.dll <span class="GreyText">- Application Wizards</span></span></div> 	
	</div>
	</p>
	
	<h3>Client-side resources</h3>
	<p>
	<div class="indent2">
		<div class=""><img class="icon" src="pix/folder.png" /> PixEvo</div>
		<div class="indent1"><img class="icon" src="pix/folder.png" /> JS</div>
		<div class="indent2"><img class="icon" src="pix/folder.png" /> lang <span class="GreyText">- Translations</span></div> 
		<div class="indent2"><span class="Highlight"><img class="icon" src="pix/script.png" /> EvoDico.js <span class="GreyText">- Javascript library</span></span></div>
		<div class="indent2"><img class="icon" src="pix/script.png" /> EvolUtility.js <span class="GreyText">- Javascript library</span></div>
		<div class="indent2"><img class="icon" src="pix/script.png" /> EvolDate.js <span class="GreyText">- Widget for dates</span></div>
		<div class="indent1"><img class="icon" src="pix/script_gear.png" /> Evol.css <span class="GreyText">- Stylesheet</span></div>
		<div class="indent1"><span class="Highlight"><img class="icon" src="pix/script_gear.png" /> EvolWiz.css <span class="GreyText">- Stylesheet</span></span></div>
		<div class="indent1"><img class="icon" src="pix/image.png" /> EvolUI.gif <span class="GreyText">- Sprites for UI elements</span></div>
		<div class="indent1"><span class="Highlight"><img class="icon" src="pix/image.png" /> 1to9.gif <span class="GreyText">- Sprites for the step wizards</span></span></div>
		
	</div>
	</p>
	  
	
<h3>Database pre-requisites</h3>

	<p>Before running Evolutility Dictionary, you will need to run the SQL scripts coming with the download.
	These scripts contain the necessary database objects (prefixed "EvoDico").<br />
	</p>
	<p>More information on Evolutility <a href="EvoDico_DB.aspx">database pre-requisites</a>.
	</p> 
 
	
 
<h2> Accessing the component definition (metadata)</h2>
	<p>
	<p>Each application component is linked to an XML document. You must create a 
		directory on the server to store your XML files and use the control property  
			<span class="xmlAttr">XMLFile
		</span>  to specify the path to it's metadata.&nbsp;</p>
	<p>Example: XMLFile= "XML/addressbook.xml"
	</p>
	 
	 
 
	<p>&nbsp;</p>


</asp:Content>

