# GalaxyDelivery

ASP.NET Core Web API for managing **Drivers**, **Vehicles**, **Routes**, **Checkpoints**, **Deliveries**, and **DeliveryEvents**.

- Target framework: `.NET 9`
- Language: `C# 13`
- Persistence: EF Core **InMemory** database
- Validation: **FluentValidation**
- Tests: **NUnit** + **Moq**

---

## Prerequisites

- .NET SDK **9.x** installed (`dotnet --info`)

---

## How to set up and build the solution

From the solution folder:

```bash
# restore
dotnet restore

# build
dotnet build
```

---

## How to run the application

From the solution folder:

```bash
dotnet run --project .\GalaxyDelivery\GalaxyDelivery.csproj
```

## Postman Collection

- Available under `Postman_Collection/*` folder

🔴 Important note:  
Currently port 5000 is used in the launchsettings. If you are running in a different port, please make sure to change the port number in the Postman collection.

The API seeds initial data on startup (see `GalaxyDelivery/Entities/DataGenerator.cs`).

### Example base URL

When running locally, the base URL is typically something like:

- `http://localhost:5000`



Use the URLs shown in controller attributes, for example:

- `GET http://localhost:<port>/v1/api/Deliveries`
- `GET http://localhost:<port>/v1/api/Deliveries/1/summary`

---

## How to run tests

From the solution folder:

```bash
dotnet test
```

---

## Assumptions

- The application uses **EF Core InMemory** database. Data is not persisted across restarts.
- No authentication/authorization is implemented (even though the pipeline contains `UseAuthorization()`).
- Route identifiers (`RouteId`) are treated as **server-generated** (configured with `ValueGeneratedOnAdd`).
- DTOs are implemented as **classes** (not records) to align with requested style changes.
- One Delivery has only one driver and one vehicle.
- One Delivery has only one route.
- One Delivery has many events.
- One Route has many Checkpoints.
- Checkpoints are related with Routes. Now while adding/updating/deleting Checkpoint, no checking is done whether the related Route exits or not.
- While deleting Route, corresponding Checkpoints will also be deleted.
- Port 5000 is used for running the API.

---

## Useful files

- App entrypoint: `GalaxyDelivery/Program.cs`
- DB model and EF mappings: `GalaxyDelivery/Entities/GalaxyDbContext.cs`
- Seed data: `GalaxyDelivery/Entities/DataGenerator.cs`
- Delivery summary DTOs: `GalaxyDelivery/Controllers/Dtos/DeliveryDtos.cs`
- In-process events: `GalaxyDelivery/Events/*`
- Unit tests: `UnitTests/*`



## Areas for improvement

- Can add Authentication and authorization.
- On Delivery Complete/Failure event, the message can be added to a message queue (preferably Kafka).
- Can add 'Delivery Return' feature.
- Caching can be done for repeated requests (preferably using Redis)
- Can use HTTPS to improve security.
- To control data flow Pagination can be implemented.
- To control traffic Rate Limiting can be implemented (in API gateway).
- Driver, Vehicle, Route, Checkpoint, DeliveryEvent can be stopped from deletion if it is used already in any delivery.
- We should not delete any objects permanently (Data retention policy) instead we can marked it as 'Deleted/Disabled'.
- API can be containerized to deploy to different platforms.

