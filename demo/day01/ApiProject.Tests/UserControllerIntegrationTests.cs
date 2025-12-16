using System.Net;
using System.Net.Http.Json;
using ApiProject.Data;
using ApiProject.Models;
using ApiProject.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace ApiProject.Tests;

public class UserControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public UserControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private HttpClient CreateClientWithInMemoryDb(Action<ApplicationDbContext>? seedAction = null)
    {
        var dbName = $"TestDb_{Guid.NewGuid()}";
        
        var factory = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Remove the existing DbContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add in-memory database for testing
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase(dbName);
                });
            });
        });

        // Seed data if needed
        if (seedAction != null)
        {
            using var scope = factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
            seedAction(context);
        }

        var client = factory.CreateClient();
        return client;
    }

    [Fact]
    public async Task RegisterUser_SuccessCase_ReturnsCreated()
    {
        // Arrange - Use in-memory database with no existing users
        var client = CreateClientWithInMemoryDb();

        var request = new RegisterUserRequest
        {
            Username = "somkiat",
            Password = "1234"
        };

        // Act
        var response = await client.PostAsJsonAsync("/user", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        Assert.NotNull(result);
        Assert.Equal("Success", result.Message);
    }

    [Fact]
    public async Task RegisterUser_UsernameExists_ReturnsConflict()
    {
        // Arrange - Seed database with existing user
        var client = CreateClientWithInMemoryDb(context =>
        {
            var existingUser = new User
            {
                Username = "somkiat",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("existing_password"),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            context.Users.Add(existingUser);
            context.SaveChanges();
        });

        var request = new RegisterUserRequest
        {
            Username = "somkiat",
            Password = "1234"
        };

        // Act
        var response = await client.PostAsJsonAsync("/user", request);

        // Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        Assert.NotNull(result);
        Assert.Equal("Invalid input", result.Message);
    }

    [Fact]
    public async Task RegisterUser_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var client = CreateClientWithInMemoryDb();
        var request = new RegisterUserRequest
        {
            Username = "", // Invalid: empty username
            Password = "1234"
        };

        // Act
        var response = await client.PostAsJsonAsync("/user", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
