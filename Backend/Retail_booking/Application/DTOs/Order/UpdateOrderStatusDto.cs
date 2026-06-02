using System.ComponentModel.DataAnnotations;

namespace RetailOrdering.Application.DTOs.Order;

public class UpdateOrderStatusDto
{
    [Required]
    public string Status { get; set; } = null!;
}