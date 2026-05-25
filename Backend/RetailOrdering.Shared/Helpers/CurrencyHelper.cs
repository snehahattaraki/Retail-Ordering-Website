namespace RetailOrdering.Shared.Helpers
{
    public static class CurrencyHelper
    {
        public static string FormatCurrency(decimal amount)
        {
            return amount.ToString("C");
        }
    }
}
