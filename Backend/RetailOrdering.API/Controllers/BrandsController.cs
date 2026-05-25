using Microsoft.AspNetCore.Mvc;

namespace RetailOrdering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        /// <summary>
        /// Get all brands
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get brand by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create new brand
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update brand
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete brand
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
