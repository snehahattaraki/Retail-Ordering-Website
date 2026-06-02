using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RetailOrdering.Application.DTOs.Auth;
using RetailOrdering.Application.Interfaces;
using RetailOrdering.Infrastructure.Data;
using RetailOrdering.Domain.Entities;
using BCrypt.Net;

namespace RetailOrdering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthController(AppDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest(RetailOrdering.API.Responses.ApiResponse<string>.FailResponse("Email already exists"));

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            RoleId = 2 // User
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var roleName = "Customer";

        // FIX: Pass user.Name as the 4th parameter
        var token = _jwtService.GenerateToken(user.Id, user.Email, roleName, user.Name);
        var roles = new[] { roleName };

        return Ok(RetailOrdering.API.Responses.ApiResponse<object>.SuccessResponse(new { token, role = roleName, roles = roles, fullName = user.Name }, "Registration successful"));
    }

    /// <summary>
    /// Login user and return JWT
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null) return Unauthorized(RetailOrdering.API.Responses.ApiResponse<string>.FailResponse("Invalid credentials"));

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized(RetailOrdering.API.Responses.ApiResponse<string>.FailResponse("Invalid credentials"));

        var roleNameDb = (await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId))?.Name ?? "User";
        var roleName = roleNameDb == "User" ? "Customer" : roleNameDb;

        // FIX: Pass user.Name as the 4th parameter
        var token = _jwtService.GenerateToken(user.Id, user.Email, roleName, user.Name);
        var roles = new[] { roleName };

        return Ok(RetailOrdering.API.Responses.ApiResponse<object>.SuccessResponse(new { token = token, role = roleName, roles = roles, fullName = user.Name }, "Login successful"));
    }

    /// <summary>
    /// Get current user profile
    /// </summary>
    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized(RetailOrdering.API.Responses.ApiResponse<string>.FailResponse("Unauthorized"));

        var user = await _context.Users.FindAsync(userId);
        if (user == null) return NotFound(RetailOrdering.API.Responses.ApiResponse<string>.FailResponse("User not found"));

        var roleNameDb = (await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId))?.Name ?? "User";
        var roleName = roleNameDb == "User" ? "Customer" : roleNameDb;
        var roles = new[] { roleName };

        return Ok(RetailOrdering.API.Responses.ApiResponse<object>.SuccessResponse(new { id = user.Id, name = user.Name, email = user.Email, role = roleName, roles = roles }));
    }
}