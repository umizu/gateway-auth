using AuthService.Api.Contracts;
using AuthService.Api.Models;

namespace AuthService.Api.Services;

public class AuthenticationService
{
    private readonly UserService _userService;
    private readonly JwtTokenGenerator _tokenGenerator;

    public AuthenticationService(UserService userService, JwtTokenGenerator tokenGenerator)
    {
        _userService = userService;
        _tokenGenerator = tokenGenerator;
    }

    public AuthResult Login(string username, string pass)
    {
        if (_userService.GetByUsername(username)
            is not User user) return new AuthResult(false);

        if (!user.Password.Equals(pass))
            return new AuthResult(false);


        var token = _tokenGenerator.GenerateToken(
            user.Id,
            user.Username,
            user.Roles);

        return new AuthResult(true, token, "Bearer", 300);
    }
}