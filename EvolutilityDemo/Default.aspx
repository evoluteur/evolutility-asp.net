<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/zmSimple.master"  Title="Evolutility demo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<h1>Metadata driven CRUD applications</h1>
		<p>
		These sample applications are different instances of the same web control, using 
			different XML definitions to adapt to different database structures. 
		</P>  
 
			<table id="table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TBODY>
					<TR>
						<TD vAlign="top" width="49%"> 
							<P>Simple CRUD applications using Evolutility to build the HTML, Javascript, and SQL at run-time:
							</P>
							<blockquote>
								<P><A href="demo_addressbook.aspx"><img height="16" src="PixEvo/m-address.gif" width="16" class="icon">Address 
										book</A><br />
									<A href="demo_todo.aspx"><img height="16" src="PixEvo/m-todo.gif" width="16" class="icon">To 
										do</A><br />
									<A href="demo_memopad.aspx"><img height="16" src="PixEvo/memo.gif" width="16" class="icon">Memo 
										pad</A><br />
									<A href="demo_restaurant.aspx"><img height="16" alt="Restaurants" src="PixEvo/resto.gif" width="16" class="icon">Restaurants</A><br />
									
									<a href="demo_winecellar.aspx"><img alt="" class="icon" src="pixevo/m-wine.gif"/>Wine Cellar</a><br />
									<A href="demo_braille.aspx"><img height="16" alt="Braille Resources" src="PixEvo/m-brl.gif" width="16" class="icon">Braille Resources</A><br />
								</P>
                            </blockquote>
                                <p>More advanced example querying SQL Server dictionary to document the database structure:</p>
                            <blockquote>    
                            <p>
									<A href="demo_db.aspx"><img height="16" src="PixEvo/m-db.gif" width="16" class="icon">DB 
										Design Documents</A><br />
									<br />
                                </p>
							</blockquote>
						<p>More demo applications are available at <a href="http://www.evolutility.org/demo/demo.aspx" target="evol">Evolutility.org</a>.</p>	
						</TD>
						<TD>&nbsp;&nbsp;</TD>
						<TD vAlign="top" width="49%"> 
                            <p>XML files for the application definitions:</p>
                           <blockquote>
								<P><A href="xml/addressbook.xml" target="xml"><img height="16" src="PixEvo/dm-xml.gif" width="16" class="icon" alt="">Address 
										book</A><br />
									<A href="xml/todo.xml" target="xml"><img height="16" src="PixEvo/dm-xml.gif" width="16" class="icon">To 
										do</A><br />
									<A href="xml/memo.xml" target="xml"><img height="16" src="PixEvo/dm-xml.gif" width="16" class="icon">Memo 
										pad</A><br />
									<A href="xml/restaurant.xml" target="xml"><img height="16" alt="" src="PixEvo/dm-xml.gif" width="16" class="icon">Restaurants</A><br />
									<a href="xml/winecellar.xml" target="xml">
										<img alt="" class="icon" height="16" src="PixEvo/dm-xml.gif" width="16">Wine Cellar</a><br />
									<A href="xml/resource4blind.xml" target="xml"><img height="16" alt="Braille Resources" src="PixEvo/dm-xml.gif" width="16"
											class="icon">Braille Resources</A><br />
									<a href="xml/dbobjects.xml" target="xml"><img class="icon" height="16" src="PixEvo/dm-xml.gif" width="16">DB Design
										Documents</a><br />
								</P>
                            </blockquote> 
                            <p>More information on this metadata is available at <a href="http://www.codeproject.com/KB/codegen/Metamodel_for_CRUD.aspx" target="cp">CodeProject.com</a>.</p>
						</TD>
					</TR>
				</TBODY>
			</table>  
</asp:Content>