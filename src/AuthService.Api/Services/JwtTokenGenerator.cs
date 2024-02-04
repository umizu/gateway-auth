using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Api.Services;

public class JwtTokenGenerator
{
    private readonly IConfiguration _config;

    public JwtTokenGenerator(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(Guid userId, string username, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new("sub", userId.ToString()),
            new("username", username)
        };

        if (roles.Count == 0)
            roles.Add("user");
        claims.AddRange(roles.Select(role => new Claim("role", role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"]!,
            audience: _config["Jwt:Audience"]!,
            claims: claims,
            expires: DateTime.Now.AddSeconds(int.Parse(_config["Jwt:ExpiresIn"]!)),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}