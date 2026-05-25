namespace RetailOrdering.Domain.Constants
{
    public static class RoleConstants
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public const string Vendor = "Vendor";
        public const string Moderator = "Moderator";
        public const string Support = "Support";

        public static readonly string[] AllRoles = new[] { Admin, Customer, Vendor, Moderator, Support };
        public static readonly string[] AdminRoles = new[] { Admin, Moderator };
        public static readonly string[] CustomerRoles = new[] { Customer };
    }
}
