using System;
using System.Threading.Tasks;
using RetailOrdering.Application.DTOs.Cart;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartResponseDto> GetCartAsync(Guid userId);
        Task<CartResponseDto> AddItemAsync(Guid userId, AddCartItemDto dto);
        Task<CartResponseDto> UpdateItemAsync(Guid userId, UpdateCartItemDto dto);
        Task<CartResponseDto> RemoveItemAsync(Guid userId, Guid productId);
    }
}
