using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailOrdering.API.Responses;
using RetailOrdering.Application.DTOs.Inventory;
using RetailOrdering.Application.Interfaces;

namespace RetailOrdering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Management")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var items = await _inventoryService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<InventoryDto>>.SuccessResponse(items, "Inventory retrieved"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var item = await _inventoryService.GetByIdAsync(id);
            if (item == null)
                return NotFound(ApiResponse<string>.FailResponse("Product not found"));

            return Ok(ApiResponse<InventoryDto>.SuccessResponse(item, "Product retrieved"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InventoryDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid data"));

            var created = await _inventoryService.CreateAsync(dto);
            return Ok(ApiResponse<InventoryDto>.SuccessResponse(created, "Product created successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] InventoryDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid data"));

            var ok = await _inventoryService.UpdateAsync(id, dto);
            if (!ok)
                return NotFound(ApiResponse<string>.FailResponse("Product not found"));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Product updated successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
        }
    }

    [HttpPut("{id}/stock")]
    public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockDto dto)
    {
        try
        {
            var ok = await _inventoryService.UpdateStockAsync(id, dto.StockQty);
            if (!ok)
                return NotFound(ApiResponse<string>.FailResponse("Product not found"));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Stock updated successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var ok = await _inventoryService.DeleteAsync(id);
            if (!ok)
                return NotFound(ApiResponse<string>.FailResponse("Product not found"));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Product deleted successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.FailResponse($"Internal server error: {ex.Message}"));
        }
    }
}
// Data Transfer Object specifically for partial stock updates
public class UpdateStockDto
{
    public int StockQty { get; set; }
}