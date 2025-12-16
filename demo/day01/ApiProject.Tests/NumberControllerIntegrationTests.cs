using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ApiProject.Services;
using Moq;

namespace ApiProject.Tests;

public class NumberControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public NumberControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetData_ReturnsSuccess_WithStubbedRandomService()
    {
        // Arrange
        var mockRandomService = new Mock<RandomService>();
        mockRandomService.Setup(s => s.Get()).Returns(5);

        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Remove the existing RandomService registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(RandomService));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add the mocked service
                services.AddScoped<RandomService>(_ => mockRandomService.Object);
            });
        }).CreateClient();

        // Act
        var response = await client.GetAsync("/data");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var content = await response.Content.ReadAsStringAsync();
        var jsonDocument = JsonDocument.Parse(content);
        var data = jsonDocument.RootElement.GetProperty("data").GetString();
        
        Assert.Equal("ABC5", data);
    }
}
