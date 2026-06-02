using RetailOrdering.Domain.Entities;
using RetailOrdering.Application.Interfaces;

namespace RetailOrdering.Infrastructure.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Role> Roles { get; }
    IGenericRepository<User> Users { get; }
    IGenericRepository<Category> Categories { get; }
    IGenericRepository<Brand> Brands { get; }
    IGenericRepository<Packaging> Packagings { get; }
    IGenericRepository<Product> Products { get; }
    IGenericRepository<Cart> Carts { get; }
    IGenericRepository<CartItem> CartItems { get; }
    IGenericRepository<Coupon> Coupons { get; }
    IGenericRepository<Order> Orders { get; }
    IGenericRepository<OrderItem> OrderItems { get; }
    IGenericRepository<LoyaltyPoint> LoyaltyPoints { get; }
    IGenericRepository<Payment> Payments { get; }
    IGenericRepository<EmailLog> EmailLogs { get; }

    Task<int> CompleteAsync();
}
