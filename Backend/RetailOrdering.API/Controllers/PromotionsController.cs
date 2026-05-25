using Microsoft.AspNetCore.Mvc;

namespace RetailOrdering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionsController : ControllerBase
    {
        /// <summary>
        /// Get all promotions
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get promotion by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get active promotions
        /// </summary>
        [HttpGet("active")]
        public async Task<IActionResult> GetActivePromotions()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create new promotion
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update promotion
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete promotion
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
