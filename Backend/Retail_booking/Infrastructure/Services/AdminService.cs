using Microsoft.EntityFrameworkCore;
using RetailOrdering.Application.DTOs.Admin;
using RetailOrdering.Application.DTOs.User;
using RetailOrdering.Application.Interfaces;
using RetailOrdering.Infrastructure.Data;
using AutoMapper;

namespace RetailOrdering.Infrastructure.Services;

public class AdminService : IAdminService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AdminService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AdminStatsDto> GetStatsAsync()
    {
        var totalProducts = await _context.Products.CountAsync();
        var totalOrders = await _context.Orders.CountAsync();
        var totalUsers = await _context.Users.CountAsync();
        var totalRevenue = await _context.Orders.SumAsync(o => o.TotalAmount);
        return new AdminStatsDto { TotalProducts = totalProducts, TotalOrders = totalOrders, TotalUsers = totalUsers, TotalRevenue = totalRevenue };
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> CreateUserAsync(UserDto dto)
    {
        // 1. Look up the role the user selected from the dropdown
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == dto.Role);
        
        // Default to Customer (Id 2) if it somehow isn't found
        int roleId = role?.Id ?? 2; 

        var user = new RetailOrdering.Domain.Entities.User 
        { 
            Name = dto.Name, 
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("DefaultPass123!"), // Required by DB
            RoleId = roleId // 2. Assign the actual role ID!
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> UpdateUserAsync(int id, UserDto dto)
    {
        var u = await _context.Users.FindAsync(id);
        if (u == null) return false;
        u.Name = dto.Name;
        u.Email = dto.Email;
        //u.Role = dto.Role;
        _context.Users.Update(u);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var u = await _context.Users.FindAsync(id);
        if (u == null) return false;
        _context.Users.Remove(u);
        await _context.SaveChangesAsync();
        return true;
    }
}