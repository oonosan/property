
# Real State Project
Hi! This project is built with .NET Core and Angular.


# Architecture
## Overview
A Clean architecture is implemented in this project. The key rule behind Clean Architecture is the Dependency Rule, which states that source code dependencies can only point inwards.
![Clean Architecture](https://github.com/oonosan/property/blob/main/img/Clean%20architecture.png?raw=true)

Within this solution there are five projects:
- Property.**ApplicationCore**: takes care of our interfaces, business rules and Entities
- Property.**UI**: Presentation layer, an Angular project
- Property.**Infrastructure**: knows about how to access our data. Implements ORM Entity Framework to get daa form the database, but it's used the Repository pattern to decouple the business logic and the data access layers in our application.
> The Repository Design Pattern in C# mediates between the domain and the data mapping layers using a collection-like interface for accessing the domain objects. In other words, we can say that a Repository Design Pattern acts as a middleman or middle layer between the rest of the application and the data access logic
- Property.**Services**
- Property.**WebAPI**: controllers

## Angular Architecture  

# Entity Framework and PostgreSQL
Entity Framework is used. Is an Object Relational Mapper that translates the code into SQL commands that update our tables in the database.
Entity Framework works with database providers, we're going to use PostgreSQL for development. Why not SQL Server? Because is not cross platform.


# Authentication
**Identity Server** is used to authenticate, authorize and secure both the application and the users. It's in charge of implement common protocols and follow common standards: OpenId and OAuth2.0
