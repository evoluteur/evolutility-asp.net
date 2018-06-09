<%@ Page AutoEventWireup="true" CodeFile="demo.aspx.cs" Inherits="demo" Language="C#"
    MasterPageFile="zmDemo.master" Title="Evolutility :: Demo : Sample applications" 
CodeFileBaseClass="BasePage"  
Menus="demos"
SubMenuID="800"
%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

		<h1>Sample applications</h1>
		
<p>These sample applications are not anything you haven't seen before. 
The cool thing is that <b>these were built in only a few minutes each</b>, and can be customized as easily.
 </p> 


<table id="table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
	<TBODY>
		<TR>
			<TD vAlign="top" width="49%"> 
				<P>Simple CRUD applications:
				</P>
				

			<ul class="indent">
				<li><nobr><a href="demo_addressbook.aspx">
					<img class="Icon" alt=""  src="../pixevo/contact.gif"/>Address book</a></nobr></li> 
				<li><a href="demo_todo.aspx">
					<img class="Icon" alt=""  src="../pixevo/todo.gif"/>To do</a></li>
				<li><a href="demo_bookmark.aspx">
					<img alt="" class="Icon" src="../PixEvo/favourites.gif" />Bookmarks</a></li>
				<li><a href="demo_memopad.aspx">
					<img class="Icon" alt=""  src="../pixevo/memo.gif"/>Memo pad</a></li>
				<li><nobr><a href="demo_photo.aspx">
					<img class="Icon" alt=""  src="../PixEvo/photo.gif"/>Photo album</a></nobr></li> 
				<li><a href="demo_winecellar.aspx">
					<img alt="" class="Icon" src="../pixevo/wine.gif"/>Wine Cellar</a></li>
				<li><a href="demo_restaurant.aspx">
					<img alt="" class="Icon" src="../pixevo/resto.gif"/>Restaurants</a></li>
 			</ul>
				
	 
                    <p>More advanced example querying SQL Server dictionary to document the database structure:</p>

                <p style="margin-left:20px">
						<A href="demo_db.aspx"><img src="../PixEvo/db.gif" class="Icon">DB 
							Design Documents</A><br />
						<br />
                    </p>

			</TD>
			<TD>&nbsp;&nbsp;</TD>
			<TD vAlign="top" width="49%"> 
                <p>Applications <a href="../doc/ED_Metamodel.aspx">metadata</a> in XML:</p>

				<ul class="indent">
				<li><A href="xml/PIM/addressbook_EN.xml" target="xml"><img src="../PixEvo/tag.png" class="Icon" alt="">Address 
							book</A></li> 
						<li><A href="xml/PIM/todo.xml" target="xml"><img src="../PixEvo/tag.png" class="Icon">To 
							do</A></li>
					<li><a href="xml/PIM/bookmark.xml" target="xml">
						<img class="Icon" src="../PixEvo/tag.png">Bookmark</a></li>
						<li><A href="xml/PIM/memo.xml" target="xml"><img src="../PixEvo/tag.png" class="Icon">Memo 
							pad</A></li>
						<li><a href="xml/PIM/photo.xml" target="xml">
							<img alt="" class="Icon" src="../PixEvo/tag.png">Photo album</a></li>
						<li><a href="xml/winecellar/winecellar.xml" target="xml">
							<img alt="" class="Icon" src="../PixEvo/tag.png">Wine Cellar</a></li>
						<li><A href="xml/PIM/restaurant.xml" target="xml"><img alt="" src="../PixEvo/tag.png" class="Icon">Restaurants</A></li>
						<li><a href="xml/dbobjects.xml" target="xml"><img class="Icon" src="../PixEvo/tag.png">DB Design
							Documents</a></li> 
					</ul>
					
					<p>
					<p>Localised address book:<br />
						<p style="margin-left: 20px">
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
					 </p>
 						</TD>
					</TR>
				</TBODY>
			</table>  
			
<p>Because these applications are built on top of a standard and robust technologie stack (Microsoft .NET Framework and SQL Server), 
they can be hosted anywhere and scale to thousands of records and hundreds of users.
</p> 
</asp:Content>

