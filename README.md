
# TvMaze WebApi Sample

This sample solution uses **Clean Architecture** and **CQRS** to expose a REST Web API for data obtained from www.tvmaze.com.

The Database is empty by default, created on the first run. You will need to use the **api/import/shows** endpoint to launch the job to retrieve all data. Access to stored data is available using the **api/shows** endpoint.

Swagger documentation is available at: **/swagger/index.html**

## Technologies

 - C# language using Microsoft .NET 8.0 Framework
 - SqLite with EntityFramework Core (In-Memory for tests)
 - MediatR for the CQRS pattern
 - Automapper
 - xUnit as testing framework (using Moq & FluentAssertions packages)

## Architecture
### Description
The entry point is the Web API project that starts the application and configures everything.
Core layer (domain) contains all business entites, and have no relations with any other layers.
Application layer contains logic for commands, queries, mappers, etc. and contracts for any other layer.
Infrastructure layer contains everything that resides outside our architecture, like databases, repositories and external API adapters. 

### Solution structure
src\
├── TvMaze.API\
│   └── Main Program + Controllers\
├── TvMaze.Application\
│   └── Application logic, including CQRS related classes\
├── TvMaze.Core\
│   └── Aggregates\
├── TvMaze.Infrastructure\
│   └── External stuff: Database, Rest API Connectors.

test \
├── TvMaze.FunctionalTests\
 ── Mocked WebApplication to test controllers\
├── TvMaze.IntegrationTests\
│   └── Repository Unit tests with fixtures\
├── TvMaze.UnitTests\
│   └── CQRS Handlers unit test

## Installation
Clone the repository and restore packages:

```sh
$ git clone https://github.com/marcosjimenez/tvmaze.git
$ cd tvmaze
$ dotnet restore
```

## Run
Use --launch-profile "https" or "http"
```sh
$ cd src\TvMaze.API
$ dotnet run --launch-profile "https"
```

## ToDo
There are many things remaining to do on this project, here are a list of cool things that can be implemented:
- Use JWT for ApiKey (currently fixed string)
- Move the Task.Run jobs to a HangFire like package to improve error control and perfomance.