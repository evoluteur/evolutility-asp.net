<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="Doc.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation : Metamodel elements" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="120"
Meta_Description="Evolutility documentation: Meta-model covering UI and database mapping for CRUD applications" 
Meta_Keywords="documentation meta model metamodel meta data metadata CRUD code generation"
%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 

<h1>panel and tab elements</h1>
	 
 
 <p><span class="xmlAttr">panel</span> and	<a href="#tab"><span class="xmlAttr">tab</span></a>  
			can be used to group fields. At least one panels is required; tabs are optional.
 </p>
   
	
 <a name="panel"></a>	
<h2>panel element</h2>

	<p>The <span class="xmlElem">panel</span> element is used to visually group fields 
		on the screen (in edit and view modes).</p>
		
		<div class="indent2">
	<p><span class="xmlAttr">cssclass</span> 
        <br/>
		CSS class for the panel. The default value is "Panel".
	</p>
	<p><span class="xmlAttr">cssclasslabel</span> 
		<br/>
		CSS class for the panel title. The default value is "PanelLabel". 
	</p>
	<p><span class="xmlAttr">img</span> 
		<br/>
		Image to display as the title panel. 
	</p>
	<p><span class="xmlAttr">label</span><br/>
		Panel title 
	</p>
	<p><span class="xmlAttr">optional</span> 
		<br/>
		Skips the panel from displaying, if every field contained is empty and 
		optional (in View mode only). 
	</p>
	<p><span class="xmlAttr">width</span> 
		<br/>
		Width of the panel in percentage of the total width of the form.<br/>
		Example: <span class="xmlAttr">width</span>="100"</p>
		
	<p>Tabs are used to divide a page into several sets of panels 
		and panel-details displayed separately. A tab contains one or more panels and 
		panel-details. More about the <a href="#tab"><span class="xmlAttr">tab</span></a> element.</p>
   
</div>


<a name="tab"></a>
<h2>tab element</h2>

	<p>Tabs are used to divide a page into several sets of panels 
		and panel-details displayed separately. A tab contains one or more panels and 
		panel-details.</p>
 
	<p><IMG src="pix/ui/tabs.gif" class="indent3 shadow"/></p>
	
	<p>Using <span class="xmlElem">tab</span> elements adds one 
		level of hierarchy in the metadata documents. The structure becomes the 
		following: one <span class="xmlElem">form</span> element containing one <span class="xmlElem">
			data</span> element, and several <span class="xmlElem">tab</span> elements 
		containing one or more <span class="xmlElem">panel</span> elements containing 
		one or more <span class="xmlElem">field</span> elements.
	</p>
	<p>Here is an example from our demo, the first few lines of 
		the&nbsp;wine cellar XML:</p>
		<P class="code">&lt;<span class="xmlElem">form</span> <span class="xmlAttr">xmlns</span>="http://www.evolutility.com"&gt;<br/>
			&lt;<span class="xmlElem">data </span>
		... /&gt;<br/>
			&lt;<span class="xmlElem">tab</span>
		
				<span class="xmlAttr">label</span>="General" &gt;<br/>
			&nbsp; &lt;
			<span class="xmlElem">panel</span>
				<span class="xmlAttr">label</span>="Wine" <span class="xmlAttr">width</span>="80" 
			&gt;<br/>
			&nbsp;&nbsp;&nbsp;&lt;<span class="xmlElem">field</span>
				<span class="xmlAttr">label</span>="Name" <span class="xmlAttr">type</span>="text" 
			...&nbsp;
			<span class="xmlAttr">width</span>="62" 
			/&gt;<br/>
			&nbsp;&nbsp;&nbsp; &lt;<span class="xmlElem">field</span>
				<span class="xmlAttr">label</span>="Vintage" <span class="xmlAttr">type</span>
			="integer" ... 
				<span class="xmlAttr">width</span>="38" /&gt;<br/>
			&nbsp;&nbsp;&nbsp; ...<br/>
			&nbsp;&nbsp;&nbsp;&lt;/<span class="xmlElem">panel</span>&gt;<br/>
			&nbsp; &lt;<span class="xmlElem">panel</span>
			<span class="xmlAttr">label</span>="Bottle Label" 
			<span class="xmlAttr">width</span>="20"&gt;<br/>
			&nbsp;&nbsp;&nbsp; &lt;<span class="xmlElem">field</span>
				<span class="xmlAttr">type</span>="image" ... /&gt;<br/>
			&nbsp;&nbsp;&nbsp;&lt;/<span class="xmlElem">panel</span>&gt;<br/>
			&lt;/<span class="xmlElem">tab</span>&gt;<br/>
			&lt;<span class="xmlElem">tab</span>					
				<span class="xmlAttr">label</span>="Purchase" &gt;<br/>
			&nbsp; &lt;<span class="xmlElem">panel</span>
				<span class="xmlAttr">label</span>="Purchase" <span class="xmlAttr">width</span>="30" 
			&gt;<br/>
			&nbsp;&nbsp;&nbsp; &lt;<span class="xmlElem">field</span>
				<span class="xmlAttr">type</span>="date" ... /&gt;<br/>
			&nbsp;&nbsp;&nbsp; ...
			<br/>
			&nbsp;&nbsp;&nbsp;&lt;/<span class="xmlElem">panel</span>&gt;<br/>
			&lt;/<span class="xmlElem">tab</span>&gt;<br/>
			...<br/>
			&lt;/<span class="xmlElem">form</span>&gt;</p>

	<p>The attributes of the element <span class="xmlElem">tab</span>
		are the same as for the element <span class="xmlElem">panel</span>.</p>


		
</asp:Content>
