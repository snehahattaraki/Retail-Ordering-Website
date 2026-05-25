namespace RetailOrdering.Domain.Enums
{
    public enum OrderStatus
    {
        Pending = 1,
        Confirmed = 2,
        Processing = 3,
        Shipped = 4,
        OutForDelivery = 5,
        Delivered = 6,
        Cancelled = 7,
        OnHold = 8,
        Failed = 9,
        Returned = 10
    }
}
