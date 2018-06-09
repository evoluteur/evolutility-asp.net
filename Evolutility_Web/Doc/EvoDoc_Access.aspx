<%@ Page AutoEventWireup="true" CodeFile="Doc.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="EvoDoc" Language="C#" MasterPageFile="zmDoc.master" Menus="doc"
	Meta_Description="Evolutility documentation: Security and users access management"
	Meta_Keywords="documentation" SubMenuID="46" Title="Evolutility :: Documentation : Security and users access management" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1>Security and users access management</h1>

<p>Evolutility web control has built-in features for user 
identification, row level security, collaboration, and user comments. You may 
use them or bypass them with your own custom code in the page nesting the 
control. 
 </p>


<h2>Evolutility Security models</h2> 

		<p>The <span class="ctrProp">SecurityModel</span> 
			property of the web control determines if users need to authenticate and which records they are allowed to
			view, update, or delete.</p>
		 
			<ul>
				<li>Single_User: every user can view or edit everything (no login required)</li>
				<li>Single_User_Password: login and password are required to view or edit data</li>
				<li>Multiple_Users_RLS: every logged-in user only sees and edits his/her own data (row level security)</li>
				<li>Multiple_Users_Sharing: every logged-in user sees all records but can only edit records he/she created</li>
				<li>Multiple_Users_Collaboration: every logged-in user can see and edit all records</li>
			</ul>
 
		<p>In order to use Multiple_Users_RLS, and 
			Multiple_Users_Sharing, the driving table must have a column called "UserID" 
			(of type integer). Multiple_Users_Sharing also need a column called "Publish" 
			(of type bit or integer).</p>
		<p>The <span class="ctrProp">DBReadOnly</span> 
			property can prevent users from editing the record, making the form Read-Only.
		</p>
		<p><span class="ctrProp">DBAllowDelete</span>,
			<span class="ctrProp">DBAllowInsert</span>, and <span class="ctrProp">DBAllowUpdate</span> properties 
			can be used to allow or disallowed database functions like Delete, Insert and 
			Update.
		</p>
		<p>All these properties can be set at runtime 
			in code.</p>
		<p>Note: For Single_User_Password, 
			Multiple_Users_RLS, Multiple_Users_Sharing to work, the driving table must have 
			a column called "UserID" (of type integer). Multiple_Users_Sharing also need a 
			column called "Publish" (of type bit or integer).</p>


<p><b>Notes</b>: At the field level security is handled in the metadata.
 
			Individual Fields can be editable or Read-Only, using the <span class="xmlAttr">readonly</span> attribute of 
			each <a href="ED_Elem_Field.aspx">field</a> element. 
			Fields can be required or optional value (attributes <span class="xmlAttr">required</span> and
			<span class="xmlAttr">optional</span>). In addition, you can choose to have different XML documents all together for 
			different users.</p>


<h2>Using Evolutility users authentication</h2>

		<p>When the SecurityModel is set to any of the models requiring user authentication (Single_User_Password, Multiple_Users_RLS, Multiple_Users_Sharing, or Multiple_Users_Collaboration), 
			Evolutility will prompt users to login the first time the page is displayed.
		</p>
		
		<p><img src="pix/ui/login.gif" style="margin-left: 40px;" class="shadow" /></p>
		<p>The stored procedure for user/password 
			validation can be specified in the XML using the <span class="xmlAttr">splogin</span> attribute 
			of the <a href="ED_Elem_Form.aspx#data">data</a> element.
		</p>
		<p>A sample stored procedure for user 
			identification called "EvoDemoSP_Login" is provided with our software 
			evaluation copy. It can be modified to fit your specific needs.
		</p>
		<p>Sharing login information between different 
			pages: Evolutility forms will share their login information with other 
			Evolutility forms on the same web application which uses the same value for 
			their <span class="ctrProp">DBApplicationID</span> property. This 
			way, users will only log-in once for all (or some) Evolutility forms.</p>
			
			
	<h3>Limiting access to specific fields for specific users</h3> 

		<p>Let’s take an example: two users can look at product information, and one is 
			allowed to see the price of products but the other is not. You can create two
			component definitions (mapping to the same set of database tables), one 
			including the product price, the other not. Then, use custom code in the page 
			to identify the user and bind the Evolutility web control to one definition or 
			the other.</p>

			
<h2>Bypassing Evolutility users authentication</h2>
	
	<p>In situations where users are already logged-in with another system, 
	it is possible to use Evolutility without forcing users to login again.
	
	To bypass Evolutility users authentication, set Evolutility UserID session variable called "EVOLUserID".</p>
	 
	 <p>For example, we can set the user ID to 2 (which should be a valid user in EVOL_User table for comments to work) with the following code:
	 </p>
	 <div class="code indent2">
		Session["EVOLUserID"] = 2;
	</div>	
	<br />&nbsp;
	
<h3>More flexibility with SecurityKey</h3>

	<p>When using Evolutility property SecurityKey, 
	it is possible to change the name of the "UserID" session variable by adding a prefix to it.
	</p>	
		<div class="code indent2">
		Session[SecurityKey+"EVOLUserID"] = 2;
		</div>
	
	<p>Example, Evolutility Dictionary uses SecurityKey="EvoDico", therefore the session variable is "EvoDicoEVOLUserID".
	</p>
		<div class="code indent2">
		Session["EvoDicoEVOLUserID"] = 2;
		</div>
	
	<p>
	should be used to bypass its authentication.</p>
	
<p>By using different prefixes for different applications it makes it possible for users to be logged in to specific applications but not others.</p>
 
	<p>This practice works with all Evolutility SecurityModels.  In addition, to prevent users from logging-out by mistake, the "Logout" button can be removed from the toolbar
	with the "DBAllowLogout" property to the control.</p>
	
	
	
<h2>Managing users</h2>
		<p>Managing users is as simple as any other entity; you just need to use 
			Evolutility to map the users table.</p>
		<p>You can create different pages as follows:</p>
<UL>
			<li>one page for the administrator to manage users
			<li>one page (using SecurityModel=Multiple_Users_RLS) to let each user edit his/her 
			own profile
			<li>one page (using SecurityModel=Multiple_Users_Sharing) to let users browse the 
				profile of other users.</li>
</UL>

		
</asp:Content>