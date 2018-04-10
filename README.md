# Description
This is a simple cookbook application which basically consists of:
- Angular client
- Web API service

# Setup
To start client application you can either:
- Build and run Client project
- Run ng serve (dev mode) or ng build --prod (prod mode) in Client\ClientApp\ folder

To start web API service:
- Build and run API project

For convenience, the solution is published to:
- Client: http://www.cook-book.boblox.org
- Web API: http://cook-book.api.boblox.org

# Project creation
The idea was to deliver a product on time, write it in the most simple but at the same time agile approach. Project creation started from the evaluation of minimal user needs and modeling of its use-cases. Then I designed user interface, thought about common project structure, created Web API facade, worked on the domain model, business layer and DB model. After that, I implemented the first version of the product and covered it with some unit tests. At that time, when you running product, you can do some reorganization and add an additional functionality.

As it's the test project, it was impossible to implement everything I wanted in such a short time. So there's still room for improvement.
For client part:
- Add pagination to recipe list and recipe revision history
- Add RTE for description part of a recipe with a possibility to add pictures
- Add the main picture to recipe
- Reorganize recipe list UI from table to a set of colorful cards
- Possibility to revert recipe to a previous version
- Add user authentication, so that everyone has its own set of recipes
- Loading spinner

For server part:
- Add more unit tests
- RecipeManager should be cut out into a separate project (Business Layer) - to make smooth implementation of Ladder pattern

# Testing
API.Test project has some unit tests for API:) Test system uses Moq, MS Test and InMemory database
