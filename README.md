
# Real State Project
Hi! This project is built with .NET Core and Angular.


# Architecture Overview
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


## General project structure
The Onion Architecture was chosen for this project for its maintainability and testability
![Onion Layer](https://github.com/oonosan/property/blob/main/img/Clean%20architecture%20layers.png?raw=true)

Within this solution there are five projects:
- Property.**ApplicationCore**: takes care of our interfaces, business rules and Entities
- Property.**UI**: Presentation layer, an Angular project
- Property.**Infrastructure**: knows about how to access our data. Implements ORM Entity Framework to get daa form the database, but it's used the Repository pattern to decouple the business logic and the data access layers in our application.
> The Repository Design Pattern in C# mediates between the domain and the data mapping layers using a collection-like interface for accessing the domain objects. In other words, we can say that a Repository Design Pattern acts as a middleman or middle layer between the rest of the application and the data access logic
- Property.**Services**
- Property.**WebAPI**: controllers


# Angular Architecture 
In Angular we want an unidirectional dataflow. Child components only notify their parent components, the parent (smart component) will send an **action** to a **store** that contains **state**, and that action will update the state for the entire application.

[Ante Burazer](https://netmedia.io/dev/angular-architecture-patterns-high-level-project-architecture_5589) article was used.

![Angular Architecture](https://github.com/oonosan/property/blob/main/img/Angular%20project%20architecture%20diagram.png?raw=true)

 1. **Application Core Module**
 We can call it the root module as well and it’s located in _app/app.module.ts_ file. It describes how the application parts fit together and it’s also the entry point used for launching the application.
The main tasks for the root module are:
-   **Imports** all other modules we want to plug in the application**
-   **Provides** services we want to expose globally inside the application and instantiate only once
-   **Declares** the application’s root component
-   **Bootstraps** the root component that Angular creates and inserts it into the _index.html_ host web page

 2. **Async Services**
 Are a collection of modules responsible for different types of communication.
The goal of the HTTP layer is to add headers, manage the request methods, intercept requests, receive the responses, parse them and handle the various types of errors.

 3. **State Managment**
[ngrx/store Documentation](https://gist.github.com/btroncone/a6e4347326749f938510)
We’ll treat our store as a database where each reducer is a table and it represents a slice of state we want to keep track of. The store acts like a relational database where we can use a high level selectors to merge different parts of our state.
We put our store inside **_app/shared/store/index.ts_** file. It will hold an interface which describes each piece of the store and represents the state from each reducer **_– State_**. This interface is just a map of keys to inner state types. Besides overall state the store contains selector functions to get each little piece of the state and child reducers have no knowledge of the overall state tree.
On the other hand we are using [ngrx/effects](https://github.com/ngrx/effects/blob/master/docs/intro.md). What are they? Effects relate to the term side effects. It‘s a piece of code which needs to be executed after the ngrx action has been invoked. It’s basically a function which returns an observable. Effects are used for handling async calls for our actions and chaining other actions when async calls end. **_To manipulate with application state we should deal with actions only_**.

  4. **Application Core Facade**
Application core facade is represented as a sandbox. It is an abstract class which holds common logic of the application core API. We placed it in _app/shared/sandbox/base.sandbox.ts_ file.
Each presentational module’s sandbox will extend the base sandbox class which will act as an interface and the base class they will inherit from. Here we can define which methods and properties each sandbox instance needs to have.

 5. **Sandbox**
Sandbox is a service which extends application core facade and exposes streams of state and connections to the async services. It acts as a mediator and a facade for each presentational module with some extra logic, like serving needed piece of state from the store, providing necessary async services to the UI components, dispatching events.
We can put it inside the _app/shared/sandbox_ folder, grouped by feature, or place it inside the corresponding presentational module folder. We’ll go with the second option because the sandbox logic is explicitly related to the presentational module we are building it for. This way we’ll have all related logic in one place.

```javascript
ClientApp/
├──i18n //folder for multi language 
└──src
     └──app/
	     ├──admin/
		 |
		 ├──basic-pages/ 
		 |  
		 ├──auth/
		 |   ├──auth.module.ts
		 |   ├──auth.page.ts
		 |   └──auth.sandbox.ts
		 |
		 ├──dashboard/
		 |   ├──dashboard.module.ts
		 |   ├──dashboard.page.ts
		 |   └──dashboard.sandbox.ts
		 |
		 ├──home/ 
		 |  
		 ├──not-found/
		 |  
		 ├──publication/
		 |  
		 ├──my-publications/
		 |  
		 ├──shared/ //will host all shared entities that will be provided to every module of the project
		 |   ├──asyncServices/
		 |   |   └──http/
		 |   |
		 |   ├──auth/
		 |   |
		 |   ├──components
		 |   |   ├──nav-menu
		 |   |   └──search-menu
		 |   |
		 |   ├──containers/layout
		 |   |   ├──admin-layout
		 |   |   ├──renter-layout
		 |   |   └──tenant-layout
		 |   |
		 |   ├──models/
		 |   |
		 |   ├──sandbox/
		 |   |   ├──base.sandbox.ts
		 |   |
		 |   ├──store/
		 |   |   ├──reducers/
		 |   |   ├──actions/
		 |   |   ├──effects/
		 |   |   └──index.ts
		 |   |
		 |   └──utilities/		 
		 |
		 ├──app.module.ts // Application Core module / root module (1)
		 ├──app.module-routing.ts
		 ├──app.component.ts
		 └──app.sandbox.ts
```


# Entity Framework and PostgreSQL
Entity Framework is used. Is an Object Relational Mapper that translates the code into SQL commands that update our tables in the database.
Entity Framework works with database providers, we're going to use PostgreSQL for development. Why not SQL Server? Because is not cross platform.


# Authentication
**Identity Server** is used to authenticate, authorize and secure both the application and the users. It's in charge of implement common protocols and follow common standards: OpenId and OAuth2.0
