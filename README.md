# Description
This is a simple cook book application which basically consists of:
- Angular client
- Web API service

# Setup
To start client application you can either:
- Build and run Client project
- Run ng serve (dev mode) or ng build --prod (prod mode) in Client\ClientApp\ folder

To start web api service:
- Build and run API project

For convenience, solution is published to:
Client: http://www.cook-book.boblox.org
Web API: http://cook-book.api.boblox.org

# Project creation
The idea was to deliver product in time, write it in the most simple but at the same time agile approach. Project creation started from evaluation of minimal user needs and modeling of its use-cases. Then I designed user interface, thinked about common project structure, created Web API facade, worked on domain model, business layer and db model. After that I implemented first version of product and covered it with some unit tests. At that time, when you running product, you can do some reorganization and add an additional functionality.

As it's the test project, it was impossible to implement everything I wanted in such a short time. So there's still room for improvement.
For client part:
- Add pagination to recipe list and recipe revision history
- Add RTE for description part of recipe with possibility to add pictures
- Add main picture to recipe
- Reorganize recipe list UI from table to a set of colourful cards
- Possibility to revert recipe to a previous version
- Add user authentication, so that everyone has it's own set of recipes

For server part:
- Add more unit tests
- RecipeManager should be cut out into a separate project (Business Layer) - to make smooth implementation of Ladder pattern

# Testing
API.Test project has some unit tests for API:)
