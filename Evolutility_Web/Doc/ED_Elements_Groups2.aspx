<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="Doc.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation : Metamodel elements" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="125"
Meta_Description="Evolutility documentation: Meta-model covering UI and database mapping for CRUD applications" 
Meta_Keywords="metamodel meta data metadata CRUD  master details"
%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
 
<h1>panel-details element</h1>

	<p>The element <span class="xmlElem">panel-details</span> allows for nested 
		sub-entities within the driving entities. In other words, it allows for 
		hierarchical data and adds the feature of master-details. 
		The sub-entity (or detail) can be edited within the page as a grid, or by linking to 
		another page using the sub-entity as its driving entity.</p>
	<p>The screenshot below shows an order. The master is composed 
		of 3 panels, one panel-details and another panel for the total.</p>
	<BLOCKQUOTE dir="ltr" style="MARGIN-RIGHT: 0px">
		<p><img src="pix/ui/panel-details.gif"></p>
	</BLOCKQUOTE>
	<p>The corresponding XML is as follow: </p>
	
	<P><div class="code">
		
		&lt;<span class="xmlElem">form</span> 
		<span class="xmlAttr">label</span>="Orders" <span class="xmlAttr">xmlns</span>="http://www.evolutility.com" &gt;<br/>
		
<div style="margin-left:20px;">
		
		&lt;<span class="xmlElem">data</span> <span class="xmlAttr">dbtable</span>="NW_ORDERS" 
		<span class="xmlAttr">dborder</span>="t.OrderDate desc"
		<br/>
		<span class="xmlAttr">entity</span>="order" 
		<span class="xmlAttr">entities</span>="orders" /&gt;<br/>
		
		&lt;<span class="xmlElem">panel</span> <span class="xmlAttr">label</span>="Order" 
		<span class="xmlAttr">width</span>="100"&gt;<br/>
		
	<div style="margin-left:20px;">
		
		&lt;<span class="xmlElem">field</span> 
		<span class="xmlAttr">label</span>="Order ID" ... /&gt;<br/>
		&lt;<span class="xmlElem">field</span>  
		<span class="xmlAttr">label</span>="Customer" ... /&gt;<br/>
		...<br/>
		
	</div>
		
		&lt;/<span class="xmlElem">panel</span>&gt;&nbsp;
		<br/>
		...
		<br/>
		
		&lt;<span class="xmlElem">panel-details</span><span class="xmlAttr">
		panelid</span>="1" <span class="xmlAttr">label</span>="Order Details" <span class="xmlAttr">width</span>="100"<br/>
		<span class="xmlAttr">dbtabledetails</span>="NW_OrderDetails"<br/>
		<span class="xmlAttr">dbcolumndetails</span>="OrderID"
		<br/>
		<span class="xmlAttr">dborder</span>="t.Productname" &gt;<br/>
		
	<div style="margin-left: 20px;">
		
		&lt;<span class="xmlElem">field</span>  <span class="xmlAttr">label</span>="Product" ... /&gt;<br/>
		&lt;<span class="xmlElem">field</span>  <span class="xmlAttr">label</span>="Unit price" ... /&gt;<br/>
		...<br/>
		
	</div>
		
		&lt;/<span class="xmlElem">panel-details</span>&gt;<br/>
		
		&lt;<span class="xmlElem">panel</span> <span class="xmlAttr">label</span>="" 
		<span class="xmlAttr">width</span>="84" <span class="xmlAttr">cssclass</span>="none"&gt;&lt;/<span class="xmlElem">panel</span>&gt;&nbsp;&nbsp;
		
		<br/>
		&lt;<span class="xmlElem">panel</span> <span class="xmlAttr">label</span>="" 
		<span class="xmlAttr">width</span>="16"&gt;&nbsp; 
		<br/>
	<div style="margin-left: 20px;">
		&lt;<span class="xmlElem">field</span>  
		<span class="xmlAttr">label</span>="Total Price" ... /&gt;&nbsp;&nbsp;&nbsp; 
		<br/>
	</div>	
		&lt;/<span class="xmlElem">panel</span>&gt;<br/>
		
</div>		
		
		&lt;/<span class="xmlElem">form</span>&gt;
		
	</div></p>

	<p>Each <span class="xmlElem">panel-detail</span> element contains one or more <span class="xmlElem">field</span> elements which are 
		displayed as a grid when in edit mode.
	</p>
	<p>The element <span class="xmlElem">panel-details</span> use 
		the same attributes as <span class="xmlElem">panel</span> (<span class="xmlAttr">label</span>,
		<span class="xmlAttr">cssclass</span>, <span class="xmlAttr">cssclasstitle</span>,
		<span class="xmlAttr">img</span>, <span class="xmlAttr">optional</span>, <span class="xmlAttr">
			width</span>) plus the following extra attributes:</p>
		<div class="indent2">
				
			<p><span class="xmlAttr">dbcolumndetails<br/>
				</span>Key column (in the details table) used to join it to the master table.
				<br/>
				In the sample XML above, with dbtable=NW_ORDERS, dbtabledetails= 
				NW_OrderDetails, dbcolumndetails="OrderID", the where clause for the query of 
				the details rows would be "NW_ORDERS.ID= NW_OrderDetails.OrderID"</p>
			<p>
				<span class="xmlAttr">dbcolumnmaster<br />
				</span>Column (in the master table) used to join with the details table. </p>
				
			<p><span class="xmlAttr">dbmaxrow</span><br/>
				Maximum number of rows displayed.
			</p>
			<p><span class="xmlAttr">dbtabledetails</span><br/>
				Name the driving table for the details.
			</p>
			<p><span class="xmlAttr">panelid</span><br />
			Each <span class="xmlElem">panel-details</span> must have this attribute (set to an integer value) and each field inside that must also have it (with the same matching value). 
			Please look at the Wine cellar sample application as an example of how it works.
			</p>
		</div>
	



 

		
</asp:Content>
