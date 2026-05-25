namespace RetailOrdering.API.Helpers
{
    public static class PaginationHelper
    {
        public static int CalculateTotalPages(int totalItems, int pageSize) => (int)Math.Ceiling((double)totalItems / pageSize);
    }
}
