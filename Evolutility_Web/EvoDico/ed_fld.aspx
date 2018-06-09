<%@ Page AutoEventWireup="false" validateRequest="false" %> 
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %> 
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Fields Definition</title>
		<link href="../evol.css" rel="stylesheet"/>
		<meta content="Evolutility" name="GENERATOR"/> 
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<meta content="NO-CACHE" http-equiv="CACHE-CONTROL"/>
		
		<script src="../PixEvo/JS/EvoDicoRules.js" type="text/javascript"></script> 
		
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
				<EVOL:UIServer id="Evo1" runat="server"  DataIsMetadata=true
					DBAllowInsert="True"
					DBAllowDelete="True" 
					DBAllowSelections="false"
					NavigationLinks="False" 
					SecurityModel="Single_User_Password" SecurityKey="EvoDico"
					XMLfile="XML/ed_field.xml" 
					UserComments="None" 
					DisplayModeStart="Edit" 
					VirtualPathToolbar="../pixevo/" VirtualPathPictures="../pixevo/"
					BackColor="#EDEDED" BackColorRowMouseOver="Beige" height="100%" width="100%" 
					ToolbarPosition="None" RowsPerPage="50" ShowTitle="false"
					></EVOL:UIServer>
		</form>
	</body>
</html>
