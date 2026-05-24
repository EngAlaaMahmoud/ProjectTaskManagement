# Project Task Management API

A clean architecture ASP.NET Core Web API for managing projects and tasks with JWT authentication.

## Architecture

- `src/ProjectTaskManagement.Domain` - Domain entities, enums, and interfaces
- `src/ProjectTaskManagement.Application` - DTOs, CQRS commands/queries, MediatR handlers, FluentValidation
- `src/ProjectTaskManagement.Infrastructure` - EF Core DbContext, repositories, JWT service, database seeding
- `src/ProjectTaskManagement.API` - Controllers, middleware, API configuration
- `tests/ProjectTaskManagement.Tests` - xUnit tests for application handlers

## Requirements

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core with SQL Server
- JWT Authentication
- Clean Architecture
- MediatR + CQRS
- Docker support

## Running locally

1. Install .NET 9 SDK.
2. Start SQL Server using Docker compose:

```bash
docker compose up -d
```

3. Update `src/ProjectTaskManagement.API/appsettings.json` if needed.
4. Run the application:

```bash
dotnet run --project src/ProjectTaskManagement.API/ProjectTaskManagement.API.csproj
```

5. Use Swagger at `http://localhost:5000/swagger`.

## API Endpoints

### Auth
- `POST /api/v1/auth/register`
- `POST /api/v1/auth/login`

### Projects
- `POST /api/v1/projects`
- `GET /api/v1/projects`
- `GET /api/v1/projects/{id}`
- `PUT /api/v1/projects/{id}`
- `DELETE /api/v1/projects/{id}`

### Tasks
- `POST /api/v1/projects/{projectId}/tasks`
- `GET /api/v1/projects/{projectId}/tasks`
- `PUT /api/v1/projects/{projectId}/tasks/{id}`
- `DELETE /api/v1/projects/{projectId}/tasks/{id}`

## Database migrations

To apply database migrations:

```bash
dotnet ef database update --project src/ProjectTaskManagement.Infrastructure/ProjectTaskManagement.Infrastructure.csproj --startup-project src/ProjectTaskManagement.API/ProjectTaskManagement.API.csproj
```

## Testing

```bash
dotnet test ProjectTaskManagement.sln
```

## Notes

- JWT secret in `appsettings.json` should be replaced with a secure key in production.
- Seed user: `demo@local.test` / `P@ssw0rd!`.
