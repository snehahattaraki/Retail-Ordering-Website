using System.ComponentModel.DataAnnotations;

namespace RetailOrdering.Application.DTOs.Inventory;

public class InventoryDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string? Category { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int StockQty { get; set; }
    public int? BrandId { get; set; }
    public string? ImageUrl { get; set; }
}