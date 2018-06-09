<%@ Page AutoEventWireup="false" %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Search Definition</title>
		<link href="../Evol.css" rel="stylesheet"/>
		<meta content="Evolutility" name="GENERATOR"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<meta content="NO-CACHE" http-equiv="CACHE-CONTROL"/>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<small>
				<EVOL:UIServer id="evo1" runat="server"  DataIsMetadata=true
				height="100%" width="100%" 
					DBAllowInsert="False"
					DBAllowDelete="False" 
					DBAllowupdatedetails="True"
					DBAllowSelections="false" 
					SecurityModel="Single_User_Password" SecurityKey="EvoDico"
					UserComments="None" 
					XMLfile="XML/ed_search.xml"
					VirtualPathToolbar="../pixevo/" VirtualPathPictures="../pixevo/"
					ToolbarPosition="None" RowsPerPage="50" ShowTitle="False" NavigationLinks="False" DisplayModeStart="Edit" 
					DBAllowHelp="True" CollapsiblePanels="False" BackColorRowMouseOver="Beige" ></EVOL:UIServer></small>
		</form>
	</body>
</html>
