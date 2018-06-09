<%@ Page AutoEventWireup="true" CodeFile="demo.aspx.cs" Inherits="demo" Language="C#"
    MasterPageFile="zmDemo.master" Title="Evolutility :: Demo : Master-details" 
CodeFileBaseClass="BasePage"  
Menus="demo_movie"
SubMenuID="205"
%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
	<h1>Master-details</h1>

			<p>This is a small "movie database" application made to demonstrate Master-details with Evolutility.</p>
 
 		<p>This demo is about relationships (showing 1-to-many and many-to-many relationships). <br />
 		<ul>
 		<li>One <A href="movie_director.aspx"><IMG class="Icon" src="../PixEvo/moviedir.gif"  alt="" >Director</A> per <A href="movie.aspx"><IMG class="Icon" src="../PixEvo/movie.gif"  alt="" >Movie</A></li>
 		<li>One or Several <A href="movie_actor.aspx"><IMG class="Icon" src="../PixEvo/mec2.gif"  alt="" >Actors</A> per <A href="movie.aspx"><IMG class="Icon" src="../PixEvo/movie.gif"  alt="" >Movie</A></li>
			 <li>One or Several <A href="movie.aspx"><IMG class="Icon" src="../PixEvo/movie.gif"  alt="" >Movies</A> per <A href="movie_actor.aspx"><IMG class="Icon" src="../PixEvo/mec2.gif"  alt="" >Actor</A></li> 
 		</ul></p>
 		<p><img src="../pix/movieDB.gif" /></p> 

</asp:Content>

