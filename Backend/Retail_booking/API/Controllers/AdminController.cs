using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailOrdering.API.Responses;
using RetailOrdering.Application.Interfaces;
using RetailOrdering.Infrastructure.Data;

namespace RetailOrdering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IAdminService _adminService;

    public AdminController(AppDbContext context, IAdminService adminService)
    {
        _context = context;
        _adminService = adminService;
    }

    /// <summary>
    /// Get dashboard statistics
    /// </summary>
    [HttpGet("stats")]
    [Authorize(Roles = "Admin,Management")]
    public async Task<IActionResult> GetStats()
    {
        try
        {
            var totalProducts = await _context.Products.CountAsync();
            var totalOrders = await _context.Orders.CountAsync();
            var totalUsers = await _context.Users.CountAsync();
            var totalRevenue = await _context.Orders.SumAsync(o => (decimal?)o.TotalAmount) ?? 0m;

            var stats = new
            {
                totalProducts,
                totalOrders,
                totalUsers,
                totalRevenue
            };

            return Ok(ApiResponse<object>.SuccessResponse(stats, "Admin stats retrieved"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
        }
    }

    [HttpGet("roles")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetRoles()
    {
        try
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(ApiResponse<object>.SuccessResponse(roles, "Roles retrieved"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
        }
    }
 

    /// <summary>
    /// Get all users with their roles
    /// </summary>
    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Email,
                    role = u.Role != null ? u.Role.Name : "Unknown"
                })
                .ToListAsync();

            return Ok(ApiResponse<object>.SuccessResponse(users, "Users retrieved"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
        }
    }

    [HttpPost("users")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser([FromBody] RetailOrdering.Application.DTOs.User.UserDto dto)
    {
        try
        {
            var user = await _adminService.CreateUserAsync(dto);
            return Ok(ApiResponse<object>.SuccessResponse(user, "User created"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse(ex.Message));
        }
    }

    [HttpPut("users/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] RetailOrdering.Application.DTOs.User.UserDto dto)
    {
        try
        {
            var success = await _adminService.UpdateUserAsync(id, dto);
            if (!success) return NotFound(ApiResponse<string>.FailResponse("User not found"));
            return Ok(ApiResponse<string>.SuccessResponse(null, "User updated"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse(ex.Message));
        }
    }

    [HttpDelete("users/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var success = await _adminService.DeleteUserAsync(id);
            if (!success) return NotFound(ApiResponse<string>.FailResponse("User not found"));
            return Ok(ApiResponse<string>.SuccessResponse(null, "User deleted"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse(ex.Message));
        }
    }

    /// <summary>
    /// Get previous dashboard view
    /// </summary>
    [HttpGet("dashboard")]
    [Authorize(Roles = "Admin,Management")]
    public async Task<IActionResult> Dashboard()
    {
        try
        {
            var totalProducts = await _context.Products.CountAsync();
            var totalOrders = await _context.Orders.CountAsync();
            var totalUsers = await _context.Users.CountAsync();
            var totalRevenue = await _context.Orders.SumAsync(o => (decimal?)o.TotalAmount) ?? 0m;

            var dashboardData = new
            {
                totalProducts,
                totalOrders,
                totalUsers,
                totalRevenue,
                lowStockProducts = await _context.Products.Where(p => p.StockQty < 10).CountAsync()
            };

            return Ok(ApiResponse<object>.SuccessResponse(dashboardData, "Dashboard data retrieved"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
        }
    }

    [HttpGet("products/low-stock")]
    [Authorize(Roles = "Admin,Management")]
    public async Task<IActionResult> LowStockProducts()
    {
        try
        {
            var products = await _context.Products
                .Where(p => p.StockQty < 10)
                .Select(p => new { p.Id, p.Name, p.StockQty })
                .ToListAsync();

            return Ok(ApiResponse<IEnumerable<object>>.SuccessResponse(products, "Low stock products retrieved"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
        }
    }

    [HttpGet("orders/status")]
    [Authorize(Roles = "Admin,Management,Staff")]
    public async Task<IActionResult> OrdersByStatus()
    {
        try
        {
            var ordersByStatus = await _context.Orders
                .GroupBy(o => o.Status)
                .Select(g => new { status = g.Key.ToString(), count = g.Count() })
                .ToListAsync();

            return Ok(ApiResponse<IEnumerable<object>>.SuccessResponse(ordersByStatus, "Orders by status retrieved"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
        }
    }

    [HttpPut("orders/{id}/status")]
    [Authorize(Roles = "Admin,Management,Staff")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] AdminUpdateOrderStatusDto dto)
    {
        try
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound(ApiResponse<string>.FailResponse("Order not found"));

            // Made this case-insensitive by adding 'true' as the second parameter
            if (Enum.TryParse<RetailOrdering.Domain.Entities.OrderStatus>(dto.Status, true, out var status))
            {
                order.Status = status;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return Ok(ApiResponse<string>.SuccessResponse(null, "Order status updated"));
            }

            return BadRequest(ApiResponse<string>.FailResponse("Invalid status"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
        }
    }

    // ==========================================
    // STAFF, MANAGEMENT & ADMIN ENDPOINTS (Order Execution)
    // ==========================================

    // ADD THIS NEW ENDPOINT
    [HttpGet("orders")]
    [Authorize(Roles = "Admin,Management,Staff")]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            // Fetch ALL orders in the database for the admin panel
            var orders = await _context.Orders
                .Select(o => new
                {
                    id = o.Id,
                    userId = o.UserId,
                    totalAmount = o.TotalAmount,
                    status = o.Status.ToString(),
                    createdAt = o.CreatedAt
                })
                .OrderByDescending(o => o.createdAt)
                .ToListAsync();

            return Ok(ApiResponse<object>.SuccessResponse(orders, "All store orders retrieved"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse(ex.Message));
        }
    }
}

public class AdminUpdateOrderStatusDto
{
    public string Status { get; set; } = null!;
}