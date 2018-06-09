<%@ Page AutoEventWireup="false" validateRequest="false"   %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
		<title>Form Definition</title>
		<link href="../Evol.css" rel="stylesheet"/>
		<meta content="Evolutility" name="GENERATOR"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<meta content="NO-CACHE" http-equiv="CACHE-CONTROL"/>
  </head>
	<body>
		<form id="Form1" method="post" runat="server">
				<EVOL:UIServer id="Evo1" runat="server" DataIsMetadata=true
					DBAllowDelete="False" 
					DBAllowInsertDetails="true" 
					DBAllowUpdateDetails="true" 
					DBAllowSelections="false" 
					DBAllowInsert="False"
					SecurityModel="Single_User_Password" SecurityKey="EvoDico"
					UserComments="None"
					XMLfile="XML/ed_form.xml"
					VirtualPathToolbar="../pixevo/"
					VirtualPathPictures="../pixevo/" 
					BackColorRowMouseOver="Beige" BackColor="#EDEDED" 
					ToolbarPosition="None" RowsPerPage="50" ShowTitle="true" height="100%" width="100%"
					NavigationLinks="False" DisplayModeStart="Edit"></EVOL:UIServer>
		</form>
	</body>
</html>
