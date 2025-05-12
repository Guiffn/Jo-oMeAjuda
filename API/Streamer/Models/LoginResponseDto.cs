namespace Streamer.Models;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public User User { get; set; } = null!;
} 