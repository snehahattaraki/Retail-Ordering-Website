using RetailOrdering.Application.Interfaces;
using RetailOrdering.Domain.Entities;
using RetailOrdering.Infrastructure.Data;
using RetailOrdering.Infrastructure.Interfaces;

namespace RetailOrdering.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Roles = new GenericRepository<Role>(_context);
        Users = new GenericRepository<User>(_context);
        Categories = new GenericRepository<Category>(_context);
        Brands = new GenericRepository<Brand>(_context);
        Packagings = new GenericRepository<Packaging>(_context);
        Products = new GenericRepository<Product>(_context);
        Carts = new GenericRepository<Cart>(_context);
        CartItems = new GenericRepository<CartItem>(_context);
        Coupons = new GenericRepository<Coupon>(_context);
        Orders = new GenericRepository<Order>(_context);
        OrderItems = new GenericRepository<OrderItem>(_context);
        LoyaltyPoints = new GenericRepository<LoyaltyPoint>(_context);
        Payments = new GenericRepository<Payment>(_context);
        EmailLogs = new GenericRepository<EmailLog>(_context);
    }

    public IGenericRepository<Role> Roles { get; }
    public IGenericRepository<User> Users { get; }
    public IGenericRepository<Category> Categories { get; }
    public IGenericRepository<Brand> Brands { get; }
    public IGenericRepository<Packaging> Packagings { get; }
    public IGenericRepository<Product> Products { get; }
    public IGenericRepository<Cart> Carts { get; }
    public IGenericRepository<CartItem> CartItems { get; }
    public IGenericRepository<Coupon> Coupons { get; }
    public IGenericRepository<Order> Orders { get; }
    public IGenericRepository<OrderItem> OrderItems { get; }
    public IGenericRepository<LoyaltyPoint> LoyaltyPoints { get; }
    public IGenericRepository<Payment> Payments { get; }
    public IGenericRepository<EmailLog> EmailLogs { get; }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
