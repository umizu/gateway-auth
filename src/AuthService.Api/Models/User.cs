namespace AuthService.Api.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!; // hash in real case 
    public List<string> Roles { get; set; } = [];
}