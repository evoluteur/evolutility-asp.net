<%@ Page AutoEventWireup="true" CodeFile="index.aspx.cs" CodeFileBaseClass="BasePage"
    Inherits="index" Language="C#" MasterPageFile="~/zmIndex.master" 
Menus="home"
    Meta_Description="Evolutility is an open source generic web UI for database CRUD applications. 
    With it you can build web applications for ASP .NET and Microsoft SQL Server easily without writing code. Simply describe your application structure and Evolutility will provide the necessary web forms and the corresponding database CRUD (Create, Read, Update, Delete) functionality based on a simple mapping." 
    Meta_Keywords="lightweight Framework evolutility open source object relational mapper database driven web application CRUD ORM metadata ASP.net Microsoft.net SQL server no code"
    Title="Evolutility  :: CRUD Framework" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

<h1>Lightweight framework for heavy lifting</h1><div id="TabHelp"></div>

<p>Evolutility is an open source generic web UI for database applications. 
With it you can create small web apps like an 
<a href="demo/demo_addressbook.aspx">Address book</a>,  
a <a href="demo/demo_todo.aspx">To do list</a> 
or even a <a href="demo/demo_winecellar.aspx">Wine Cellar</a> 
without writing any code.
							 
</p>
  
 		 				 
						 
<table class="groupedColumns" >
<tr>
	<td valign="top" style="width:50%">
	
		
		<div class="dft">
			<a href="evodico/evodico.aspx"><h2>Evolutility Dictionary</h2></a>
			
			<p><a href="evodico/evodico.aspx">EvoDico</a> is a database to store applications metadata, and an application to modify this metadata. 
			It is a Web application which can modify itself. With it you do not need XML any more.</p>
			
		</div>
				
			
		<div class="dft">
			<a href="product/product.aspx">
				<h2>
					Imagine the possibilities</h2>
			</a>
			<p>
				Build your own custom web application quickly and easily without having to write
				any C#, HTML, CSS, Javascript, or SQL.
			</p>
			<p>
				Quickly build professional looking web administration pages for your web site.
			</p>
		</div>
		
			
		<div class="dft">
			<a href="product/news.aspx"><h2>News & Press</h2></a>
 
			<table border="0">
				<tr valign="top">
					<td> 
						<a href="http://sourceforge.net/blog/create-database-driven-web-apps-easily-with-evolutility/" target="new"><img src="http://sflogo.sourceforge.net/sflogo.php?group_id=225915&type=12" width="120" height="30" border="0" class="shadow" style="margin-right:10px;" /></a>
					</td> 
					<td> 
						<a href="http://sourceforge.net/blog/create-database-driven-web-apps-easily-with-evolutility/" target="new">Interview by SourceForge</a>
						<br /><i>Create database-driven web apps easily with Evolutility</i>
					</td>
				</tr>
				<tr valign="top">
					<td> 
						<a href="http://www.codeproject.com/KB/codegen/Metamodel_for_CRUD.aspx" target="cp1"><img border="0" src="product/pix/CPsl.gif" class="shadow" style="margin-right:10px;" /></a>
					</td> 
					<td> 
						<a href="http://www.codeproject.com/KB/codegen/Metamodel_for_CRUD.aspx" target="cp1">Code Generation contest winner on CodeProject</a>
						<br /><i>Minimalist Meta-Model for CRUD Applications</i>
					</td>
				</tr>
			</table> 
		</div> 	
		 
	</td>  
	<td style="width: 50%" valign="top"> 
 
    <div class="dft">
		<a href="demo/demo.aspx"><h2>Demo applications</h2> </a> 
			
			<p>These sample applications are not anything you haven't seen before. 
			The cool thing is that <b>these were built in only a few minutes each</b>, and can be customized as easily.</p>									
			
			<table border="0"  cellpadding="0" cellspacing="0">
				<tr valign="top">
				<td>
					<ul class="indent">
						<li><nobr><a href="demo/demo_addressbook.aspx">
							<img class="Icon16" alt="" src="PixEvo/contact.gif"/>Address book</a></nobr></li> 
						<li><a href="demo/demo_todo.aspx">
							<img class="Icon16" alt="" src="PixEvo/todo.gif"/>To do</a></li>
						<li><a href="demo/demo_memopad.aspx">
							<img class="Icon16" alt="" src="PixEvo/memo.gif"/>Memo pad</a></li>
						<li><nobr><a href="demo/demo_photo.aspx">
							<img class="Icon16" alt="" src="PixEvo/photo.gif"/>Photo album</a></nobr></li> 
					</ul>
				</td> 
				<td>
					<ul class="indent">
						<li><a href="demo/demo_winecellar.aspx">
							<img alt="" class="Icon16" src="PixEvo/wine.gif"/>Wine Cellar</a></li>
						<li><a href="demo/demo_restaurant.aspx">
							<img alt="" class="Icon16" src="PixEvo/resto.gif"/>Restaurants</a></li>
						<li><a href="demo/demo_bookmark.aspx">
							<img alt="" class="Icon16" src="PixEvo/favourites.gif" />Bookmarks</a></li>
						<li><nobr><a href="demo/demo_braille.aspx">
							<img alt="" class="Icon16" src="PixEvo/brl.gif"/>Braille Resources</a></nobr></li>
					</ul>
				</td> 
			</tr>
			<tr>
			<td colspan="2">  
			 
		<p>Evolutility is localized in 14 languages: 
		<nobr><a href="demo/demo_addressbook.aspx?LNG=CA"><img alt="Catalan" class="IconFlag" src="pixevo/flags/ca.gif" />Catalan</a></nobr>
<nobr><a href="demo/demo_addressbook.aspx?LNG=ZH"><img alt="Chinese" class="IconFlag" src="pixevo/flags/zh.gif" />Chinese</a></nobr> 
<nobr><a href="demo/demo_addressbook.aspx?LNG=DA"><img alt="Danish" class="IconFlag" src="pixevo/flags/da.gif" />Danish</a></nobr> 
<nobr><a href="demo/demo_addressbook.aspx"><img alt="English" src="pixevo/flags/en.gif" class="IconFlag"/>English</a></nobr>
<nobr><a href="demo/demo_addressbook.aspx?LNG=FR"><img alt="French" class="IconFlag" src="pixevo/flags/fr.gif" />French</a></nobr> 
<nobr><a href="demo/demo_addressbook.aspx?LNG=DE"><img alt="German" class="IconFlag" src="pixevo/flags/de.gif" />German</a></nobr>
<nobr><a href="demo/demo_addressbook.aspx?LNG=HI"><img alt="Hindi" class="IconFlag" src="pixevo/flags/hi.gif" />Hindi</a></nobr>
<nobr><a href="demo/demo_addressbook.aspx?LNG=IT"><img alt="Italian" class="IconFlag" src="pixevo/flags/it.gif" />Italian</a></nobr> 
<nobr><a href="demo/demo_addressbook.aspx?LNG=JP"><img alt="Japanese" class="IconFlag" src="pixevo/flags/jp.gif"/>Japanese</a></nobr>
<nobr><a href="demo/demo_addressbook.aspx?LNG=FA"><img alt="Persian" class="IconFlag" src="pixevo/flags/fa.gif" />Persian</a></nobr>
<nobr><a href="demo/demo_addressbook.aspx?LNG=PT"><img alt="Portuguese" class="IconFlag" src="pixevo/flags/pt.gif" />Portuguese</a></nobr> 
<nobr><a href="demo/demo_addressbook.aspx?LNG=RO"><img alt="Romanian" class="IconFlag" src="pixevo/flags/ro.gif" />Romanian</a></nobr> 
<nobr><a href="demo/demo_addressbook.aspx?LNG=ES"><img alt="Spanish" class="IconFlag" src="pixevo/flags/es.gif" />Spanish</a></nobr>
<nobr><a href="demo/demo_addressbook.aspx?LNG=TR"><img alt="Turkish" class="IconFlag" src="pixevo/flags/tr.gif" />Turkish</a></nobr>
 </p>
		 
			</td></tr>
			</table>
			  
		</div>
			

			
		<div class="dft">			
			 <a href="demo/demo__techno.aspx"><h2>Feature specific demos</h2></a>
  		
		
			<p>
				<a href="demo/dev_RTF.aspx">Rich Text editor</a>,
<a href="demo/dev_Dependent_fields.aspx">Dependent fields</a>, 
<a href="demo/dev_Permissions.aspx">Permissions</a>,
<a href="demo/dev_Validation.aspx">Custom Validation</a>, 
<a href="demo/dev_Navigation.aspx">Navigation</a>, 
<a href="demo/dev_Localization.aspx">Localization</a>, 
<a href="demo/dev_Events.aspx">Server Events</a>, and
<a href="demo/demo_db.aspx"><img alt="" class="Icon16" src="PixEvo/db.gif" width="16" />Database structure</a>.
 
			</p>
			</div>			
			
		</td> 
	</tr>
</table>


</asp:Content>

