# Real State Project

Hi! This project is built with .NET Core and Angular.


## Architecture
![alt text](https://github.com/image.jpg?raw=true)

Within this solution there are five projects:
 - Property.**ApplicationCore**: takes care of our interfaces, business rules and Entities
 - Property.**Infrastructure**: knows about how to access our data, repositories
 - Property.**Services**
 - Property.**UI**
 - Property.**WebAPI**: controllers

 ## Entity Framework and PostgreSQL
 Entity Framework is used. Is an Object Relational Mapper that translates the code into SQL commands that update our tables in the database.
 Entity Framework works with database providers, we're going to use PostgreSQL for development. 
 Why not SQL Server? Because is not cross platform.

 