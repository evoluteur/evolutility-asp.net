<%@ Page AutoEventWireup="true" CodeFile="demo.aspx.cs" Inherits="demo" Language="C#"
    MasterPageFile="zmDemo.master" Title="Evolutility :: Security Demo" 
CodeFileBaseClass="BasePage"  
Menus="demo_mu"
SubMenuID="12"
%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Multi-user Demos</h1>

<p>These demos demonstrate some of Evolutility build-in security models. 
	Here you can try our addressbook setup for multi-users environments:&nbsp;
</p>

<div style="margin-left:20px">
<p><a href="demo_addressbook_sharing.aspx">
    <img border="0" height="16" hspace="8" src="../PixEvo/contactshared.gif" width="16">Multi-user sharing data and comments</a>
  <div style="margin-left: 30px">
	Every user sees all data but can only edit records he/she created. In addition, users can post comments on all records.
	</div>
</p>        
        
<p><a href="demo_addressbook_rls.aspx">
    <img border="0" height="16" hspace="8" src="../PixEvo/contactrls.gif" width="16">Multi-user with row level security</a>
	<div style="margin-left: 30px">
    Each user only sees and edit his/her own data.
	</div> 	
</p>
</div>

<p>For both of these demos, use <b>John/John</b>
	or <b>Mary/Mary</b> as your login/password.</p>

</asp:Content>

