# ToDoApi

A simple RESTful API for managing to-do items built with .NET 9 and ASP.NET Core.

## Overview

ToDoApi is a lightweight API that allows you to create, read, update, and delete to-do items. Each to-do item includes a title, optional description, completion status, and due date tracking. The API uses an in-memory database for quick setup and testing.

## Features

- **Full CRUD Operations**: Create, read, update, and delete to-do items
- **.NET 9**: Built on the latest .NET framework
- **Entity Framework Core**: With in-memory database for easy development and testing
- **OpenAPI Documentation**: Built-in API documentation with OpenAPI
- **Data Validation**: Ensures required fields are properly validated
- **Auto-populated Dates**: Automatically sets creation date on new items

## Prerequisites

- .NET 9 SDK or later
- Visual Studio Code, Visual Studio, or your preferred IDE

## Getting Started

### Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd ToDoApi
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

### Running the Application

Start the API server:

```bash
dotnet run
```

The API will be available at `http://localhost:5000` (or the configured port in your environment).

## API Endpoints

All endpoints are prefixed with `/api/todos`

### Get All To-Dos

```
GET /api/todos
```

Returns a list of all to-do items.

**Response:**
```json
[
  {
    "id": 1,
    "createDate": "2025-02-04",
    "title": "Complete project",
    "description": "Finish the ToDoApi project",
    "isCompleted": false,
    "dueDate": "2025-02-15"
  }
]
```

### Get To-Do by ID

```
GET /api/todos/{id}
```

Returns a specific to-do item by its ID.

**Parameters:**
- `id` (integer): The ID of the to-do item

**Response:** 200 OK with the to-do object, or 404 Not Found if not found.

### Create a To-Do

```
POST /api/todos
Content-Type: application/json

{
  "title": "Buy groceries",
  "description": "Get milk, eggs, and bread",
  "dueDate": "2025-02-05"
}
```

Creates a new to-do item. The `title` field is required; `description` and `dueDate` are optional.

**Response:** 201 Created with the created to-do object and location header.

### Update a To-Do

```
PUT /api/todos/{id}
Content-Type: application/json

{
  "id": 1,
  "title": "Updated title",
  "description": "Updated description",
  "isCompleted": true,
  "dueDate": "2025-02-20"
}
```

Updates an existing to-do item. The `createDate` field cannot be modified.

**Parameters:**
- `id` (integer): The ID of the to-do item to update

**Response:** 204 No Content on success, or 404 Not Found if not found.

### Delete a To-Do

```
DELETE /api/todos/{id}
```

Deletes a to-do item.

**Parameters:**
- `id` (integer): The ID of the to-do item to delete

**Response:** 204 No Content on success, or 404 Not Found if not found.

## Data Model

### ToDo

| Field | Type | Required | Notes |
|-------|------|----------|-------|
| `id` | integer | Auto-generated | Primary key |
| `createDate` | DateOnly | Auto-set | Date item was created (cannot be modified) |
| `title` | string | Yes | Cannot be empty or whitespace |
| `description` | string? | No | Optional description |
| `isCompleted` | boolean | No | Defaults to false |
| `dueDate` | DateOnly? | No | Optional due date |

## Configuration

The API uses an in-memory database by default. Configuration can be found in:

- **Program.cs**: Main application setup and service configuration
- **appsettings.json**: Application settings
- **appsettings.Development.json**: Development-specific settings

## Testing

You can test the API using:

- **Visual Studio Code REST Client**: Use the `ToDoApi.http` file to make test requests
- **Postman**: Import the API endpoints and test them
- **cURL**: Use command-line HTTP requests
- **OpenAPI UI**: Available in development mode at the configured OpenAPI endpoint

## Project Structure

```
ToDoApi/
├── Controllers/
│   └── ToDoController.cs      # API endpoint definitions
├── Data/
│   └── DataSeedExtension.cs   # Data seeding for development
├── ToDo.cs                     # To-do model class
├── ToDoDbContext.cs            # Entity Framework context
├── Program.cs                  # Application startup configuration
├── ToDoApi.csproj              # Project file
└── README.md                   # This file
```

## Dependencies

- **Microsoft.AspNetCore.OpenApi** (9.0.12): OpenAPI support
- **Microsoft.EntityFrameworkCore** (9.0.12): ORM framework
- **Microsoft.EntityFrameworkCore.InMemory** (9.0.12): In-memory database provider

## Development

### Data Seeding

Sample data can be seeded into the database on startup through the `DataSeedExtension` class. This is useful for development and testing.

## License

Include your license information here.

## Support

For issues or questions, please create an issue in the repository.
