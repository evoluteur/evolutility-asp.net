<%@ Page Language="C#" MasterPageFile="zmEvoDico.master" AutoEventWireup="true" CodeFile="EvoUser.aspx.cs" Inherits="EvoDicoWiz" 
CodeFileBaseClass="BasePage" 
Title="Users Management"  
Menus="evodico" 
SubMenuID="150"
%>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<h1><img src="../pixevo/user.gif" class="icon" alt=""/> Users</h1>
			<p>
				<EVOL:UIServer id="UIServer1" runat="server" DataIsMetadata=true
					DBAllowInsert="true"
					DBAllowDelete="true" 
					DBAllowInsertDetails="true" 
					DBAllowUpdateDetails="true" 
					DBAllowSelections="false" 
					DBAllowExport="true" 
					DBAllowHelp="true"  
					SecurityModel="Single_User_Password" SecurityKey="EvoDico"
					XMLfile="XML/EvoDico_user.xml"
					VirtualPathToolbar="../pixevo/"
					VirtualPathPictures="../pixevo/"  
					BackColorRowMouseOver="Beige" BackColor="#EDEDED" 
					ToolbarPosition="Top" RowsPerPage="20" ShowTitle="true" height="100%" width="100%"
					DisplayModeStart="List" UserComments="None"></EVOL:UIServer>
			</p>
	  
</asp:Content>

