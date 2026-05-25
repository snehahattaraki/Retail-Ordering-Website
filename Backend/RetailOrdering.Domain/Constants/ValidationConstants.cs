namespace RetailOrdering.Domain.Constants
{
    public static class ValidationConstants
    {
        // User Validation
        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 128;
        public const int MinNameLength = 2;
        public const int MaxNameLength = 100;
        public const int MaxEmailLength = 256;
        public const int MaxPhoneNumberLength = 20;
        public const int MinPhoneNumberLength = 7;

        // Product Validation
        public const int MinProductNameLength = 3;
        public const int MaxProductNameLength = 200;
        public const int MaxProductDescriptionLength = 2000;
        public const int MaxSkuLength = 50;
        public const decimal MinProductPrice = 0.01m;
        public const decimal MaxProductPrice = 999999.99m;

        // Category Validation
        public const int MinCategoryNameLength = 2;
        public const int MaxCategoryNameLength = 100;
        public const int MaxCategoryDescriptionLength = 500;

        // Brand Validation
        public const int MinBrandNameLength = 2;
        public const int MaxBrandNameLength = 100;
        public const int MaxBrandDescriptionLength = 500;

        // Coupon Validation
        public const int MinCouponCodeLength = 3;
        public const int MaxCouponCodeLength = 50;
        public const int MaxCouponDescriptionLength = 500;
        public const decimal MinCouponDiscountAmount = 0.01m;
        public const decimal MaxCouponDiscountPercentage = 100m;

        // Order Validation
        public const int MinOrderItems = 1;
        public const int MaxOrderItems = 1000;
        public const int MinAddressLength = 5;
        public const int MaxAddressLength = 500;

        // Inventory Validation
        public const int MinQuantity = 0;
        public const int MaxQuantity = 999999;
        public const int MinReorderLevel = 1;

        // Loyalty Points
        public const int MinLoyaltyPoints = 0;
        public const int MaxLoyaltyPoints = 999999;

        // Regex Patterns
        public const string EmailRegexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        public const string PhoneNumberRegexPattern = @"^\+?1?\d{9,15}$";
        public const string PasswordRegexPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$";
    }
}
