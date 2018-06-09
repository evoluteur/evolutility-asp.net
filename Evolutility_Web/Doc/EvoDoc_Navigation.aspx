<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="Doc.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation : Navigation" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="47"
Meta_Description="Evolutility documentation: Navigation" 
Meta_Keywords="documentation open source navigation"
%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1>Navigation Link</h1> 

	<p>When linking to a page which embeds the Evolutility web control, 
	it is possible to specify parameters in the page URL.
	</p> 

<h2>Choosing a specific record</h2>

<div class="indent3">
	<p>View the record ID=5:<br />
	<span class="xmlAttr">MyPage.aspx?ID=5</span>

	</p>
	
	<p>Edit the record ID=5:<br />
		<span class="xmlAttr">MyPage.aspx?ID=5&MODE=edit</span></p>
</div>		
		
 <h2>Choosing a specific display mode (or view)</h2>
	
	<div class="indent3">
	<p>To open the page in a specific mode, you can do the following:
	</p>
	
	<p>Create a new record:<br />
	<span class="xmlAttr">MyPage.aspx?MODE=new</span></p>
	
	<p>Edit the first record:<br />
	<span class="xmlAttr">MyPage.aspx?MODE=edit</span></p> 

	<p>Search page:<br />
	<span class="xmlAttr">MyPage.aspx?MODE=search</span></p>
	
	<p>Advanced search page:<br />
	<span class="xmlAttr">MyPage.aspx?MODE=searchadv</span></p>
        <p>
            Charts page:<br />
            <span class="xmlAttr">MyPage.aspx?MODE=charts</span></p>
            
        <p>Selections list:<br />
	<span class="xmlAttr">MyPage.aspx?MODE=selections</span></p>
	
	<p>List of all records:<br />
	<span class="xmlAttr">MyPage.aspx?MODE=list</span></p>
	
	<p>Exports all records:<br />
	<span class="xmlAttr">MyPage.aspx?MODE=export</span></p>
</div> 	

	<h2>Choosing a specific query result</h2>
	<div class="indent3">
		<p>List result of the query "amex":<br />
		<span class="xmlAttr">MyPage.aspx?QUERY=amex</span></p>
	</div>
 

	<P>See a <a href="../demo/dev_Navigation.aspx">live demo of this feature</a>.</P>
</asp:Content>