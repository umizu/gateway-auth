namespace AuthService.Api.Services;

public record AuthResult(
    bool IsSuccess,
    string? AccessToken = null,
    string? TokenType = null,
    int ExpiresIn = 0);