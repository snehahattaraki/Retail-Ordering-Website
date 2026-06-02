using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailOrdering.API.Responses;
using RetailOrdering.Application.DTOs.Order;
using RetailOrdering.Application.Interfaces;

namespace RetailOrdering.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        // Inject IOrderService instead of AppDbContext
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("Invalid user ID in token");
            return userId;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            try
            {
                var userId = GetUserId();
                // Use the service to get DTOs (solves the 500 cycle error)
                var orders = await _orderService.GetUserOrdersAsync(userId);
                return Ok(ApiResponse<object>.SuccessResponse(orders, "User orders retrieved"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var userId = GetUserId();
                var order = await _orderService.GetOrderAsync(id, userId);

                if (order == null)
                    return NotFound(ApiResponse<string>.FailResponse("Order not found"));

                return Ok(ApiResponse<object>.SuccessResponse(order, "Order retrieved"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
            }
        }

        [HttpPost("place")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDto dto)
        {
            try
            {
                var userId = GetUserId();
                var orderId = await _orderService.PlaceOrderAsync(userId, dto);
                return Ok(ApiResponse<object>.SuccessResponse(new { orderId }, "Order placed successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<string>.FailResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
            }
        }

        [HttpPost("{id}/reorder")]
        public async Task<IActionResult> ReorderItems(int id)
        {
            try
            {
                var userId = GetUserId();
                var success = await _orderService.ReorderAsync(userId, id);

                if (!success)
                    return NotFound(ApiResponse<string>.FailResponse("Original order not found or could not be reordered"));

                return Ok(ApiResponse<object>.SuccessResponse(null, "Reorder placed successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
            }
        }
    }
}