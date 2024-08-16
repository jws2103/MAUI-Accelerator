namespace MauiAccelerator.Features.Login.Models;

public class TokenResponse
{
    public long? Id { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public string? Image { get; set; }

    public required string Token { get; set; }

    public required string RefreshToken { get; set; }
}