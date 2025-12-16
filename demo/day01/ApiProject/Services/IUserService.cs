using ApiProject.Models;

namespace ApiProject.Services;

public interface IUserService
{
    Task<(bool Success, string Message)> RegisterUserAsync(string username, string password);
}
