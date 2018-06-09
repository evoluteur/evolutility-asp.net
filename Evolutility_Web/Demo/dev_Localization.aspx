<%@ Page AutoEventWireup="true" CodeFile="dev_Localization.aspx.cs" CodeFileBaseClass="BasePage"
	Inherits="localiz" Language="C#" MasterPageFile="zmDemo.master" Menus="demo_dev"
	Meta_Description="Evolutility :: Demo Addressbook localized in English, French, Japanese, Portuguese, Spanish, Catalan, Danish, Romanian"
	Meta_Keywords="open source multi-lingual language localized localization translation English, French, Japanese, Portuguese, Spanish, Catalan, Danish, Romanian"
	SubMenuID="3330" Title="Evolutility :: Localization" ValidateRequest="false" %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table style="width:100%"><tr><td>
	<h1><img src="../PixEvo/contact.gif" class="icon" alt=""/>
	 <asp:Label ID="Label1" runat="server" Text=""></asp:Label></h1>
</td><td align="right">
	<a href="dev_Localization.aspx"><img src="../pixevo/flags/en.gif" class="icon" alt="English"/></a> 
	<a href="dev_Localization.aspx?LNG=ZH"><img alt="Chinese" class="icon" src="../pixevo/flags/zh.gif" /></a>
	<a href="dev_Localization.aspx?LNG=ES"><img alt="Spanish" class="icon" src="../pixevo/flags/es.gif" /></a>
	<a href="dev_Localization.aspx?LNG=FR"><img alt="French" class="icon" src="../pixevo/flags/fr.gif" /></a> 
	<a href="dev_Localization.aspx?LNG=DE"><img alt="German" class="icon" src="../pixevo/flags/de.gif" /></a>
	<a href="dev_Localization.aspx?LNG=HI"><img alt="Hindi" class="icon" src="../pixevo/flags/hi.gif" /></a>
	<a href="dev_Localization.aspx?LNG=JP"><img alt="Japanese" class="icon" src="../pixevo/flags/jp.gif"/></a>
	<a href="dev_Localization.aspx?LNG=IT"><img alt="Italian" class="icon" src="../pixevo/flags/it.gif" /></a> 
	<a href="dev_Localization.aspx?LNG=PT"><img alt="Portuguese" class="icon" src="../pixevo/flags/pt.gif" /></a>
	<a href="dev_Localization.aspx?LNG=DA"><img alt="Danish" class="icon" src="../pixevo/flags/da.gif" /></a>
	<a href="dev_Localization.aspx?LNG=TR"><img alt="Turkish" class="icon" src="../pixevo/flags/tr.gif" /></a>
	<a href="dev_Localization.aspx?LNG=RO"><img alt="Romanian" class="icon" src="../pixevo/flags/ro.gif" /></a>
	<a href="dev_Localization.aspx?LNG=CA"><img alt="Catalan" class="icon" src="../pixevo/flags/ca.gif" /></a>
</td></tr></table> 
			
		<EVOL:UIServer id="Evo1" runat="server" VirtualPathPictures="../pix/"
				 ToolbarPosition="Top" Width="100%" CssClass="main1"  
				 SqlConnection="" VirtualPathToolbar="../PixEvo/" Language="FR"  
				 BackColor="#EDEDED" BackColorRowMouseOver="Beige" UserComments="None"
				XMLfile="XML/PIM/addressbook_FR.xml" DBAllowSelections="false" SecurityModel="Single_User"
				DBAllowDelete="true" DisplayModeStart="List" ShowTitle="true" DBAllowExport="True" RowsPerPage="20" />
					
<p><a href="dev_LocXML.html" target="mdxml"><img src="../PixEvo/tag.png" class="icon" alt=""/> XML definition of this application</a>.</p>


</asp:Content>

