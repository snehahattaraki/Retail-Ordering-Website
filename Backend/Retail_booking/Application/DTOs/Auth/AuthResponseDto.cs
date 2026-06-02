namespace RetailOrdering.Application.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}
