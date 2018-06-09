<%@ Page AutoEventWireup="true" CodeFile="demo.aspx.cs" 
CodeFileBaseClass="BasePage" Inherits="demo" 
	Language="C#" MasterPageFile="zmDemo.master" Menus="demo_dev"
	Meta_Description="Evolutility Demo custom form validation" SubMenuID="3340" Title="Evolutility :: Demo : Custom Validation"
	ValidateRequest="false" %>
<%@ Register Assembly="Evolutility.UIServer" Namespace="Evolutility" TagPrefix="EVOL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

<h1>Custom validation... on the Addressbook</h1>

<p>In this example, we added some custom validation to the <nobr><a href="demo_addressbook.aspx">
 Address book demo</a></nobr>.
<ul>
	<li>Only company name starting with "E" are allowed.</li>
	<li>Category cannot be equal to "Hobby".</li>
	<li>At least one of the 2 fields "Home phone" or "Mobile" must have a value.</li>
	<li>City can only be "San Francisco", "Paris", or empty.</li>
</ul></p>

<script language="javascript" type="text/javascript">
function StartWithE(fID,fLabel){
	var v=e$(fID).value;
	if(v.length>0&&v.substr(0,1)!="E")
		return 'The field "'+fLabel+'" must start with "E".';
}
function TelHome(){
	if(e$("EVOLU_phoneh").value==""&&e$("EVOLU_phonem").value=="")
		return 'At least one of "Home phone" or "Mobile" must have a value.';
}
function NoHobby(fID,fLabel){
	var f=e$(fID);
	if(f.options[f.selectedIndex].value=="2")
		return 'No hobby allowed for the field "'+fLabel+'".';
}
function ParisOrSF(fID,fLabel){
	var v=e$(fID).value.toLowerCase();
	if(v.length>0 && v!="paris" && v!="san francisco")
		return 'The field "'+fLabel+'" can only be "San Francisco" or "Paris.';
}
</script>

	<EVOL:UIServer id="Evo1" runat="server" CssClass="main1" BackColor="#EDEDED" Language="EN" DisplayModeStart="NewItem"
	BackColorRowMouseOver="Beige"  XMLfile="xml/Addressbook_CustVal.xml" VirtualPathToolbar="../PixEvo/"
	RowsPerPage="20" Width="100%" ToolbarPosition="top"  NavigationLinks="true" DBAllowSelections="true"
	SecurityModel="Single_User" DBAllowExport="True"   />

<p>Learn more <a href="../doc/EvoDoc_Validation.aspx">about custom validation</a> in the documentation.</p>
</asp:Content>

