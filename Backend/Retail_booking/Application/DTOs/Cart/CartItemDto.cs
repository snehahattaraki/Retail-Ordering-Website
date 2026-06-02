namespace RetailOrdering.Application.DTOs.Cart;

public class CartItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
}
