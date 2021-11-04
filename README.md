# Real State Project

Hi! This project is built with .NET Core and Angular.


## Clean Architecture

Within this solution there are five projects:

 - ApplicationCore: takes care of our interfaces, business rules and Entities
 - Infrastructure: knows about how to access our data, repositories
 - Services
 - UI
 - WebAPI: controlles

 ## Entity Framework and SQLite
 Entity Framework is used. Is an Object Relational Mapper that translates the code into SQL commands that update our tables in the database.
 Entity Framework works with database providers, we're going to use SQLite for development. SQLite don't need to install a database server and uses a file to store our database.
 SQLite is not production worthy, but is great for development because it's very portable and simply adds a file into our project folder for the database.
 Why not SQL Server? Because is not cross platform.

 