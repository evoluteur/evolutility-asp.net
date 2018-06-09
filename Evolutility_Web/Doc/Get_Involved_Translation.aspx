<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="MoreStuff"
 Title="Evolutility :: Get involved"
CodeFileBaseClass="BasePage"  
Menus="more"
SubMenuID="500"
Meta_Description="Become active in the open source world: Evolutility translation kit" 
Meta_Keywords="open source oss praticipate contribute contribution participation FOSS"
  %>

<%@ Register Src="Lang_Authors.ascx" TagName="Lang_Authors" TagPrefix="uc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<a href="http://www.opensource.org/docs/definition.php" target="oss" style="float:right"><img src="../pix/oss2.gif" border="0"/></a>
<h1>Evolutility Translation Kit</h1>
Version 4.0 - 12/12/2011
 
<p>If Evolutility is not available in your language of choice and you wish to translate it,
 here are the steps to follow:</p> 
 
<ol class="vSpaced">  
	<li>Download and Extract <a href="https://sourceforge.net/project/showfiles.php?group_id=225915&package_id=311200" target="sf">Evol_Languages.zip<span class="ExtWeb"></span></a>.<br />&nbsp;</li>
	<li>Lookup the ISO 639-1 code for your language at <a href="http://en.wikipedia.org/wiki/List_of_ISO_639-1_codes" target="WP">Wikipedia<span class="ExtWeb"></span></a>.<br />&nbsp;</li>
	<li>In the CS folder, translate one of the C# files, and save it as a new file named after the abbreviation of the language "EvoLang_XX.cs" (examples: "EvoLang_FR.cs" for French; "EvoLang_ES.cs" for Spanish).<br />&nbsp;</li>
	<li>In the JS folder, translate one of the Javascript files, and save it as "XX.js" (examples: "FR.js" for French; "ES.js" for Spanish).<br />&nbsp;</li>
	<li>In the XML folder, for the file AddressBook.XML, translate all attributes called "label" and save it as "AddressBook_XX.XML". Do not translate other attributes in these files.<br />&nbsp;</li>
	<li>If possible, test your translation with the latest download of Evolutility code and 
	update the heading comment in your 3 files to show your name as the author of the translation.<br />&nbsp;</li>
	<li>Tell us about date formats in your language.
		<ul>
			<li>Do you write dates Month/Day/Year (like in American English) or Day/Month/Year (like in French)? </li>
			<li>Does the week starts on Sunday (like in English) or Monday (like in French) on calendars?</li>
		</ul><br />&nbsp;
 </li>
	<li>Tell us about other specificities of your language.
		<ul>
			<li>Do you write "Pi = 3.14" (with a dot like in American English) or "Pi = 3,14" (with a coma like in French or Danish)? </li>
		</ul><br />&nbsp;
	</li>
	<li>Copy the following text (our short contributor license agreement) and add it to your email : <br />
 "I'm offering this translation to Olivier Giulieri free of charge, with full rights to do anything he wants with it, and I do not expect anything in return."
	<br />&nbsp;</li>
	<li>Send an email to Olivier at Evolutility dot org with:
		<ul>
			<li>the translated files (3), (4), and (5)</li>
			<li>the language formats information (7) and (8)</li>
			<li>the intellectual property ownership release (9) </li>
		</ul> <br />&nbsp;
  </li>  
</ol>

 
<p>Thank you very much for helping the Evolutility project.</p> 

<p>Available translations:</p>
	<p>
		<uc1:Lang_Authors ID="Lang_Authors1" runat="server" />
		&nbsp;</p>
 
 
<p>Localized <a href="http://evolutility.org/demo/dev_Localization.aspx?LNG=ES">demo of a contact list</a>  are available at www.evolutility.org.
</p>
 
 
	<p><b>Technical Notes</b>:<br />
Some strings contain "{0}" or "{1}". These are placeholders for other words to be inserted at run-time.
For example: "Welcome {0}" could become "Welcome John" or "Welcome Mary" when the program is running.
We use this placeholder scheme because words may be in different order in different languages. 
	</p>
	  
	 
	<p>To have<br />
	<div class="indent3"> 
	<img alt="English" class="icon" src="../pixevo/flags/en.gif"/> 
		New <span class="Highlight">contact</span> saved at <span class="HighlightBlue">12:15 AM</span>.<br />
	<img alt="Japanese" class="icon" src="../pixevo/flags/jp.gif"/>	
	<span class="HighlightBlue">12:15 AM</span>に保存された新しい<span class="Highlight">連絡先</span>.<br />
		</div>
	
	<p>We need <span class="Highlight">{0}</span>=entity <span class="HighlightBlue">{1}</span>=now <br />

</p>
	<div class="indent3">
	<img alt="English" class="icon" src="../pixevo/flags/en.gif"/> NewSave = "New <span class="Highlight">{0}</span> saved at <span class="HighlightBlue">{1}</span>." 
	<br/><img alt="Japanese" class="icon" src="../pixevo/flags/jp.gif"/> NewSave = "<span class="HighlightBlue">{1}</span>に保存された新しい<span class="Highlight">{0}</span>." 
</div>
<p> 

 <br /><br /><br />&nbsp;</p>
</asp:Content>

