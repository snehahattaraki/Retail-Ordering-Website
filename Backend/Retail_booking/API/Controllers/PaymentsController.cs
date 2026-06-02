using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailOrdering.API.Responses;
using RetailOrdering.Application.DTOs.Payment;
using RetailOrdering.Application.Interfaces;
using RetailOrdering.Infrastructure.Data;
using RetailOrdering.Domain.Entities;
using System.Security.Claims;

namespace RetailOrdering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly AppDbContext _context;
    private readonly IEmailService _emailService; // INJECT THE EMAIL SERVICE

    public PaymentsController(IPaymentService paymentService, AppDbContext context, IEmailService emailService)
    {
        _paymentService = paymentService;
        _context = context;
        _emailService = emailService;
    }

    [HttpPost("{orderId}")]
    public async Task<IActionResult> Pay(int orderId, [FromBody] PaymentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<string>.FailResponse("Invalid payment data"));

        var ok = await _paymentService.ProcessPaymentAsync(orderId, dto);
        if (!ok)
            return NotFound(ApiResponse<string>.FailResponse("Order not found"));

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (int.TryParse(userIdClaim, out int userId))
        {
            // 1. Fetch user data to get their email address
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return BadRequest(ApiResponse<string>.FailResponse("User tracking error"));

            // 2. Award Loyalty Points (50 points per successful order)
            var userPoints = await _context.LoyaltyPoints.FirstOrDefaultAsync(lp => lp.UserId == userId);
            if (userPoints == null)
            {
                userPoints = new LoyaltyPoint { UserId = userId, Points = 50 };
                await _context.LoyaltyPoints.AddAsync(userPoints);
            }
            else
            {
                userPoints.Points += 50;
                _context.LoyaltyPoints.Update(userPoints);
            }
            await _context.SaveChangesAsync();

            // 3. Construct a beautiful real-time HTML Invoice Email template
            string emailSubject = $"Order #{orderId} Confirmed! 🍕";
            string htmlContent = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #eee; border-radius: 10px;'>
                    <h2 style='color: #0d6efd;'>Thank you for your order, {user.Name}!</h2>
                    <p>Your payment has been successfully processed for <strong>Order #{orderId}</strong>.</p>
                    <hr style='border: 0; border-top: 1px solid #eee;' />
                    <p style='color: #198754; font-weight: bold;'>🎉 You earned 50 Loyalty Points on this purchase!</p>
                    <p style='color: #6c757d; font-size: 12px;'>We are preparing your meal now. Use the storefront dashboard to track delivery progress in real time.</p>
                </div>";

            try
            {
                // 4. Trigger email delivery. This automatically logs to EmailLogs table internally!
                await _emailService.SendEmailAsync(user.Email, emailSubject, htmlContent);
            }
            catch (Exception emailEx)
            {
                // Log email gateway exceptions to terminal but do not break the API response
                Console.WriteLine($"[EMAIL GATEWAY ERROR]: {emailEx.Message}");
            }
        }

        return Ok(ApiResponse<string>.SuccessResponse(null, "Payment processed successfully!"));
    }
}