<%@ Page Language="C#" MasterPageFile="zmEvoDico.master" AutoEventWireup="true" CodeFile="EvoDico.aspx.cs" Inherits="EvoDicoWiz" 
CodeFileBaseClass="BasePage" 
Title="EvoDico :: App Wizard"  
Menus="evodico"
SubMenuID="0"
%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h1>Custom Applications</h1>
	<p>Add new custom applications 
	<center>
<table width=80%> 
<tr valign="top">
	<td width="160">

		<div onmouseout="javascript:this.className=''" onmouseover="javascript:this.className='LightGrey'"
			style="padding: 3 10 10 10"> 		
	<h3>
		<img class="icon" src="../pixevo/wand.png">Wizards</h3>
			<p>
	<a href="EvoDicoWiz.aspx?WIZ=build">Build a new app</a><br />
	<a href="EvoDicoWiz.aspx?WIZ=setup">Install apps</a><br />
	<a href="EvoDicoWiz.aspx?WIZ=dbscan">Map DB table</a><br />
	<a href="EvoDicoWiz.aspx?WIZ=xml2db">Import XML</a><br />
			&nbsp;
			
			</p>
		</div> 
</td><td width="130">
		<div onmouseout="javascript:this.className=''" onmouseover="javascript:this.className='LightGrey'"
			style="padding: 3 10 10 10"> 		
			
		<h3>
			<img class="icon" src="../pixevo/edi_frm_edit.png">Designer</h3>
		<p>
		<a href="EvoDicoForm.aspx"><img src="../pixevo/edi_frm.png" class="icon">Forms</a><br />
		<a href="EvoDicoPanel.aspx"><img src="../pixevo/edi_pnl.png" class="icon">Panels</a><br />
		<a href="EvoDicoField.aspx"><img src="../pixevo/edi_fld.png" class="icon">Fields</a><br />
		</p>	
	
		</div> 
</td><td width="150">
		<div onmouseout="javascript:this.className=''" onmouseover="javascript:this.className='LightGrey'"
			style="padding: 3 10 10 10"> 	
	<h3>
		<nobr><img class="icon" src="../pixevo/book_open.png">Design Docs</nobr></h3>
			<p>
	<a href="EvoDoc.aspx"><img class="icon" src="../pixevo/md.gif">Applications</a><br />
	<a href="EvoDoc_db.aspx"><img src="../pixevo/db.gif" class="icon">Database</a><br />
	<a href="EvoDoc_Export.aspx"><img src="../pixevo/db_go.png" class="icon">Export</a><br />	&nbsp;
			
			</p>
		</div>  

</td></tr></table> 	
		</center>
		 
	<p>The metadata is now stored directly in the database. This way you can directly use Evolutility to change manipulate applications definitions. 
		The application updates itself real time and there is no more need for XML files.
	</p>

<table><tr><td><nobr>Download the latest version of Evolutility at </nobr> 
</td><td><a href="http://sourceforge.net/projects/evolutility" target="new"><img src="http://sflogo.sourceforge.net/sflogo.php?group_id=225915&type=12" width="120" height="30" border="0" alt="Get Evolutility at SourceForge.net" /></a>
</td></tr></table>
 

</asp:Content>

