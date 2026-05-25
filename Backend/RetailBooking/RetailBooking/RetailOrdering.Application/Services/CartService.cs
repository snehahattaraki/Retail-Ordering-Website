using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RetailOrdering.Application.DTOs.Cart;
using RetailOrdering.Application.Interfaces.Repositories;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IGenericRepository<Domain.Entities.Cart> _cartRepo;
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
        private readonly IMapper _mapper;

        public CartService(IGenericRepository<Domain.Entities.Cart> cartRepo, IGenericRepository<Domain.Entities.Product> productRepo, IMapper mapper)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<CartResponseDto> AddItemAsync(Guid userId, AddCartItemDto dto)
        {
            var carts = await _cartRepo.FindAsync(c => c.UserId == userId);
            var cart = carts.FirstOrDefault() ?? new Domain.Entities.Cart { Id = Guid.NewGuid(), UserId = userId };
            var product = await _productRepo.GetByIdAsync(dto.ProductId);
            if (product == null) throw new Exception("Product not found");
            var item = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);
            if (item == null)
            {
                item = new Domain.Entities.CartItem { Id = Guid.NewGuid(), ProductId = dto.ProductId, Quantity = dto.Quantity, UnitPrice = product.Price };
                cart.Items.Add(item);
            }
            else
            {
                item.Quantity += dto.Quantity;
            }
            if (cart.Id == Guid.Empty) await _cartRepo.AddAsync(cart);
            else await _cartRepo.UpdateAsync(cart);
            return Map(cart);
        }

        public async Task<CartResponseDto> GetCartAsync(Guid userId)
        {
            var carts = await _cartRepo.FindAsync(c => c.UserId == userId);
            var cart = carts.FirstOrDefault() ?? new Domain.Entities.Cart { Id = Guid.NewGuid(), UserId = userId };
            return Map(cart);
        }

        public async Task<CartResponseDto> RemoveItemAsync(Guid userId, Guid productId)
        {
            var carts = await _cartRepo.FindAsync(c => c.UserId == userId);
            var cart = carts.FirstOrDefault() ?? throw new Exception("Cart not found");
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId) ?? throw new Exception("Item not found");
            cart.Items.Remove(item);
            await _cartRepo.UpdateAsync(cart);
            return Map(cart);
        }

        public async Task<CartResponseDto> UpdateItemAsync(Guid userId, UpdateCartItemDto dto)
        {
            var carts = await _cartRepo.FindAsync(c => c.UserId == userId);
            var cart = carts.FirstOrDefault() ?? throw new Exception("Cart not found");
            var item = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId) ?? throw new Exception("Item not found");
            item.Quantity = dto.Quantity;
            await _cartRepo.UpdateAsync(cart);
            return Map(cart);
        }

        private CartResponseDto Map(Domain.Entities.Cart cart)
        {
            var items = cart.Items.Select(i => new CartItemDto(i.ProductId, i.Quantity, i.UnitPrice));
            var sub = items.Sum(i => i.UnitPrice * i.Quantity);
            var tax = sub * 0.1m;
            return new CartResponseDto(cart.Id, cart.UserId, items, sub, tax, sub + tax);
        }
    }
}
