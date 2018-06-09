<%@ Page Language="C#" MasterPageFile="zmDoc.master" AutoEventWireup="true" CodeFile="Doc.aspx.cs" Inherits="EvoDoc" 
Title="Evolutility :: Documentation : Element positioning" 
CodeFileBaseClass="BasePage"
Menus="doc"
SubMenuID="150"
Meta_Description="Evolutility documentation: Element positioning" 
Meta_Keywords="documentation"
%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h1>Elements positioning</h1>
		
		<p>Evolutility follows the "flow positioning" method. Elements position is entirely determined by their order in the metadata, which 
			does not require any description of the positioning, absolute or relative, of 
			the user interface on the screen. The ordering of the information and the 
			internal description of each element (such as field type, width, and height) implies 
			the coordonates of every element on each screen.</p>
		<p>You can organize fields in panels (sections) on the page. 
			Panels are placed sequentially, left to right, until a width of 100% is 
			reached. Once the maximum width is reached, the next panel will appear below 
			the previous group of panels.</p>
		<p>In the following screenshot the form is organized into 4 panels for name 
			(width=60%), contact information (width=40%), address (width=60%), and 
			misc.(width=40%). Each of these panels contains one or several fields.</p> 
			<p><img src="pix/ui/position1.gif" class="indent3">
			</p> 
		<p>Inside each panel, fields are placed using the same rule: placed sequentially, 
			left to right, until a width of 100% is reached.</p> 
			
				<TABLE cellSpacing="0" cellPadding="0" border="0" class="indent3">
					<TR>
						<TD vAlign="top">
							<p><img src="pix/ui/position2.gif"><br/>
								&nbsp;</p>
						</TD>
						<td>&nbsp;&nbsp;</td>
						<TD vAlign="top">
							<P>Lastname: width=100%<br/>
								Firstname: width=100%<br/>
								Title: width=50%<br/>
								Company: width=50%</p>
						</TD>
					</TR>
					<TR>
						<TD vAlign="top">
							<p><img src="pix/ui/position3.gif">
							</p>
						</TD>
						<td>&nbsp;&nbsp;</td>
						<TD vAlign="top">
							<P>Lastname: width=50%<br/>
								Firstname: width=50%<br/>
								Title: width=50%<br/>
								Company: width=50%<br/>
								<br/>
							</p>
						</TD>
					</TR>
				</TABLE>
			 
		<p>The previous 2 screenshots demonstrate how elements are re-positioned simply by 
			changing their width.
		</p>
		
		<p>The "flow positioning" method is also quite flexible as it allows different numbers of fields on different rows which is quite
		useful for cases like an address.</p>
				<TABLE cellSpacing="0" cellPadding="0" border="0" >
					<TR>
						<TD vAlign="top"><img src="pix/ui/address.gif" class="indent3"> 
						</TD>
						<td>&nbsp;&nbsp;</td>
						<TD vAlign="top">
							<P >Address: width=100% height=2<br/>
								City: width=62%<br/>
								State: width=15%<br/>
								Zip: width=23%<br/>
								<br/>
							</p>
						</TD>
					</TR>
				</TABLE>
			<p>
			</p>
		
		
		<p>This positioning scheme is forgiving in the sense that any value greater than 
			100% will be considered as 100%; a field of width less than 100% between two 
			fields of width 100% will also behave as 100%.
		</p>
		
		<p>The Edit and View mode follow the same positioning. For other modes, fields 
			appear or not based on the attribute <span class="xmlattr1">search</span> for search,
			 <span class="xmlattr1">searchadv</span> for advanced search,
			  and <span class="xmlattr1">searchlist</span>
			  for list. All fields present in the metadata will appear in the export 
			mode.</p>
	
	<h2>Labels positioning</h2>
	 
		<p>Labels positioning is more important for usability than we often imagine. 
		In his latest book "Web form design", 
		Luke Wroblewski dedicates a full chapter to field labels positioning. Evolutility follows it as a guideline.
		</p>
		
		<ul>
			<li>Top aligned labels in View & Edit modes</li>
			<li>Left-aligned labels - Search & Advanced Search</li>
			<li>Labels with inputs - Export</li>
		</ul> 	 
				
	
	<h2>Using golden proportions</h2>
	
		<p>From the ancient Greek architects to Da Vinci or the French impressionists, many great Masters noticed the golden ratio 
		(<a href="http://en.wikipedia.org/wiki/Golden_ratio" target="GR">golden ratio on Wikipedia</a>) 
		in the human body and everywhere in nature. They incorporated it into their art in a very consistent way. </p> 
		
		<p>Universally recognized as more aesthetical, the golden ratio can be used in web application screens  
		simply dividing space close to 2/3 - 1/3 ratio (more exactly 62% - 38%)... as long as it doesn't get in the way of usability.  
		</p>
		 
		 <p>Standard ratio: 50% - 50%.</p>
					<p><img src="pix/ui/contact-50-50s.gif" class="indent3"/>
				</p> 
		<p>Golden ratio: 62% - 38%</p>
	 				<p><img src="pix/ui/contact-38-62s.gif" class="indent3"/>
				</p> 
		
		<p>We can apply the golden ratio in a recursive or fractal way by also applying it for fields on 2 columns of 62% and 38% inside a panel. 
		The difference is subtle but at the unconscious level users should feel more confortable with proportions which remind them 
		of their environment or their body. </p>
		
</asp:Content>