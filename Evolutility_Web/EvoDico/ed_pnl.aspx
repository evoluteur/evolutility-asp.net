<%@ Page AutoEventWireup="false" validateRequest="false" %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Panel Definition</title>
		<link href="../Evol.css" rel="stylesheet"/>
		<meta content="Evolutility" name="GENERATOR"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<meta content="NO-CACHE" http-equiv="CACHE-CONTROL"/>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<EVOL:UIServer id="evo1" runat="server"  DataIsMetadata=true
				height="100%" width="100%" 
				DBAllowInsert="False" 
				dballowinsertdetails="True" 
				dballowupdatedetails="True" 
				DBAllowSelections="false"
				DBAllowDelete="False"
				SecurityModel="Single_User_Password" SecurityKey="EvoDico"
				XMLfile="XML/ed_panel.xml"
				VirtualPathToolbar="../pixevo/" BackColor="#EDEDED" ToolbarPosition="None"
				RowsPerPage="50" ShowTitle="False"
				UserComments="None" BackColorRowMouseOver="Beige"  NavigationLinks="False" DisplayModeStart="Edit" 
				DBAllowHelp="True" VirtualPathPictures="../pixevo/" CollapsiblePanels="False"></EVOL:UIServer>
		</form>
	</body>
</html>
