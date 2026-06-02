using Microsoft.EntityFrameworkCore;
using RetailOrdering.Domain.Entities;

namespace RetailOrdering.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Framework Database Table Registers
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Packaging> Packagings { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Coupon> Coupons { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<LoyaltyPoint> LoyaltyPoints { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<EmailLog> EmailLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed System Identity Security Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            );

            // Seed Initial Master Administrator User Profile
            var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!");
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@retail.com",
                    PasswordHash = adminPasswordHash,
                    RoleId = 1
                }
            );

            // Assert Unique Constraints on User Accounts
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            // Seed Sample Data lookup tables
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Pizza" },
                new Category { Id = 2, Name = "Cold Drinks" },
                new Category { Id = 3, Name = "Bread" }
            );

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "House" }
            );

            modelBuilder.Entity<Packaging>().HasData(
                new Packaging { Id = 1, Type = "Box" }
            );

            // Seed Catalog Storage Products items
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Margherita Pizza",
                    Description = "Classic pizza",
                    Price = 9.99m,
                    StockQty = 10,
                    CategoryId = 1,
                    BrandId = 1,
                    PackagingId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Coke",
                    Description = "Cold drink",
                    Price = 1.99m,
                    StockQty = 50,
                    CategoryId = 2,
                    BrandId = 1,
                    PackagingId = 1
                }
            );
        }
    }
}