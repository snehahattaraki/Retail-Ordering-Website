using Microsoft.EntityFrameworkCore;
using RetailOrdering.Application.DTOs.Common;
using RetailOrdering.Application.DTOs.Product;
using RetailOrdering.Application.Interfaces;
using RetailOrdering.Infrastructure.Data;
using AutoMapper;

namespace RetailOrdering.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginationResponseDto<ProductDto>> GetProductsAsync(int page, int pageSize, string? search, string? category, string? brand)
    {
        // FIX: Explicitly include Category and Brand tables
        var query = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search)) query = query.Where(p => p.Name.Contains(search));
        if (!string.IsNullOrEmpty(category)) query = query.Where(p => p.Category.Name == category);
        if (!string.IsNullOrEmpty(brand)) query = query.Where(p => p.Brand.Name == brand);

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        var dtos = _mapper.Map<IEnumerable<ProductDto>>(items);

        return new PaginationResponseDto<ProductDto>
        {
            TotalItems = total,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(total / (double)pageSize),
            Items = dtos
        };
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var p = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (p == null) return null;
        return _mapper.Map<ProductDto>(p);
    }

    public async Task<IEnumerable<string>> GetBrandsAsync()
    {
        return await _context.Brands.Select(b => b.Name).Distinct().ToListAsync();
    }

    public async Task<IEnumerable<string>> GetCategoriesAsync()
    {
        return await _context.Categories.Select(c => c.Name).Distinct().ToListAsync();
    }
}