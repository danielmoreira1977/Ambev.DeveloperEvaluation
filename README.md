# Developer Evaluation Project

---
In addition to simply solving the implementation proposed in the challenge — which we can consider trivial for a Senior Developer (especially since it was well-documented and partially implemented) — I chose to take a different approach.

I’m aware that unconventional approaches can sometimes frustrate reviewers, but I took the risk to showcase other skills within the .NET platform.

Strongly Typed IDs, addressing Primitive Obsession, using Records, moving the project toward a more object-oriented design, creating an automated "migrator," and exploring different versions of entities with various creation patterns and constructors, among other things.

I decided to experiment a bit beyond what was expected. However, time is the biggest challenge in this kind of test — writing well-commented, thoroughly tested code that reflects one’s knowledge and experience demands more time than what is typically available for a simple evaluation.

Nevertheless, I’m grateful for the opportunity. It was fun to participate, and I wish all the best to everyone involved.

Daniel Moreira
---

`DEV NOTES - PLEASE READ CAREFULLY`
## Improvements
 - Update to .NET 9
 - Implement Central Package Management for Nuget [https://devblogs.microsoft.com/dotnet/introducing-central-package-management/]
 - Implement .NET ASPIRE 
 - Refactory of Entities
	- void primitive obssesion
	- Use Strong Type Ids
 - Remove AutoMapper (It is becoming commercial and should be replaced - MediatR should also be replaced - but it was out of the scope of this test due to time constraints)	
 - Implement Unit of Work on DbContext
 - Include AggregationRoot do Domain (as recomended on DDD)
 - Add UserSecrets (to avoid Sensitive Data from AppSetting)
 - Using SmartEnums
 - Implement IOptions<T> pattern for JwtSettings
 - Return API errors based on RFC 9457 document

## Notes

* Generic repositories were removed, and the `DbContext` is now consumed directly. This is simply a technical preference of mine, as it facilitates consistency with the Unit of Work pattern. I find it easier not to abstract something that is already abstracted by EF Core, among other reasons. However, there's nothing wrong with using the repository approach.

* Some minor refactorings were made, such as moving `IUser` from `Common` to `Domain`, simply to better align with the refactoring model proposed by this developer. In a different context, these files could remain in their original locations, and the code would be refactored to accommodate them appropriately.

* Some method signatures and interfaces (such as `IJwtTokenGenerator`) were adjusted to avoid unnecessary coupling between the refactored layers.

* Some comments were omitted purely due to time constraints — their importance is clear, and this developer is fully in favor of their mandatory use.

* The `[BaseController]` was separated to avoid mixing logged-in user properties with common return objects (Single Responsibility Principle).

* There are generally two types of `[Validator]` for the same request: one acting at the API level and another at the Application level, both validating the same fields. I chose to validate only at the Application level to avoid having two points of change for the same rule. However, I also understand the benefit of validating at the API level using the "Fail Fast" concept with guard clauses.

* When using offsets in queries, results may be skipped or omitted.

* Regarding filter rules, the code in `IQueryableExtensions` should be extended to support all \[value objects] and \[records] — this was omitted due to time constraints, but I made it work using `Price` as an example.

* The RabbitMQ implementation used is the one present in the eShop project.

## How to run
- Run EF Migrations
-- Copy FULL path of [Ambev.DeveloperEvaluation.API] and use it on [startup-project]
-- Open a terminal on [Ambev.DeveloperEvaluation.ORM] folder and execute the command:
dotnet ef migrations add InitialCreate --startup-project [FULL_PATH_API] --context DefaultContext

the command should be like:
dotnet ef migrations add InitialCreate --startup-project C:\Users\User\Desktop\DESAFIO\template\backend\src\Ambev.DeveloperEvaluation.WebApi\Ambev.DeveloperEvaluation.WebApi.csproj  --context DefaultContext	

- RUN on Visual Studio
	The HTTP file [...\src\Ambev.DeveloperEvaluation.WebApi\Ambev.DeveloperEvaluation.WebApi.http] contains the tests 
		
---

## Instructions
**The test below will have up to 7 calendar days to be delivered from the date of receipt of this manual.**

- The code must be versioned in a public Github repository and a link must be sent for evaluation once completed
- Upload this template to your repository and start working from it
- Read the instructions carefully and make sure all requirements are being addressed
- The repository must provide instructions on how to configure, execute and test the project
- Documentation and overall organization will also be taken into consideration

## Use Case
**You are a developer on the DeveloperStore team. Now we need to implement the API prototypes.**

As we work with `DDD`, to reference entities from other domains, we use the `External Identities` pattern with denormalization of entity descriptions.

Therefore, you will write an API (complete CRUD) that handles sales records. The API needs to be able to inform:

* Sale number
* Date when the sale was made
* Customer
* Total sale amount
* Branch where the sale was made
* Products
* Quantities
* Unit prices
* Discounts
* Total amount for each item
* Cancelled/Not Cancelled

It's not mandatory, but it would be a differential to build code for publishing events of:
* SaleCreated
* SaleModified
* SaleCancelled
* ItemCancelled

If you write the code, **it's not required** to actually publish to any Message Broker. You can log a message in the application log or however you find most convenient.

### Business Rules

* Purchases above 4 identical items have a 10% discount
* Purchases between 10 and 20 identical items have a 20% discount
* It's not possible to sell above 20 identical items
* Purchases below 4 items cannot have a discount

These business rules define quantity-based discounting tiers and limitations:

1. Discount Tiers:
   - 4+ items: 10% discount
   - 10-20 items: 20% discount

2. Restrictions:
   - Maximum limit: 20 items per product
   - No discounts allowed for quantities below 4 items

## Overview
This section provides a high-level overview of the project and the various skills and competencies it aims to assess for developer candidates. 

See [Overview](/doc/overview.md)

## Tech Stack
This section lists the key technologies used in the project, including the backend, testing, frontend, and database components. 

See [Tech Stack](/.doc/tech-stack.md)

## Frameworks
This section outlines the frameworks and libraries that are leveraged in the project to enhance development productivity and maintainability. 

See [Frameworks](/.doc/frameworks.md)

<!-- 
## API Structure
This section includes links to the detailed documentation for the different API resources:
- [API General](./docs/general-api.md)
- [Products API](/.doc/products-api.md)
- [Carts API](/.doc/carts-api.md)
- [Users API](/.doc/users-api.md)
- [Auth API](/.doc/auth-api.md)
-->

## Project Structure
This section describes the overall structure and organization of the project files and directories. 

See [Project Structure](/.doc/project-structure.md)
