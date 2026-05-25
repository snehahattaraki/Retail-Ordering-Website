using Microsoft.AspNetCore.Mvc;

namespace RetailOrdering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoyaltyController : ControllerBase
    {
        /// <summary>
        /// Get loyalty points for user
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserLoyaltyPoints(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get loyalty history
        /// </summary>
        [HttpGet("user/{userId}/history")]
        public async Task<IActionResult> GetLoyaltyHistory(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add loyalty points
        /// </summary>
        [HttpPost("add-points")]
        public async Task<IActionResult> AddPoints()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Redeem loyalty points
        /// </summary>
        [HttpPost("redeem-points")]
        public async Task<IActionResult> RedeemPoints()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get loyalty balance
        /// </summary>
        [HttpGet("user/{userId}/balance")]
        public async Task<IActionResult> GetLoyaltyBalance(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
