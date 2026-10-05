# Mini Issue Tracker

Mini Issue Tracker is a small full-stack issue tracking application built with C# and .NET.

I created this project as a learning and portfolio project to strengthen my understanding of the fundamentals of software development. The application was built step by step, starting with a simple domain model and console application and gradually expanding it with persistence, a REST API, a web frontend, business rules, and automated tests.

## Features

### Issue Management

- Create issues with a title, description, and type
- View all issues
- Edit open issues
- Delete open issues
- Assign a priority to an issue
- Change the status of an issue
- Store the creation date of an issue
- Persist issues in a SQLite database

### Roles

The frontend contains two simple roles to demonstrate different workflows.

**Reporter**

- Can create new issues
- Can edit an issue while its status is `Open`
- Can delete an issue while its status is `Open`

**Handler**

- Can update the description and type
- Can assign a priority
- Can change the issue status
- Cannot change the issue title

The role switch is intended to demonstrate application behavior and business rules. It is not an authentication or authorization system.

## Issue States

Issues can have one of the following statuses:

- `Open`
- `InProgress`
- `Done`

An issue can only be edited or deleted by the reporter while it is still `Open`.

## Technologies

### Backend

- C#
- .NET 10
- ASP.NET Core Minimal API
- Entity Framework Core
- SQLite

### Frontend

- HTML
- CSS
- JavaScript

### Testing

- xUnit
- Entity Framework Core integration tests
- ASP.NET Core API integration tests
- SQLite in-memory test databases

### Development

- Git
- GitHub
- .NET CLI

## Project Structure

```text
MiniIssueTracker
├── src
│   ├── MiniIssueTracker
│   │   ├── Data
│   │   └── Issues
│   ├── MiniIssueTracker.Api
│   │   └── Dtos
│   └── MiniIssueTracker.Web
│       ├── CSS
│       └── js
└── tests
    └── MiniIssueTracker.Tests
        ├── Api
        └── Data
```

The solution is divided into:

- **MiniIssueTracker** — domain model, business rules and data access
- **MiniIssueTracker.Api** — ASP.NET Core REST API
- **MiniIssueTracker.Web** — browser-based user interface
- **MiniIssueTracker.Tests** — unit and integration tests

## API Endpoints

| Method | Endpoint | Description |
| --- | --- | --- |
| GET | `/api/issues` | Get all issues |
| GET | `/api/issues/{id}` | Get an issue by ID |
| POST | `/api/issues` | Create a new issue |
| PUT | `/api/issues/{id}` | Edit an open issue |
| PUT | `/api/issues/{id}/handling` | Update issue handling information |
| DELETE | `/api/issues/{id}` | Delete an open issue |

## Running the Project

### 1. Start the API

From the repository root:

```bash
dotnet run --project src/MiniIssueTracker.Api
```

The API runs locally on:

```text
http://localhost:5203
```

### 2. Start the frontend

Open a second terminal:

```bash
cd src/MiniIssueTracker.Web
python -m http.server 5500
```

Then open:

```text
http://localhost:5500
```

## Running the Tests

From the repository root:

```bash
dotnet test
```

The project currently contains **22 automated tests** covering domain business rules, database persistence, and API behavior.

## What I Learned

This project gave me practical experience with:

- Designing a small domain model with classes and enums
- Implementing business rules in C#
- Working with Entity Framework Core and SQLite
- Building REST endpoints with ASP.NET Core
- Separating API models using DTOs
- Connecting a JavaScript frontend to a REST API
- Handling HTTP requests and errors in the frontend
- Managing UI state and role-dependent behavior
- Writing unit and integration tests
- Using an isolated SQLite in-memory database for integration testing
- Working incrementally with Git and GitHub

## Scope

Mini Issue Tracker is intentionally kept small.

The goal of version 1.0 is not to reproduce a full issue tracking platform such as Jira, but to demonstrate a complete application flow and the software development fundamentals behind it.

Features such as authentication, authorization, issue history, soft deletion, and more advanced architecture are intentionally outside the scope of version 1.0.
