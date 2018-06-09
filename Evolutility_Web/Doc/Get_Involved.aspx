<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="MoreStuff"
 Title="Evolutility :: Get involved"
CodeFileBaseClass="BasePage"  
Menus="more"
SubMenuID="500"
Meta_Description="Get involved with Evolutility and participate in a new and promissing open source project" 
Meta_Keywords="open source oss praticipate contribute contribution participation FOSS"
  %>

<%@ Register Src="Lang_Authors.ascx" TagName="Lang_Authors" TagPrefix="uc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<a href="http://www.opensource.org/docs/definition.php" target="oss" style="float:right"><img src="../pix/oss2.gif" border="0"/></a>
<h1>Get involved</h1>

<p>Evolutility is a young open source project. We can definitly use your help.</p>

<p>Easy ways you can help Evolutility:</p>
 <ul> 
 <li>Tell your family, friends and colleagues about Evolutility</li>
 <li>Let us know if anything doesn't work or could be improved 
 by <a href="http://sourceforge.net/tracker/?func=add&group_id=225915&atid=1066274" target="SF">submitting a bug at SourceForge<span class="ExtWeb"></span></a></li>
 <li>Let us know what you do with Evolutility, share your story</li>
 </ul>
 
 <p>For the more technically-oriented:</p>
 <ul>
 <li>Help with the documentation</li>
 <li><a href="Get_Involved_Translation.aspx">Translate Evolutility</a> in another language</li>
 <li>Fix a bug or contribute code to the project</li>
 <li>Publish and share a web application</li> 
 </ul>
 
 <p>You can also <a href="http://localhost:49527/EvolutilityWeb/doc/Purchase.aspx">purchase a license</a>.</p>
 
 <a name="Thanks"></a>	
 <h2>Thanks to our contributors</h2>

<p>We would like to give thanks to people who contributed to Evolutility.</p>

	
	<p>We would also like to give credit to people who let us use some of their work:</p> 
	<ul>
		<li>Brian Kirchoff for his wonderful Rich Text editor <a href="http://nicedit.com/" target="ico">NicEdit<span class="ExtWeb"></span></a> </li>
		<li>Mark James for his  fantastic set of icons <a href="http://www.famfamfam.com/lab/icons/silk/" target="ico">Silk Icons<span class="ExtWeb"></span></a></li>
		<li>Julian Robichaux for his Date Picker widget</li>
		<li>Emanuele Feronato for her Lightbox <a href="http://www.emanueleferonato.com">emanueleferonato.com<span class="ExtWeb"></span></a></li>
	</ul>
	
	<p>Bug fixes and other improvements:</p>
	<ul>
		<li>Tomokazu Ohno for Japanese characters support</li>
		<li>Ramy Omar for stored procedure improvements</li>
		<li>Terry Kernan for helping with cross-browser issues</li>
	</ul>
<p><a href="Get_Involved_Translation.aspx">Translations</a>:</p>
	<uc1:Lang_Authors ID="Lang_Authors1" runat="server" />
	
	<p><br />&nbsp;</p>
</asp:Content>

