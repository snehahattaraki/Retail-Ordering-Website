namespace RetailOrdering.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending = 1,
        Processing = 2,
        Completed = 3,
        Failed = 4,
        Cancelled = 5,
        Refunded = 6,
        PartiallyRefunded = 7,
        Expired = 8,
        Declined = 9,
        OnHold = 10
    }
}
