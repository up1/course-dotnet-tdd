# Create REST API :: Get number

## Request 
GET /data

## Response 
code = 200
{
    "data": "ABC12"
}

## Project structure
ApiProject/
  + controllers/
    + NumberController.cs
  + services/
    + RandomService.cs

## RandomService.cs
* public int get()
* Random number 1-10 only

```mermaid
sequenceDiagram
    actor User
    participant Client as Client App
    participant Gateway as API Gateway
    participant Auth as Auth Service
    participant LDAP as LDAP

    User->>Client: Login with username & password
    Client->>Gateway: Submit login form
    Gateway->>Auth: Redirect to Auth Service
    Auth->>LDAP: Check credentials in LDAP
    LDAP-->>Auth: Return validation result
    Auth-->>Gateway: Send authentication result
    Gateway-->>Client: Return result
    Client-->>User: Display login result
```


# Testing with WebApplicationFactory
* Microsoft.AspNetCore.Mvc.Testing

## Project structure
* ApiProject.Test/
  * NumberControllerIntegrationTests
    * Success case :: ทำการ stub RandomService ให้ return 5
    * GET /data

