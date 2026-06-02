using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailOrdering.Infrastructure.Data;
using RetailOrdering.Domain.Entities;
using RetailOrdering.API.Responses;

namespace RetailOrdering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BrandsController(AppDbContext context)
    {
        _context = context;
    }

    // 1. Public endpoint to load all brands for storefront filters
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var brands = await _context.Brands.OrderBy(b => b.Name).ToListAsync();
        return Ok(ApiResponse<object>.SuccessResponse(brands, "Brands loaded"));
    }

    // 2. Admin/Management only endpoint to add a new brand
    [Authorize(Roles = "Admin,Management")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Brand brand)
    {
        if (string.IsNullOrWhiteSpace(brand.Name))
            return BadRequest(ApiResponse<string>.FailResponse("Brand name is required"));

        await _context.Brands.AddAsync(brand);
        await _context.SaveChangesAsync();
        return Ok(ApiResponse<Brand>.SuccessResponse(brand, "Brand added successfully"));
    }

    // 3. Admin only endpoint to remove a brand
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var brand = await _context.Brands.FindAsync(id);
        if (brand == null) return NotFound(ApiResponse<string>.FailResponse("Brand not found"));

        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();
        return Ok(ApiResponse<string>.SuccessResponse(null, "Brand removed"));
    }
}