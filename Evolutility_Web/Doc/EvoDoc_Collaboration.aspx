<%@ Page AutoEventWireup="true" CodeFile="Doc.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="EvoDoc" Language="C#" MasterPageFile="zmDoc.master" 
	Meta_Description="Evolutility documentation: Collaboration" Meta_Keywords="documentation"
	Menus="doc" SubMenuID="48" Title="Evolutility :: Documentation : Collaboration and user comments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1>Collaboration and user comments</h1>
 
	<p>Any application component can be set to allow users to post comments on any record. </p>
	
	
	<p>This is done setting Evolutility web control property <span class="ctrProp">UserComments</span> to one of the following values: 
		<ul>
			<li>None</li>
			<li>Read only</li>
			<li>Logged Users</li>
			<li>Anonymous</li>
		</ul> 
	</p> 
 
	<h2>Database pre-requisite</h2>
	
	<p>The database table must have a column "CommentCount" (of type int) to support comments.</p>
	
	<p>By default, comments are 
		saved in the database table “Evol_Comment” but another table can be specified 
		using the attribute <span class="xmlAttr">dbtablecomments</span> in the XML. Other specifications are 
		possible with <span class="xmlAttr">dbcommentsformid</span> and <span class="xmlAttr">dbcommentsuserpage</span>.</p>
	
	<p>More information on <a href="EvoDoc_DB.aspx">Database pre-requisites for Evolutility</a>.</p>
	
	<h2>User Interface</h2>
	
	<p>Letting users comment on records affects the application UI and behavior on several modes: View, List, and Search.</p>
	
	 
	<h3>View</h3>
	<p>Comments on record (if any) are displayed on the bottom of the page with a text 
		box to post new comments:</p>
		
	<p style="margin-left:30px;"><img src="pix/ui/c-view.gif" alt="" class="shadow" /></p>
 

	<h3>List</h3>		 
	<p>Records with comments are flagged.</p>
	<p style="margin-left:30px;"><img alt="" class="shadow" src="pix/ui/c-list.gif" /></p> 

	<h3>Search & Advanced search</h3>
	<p>Records can be filtered by comments.</p>
	<p style="margin-left: 30px;">
		<img alt="" class="shadow" src="pix/ui/c-search.gif" style="border: solid 1 #c0c0c0" /></p>
 

	 
	<P>See a <a href="../demo/demo_addressbook_sharing.aspx">demo of this feature</a>.</P>
	
</asp:Content>