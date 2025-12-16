using ApiProject.Models;
using ApiProject.Repositories;

namespace ApiProject.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<(bool Success, string Message)> RegisterUserAsync(string username, string password)
    {
        // Check if username already exists
        var existingUser = await _userRepository.GetByUsernameAsync(username);

        if (existingUser != null)
        {
            return (false, "Invalid input");
        }

        // Hash the password using BCrypt
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        // Create new user
        var newUser = new User
        {
            Username = username,
            PasswordHash = passwordHash
        };

        await _userRepository.CreateAsync(newUser);

        return (true, "Success");
    }
}
