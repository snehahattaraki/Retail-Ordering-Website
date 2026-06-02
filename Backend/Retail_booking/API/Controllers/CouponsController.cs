using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailOrdering.Infrastructure.Data;
using RetailOrdering.API.Responses;

namespace RetailOrdering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CouponsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CouponsController(AppDbContext context)
    {
        _context = context;
    }

    private int GetUserId() => int.Parse(User.Claims.First(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);

    [HttpPost("apply")]
    public async Task<IActionResult> Apply([FromBody] ApplyCouponDto request)
    {
        if (string.IsNullOrWhiteSpace(request?.Code))
            return BadRequest(ApiResponse<string>.FailResponse("Coupon code is required"));

        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == request.Code);
        if (coupon == null) return NotFound(ApiResponse<string>.FailResponse("Coupon not found or expired"));

        // If the frontend sends a subtotal of 0
        if (request.Subtotal <= 0)
            return BadRequest(ApiResponse<string>.FailResponse("Add items to your cart first"));

        var total = request.Subtotal;
        var discounted = total - coupon.Discount;

        // Prevent the total from going into the negatives!
        if (discounted < 0) discounted = 0;

        return Ok(ApiResponse<object>.SuccessResponse(new { total, discounted }));
    }
}

// Updated DTO to expect the Subtotal from Angular
public class ApplyCouponDto
{
    public string Code { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
}