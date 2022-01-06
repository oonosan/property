
# Real State Project
Hi! This project is built with .NET Core and Angular.


# Architecture
## Overview
A Clean architecture is implemented in this project. The key rule behind Clean Architecture is the Dependency Rule, which states that source code dependencies can only point inwards. The following diagram explains the general structure:
![Clean Architecture](https://github.com/oonosan/property/blob/main/img/Clean%20architecture.png?raw=true)

The layers have these rules on how they should interact with each other:
- **Core entities**: Encapsulate enterprise wide business rules. These are plain data models which are essentially needed to represent our core logic, build a data flow upon and get our business rule working.
- **Use cases**: Contains application specific business rules. These are built on top of the core entities and implement the whole business logic of the application. Usecases should “live in their own world” and only do what they are supposed to do.
- **Interface adapters**: Convert data from the format most convenient for the use cases and entities. To get a working architecture beyond the border of each layer, the best way is to work with interface adapters from the core (entity and usecase layer) to ensure a homogenous structure all over the projects, which fits like a puzzle in the end.
- **Frameworks & Drivers**: Where all the I/O components go. We keep these things on the outside where they can do little harm. It’s the most volatile layer. Since the things in this layer are so likely to change, they are kept as far away as possible from the more stable domain layers. 
- **Dependency rule**: states that source code dependencies can only point inwards. So, the usecase layer should only use entities which are specified in the entity layer, and the controller should only use usecases from the usecase layer below. 
- **Crossing Boundaries**: At the lower right of the above diagram is an example of how we cross the circle boundaries. It shows the Controllers and Presenters communicating with the Use Cases in the next layer. Note the flow of control. It begins in the controller, moves through the use case, and then winds up executing in the presenter. How do we implement this? “Dependency Inversion Principle”.
- **Dependency Inversion Principle (DIP)**: This is the D of SOLID. This means that less stable classes and components should depend on more stable ones, and not the other way around. If a stable class depends on an unstable class, then every time the unstable class changes, it will also affect the stable class. So the direction of dependency needs to be inverted. How is this done? By using an abstract class or hiding the stable class behind an interface.

The Onion Architecture was chosen for this project for its maintainability and testability
![Onion Layer](https://github.com/oonosan/property/blob/main/img/Clean%20architecture%20layers.png?raw=true)

Within this solution there are five projects:
- Property.**ApplicationCore**: takes care of our interfaces, business rules and Entities
- Property.**UI**: Presentation layer, an Angular project
- Property.**Infrastructure**: knows about how to access our data. Implements ORM Entity Framework to get daa form the database, but it's used the Repository pattern to decouple the business logic and the data access layers in our application.
> The Repository Design Pattern in C# mediates between the domain and the data mapping layers using a collection-like interface for accessing the domain objects. In other words, we can say that a Repository Design Pattern acts as a middleman or middle layer between the rest of the application and the data access logic
- Property.**Services**
- Property.**WebAPI**: controllers


## Angular Architecture 
![Angular Architecture]()
In Angular we want an unidirectional dataflow. Child components only notify their parent components, the parent (smart component) will send an **action** to a **store** that contains **state**, and that action will update the state for the entire application.

![Unidirectional dataflow]()

A sandbox principle was implemented as [Ante Burazer](https://netmedia.io/dev/angular-architecture-patterns-high-level-project-architecture_5589) described in his article.
**The sandbox is a way to decouple the presentation layer from the application logic**, but that's not its only responsibility.


# Entity Framework and PostgreSQL
Entity Framework is used. Is an Object Relational Mapper that translates the code into SQL commands that update our tables in the database.
Entity Framework works with database providers, we're going to use PostgreSQL for development. Why not SQL Server? Because is not cross platform.


# Authentication
**Identity Server** is used to authenticate, authorize and secure both the application and the users. It's in charge of implement common protocols and follow common standards: OpenId and OAuth2.0
