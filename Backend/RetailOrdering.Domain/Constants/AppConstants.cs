namespace RetailOrdering.Domain.Constants
{
    public static class AppConstants
    {
        // Pagination
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;
        public const int MinPageSize = 1;

        // Application Settings
        public const string ApplicationName = "RetailOrdering";
        public const string ApplicationVersion = "1.0.0";
        public const string ApiVersion = "v1";

        // File Upload
        public const int MaxFileUploadSize = 5242880; // 5 MB
        public const string UploadFolderPath = "/uploads";
        public const string ProductImageFolder = "products";
        public const string InvoiceFolder = "invoices";
        public const string ProfileImageFolder = "profiles";

        // Email Settings
        public const string EmailFromAddress = "noreply@retailordering.com";
        public const string EmailFromName = "Retail Ordering System";

        // Discount and Pricing
        public const decimal MaxDiscountPercentage = 100m;
        public const decimal MinDiscountPercentage = 0m;
        public const decimal MaxTaxPercentage = 100m;

        // Order Related
        public const int OrderNumberPrefixLength = 6;
        public const string OrderNumberPrefix = "ORD";

        // JWT Token
        public const int JwtTokenExpirationMinutes = 60;
        public const int RefreshTokenExpirationDays = 7;

        // Cache Keys
        public const string CacheKeyProducts = "Products_Cache";
        public const string CacheKeyCategories = "Categories_Cache";
        public const string CacheKeyBrands = "Brands_Cache";
        public const string CacheKeyPromotions = "Promotions_Cache";
    }
}
