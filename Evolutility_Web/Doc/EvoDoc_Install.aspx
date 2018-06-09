<%@ Page AutoEventWireup="true" CodeFile="Doc.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="EvoDoc" Language="C#" MasterPageFile="zmDoc.master" Menus="doc"
	Meta_Description="Evolutility documentation: Installation" 
	Meta_Keywords="documentation"
	SubMenuID="50" 
	Title="Evolutility :: Documentation : Installation" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1>Installation</h1>

<p>Because Evolutility is "generic" and metadata-driven, you will only need one single web control for all the different applications built with it. 
</p>

<h2>Installing the download</h2>

    <ol>
        <li>Copy the directory "EvolutilityWeb" which contains the web site to your web server.</li>
        <li>Attach the database (located in the "App_Data" directory of the web site).</li>
        <li>If necessary, Change the database connection string in the "appSettings" section of the Web.config
            file (or in every ASPX page). </li>   
    </ol>

<h2>Integrating Evolutility in your web site</h2>

	<p>To add Evolutility applications to your web site you will need the following files:</p>
	
	<h3>Assemblies</h3>	
 
	<div class="indent2">
		<div class=""><img class="icon" src="pix/folder.png" /> Bin</div>
		<div class="indent1"><img class="icon" src="pix/cog.png" /> Evolutility.UIServer.dll <span class="GreyText">- Evolutility Web control</span></div> 
		<div class="indent1"><img class="icon" src="pix/cog.png" />	Evolutility.DataServer.dll <span class="GreyText">- Http handler for dependent fields</span></div>
		<div class="indent1"><img class="icon" src="pix/cog.png" />	Evolutility.Toolbar.dll <span class="GreyText">- Toolbar (optional)</span></div> 
	</div>
	
	<br />&nbsp;
	
	<h3>Client-side resources</h3>
 
	<div class="indent2">
		<div class=""><img class="icon" src="pix/script_gear.png" /> Evol.css <span class="GreyText">- Stylesheet</span></div>
		<div class=""><img class="icon" src="pix/folder.png" /> PixEvo</div>
		<div class="indent1"><img class="icon" src="pix/folder.png" /> JS</div>
		<div class="indent2"><img class="icon" src="pix/folder.png" /> lang <span class="GreyText">- Translations</span></div> 
		<div class="indent2"><img class="icon" src="pix/script.png" /> EvolUtility.js <span class="GreyText">- Javascript library</span></div>
		<div class="indent2"><img class="icon" src="pix/script.png" /> EvolDate.js <span class="GreyText">- Widget for dates</span></div>
		<div class="indent1"><img class="icon" src="pix/folder.png" /> nicEdit <span class="GreyText">- Rich Text Editor</span></div> 
		<div class="indent1"><img class="icon" src="pix/image.png" /> EvolUI.gif <span class="GreyText">- Sprites for UI elements</span></div>
	</div>
 
	 <br/>&nbsp;

	<h3>Web.Config file</h3>
	
	<p>The following are the necessary Web.Config entries for Evolutility.</p>
	<div class="code">
	
	&lt;<span class="xmlElem">configuration</span> &gt;
		<br/>
		<div style="margin-left:20px;">		
			&lt;<span class="xmlElem">appSettings</span>  /&gt;
			<br/>
				<div style="margin-left: 20px;">
					&lt;<span class="xmlElem">add</span> <span class="xmlAttr">key</span>="SQLConnection"
					<span class="xmlAttr">value</span>="YOUR_DATABASE_CONNECTION" /&gt;
				</div>
			&lt;/<span class="xmlElem">appSettings</span>&gt;
			<br/>	
			...<br />
			&lt;<span class="xmlElem">system.web</span> &gt;
			<br/>
				<div style="margin-left:20px;">		
				&lt;<span class="xmlElem">httpHandlers</span>  /&gt;
				<br/>
					<div style="margin-left: 20px;">
						&lt;<span class="xmlElem">add</span> <span class="xmlAttr">verb</span>="*"
						<span class="xmlAttr">path</span>="evolutility.aspx" 
						<span class="xmlAttr">type</span>="Evolutility.DataServer.DataServer,Evolutility.DataServer"
						/&gt; 
					</div>
				&lt;/<span class="xmlElem">httpHandlers</span>&gt;
				<br/>
				</div>
			&lt;/<span class="xmlElem">system.web</span> &gt;			
		</div>
		...<br />
		&lt;/<span class="xmlElem">configuration</span> &gt;
	</div> 
	
	
	 <br/>&nbsp;
	 
<h3>Database pre-requisites</h3>

	<p>Before running Evolutility you will need to run the SQL scripts coming with the download.
	These scripts contain the necessary database objects for Evolutility.<br />
	</p>
	<p>More information on Evolutility <a href="EvoDoc_DB.aspx">database pre-requisites</a>.
	</p> 
 
	
 
<h2> Accessing the component definition (metadata)</h2>
	<p>
	<p>Each application component is linked to an XML document. You must create a 
		directory on the server to store your XML files and use the control property  
			<span class="xmlAttr">XMLFile
		</span>  to specify the path to it's metadata.&nbsp;</p>
	<p>Example: XMLFile= "XML/addressbook.xml"
	</p>
	 
	 

			
<h2>Embedding the Control into the Page</h2>
	
	<p>To embed the control, you will copy the control DLL into the bin directory of 
		your web application and add two lines of code to your page.&nbsp;The first 
		line registers the control tag prefix (place this at the top of the page):
	</p>
	<P class="code">&lt;%@ <span class="xmlElem">Register</span>&nbsp;<span class="xmlAttr">TagPrefix
		</span>="EVOL" <span class="xmlAttr">Namespace</span>="Evolutility" <span class="xmlAttr">
			Assembly</span>="Evolutility.UIServer" %&gt;
		<br/>
	</p>
	<p>The second line embeds the Evolutility control (place anywhere within the page):
	</p>
	<P class="code">&lt;<span class="xmlElem">EVOL:UIServer</span>&nbsp;<span class="xmlAttr">id
		</span>= "Evo1"&nbsp;<span class="xmlAttr">runat </span>= "server"<br/>
		&nbsp;&nbsp;&nbsp;&nbsp;<span class="xmlAttr">XMLfile</span>= 
		"xml/Customers.xml"<br/>
		&nbsp;&nbsp;&nbsp;&nbsp;<span class="xmlAttr">VirtualPathToolbar </span>= "PixEvo"<br/>
		<STRONG>...</STRONG>&nbsp;/&gt;
		<br/>
	</p>
	<p>With Visual Studio .Net, you can use drag &amp; drop to embed the control into 
		your page. This option requires that the control has been added to the Toolbox. 
		To add a control to the Toolbox, perform the following steps:
	</p> 
		<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
			<TR>
				<TD vAlign="top"><IMG src="pix/ui/toolbox.gif"></TD>
				<TD vAlign="top">
					<ol>
						<li>Open an .ASPX page in Design view, and make the Web Forms tab active in the 
						Toolbox.</li>
						<li>Right-Click the Toolbox, click Add/Remove Items...</li>
						<li>In the Customize Toolbox dialog box click the Browse button on the lower right 
						below the components list.</li>
						<li>Navigate to the directory that contains the assembly into which your controls 
						are, and click the name of that assembly (for example, 
						C:\Evolutility\Evolutility.UIServer.dll).</li>
						<li>Click OK to close the dialog box. The control now appears in the Toolbox under 
							the Web Forms tab.</li>
					</ol>
				</TD>
			</TR>
		</TABLE> 
		
		
	<p>&nbsp;</p>
	<p></p>
</asp:Content>
