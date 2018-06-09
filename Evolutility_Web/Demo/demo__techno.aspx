<%@ Page validateRequest="false"   Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" 
CodeFile="demo.aspx.cs" Inherits="demo"
 Title="Evolutility :: Demos for developers"
CodeFileBaseClass="BasePage"  
Menus="demo_dev"
SubMenuID="3001"
  %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1>Technical demos</h1>

<p>Go further with Evolutility with some demos for developers showing speific product features.</p>
 
<div class="indent0">
<ul class="indent">
	<li><a href="dev_RTF.aspx">Rich Text Editor</a>: Integrated WYSIWYG editor for fields of type HTML (using TinyMCE).</li>
	<li><a href="dev_Permissions.aspx">Permissions</a>: See how the toolbar reflects permissions settings.</li>
	<li><a href="dev_Localization.aspx?LNG=FR">Localization</a>: Address book 
						<a href="dev_Localization.aspx?LNG=EN"><img alt="English" class="Icon" src="../pixevo/flags/en.gif" /></a>
						<a href="dev_Localization.aspx?LNG=CA"><img alt="Catalan" class="Icon" src="../pixevo/flags/ca.gif"/></a> 
						<a href="dev_Localization.aspx?LNG=ZH"><img alt="Chinese" class="Icon" src="../pixevo/flags/zh.gif" /></a> 
						<a href="dev_Localization.aspx?LNG=DA"><img alt="Danish" class="Icon" src="../pixevo/flags/da.gif" /></a>
						<a href="dev_Localization.aspx?LNG=FR"><img alt="French" class="Icon" src="../pixevo/flags/fr.gif" /></a> 
						<a href="dev_Localization.aspx?LNG=DE"><img alt="German" class="Icon" src="../pixevo/flags/de.gif" /></a>
						<a href="dev_Localization.aspx?LNG=HI"><img alt="Hindi" class="Icon" src="../pixevo/flags/hi.gif" /></a>
						<a href="dev_Localization.aspx?LNG=IT"><img alt="Italian" class="Icon" src="../pixevo/flags/it.gif" /></a> 
						<a href="dev_Localization.aspx?LNG=JP"><img alt="Japanese" class="Icon" src="../pixevo/flags/jp.gif"/></a>
						<a href="dev_Localization.aspx?LNG=FA"><img alt="Persian" class="Icon" src="../pixevo/flags/fa.gif" /></a>
						<a href="dev_Localization.aspx?LNG=PT"><img alt="Portuguese" class="Icon" src="../pixevo/flags/pt.gif" /></a>
						<a href="dev_Localization.aspx?LNG=RO"><img alt="Romanian" class="Icon" src="../pixevo/flags/ro.gif" /></a>
						<a href="dev_Localization.aspx?LNG=ES"><img alt="Spanish" class="Icon" src="../pixevo/flags/es.gif" /></a>
						<a href="dev_Localization.aspx?LNG=TR"><img alt="Turkish" class="Icon" src="../pixevo/flags/tr.gif" /></a>
	</li> 
	<li><a href="dev_Dependent_fields.aspx">Dependent fields</a>: Dependent dropdown where one field determines the list of value for another.</li>
	<li><a href="dev_Validation.aspx">Custom Validation</a>: Custom client validation rules.</li>
	<li><a href="dev_Navigation.aspx">Navigation</a>: Simple links to navigate Evolutility.</li> 
	<li><a href="dev_Events.aspx">Server Events</a>: Track changes in database (Insert, Update, Delete) as well as user login or logout. </li>
</ul>
</div>	

</asp:Content>

