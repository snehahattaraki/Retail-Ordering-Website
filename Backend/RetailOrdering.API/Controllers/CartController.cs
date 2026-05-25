using Microsoft.AspNetCore.Mvc;

namespace RetailOrdering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        /// <summary>
        /// Get cart for user
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add item to cart
        /// </summary>
        [HttpPost("{userId}/items")]
        public async Task<IActionResult> AddToCart(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update cart item
        /// </summary>
        [HttpPut("{userId}/items/{itemId}")]
        public async Task<IActionResult> UpdateCartItem(int userId, int itemId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove item from cart
        /// </summary>
        [HttpDelete("{userId}/items/{itemId}")]
        public async Task<IActionResult> RemoveCartItem(int userId, int itemId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clear cart
        /// </summary>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
