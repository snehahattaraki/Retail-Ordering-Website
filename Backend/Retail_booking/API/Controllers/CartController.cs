using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailOrdering.Infrastructure.Data;
using RetailOrdering.Domain.Entities;
using RetailOrdering.API.Responses;
using System.Security.Claims;
using System.Linq;

namespace RetailOrdering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }

    private int GetUserId() => int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

    // ==========================================
    // NEW ENDPOINTS (For Angular Syncing)
    // ==========================================

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = GetUserId();
        var cart = await _context.Carts
            .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        // If the cart doesn't exist, return an empty array for the frontend
        if (cart == null)
            return Ok(ApiResponse<object>.SuccessResponse(new List<object>()));

        // Map it to exactly match the Angular CartItem interface format
        var items = cart.CartItems.Select(ci => new {
            product = new
            {
                id = ci.Product.Id,
                name = ci.Product.Name,
                price = ci.Product.Price,
                imageUrl = ci.Product.ImageUrl,
                stockQty = ci.Product.StockQty
            },
            quantity = ci.Quantity
        });

        return Ok(ApiResponse<object>.SuccessResponse(items));
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncCart([FromBody] List<SyncCartDto> items)
    {
        var userId = GetUserId();
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        // Clear out the old items in the database
        _context.CartItems.RemoveRange(cart.CartItems);

        // Save the exact current state of the Angular cart
        var newItems = items.Select(i => new CartItem
        {
            CartId = cart.Id,
            ProductId = i.ProductId,
            Quantity = i.Quantity
        });

        await _context.CartItems.AddRangeAsync(newItems);
        await _context.SaveChangesAsync();

        return Ok(ApiResponse<string>.SuccessResponse(null, "Cart synced with database"));
    }

    // ==========================================
    // LEGACY ENDPOINTS (Kept for compatibility)
    // ==========================================

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CartItem item)
    {
        var userId = GetUserId();
        var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        var product = await _context.Products.FindAsync(item.ProductId);
        if (product == null) return NotFound(new { message = "Product not found" });
        if (product.StockQty < item.Quantity) return BadRequest(new { message = "Not enough stock" });

        var existing = cart.CartItems.FirstOrDefault(ci => ci.ProductId == item.ProductId);
        if (existing != null)
        {
            existing.Quantity += item.Quantity;
            _context.CartItems.Update(existing);
        }
        else
        {
            item.CartId = cart.Id;
            await _context.CartItems.AddAsync(item);
        }

        await _context.SaveChangesAsync();
        return Ok(ApiResponse<string>.SuccessResponse(null, "Item added"));
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] CartItem item)
    {
        var userId = GetUserId();
        var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null) return NotFound();

        var existing = await _context.CartItems.FindAsync(item.Id);
        if (existing == null) return NotFound();
        var product = await _context.Products.FindAsync(existing.ProductId);
        if (product == null) return NotFound();
        if (product.StockQty < item.Quantity) return BadRequest(new { message = "Not enough stock" });

        existing.Quantity = item.Quantity;
        _context.CartItems.Update(existing);
        await _context.SaveChangesAsync();
        return Ok(ApiResponse<string>.SuccessResponse(null, "Updated"));
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> Remove([FromQuery] int id)
    {
        var userId = GetUserId();
        var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null) return NotFound();
        var existing = await _context.CartItems.FindAsync(id);
        if (existing == null) return NotFound();
        _context.CartItems.Remove(existing);
        await _context.SaveChangesAsync();
        return Ok(ApiResponse<string>.SuccessResponse(null, "Removed"));
    }
}

// DTO to receive the cart array from Angular
public class SyncCartDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}