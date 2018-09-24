# Evolutility-ASP.net

Evolutility is a generic CRUD (Create, Read, Update, Delete) web application running on Microsoft ASP.net, and SQL Server or MySQL databases.

You may think of it as a "dynamic scaffolding" or "metadata-driven MVC" that generates all web pages at run-time, and can be modified by editing metadata (screen definitions and database mapping) instead of code.

With Evolutility the user interface (e.g. fields titles, positions, visual groups, CSS classes) and its database mapping (e.g. tables, columns, stored procedures) are not defined in the code but in external metadata (stored as XML files or in the database). Evolutility web control can be nested into any ASP.net page. It will generate at run-time all necessary web forms, manage user interaction, and database CRUD (create, read, update, delete) operations automatically.

Demos: 
[To Do list](http://evolutility.org/demo/demo_ToDo.aspx),
[AddressBook](http://evolutility.org/demo/demo_addressbook.aspx),
[Wine Cellar](http://evolutility.org/demo/demo_WineCellar.aspx),
[Restaurants list](http://evolutility.org/demo/demo_Restaurant.aspx).

Some [documentation](http://evolutility.org/doc/doc.aspx).

Hosted at [SourceForge](https://sourceforge.net/projects/evolutility/) since 2008.


## Installation

To run the Evolutility sample applications:

- Copy the directory "Evolutility__Web" (which contains the web site) to your web server.
- Attach the database (located in the "Evolutility__Web/App_Data" directory of the web site). 
   You may also create a new database and run the SQL scripts (located in "Resources/SQL/").
- If necessary, change the database connection string in the "appSettings" section 
    of the Web.config file (or in specific ASPX page). 

Default login/password:

 - For the multi-users demos, use John/John or Mary/Mary as your login/password.
 - For EvoDico, use EVOL/LOVE as your login/password.

More about [installing Evolutility](http://evolutility.org/doc/EvoDoc_Install.aspx).

## Other implementations of Evolutility

[Evolutility-UI-React](https://github.com/evoluteur/evolutility-ui-react) - Model-driven Web UI for CRUD using React.

[Evolutility-UI-jQuery](https://github.com/evoluteur/evolutility-ui-jquery) - Model-driven Web UI for CRUD using jQuery and Backbone (for REST or localStorage).

[Evolutility-Server-Node](https://github.com/evoluteur/evolutility-server-node) - RESTful Micro-ORM for CRUD and more, written in Javascript, using Node.js, Express, and Postgres.
