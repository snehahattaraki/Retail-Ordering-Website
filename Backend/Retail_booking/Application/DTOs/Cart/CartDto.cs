namespace RetailOrdering.Application.DTOs.Cart;

public class CartDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<CartItemDto> CartItems { get; set; } = new();
    public decimal TotalAmount { get; set; }
}
