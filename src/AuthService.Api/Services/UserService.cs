/// <summary>
/// in mem user service implementation
/// </summary>

using AuthService.Api.Models;

namespace AuthService.Api.Services;

public class UserService
{
    private static List<User> _users = new List<User>()
    {
        new() { Id = Guid.NewGuid(), Username = "mod", Password = "mod", Roles = ["mod"] },
        new() { Id = Guid.NewGuid(), Username = "user", Password = "user"},
    };

    public User? GetByUsername(string username)
    {
        return _users.FirstOrDefault(x => x.Username == username);
    }
}
