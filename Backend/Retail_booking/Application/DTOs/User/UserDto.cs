using System.ComponentModel.DataAnnotations;

namespace RetailOrdering.Application.DTOs.User;

public class UserDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string Role { get; set; } = "Customer";
}