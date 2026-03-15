# Nurmukhan Arlan & Kaltayev Taskyn
# GROUP: IT3-2303
# MIDTERM ASP.NET

# Task Tracker API

Task Tracker API is an ASP.NET Core microservice that implements the required task-management domain model, an in-memory repository, dependency injection, a controller-based Web API, and Docker support.

## Stack

- ASP.NET Core Web API
- C#
- In-memory repository for the initial implementation
- Docker multi-stage build
- Docker Compose for local orchestration

## Project Structure

- `Domain` - entities, enums, events, and LINQ-based filtering service
- `Repositories` - repository abstraction and in-memory implementation
- `Controllers` - HTTP API endpoints
- `Contracts` - request and response models
- `Infrastructure` - seed data
- `Extensions` - DTO mappings using pattern matching

## Requirements Coverage

### Phase 1: Domain Model

- `BaseTask` is abstract and contains `Id`, `Title`, `CreatedAt`, and `IsCompleted`.
- `Id` and `CreatedAt` are assigned only during construction.
- `BugReportTask` extends `BaseTask` with `SeverityLevel`.
- `FeatureRequestTask` extends `BaseTask` with `EstimatedHours`.
- `BaseTask` declares a delegate and `OnTaskCompleted` event.
- `CompleteTask()` updates status and raises `OnTaskCompleted` exactly once.
- `TaskFilterService` uses LINQ to return:
  - incomplete high-severity bug reports ordered newest to oldest
  - the total estimated hours of incomplete feature requests

### Phase 2: API Layer

Required endpoints:

- `GET /api/tasks` - returns all tasks
- `POST /api/tasks/bug` - creates a bug report
- `PUT /api/tasks/{id}/complete` - completes a task and triggers the completion event

Additional convenience endpoints:

- `GET /api/tasks/{id}` - returns a single task by id
- `GET /api/tasks/insights` - exposes the LINQ filter summary
- `POST /api/tasks/feature` - creates a feature request

Architecture notes:

- `ITaskRepository` is injected into `TasksController`
- controller logic is separated from repository/data access logic
- `InMemoryTaskRepository` subscribes to `OnTaskCompleted` and logs completions

## Running Locally

### With the .NET SDK

```bash
dotnet restore
dotnet run
```

The API starts on `http://localhost:5081` when using the included launch profile.

### With Docker

```bash
docker compose up --build
```

The container exposes the API on `http://localhost:8080`.

## Sample Requests

Get all tasks:

```bash
curl http://localhost:8080/api/tasks
```

Create a bug report:

```bash
curl -X POST http://localhost:8080/api/tasks/bug   -H "Content-Type: application/json"   -d '{
    "title": "Editing task priority returns 500",
    "severityLevel": "High"
  }'
```

Complete a task:

```bash
curl -X PUT http://localhost:8080/api/tasks/<task-id>/complete
```

## Integration Recommendation for a Future NotificationService

Use asynchronous integration for task-completion notifications.

 flow:

1. `Task Service` marks the task as completed.
2. The service publishes a `TaskCompleted` integration event.
3. `NotificationService` consumes the event and sends the email.
