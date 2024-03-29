using System.Security.Claims;
using System.Text;
using AuthService.Api;
using AuthService.Api.Contracts;
using AuthService.Api.Middlewares;
using AuthService.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
    {
        opts.Audience = builder.Configuration["Jwt:Audience"];
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
    });
builder.Services.AddAuthorization(cfg =>
{
    cfg.AddPolicy("Bearer", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser();
    });
});

builder.Services.AddSingleton<UserService>()
    .AddSingleton<JwtTokenGenerator>()
    .AddSingleton<AuthenticationService>();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<InjectUserContextMiddleware>();

app.MapPost("/auth/login", (
    LoginRequest req,
    AuthenticationService authService) =>
{
    var result = authService.Login(req.Username, req.Password);

    if (!result.IsSuccess) return Results.Unauthorized();

    return Results.Ok(new LoginResponse
    {
        AccessToken = result.AccessToken!,
        TokenType = result.TokenType!,
        ExpiresIn = result.ExpiresIn
    });
});

app.MapGet("/auth/forward-auth",
    (HttpContext ctx,
    ILogger<Program> logger) =>
{
    return Results.Ok();
}).RequireAuthorization("Bearer");

app.Run();
