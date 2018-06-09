<%@ Page AutoEventWireup="true" CodeFile="Doc.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="EvoDoc" Language="C#" MasterPageFile="zmDoc.master" Menus="doc"
	Meta_Description="Evolutility documentation: Web Control Properties" Meta_Keywords="documentation"
	SubMenuID="44" Title="Evolutility :: Documentation : Web Control Properties" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1><img src="pix/dn_prop.gif" class="icon" alt="" />Web Control Properties</h1>

<p>In addition to the standard WebControls properties inherited from the .net class, Evolutility provides the following properties.
</p>
<div class="indent3">
<p><span class="ctrProp">AllowSorting</span></p><p>
Allow search results to be sorted by columns. Setting it to false removes the up and down arrows for sorting records in the list header.</p> 

<p><span class="ctrProp">BackColorRowMouseOver</span></p><p>
Determines color to highlight rows when the mouse cursor passes over.</p> 

<p><span class="ctrProp">CollapsiblePanels</span></p><p>
Determines if user can show and hide panels.</p>

<p><span class="ctrProp">DataIsMetadata</span></p><p>
Indicates if the data is the metadata (uses SqlConnectionDico for data).</p>
    <p>
        <span class="ctrProp">DBAllowCharts</span> (new in 4.0)<br />
        Properties used to allow or disallow the Charts feature (and show/hide the "Charts" toolbar button).</p>
        
    <p><span class="ctrProp">DBAllowDelete</span></p><p>
Properties used to allow user to delete records (and show/hide the "Delete" button on toolbar).</p> 

<p><span class="ctrProp">DBAllowExport</span></p><p>
Enables the export feature (for lists and single records).</p> 

<p><span class="ctrProp">DBAllowHelp</span></p><p>
Will display the content of the help attribute of each field while in edit mode.</p>

<p><span class="ctrProp">DBAllowInsert</span></p><p>
Properties used to allow or disallow database record insertion on the driving table. DBReadOnly need to be False for these properties to take action.</p> 

<p><span class="ctrProp">DBAllowInsertDetails</span></p><p>
Properties used to allow or disallow database record insertion on the details table.</p>
	
<p><span class="ctrProp">DBAllowLogout</span></p><p>
Displays or not the "Logout" button on the toolbar (for logged-in users). 
This property is useful when bypassing Evolutility integrated user authentication.</p>

<p><span class="ctrProp">DBAllowMassUpdate</span></p><p>
Specify whether or not "Mass Update" is allowed (applied to search result or selections).</p>
		
<p><span class="ctrProp">DBAllowPrint</span></p><p> 
Specify whether or not the "Print" button should be displayed on the toolbar.</p>

<p><span class="ctrProp">DBAllowSearch</span></p><p>
Specify whether or not the "Search" and "Advanced Search" buttons should be displayed on the toolbar.</p> 

<p><span class="ctrProp">DBAllowSelections</span></p><p>
Displays a "Selection" button on the toolbar to provide a list of custom queries. Need queries and query defined in the XML.</p> 

<p><span class="ctrProp">DBAllowUpdate</span></p><p>
Properties used to allow or disallow modification of records on the driving table. DBReadOnly need to be False for these properties to take action.</p> 

<p><span class="ctrProp">DBAllowUpdateDetails</span></p><p>
Properties used to allow or disallow in-place edition of the details table.</p> 

<p><span class="ctrProp">DBReadOnly</span></p><p>
Make the page read only. Disables Add, Edit, and Delete on all records (toolbar buttons disappear when DBReadOnly is set to true).</p> 

<p><span class="ctrProp">DisplayMode</span> (Read Only)</p>
<p>Indicates the current display mode (View, Edit, List, Search, Advanced Search, or Selections).</p> 

<p><span class="ctrProp">DisplayModeStart</span></p><p>
Mode in which Evolutility is displayed the first time the page is called. This property can be overridden by using a "MODE" parameter in the calling URL (see <a href="EvoDoc_Navigation.aspx">navigation</a> for more information).</p> 

<p><span class="ctrProp">ItemID</span> (Read Only)</p><p>
ID of the current record.</p> 

<p><span class="ctrProp">Language</span></p><p>
Localization of all text displayed in the control and default date format. Supported languages are:

 	<ul>
		<li>CA - <img alt="Catalan" class="icon" src="../pixevo/flags/ca.gif" /> Catalan</li>
		<li>ZH - <img alt="Chinese" class="icon" src="../pixevo/flags/zh.gif" /> Chinese (simplified)</li>
		<li>DA - <img alt="Danish" class="icon" src="../pixevo/flags/da.gif" />	Danish</li>
		<li>EN - <img alt="English" class="icon" src="../pixevo/flags/en.gif" /> English</li>
		<li>FA - <img alt="Farsi" class="icon" src="../pixevo/flags/fa.gif" /> Farsi</li>
		<li>FR - <img alt="French" class="icon" src="../pixevo/flags/fr.gif" /> French</li>
		<li>DE - <img alt="German" class="icon" src="../pixevo/flags/de.gif" /> German</li>
		<li>HI - <img alt="Hindi" class="icon" src="../pixevo/flags/hi.gif" /> Hindi</li>
		<li>IT - <img alt="Italian" class="icon" src="../pixevo/flags/it.gif" /> Italian</li>
		<li>JP - <img alt="Japanese" class="icon" src="../pixevo/flags/jp.gif"/> Japanese</li>
		<li>PT - <img alt="Portuguese" class="icon" src="../pixevo/flags/pt.gif" /> Portuguese</li>
		<li>RO - <img alt="Romanian" class="icon" src="../pixevo/flags/ro.gif" /> Romanian</li>
		<li>ES - <img alt="Spanish" class="icon" src="../pixevo/flags/es.gif" /> Spanish</li>
		 <li>RO - <img alt="Turkish" class="icon" src="../pixevo/flags/tr.gif" /> Turkish</li>
	</ul>
	
	<a href="Get_Involved_Translation.aspx">Translation kit</a>
</p> 

<p><span class="ctrProp">NavigationLinks</span></p><p>
Show navigation links (for First, Previous, Next, and Last records) on the bottom of the page when editing or viewing records.</p> 

<p><span class="ctrProp">RowsPerPage</span></p><p>
Number of rows displayed at one time in search results. This property is only available if a stored procedure for paging is specified in the XML of the application definition.</p> 

<p><span class="ctrProp">SecurityKey</span></p><p>
Key to allow credential sharing among components.</p> 

<p><span class="ctrProp">SecurityModel</span></p><p>
Manage users in 4 different ways with one of the following built-in security model:
</p>
	<ul>
	<li>Single_User (no password)</li>
	<li>Single_User_Password</li>
	<li>Multiple_Users_RLS (each user only sees his own data)</li>
	<li>Multiple_Users_Sharing (each user can view all data but only modify or delete his own records).</li>
	<li>Multiple_Users_Collaboration: every logged-in user can see and edit all records</li>	
</ul>
<p><b>Notes</b>: Multiple_Users_RLS, Multiple_Users_Sharing, and Multiple_Users_Collaboration
 require your object database table to have a column of type integer called "UserID".
 <br />	
 More information about it in <a href="EvoDoc_Access.aspx">Security and users access management</a>.
</p> 

<p><span class="ctrProp">ShowTitle</span></p><p> 
Determines the display of a header title.</p>

<p><span class="ctrProp">SQLConnection</span></p><p>
Connection string to the database. To avoid entering this property for every web control of your application, it can be set in the Web.config file of the 
application.</p> 

<p>
			<span class="ctrProp">SqlConnectionDico</span></p><p>
			Connection string to "EvoDico" (Evolutility Dictionary) Database. This property is used to run 2 different databases for the data and the metadata (instead of XML). 
			It can be set
			in the Web.config file.</p>
			
<p><span class="ctrProp">Text</span></p><p>
Introduction text only displayed the first time the form is called.</p> 

<p><span class="ctrProp">Title</span></p><p> 
Application title.</p>

<p><span class="ctrProp">ToolbarPosition</span></p><p> 
Position/display of the toolbar. Possible values:
<ul>
	<li>Top</li>
	<li>Top_and_Bottom</li>
	<li>None</li></ul>
</p>

<p><span class="ctrProp">UseCache</span></p><p>
 Enables or disables caching of lists of values. Setting it to true will improve performances.</p>

<p><span class="ctrProp">UserComments</span></p><p>
 Allows users to post comments on specific records. Possible values:</p>
<ul>
	<li>None</li>
	<li>Read only</li>
	<li>Logged Users</li>
	<li>Anonymous</li>
</ul>
	<p><b>Notes</b>: Using comments requires your object database table to have a column of type integer called "CommentCount".
	<br />More information about it in <a href="EvoDoc_Collaboration.aspx">Collaboration and user comments</a>.<br />

</p>

<p><span class="ctrProp">UserID</span> (Read Only)</p><p>
ID of the current user (0 if the user is not logged in or Evolutility is used in Single User mode).</p> 

<p><span class="ctrProp">VirtualPathPictures</span></p><p>
Sets the virtual path to the pictures used in fields.</p> 

<p><span class="ctrProp">VirtualPathToolbar</span></p><p>
Sets the virtual path to the toolbar pictures.</p> 

<p><span class="ctrProp">XMLFile</span></p><p>
Name of the XML file containing the metadata, it can be specified as an absolute path as "c:/EVOLUTILITY/XML/AddressBook.XML".
<br />
Using <A href="EvoDico.aspx">Evolutility Dictionary</A>, this property can also be given an integer value which is the ID of the form in the table EvoDico_Form.
</p> 

</div>


</asp:Content>
