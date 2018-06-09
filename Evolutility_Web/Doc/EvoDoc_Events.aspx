<%@ Page AutoEventWireup="true" CodeFile="Doc.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="EvoDoc" Language="C#" MasterPageFile="zmDoc.master" Menus="doc"
	Meta_Description="Evolutility Documentation: Events" Meta_Keywords="CRUD MVC ORM properties ASP.net Microsoft.net SQL server"
	SubMenuID="52" Title="Evolutility :: Documentation : Events" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1><img src="pix/dn_event.gif" class="icon" alt="" />Web Control Events</h1>

	<p>Evolutility provides 2 server events:</p>
	
	<ul class="noindent">
	<li><STRONG>DBChange</STRONG> raised when a record is 
		Inserted, Updated or Deleted.</li>
	<li><strong>CredentialChange</strong> raised when the user logs in or out.</li>
	</ul> 
	
<h2>DBChange</h2>

	<p>DatabaseEventArgs: </p> 
	<ul>
	<li>Action (DBAction) action performed by the database (Insert, Update, or Delete).</li>
	<li>ID - (Integer) record primary key. </li>
	</ul>
	
	<p>Custom code in the page nesting the control can be triggered by the web control 
		doing an action to the database. Here is a code sample to display the last 
		database action in a label.</p>	
					
<p>C#<br /><div class="code">			
		protected void Evo1_DBChange(object sender, Evolutility.WebControls.UIServer.DatabaseEventArgs e)<br />
		{<div style="margin-left:20px;">
			Label1.Text = string.Format("DB: {0} #{1}", e.Action.ToString(), e.ID);<br />
		</div> }
	</div></p>
	
<p>VB.net<br /><div class="code">
		Protected Sub Evo1_DBChange(ByVal sender As Object, ByVal e As Evolutility.UIServer.DatabaseEventArgs) Handles Evo1.DBChange<br />
		<div style="margin-left: 20px;">
			Label1.Text = "DB: " & e.Action.ToString & " #" & e.ID<br />
		</div>
		End Sub
	</div>
</p>

<h2>CredentialChange</h2>

	<p>CredentialEventArgs: </p>
	<ul> 
		<li>Action (CredentialAction)</li>
		<li>UserID (Integer) </li>
		<li>UserName (String) </li>
		<li>DBApplicationKey (String) </li>
		<li>Description (String) </li> 
	</ul>
	<p>
		Custom code in the page nesting the control can be triggered by the user logs in or out. 
		Here is a code sample to log these in a label.</p>
		
<p>C#<br /><div class="code">
		Evo1_CredentialChange(object sender, Evolutility.WebControls.UIServer.CredentialEventArgs e)<br />
		{<div style="margin-left: 20px;">
			Label1.Text = string.Format("Cred.: {0} #{1} {2}", e.Action, e.UserID, e.UserName);<br />
		</div> }
			</div></p>
			
<p>VB.net<br /><div class="code">
		Protected Sub Evo1_CredentialChange(ByVal sender As Object, ByVal e As Evolutility.UIServer.CredentialEventArgs) Handles Evo1.CredentialChange<br />
		<div style="margin-left: 20px;">
			Label1.Text = "Cred.: " & e.Action.ToString & " #" & e.UserID & " " & e.UserName<br />
		</div>
		End Sub
	</div>
</p>

	<P></P>
	<P>See a <a href="../demo/dev_events.aspx">demo of this feature</a>.</P>
</asp:Content>