<%@ Page AutoEventWireup="true" CodeFile="EvoDico.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="EvoDoc" Language="C#" MasterPageFile="zmDoc.master" Menus="doc" Meta_Description="Evolutility Documentation : Wizards"
	Meta_Keywords="documentation developer user guide" SubMenuID="1599" Title="Evolutility :: Documentation : Wizards" %> 
	
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
<h1>EvoDico Wizards</h1>
   
	<p>EvoDico provides a set of 4 wizards to build applications more easily.</p> 
	 <ul>
		<li><a href="EvoDico_Wiz_Build.aspx">Build Apps</a> - to build new applications from scratch.</li>
		<li><a href="EvoDico_Wiz_Install.aspx">Install Apps</a> - to install packaged applications</li>
		<li><a href="EvoDico_Wiz_XML2DB.aspx">Import XML</a> - to import existing XML Evolutility metadata</li>
		<li><a href="EvoDico_Wiz_DBScan.aspx">Map DB</a> - to map existing database tables</li>
	 </ul>
 	
	
<h2>Web control Properties</h2>

	<p>These step wizards require the assembly Evolutility.Wizards.dll. It provides the following properties:</p>
			
	<div class="indent2">
		<p><span class="ctrProp">BuildPages</span><br />
		Specifies if building a component metadata also builds a new ASPX web page file.</p> 

		<p><span class="ctrProp">BuildDatabase</span><br />
		Specifies if building a component metadata also builds the database objects too.</p> 

		<p><span class="ctrProp">MustLogin</span><br />
		Specifies if user authentication is required. By default it uses the stored procedure "EvoDicoSP_Login".</p> 

		<p><span class="ctrProp">PathXML</span><br />
		Path to Wizards metadata.</p> 

		<p><span class="ctrProp">PathWeb</span><br />
		Path to published Wizards metadata.</p> 
		
		<p><span class="ctrProp">ShowASPX</span><br />
		Displays component ASPX page code (after build).</p> 

		<p><span class="ctrProp">ShowXML</span><br />
		Displays component XML (after build).</p> 
			 
		<p><span class="ctrProp">ShowSQL</span><br />
		Displays component SQL for database creation (after build).</p> 

		<p><span class="ctrProp">ShowSkin</span><br />
		Displays links to different skins for testing.</p> 
			 
		<p><span class="ctrProp">StepID</span><br />
		Currently displayed step of the selected wizard.</p> 
		
		<p><span class="ctrProp">SqlConnection</span><br />
		Connection string to the Database. To avoid entering this property for every web control of your application, it can be set in the Web.config file of the application.</p>
		<p>
			<span class="ctrProp">SqlConnectionDico</span><br />
			Connection string to "EvoDico" (Evolutility Dictionary) Database. This property is used to run 2 different databases for the data and the metadata. 
			It can be set
			in the Web.config file.</p>
		<p><span class="ctrProp">Title</span><br />
		Wizard title (as displayed with step number).</p> 
		
		<p><span class="ctrProp">VirtualPathPictures</span><br />
		Sets the virtual path to the toolbar pictures (same as VirtualPathToolbar in Evolutility).</p> 

		<p><span class="ctrProp">WizardMode</span><br />
		Wizard to be performed. Possible values:
			<ul>
				<li>Build</li>
				<li>Install</li>
				<li>Map_DB</li>
				<li>Import_XML</li>
				<li>Wizard_Catalog</li>
			</ul> 			
		</p> 

	</div> 
		 
</asp:Content>

