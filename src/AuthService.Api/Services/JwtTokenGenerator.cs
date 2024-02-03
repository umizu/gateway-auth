using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Api.Services;

public class JwtTokenGenerator
{
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fvYxIAq9nrNIMNRh8E5Sh8kmvOJXfPjw"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "auth_service",
            audience: "gateway_auth_demo",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}