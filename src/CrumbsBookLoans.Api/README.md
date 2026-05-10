# CrumbsBookLoans API

ASP.NET Core Web API for the CrumbsBookLoans book lending library.
## Requirements

- .NET 10 SDK
- `dotnet-ef` tool for migrations

Install the EF Core CLI tool if you don't have it:

```bash
dotnet tool install --global dotnet-ef
```

## Running

```bash
dotnet run --project src/CrumbsBookLoans.Api
```

```vscode
Use the task to build, and the debugger tooling to run either the FE, BE or both
```

API is available at `https://localhost:5001`. Migrations are applied automatically on startup.

In development, the Scalar API reference is available at `https://localhost:5001/scalar/v1` (this is super cool, new since I've been using .net core APIs).

## Migrations

Create a new migration after changing the data model:

```bash
dotnet ef migrations add <MigrationName> --project src/CrumbsBookLoans.Api
```

Apply pending migrations:

```bash
dotnet ef database update --project src/CrumbsBookLoans.Api
```

The SQLite database file (`bookloans.db`) is created in the project directory on first run. Delete it to reset back to seed data — the next startup will recreate it.

## Endpoints

```
GET    /api/books              returns all books
POST   /api/books              create a book
DELETE /api/books/{id}         delete a book
POST   /api/books/{id}/loan    mark a book as borrowed
POST   /api/books/{id}/return  mark a book as returned
```

## Project Structure

```
Controllers/        API controllers
Data/               EF Core DbContext and migrations
Entities/           Database entity models
Models/             Request and response DTOs
```

## Production Build

Build the React frontend and copy the output to `wwwroot/` before running in production:

```bash
cd src/CrumbsBookLoans.Client
npm run build
cp -r dist/* ../CrumbsBookLoans.Api/wwwroot/
```

ASP.NET Core then serves the React app from `wwwroot/` alongside the API.
