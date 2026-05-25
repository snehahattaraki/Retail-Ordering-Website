namespace RetailOrdering.Application.DTOs.Inventory
{
    public class StockValidationDto
    {
        public int ProductId { get; set; }
        public int RequiredQuantity { get; set; }
    }
}
