using Microsoft.EntityFrameworkCore;
using RetailOrdering.Application.DTOs.Inventory;
using RetailOrdering.Application.Interfaces;
using RetailOrdering.Infrastructure.Data;
using AutoMapper;

namespace RetailOrdering.Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public InventoryService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InventoryDto>> GetAllAsync()
    {
        // FIX: Included Category and Brand to prevent AutoMapper null reference crashes
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .ToListAsync();
        return _mapper.Map<IEnumerable<InventoryDto>>(products);
    }

    public async Task<InventoryDto?> GetByIdAsync(int id)
    {
        var p = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (p == null) return null;
        return _mapper.Map<InventoryDto>(p);
    }

    public async Task<InventoryDto> CreateAsync(InventoryDto dto)
    {
        // 1. Ensure Category exists, if not, create it on the fly!
        var categoryName = string.IsNullOrWhiteSpace(dto.Category) ? "General" : dto.Category;
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
        if (category == null)
        {
            category = new RetailOrdering.Domain.Entities.Category { Name = categoryName };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync(); // Generates the new valid ID
        }

        // 2. Ensure at least one Brand exists
        var brand = await _context.Brands.FirstOrDefaultAsync();
        if (brand == null)
        {
            brand = new RetailOrdering.Domain.Entities.Brand { Name = "In-House" };
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync(); // Generates the new valid ID
        }

        var product = new RetailOrdering.Domain.Entities.Product
        {
            Name = dto.Name,
            Price = dto.Price,
            StockQty = dto.StockQty,
            CategoryId = category.Id, // 100% guaranteed to be a valid Foreign Key now
            BrandId = brand.Id
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync(); // Will no longer crash!

        return _mapper.Map<InventoryDto>(product);
    }

    public async Task<bool> UpdateAsync(int id, InventoryDto dto)
    {
        var p = await _context.Products.FindAsync(id);
        if (p == null) return false;

        p.Name = dto.Name;
        p.Price = dto.Price;
        p.StockQty = dto.StockQty;
        p.BrandId = dto.BrandId ?? p.BrandId;
        p.ImageUrl = dto.ImageUrl ?? p.ImageUrl;

        // Handle category updates dynamically
        var categoryName = string.IsNullOrWhiteSpace(dto.Category) ? "General" : dto.Category;
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
        if (category == null)
        {
            category = new RetailOrdering.Domain.Entities.Category { Name = categoryName };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        p.CategoryId = category.Id;

        _context.Products.Update(p);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var p = await _context.Products.FindAsync(id);
        if (p == null) return false;
        _context.Products.Remove(p);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateStockAsync(int id, int stockQty)
    {
        var p = await _context.Products.FindAsync(id);
        if (p == null) return false;

        p.StockQty = stockQty;
        _context.Products.Update(p);
        await _context.SaveChangesAsync();
        return true;
    }
}