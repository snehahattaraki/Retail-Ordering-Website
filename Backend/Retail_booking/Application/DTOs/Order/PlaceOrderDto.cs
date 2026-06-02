using System.ComponentModel.DataAnnotations;
using RetailOrdering.Application.DTOs.Order;

namespace RetailOrdering.Application.DTOs.Order;

public class PlaceOrderItemDto
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }
}

public class PlaceOrderDto
{
    [Required]
    public string ShippingAddress { get; set; } = null!;

    [Required]
    public string Phone { get; set; } = null!;

    [Required]
    public string PaymentMethod { get; set; } = "COD";

    [Required]
    public List<PlaceOrderItemDto> Items { get; set; } = new();
}