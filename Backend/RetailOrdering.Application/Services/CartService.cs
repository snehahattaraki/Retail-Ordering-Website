using RetailOrdering.Application.DTOs.Cart;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class CartService : ICartService
    {
        public Task<CartDto> AddToCartAsync(int userId, AddToCartDto addToCartDto) => Task.FromResult(new CartDto());
        public Task ClearCartAsync(int userId) => Task.CompletedTask;
        public Task<CartDto> GetCartAsync(int userId) => Task.FromResult(new CartDto());
        public Task RemoveCartItemAsync(int userId, int itemId) => Task.CompletedTask;
        public Task<CartDto> UpdateCartItemAsync(int userId, int itemId, UpdateCartItemDto updateCartItemDto) => Task.FromResult(new CartDto());
    }
}
