<%@ Page Language="C#" MasterPageFile="zmDemo.master" AutoEventWireup="true" CodeFile="demo.aspx.cs" Inherits="demo" 
    Meta_Description="Evolutility :: Demo Applications" 
    Meta_Keywords="open source metadata web application CRUD ORM ASP.net SQL server"

CodeFileBaseClass="BasePage" 
Title="Evolutility :: Demos" 
Menus="demo"
SubMenuID="0"

%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1>Demo applications</h1>

<p>These sample applications are not anything you haven't seen before. 
The cool thing is that these are easy to customize and integrate into your web site. 
Evolutility can also be used to map you existing database tables and share them on your intranet, or the whole web.
</p> 


<TABLE class="groupedColumns">
	<TBODY>
		<TR>
			<TD vAlign="top" width="50%">
			
			
<div class="dft">			
			
			
			 <a href="demo__simple.aspx"><h2>Small &amp; Simple applications</h2></a>
 
 <p>The classic simple apps:</p>
 
			<table border="0" >
					<tr valign="top">
					<td>
						<ul class="indent">
							<li><nobr><a href="demo_addressbook.aspx">
								<img class="Icon" alt=""  src="../PixEvo/contact.gif"/>Address book</a></nobr></li> 
							<li><a href="demo_todo.aspx">
								<img class="Icon" alt=""  src="../PixEvo/todo.gif"/>To do</a></li>
							<li><a href="demo_memopad.aspx">
								<img class="Icon" alt=""  src="../PixEvo/memo.gif"/>Memo pad</a></li>
							<li><a href="demo_restaurant.aspx">
								<img alt="" class="Icon" src="../PixEvo/favourites.gif" />Bookmarks</a></li>
						</ul>
					</td>
					<td>&nbsp;&nbsp;</td>
					<td>
						<ul class="indent">
								<li><a href="demo_winecellar.aspx">
									<img alt="" class="Icon" src="../PixEvo/wine.gif" />Wine Cellar</a></li>
								<li>
									<nobr>
										<a href="demo_photo.aspx">
											<img alt="" class="Icon" src="../PixEvo/photo.gif" />Photo album</a></nobr></li>
								<li><a href="demo_restaurant.aspx">
									<img alt="" class="Icon" src="../PixEvo/resto.gif" />Restaurants</a></li>
						</ul>
					</td>
				<td></td></tr></table>
				 
				
				 Tired of blue ? <a href="skin_green.aspx">try green !</a>
				 <br />&nbsp;
			</div>	
			
								
<div class="dft">			
					<a href="demo__security.aspx"><h2>Multi-user environment</h2></a>
					<p>Evolutility can be setup for single- or multiple-user environments for 
					collaboration (sharing records and comments) or with row-level security.
					</p> 
						<ul class="indent">
								<li><a href="demo_addressbook_sharing.aspx">
									<img class="Icon" height="16" width="16" alt="" src="../PixEvo/contactShared.gif"/>Collaboration</a> 
						 </li>
								<li><a href="demo_addressbook_rls.aspx">
									<img  class="Icon" height="16" width="16" alt="" src="../PixEvo/contactRLS.gif"/>Row level security</a>
							  </li>
						</ul>		 
				</div>
				
							 
			
			</TD> 
			<TD vAlign="top" width="50%">
				

					
<div class="dft">			
				<a href="demo__techno.aspx"><h2>Technical demos</h2></a>
                  
				<p>More technical demos showing specific features:
				</p> 
					<ul class="indent">
                        <li><a href="demo_todo.aspx?MODE=charts">Charts</a></li>
						<li><a href="dev_RTF.aspx">Rich Text Editor</a></li>
						 <li><a href="dev_Permissions.aspx">Permissions</a></li>
						 <li><a href="dev_Localization.aspx?LNG=FR">14 languages</a>
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
						<li><a href="dev_Dependent_fields.aspx">Dependent fields</a></li>
						<li><a href="dev_Validation.aspx">Custom Validation</a></li>
						<li><a href="dev_Navigation.aspx">Navigation</a></li>
						<li><a href="dev_Events.aspx">Server Events</a></li> 
					</ul>   
                    
					 </div>
					 
			</TD>
		</TR>
	</TBODY>
</TABLE>  

<p>In addition, because these are built on top of a standard and robust technologie stack (Microsoft .NET Framework and SQL Server), 
they can be hosted anywhere and scale to thousands of records and hundreds of users.
</p>

</asp:Content>

