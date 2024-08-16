namespace MauiAccelerator.Features.Login.Models;

public class TokenRequest
{
    public required string Username { get; set; }
    
    public required string Password { get; set; }
}