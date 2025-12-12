# Hello Testing
* .NET C#
* WebAPI project
* Testing with xUnit and Moq

## Create project
```
$dotnet new webapi -n ApiProject
```

Add partial class in file `Program.cs`
```
public partial class Program { }
```

## Create XUnit prject
```
$dotnet new xunit -n ApiProject.Tests
```

## Add Project Reference
```
$dotnet add ApiProject.Tests/ApiProject.Tests.csproj reference ApiProject/ApiProject.csproj
```

## Run project
```
$dotnet run --project ApiProject/
```

Access to REST API
* http://localhost:5043/weatherforecast
* http://localhost:5043/openapi/v1.json

## Install dependencies in Test project
* Mocking library
```
$dotnet add ApiProject.Tests/ApiProject.Tests.csproj package Moq
$dotnet add ApiProject.Tests/ApiProject.Tests.csproj package Microsoft.AspNetCore.Mvc.Testing
```

## Write test case
* Arrange, Act, Assert (AAA) pattern

```
using Microsoft.AspNetCore.Mvc.Testing;

public class BasicIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BasicIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetEndpoints_ReturnsSuccessAndCorrectContentType()
    {
        // Act
        var response = await _client.GetAsync("/weatherforecast"); // Use an existing endpoint

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }
}
```

## Run integration test
```
$dotnet test ApiProject.Tests/ApiProject.Tests.csproj
```

## Install code coverage
```
$dotnet add ApiProject.Tests/ApiProject.Tests.csproj package coverlet.collector
```

Run test with code coverage report
```
$dotnet test ApiProject.Tests/ApiProject.Tests.csproj --collect:"XPlat Code Coverage"
```

Generate code coverage report
```
$dotnet tool install -g dotnet-reportgenerator-globaltool

$reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"CoverageReport" -reporttypes:Html
```