using AuthService.Api.Contracts;
using AuthService.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<UserService>()
    .AddSingleton<JwtTokenGenerator>()
    .AddSingleton<AuthenticationService>();

var app = builder.Build();

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
    logger.LogInformation("Authorize request received");
    ctx.Request.Headers.Append("X-User-Id", "sample user id"); // todo: get user id from token
    return Results.Ok();
});

app.Run();
