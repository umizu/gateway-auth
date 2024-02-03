using Newtonsoft.Json;

namespace AuthService.Api.Contracts;

public record LoginResponse
{
    [JsonProperty("access_token")]
    public required string AccessToken { get; init; }

    [JsonProperty("token_type")]
    public required string TokenType { get; init; }

    [JsonProperty("expires_in")]
    public required int ExpiresIn { get; init; }
}