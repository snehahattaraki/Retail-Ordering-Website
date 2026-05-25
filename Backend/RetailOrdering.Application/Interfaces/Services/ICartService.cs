using RetailOrdering.Application.DTOs.Cart;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(int userId);
        Task<CartDto> AddToCartAsync(int userId, AddToCartDto addToCartDto);
        Task<CartDto> UpdateCartItemAsync(int userId, int itemId, UpdateCartItemDto updateCartItemDto);
        Task RemoveCartItemAsync(int userId, int itemId);
        Task ClearCartAsync(int userId);
    }
}
