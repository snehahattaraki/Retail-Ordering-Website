using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailOrdering.Infrastructure.Data;
using RetailOrdering.Domain.Entities;
using RetailOrdering.API.Responses;


namespace Retail_booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackagingController : Controller
    {
        private readonly AppDbContext _context;

        public PackagingController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Public endpoint to load all packagingss for storefront filters
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var packagings = await _context.Packagings.OrderBy(b => b.Type).ToListAsync();
            
            return Ok(ApiResponse<object>.SuccessResponse(packagings, "Packaging loaded"));
        }

        // 2. Admin/Management only endpoint to add a new packagings
        [Authorize(Roles = "Admin,Management")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Packaging packaging)
        {
            if (string.IsNullOrWhiteSpace(packaging.Type))
                return BadRequest(ApiResponse<string>.FailResponse("Packagings Type is required"));

            await _context.Packagings.AddAsync(packaging);
            await _context.SaveChangesAsync();
            return Ok(ApiResponse<Packaging>.SuccessResponse(packaging, "Packagings added successfully"));
        }

        // 3. Admin only endpoint to remove a packagings
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var packaging = await _context.Packagings.FindAsync(id);
            if (packaging == null) return NotFound(ApiResponse<string>.FailResponse("Packagings not found"));

            _context.Packagings.Remove(packaging);
            await _context.SaveChangesAsync();
            return Ok(ApiResponse<string>.SuccessResponse(null, "Packaging removed"));
        }
    }
}
