# PostgreSQL Database Setup

This project uses PostgreSQL 18 with Entity Framework Core for data persistence.

## Database Configuration

### Connection Strings

**Development:**
```
Host=localhost;Port=5432;Database=userdb_dev;Username=postgres;Password=postgres
```

**Production:**
```
Host=localhost;Port=5432;Database=userdb;Username=postgres;Password=postgres
```

## Running PostgreSQL with Docker

Start PostgreSQL:
```bash
docker-compose up -d postgres
```

Stop PostgreSQL:
```bash
docker-compose down
```

## Database Migrations

### Create a new migration
```bash
cd ApiProject
dotnet ef migrations add MigrationName
```

### Apply migrations
```bash
cd ApiProject
dotnet ef database update
```

### Remove last migration (if not applied)
```bash
cd ApiProject
dotnet ef migrations remove
```

## Database Schema

The `users` table stores user registration information:

| Column        | Type         | Constraints           |
|---------------|--------------|----------------------|
| user_id       | INT          | PRIMARY KEY, AUTO    |
| username      | VARCHAR(100) | UNIQUE, NOT NULL     |
| password_hash | VARCHAR(255) | NOT NULL             |
| created_at    | TIMESTAMP    | DEFAULT CURRENT_TIME |
| updated_at    | TIMESTAMP    | DEFAULT CURRENT_TIME |

## Testing

Integration tests use an in-memory database to avoid dependencies on the actual PostgreSQL instance.

```bash
dotnet test --filter "FullyQualifiedName~UserControllerIntegrationTests"
```
